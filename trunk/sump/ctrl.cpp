// ctrl.cpp
#include "stdafx.h"
#include "ctrl.h"
// CODCombo
IMPLEMENT_DYNAMIC(CODCombo, CComboBox)
void CODCombo::DrawItem(LPDRAWITEMSTRUCT lpDIS)
{
	if (SendMessage(CB_GETCOUNT, 0, 0) == 0) return;
	
	CDC dc;
	dc.Attach(lpDIS->hDC);	
	dc.SelectStockObject(DEFAULT_GUI_FONT);   
	// Save these value to restore them when done drawing.
	COLORREF oldText = dc.GetTextColor(),
			 oldBkgnd = dc.GetBkColor();

	if (!IsWindowEnabled()) dc.SetTextColor(::GetSysColor(COLOR_GRAYTEXT));		
	if ((lpDIS->itemAction | ODA_SELECT) && (lpDIS->itemState  & ODS_SELECTED))
	{
		dc.SetTextColor(::GetSysColor(COLOR_HIGHLIGHTTEXT));
		dc.SetBkColor(::GetSysColor(COLOR_HIGHLIGHT));
		dc.FillSolidRect(&lpDIS->rcItem, ::GetSysColor(COLOR_HIGHLIGHT));
	}
	else dc.FillSolidRect(&lpDIS->rcItem, oldBkgnd);		

	RECT rc(lpDIS->rcItem);
	rc.left += 2;
	rc.right -= 2;	   

	TCHAR line[32];
	memset(line, 0, 32);
	SendMessage(CB_GETLBTEXT, lpDIS->itemID, (LPARAM)line);

	dc.DrawText(line, &rc, DT_LEFT | DT_SINGLELINE | DT_VCENTER);				

	dc.SetTextColor(oldText);
	dc.SetBkColor(oldBkgnd);
	dc.Detach();
}
void CODCombo::MeasureItem(LPMEASUREITEMSTRUCT)
{
	ASSERT(1);
}
BEGIN_MESSAGE_MAP(CODCombo, CComboBox)
	ON_WM_CREATE()
END_MESSAGE_MAP()
int CODCombo::OnCreate(LPCREATESTRUCT lpCreateStruct)
{
	if (CComboBox::OnCreate(lpCreateStruct) == -1)
	{
		TRACE0("Failed to create odcombo\n");
		return -1;      // fail to create
	}
	SendMessage(CB_SETITEMHEIGHT, -1, MAKELONG(ITEM_H, 0)); 	
	return 0;
}
// CToolBarEx
bool CToolBarEx::setTrueColor(UINT nIDResource)
{
	BITMAP bm;
	CBitmap bitmap;
	CImageList ilist;
	
	if (!bitmap.Attach(LoadImage(AfxGetResourceHandle(), MAKEINTRESOURCE(nIDResource), 
		IMAGE_BITMAP, 0, 0, LR_DEFAULTSIZE | LR_CREATEDIBSECTION))) return 0; 	
	if (!bitmap.GetBitmap(&bm)) return 0;				
	if (!ilist.Attach(ImageList_Create(24, ITEM_H, ILC_COLORDDB | ILC_MASK, bm.bmWidth / 24, 0))) return 0;		
	
	RGBTRIPLE* rgb = (RGBTRIPLE*)(bm.bmBits);
	COLORREF rgbMask = RGB(rgb[0].rgbtRed, rgb[0].rgbtGreen, rgb[0].rgbtBlue);

	if (ilist.Add(&bitmap, rgbMask) == -1) return 0;
    SendMessage(TB_SETIMAGELIST, 0, (LPARAM)ilist.m_hImageList);
    
	ilist.Detach(); 
	bitmap.DeleteObject();
	
	return 1;
}
void CToolBarEx::dropArrow(UINT nID, UINT nMenu)
{
	menuID = nMenu;
	SendMessage(TB_SETEXTENDEDSTYLE, 0, (LPARAM)TBSTYLE_EX_DRAWDDARROWS);	
	
	DWORD dw = GetButtonStyle(CommandToIndex(nID));
	dw |= TBSTYLE_DROPDOWN;
	SetButtonStyle(CommandToIndex(nID), dw);	
}
CWnd* CToolBarEx::InsertControl(CRuntimeClass* pClass, PCTSTR lpszWindowName, RECT& rect, UINT nID, DWORD dwStyle)
{
	CWnd* pCtrl = 0;	
	if (pClass->IsDerivedFrom(RUNTIME_CLASS(CComboBox)))		// CODCombo control.
	{
		pCtrl = new CODCombo();
		ASSERT_VALID(pCtrl);
		if (!((CODCombo*)pCtrl)->Create(WS_CHILD | WS_VISIBLE | dwStyle, rect, this, nID))
		{			
			delete pCtrl;
			return 0;
		}
	}
	else if( pClass->IsDerivedFrom(RUNTIME_CLASS(CEdit)))		// CEdit control.
	{
		pCtrl = new CEdit();
		ASSERT_VALID(pCtrl);
		if (!((CEdit*)pCtrl)->Create(WS_CHILD | WS_VISIBLE | dwStyle, rect, this, nID))
		{
			delete pCtrl;
			return 0;
		}
	}
	else if (pClass->IsDerivedFrom(RUNTIME_CLASS(CButton)))		// CButton control.
	{
		pCtrl = new CButton();
		ASSERT_VALID(pCtrl);
		if (!((CButton*)pCtrl)->Create(lpszWindowName, WS_CHILD | WS_VISIBLE | dwStyle, rect, this, nID))
		{
			delete pCtrl;
			return 0;
		}
	}
	else if (pClass->IsDerivedFrom(RUNTIME_CLASS(CWnd)))			// CWnd object.
	{
		pCtrl = new CWnd();				
		ASSERT_VALID(pCtrl);
#ifdef _UNICODE
		TCHAR szClassName[_MAX_PATH];
		MultiByteToWideChar(CP_ACP, MB_PRECOMPOSED, pClass->m_lpszClassName, -1, szClassName, 255);
		if (!((CWnd*)pCtrl)->Create(szClassName, lpszWindowName, WS_CHILD | WS_VISIBLE | dwStyle, rect, this, nID))
		{
			delete pCtrl;
			return 0;
		}
#else
		if (!((CWnd*)pCtrl)->Create(pClass->m_lpszClassName, lpszWindowName, WS_CHILD | WS_VISIBLE | dwStyle, rect, this, nID))
		{
			delete pCtrl;
			return 0;
		}
#endif
	}
	else															// An invalid object was passed in
	{
		ASSERT(0);
		return 0;
	}
	return InsertControl(pCtrl, rect, nID);	
}
CWnd* CToolBarEx::InsertControl(CWnd* pCtrl, RECT& rect, UINT nID)
{
	ASSERT_VALID(pCtrl);
	// make sure the id is valid, and set the button style for a seperator.
	int nState, nIndex = CommandToIndex(nID);
	if (nIndex > -1)
	{
		ASSERT(nIndex >= 0);
		SetButtonInfo(nIndex, nID, TBBS_SEPARATOR, rect.right - rect.left);
		// insert the control into the toolbar.
		GetItemRect(nIndex, &rect);				
		pCtrl->SetWindowPos(0, rect.left, rect.top, 0, 0, SWP_NOZORDER | SWP_NOACTIVATE | SWP_NOSIZE | SWP_NOCOPYBITS);
		pCtrl->SetFont(GetFont());

		BOOL bVert = (m_dwStyle & CBRS_ORIENT_VERT) != 0;
		if (bVert)
		{
		   	nState = GetToolBarCtrl().GetState(nIndex);
		   	GetToolBarCtrl().SetState(nID,(nState | TBSTATE_HIDDEN));
			pCtrl->ShowWindow(SW_HIDE);
		}
		else
		{
		   	nState = GetToolBarCtrl().GetState(nIndex);
		   	GetToolBarCtrl().SetState(nIndex,(nState & ~TBSTATE_HIDDEN));
			pCtrl->ShowWindow(SW_SHOW);
		}
	}
	else pCtrl->ShowWindow(SW_HIDE);	
	return pCtrl;	
}
BEGIN_MESSAGE_MAP(CToolBarEx, CToolBar)
	ON_MESSAGE(WM_IDLEUPDATECMDUI, OnIdleUpdateCmdUI)	
	ON_MESSAGE(WM_KICKIDLE, OnKickIdle)	
	ON_NOTIFY_REFLECT(TBN_DROPDOWN, OnDropDown)	
END_MESSAGE_MAP()
LPARAM CToolBarEx::OnKickIdle(WPARAM wParam, LPARAM lParam)
{
	app.OnIdle(-1);
	OnIdleUpdateCmdUI(wParam, lParam);
	return 0L;
}
LPARAM CToolBarEx::OnIdleUpdateCmdUI(WPARAM wParam, LPARAM lParam)
{
	if (IsWindowVisible())
	{
		CFrameWnd* pParent = (CFrameWnd *)GetParent();
		if (pParent) OnUpdateCmdUI(pParent, (BOOL)wParam);
	}
	return 0L;
}
void CToolBarEx::OnDropDown(NMHDR* pNMHDR, LRESULT* pResult)
{	
	LPNMTOOLBAR pNMTB = reinterpret_cast<LPNMTOOLBAR>(pNMHDR);
	
	CMenu menu;
	if (!menu.LoadMenu(menuID)) return;
	
	RECT rc;	
	SendMessage(TB_GETRECT, (WPARAM)pNMTB->iItem, (LPARAM)&rc);
	
	ClientToScreen(&rc);			
	menu.GetSubMenu(0)->TrackPopupMenu(TPM_LEFTALIGN | TPM_LEFTBUTTON, rc.left, rc.bottom, this, 0); 
}