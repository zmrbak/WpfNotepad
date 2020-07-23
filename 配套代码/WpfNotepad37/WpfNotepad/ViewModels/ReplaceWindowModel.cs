using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfNotepad.Helpers;
using WpfNotepad.Models;
using WpfNotepad.Utility;

namespace WpfNotepad.ViewModels
{
    public class ReplaceWindowModel
    {
        public ReplaceWindowModel()
        {
            DocumentModel = (Application.Current.Resources["Locator"] as ViewModelLocator).DocumentModel;

            FindTextCommand = new RelayCommand<TextBox>(OnFindTextCommand);
            CancelCommand = new RelayCommand<Window>(OnCancelCommand);
            ViewLoadedCommand = new RelayCommand<TextBox>(OnViewLoadedCommand);
            ReplaceCommand = new RelayCommand(OnReplaceCommand);
            ReplaceAllCommand = new RelayCommand(OnReplaceAllCommand);
        }

        private void OnReplaceAllCommand()
        {
            DocumentModel.TextBox.Text = Strings.Replace(
                DocumentModel.TextBox.Text,
                DocumentModel.FindText,
                DocumentModel.ReplacedText,
                1,
                -1,
                DocumentModel.IsFindCaseSensitive ? CompareMethod.Binary:CompareMethod.Text
                );
            DocumentModel.TextBox.SelectionLength = 0;
            DocumentModel.TextBox.SelectedText = "";

            DocumentModel.TextBox.ScrollToHome();
        }

        private void OnReplaceCommand()
        {
            if(DocumentModel.FindText.Equals(
                DocumentModel.TextBox.SelectedText,
                DocumentModel.IsFindCaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase
                ))
            {
                DocumentModel.TextBox.SelectedText = DocumentModel.ReplacedText;
            }

            Tools.OnFindTextCommand(DocumentModel,Direction.Down);
        }

        private void OnViewLoadedCommand(TextBox textBox)
        {
            textBox.SelectAll();
            textBox.Focus();
        }

        private void OnCancelCommand(Window window)
        {
            DocumentModel.LoadSettings();
            window.Close();
        }

        private void OnFindTextCommand(TextBox textBox)
        {
            Tools.OnFindTextCommand(DocumentModel);

            textBox.Focus();

            DocumentModel.SaveSettings();
        }


        public DocumentModel DocumentModel { get; }

        public RelayCommand<TextBox> FindTextCommand { get; }
        public RelayCommand<Window> CancelCommand { get; }
        public RelayCommand<TextBox> ViewLoadedCommand { get; }
        public RelayCommand ReplaceCommand { get; }
        public RelayCommand ReplaceAllCommand { get; }

    }
}
