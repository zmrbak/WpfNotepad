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
using WpfNotepad.Helpers;
using WpfNotepad.MessengerArgs;
using WpfNotepad.ViewModels;
using WpfNotepad.Views;

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
            Messenger.Default.Register<ReplaceWindowArg>(this, OnReplaceWindowArg);
            Messenger.Default.Register<FindWindowArg>(this, OnFindWindowArg);
            Messenger.Default.Register<AboutArg>(this, OnAboutArg);
            Messenger.Default.Register<GotoWindowArg>(this, OnGotoWindowArg);
            Messenger.Default.Register<SaveWindowArgs>(this, OnSaveWindowArgs);

            this.Unloaded += MainWindow_Unloaded;
        }

        private void OnSaveWindowArgs(SaveWindowArgs obj)
        {
            SaveWindow saveWindow = new SaveWindow();
            saveWindow.Owner = Application.Current.MainWindow;
            bool? dialogResult = saveWindow.ShowDialog();
            if (saveWindow.Tag == null)
            {
                return;
            }

            if (dialogResult == true)
            {
                //保存
                if ((Application.Current.Resources["Locator"] as ViewModelLocator).FileViewModel.OnSaveCommand() == false) return;
            }
                //不保存
           (Application.Current.Resources["Locator"] as ViewModelLocator).DocumentModel.Clear();

        }

        private void OnGotoWindowArg(GotoWindowArg obj)
        {
            GotoWindow gotoWindow = new GotoWindow();
            gotoWindow.Owner = Application.Current.MainWindow;
            gotoWindow.Show();
        }

        private void OnAboutArg(AboutArg obj)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            var title = assembly.GetCustomAttribute(typeof(AssemblyTitleAttribute)) as AssemblyTitleAttribute;
            var version = assembly.GetCustomAttribute(typeof(AssemblyFileVersionAttribute)) as AssemblyFileVersionAttribute;

            StreamResourceInfo streamResourceInfo = Application.GetResourceStream(new Uri("WpfNotepad.ico", UriKind.Relative));
            Icon icon = new Icon(streamResourceInfo.Stream);

            Win32Helper.ShellAbout(Process.GetCurrentProcess().MainWindowHandle, title.Title, version.Version, icon.Handle);
        }

        private void OnFindWindowArg(FindWindowArg obj)
        {
            FindWindow findWindow = new FindWindow();
            //findWindow.DataContext = (this.DataContext as MainViewModel).DocumentModel;
            findWindow.Owner = Application.Current.MainWindow;
            findWindow.Show();
        }

        private void MainWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister<ReplaceWindowArg>(this, OnReplaceWindowArg);
            Messenger.Default.Unregister<FindWindowArg>(this, OnFindWindowArg);
            Messenger.Default.Unregister<AboutArg>(this, OnAboutArg);
            Messenger.Default.Unregister<GotoWindowArg>(this, OnGotoWindowArg);
            Messenger.Default.Unregister<SaveWindowArgs>(this, OnSaveWindowArgs);
        }

        private void OnReplaceWindowArg(ReplaceWindowArg obj)
        {
            ReplaceWindow replaceWindow = new ReplaceWindow();
            //replaceWindow.DataContext = (this.DataContext as MainViewModel).DocumentModel;
            replaceWindow.Owner = Application.Current.MainWindow;
            replaceWindow.Show();
        }
    }
}
