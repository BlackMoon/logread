// dlg.cpp
#include "stdafx.h"
#include "dlg.h"

const int					hwidth[HD_NUM] = {50, 50, 70, 70, 50, 50, 50, 60, 70, 70, 70, 80};	

bool isOLEObjectInstalled(PCWSTR progId)
{
	CLSID clsid;
	return (S_OK == CLSIDFromProgID(progId, &clsid));
}
void GetDir(PSTR _Dst, UINT _DstSize, PCSTR _Src)
{	
	PCSTR slash = strrchr(_Src, 0x5c);								// '\\'
	if (slash) strncpy_s(_Dst, _DstSize, _Src, slash - _Src + 1);
	else strcpy_s(_Dst, _DstSize, _Src);
}
void GetFile(PSTR _Dst, UINT _DstSize, PCSTR _Src)
{	
	PCSTR slash = strrchr(_Src, 0x5c);								// '\\'
	if (slash) strcpy_s(_Dst, _DstSize, slash + 1);
	else strcpy_s(_Dst, _DstSize, _Src);
}
void GetExt(PSTR _Dst, UINT _DstSize, PSTR _Src)
{	
	PSTR dot = strrchr(_Src, 0x2e);									// '.'

	if (dot) strcpy_s(_Dst, _DstSize, dot + 1);
	else _Dst[0] = 0;
}
void trim_left(PSTR _Dst, UINT _DstSize, PCSTR _Src)
{
	char symb;
	int i, len = (int)strlen(_Src);	
	for (i = 0; i < len; i++)
	{
		symb = _Src[i];
		if (symb != 0x2) break;										// '\0'
	}	
	strncpy_s(_Dst, _DstSize, _Src + (i + 1), len - i);	
}
// CDlg
CDlg::CDlg(CWnd* pParent) : CDialog(CDlg::IDD, pParent)
{
	m_hIcon = afxCurrentWinApp->LoadIcon(IDR_MAINFRAME);
	m_pcombo = 0;
	m_ncheck = 0;	
	
	m_bexcel = isOLEObjectInstalled(L"Excel.Application");	
}
CDlg::~CDlg()
{	
	m_data.close();
}
BOOL CDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	SendMessage(WM_SETICON, (WPARAM)ICON_BIG, (LPARAM)m_hIcon);
	SendMessage(WM_SETICON, (WPARAM)ICON_SMALL, (LPARAM)m_hIcon);	
	
	m_horzbox.SetWindowPos(0, 642, 0, 120, 200, SWP_NOZORDER | SWP_SHOWWINDOW); 
	m_wellbox.SetWindowPos(0, 0, 0, 642, 200, SWP_NOZORDER | SWP_SHOWWINDOW); 
	m_outlist.SetWindowPos(0, 0, 200, 762, 224, SWP_NOZORDER | SWP_SHOWWINDOW); 
	
	if (!m_toolbar.CreateEx(this, TBSTYLE_FLAT, WS_CHILD | WS_VISIBLE | CBRS_TOP | 
		CBRS_TOOLTIPS | CBRS_FLYBY | CBRS_SIZE_DYNAMIC) || !m_toolbar.LoadToolBar(IDR_TOOLBAR))
	{		
		EndDialog(IDCANCEL);
	}
	m_pcombo = (CODCombo*)m_toolbar.InsertControl(RUNTIME_CLASS(CODCombo), _T(""), 
		CRect(0, 0, 200, 200), ID_BLOCK, WS_VSCROLL | CBS_DROPDOWNLIST | CBS_OWNERDRAWFIXED | CBS_HASSTRINGS);	
	m_pcombo->EnableWindow(0); 

	m_toolbar.setTrueColor(IDB_TOOL);	

	RECT rc_child, rc_old, rc_wnd;		
	GetClientRect(&rc_old);
	RepositionBars(AFX_IDW_CONTROLBAR_FIRST, AFX_IDW_CONTROLBAR_LAST, 0, reposQuery, &m_rectBar);
	
	POINT pt;
	pt.x = m_rectBar.left- rc_old.left;
	pt.y = m_rectBar.top - rc_old.top;

	CWnd* pwnd = GetWindow(GW_CHILD);
	while (pwnd)
	{
		pwnd->GetWindowRect(&rc_child);
		ScreenToClient(&rc_child);
	
		OffsetRect(&rc_child, pt.x, pt.y);
		pwnd->MoveWindow(&rc_child, 0); 
		pwnd = pwnd->GetNextWindow();
	}
	
	GetWindowRect(&rc_wnd);
	rc_wnd.right += rc_old.right - rc_old.left - m_rectBar.right - m_rectBar.left;
	rc_wnd.bottom += rc_old.bottom - rc_old.top - m_rectBar.bottom - m_rectBar.top;
	MoveWindow(&rc_wnd, 0);	
	RepositionBars(AFX_IDW_CONTROLBAR_FIRST, AFX_IDW_CONTROLBAR_LAST, 0);	

	DWORD dwStyle = GetWindowLong(m_outlist.m_hWnd, GWL_STYLE); 
    dwStyle &= ~(LVS_TYPEMASK); 
	dwStyle &= ~(LVS_EDITLABELS); 

    SetWindowLong(m_outlist.m_hWnd, GWL_STYLE, dwStyle | LVS_REPORT | LVS_SINGLESEL);	
	for (int i = 0; i < HD_NUM; i++)
	{
		m_outlist.InsertColumn(i, headers[i], LVCFMT_LEFT, hwidth[i]);
	}
	
	return 1;
}
void CDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDL_OUT, m_outlist);
	DDX_Control(pDX, IDL_HORZ, m_horzbox);
	DDX_Control(pDX, IDL_WELL, m_wellbox);	
}
void CDlg::GetListData()
{	
	char line[16] = {0};
	int i;	
	
	for (i = 0; i < m_horzbox.GetCount(); i++)
	{		
		if (m_horzbox.GetCheck(i) == 1) 
		{
			m_horzbox.GetText(i, line);
			m_data.m_codes.Add(atoi(line));
		}
	}
	for (i = 0; i < m_wellbox.GetCount(); i++)
	{		
		if (m_wellbox.GetCheck(i) == 1) 
		{
			m_wellbox.GetText(i, line);
			trim_left(line, 16, line);
			m_data.m_wells.Add(line);
		}
	}		
}
void CDlg::OnCancel()
{
}
void CDlg::OnOK()
{
}
BEGIN_MESSAGE_MAP(CDlg, CDialog)
	ON_COMMAND(ID_OUT_SAVE, OnOutSave)
	ON_COMMAND(ID_RUN, OnRun)
	ON_COMMAND(ID_TAB_OPEN, OnTabOpen)	
	ON_COMMAND(ID_SEL_ALL, OnSelAll)
	ON_MESSAGE(WM_KICKIDLE, OnKickIdle)			
	ON_NOTIFY_EX_RANGE(TTN_NEEDTEXTA, 0, 0xffff, OnToolTipText)	
	ON_UPDATE_COMMAND_UI(ID_OUT_SAVE, OnUpdateSave)	
	ON_UPDATE_COMMAND_UI(ID_RUN, OnUpdateRun)	
	ON_UPDATE_COMMAND_UI(ID_SEL_ALL, OnUpdateSelAll)	
	ON_WM_CLOSE()
	ON_WM_DESTROY()
	ON_WM_QUERYDRAGICON()
	ON_WM_PAINT()
END_MESSAGE_MAP()
BOOL CDlg::OnToolTipText(UINT nID, NMHDR* pNMHDR, LRESULT* pResult)
{	
	if (pNMHDR->code == TTN_NEEDTEXTA)
	{
		if (GetRoutingFrame() != 0) return 0;		
		TOOLTIPTEXTA* pTTTA = (TOOLTIPTEXTA*)pNMHDR;	
		
		nID = (UINT)pNMHDR->idFrom;
		if (nID != 0) 
		{
			char text[64];
			LoadString(app.m_hInstance, nID, text, 64);			
			lstrcpyn(pTTTA->szText, text, sizeof(pTTTA->szText));					
		}		
		// bring the tooltip window above other popup windows
		::SetWindowPos(pNMHDR->hwndFrom, HWND_TOP, 0, 0, 0, 0, SWP_NOACTIVATE | SWP_NOSIZE | SWP_NOMOVE | SWP_NOOWNERZORDER);
		*pResult = 0;
		return 1;
	}
	return 0;
}
HCURSOR CDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}
LRESULT CDlg::OnKickIdle(WPARAM wParam, LPARAM lParam)
{
	app.OnIdle(-1);
	UpdateDialogControls(this, 1);	
	return 0L;
}
void CDlg::OnClose()
{
	EndDialog(IDOK);
}
void CDlg::OnDestroy()
{
	delete m_pcombo;
}
void CDlg::OnOutSave()
{
	OPENFILENAME ofn;
	char szfile[MAX_PATH] = {0};			
	memset(&ofn, 0, sizeof(OPENFILENAME));
			
	ofn.lStructSize = 76;
	ofn.hwndOwner = m_hWnd;			
	ofn.lpstrFile = szfile;	
	ofn.nMaxFile = MAX_PATH;
	ofn.lpstrDefExt = "";	
	ofn.nFilterIndex = 1;
	ofn.Flags = OFN_HIDEREADONLY | OFN_OVERWRITEPROMPT;

	if (m_bexcel) ofn.lpstrFilter = "Файлы Excel(*.xls)\0*.xls\0Текстовые файлы (*.txt)\0*.txt\0";			
	else ofn.lpstrFilter = "Текстовые файлы (*.txt)\0*.txt\0";	
	
	if (GetSaveFileName(&ofn) == 1)
	{	
		char ext[8];
		GetExt(ext, 8, ofn.lpstrFile);
		_strlwr_s(ext, 8);
		
/*		if (strcmp(ext, "xls") == 0) m_data.saveXls(ofn.lpstrFile);
		else */m_data.saveTxt(ofn.lpstrFile);
	}	
}
void CDlg::OnRun()
{
	::SetCapture(m_hWnd);
	SetCursor(LoadCursor(0, IDC_WAIT));

	GetListData();
	if (m_data.load()) 
	{
		m_data.fillOut(&m_outlist);
		m_data.m_codes.RemoveAll();
		m_data.m_wells.RemoveAll();
	}
	else MessageBox(m_data.msg, 0, MB_ICONWARNING + MB_OK);	
	
	SetCursor(LoadCursor(0, IDC_ARROW));
	::ReleaseCapture();
}
void CDlg::OnSelAll()
{
	m_ncheck = 1 - m_ncheck;

	int i;
	for (i = 0; i < m_horzbox.GetCount(); i++)
	{		
		m_horzbox.SetCheck(i, m_ncheck);		
	}
	for (i = 0; i < m_wellbox.GetCount(); i++)
	{		
		m_wellbox.SetCheck(i, m_ncheck);		
	}				
}
void CDlg::OnTabOpen()
{
	OPENFILENAME ofn;
	char szfile[MAX_PATH] = {0};			
	memset(&ofn, 0, 76);
			
	ofn.lStructSize = 76;
	ofn.hwndOwner = m_hWnd;			
	ofn.lpstrFile = szfile;	
	ofn.nMaxFile = MAX_PATH;
	ofn.lpstrFile[0] = '\0';			
	ofn.lpstrDefExt = "";				
	ofn.nFilterIndex = 1;
	ofn.Flags = OFN_HIDEREADONLY | OFN_FILEMUSTEXIST;

	ofn.lpstrFilter = "Файлы dBASE (*.dbf)\0*.dbf\0Файлы Paradox (*.db)\0*.db\0";		
	
	if (GetOpenFileName(&ofn) == 1)
	{			
		char ext[8] = {0}, path[MAX_PATH] = {0}, file[L_SIZE] = {0};			
		
		GetDir(path, MAX_PATH, ofn.lpstrFile);		
		GetFile(file, L_SIZE, ofn.lpstrFile);
		GetExt(ext, 8, file);
		_strlwr_s(ext, 8);

		::SetCapture(m_hWnd);
		SetCursor(LoadCursor(0, IDC_WAIT));
		try
		{
			if ((file[0] != 0x70) || (file[1] != 0x69))							// pi
			{
				char msg[MAX_PATH];
				sprintf_s(msg, MAX_PATH, "Некорректное имя файла - %s", file, MAX_PATH);
				throw msg;
			}
			
			m_data.close();			
			if (!m_data.open(path, file, ext)) throw m_data.msg;
			
			m_data.fillIn(&m_horzbox, &m_wellbox);
			m_data.fillBlock(m_pcombo);
			m_outlist.SendMessage(LVM_DELETEALLITEMS);			
		}
		catch (char* pex)
		{
			MessageBox(pex, 0, MB_ICONWARNING + MB_OK);
		}					
		SetCursor(LoadCursor(0, IDC_ARROW));
		::ReleaseCapture();
	}	
}
void CDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting
		SendMessage(WM_ICONERASEBKGND, (WPARAM)(dc.m_hDC), 0);
		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		
		RECT rc;
		GetClientRect(&rc);
		int x = (rc.right - rc.left - cxIcon + 1) / 2;
		int y = (rc.bottom - rc.top - cyIcon + 1) / 2;
		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else CDialog::OnPaint();	
}
void CDlg::OnUpdateRun(CCmdUI* pCmdUI)
{
	pCmdUI->Enable(1);
}
void CDlg::OnUpdateSelAll(CCmdUI* pCmdUI)
{
	int numh = m_horzbox.GetCount(),
		numw = m_wellbox.GetCount();

	pCmdUI->Enable(numh > 0 || numw > 0);
	pCmdUI->SetCheck(m_ncheck);
}
void CDlg::OnUpdateSave(CCmdUI* pCmdUI)
{	
	pCmdUI->Enable(m_outlist.SendMessage(LVM_GETITEMCOUNT) > 0);
}