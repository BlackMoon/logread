// hklib.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "hklib.h"


// This is an example of an exported variable
HKLIB_API int nhklib=0;

// This is an example of an exported function.
HKLIB_API int fnhklib(void)
{
	return 42;
}

