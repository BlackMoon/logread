// dbocbj.cpp
#include <stdafx.h>
#include "dbobj.h"

// CWell
CWell::CWell(PCSTR name)
{
	strcpy_s(nc, 9, name);	
	m = oil_m = wat_m = 0.0f;
	k = oil_k = wat_k = 0.0f;
	s = 0.0f;
	h = oh = wh = mh = 0.0f;
}
CWell::~CWell()
{
	params.RemoveAll();
}
void CWell::calcRes()
{	
	float _h;
	param* pparam;	

	for (int i = 0; i < params.GetSize(); i++)
	{
		pparam = params[i];
		_h = pparam->height();
		h += _h; 
		switch (pparam->hn)
		{
			case HN_OIL:
			{
				oh += _h;				
				if (pparam->m != N_VAL) 
				{
					m += pparam->m * _h;  
					oil_m += pparam->m * _h;  				
					
					if (pparam->s != N_VAL) s += pparam->m * pparam->s * _h;					
					mh += pparam->m * _h;
				}

				if (pparam->k != N_VAL)
				{
					k += pparam->k * _h;  
					oil_k += pparam->k * _h;  				
				}								
				
				break;
			}
			case HN_WAT:
			{				
				wh += _h;

				if (pparam->m != N_VAL)
				{
					m += pparam->m * _h;  
					wat_m += pparam->m * _h;  				
				}

				if (pparam->k != N_VAL)
				{
					k += pparam->k * _h;  
					oil_k += pparam->k * _h;  				
				}				

				break;
			}
		}		
	}	
}
// CHorz
CHorz::CHorz(USHORT _index)
{	
	index = _index;		
}
CHorz::~CHorz()
{
	wells.RemoveAll();
}
void CHorz::calcRes()
{
	CWell* pwell;
	for (int i = 0; i < wells.GetSize(); i++)
	{
		pwell = wells[i];
		h += pwell->h;
		oh += pwell->oh;
		wh += pwell->wh;
	}
}
// CData
CData::CData()
{	
	memset(msg, 0, MAX_PATH);		
	HRESULT hr = m_cn.CreateInstance("ADODB.Connection");
}
CData::~CData()
{
	close();
}
bool CData::fillBlock(CComboBox* pcombo)
{
	bool bres = 1;
	char line[32] = {0}, _text[MAX_PATH] = {0};

	_RecordsetPtr rs("ADODB.Recordset");
	rs->PutRefActiveConnection(m_cn);

	strcpy_s(_text, MAX_PATH, SEL_BL);
	pcombo->SendMessage(CB_RESETCONTENT);	

	ADO_LONGPTR size = 0;
	
	char* p;
	_variant_t v;	
	
	try
	{
		rs->Open(_text, vtMissing, adOpenKeyset, adLockOptimistic, adCmdText);		
		while (!rs->__EOF)
		{			
			strcpy_s(_text, MAX_PATH, (PSTR)(_bstr_t)rs->Fields->Item[0l]->Value); 
			
			v = rs->Fields->Item[2l]->Value;
			if (v.vt != VT_NULL) sprintf_s(_text, MAX_PATH, "%s - %s", _text, (PSTR)(_bstr_t)v);			
			
			strncpy_s(line, 32, _text, 31);
			pcombo->SendMessage(CB_ADDSTRING, 0, (LPARAM)line);
			pcombo->SendMessage(CB_SETITEMHEIGHT, 0, MAKELONG(ITEM_H, 0)); 			
			
			long s = 0;
			rs->Fields->get_Count(&s);
//			rs->get_Collect(

			v = rs->Fields->Item[5l]->Value;

			if (v.vt != VT_NULL)
			{
				p = (PSTR)(_bstr_t)v;
			}			
			
			rs->MoveNext();
		}
		rs->Close();

		pcombo->SendMessage(CB_SETCURSEL, 0, 0);
		pcombo->EnableWindow(1);
	}
	catch (_com_error& e)
	{	
		strcpy_s(msg, MAX_PATH, (PSTR)e.Description());
		bres = 0;
	}
	return bres;
}
bool CData::fillIn(CCheckListBox* phlb, CCheckListBox* pwlb)
{
	char line[16] = {0}, sql[MAX_PATH] = {0};
	int index;	
	
	_RecordsetPtr rs("ADODB.Recordset");   
	rs->PutRefActiveConnection(m_cn);
	// horzs
	sprintf_s(sql, MAX_PATH, SEL_PL, file);		
	rs->Open(sql, vtMissing, adOpenKeyset, adLockOptimistic, adCmdText);
	
	index = 0;
	phlb->SendMessage(LB_RESETCONTENT);

	while (!rs->__EOF)
	{
		sprintf_s(line, 16, " %d", rs->Fields->Item[0l]->Value.iVal);
		phlb->SendMessage(LB_SETITEMHEIGHT, index, MAKELONG(ITEM_H, 0));
		phlb->SendMessage(LB_INSERTSTRING, index, (LPARAM)line); 		

		rs->MoveNext();
		index++;
	}
	rs->Close();
	// wells
	sprintf_s(sql, MAX_PATH, SEL_NC, file);
	rs->Open(sql, vtMissing, adOpenKeyset, adLockOptimistic, adCmdText);
	
	index = 0;
	pwlb->SendMessage(LB_RESETCONTENT);
	
	while (!rs->__EOF)
	{			
		sprintf_s(line, 16, " %s", (PSTR)(_bstr_t)rs->Fields->Item[0l]->Value);
		pwlb->SendMessage(LB_SETITEMHEIGHT, index, MAKELONG(ITEM_H, 0));
		pwlb->SendMessage(LB_INSERTSTRING, index, (LPARAM)line); 		

		rs->MoveNext();
		index++;
	}
	rs->Close();
	
	return 1;
}
bool CData::load()
{	
	m_horzs.RemoveAll();
	
	INT_PTR i, csz = m_codes.GetSize(), wsz = m_wells.GetSize();	
	if ((csz == 0) || (wsz == 0)) return 1;

	bool bres = 1;	
	char line[16] = {0}, sql[MAX_PATH] = {0}, _where[MAX_PATH] = {0};	

	_RecordsetPtr rs("ADODB.Recordset");
	rs->PutRefActiveConnection(m_cn);

	switch (m_isam)
	{
		case iDB:
		{
			sprintf_s(sql, MAX_PATH, SEL_DB, file);
			break;
		}
		case iPdox:
		{
			sprintf_s(sql, MAX_PATH, SEL_PD, file);
			break;
		}
	}	
	// pl
	strcpy_s(_where, MAX_PATH, " WHERE pl in (");
	for (i = 0; i < csz - 1; i++)
	{
		sprintf_s(line, 16, "%d, ", m_codes[i]); 
		strcat_s(_where, MAX_PATH, line);
	}
	sprintf_s(line, 16, "%d)", m_codes[i]); 
	strcat_s(_where, MAX_PATH, line);
	// nc		
	strcat_s(_where, MAX_PATH, " AND TRIM(nc) in (");	
	for (i = 0; i < wsz - 1; i++)
	{		
		sprintf_s(line, 16, "\'%s\', ", m_wells[i]); 
		strcat_s(_where, MAX_PATH, line);
	}
	sprintf_s(line, 16, "\'%s\')", m_wells[i]); 
	strcat_s(_where, MAX_PATH, line);
	strcat_s(_where, MAX_PATH, " AND hn in (1, 3) ORDER BY pl, nc");		
	strcat_s(sql, MAX_PATH, _where);
	
	char nc[16] = {0}; 
	USHORT pl = 0;
	_variant_t v0, v1;

	param* pparam = 0;
	CWell* pwell = 0;
	CHorz* phorz = 0;	

	try
	{
		rs->Open(sql, vtMissing, adOpenKeyset, adLockOptimistic, adCmdText);		
		while (!rs->__EOF)
		{
			v0 = rs->Fields->Item[0l]->Value;
			v1 = rs->Fields->Item[1l]->Value;					

			if (pl != v0.iVal) 
			{			
				pl = v0.iVal;
				phorz = new CHorz(pl);			
				m_horzs.Add(phorz);	
			
				memset(nc, 0, 9);
			}		
			if (strcmp(nc, (PCSTR)(_bstr_t)v1) != 0)
			{			
				strcpy_s(nc, 16, (PCSTR)(_bstr_t)v1);
				pwell = new CWell(nc);				
				phorz->wells.Add(pwell);						
			}		
			pparam = new param();
			// apk
			v0 = rs->Fields->Item[2l]->Value;
			if (v0.vt != VT_NULL) pparam->apk = (float)v0.dblVal;
			// app
			v0 = rs->Fields->Item[3l]->Value;
			if (v0.vt != VT_NULL) pparam->app = (float)v0.dblVal;							
			// hn
			v0 = rs->Fields->Item[4l]->Value;
			if (v0.vt != VT_NULL) pparam->hn = v0.bVal;
			// m
			v0 = rs->Fields->Item[5l]->Value;
			if (v0.vt != VT_NULL) pparam->m = (float)v0.dblVal;							
			// s
			v0 = rs->Fields->Item[6l]->Value;
			if (v0.vt != VT_NULL) pparam->s = (float)v0.dblVal;				
			// k
			v0 = rs->Fields->Item[7l]->Value;
			if (v0.vt != VT_NULL) pparam->k = (float)v0.dblVal;	

			pwell->params.Add(pparam);
			rs->MoveNext();
		}
		rs->Close();	
	}			
	catch (_com_error&)
	{
		strcpy_s(msg, MAX_PATH, "Неверный формат таблицы");
		bres = 0;
	}

	return bres;
}
bool CData::open(PCSTR _path, PCSTR _file, PCSTR _ext)
{	
	bool bres = 1;
	if (strcmp(_ext, "db") == 0)
	{
		m_isam = iPdox;
//		sprintf_s(con, MAX_PATH, PDOX, _path, _path);
		sprintf_s(con, MAX_PATH, JET, _path);
	}
	else if (strcmp(_ext, "dbf") == 0)
	{
		m_isam = iDB;
		sprintf_s(con, MAX_PATH, DBASE, _path);
	}		
	strcpy_s(file, L_SIZE, _file);		
	
	try
	{		
		m_cn->Open(con, "", "", 0);
	}
	catch (_com_error& e)
	{
		strcpy_s(msg, MAX_PATH, (PSTR)e.Description());
		bres = 0;
	}
	
	return bres;
}
bool CData::saveTxt(PCSTR file)
{
	bool bres = 0;
	FILE* stream;
	
	if (fopen_s(&stream, file, "wt") == 0)
	{
		int i, j;
		// header
		for (i = 0; i < HD_NUM; i++)
		{
			fprintf_s(stream, "%s\t", headers[i], 16);		
		}
		fprintf_s(stream, "\n");	
		// wells
		CHorz* phorz;
		CWell* pwell;	
		for (i = 0; i < m_horzs.GetSize(); i++)
		{
			phorz = m_horzs[i];		
			fprintf_s(stream, "%d\n", phorz->index);				

			for (j = 0; j < phorz->wells.GetSize(); j++)
			{
				pwell = phorz->wells[j];
				fprintf_s(stream, "\t%s\t", pwell->nc, 9);
				fprintf_s(stream, "%.3f\t%.3f\t%.3f\t%.3f\t%.3f\t%.3f\t%.3f\t%.3f\t%.3f\t%.3f\n",  
						  pwell->oh, pwell->h, 
						  pwell->m, pwell->oil_m, pwell->wat_m, 
						  pwell->k, pwell->oil_k, pwell->wat_k, 
						  pwell->s, pwell->coeff);				
			}
		}	
		fprintf_s(stream, "Всего\t\t%.3f\t%.3f\t%.3f\t%.3f\t%.3f\t%.3f\t%.3f\t%.3f\t%.3f\t%.3f\n",  
						  oh, h, m, oil_m, wat_m, k, oil_k, wat_k, s, coeff);				
		fclose(stream);
		bres = 1;
	}	
	return bres;
}
/*bool CData::saveXls(PCSTR file)
{
	using namespace Excel;		
	
	bool bres = 0;
	_ApplicationPtr xl("Excel.Application");		
	
	if (xl)
	{
		xl->Visible[0] = VARIANT_FALSE;
		xl->DisplayAlerts[0] = VARIANT_FALSE;
		xl->UserControl = VARIANT_FALSE;
	
		_WorkbookPtr book = xl->Workbooks->Add();
		_WorksheetPtr sheet = book->GetWorksheets()->Item[1L];		

		int i, j, k = 1;
		// header
		for (i = 0; i < HD_NUM; i++)
		{
			sheet->Cells->Item[k, i + 1] = headers[i];
		}
		k++;
		// wells
		CHorz* phorz;
		CWell* pwell;	
		for (i = 0; i < m_horzs.GetSize(); i++)
		{
			phorz = m_horzs[i];		
			sheet->Cells->Item[k, 1] = phorz->index;
			k++;

			for (j = 0; j < phorz->wells.GetSize(); j++)
			{
				pwell = phorz->wells[j];						

				sheet->Cells->Item[k, 2] = pwell->nc;
				sheet->Cells->Item[k, 3] = pwell->oh;
				sheet->Cells->Item[k, 4] = pwell->h;
				sheet->Cells->Item[k, 5] = pwell->m;
				sheet->Cells->Item[k, 6] = pwell->oil_m;
				sheet->Cells->Item[k, 7] = pwell->wat_m;
				sheet->Cells->Item[k, 8] = pwell->k;
				sheet->Cells->Item[k, 9] = pwell->oil_k;
				sheet->Cells->Item[k, 10] = pwell->wat_k;
				sheet->Cells->Item[k, 11] = pwell->s;
				sheet->Cells->Item[k, 12] = pwell->coeff;			
				k++;
			}
		}	

		sheet->Cells->Item[k, 1] = "Всего";	
		sheet->Cells->Item[k, 3] = oh;
		sheet->Cells->Item[k, 4] = h;
		sheet->Cells->Item[k, 5] = m;
		sheet->Cells->Item[k, 6] = oil_m;
		sheet->Cells->Item[k, 7] = wat_m;
		sheet->Cells->Item[k, 8] = k;
		sheet->Cells->Item[k, 9] = oil_k;
		sheet->Cells->Item[k, 10] = wat_k;
		sheet->Cells->Item[k, 11] = s;
		sheet->Cells->Item[k, 12] = coeff;		
	
		RangePtr range = sheet->GetRange(sheet->Cells->Item[2, 3], sheet->Cells->Item[k, 12]);
		range->NumberFormat = "0.000";

		book->SaveAs(file, xlNormal, "", "", 0, 0, xlNoChange); 
		xl->Quit();

		sheet.Release();
		book.Release();	
		xl.Release();	

		bres = 1;
	}

	return bres;
}*/
void CData::fillOut(CListCtrl* plist)
{
	char line[16] = {0};
	float mh = 0.0f, wh = 0.0f;		
	
	h = oh = 0.0f;
	m = oil_m = wat_m = 0.0f,
	k = oil_k = wat_k = 0.0f,	
	s = coeff = 0.0f;

	int i, j, index = 0;
	param _param;

	CHorz* phorz;
	CWell* pwell;
	// wells
	plist->SendMessage(LVM_DELETEALLITEMS);
	for (i = 0; i < m_horzs.GetSize(); i++)
	{
		phorz = m_horzs[i];
		
		sprintf_s(line, 16, " %d", phorz->index);
		plist->InsertItem(index, line);		
		index++;

		for (j = 0; j < phorz->wells.GetSize(); j++)
		{
			pwell = phorz->wells[j];
			pwell->calcRes();

			h += pwell->h;
			oh += pwell->oh;
			wh += pwell->wh;
			mh += pwell->mh;			

			m += pwell->m;  
			oil_m += pwell->oil_m;  				
			wat_m += pwell->wat_m;  				

			k += pwell->k;  
			oil_k += pwell->oil_k;  				
			wat_k += pwell->wat_k;  				

			s += pwell->s;			
			// h
			if (pwell->h != 0.0f)
			{
				pwell->m /= pwell->h;
				pwell->k /= pwell->h;
				pwell->coeff = pwell->oh / pwell->h;				
			}			
			else 
			{
				pwell->m = 0.0f;
				pwell->k = 0.0f;
				pwell->coeff = 0.0f;			
			}
			// oh
			if (pwell->oh != 0.0f)
			{
				pwell->oil_m /= pwell->oh;
				pwell->oil_k /= pwell->oh;				
			}
			else 
			{
				pwell->oil_m = 0.0f;
				pwell->oil_k = 0.0f;				
			}
			// wh
			if (pwell->wh != 0.0f)
			{
				pwell->wat_m /= pwell->wh;
				pwell->wat_k /= pwell->wh;				
			}
			else
			{
				pwell->wat_m = 0.0f;
				pwell->wat_k = 0.0f;				
			}
			// mh
			if (pwell->mh != 0.0f) pwell->s /= pwell->mh;
			else pwell->s = 0.0f;
			
			plist->InsertItem(index, 0);
			plist->SetItemText(index, 1, pwell->nc);	

			sprintf_s(line, 16, " %.2f", pwell->oh);
			plist->SetItemText(index, 2, line);	

			sprintf_s(line, 16, " %.2f", pwell->h);
			plist->SetItemText(index, 3, line);	

			sprintf_s(line, 16, " %.3f", pwell->m);				
			plist->SetItemText(index, 4, line);					
				
			sprintf_s(line, 16, " %.3f", pwell->oil_m);							
			plist->SetItemText(index, 5, line);		

			sprintf_s(line, 16, " %.3f", pwell->wat_m);
			plist->SetItemText(index, 6, line);					
			
			sprintf_s(line, 16, " %.3f", pwell->k);
			plist->SetItemText(index, 7, line);					

			sprintf_s(line, 16, " %.3f", pwell->oil_k);
			plist->SetItemText(index, 8, line);					

			sprintf_s(line, 16, " %.3f", pwell->wat_k);
			plist->SetItemText(index, 9, line);					

			sprintf_s(line, 16, " %.3f", pwell->s);
			plist->SetItemText(index, 10, line);									
			
			sprintf_s(line, 16, " %.3f", pwell->coeff);
			plist->SetItemText(index, 11, line);								
			
			index++;
		}			
	}
	// total			
	if (h != 0.0f)
	{
		m /= h;
		k /= h;
		coeff = oh / h;	
	}
	else 
	{
		m = 0.0f;
		k = 0.0f;
		coeff = 0.0f;
	}
	if (oh != 0.0f)
	{
		oil_m /= oh;
		oil_k /= oh;
	}
	else 
	{
		oil_m = 0.0f;
		oil_k = 0.0f;
	}	
	if (wh != 0.0f)
	{
		wat_m /= wh;
		wat_k /= wh;		
	}
	else
	{
		wat_m = 0.0f;
		wat_k = 0.0f;
	}

	if (mh != 0.0f) s /= mh;
	else s = 0.0f;		

	strcpy_s(line, 16, "Всего");
	plist->InsertItem(index, line);				

	sprintf_s(line, 16, " %.2f", oh);
	plist->SetItemText(index, 2, line);	

	sprintf_s(line, 16, " %.2f", h);
	plist->SetItemText(index, 3, line);	
	
	sprintf_s(line, 16, " %.3f", m);
	plist->SetItemText(index, 4, line);	
	
	sprintf_s(line, 16, " %.3f", oil_m);
	plist->SetItemText(index, 5, line);	

	sprintf_s(line, 16, " %.3f", wat_m);
	plist->SetItemText(index, 6, line);	

	sprintf_s(line, 16, " %.3f", k);
	plist->SetItemText(index, 7, line);	

	sprintf_s(line, 16, " %.3f", oil_k);
	plist->SetItemText(index, 8, line);	

	sprintf_s(line, 16, " %.3f", wat_k);
	plist->SetItemText(index, 9, line);	

	sprintf_s(line, 16, " %.3f", s);
	plist->SetItemText(index, 10, line);	

	sprintf_s(line, 16, " %.3f", coeff);
	plist->SetItemText(index, 11, line);		
}
void CData::close()
{
	m_codes.RemoveAll();
	m_wells.RemoveAll();
	m_horzs.RemoveAll();		
	
	if (m_cn->GetState() == adStateOpen) m_cn->Close();
}