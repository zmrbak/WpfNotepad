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
    public class GotoWindowModel
    {
        public GotoWindowModel(DocumentModel documentModel)
        {
            DocumentModel = documentModel;

            GotoLineCommand = new RelayCommand<Window>(OnGotoLineCommand);
            CancelCommand = new RelayCommand<Window>(OnCancelCommand);
            ViewLoadedCommand = new RelayCommand<TextBox>(OnViewLoadedCommand);
        }

        private void OnGotoLineCommand(Window window)
        {
            //跳转到那一行
            if (DocumentModel.GotoLine > DocumentModel.TextBox.LineCount || DocumentModel.GotoLine < 1)
            {
                MessageBox.Show("行数超过了总行", "记事本-跳行", MessageBoxButton.OK);
                DocumentModel.GotoLine = DocumentModel.TextBox.LineCount;
                return;
            }

            int firstVisibleLineIndex = DocumentModel.TextBox.GetFirstVisibleLineIndex();
            int lastVisibleLineIndex = DocumentModel.TextBox.GetLastVisibleLineIndex();
            if (DocumentModel.GotoLine > lastVisibleLineIndex || DocumentModel.GotoLine < firstVisibleLineIndex)
            {
                DocumentModel.TextBox.ScrollToLine(DocumentModel.GotoLine - 1);
            }

            DocumentModel.TextBox.CaretIndex = DocumentModel.TextBox.GetCharacterIndexFromLineIndex(DocumentModel.GotoLine - 1);
            window.Close();
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

        public DocumentModel DocumentModel { get; }

        public RelayCommand<Window> GotoLineCommand { get; }
        public RelayCommand<Window> CancelCommand { get; }
        public RelayCommand<TextBox> ViewLoadedCommand { get; }
    }
}
