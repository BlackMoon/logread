// dbobj.h
#pragma once
#pragma warning (disable: 4192) 

/*#import "libid:2DF8D04C-5BFA-101B-BDE5-00AA0044DE52" auto_rename 
#import "libid:0002E157-0000-0000-C000-000000000046" auto_rename 
#import "libid:00020813-0000-0000-C000-000000000046" auto_rename 		*/

#import "libid:EF53050B-882E-4776-B643-EDA472E8E3F2" no_namespace auto_rename


#include <atlbase.h>

#define	N_VAL				(float)-1.0f
#define HN_OIL				1
#define HN_WAT				3
#define HD_NUM				12
#define ITEM_H				23
#define L_SIZE				64

#define I_DB				1								// dBase
#define I_PD				2								// Pdox
#define CHUNKSIZE			9

#define JET					"Provider=Microsoft.Jet.OLEDB.4.0; Extended Properties=Paradox 5.x; Data Source=%s;"
#define DBASE				"Driver={Microsoft dBASE Driver (*.dbf)}; DriverID=277; Dbq=%s;"
#define PDOX				"Driver={Microsoft Paradox Driver (*.db )}; DriverID=538; Fil=Paradox 5.X; DefaultDir=%s; Dbq=%s; CollatingSequence=ASCII;"
//#define DBASE				"dBase IV;"
//#define PDOX				"Paradox 5.x;"

#define SEL_BL				"SELECT * FROM [block.db]"	
#define SEL_PL				"SELECT DISTINCT pl FROM [%s]"
#define SEL_NC				"SELECT DISTINCT TRIM(nc) FROM [%s]"
#define SEL_DB				"SELECT pl, TRIM(nc), apik, apip, hn, m, s, k FROM [%s]"
#define SEL_PD				"SELECT pl, TRIM(nc), apk, app, hn, m, s, k FROM [%s]"

const char					headers[HD_NUM][16] = {"пласт", "скв.", "тол. эфф.", "тол. общ.", "пор.", "пор. н.", "пор. в.", 
									  	  	       "прониц.", "прониц. н.", "прониц. в.", "нач. нефт.", "коэфф. песч."};

// CTypedPtrArrayEx
template<class BASE_CLASS, class TYPE>
class CTypedPtrArrayEx : public CTypedPtrArray<BASE_CLASS, TYPE>
{
public:
	void RemoveAll()
	{		
		for (int i = 0; i < GetSize(); i++)
		{
			delete GetAt(i);
		}				
		BASE_CLASS::RemoveAll();
	}
};
// param
struct param
{
	float					apk, app;
	float					m;
	float					s;
	float					k;
	UCHAR					hn;
	inline param()
	{		
		apk = app = N_VAL;
		m = s = k = N_VAL;
		hn = 0;
	}
	inline float height()
	{
		return apk - app;
	}
};
// COper
class COper
{
public:
	COper(){h = oh = wh = 0.0f;}
// Overrides		
	virtual void calcRes() = 0;		
// Attributes
public:
	float					h;
	float					oh;
	float					wh;
};
class CWell : public COper
{
public:
	CWell(PCSTR);
	~CWell();	
	void calcRes();	
// Attributers	
public:
	char					nc[9];
	float					m;										// numerators
	float					oil_m;
	float					wat_m;
	float					k;
	float					oil_k;
	float					wat_k;
	float					s;
	float					coeff;	
	float					mh;										// denominator
	CTypedPtrArrayEx<CPtrArray, param*>		params;	
};
// CHorz
class CHorz : public COper
{
public:
	CHorz(USHORT);	
	~CHorz();		
	void calcRes();	
// Attributes	
public:	
	USHORT					index;
	CTypedPtrArrayEx<CPtrArray, CWell*>		wells;	
};
// CData
class CData
{	
public:	
	CData();
	~CData();	
	bool fillBlock(CComboBox*);			
	bool fillIn(CCheckListBox*, CCheckListBox*);			
	bool load();
	bool open(PCSTR, PCSTR, PCSTR);				
	bool saveTxt(PCSTR); 
//	bool saveXls(PCSTR); 	
	void close();		
	void fillOut(CListCtrl*);		
// Attributes
private:	
	char					con[MAX_PATH];
	char					file[L_SIZE];
	char					path[MAX_PATH];		
	float					h;
	float					oh;	
	float					m;
	float					oil_m;
	float					wat_m;
	float					k;
	float					oil_k;
	float					wat_k;
	float					s;
	float					coeff;		
	enum 
	{
		iDB,
		iPdox
	}			m_isam;		
	_ConnectionPtr			m_cn;
//	CDaoDatabase			m_db;			
	CTypedPtrArrayEx<CPtrArray, CHorz*>		m_horzs;
public:
	char					msg[MAX_PATH];	
	CArray<UINT, UINT>		m_codes;
	CStringArray			m_wells;	
};