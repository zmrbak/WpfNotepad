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
using WpfNotepad.MessengerArgs;
using WpfNotepad.Models;
using WpfNotepad.Utility;
using WpfNotepad.Views;

namespace WpfNotepad.ViewModels
{
    public class EditViewModel
    {
        public EditViewModel(DocumentModel documentModel)
        {
            //DocumentModel = (Application.Current.Resources["Locator"] as ViewModelLocator).DocumentModel;
            DocumentModel = documentModel;

            BingCommand = new RelayCommand<TextBox>(OnBingCommand);

            FindUpCommand = new RelayCommand(OnFindUpCommand);
            FindDownCommand = new RelayCommand(OnFindDownCommand);
            GotoCommand = new RelayCommand(OnGotoCommand,CanGotoCommand);
            TimeDateCommand = new RelayCommand(OnTimeDateCommand);

            var findCommand = new CommandBinding(ApplicationCommands.Find, OnFindCommand, CanFindCommand);
            var replaceCommand = new CommandBinding(ApplicationCommands.Replace, OnReplaceCommand, CanReplaceCommand);
            CommandManager.RegisterClassCommandBinding(typeof(MainWindow), findCommand);
            CommandManager.RegisterClassCommandBinding(typeof(MainWindow), replaceCommand);
        }

        private bool CanGotoCommand()
        {
            return !DocumentModel.IsWrapped;
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
            DocumentModel.SaveSettings();

            if (DocumentModel.TextBox.SelectedText.Length > 0)
            {
                DocumentModel.FindText = DocumentModel.TextBox.SelectedText;
            }

            Messenger.Default.Send(new ReplaceWindowArg());
        }

        private void CanFindCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OnFindCommand(object sender, ExecutedRoutedEventArgs e)
        {
            DocumentModel.SaveSettings();

            if (DocumentModel.TextBox.SelectedText.Length > 0)
            {
                DocumentModel.FindText = DocumentModel.TextBox.SelectedText;
            }

            DocumentModel.IsFindWindowOpened = true;
            Messenger.Default.Send(new FindWindowArg());

        }

        private void OnFindDownCommand()
        {
            //向下查找
            if (DocumentModel.IsFindWindowOpened == false)
            {
                OnFindCommand(null, null);
                return;
            }

            Tools.OnFindTextCommand(DocumentModel, Direction.Down);
        }

        private void OnFindUpCommand()
        {
            //向上查找
            if (DocumentModel.IsFindWindowOpened == false)
            {
                OnFindCommand(null, null);
                return;
            }

            Tools.OnFindTextCommand(DocumentModel, Direction.Up);
        }



        private void OnGotoCommand()
        {
            DocumentModel.GotoLine = DocumentModel.TextBox.GetLineIndexFromCharacterIndex(DocumentModel.TextBox.CaretIndex) + 1;
            Messenger.Default.Send(new GotoWindowArg());
        }

        private void OnTimeDateCommand()
        {
            String dateTimeString = DateTime.Now.ToString("H:mm yyyy/M/d");
            int caretIndex = DocumentModel.TextBox.CaretIndex + dateTimeString.Length;

            DocumentModel.TextBox.Text = DocumentModel.TextBox.Text.Insert(DocumentModel.TextBox.CaretIndex, dateTimeString);
            DocumentModel.TextBox.CaretIndex = caretIndex;
        }

        public DocumentModel DocumentModel { get; }


        public RelayCommand<TextBox> BingCommand { get; }

        public RelayCommand FindDownCommand { get; }
        public RelayCommand FindUpCommand { get; }
        public RelayCommand GotoCommand { get; }
        public RelayCommand TimeDateCommand { get; }
    }
}
