// ctrl.h
#pragma once
#define ITEM_H				23
// CODCombo
class CODCombo : public CComboBox
{
	DECLARE_DYNAMIC(CODCombo)
public:	
// Overrides
	void DrawItem(LPDRAWITEMSTRUCT);
	void MeasureItem(LPMEASUREITEMSTRUCT);	
protected:
	DECLARE_MESSAGE_MAP()
	afx_msg int OnCreate(LPCREATESTRUCT);	
};
// CToolBarEx
class CToolBarEx : public CToolBar
{
public:	
	bool setTrueColor(UINT); 
	void dropArrow(UINT, UINT);
	CWnd* InsertControl(CRuntimeClass*, PCTSTR, RECT&, UINT, DWORD);
	CWnd* InsertControl(CWnd*, RECT&, UINT);		
// Attributes
private:
	UINT					menuID;
protected:
	DECLARE_MESSAGE_MAP()
	afx_msg LRESULT OnIdleUpdateCmdUI(WPARAM, LPARAM);
	afx_msg LRESULT OnKickIdle(WPARAM, LPARAM);	
	afx_msg void OnDropDown(NMHDR*, LRESULT*);
};
