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
    public class FindWindowModel
    {
        public FindWindowModel(/*DocumentModel documentModel*/)
        {
            DocumentModel = (Application.Current.Resources["Locator"] as ViewModelLocator).DocumentModel;

            FindTextCommand = new RelayCommand<TextBox>(OnFindTextCommand);
            CancelCommand = new RelayCommand<Window>(OnCancelCommand);
            ViewLoadedCommand = new RelayCommand<TextBox>(OnViewLoadedCommand);
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
    }
}
