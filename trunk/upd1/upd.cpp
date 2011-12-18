// upd.cpp
#include <tchar.h>
#include "dbobj.h"
	
CData*							pdata = 0;
int _tmain(int argc, _TCHAR* argv[])
{
	pdata = new CData();
	pdata->open("D:\\skl\\1", "D:\\skl\\2");
	pdata->update();

	delete pdata;
	return 0;
}