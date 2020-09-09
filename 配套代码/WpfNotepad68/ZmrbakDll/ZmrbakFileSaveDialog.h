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

class ZmrbakFileOpenDialog
{
public:
	bool ShowDialog(HWND hWND);
	~ZmrbakFileOpenDialog();
	DWORD selectedItemIndex = 0;
	LPWSTR fileName = NULL;
};

class ZmrbakPageSetupDialog
{
public:
	bool ShowDialog(HWND hWND);
	DWORD MarginTop;
	DWORD MarginLeft;
	DWORD MarginRight;
	DWORD MarginBottom;
	DWORD Landscape;
	DWORD DefaultSource;
	DWORD PaperSize;
	LPWSTR PaperHeader;
	LPWSTR PaperFooter;
};