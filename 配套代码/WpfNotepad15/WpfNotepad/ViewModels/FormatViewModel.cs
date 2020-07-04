using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WpfNotepad.Helpers;
using WpfNotepad.Models;

namespace WpfNotepad.ViewModels
{
    public class FormatViewModel
    {
        public FormatViewModel(DocumentModel documentModel)
        {
            DocumentModel = documentModel;

            FontCommand = new RelayCommand(OnFontCommand);
        }



        private void OnFontCommand()
        {
            //FontDialog fontDialog = new FontDialog();
            //fontDialog.ShowDialog();
            LOGFONT logfont = new LOGFONT();
            logfont.lfFaceName = "Segoe UI";

            IntPtr pLogfont = Marshal.AllocHGlobal(Marshal.SizeOf(logfont));
            Marshal.StructureToPtr(logfont, pLogfont, false);

            CHOOSEFONT choosefont = new CHOOSEFONT();
            choosefont.lStructSize = Marshal.SizeOf(choosefont);
            choosefont.hwndOwner = Process.GetCurrentProcess().MainWindowHandle;
            choosefont.nSizeMin = 64;
            choosefont.nSizeMax = 64;
            choosefont.Flags = (int)CHOOSEFONTFLAGS.CF_SCREENFONTS
                             | (int)CHOOSEFONTFLAGS.CF_INITTOLOGFONTSTRUCT
                             | (int)CHOOSEFONTFLAGS.CF_FORCEFONTEXIST
                             | (int)CHOOSEFONTFLAGS.CF_NOVERTFONTS;
            choosefont.lpLogFont = pLogfont;
            choosefont.nFontType = 0x2000;

            IntPtr pChoosefont = Marshal.AllocHGlobal(Marshal.SizeOf(choosefont));
            Marshal.StructureToPtr(choosefont, pChoosefont, false);

            bool result = Win32Helper.ChooseFont(pChoosefont);
            if(result)
            {
                LOGFONT logfont1 = (LOGFONT)Marshal.PtrToStructure(pLogfont,typeof(LOGFONT));
                CHOOSEFONT choosefont1 = (CHOOSEFONT)Marshal.PtrToStructure(pChoosefont, typeof(CHOOSEFONT));
            }


            Marshal.FreeHGlobal(pChoosefont);
            Marshal.FreeHGlobal(pLogfont);
        }

        public DocumentModel DocumentModel { get; }

        public RelayCommand FontCommand { get; }
    }
}
