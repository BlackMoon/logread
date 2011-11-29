// palette.cpp
#include "palette.h"
#include "resource.h"

char						msg[MAX_PATH];
size_t						csize = 0, psize = 0;					 
HBITMAP						hbmOpen, hbmSave;
HINSTANCE					hInst = 0;
PALFILENAME					pfn;	
OPENFILENAME				ofn;
std::vector<curve>			curves;
quad*						pquad = 0;

edge iter(point& p, edge& e0, edge& e1)
{
	edge a, b, e;
	
	if ((e0.org == e0.dest) || (e1.org == e1.dest)) e = edge(e0.org, e1.org);
	else
	{	
		e = edge(e0.middle(), e1.middle());			
		switch (p.classify(e))
		{
			case LEFT:
			{
				a = edge(e0.org, e.org);
				b = edge(e1.org, e.dest);			
				e = iter(p, a, b);
				break;
			}
			case RIGHT:
			{
				a = edge(e.org, e0.dest);
				b = edge(e.dest, e1.dest);
				e = iter(p, a, b);						
				break;
			}		
		}
	}
	return e;
}
// pointz
pointz::pointz(double _x, double _y, double _z) : point(_x, _y), z(_z)
{
}
// curve
curve::~curve()
{
	clear();
}
size_t curve::size()
{
	return pts.size();
}
void curve::clear()
{
	pts.clear();
}
// quad
quad::quad()
{
}
void quad::horz()
{	
	edge edges[2];			

	edges[0].org = vedges[0].org;
	edges[0].dest = vedges[1].org;		
		
	edges[1].org = vedges[0].dest;
	edges[1].dest = vedges[1].dest;	
		
	memcpy(hedges, edges, sizeof(edges));			
}
int quad::pointIn(point& p)
{
	u_char i, parity = 0;	
	// h 	
	for (i = 0; i < 2; i++)
	{
		edge e = hedges[i];
		switch (edgeType(p, e)) 
		{
			case TOUCHING: return BOUNDARY_H0 + i;
			case CROSSING: 
			{
				parity = 1 - parity;
				break;
			}
		}
	}
	// v 	
	for (i = 0; i < 2; i++)
	{
		edge e = vedges[i];
		switch (edgeType(p, e)) 
		{
			case TOUCHING: return BOUNDARY_V0 + i;
			case CROSSING: 
			{
				parity = 1 - parity;
				break;
			}
		}
	}	
	return (parity ? INSIDE : OUTSIDE);
}
point quad::gethv(point& p, int pos)
{
	edge e;
	point a;
	switch (pos)
	{
		case BOUNDARY_H0:
		{
			e = edge(p, hedges[0].org);	
			
			a.x = z[0] + (z[1] - z[0]) * e.length() / hedges[0].length();
			a.y = 1.0;

			break;
		}
		case BOUNDARY_H1:
		{
			e = edge(p, hedges[1].org);	
			
			a.x = z[0] + (z[1] - z[0]) * e.length() / hedges[1].length();
			a.y = 0.0;
			
			break;
		}
		case BOUNDARY_V0:
		{
			e = edge(p, vedges[0].dest);	
			
			a.x = z[0];
			a.y = e.length() / vedges[0].length();

			break;
		}
		case BOUNDARY_V1:
		{			
			e = edge(p, vedges[1].dest);	
			
			a.x = z[1];
			a.y = e.length() / vedges[1].length();
			
			break;
		}
		case INSIDE:
		{			
			e = iter(p, hedges[0], hedges[1]);			
			
			a.x = z[0] + (z[1] - z[0]) * (e.org - hedges[0].org).length() / hedges[0].length();
			a.y = (e.dest - p).length() / e.length();

			break;
		}
	}
	return a;
}
bool readpts()
{
	bool bres = 1;
	char line[32], name[9];
	FILE *istream, *ostream;	
	
	if (0 == fopen_s(&istream, pfn.ptfile, "rt"))
	{		
		quad _quad;
		point p, q;	
		int pos = OUTSIDE; 
		size_t i, j;

		fopen_s(&ostream, pfn.outfile, "wt");		
		while (3 == fscanf_s(istream, "%s\t%lf\t%lf\n", name, 9, &p.x, &p.y))			
		{					
			fprintf_s(ostream, "%s\t%.3f\t%.3f\t", name, p.x, p.y);
			if ((N_VAL != p.x) && (N_VAL != p.y))
			{				
				try
				{
					// bottom quads
					for (j = 0; j < psize; j++)
					{					
						_quad = pquad[j];
						pos = _quad.pointIn(p);										

						if (pos == OUTSIDE)
						{
							if (p.classify(_quad.hedges[0]) == RIGHT)
							{								
								fprintf_s(ostream, "%.3f\t\tниже %s\t", p.x, _quad.layers[0]);
								throw 0;
							}							
						}
						else 
						{
							q = _quad.gethv(p, pos);				
							memset(line, 32, 0);
						
							if ((0.0 <= q.y) && (1/3.0 > q.y)) strcpy_s(line, 32, _quad.layers[1]);
							else if ((1.0/3.0 <= q.y) && (2.0/3.0 > q.y)) sprintf_s(line, 32, "%s + %s", _quad.layers[1], _quad.layers[0]);
							else if ((2.0/3.0 <= q.y) && (1.0 >= q.y)) strcpy_s(line, 32, _quad.layers[0]);

							fprintf_s(ostream, "%.3f\t%.3f\t%s\t", q.x, q.y, line);
							throw 1;
						}						
					}
					for (i = 1; i < csize - 1; i++)
					{
						for (j = 0; j < psize; j++)
						{
							_quad = pquad[i * psize + j];
							pos = _quad.pointIn(p);
							if (OUTSIDE != pos) 
							{
								q = _quad.gethv(p, pos);
							
								memset(line, 0, 32);
								if ((0.0 <= q.y) && (1/3.0 > q.y)) strcpy_s(line, 32, _quad.layers[1]);
								else if ((1.0/3.0 <= q.y) && (2.0/3.0 > q.y)) sprintf_s(line, 32, "%s + %s", _quad.layers[1], _quad.layers[0]);
								else if ((2.0/3.0 <= q.y) && (1.0 >= q.y)) strcpy_s(line, 32, _quad.layers[0]);

								fprintf_s(ostream, "%.3f\t%.3f\t%s\t", q.x, q.y, line);
								throw 1;
							}
						}
					}
					// top quads
					for (j = 0; j < psize; j++)
					{
						_quad = pquad[i * psize + j];
						pos = _quad.pointIn(p);										

						if (pos == OUTSIDE)
						{
							if (p.classify(_quad.hedges[1]) == LEFT)
							{								
								fprintf_s(ostream, "%.3f\t\tвыше %s\t", p.x, _quad.layers[1]);
								throw 0;	
							}
						}
						else
						{
							q = _quad.gethv(p, pos);			
						
							memset(line, 0, 32);						
							if ((0.0 <= q.y) && (1/3.0 > q.y)) strcpy_s(line, 32, _quad.layers[1]);
							else if ((1.0/3.0 <= q.y) && (2.0/3.0 > q.y)) sprintf_s(line, 32, "%s + %s", _quad.layers[1], _quad.layers[0]);
							else if ((2.0/3.0 <= q.y) && (1.0 >= q.y)) strcpy_s(line, 32, _quad.layers[0]);

							fprintf_s(ostream, "%.3f\t%.3f\t%s\t", q.x, q.y, line);
							throw 1;
						}
					}
					// not in one quad, not above or below
					fprintf_s(ostream, "%.3f\t", p.x);				
				}
				catch (...)
				{
				}
			}
			else fprintf_s(ostream, "%.3f\t", p.x);				
			fprintf_s(ostream, "\n");				
		}				

		fclose(ostream);
		fclose(istream);	
	}
	else bres = 0;

	return bres;
}
bool readscales()
{
	bool bres = 1;	
	FILE* stream;	
	
	if (0 == fopen_s(&stream, pfn.scalefile, "rt"))
	{			
		char head[16];		
		curve crv;		
		pointz p;
		while (fscanf_s(stream, "%s\n", head, 16) == 1)
		{			
			strcpy_s(crv.layer, 16, head);
			while (fscanf_s(stream, "%lf\t%lf\t%lf\n", &p.x, &p.y, &p.z) == 3)
			{
				crv.pts.push_back(p);									
			}			
			fscanf_s(stream, "/\n");		
			
			curves.push_back(crv);
			crv.clear();
		}		
		fclose(stream);		
	}
	else bres = 0;	

	return bres;
}
void createQuads()
{
	size_t i, j, k, index, 		   		   		   
	       qsize = 0;
	curve crv0, crv1;
	
	csize = curves.size(); 
	psize = UCHAR_MAX;
	// min size of curve pts
	for (i = 0; i < csize; i++)
	{
		crv0 = curves[i];
		psize = min(psize, crv0.size());		
	}
	qsize = --psize * --csize;
	pquad = new quad[qsize];

	pointz pt0[2], pt1[2];
	edge edges[2];
	
	for (i = 0; i < csize; i++)
	{
		crv0 = curves[i];
		crv1 = curves[i + 1];

		for (j = 0; j < psize; j++)
		{
			pt0[0] = crv0.pts[j];
			pt0[1] = crv0.pts[j + 1];

			pt1[0] = crv1.pts[j];
			pt1[1] = crv1.pts[j + 1];
			
			index = i * psize + j;	
			for (k = 0; k < 2; k++)
			{				
				edges[k].org = pt0[k];
				edges[k].dest = pt1[k];		

				pquad[index].z[k] = pt0[k].z;
			}						
			memcpy(pquad[index].vedges, edges, sizeof(edges));				

			pquad[index].horz();
			strcpy_s(pquad[index].layers[0], 16, crv0.layer);
			strcpy_s(pquad[index].layers[1], 16, crv1.layer);
		}	
	}
	curves.clear();	
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
	
	SetBkColor(hdcSrc, crBack);	
	DeleteObject(bmp);
	DeleteDC(maskDC);

	return bres;
}
void DrawItem(LPDRAWITEMSTRUCT lpdis)
{
	if (lpdis->CtlType != ODT_BUTTON) return;	
	
	HDC DC = lpdis->hDC, memDC = CreateCompatibleDC(DC);
	UINT state = DFCS_BUTTONPUSH;	

	if (lpdis->itemState & ODS_SELECTED) state |= DFCS_PUSHED;
	DrawFrameControl(DC, &lpdis->rcItem, DFC_BUTTON, state);
	
	if (lpdis->itemState & ODS_FOCUS)
	{		
		lpdis->rcItem.left += 2;
		lpdis->rcItem.top += 2;
		lpdis->rcItem.right -= 2;
		lpdis->rcItem.bottom -= 2;
		DrawFocusRect(DC, &lpdis->rcItem);
	}		

	BITMAP bm = {0};		
	HGDIOBJ gdiObj = 0;	

	switch (lpdis->CtlID)
	{
		case IDB_PTS:
		case IDB_SCALE:
		{			
			gdiObj = SelectObject(memDC, hbmOpen);	
			COLORREF crMask = GetPixel(memDC, 1, 1);

			GetObject(hbmOpen, sizeof(BITMAP), &bm);				
			TranspBlt(DC, 3, 3, bm.bmWidth, bm.bmHeight, memDC, 0, 0, bm.bmWidth, bm.bmHeight, crMask); 
			break;
		}
		case IDB_OUT:
		{
			gdiObj = SelectObject(memDC, hbmSave);
			GetObject(hbmSave, sizeof(BITMAP), &bm);
			BitBlt(DC, 3, 3, bm.bmWidth, bm.bmHeight, memDC, 0, 0, SRCCOPY);
			break;
		}
	}	
	DeleteDC(memDC);	
}
INT_PTR CALLBACK DialogProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
	switch (uMsg) 
    { 		
		case WM_INITDIALOG:
		{   
			hbmOpen = LoadBitmap(hInst, MAKEINTRESOURCE(IDB_OPEN)),
			hbmSave = LoadBitmap(hInst, MAKEINTRESOURCE(IDB_SAVE));									
			
			SetWindowText(GetDlgItem(hWnd, IDE_OUT), pfn.outfile);
			return 1;            
		}		
		case WM_CLOSE:
		case WM_DESTROY: 
		{
			PostQuitMessage(0);
			break;
		}
		case WM_COMMAND:
		{			
			BOOL bEnable = 0;
			
			char szfile[MAX_PATH] = {0};			
			memset(&ofn, 0, 76);
			
			ofn.lStructSize = 76;
			ofn.hwndOwner = hWnd;			
			ofn.lpstrFile = szfile;
			ofn.nMaxFile = MAX_PATH;
			ofn.lpstrFile[0] = '\0';			
			
			ofn.nFilterIndex = 1;
			ofn.Flags = OFN_HIDEREADONLY;

			switch (LOWORD(wParam))
			{				
				case IDB_RUN:
				{	
					SetCapture(hWnd);
					SetCursor(LoadCursor(0, IDC_WAIT));

					UINT uType = MB_OK;
					try
					{						
						strcpy_s(msg, MAX_PATH, "OK");		

						if (!readscales()) 
						{
							memset(msg, 0, MAX_PATH);
							sprintf_s(msg, MAX_PATH, "Не удалось прочитать %s.", pfn.scalefile);		
							throw 1;
						}
						createQuads();						
						if (!readpts())
						{
							memset(msg, 0, MAX_PATH);
							sprintf_s(msg, MAX_PATH, "Не удалось прочитать %s.", pfn.ptfile);		
							throw 1;							
						}
						uType += MB_ICONINFORMATION;
					}
					catch (int)
					{
						uType += MB_ICONWARNING;	
					}
					delete [] pquad;

					SetCursor(LoadCursor(0, IDC_ARROW));
					ReleaseCapture();
					
					MessageBox(hWnd, msg, "Палетка", uType);					
					break;
				}
				case IDB_SCALE:
				{					
					ofn.lpstrFilter = "Текстовые документы (*.txt)\0*.txt\0";
					ofn.lpstrDefExt = "txt";
					ofn.Flags |= OFN_FILEMUSTEXIST;

					if (1 == GetOpenFileName(&ofn)) 
					{
						strcpy_s(pfn.scalefile, MAX_PATH, ofn.lpstrFile);	
						SetWindowText(GetDlgItem(hWnd, IDE_SCALE), pfn.scalefile);	
						
						bEnable = (0 != strlen(pfn.ptfile)); 
						EnableWindow(GetDlgItem(hWnd, IDB_RUN), bEnable);  
					};
					break;
				}
				case IDB_PTS:
				{			
					ofn.lpstrFilter = "Источники данных (*.dat)\0*.dat\0";	
					ofn.lpstrDefExt = "dat";
					ofn.Flags |= OFN_FILEMUSTEXIST;

					if (1 == GetOpenFileName(&ofn))
					{
						strcpy_s(pfn.ptfile, MAX_PATH, ofn.lpstrFile);	
						SetWindowText(GetDlgItem(hWnd, IDE_PTS), pfn.ptfile);	

						bEnable = (0 != strlen(pfn.scalefile)); 
						EnableWindow(GetDlgItem(hWnd, IDB_RUN), bEnable);  
					}
					break;
				}
				case IDB_OUT:
				{	
					ofn.lpstrFilter = "Источники данных (*.dat)\0*.dat\0";	
					ofn.lpstrDefExt = "dat";	
					ofn.Flags = OFN_OVERWRITEPROMPT;
					
					if (1 == GetSaveFileName(&ofn))
					{
						strcpy_s(pfn.outfile, MAX_PATH, ofn.lpstrFile);	
						SetWindowText(GetDlgItem(hWnd, IDE_OUT), pfn.outfile);						
					}
					break;
				}
			}
			break;
		}				
		case WM_DRAWITEM:
		{
			DrawItem((LPDRAWITEMSTRUCT)lParam);
			break;
		}
    } 
    return 0; 
}
int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow)
{
	hInst = hInstance;
	HWND hwnd = CreateDialog(hInstance, MAKEINTRESOURCE(IDD_PALETTE), 0, (DLGPROC)DialogProc);
	
	HICON hicon = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_PALETTE));
	SendMessage(hwnd, WM_SETICON, (WPARAM)ICON_BIG, (LPARAM)hicon);
	SendMessage(hwnd, WM_SETICON, (WPARAM)ICON_SMALL, (LPARAM)hicon);
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