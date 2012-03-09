// ForsageWnd.cpp
#include "ForsageWnd.h"
#include <Tlhelp32.h>

extern CHAR idx;
extern CHAR GetWindowIndex(DWORD pid);

BOOL CALLBACK HaveFoundMonitor(HMONITOR hMonitor, HDC hdcMonitor, LPRECT lprcMonitor, LPARAM dwData)
{
	(*(int*)dwData)++;
	return true;
}

BOOL CALLBACK HaveFoundWindow(HWND hwnd, LPARAM lParam)
{
	*((HWND*)lParam) = hwnd;	
	return false;
}
// поиск окна по процессу
HWND FindUDKWindow(DWORD pid)
{	
	THREADENTRY32 te = {0};
	te.dwSize = sizeof(THREADENTRY32);
	
	HANDLE hSnap = CreateToolhelp32Snapshot(TH32CS_SNAPTHREAD, 0);
	if (hSnap == INVALID_HANDLE_VALUE) return 0;

	if (!Thread32First(hSnap, &te))
	{
		CloseHandle(hSnap);
		return 0;
	}
	
	do
	{
		if (te.th32OwnerProcessID == pid)
			break;		
	}
	while (Thread32Next(hSnap, &te));
	CloseHandle(hSnap);

	HWND hwnd = 0;
	EnumThreadWindows(te.th32ThreadID, HaveFoundWindow, (LPARAM)&hwnd);	
	return hwnd;
}

extern "C" FORSAGEWND_API int MonitorsNum()
{	
	int nMonitors = 0;
	EnumDisplayMonitors(0, 0, HaveFoundMonitor, (LPARAM)&nMonitors);
	return nMonitors;
}

extern "C" FORSAGEWND_API void WindowPos()
{		
	if (idx != NONE) 
	{
		HWND hWndUDK = FindUDKWindow(GetCurrentProcessId());
		MoveWindow(hWndUDK, idx * CX, 0, CX, CY, 1);
		SetForegroundWindow(hWndUDK);
	}	
}