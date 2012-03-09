// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the FORSAGEWND_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// FORSAGEWND_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#include <windows.h>

#ifdef FORSAGEWND_EXPORTS
#define FORSAGEWND_API __declspec(dllexport)
#else
#define FORSAGEWND_API __declspec(dllimport)
#endif

#define	CX			800
#define	CY			600
#define	NONE		-1

extern "C" FORSAGEWND_API int MonitorsNum();
extern "C" FORSAGEWND_API void WindowPos();
