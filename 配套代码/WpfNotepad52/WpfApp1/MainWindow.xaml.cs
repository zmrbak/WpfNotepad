using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MyCommand = new RelayCommand();

            this.DataContext = this;
        }

        public RelayCommand MyCommand { get; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void textBox_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //SaveFileDialog saveFileDialog = new SaveFileDialog();
            //saveFileDialog.Filter = "文本文档(*.txt)|*.txt|所有文件|*.*";
            //saveFileDialog.FileName = "*.txt";
            //saveFileDialog.DefaultExt = "txt";
            //saveFileDialog.Title = "另存为";
            //saveFileDialog.AddExtension = true;
            //saveFileDialog.CreatePrompt = true;
            //saveFileDialog.CustomPlaces.Add(new FileDialogCustomPlace(@"C:\app"));
            //saveFileDialog.CustomPlaces.Add(new FileDialogCustomPlace(@"C:\app\Zmrbak"));
            //saveFileDialog.CustomPlaces.Add(new FileDialogCustomPlace(@"C:\app\Zmrbak\cfgtoollogs\netca"));
            //saveFileDialog.DereferenceLinks = true;
            //saveFileDialog.InitialDirectory = "D:\\";
            //saveFileDialog.OverwritePrompt = true;
            //saveFileDialog.RestoreDirectory = true;
            //saveFileDialog.ValidateNames = true;
            //saveFileDialog.ShowDialog();
            //var aaa= saveFileDialog.CustomPlaces;

            OPENFILENAME_I oPENFILENAME_I = new OPENFILENAME_I();
            GetSaveFileName(oPENFILENAME_I);
        }

        [SecurityCritical]
        [SuppressUnmanagedCodeSecurity]
        [DllImport(ExternDll.Comdlg32, SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern bool GetSaveFileName([In, Out] OPENFILENAME_I ofn);
    }

    public delegate IntPtr WndProc(IntPtr hWnd, Int32 msg, IntPtr wParam, IntPtr lParam);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public class OPENFILENAME_I
    {
        public int lStructSize = SizeOf(); //ndirect.DllLib.sizeOf(this);
        public IntPtr hwndOwner;
        public IntPtr hInstance;
        public string lpstrFilter;   // use embedded nulls to separate filters
        public IntPtr lpstrCustomFilter;
        public int nMaxCustFilter;
        public int nFilterIndex;
        public IntPtr lpstrFile;
        public int nMaxFile = 260;
        public IntPtr lpstrFileTitle;
        public int nMaxFileTitle = 260;
        public string lpstrInitialDir;
        public string lpstrTitle;
        public int Flags;
        public short nFileOffset;
        public short nFileExtension;
        public string lpstrDefExt;
        public IntPtr lCustData;
        public WndProc lpfnHook;
        public string lpTemplateName;
        public IntPtr pvReserved;
        public int dwReserved;
        public int FlagsEx;

        /// <SecurityNote>
        ///  Critical : Calls critical Marshal.SizeOf
        ///  Safe     : Calls method with trusted input (well known safe type)
        /// </SecurityNote>
        [SecuritySafeCritical]
        private static int SizeOf()
        {
            return Marshal.SizeOf(typeof(OPENFILENAME_I));
        }
    }

    internal static class ExternDll
    {
        public const string Activeds = "activeds.dll";
        public const string Advapi32 = "advapi32.dll";
        public const string Comctl32 = "comctl32.dll";
        public const string Comdlg32 = "comdlg32.dll";
        public const string DwmAPI = "dwmapi.dll";
        public const string Gdi32 = "gdi32.dll";
        public const string Gdiplus = "gdiplus.dll";
        public const string Hhctrl = "hhctrl.ocx";
        public const string Imm32 = "imm32.dll";
        public const string Kernel32 = "kernel32.dll";
        public const string Loadperf = "Loadperf.dll";
        public const string Mqrt = "mqrt.dll";
        public const string Mscoree = "mscoree.dll";
        public const string MsDrm = "msdrm.dll";
        public const string Mshwgst = "mshwgst.dll";
        public const string Msi = "msi.dll";
        public const string NaturalLanguage6 = "naturallanguage6.dll";
        public const string Ntdll = "ntdll.dll";
        public const string Ole32 = "ole32.dll";
        public const string Oleacc = "oleacc.dll";
        public const string Oleaut32 = "oleaut32.dll";
        public const string Olepro32 = "olepro32.dll";
        public const string Penimc = "penimc2_v0400.dll";
        public const string PresentationHostDll = "PresentationHost_v0400.dll";
        public const string PresentationNativeDll = "PresentationNative_v0400.dll";
        public const string Psapi = "psapi.dll";
        public const string Shcore = "shcore.dll";
        public const string Shell32 = "shell32.dll";
        public const string Shfolder = "shfolder.dll";
        public const string Urlmon = "urlmon.dll";
        public const string User32 = "user32.dll";
        public const string Uxtheme = "uxtheme.dll";
        public const string Version = "version.dll";
        public const string Vsassert = "vsassert.dll";
        public const string Wininet = "wininet.dll";
        public const string Winmm = "winmm.dll";
        public const string Winspool = "winspool.drv";
        public const string Wldp = "wldp.dll";
        public const string WtsApi32 = "wtsapi32.dll";
    }
}
