#include "pch.h"
#include "MyPageSetupDlg.h"

#include <commdlg.h>
#include <CommCtrl.h>
#include <shellapi.h>
#include <dlgs.h>
#include "resource.h"

#pragma comment(linker,"\"/manifestdependency:type='win32' name='Microsoft.Windows.Common-Controls'  \
version = '6.0.0.0' processorArchitecture = '*' publicKeyToken = '6595b64144ccf1df' language = '*'\"")


UINT_PTR CALLBACK  Lppagesetuphook(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam);

bool ShowPageSetupDlg()
{
    PAGESETUPDLG page;
    page.lStructSize = sizeof(page);
    page.hwndOwner = NULL;
    page.hDevMode = NULL;
    page.hDevNames = NULL;
    page.Flags = PSD_ENABLEPAGESETUPHOOK
        | PSD_ENABLEPAGESETUPTEMPLATE
        | PSD_INHUNDREDTHSOFMILLIMETERS
        | PSD_MARGINS
        ;
    page.ptPaperSize.x = 1000;
    page.ptPaperSize.x = 2000;
    /*  page.rtMinMargin.top =1000 ;
      page.rtMinMargin.left = 1100;
      page.rtMinMargin.bottom = 1200;
      page.rtMinMargin.right = 1300;*/
    page.rtMargin.top = 1000;
    page.rtMargin.left = 1100;
    page.rtMargin.right = 1200;
    page.rtMargin.bottom = 1300;
    page.hInstance = GetModuleHandle(L"DialogDll.dll");
    page.lCustData = 1400;
    page.lpfnPageSetupHook = Lppagesetuphook;
    page.lpPageSetupTemplateName = MAKEINTRESOURCE(IDD_PAGE_SETUP);



   return PageSetupDlg(&page);

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

    /*case WM_INITDIALOG:
        return (INT_PTR)TRUE;*/

        /* case WM_COMMAND:
             if (LOWORD(wParam) == IDOK || LOWORD(wParam) == IDCANCEL)
             {
                 EndDialog(hDlg, LOWORD(wParam));
                 return (INT_PTR)TRUE;
             }
             break;*/
    }
    return (INT_PTR)FALSE;
}
