// logreader.h
#pragma once
#include "malloc.h"
#include <windows.h>

#define EOLN		(char*)"\r\n"
#define RLENGTH		64

struct regX
{	
	char*	quants;
	char**	words;
	bool	wordBegins;				// begins with letter
	int		count_q, count_w;
	inline regX()
	{
		count_q = count_w = 0;		
		wordBegins = 0;
		quants = 0;
		words = 0;
	}
	inline void addQuant(const char q)
	{
		count_q++;
		quants = (char*)realloc(quants, count_q);		
		quants[count_q - 1] = q;
	}
	inline void addWord(const char* w)
	{
		count_w++;
		words = (char**)realloc(words, count_w * RLENGTH);
		words[count_w - 1] = new char[RLENGTH];
		memset(words[count_w - 1], 0, RLENGTH);
		strcpy_s(words[count_w - 1], RLENGTH, w);
	}
	inline void clear()
	{
		if (quants) {
			delete [] quants;
			quants = 0;
		}
		
		if (words) {		
			for (int i = 0; i < count_w; i++) 
			{
				delete [] words[i];
				words[i] = 0;
			}
			delete [] words;
			words = 0;
		}
	}
	bool match(char* buf, const int size);
};
class CLogReader
{
public:
	CLogReader();
	bool getNextLine(char* buf, const int size); 
	bool open(const char* filename);
	bool setFilter(const char* filter);	
	void close();	
	~CLogReader();
// Attributes
private:
	char				_filter[MAX_PATH];
	__int64				qwFileSize;	
	regX				rgx;
	HANDLE				hFile, hMap;	
public:	
	__int64				qwOffset;			// file offset where the view begins
};