// rkey32.cpp
#include "rkey32.h"
#include <list>
#include <stdexcept>

#define		b	7	
#define		c	5
static const char cb64[]="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";

BOOL APIENTRY DllMain(HANDLE hModule, DWORD ul_reason_for_call, LPVOID lpReserved)
{
	switch (ul_reason_for_call)
	{
		case DLL_PROCESS_ATTACH:
		case DLL_THREAD_ATTACH:
		case DLL_THREAD_DETACH:
		case DLL_PROCESS_DETACH: break;
	}
    return 1;
}
void encode64(byte in[3], byte out[4], int len)
{
	out[0] = cb64[in[0] >> 2];
    out[1] = cb64[((in[0] & 0x03) << 4) | (in[1] >> 4)];
    out[2] = len > 1 ? cb64[((in[1] & 0x0f) << 2) | (in[2] >> 6)] : '=';
    out[3] = len > 2 ? cb64[in[2] & 0x3f] : '=';
}
wstring tobase64(list<byte> inList)
{		
	wstring str = L"";
	byte in[3] = {0}, out[4], n = 0;	

	list <byte>::iterator iter = inList.begin();
	while (iter != inList.end())
	{
		in[n] = (byte)*iter;
		n++;

		if (n == 3)
		{
			encode64(in, out, n);
			str += out[0];
			str += out[1];
			str += out[2];
			str += out[3];
			memset(in, 0, 3);
			n = 0;
		}		
		iter++;
	}
	if (n > 0) 
	{
		encode64(in, out, n);
		str += out[0];
		str += out[1];
		str += out[2];
		str += out[3];
	}	
	return str;
}
list<wstring> a2(wstring A_0, size_t a)
{
	list<wstring> list1;
	if (A_0.empty()) throw invalid_argument("The string is not given..");
	
	for (wstring str = A_0; str.length() > 0; str = str.substr(a - 3))
	{
		if (str.length() <= a)
		{
			list1.push_back(str);
			break;
		}
		list1.push_back(str.substr(0, a));
	}   
    return list1;
}

_dll bool a(LPCTSTR a_0, LPCTSTR a_1, LPCTSTR a_2)
{
	//if (a_0 == 0 || a_1 == 0 || a_2 == 0) throw invalid_argument("Invalid arguments..");    
	return (wcscmp(a_1, a1(a_0, a_2)) == 0);
}
_dll LPTSTR a1(LPCTSTR a_0, LPCTSTR a_1)
{
	wstring wa_0 = a_0, wa_1 = a_1;	
	list<wstring> list1, temp;	
	// a2, c
	temp = a2(wa_0 + wa_1, c);
	list1.insert(list1.end(), temp.begin(), temp.end());	
	// a2, b
	temp = a2(wa_0 + wa_1, b);
	list1.insert(list1.end(), temp.begin(), temp.end());	
	// 
	size_t i;
	int num2;
	wstring str;
	list <wstring>::iterator iter;
	list<byte> list2;	
	for (iter = list1.begin(); iter != list1.end(); iter++)
	{
		num2 = 1;
		str = *iter;		
		for (i = 0; i < str.length(); i++)
		{
			num2 = ((3 * num2) + str[i]) + 7;
		}
		list2.push_back(num2 % 0xfd);		
	}
	list1.clear();	
	str = tobase64(list2);
	size_t sz = str.length();

	LPTSTR res = new TCHAR[++sz];
	wcscpy_s(res, sz, str.c_str());	
	return res;
}