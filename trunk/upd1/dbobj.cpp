// dbobj.cpp
#include "dbobj.h"
#include <stdio.h>
#include <map>

#define	SZ						56
#define	INS						"INSERT INTO %s "
#define SEL						"SELECT * FROM %s "
#define UPD						"UPDATE %s SET "

#define JET						"Driver={Microsoft dBASE Driver (*.dbf)}; DriverID=277; Dbq=%s; Charset=cp866;"
//#define JET						"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=%s;Extended Properties=dBASE III;Persist Security Info=False"

char *ptables[SZ] =	   {"TOVAR.DBF",//"APP_BASE.DBF", 
						"BASE1.DBF",
						"BASE2.DBF",
						"BZC_STR.DBF",
						"DEFICIT.DBF",
						"DEFICITS.DBF",
						"DOP_ORG.DBF",
						"ED_IZ.DBF",
						"GRUPPA.DBF",
						"KL_DOST.DBF",
						"KLIENT.DBF",
						"KLIENT1.DBF",
						"MEMVAR.DBF",
						"NALI_PR.DBF",
						"NALI_PRK.DBF",
						"NUM_FAKT.DBF",
						"NAZNPL.DBF",
						"OB_DOLG.DBF",
						"OB_E_PR.DBF",
						"OB_E_PRK.DBF",
						"OB_PRIH.DBF",
						"OB_RASH.DBF",
						"OPER_D.DBF",
						"OPL_KLI.DBF",
						"OPL_PLAT.DBF",
						"ORGANIZ.DBF",
						"PEREOC.DBF",
						"PEREOC_1.DBF",
						"PRIHOD.DBF",
						"PRIHODK.DBF",
						"PRIHODK1.DBF",
						"R_SCHET.DBF",
						"RAION.DBF",
						"RASHOD.DBF",
						"RASHOD1.DBF",
						"RASHODK.DBF",
						"RASHODO.DBF",
						"S_MENEG.DBF",
						"S_NAL_PR.DBF"
						"S_NDS.DBF",
						"S_RABOT.DBF",
						"SEMAPHOR.DBF",
						"SERTIF.DBF",
						"SKIDKA.DBF",
						"SKLAD.DBF",
						"STRANA.DBF",
						"SVID_KEM.DBF",
						"TOV_LIC.DBF",
						"TOV_NDS.DBF",
						"TOVAR.DBF",
						"VALUTA.DBF",
						"VOZV_NAK.DBF",
						"VOZV_YAV.DBF",
						"Z_DOST.DBF",
						"ZAK_PR.DBF",
						"ZAK_PRK.DBF"};



bool fileExists(LPCTSTR fname)
{
	WIN32_FIND_DATA wfd;
	HANDLE hFind = ::FindFirstFile(fname, &wfd);
	if (INVALID_HANDLE_VALUE != hFind)
	{			    
		::FindClose(hFind);
		return true;
	}
	return false;
}

CData::CData()
{		
	CoInitialize(0);			
	m_cn_src.CreateInstance(__uuidof(Connection));
	m_cn_dst.CreateInstance(__uuidof(Connection));
	memset(msg, 0, MAX_PATH);	
}
CData::~CData()
{
	close();
	m_cn_src.Release();
	m_cn_dst.Release();
	CoUninitialize();
}
bool CData::compare(std::vector<KEY_VAL> keys, std::vector<FLD_VAL> fields, PCSTR ptable, byte state)
{	
	char ins[MAX_PATH], sql[MAX_PATH], upd[MAX_PATH], _where[MAX_PATH] = "WHERE ";
	size_t i, n;

	KEY_VAL kv;
	FLD_VAL fv;
	
	sprintf_s(ins, MAX_PATH, INS, ptable);	
	strcat_s(ins, MAX_PATH, "VALUES (");
	sprintf_s(upd, MAX_PATH, UPD, ptable);	

	_RecordsetPtr rs;
	rs.CreateInstance(__uuidof(Recordset));	
	// keys
	for (i = 0; i < keys.size() - 1; i++)
	{
		kv = keys[i];
		kv.vartoline();
		
		strcat_s(_where, MAX_PATH, kv.line);
		strcat_s(_where, MAX_PATH, " AND ");

		strcat_s(ins, MAX_PATH, kv.buf);
		strcat_s(ins, MAX_PATH, ",");		
	}

	kv = keys[i];
	kv.vartoline();

	strcat_s(_where, MAX_PATH, kv.line);	
	strcat_s(ins, MAX_PATH, kv.buf);
	// fields
	for (i = 0; i < fields.size() - 1; i++)
	{
		fv = fields[i];
		fv.vartostr();		

		strcat_s(ins, MAX_PATH, fv.buf);
		strcat_s(ins, MAX_PATH, ",");		
	}
	
	fv = fields[i];
	fv.vartostr();
	
	sprintf_s(sql, MAX_PATH, SEL, ptable);
	strcat_s(sql, MAX_PATH, _where);

	strcat_s(ins, MAX_PATH, fv.buf);	
	strcat_s(ins, MAX_PATH, ")");
	strcat_s(upd, MAX_PATH, _where);

	try
	{
		rs->Open(sql, m_cn_dst.GetInterfacePtr(), adOpenStatic, adLockOptimistic, adCmdText);
		n = rs->GetRecordCount();
		rs->Close();

		if (n != 0) strcpy_s(sql, MAX_PATH, upd);				// update	
		else strcpy_s(sql, MAX_PATH, ins);						// insert
		
		rs->Open(sql, m_cn_dst.GetInterfacePtr(), adOpenForwardOnly, adLockReadOnly, adCmdText);
		rs->Close();
	}
	catch (_com_error& e)
	{
		strcpy_s(msg, MAX_PATH, (PSTR)e.Description());		
	}
	
	rs.Release();
	return 0;
}
bool CData::open(PCSTR _Src, PCSTR _Dst)
{
	bool bres = 1;  	
	
	try
	{	
		sprintf_s(con, MAX_PATH, JET, _Src);
		m_cn_src->Open(con, "", "", 0);

		sprintf_s(con, MAX_PATH, JET, _Dst);
		m_cn_dst->Open(con, "", "", 0);		
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
	if (m_cn_src->GetState() == adStateOpen) m_cn_src->Close();
	if (m_cn_dst->GetState() == adStateOpen) m_cn_dst->Close();
}
void CData::update()
{
	long i, j, n, nkey, nfield;
	size_t PtNum;

	char sql[MAX_PATH];
	_variant_t v;

	FLD_VAL fv;
	KEY_VAL kv;	

	std::vector<FLD_VAL> fields;
	std::vector<KEY_VAL> keys;	

	FieldPtr fp;
	_RecordsetPtr rs;
	rs.CreateInstance(__uuidof(Recordset));	

	for (i = 0; i < 1;/*SZ*/ i++)
	{		
		switch (i)
		{
			case 0:
			{
				nkey = 0;
				break;
			}
		}

		sprintf_s(sql, MAX_PATH, SEL, ptables[i]);
		try
		{			
			// source
			n = 0;
			rs->Open(sql, m_cn_src.GetInterfacePtr(), adOpenForwardOnly, adLockOptimistic, adCmdText);		
			nfield = rs->Fields->Count;
			
			while (!rs->__EOF)
			{	
				// keys
				for (j = 0; j < nkey; j++)
				{
					fp = rs->Fields->Item[j];
					v = fp->Name;
					
					wcstombs_s(&PtNum, kv.name, v.bstrVal, 16);	
					kv.v = fp->Value; 
					
					keys.push_back(kv);			
				}
				// fields
				for (j = nkey; j < nfield; j++)
				{
					fp = rs->Fields->Item[j];
					v = fp->Name;					
					
					wcstombs_s(&PtNum, fv.name, v.bstrVal, 16);	
					fv.v = fp->Value; 
					
					fv.vartostr();


					OemToChar(fv.buf, fv.buf);
					fields.push_back(fv);	

								
				}
				//compare(keys, fields, ptables[i]);
				
				keys.clear();
				fields.clear();

				rs->MoveNext();
				n++;
			}	
			
			rs->Close();
		}
		catch (_com_error& e)
		{
			strcpy_s(msg, MAX_PATH, (PSTR)e.Description());				
		}

		

		
	
		
	}

	rs.Release();
	
}