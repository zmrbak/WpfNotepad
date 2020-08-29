#include "pch.h"
#include "ZmrbakFileSaveDialog.h"
#include <shlobj.h>
#include <Windows.h>
#include <commdlg.h>
#include <shellapi.h>
#include "resource.h"

#define CONTROL_GROUP           2000
TCHAR Header[MAX_PATH] = { 0 };
TCHAR Footer[MAX_PATH] = { 0 };
bool HeaderFootInit = false;

UINT_PTR CALLBACK  Lppagesetuphook(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam);
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

bool ZmrbakPageSetupDialog::ShowDialog(HWND hWND)
{
	//获取默认的DEVMODE
	PAGESETUPDLG page;
	ZeroMemory(&page, sizeof(PAGESETUPDLG));
	page.lStructSize = sizeof(PAGESETUPDLG);
	page.Flags = PSD_RETURNDEFAULT;
	PageSetupDlg(&page);

	//设置DEVMODE属性
	LPDEVMODE lpvoid = (LPDEVMODE)GlobalLock(page.hDevMode);
	if (lpvoid == NULL) return false;
	lpvoid->dmOrientation = (short)Landscape;
	lpvoid->dmPaperSize = (short)PaperSize;
	lpvoid->dmDefaultSource = (short)DefaultSource;
	GlobalUnlock(page.hDevMode);

	page.hwndOwner = hWND;
	page.Flags = PSD_ENABLEPAGESETUPHOOK
		| PSD_ENABLEPAGESETUPTEMPLATE
		| PSD_INHUNDREDTHSOFMILLIMETERS
		| PSD_MARGINS
		;

	page.rtMargin.top = MarginTop;
	page.rtMargin.left = MarginLeft;
	page.rtMargin.right = MarginRight;
	page.rtMargin.bottom = MarginBottom;
	page.hInstance = GetModuleHandle(L"ZmrbakDll.dll");
	page.lpfnPageSetupHook = Lppagesetuphook;
	page.lpPageSetupTemplateName = MAKEINTRESOURCE(IDD_PAGE_SETUP);

	//复制页眉页脚
	wcscpy_s(Header, MAX_PATH, PaperHeader);
	wcscpy_s(Footer, MAX_PATH, PaperFooter);

	HeaderFootInit = false;

	//打开对话框
	bool result = PageSetupDlg(&page);
	if (result)
	{
		//获取margin
		MarginTop = page.rtMargin.top;
		MarginLeft = page.rtMargin.left;
		MarginRight = page.rtMargin.right;
		MarginBottom = page.rtMargin.bottom;

		//获取纸张方向
		LPDEVMODE lpvoid1 = (LPDEVMODE)GlobalLock(page.hDevMode);
		if (lpvoid1 == NULL) return false;
		Landscape = lpvoid1->dmOrientation;
		PaperSize = lpvoid1->dmPaperSize;
		DefaultSource = lpvoid1->dmDefaultSource;
		GlobalUnlock(page.hDevMode);

		//页眉页脚
		PaperHeader = Header;
		PaperFooter = Footer;
	}
	return result;
}

UINT_PTR CALLBACK  Lppagesetuphook(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
	UNREFERENCED_PARAMETER(lParam);
	switch (message)
	{
	case WM_NOTIFY:
	{
		NMHDR* pHDR = (NMHDR*)lParam;
		if (pHDR->code == NM_CLICK && pHDR->idFrom == 34)
		{
			ShellExecute(NULL, NULL, L"https://go.microsoft.com/fwlink/p/?linkid=838060", NULL, NULL, SW_NORMAL);
		}
	}

	case WM_INITDIALOG:
	{
		if (HeaderFootInit == false)
		{
			SetDlgItemText(hDlg, 30, Header);
			SetDlgItemText(hDlg, 31, Footer);
			HeaderFootInit = true;
		}
		return (INT_PTR)TRUE;
	}

	 case WM_COMMAND:
		 if (LOWORD(wParam) == IDOK || LOWORD(wParam) == IDCANCEL)
		 {
			 if (LOWORD(wParam) == IDOK)
			 {
				 GetDlgItemText(hDlg, 30, Header, MAX_PATH);
				 GetDlgItemText(hDlg, 31, Footer, MAX_PATH);
			 }
			 EndDialog(hDlg, LOWORD(wParam));
			 return (INT_PTR)TRUE;
		 }
		 break;
	}
	return (INT_PTR)FALSE;
}
