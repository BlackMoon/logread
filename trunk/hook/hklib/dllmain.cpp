// dllmain.cpp : Defines the entry point for the DLL application.
#include "stdafx.h"
#include <tlhelp32.h>

bool bActive = 0;
HHOOK cbt_hhook = 0;


DWORD GetModuleThreadId(DWORD processID)
{
	DWORD dwThreadId = 0;
	HANDLE hThreadSnap = CreateToolhelp32Snapshot(TH32CS_SNAPTHREAD, processID);
	THREADENTRY32 te32;

	if (INVALID_HANDLE_VALUE != hThreadSnap)
	{		
		te32.dwSize = sizeof(THREADENTRY32);
		if (Thread32First(hThreadSnap, &te32))
		{
			do
			{
				if (processID == te32.th32OwnerProcessID) 
				{
					dwThreadId = te32.th32ThreadID;					
					break;
				}
			}
			while (Thread32Next(hThreadSnap, &te32));
		}		
		CloseHandle(hThreadSnap);				
	}	
	return dwThreadId;
}

LRESULT CALLBACK CBTProc(int nCode, WPARAM wParam, LPARAM lParam)
{	
	if (nCode == HCBT_CREATEWND)
	{		
		LPCREATESTRUCT pcs = ((LPCBT_CREATEWND)lParam)->lpcs;
		if (wcscmp(pcs->lpszName, L"hkproc") == 0)
		{
			pcs->x = 800;	
			pcs->cx = 800;
			pcs->y = 0;	
			pcs->cy = 600;
		}
	}

	if (nCode == HCBT_ACTIVATE)
	{
		HWND hwnd = (HWND)wParam;
        WCHAR buff[MAX_PATH];
        GetWindowText(hwnd, buff, MAX_PATH);
		if (wcscmp(buff, L"hkproc") == 0)
        { 
			if (!bActive)
			{
				ShowWindow(hwnd, SW_MAXIMIZE);
				bActive = true;
			}
		}
	}
	return CallNextHookEx(0, nCode, wParam, lParam);
}

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
					 )
{
	switch (ul_reason_for_call)
	{
		case DLL_PROCESS_ATTACH:
		{
			DWORD dwProcessId = GetCurrentProcessId(),
				  dwThreadId = GetModuleThreadId(dwProcessId);
									         
			cbt_hhook = SetWindowsHookEx(WH_CBT, CBTProc, hModule, dwThreadId);				         
			
		}
		break;
	case DLL_THREAD_ATTACH:
	case DLL_THREAD_DETACH:
	case DLL_PROCESS_DETACH:
		UnhookWindowsHookEx(cbt_hhook);		
		break;
	}
	return TRUE;
}
