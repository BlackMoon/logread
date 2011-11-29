//logreader.cpp
#include "logreader.h"

extern	DWORD	ALLOC_GRANULARITY;

bool regX::match(char* buf, const int size)
{
	char *pdest = 0;
	bool res = 1;
	size_t index = 0;
	switch (count_w)
	{
		case 0: 
			res = 1;
			break;
		case 1: 
			pdest = strstr(buf, words[0]);
			// chech begining
			if ((quants[index] == '^') && (!wordBegins)) res = ((size_t)pdest == 0);
			// check ending
			else {
				index = (count_q - 1);
				if (quants[index] == '$') res = ((size_t)pdest + strlen(words[0]) == strlen(buf));
			}			
			break;
		default:			
			int i;	
			size_t* pos = new size_t[count_w];
			memset(pos, 0, count_w << 2);
			for (i = 0; i < count_w; i++)
			{
				pdest = strstr(buf, words[i]);
				if (pdest) pos[i] = (size_t)pdest - (size_t)buf;
			}
			// chech begining
			if ((quants[index] == '^') && (!wordBegins)) {
				res = (pos[0] == 0);
				break;
			}
			// check words order			
			size_t p = pos[0] + strlen(words[0]) ;
			for (i = 1; i < count_w; i++)
			{
				if (pos[i] < p) {
					res = 0;
					break;
				}
				else {										
					if (quants[index] == '?') {
						if (wordBegins) {
							if (p == pos[i]) {
								res = 0;
								break;
							}
						}
						else {
								if (p == 0) {
								res = 0; 
								break;								
							}
						}
					}					
				}
				p = pos[i] + strlen(words[i]);
				index = min(i, count_q);
			}			
			// check ending			
			index = (count_q - 1);
			if (quants[index] == '$') res = (pos[i] + strlen(words[i]) == strlen(buf));				

			delete [] pos;
			break;		
	}
	return res;
}
CLogReader::CLogReader()
{
}
bool CLogReader::getNextLine(char* buf, const int size)
{	
	DWORD dwBytesInBlock = ALLOC_GRANULARITY;	
	if (qwFileSize < ALLOC_GRANULARITY) dwBytesInBlock = (DWORD)qwFileSize;

	size_t dwStrOffset = (DWORD)qwOffset % ALLOC_GRANULARITY;	// count from what string read
	__int64 qwFileOffset = ALLOC_GRANULARITY * (qwOffset / ALLOC_GRANULARITY);	// count from what block read
	
	char *pdest, *pfile, *pfile1;
	pdest = pfile = pfile1 = 0;
	size_t count = 0;
	while(qwFileOffset < qwFileSize)
	{
		pfile = (char*)MapViewOfFile(hMap, FILE_MAP_READ, (DWORD)(qwFileOffset >> 32), (DWORD)(qwFileOffset & 0xFFFFFFFF), 0); 				
		pfile1 = pfile + dwStrOffset;
		// find \r\n symbol
		pdest = strstr(pfile1, EOLN);	
		while (pdest)
		{					
			count = (size_t)pdest - (size_t)pfile1 + 2;		// for  \r&\n
			strncpy_s(buf, size, pfile1, count);
			qwOffset += count;					
			if (rgx.match(buf, size)) {
				UnmapViewOfFile(pfile);
				return 1;
			}
			
			memset(buf, 0, size);
			pfile1 += count;	
			pdest = strstr(pfile1, EOLN);
		}
		// if there's no lines - go to next block
		UnmapViewOfFile(pfile);			
		qwFileOffset += dwBytesInBlock;		
		dwStrOffset = 0;
	}
	
	return 0;
}
bool CLogReader::open(const char* filename)
{
	hFile = CreateFile(filename, GENERIC_READ, 0, 0, OPEN_EXISTING, FILE_FLAG_NO_BUFFERING, 0);
	if (hFile == INVALID_HANDLE_VALUE) return 0;
	hMap = CreateFileMapping(hFile, 0, PAGE_READONLY | SEC_COMMIT | SEC_NOCACHE, 0, 0, "fileMapping");
	
	DWORD dwFileSizeHigh;
	qwFileSize = GetFileSize(hFile, &dwFileSizeHigh);
	qwFileSize += (((__int64)dwFileSizeHigh) << 32); 
	qwOffset = 0;
	CloseHandle(hFile);

	return 1;
}
bool CLogReader::setFilter(const char* filter)
{
	rgx.clear();
	size_t len = strlen(filter);
	if (len > 0) {
		
		char last_symb, symb;
		last_symb = filter[0];
		if ((last_symb == '*') || (last_symb == '?')) rgx.addQuant(last_symb);
		else rgx.wordBegins = 1;

		for (size_t i = 1; i < len; i++)
		{
			symb = filter[i];
			if ((symb == '*') || (symb == '?')) {
				if (symb != last_symb) rgx.addQuant(symb);
			}
			last_symb = symb;
		}
		
		char *next_token, *token;
		char seps[] = "*,?";			

		token = strtok_s((char*)filter, seps, &next_token); 
		while (token)
		{
			rgx.addWord(token);	
			token = strtok_s(NULL, seps, &next_token); 
		}
		return 1;
	}
	return 0;
}
void CLogReader::close()
{
	if (hMap) CloseHandle(hMap);
	hMap = 0;	
	rgx.clear();
}
CLogReader::~CLogReader()
{	
	close();
}