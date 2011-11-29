// union.cpp
#pragma comment(lib, "comctl32")
#pragma comment(lib, "shlwapi")

#include <comdef.h>
#include <commctrl.h>
#include <shlobj.h>
#include <shlwapi.h>
#include <vector>
#include <windows.h>
#include "resource.h"

#define BMP_H				36
#define	CHECK_H				11
#define LEN					8

const SIZE					quad = {200, 200};

bool						bdraw = 0;
size_t						act_size = 0;									// active size
RECT						_rc = {0};
SIZE						_sz = {0};										// bitmap size
HBITMAP						hbm_check, hbm_clear, hbm_open, hbm_run, hbm_save, hbm_uncheck;
HINSTANCE					hInst = 0;
HWND						hLBox = 0, hStat = 0;
WNDPROC						lpOldEditProc;

#pragma pack(push, 1)
typedef class xItem
{
public:
	xItem(){active = 1;}
	~xItem(){if (hBmp) DeleteObject(hBmp);}
// Attributes	
public:
	bool					active;
	HBITMAP					hBmp;
	xItem& operator = (const xItem& xi)
	{
		active = xi.active;
		hBmp = xi.hBmp;
		return *this;
	}
}*pxItem;
#pragma pack(pop)
std::vector<pxItem>			xItems;

bool saveImage(HBITMAP hbmp, PCSTR pszFileName)
{
	bool bres = 0; 	

    IStreamPtr stm;
    if (SUCCEEDED(SHCreateStreamOnFile(pszFileName, STGM_WRITE | STGM_CREATE, &stm)))
    {
        IPicturePtr pic;
        PICTDESC pd;
        pd.picType = PICTYPE_BITMAP;
        pd.bmp.hbitmap = hbmp;
        pd.bmp.hpal = 0;
        if (SUCCEEDED(OleCreatePictureIndirect(&pd, __uuidof(IPicture), TRUE, (PVOID*)&pic)))
        {
            long cb = 0;
            bres = SUCCEEDED(pic->SaveAsFile(stm, 1, &cb));
        }
    }    
    return bres;
}
BOOL TranspBlt(HDC hdcDest, int nXOriginDest, int nYOriginDest, int nWidthDest, int nHeightDest, 
			   HDC hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc, UINT crTransparent)
{
	BOOL bres = 1;
	
	if (!hdcDest || !hdcSrc) return 0;	
	COLORREF crBack = SetBkColor(hdcDest, crTransparent);	
	HBITMAP bmp = CreateBitmap(nWidthSrc, nHeightSrc, 1, 1, 0); 
	HDC maskDC = CreateCompatibleDC(hdcDest);

	try
	{
		if (!maskDC) throw;		
		SelectObject(maskDC, bmp);		
		SetBkColor(maskDC, crTransparent);
		
		if (!BitBlt(maskDC, 0, 0, nWidthSrc, nHeightSrc, hdcSrc, 0, 0, SRCCOPY)) throw;			
		if (!BitBlt(hdcDest, nXOriginDest, nYOriginDest, nWidthDest, nHeightDest, hdcSrc, 0, 0, SRCINVERT)) throw;
		if (!BitBlt(hdcDest, nXOriginDest, nYOriginDest, nWidthDest, nHeightDest, maskDC, 0, 0, SRCAND)) throw;
		if (!BitBlt(hdcDest, nXOriginDest, nYOriginDest, nWidthDest, nHeightDest, hdcSrc, 0, 0, SRCINVERT)) throw;	
	}
	catch (...)
	{
		bres = 0;
	}	
	SetBkColor(hdcDest, crBack);	

	DeleteObject(bmp);
	DeleteDC(maskDC);

	return bres;
}
void DrawGray(HDC hdc, int x0, int y0, int x1, int y1) 			  
{	
	int i, j;
	BYTE r, g, b, gray;
	COLORREF rgb;
	for (i = x0; i < x1; i++)
	{
		for (j = y0; j < y1; j++)
		{
			rgb = GetPixel(hdc, i, j);
			r = LOBYTE(rgb);
			g = LOBYTE((rgb) >> 0x8); 
			b = LOBYTE((rgb) >> 0x10);
			gray = (BYTE)(0.299f * r + 0.587f * g + 0.114f * b);						
			SetPixel(hdc, i, j, RGB(gray, gray, gray));
		}
	}	
}
BOOL DrawDisable(HDC hdcDest, int nXOriginDest, int nYOriginDest, int nWidthDest, int nHeightDest, 
			     HDC hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc, UINT crTransparent)
{
	BOOL bres = 1;
	if (!hdcDest || !hdcSrc) return 0;	

	COLORREF crBack = SetBkColor(hdcDest, GetSysColor(COLOR_BTNFACE));	
	HBITMAP bmpMask = CreateBitmap(nWidthSrc, nWidthSrc, 1, 0, 0),			
			bmpShadow = CreateCompatibleBitmap(hdcDest, nWidthSrc, nHeightSrc);

	HDC maskDC = CreateCompatibleDC(hdcDest),		
		shadowDC = CreateCompatibleDC(hdcDest);    

	RECT rcDest = {nXOriginDest, nYOriginDest, nXOriginDest + nWidthDest, nYOriginDest + nHeightDest},
		 rcShadow = {0, 0, nWidthDest, nHeightDest};

	try
	{
		SelectObject(maskDC, bmpMask);	
		if (!BitBlt(maskDC, 0, 0, nWidthSrc, nHeightSrc, hdcSrc, nXOriginSrc, nYOriginSrc, SRCCOPY)) throw;		
		//
		SelectObject(shadowDC, bmpShadow);      		
		SetBkColor(shadowDC, GetSysColor(COLOR_BTNSHADOW));	
		ExtTextOut(shadowDC, 0, 0, ETO_OPAQUE, &rcShadow, 0, 0, 0);		
		if (!BitBlt(shadowDC, 0, 0, nWidthSrc, nHeightSrc, maskDC, 0, 0, SRCINVERT)) throw;		
		//		
		ExtTextOut(hdcDest, 0, 0, ETO_OPAQUE, &rcDest, 0, 0, 0);	
		SetTextColor(hdcDest, GetSysColor(COLOR_BTNHIGHLIGHT)); 

		if (!BitBlt(hdcDest, nXOriginDest + 1, nYOriginDest + 1, nWidthDest, nHeightDest, maskDC, 0, 0, SRCPAINT)) throw;  	
		if (!BitBlt(hdcDest, nXOriginDest, nYOriginDest, nWidthDest, nHeightDest, shadowDC, 0, 0, SRCINVERT)) throw;
	}
	catch(...)
	{
		bres = 0;
	}
	SetBkColor(hdcDest, crBack);	

	DeleteDC(maskDC);	
	DeleteDC(shadowDC);

	DeleteObject(bmpMask);
	DeleteObject(bmpShadow);

	return bres;
}
void DrawItem(LPDRAWITEMSTRUCT lpdis)
{
	BITMAP bm = {0};
	HDC hdc = lpdis->hDC;	

	switch (lpdis->CtlType)
	{
		case ODT_BUTTON:
		{
			HDC memDC = CreateCompatibleDC(hdc);
			UINT state = DFCS_BUTTONPUSH;	

			if (lpdis->itemState & ODS_SELECTED) state |= DFCS_PUSHED;
			DrawFrameControl(hdc, &lpdis->rcItem, DFC_BUTTON, state);
	
			if (lpdis->itemState & ODS_FOCUS)
			{		
				lpdis->rcItem.left += 2;
				lpdis->rcItem.top += 2;
				lpdis->rcItem.right -= 2;
				lpdis->rcItem.bottom -= 2;
				DrawFocusRect(hdc, &lpdis->rcItem);
			}
			
			COLORREF crMask;
			HGDIOBJ gdiObj = 0;	
			switch (lpdis->CtlID)
			{
				case ID_CLEAR:				
				{			
					gdiObj = SelectObject(memDC, hbm_clear);	
					crMask = GetPixel(memDC, 1, 1);

					GetObject(hbm_clear, sizeof(BITMAP), &bm);				
					if (lpdis->itemState & ODS_DISABLED) DrawDisable(hdc, 3, 3, bm.bmWidth, bm.bmHeight, memDC, 0, 0, bm.bmWidth, bm.bmHeight, crMask);
					else TranspBlt(hdc, 3, 3, bm.bmWidth, bm.bmHeight, memDC, 0, 0, bm.bmWidth, bm.bmHeight, crMask); 

					break;
				}
				case ID_OPEN:				
				{			
					gdiObj = SelectObject(memDC, hbm_open);	
					crMask = GetPixel(memDC, 1, 1);

					GetObject(hbm_open, sizeof(BITMAP), &bm);				
					if (lpdis->itemState & ODS_DISABLED) DrawDisable(hdc, 3, 3, bm.bmWidth, bm.bmHeight, memDC, 0, 0, bm.bmWidth, bm.bmHeight, crMask);
					else TranspBlt(hdc, 3, 3, bm.bmWidth, bm.bmHeight, memDC, 0, 0, bm.bmWidth, bm.bmHeight, crMask); 

					break;
				}
				case ID_RUN:				
				{			
					gdiObj = SelectObject(memDC, hbm_run);	
					crMask = GetPixel(memDC, 1, 1);

					GetObject(hbm_run, sizeof(BITMAP), &bm);				
					if (lpdis->itemState & ODS_DISABLED) DrawDisable(hdc, 3, 3, bm.bmWidth, bm.bmHeight, memDC, 0, 0, bm.bmWidth, bm.bmHeight, crMask);
					else TranspBlt(hdc, 3, 3, bm.bmWidth, bm.bmHeight, memDC, 0, 0, bm.bmWidth, bm.bmHeight, crMask); 

					break;
				}
				case ID_SAVE:				
				{			
					gdiObj = SelectObject(memDC, hbm_save);	
					crMask = GetPixel(memDC, 1, 1);

					GetObject(hbm_save, sizeof(BITMAP), &bm);				
					if (lpdis->itemState & ODS_DISABLED) DrawDisable(hdc, 3, 3, bm.bmWidth, bm.bmHeight, memDC, 0, 0, bm.bmWidth, bm.bmHeight, crMask);
					else TranspBlt(hdc, 3, 3, bm.bmWidth, bm.bmHeight, memDC, 0, 0, bm.bmWidth, bm.bmHeight, crMask); 

					break;
				}
			}
			DeleteDC(memDC);
			break;
		}
		case ODT_LISTBOX:
		{			
			if (lpdis->itemID == -1) return;

			switch (lpdis->itemAction) 
			{
				case ODA_SELECT: 
				case ODA_DRAWENTIRE: 
				{	
					HDC bmpDC = CreateCompatibleDC(hdc),
						checkDC = CreateCompatibleDC(hdc);					
			
					pxItem pxi = (pxItem)SendMessage(lpdis->hwndItem, LB_GETITEMDATA, lpdis->itemID, 0);			
			
					HBITMAP hbmp = pxi->hBmp;
					HGDIOBJ gdiBmp = SelectObject(bmpDC, hbmp), 
						    gdiCheck = SelectObject(checkDC, pxi->active ? hbm_check : hbm_uncheck);
					
					GetObject(hbmp, sizeof(BITMAP), &bm);			
			
					RECT rBmp, rCheck, rTotal;
					rBmp = rCheck = rTotal = lpdis->rcItem;
					rBmp.left += CHECK_H << 1;
			
					rCheck.left += (rBmp.left - rCheck.left - CHECK_H) >> 1;
					rCheck.top += (rCheck.bottom - rCheck.top - CHECK_H) >> 1; 					
			
					PatBlt(hdc, rTotal.left, rTotal.top, rTotal.right - rTotal.left, rTotal.bottom - rTotal.top, WHITENESS);		// clear			
					BitBlt(hdc, rCheck.left, rCheck.top, CHECK_H, CHECK_H, checkDC, 0, 0, SRCCOPY);			
			
					StretchBlt(hdc, rBmp.left + 1, rBmp.top + 1, rBmp.right - rBmp.left - 2, rBmp.bottom - rBmp.top - 2, bmpDC, 0, 0, bm.bmWidth, bm.bmHeight, SRCCOPY);						
					if (!pxi->active) DrawGray(hdc, rBmp.left + 1, rBmp.top + 1, rBmp.right - 1, rBmp.bottom - 1);			
					// focus
					if (lpdis->itemState & ODS_SELECTED) DrawFocusRect(hdc, &rTotal);     						

					DeleteDC(bmpDC);
					DeleteDC(checkDC);			
					break;
				}
				case ODA_FOCUS: break; 	
			}
			break;
		}		
	}	
}
void addItem(pxItem pxi)
{
	int index = SendMessage(hLBox, LB_ADDSTRING, 0, 0);			
	SendMessage(hLBox, LB_SETITEMDATA, index, (LPARAM)pxi);
	xItems.push_back(pxi);							
	InvalidateRect(hLBox, 0, 0);
}
void clear()
{	
	for (size_t i = 0; i < xItems.size(); i++)
	{
		delete xItems[i];
	}
	xItems.clear();	
}
void drawImage(HDC dc, int w, int h, int offset = 3)
{
	HDC bmpDC = CreateCompatibleDC(dc);		
	HGDIOBJ gdiBmp = 0;
	
	pxItem pxi;	
	int _h = h / act_size;

	BITMAP bm;
	RECT rBmp;				

	rBmp.left = rBmp.top = 0;
	rBmp.right = w - offset;
	rBmp.bottom = _h - offset;

	for (size_t i = 0; i < xItems.size(); i++)
	{
		pxi = xItems[i];
		if (pxi->active)
		{				
			gdiBmp = SelectObject(bmpDC, pxi->hBmp);
			GetObject(pxi->hBmp, sizeof(BITMAP), &bm);			

			StretchBlt(dc, rBmp.left, rBmp.top, rBmp.right, _h, bmpDC, 0, 0, bm.bmWidth, bm.bmHeight, SRCCOPY);
			rBmp.top += _h - offset;			
		}
	}
	DeleteDC(bmpDC);		
}
HBITMAP getImage(HWND hWnd, int w, int h)
{
	HDC hdc0 = GetDC(hWnd), 
		hdc1 = CreateCompatibleDC(hdc0);

    HBITMAP hbm0 = CreateCompatibleBitmap(hdc0, w, h);
	HGDIOBJ gdi = SelectObject(hdc1, hbm0); 			
	
	drawImage(hdc1, w, h, 0);			
	DeleteDC(hdc1);	
	
	return hbm0;
}

void enable(HWND hDlg, int index)
{
	HWND hDown = GetDlgItem(hDlg, ID_DOWN), 
		 hUp = GetDlgItem(hDlg, ID_UP);

	EnableWindow(hUp, index != 0);
	EnableWindow(hDown, index != xItems.size() - 1);
}
void swap(int index1, int index2)
{
	pxItem pxi1 = (pxItem)SendMessage(hLBox, LB_GETITEMDATA, index1, 0),
		   pxi2 = (pxItem)SendMessage(hLBox, LB_GETITEMDATA, index2, 0); 	
	
	SendMessage(hLBox, LB_SETITEMDATA, index1, (LPARAM)pxi2);
	xItems[index1] = pxi2;

	SendMessage(hLBox, LB_SETITEMDATA, index2, (LPARAM)pxi1);
	xItems[index2] = pxi1;	

	SendMessage(hLBox, LB_SETCURSEL, index2, 0);
	InvalidateRect(hLBox, 0, 0);
}
LRESULT WINAPI EditProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{		
	if (uMsg == WM_PAINT)
	{			
		RECT rc;
		GetClientRect(hWnd, &rc);

		HBRUSH br = CreateSolidBrush(GetSysColor(COLOR_WINDOW));		
		HDC hdc = GetDC(hWnd);
		
		PAINTSTRUCT ps;	
		BeginPaint(hWnd, &ps);
		FillRect(hdc, &rc, br);
		
		if (bdraw) drawImage(hdc, _rc.right - _rc.left, _rc.bottom - _rc.top);

		EndPaint(hWnd, &ps);
		return 0L;
	}
    return CallWindowProc(lpOldEditProc, hWnd, uMsg, wParam, lParam);  
}
LRESULT WINAPI DialogProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{		
	switch (uMsg) 
    { 			
		case WM_INITDIALOG:
		{   			
			hLBox = GetDlgItem(hWnd, IDL_IMAGES);
			hStat = GetDlgItem(hWnd, IDS_PIC);
			lpOldEditProc = (WNDPROC)SetWindowLong(hStat, GWL_WNDPROC, (LONG)EditProc);			

			hbm_check = LoadBitmap(hInst, MAKEINTRESOURCE(IDB_CHECK));
			hbm_clear = LoadBitmap(hInst, MAKEINTRESOURCE(IDB_CLEAR));
			hbm_open = LoadBitmap(hInst, MAKEINTRESOURCE(IDB_OPEN));
			hbm_run = LoadBitmap(hInst, MAKEINTRESOURCE(IDB_RUN));
			hbm_save = LoadBitmap(hInst, MAKEINTRESOURCE(IDB_SAVE));
			hbm_uncheck = LoadBitmap(hInst, MAKEINTRESOURCE(IDB_UNCHECK));			

			return 1;            
		}			
		case WM_CLOSE:
		case WM_DESTROY: 
		{
			clear();
			PostQuitMessage(0);
			return 1;
		}
		case WM_COMMAND:
		{
			int index;
			switch (LOWORD(wParam))
			{				    
				case IDL_IMAGES:
				{
					if (HIWORD(wParam) == LBN_SELCHANGE)
					{
						index = (int)SendMessage(hLBox, LB_GETCURSEL, 0, 0); 						
						if (index != -1)
						{
							pxItem pxi = (pxItem)SendMessage(hLBox, LB_GETITEMDATA, index, 0); 
							pxi->active = !pxi->active;							
							
							act_size = (pxi->active ? act_size + 1 : act_size - 1);							
							
							InvalidateRect(hLBox, 0, 0);
							enable(hWnd, index);							
						}	
					}
					break;
				}
				case ID_CLEAR:
				{ 
					bdraw = 0;
					InvalidateRect(hStat, 0, 0);
					EnableWindow(GetDlgItem(hWnd, ID_SAVE), 0);	
					break;
				}
				case ID_DOWN:
				{
					index = (int)SendMessage(hLBox, LB_GETCURSEL, 0, 0); 
					swap(index, index + 1);					
					enable(hWnd, index + 1);					
					break;
				}
				case ID_RUN:
				{						
					char line[LEN] = {0};
					GetWindowText(GetDlgItem(hWnd, IDE_W), line, LEN);
					_sz.cx = atoi(line);

					GetWindowText(GetDlgItem(hWnd, IDE_H), line, LEN);
					_sz.cy = atoi(line);
					
					_rc.left = _rc.top = 6;
					_rc.right = _rc.left + quad.cx;
					_rc.bottom = _rc.top + quad.cy;

					float delta;											
					if ((_sz.cy <= quad.cy) && (_sz.cx <= quad.cx))
					{						
						_rc.left += (quad.cx - _sz.cx) >> 1;
						_rc.right = _rc.left + _sz.cx;

						_rc.top += (quad.cy - _sz.cy) >> 1;
						_rc.bottom = _rc.top + _sz.cy;						
					}
					//
					else if (_sz.cx > _sz.cy)
					{
						delta = (float)_sz.cy / _sz.cx;
						int h = (int)(delta * quad.cx);												

						_rc.top += (quad.cy - h) >> 1;
						_rc.bottom = _rc.top + h;	
					}
					else 					
					{
						delta = (float)_sz.cx / _sz.cy;
						int w = (int)(delta * quad.cy);							
						
						_rc.left += (quad.cx - w) >> 1;
						_rc.right = _rc.left + w;						
					}					

					MoveWindow(hStat, _rc.left, _rc.top, _rc.right - _rc.left, _rc.bottom - _rc.top, 1);
					ShowWindow(hStat, SW_SHOW);

					bdraw = 1;
					InvalidateRect(hStat, 0, 0);					
					EnableWindow(GetDlgItem(hWnd, ID_SAVE), 1);	
					break;
				}
				case ID_SAVE:
				{
					OPENFILENAME ofn = {0};
					char szfile[MAX_PATH] = {0};								
			
					ofn.lStructSize = 76;
					ofn.hwndOwner = hWnd;			
					ofn.lpstrFile = szfile;
					ofn.nMaxFile = MAX_PATH;
					ofn.lpstrFile[0] = '\0';			
			
					ofn.nFilterIndex = 1;
					ofn.Flags = OFN_HIDEREADONLY; 

					ofn.lpstrFilter = "Windows BMP image (*.bmp)\0*.bmp\0";	
					ofn.lpstrDefExt = "bmp";	
					ofn.Flags = OFN_OVERWRITEPROMPT;

					if (GetSaveFileName(&ofn) == 1)
					{
						HBITMAP hBmp = getImage(hStat, _sz.cx, _sz.cy);
						saveImage(hBmp, ofn.lpstrFile);
					}
	
					break;
				}
				case ID_OPEN:
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
							SetCurrentDirectory(dir);
							strcat_s(dir, MAX_PATH, "\\*.bmp");

							WIN32_FIND_DATA find;
							HANDLE hfind = FindFirstFile(dir, &find);
							if (hfind != INVALID_HANDLE_VALUE)
							{								
								clear();
								SendMessage(hLBox, LB_RESETCONTENT, 0, 0);								
								
								pxItem pxi = new xItem();
								pxi->hBmp = (HBITMAP)LoadImage(0, find.cFileName, IMAGE_BITMAP, 0, 0, LR_LOADFROMFILE); 
								addItem(pxi);	
								
								while (FindNextFile(hfind, &find) != 0) 
								{									
									pxi = new xItem();
									pxi->hBmp = (HBITMAP)LoadImage(0, find.cFileName, IMAGE_BITMAP, 0, 0, LR_LOADFROMFILE); 
									addItem(pxi);										
								}
								FindClose(hfind);
								act_size = xItems.size();

								EnableWindow(GetDlgItem(hWnd, ID_CLEAR), 1);
								EnableWindow(GetDlgItem(hWnd, ID_RUN), 1);							
							}										
						}
					}
					break;
				}				
				case ID_UP:
				{
					index = (int)SendMessage(hLBox, LB_GETCURSEL, 0, 0); 
					swap(index, index - 1);	
					enable(hWnd, index - 1);					
					break;
				}
			}
			return 1;
		}		
		case WM_DRAWITEM:
		{			
			DrawItem((LPDRAWITEMSTRUCT)lParam);
			return 1;
		}	
		case WM_MEASUREITEM:
		{
			LPMEASUREITEMSTRUCT lpmis; 
			lpmis = (LPMEASUREITEMSTRUCT)lParam; 			
			lpmis->itemHeight = BMP_H; 
			return 1;
		}		
    } 
    return 0; 
}
int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, PSTR lpCmdLine, int nCmdShow)
{	
	hInst = hInstance;
	CoInitialize(0);

	INITCOMMONCONTROLSEX iccex;
	iccex.dwICC = ICC_INTERNET_CLASSES;
	iccex.dwSize = sizeof(INITCOMMONCONTROLSEX);
	InitCommonControlsEx(&iccex);

	HWND hwnd = CreateDialog(hInstance, MAKEINTRESOURCE(IDD_UNION), 0, (DLGPROC)DialogProc);
	
//	HICON hicon = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_PALETTE));
//	SendMessage(hwnd, WM_SETICON, (WPARAM)ICON_BIG, (LPARAM)hicon);
//	SendMessage(hwnd, WM_SETICON, (WPARAM)ICON_SMALL, (LPARAM)hicon);
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
	
	CoUninitialize();
	return 0;	
}