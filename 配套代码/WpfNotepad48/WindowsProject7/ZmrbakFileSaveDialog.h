#pragma once
#include <Windows.h>
class ZmrbakFileSaveDialog
{
public:
	bool ShowDialog(HWND hWND);
	~ZmrbakFileSaveDialog();
	DWORD selectedItemIndex = 0;
	LPWSTR fileName = NULL;
};

