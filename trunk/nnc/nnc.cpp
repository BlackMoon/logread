// nnc.cpp
#include <stdio.h>
#include <windows.h>
#include "resource.h"

#define NNC					"NNC"
#define ZERO				"0"

char						line[16] = {0};

LRESULT WINAPI DialogProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
	switch (uMsg) 
    { 			
		case WM_INITDIALOG: 
		{
			SetWindowText(GetDlgItem(hWnd, IDE_Z1), ZERO);
			SetWindowText(GetDlgItem(hWnd, IDE_Z2), ZERO);
			SetWindowText(GetDlgItem(hWnd, IDE_TRAN), ZERO);
			
			SetWindowText(GetDlgItem(hWnd, IDE_NX), ZERO);
			SetWindowText(GetDlgItem(hWnd, IDE_NY), ZERO);
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
				case ID_RUN:
				{
					char line[16] = {0}, file[MAX_PATH] = {0};
					int i, j, nx, ny, z1, z2;
					float tran;

					GetWindowText(GetDlgItem(hWnd, IDE_NX), line, 16);
					sscanf_s(line, "%d", &nx);

					GetWindowText(GetDlgItem(hWnd, IDE_NY), line, 16);
					sscanf_s(line, "%d", &ny);

					GetWindowText(GetDlgItem(hWnd, IDE_Z1), line, 16);
					sscanf_s(line, "%d", &z1);

					GetWindowText(GetDlgItem(hWnd, IDE_Z2), line, 16);
					sscanf_s(line, "%d", &z2);

					GetWindowText(GetDlgItem(hWnd, IDE_TRAN), line, 16);
					sscanf_s(line, "%f", &tran);

					sprintf_s(file, MAX_PATH, "%s_%d_%d.txt", NNC, z1, z2); 
					
					FILE* stream;
					if (0 == fopen_s(&stream, file, "wt")) {				
						for (i = 1; i < nx + 1; i++)
						{
							for (j = 1; j < ny + 1; j++)
							{
								fprintf_s(stream, "%d\t%d\t%d\t\t\t%d\t%d\t%d\t%.3f\n", i, j, z1, i, j, z2, tran); 
							}
						}
						fprintf_s(stream, "/"); 

						fclose(stream);						
						sprintf_s(line, 16, "Ok");
					}
					else sprintf_s(line, 16, "Failed"); 
					MessageBox(hWnd, line, NNC, MB_ICONINFORMATION | MB_OK);
				}
				return 1;
			}				
		}
    } 
    return 0; 
}
int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, PSTR lpCmdLine, int nCmdShow)
{	
	HWND hwnd = CreateDialog(hInstance, MAKEINTRESOURCE(IDD_NNC), 0, (DLGPROC)DialogProc);	
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
	return 0;	
}