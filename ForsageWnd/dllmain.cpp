// dllmain.cpp : Defines the entry point for the DLL application.
#include "ForsageWnd.h"

#pragma data_seg("UDK")
bool udkproc[3] = {0, 0, 0};		// массив состояний окон
#pragma data_seg()
#pragma comment(linker,"/SECTION:UDK,RWS") 

CHAR idx;							// индекс состояния
BOOL APIENTRY DllMain(HMODULE hModule, DWORD  ul_reason_for_call, LPVOID lpReserved)
{		
	switch (ul_reason_for_call)
	{
		case DLL_PROCESS_ATTACH:
			{	
				idx = NONE;			
				for (BYTE i = 0; i < 3; i++)		// поиск свободного расположения
				{
					if (!udkproc[i]) 
					{
						udkproc[i] = true;			// записать положение							
						idx = i;
						break;
					}
				}			
			}
			break;		
		case DLL_PROCESS_DETACH:	
			if (idx != NONE)						// освободить		
				udkproc[idx] = false;
			break;
	}
	return TRUE;  
}

