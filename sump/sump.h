// sump.h
#pragma once

class CsumpApp : public CWinApp
{
public:
	CsumpApp();
// Overrides
	BOOL InitInstance();	
	int ExitInstance();
};

extern CsumpApp app;