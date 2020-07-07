using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
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
            FontStyle fontStyle = FontStyle.Regular;
            if (DocumentModel.FontStyle == "Italic")
            {
                fontStyle |= FontStyle.Italic;
            }

            if (DocumentModel.FontWeight == "Bold")
            {
                fontStyle |= FontStyle.Bold;
            }

            Font font = new Font(DocumentModel.FontFamily, (float)DocumentModel.FontSize, fontStyle, GraphicsUnit.World, 0, false);
            LOGFONT logfont = new LOGFONT();
            font.ToLogFont(logfont);

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
            if (result)
            {
                LOGFONT logfont1 = (LOGFONT)Marshal.PtrToStructure(pLogfont, typeof(LOGFONT));
                Font font1 = Font.FromLogFont(logfont1);

                DocumentModel.FontFamily = font1.Name;
                DocumentModel.FontSize = font1.Size;
                DocumentModel.FontStyle = "Normal";
                DocumentModel.FontWeight = "Normal";

                if ((font1.Style & FontStyle.Italic) != 0)
                {
                    DocumentModel.FontStyle = "Italic";
                }

                if ((font1.Style & FontStyle.Bold) != 0)
                {
                    DocumentModel.FontWeight = "Bold";
                }               
            }

            Marshal.FreeHGlobal(pChoosefont);
            Marshal.FreeHGlobal(pLogfont);
        }

        public DocumentModel DocumentModel { get; }

        public RelayCommand FontCommand { get; }
    }
}
