// dbobj.h
#pragma once
#pragma warning (disable: 4192) 

#import "libid:EF53050B-882E-4776-B643-EDA472E8E3F2" no_namespace auto_rename

#include <atlbase.h>
#include <windows.h>

#define	N_VAL				(float)-1.0f
#define JET					"Provider=Microsoft.Jet.OLEDB.4.0; Extended Properties=Paradox 5.x; Data Source=%s;"

#define SEL_PL				"SELECT DISTINCT pl FROM [pl%s.db]"
#define SEL_NC				"SELECT DISTINCT TRIM(nc), apk, pk, app, pp, m, s FROM [pi%s] WHERE pl = %d AND prk = 1"

// param
struct param
{
	float					apk, app;
	float					pk, pp;
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
// CWell
class CWell
{
public:
	CWell(PCSTR name){strcpy_s(nc, 9, name);};
	~CWell();	
// Attributers	
public:
	char					nc[9];
	float					apk, app;
	float					pk, pp;
};
// CData
class CData
{	
public:
	CData();
	~CData();
	bool open(PCSTR);	
	bool load(HWND);	
	bool run(int horz);
	void close();
// Attributes
private:
	char					con[MAX_PATH];
	_ConnectionPtr			m_cn;
public:
	char					alias[8];
	char					msg[MAX_PATH];		
};