#include "pch.h"
#include "ZmrbakFileSaveDialog.h"
#include <shlobj.h>

#define CONTROL_GROUP           2000

template <class T> void SafeRelease(T** ppT)
{
	if (*ppT)
	{
		(*ppT)->Release();
		*ppT = NULL;
	}
}

bool ZmrbakFileSaveDialog::ShowDialog(HWND hWND)
{
	const COMDLG_FILTERSPEC c_rgSaveTypes[] =
	{
	   { L"文本文档(*.txt)",    L"*.txt" },
	   { L"全部文件",           L"*.*" }
	};

	IFileDialog* pfd = NULL;
	IFileDialogCustomize* pfdc = NULL;
	IShellItem* pShellItem = NULL;
	bool result = false;

	if (FAILED(CoCreateInstance(CLSID_FileSaveDialog, NULL, CLSCTX_INPROC_SERVER, IID_PPV_ARGS(&pfd)))) goto End;

	if (FAILED(pfd->QueryInterface(IID_PPV_ARGS(&pfdc)))) goto End;
	if (FAILED(pfd->SetFileTypes(ARRAYSIZE(c_rgSaveTypes), c_rgSaveTypes))) goto End;
	if (FAILED(pfd->SetFileTypeIndex(1))) goto End;
	if (FAILED(pfd->SetDefaultExtension(L"txt"))) goto End;
	if (FAILED(pfd->SetFileName(fileName))) goto End;

	if (FAILED(pfdc->StartVisualGroup(CONTROL_GROUP, L"编码(&E):"))) goto End;
	if (FAILED(pfdc->AddComboBox(1))) goto End;
	if (FAILED(pfdc->AddControlItem(1, 1, L"ANSI"))) goto End;
	if (FAILED(pfdc->AddControlItem(1, 2, L"UTF-16 LE"))) goto End;
	if (FAILED(pfdc->AddControlItem(1, 3, L"UTF-16 BE"))) goto End;
	if (FAILED(pfdc->AddControlItem(1, 4, L"UTF-8"))) goto End;
	if (FAILED(pfdc->AddControlItem(1, 5, L"带有 BOM 的 UTF-8"))) goto End;
	if (FAILED(pfdc->SetSelectedControlItem(1, selectedItemIndex))) goto End;
	if (FAILED(pfdc->EndVisualGroup())) goto End;

	if (FAILED(pfd->Show(hWND))) goto End;

	if (FAILED(pfd->GetResult(&pShellItem))) goto End;
	if (FAILED(pShellItem->GetDisplayName(SIGDN_FILESYSPATH, &fileName))) goto End;
	if (FAILED(pfdc->GetSelectedControlItem(1, &selectedItemIndex))) goto End;
	result = true;
End:
	SafeRelease(&pfdc);
	SafeRelease(&pfd);
	SafeRelease(&pShellItem);
	return result;
}

ZmrbakFileSaveDialog::~ZmrbakFileSaveDialog()
{
	if (fileName != NULL)
	{
		CoTaskMemFree(fileName);
		fileName = NULL;
	}
}

bool ZmrbakFileOpenDialog::ShowDialog(HWND hWND)
{
	const COMDLG_FILTERSPEC c_rgSaveTypes[] =
	{
	   { L"文本文档(*.txt)",    L"*.txt" },
	   { L"全部文件",           L"*.*" }
	};

	IFileDialog* pfd = NULL;
	IFileDialogCustomize* pfdc = NULL;
	IShellItem* pShellItem = NULL;
	bool result = false;

	if (FAILED(CoCreateInstance(CLSID_FileOpenDialog, NULL, CLSCTX_INPROC_SERVER, IID_PPV_ARGS(&pfd)))) goto End;

	if (FAILED(pfd->QueryInterface(IID_PPV_ARGS(&pfdc)))) goto End;
	if (FAILED(pfd->SetFileTypes(ARRAYSIZE(c_rgSaveTypes), c_rgSaveTypes))) goto End;
	if (FAILED(pfd->SetFileTypeIndex(1))) goto End;
	if (FAILED(pfd->SetDefaultExtension(L"txt"))) goto End;
	if (FAILED(pfd->SetFileName(fileName))) goto End;

	if (FAILED(pfdc->StartVisualGroup(CONTROL_GROUP, L"编码(&E):"))) goto End;
	if (FAILED(pfdc->AddComboBox(1))) goto End;
	if (FAILED(pfdc->AddControlItem(1, 0, L"自动检测"))) goto End;
	if (FAILED(pfdc->AddControlItem(1, 1, L"ANSI"))) goto End;
	if (FAILED(pfdc->AddControlItem(1, 2, L"UTF-16 LE"))) goto End;
	if (FAILED(pfdc->AddControlItem(1, 3, L"UTF-16 BE"))) goto End;
	if (FAILED(pfdc->AddControlItem(1, 4, L"UTF-8"))) goto End;
	if (FAILED(pfdc->AddControlItem(1, 5, L"带有 BOM 的 UTF-8"))) goto End;
	if (FAILED(pfdc->SetSelectedControlItem(1, selectedItemIndex))) goto End;
	if (FAILED(pfdc->EndVisualGroup())) goto End;

	if (FAILED(pfd->Show(hWND))) goto End;

	if (FAILED(pfd->GetResult(&pShellItem))) goto End;
	if (FAILED(pShellItem->GetDisplayName(SIGDN_FILESYSPATH, &fileName))) goto End;
	if (FAILED(pfdc->GetSelectedControlItem(1, &selectedItemIndex))) goto End;
	result = true;
End:
	SafeRelease(&pfdc);
	SafeRelease(&pfd);
	SafeRelease(&pShellItem);
	return result;
}

ZmrbakFileOpenDialog::~ZmrbakFileOpenDialog()
{
	if (fileName != NULL)
	{
		CoTaskMemFree(fileName);
		fileName = NULL;
	}
}
