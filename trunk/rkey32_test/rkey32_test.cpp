// rkey32_test.cpp : Defines the entry point for the console application.
#pragma once
#pragma comment(lib, "..\\rkey32\\debug\\rkey32")
#include "..\rkey32\rkey32.h"
#include <string>
#include <tchar.h>
#include <windows.h>

using namespace std;
typedef bool (*A)(LPCTSTR, LPCTSTR, LPCTSTR); 
typedef LPTSTR (*A1)(LPCTSTR, LPCTSTR); 

int _tmain(int argc, _TCHAR* argv[])
{
	
	LPCTSTR a_0 = L"Ключ: Основной\r\nПродукт: БАРС.ЭПК\r\nУчреждение: Администрация Ближнеосиновского сельского поселения Суровикинского района\r\nИНН: 3430008173\r\nКПП: 343001001\r\nДата окончания: 27.08.2010\r\nБэк-Офис: 0\r\nФронт-Офис: 1\r\nКоличество учреждений:1\r\nКод учреждения: ea8048208d4a",
	  	   a_1 = L"БАРС.ЭПК",
		   
		a_2 = L"dpX6r+5yHULhxacQV1ZPrxqhVn5kEAjuCYE1wuKMPcK31+32+9qtRQRQ2rBbnySHxFtYIiO5GgkaqKbMdnqRottcP90A007qwDxfulo4pK5LvAz6k5NgEtfMHcWuJ7nmcW+WQ8nY7Dp6Tjx3IziBMe61XyPUBVZOMe61oaBBwRJ/0AbW2qwG2CBxVUCA9x0xOnqDpTzEZrW5C3NgDHGXRlZ5tTNGHxDAiteytK14EcVZE07xJmhPUiVmsY52DLm0H8Zz3Nks1AXStAY=";
	bool b = a(a_0, a_2, a_1);	
	LPTSTR res = a1(a_0, a_1);

	/*HMODULE h = LoadLibrary(L"rkey32.dll");
	DWORD dw = GetLastError();
	if (h)
	{
		A Proc = (A)GetProcAddress(h, "a");
		A1 Proc1 = (A1)GetProcAddress(h, "a1");
		if (Proc)  b = Proc(a_0, a_2, a_1);	
		if (Proc1)  res = Proc1(a_0, a_1);	
		FreeLibrary(h);
	}*/

	
	wprintf(res);
	return 0;
}

