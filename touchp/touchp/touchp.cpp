// touchp.cpp
#include <tchar.h>
#include <windows.h>

int _tmain(int argc, _TCHAR* argv[])
{
	//keybd_event(VK_APPS | VK_F3, 0, KEYEVENTF_EXTENDEDKEY, 0);
	keybd_event(VK_CTRL | VK_ESCAPE , 0, KEYEVENTF_EXTENDEDKEY, 0);
	return 0;
}