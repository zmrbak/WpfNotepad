#include "pch.h"
#include "ZmrbakFileDialog.h"
#include "ZmrbakFileSaveDialog.h"

bool ZmrbakFileDialog::FileSaveDialog::ShowDialog(int hWnd)
{
	TCHAR* tempFileName = fileName;
	ZmrbakFileSaveDialog zmrbakFileSaveDialog;
	zmrbakFileSaveDialog.selectedItemIndex = selectedItemIndex;
	zmrbakFileSaveDialog.fileName = tempFileName;

	if (zmrbakFileSaveDialog.ShowDialog((HWND)hWnd))
	{
		selectedItemIndex = zmrbakFileSaveDialog.selectedItemIndex;
		fileName = zmrbakFileSaveDialog.fileName;
		return true;
	}

	return false;
}

bool ZmrbakFileDialog::FileOpenDialog::ShowDialog(int hWnd)
{
	TCHAR* tempFileName = fileName;
	ZmrbakFileOpenDialog zmrbakFileOpenDialog;
	zmrbakFileOpenDialog.selectedItemIndex = selectedItemIndex;
	zmrbakFileOpenDialog.fileName = tempFileName;

	if (zmrbakFileOpenDialog.ShowDialog((HWND)hWnd))
	{
		selectedItemIndex = zmrbakFileOpenDialog.selectedItemIndex;
		fileName = zmrbakFileOpenDialog.fileName;
		return true;
	}

	return false;
}
