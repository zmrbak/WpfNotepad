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
using WpfNotepad.MessengerArgs;

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
            Messenger.Default.Send(new AboutArg());

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
