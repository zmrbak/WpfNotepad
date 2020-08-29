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

bool ZmrbakFileDialog::PageSetupDialog::ShowDialog(int hWnd)
{
	ZmrbakPageSetupDialog zmrbakPageSetupDialog;
	zmrbakPageSetupDialog.PaperSize = PaperSize;
	zmrbakPageSetupDialog.DefaultSource = DefaultSource;
	zmrbakPageSetupDialog.Landscape = Landscape;

	zmrbakPageSetupDialog.MarginBottom = MarginBottom;
	zmrbakPageSetupDialog.MarginLeft = MarginLeft;
	zmrbakPageSetupDialog.MarginRight = MarginRight;
	zmrbakPageSetupDialog.MarginTop = MarginTop;

	zmrbakPageSetupDialog.PaperHeader = PaperHeader;
	zmrbakPageSetupDialog.PaperFooter = PaperFooter;

	if (zmrbakPageSetupDialog.ShowDialog((HWND)hWnd))
	{
		PaperSize = zmrbakPageSetupDialog.PaperSize;
		DefaultSource = zmrbakPageSetupDialog.DefaultSource;
		Landscape = zmrbakPageSetupDialog.Landscape;

		MarginBottom = zmrbakPageSetupDialog.MarginBottom;
		MarginLeft = zmrbakPageSetupDialog.MarginLeft;
		MarginRight = zmrbakPageSetupDialog.MarginRight;
		MarginTop = zmrbakPageSetupDialog.MarginTop;

		PaperHeader = zmrbakPageSetupDialog.PaperHeader;
		PaperFooter = zmrbakPageSetupDialog.PaperFooter;

		return true;
	}

	return false;
}
