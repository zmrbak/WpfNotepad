using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Resources;
using WpfNotepad.Helpers;

namespace WpfNotepad.ViewModels
{
    public class HelpViewModel
    {
        public RelayCommand HelpCommand { get; }
        public RelayCommand FeedBackCommand { get; }
        public RelayCommand AboutCommand { get; }

        public HelpViewModel()
        {
            HelpCommand = new RelayCommand(OnHelpCommand);
            FeedBackCommand = new RelayCommand(OnFeedBackCommand);
            AboutCommand = new RelayCommand(OnAboutCommand);
        }

        private void OnAboutCommand()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            var title = assembly.GetCustomAttribute(typeof(AssemblyTitleAttribute)) as AssemblyTitleAttribute;
            var version = assembly.GetCustomAttribute(typeof(AssemblyFileVersionAttribute)) as AssemblyFileVersionAttribute;

            StreamResourceInfo streamResourceInfo = Application.GetResourceStream(new Uri("WpfNotepad.ico", UriKind.Relative));
            Icon icon = new Icon(streamResourceInfo.Stream);

            Win32Helper.ShellAbout(Process.GetCurrentProcess().MainWindowHandle, title.Title, version.Version, icon.Handle);
        }

        private void OnFeedBackCommand()
        {
            Process.Start("feedback-hub://?appid=1231313");
        }

        private void OnHelpCommand()
        {
            Process.Start("https://go.microsoft.com/fwlink/?LinkId=834783");
        }
    }
}
