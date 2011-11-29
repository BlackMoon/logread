// rkey32.h
#pragma once
#include <string>
#include <windows.h>

#ifdef _exp
#define _dll __declspec(dllexport)
#else
#define _dll __declspec(dllimport)
#endif

using namespace std;
extern "C"
{
	_dll bool a(LPCTSTR a_0, LPCTSTR a_1, LPCTSTR a_2);	
	_dll LPTSTR a1(LPCTSTR a_0, LPCTSTR a_1);	
}