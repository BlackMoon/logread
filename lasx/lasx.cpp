// lasx.cpp
#pragma comment(lib, "shell32")

#include "dbobj.h"
#include <shlobj.h>
#include "resource.h"

HWND						hCBox = 0;
CData*						pdata = 0;

bool getAlias(PSTR _Dst, UINT _DstSize, PCSTR _Src)
{
	char symb;
	int i, len = (int)strlen(_Src);		
	if (strstr(_Src, "Gr") == 0) return 0;
	
	for (i = --len; i > 0; i--)
	{
		symb = _Src[i];
		if (symb == 0x5c) break;	
	}
	strncpy_s(_Dst, _DstSize, _Src + (i + 1), len - i);
	return 1;
}
INT_PTR CALLBACK DialogProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{	
	switch (uMsg) 
    { 		
		case WM_INITDIALOG:
		{  
			hCBox = GetDlgItem(hWnd, IDC_HORZ);
			return 1;            
		}		
		case WM_CLOSE:
		case WM_DESTROY: 
		{
			PostQuitMessage(0);
			return 1;
		}
		case WM_COMMAND:
		{
			switch LOWORD(wParam)
			{
				case IDB_RUN:
				{
					char line[16];
					SendMessage(hCBox, CB_GETLBTEXT, 0, (LPARAM)line);
					pdata->run(atoi(line));
					break;
				}
				case IDB_OPEN:
				{
					BROWSEINFO bi = {0};
					bi.hwndOwner = hWnd;
					bi.ulFlags = BIF_RETURNONLYFSDIRS;
					bi.lpszTitle = "Выберите папку";
					LPITEMIDLIST piidl = SHBrowseForFolder(&bi);
					if (piidl)
					{
						char dir[MAX_PATH];	
						if (SHGetPathFromIDList(piidl, dir)) 
						{
							getAlias(pdata->alias, 8, dir);							
							if (pdata->open(dir))
							{		
								pdata->load(hCBox);								
							}
						}
					}
					break;
				}				

			}
			return 1;
		}
	}
	return 0;
}

int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow)
{
	CoInitialize(0);
	pdata = new CData();

	HWND hwnd = CreateDialog(hInstance, MAKEINTRESOURCE(IDD_LASX), 0, (DLGPROC)DialogProc);	
	ShowWindow(hwnd, SW_SHOW); 	

	MSG msg;
	while (GetMessage(&msg, 0, 0, 0))
	{ 
		if (!IsWindow(hwnd) || !IsDialogMessage(hwnd, &msg))			
		{ 
			TranslateMessage(&msg); 
			DispatchMessage(&msg); 
		} 
	} 

	delete pdata;
	CoUninitialize();
    return 0;	
}