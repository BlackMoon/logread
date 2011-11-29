// sump.cpp
#include "stdafx.h"
#include "sump.h"
#include "dlg.h"

CsumpApp app;
CsumpApp::CsumpApp()
{
}
int CsumpApp::ExitInstance()
{
	CoUninitialize();	
	return CWinApp::ExitInstance();
}
BOOL CsumpApp::InitInstance()
{	
	CWinApp::InitInstance();			
	CoInitialize(0);

	CDlg dlg(0);
	m_pMainWnd = &dlg;
	dlg.DoModal();	

	return 0;
}