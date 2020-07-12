using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfNotepad.Helpers;
using WpfNotepad.Models;
using WpfNotepad.Views;

namespace WpfNotepad.ViewModels
{
    public class EditViewModel
    {
        public EditViewModel(DocumentModel documentModel)
        {
            DocumentModel = documentModel;

            BingCommand = new RelayCommand<TextBox>(OnBingCommand);

            FindUpCommand = new RelayCommand(OnFindUpCommand);
            FindDownCommand = new RelayCommand(OnFindDownCommand);
            GotoCommand = new RelayCommand(OnGotoCommand);
            TimeDateCommand = new RelayCommand(OnTimeDateCommand);

            var findCommand = new CommandBinding(ApplicationCommands.Find, OnFindCommand, CanFindCommand);
            var replaceCommand = new CommandBinding(ApplicationCommands.Replace, OnReplaceCommand, CanReplaceCommand);
            CommandManager.RegisterClassCommandBinding(typeof(TextBox), findCommand);
            CommandManager.RegisterClassCommandBinding(typeof(TextBox), replaceCommand);
        }

        private void OnBingCommand(TextBox obj)
        {
            Process.Start("https://cn.bing.com/search?q=" + obj.SelectedText + "&form=NPCTXT");
        }


        private void CanReplaceCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OnReplaceCommand(object sender, ExecutedRoutedEventArgs e)
        {
            ReplaceWindow replaceWindow = new ReplaceWindow();
            replaceWindow.DataContext = DocumentModel;
            replaceWindow.Owner = Application.Current.MainWindow;
            replaceWindow.Show();
        }

        private void CanFindCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OnFindCommand(object sender, ExecutedRoutedEventArgs e)
        {
            FindWindow findWindow = new FindWindow();
            findWindow.DataContext = DocumentModel;
            findWindow.Owner = Application.Current.MainWindow;
            findWindow.Show();
        }

        private void OnFindDownCommand()
        {
            throw new NotImplementedException();
        }

        private void OnFindUpCommand()
        {
            throw new NotImplementedException();
        }



        private void OnGotoCommand()
        {
            throw new NotImplementedException();
        }

        private void OnTimeDateCommand()
        {
            throw new NotImplementedException();
        }

        public DocumentModel DocumentModel { get; }

        
        public RelayCommand<TextBox> BingCommand { get; }

        public RelayCommand FindDownCommand { get; }
        public RelayCommand FindUpCommand { get; }
        public RelayCommand GotoCommand { get; }
        public RelayCommand TimeDateCommand { get; }
    }
}
