// dbobj.cpp
#include "dbobj.h"

CData::CData()
{
	memset(alias, 0, 16);			
	memset(msg, 0, MAX_PATH);			
	m_cn.CreateInstance("ADODB.Connection");
}
CData::~CData()
{
	close();
}
bool CData::load(HWND hcombo)
{
	bool bres = 1;
	char line[32] = {0}, sql[MAX_PATH] = {0};

	_RecordsetPtr rs("ADODB.Recordset");
	rs->PutRefActiveConnection(m_cn);

	sprintf_s(sql, MAX_PATH, SEL_PL, alias);		
	SendMessage(hcombo, CB_RESETCONTENT, 0, 0);	
	
	_variant_t v;
	try
	{
		rs->Open(sql, vtMissing, adOpenKeyset, adLockOptimistic, adCmdText);		
		while (!rs->__EOF)
		{				
			v = rs->Fields->Item[0l]->Value;			
			sprintf_s(line, 32, "%d", v.iVal);
			
			SendMessage(hcombo, CB_ADDSTRING, 0, (LPARAM)line);				
			rs->MoveNext();
		}
		rs->Close();

		SendMessage(hcombo, CB_SETCURSEL, 0, 0);
		EnableWindow(hcombo, 1);
	}
	catch (_com_error& e)
	{	
		strcpy_s(msg, MAX_PATH, (PSTR)e.Description());
		bres = 0;
	}

	return bres;
}
bool CData::open(PCSTR _path)
{
	bool bres = 1;  
	try
	{		
		sprintf_s(con, MAX_PATH, JET, _path);
		m_cn->Open(con, "", "", 0);
	}
	catch (_com_error& e)
	{
		strcpy_s(msg, MAX_PATH, (PSTR)e.Description());
		bres = 0;
	}
	return bres;
}
bool CData::run(int horz)
{
	bool bres = 1;
	char sql[MAX_PATH] = {0};

	_RecordsetPtr rs("ADODB.Recordset");
	rs->PutRefActiveConnection(m_cn);

	sprintf_s(sql, MAX_PATH, SEL_NC, alias, horz);		
	
	_variant_t v;
	try
	{
		rs->Open(sql, vtMissing, adOpenKeyset, adLockOptimistic, adCmdText);		
		while (!rs->__EOF)
		{				
			v = rs->Fields->Item[0l]->Value;			
			
			
			rs->MoveNext();
		}
		rs->Close();
	}
	catch (_com_error& e)
	{	
		strcpy_s(msg, MAX_PATH, (PSTR)e.Description());
		bres = 0;
	}
	
	return bres;
}
void CData::close()
{
	if (m_cn->GetState() == adStateOpen) m_cn->Close();
}