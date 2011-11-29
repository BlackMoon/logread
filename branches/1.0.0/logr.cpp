// logr.cpp
#include "stdafx.h"
#include "logreader.h"

char	msg[MAX_PATH] = {0};
DWORD	ALLOC_GRANULARITY = 0;

int _tmain(int argc, _TCHAR* argv[])
{
	SYSTEM_INFO	sinf = {0};
	GetSystemInfo(&sinf);
	ALLOC_GRANULARITY = sinf.dwAllocationGranularity;

	setlocale(LC_ALL, "rus");	
	if (argc < 3) {
		switch (argc)
		{
			case 1: 
				printf("Не задан лог-файл");				
				return 0;
			case 2: 
				printf("Не задана маска");			
				return 0;					
		}		
	}	

	bool fWrite = 0;
	FILE* outf;
	if (argc == 4)
	{
		fopen_s(&outf, argv[3], "wt");	
		fWrite = 1;
	}

	char buf[MAX_PATH];
	CLogReader lr;
	if (lr.open((char*)argv[1])) {				
		lr.setFilter((char*)argv[2]);
		while (lr.getNextLine(buf, MAX_PATH)) {
			if (fWrite) fprintf_s(outf, buf);
			else printf(buf);
		}
		lr.close();
	}
	if (fWrite) fclose(outf);	
	return 0;
}