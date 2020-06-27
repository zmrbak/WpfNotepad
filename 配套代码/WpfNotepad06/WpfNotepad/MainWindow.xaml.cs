using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
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
using System.Windows.Resources;
using System.Windows.Shapes;

namespace WpfNotepad
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://go.microsoft.com/fwlink/?LinkId=834783");
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Process.Start("feedback-hub://?appid=1231313");
        }

        [DllImport("shell32.dll")]
        static extern int ShellAbout(IntPtr hWnd, string szApp, string szOtherStuff, IntPtr hIcon);

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            var title = assembly.GetCustomAttribute(typeof(AssemblyTitleAttribute)) as AssemblyTitleAttribute;
            var version = assembly.GetCustomAttribute(typeof(AssemblyFileVersionAttribute)) as AssemblyFileVersionAttribute;

            StreamResourceInfo streamResourceInfo = Application.GetResourceStream(new Uri("WpfNotepad.ico", UriKind.Relative));
            Icon icon = new Icon(streamResourceInfo.Stream); 

            ShellAbout(Process.GetCurrentProcess().MainWindowHandle, title.Title, version.Version, icon.Handle);
        }
    }
}
