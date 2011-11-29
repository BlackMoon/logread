// dlg.h
#pragma once
#include "ctrl.h"
#include "dbobj.h"
#include "resource.h"
// CDlg 
class CDlg : public CDialog
{
private:
	bool saveTxt(PCSTR);
	bool saveXls(PCSTR);
protected:
	void DoDataExchange(CDataExchange*);
	void GetListData();
	void OnCancel();
	void OnOK();	
public:
	CDlg(CWnd*);
	~CDlg();
// Overrides
	BOOL OnInitDialog();
// Attributes
private:
	bool					m_bexcel;
	int						m_ncheck;
	RECT					m_rectBar;	
	CData					m_data;
protected:
	HICON					m_hIcon;
	CCheckListBox			m_horzbox;
	CCheckListBox			m_wellbox;	
	CListCtrl				m_outlist;
	CToolBarEx				m_toolbar;
	CODCombo*				m_pcombo;
public:
	enum {IDD = IDD_SUMP};
protected:	
	DECLARE_MESSAGE_MAP()	
	afx_msg BOOL OnToolTipText(UINT, NMHDR*, LRESULT*);
	afx_msg HCURSOR OnQueryDragIcon();		
	afx_msg LRESULT OnKickIdle(WPARAM, LPARAM);	
	afx_msg void OnClose();
	afx_msg void OnDestroy();
	afx_msg void OnPaint();	
	afx_msg void OnRun();		
	afx_msg void OnOutSave();		
	afx_msg void OnSelAll();		
	afx_msg void OnTabOpen();
	afx_msg void OnUpdateRun(CCmdUI*);	
	afx_msg void OnUpdateSave(CCmdUI*);	
	afx_msg void OnUpdateSelAll(CCmdUI*);	
};