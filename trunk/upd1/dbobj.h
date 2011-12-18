// dbobj.h
#pragma once
#import "libid:EF53050B-882E-4776-B643-EDA472E8E3F2" no_namespace auto_rename
#include <vector>

#define FRST					0x1
#define	LAST					0x2
#define SUMM					0x3

typedef struct FLD_VAL
{
	char		buf[664], name[16];
	_variant_t	v;
	FLD_VAL()
	{
		memset(buf, 0, 664);
		memset(name, 0, 16);		
		
		v.vt = VT_EMPTY;
	}
	void vartostr()
	{
		size_t PtNum;
		switch (v.vt)
		{
			case VT_R8:
			{
				sprintf_s(buf, 664, "%.2f", v.dblVal);				
				break;
			}
			case VT_BSTR:
			{
				char bstr[664];
				wcstombs_s(&PtNum, bstr, v.bstrVal, 664);	
				sprintf_s(buf, 664, "'%s'", bstr);				
				break;
			}
		}			 
	}
} *PFLD_VAL;
typedef struct KEY_VAL : FLD_VAL
{
	char	line[64];
	KEY_VAL() : FLD_VAL()
	{
		memset(line, 0, 64);
	}
	void vartoline()
	{
		vartostr();
		sprintf_s(line, 64, "%s=%s", name, buf);
	}

} *PKEY_VAL; 
class CData
{
private:
	bool compare(std::vector<KEY_VAL>, std::vector<FLD_VAL>, PCSTR, byte state = FRST);
	void checkDest(_RecordsetPtr, PSTR);		// check destination, if not exists - create
public:
	CData();
	~CData();
	bool open(PCSTR, PCSTR);		
	void update();
	void close();
// Attributes
private:
	char					con[MAX_PATH];
	_ConnectionPtr			m_cn_src, m_cn_dst;
public:	
	char					msg[MAX_PATH];		
};