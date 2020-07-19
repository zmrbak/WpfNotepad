using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfNotepad.Helpers;
using WpfNotepad.Models;

namespace WpfNotepad.ViewModels
{
    public class FindWindowModel
    {
        public FindWindowModel(/*DocumentModel documentModel*/)
        {
            DocumentModel = (Application.Current.Resources["Locator"] as ViewModelLocator).DocumentModel;

            FindTextCommand = new RelayCommand<Button>(OnFindTextCommand);
        }

        private void OnFindTextCommand(Button button)
        {
            OnFindTextCommand();
            button.Focus();
        }

        private void OnFindTextCommand()
        {
            //查找开始点
            int indexStart = 0;

            //找点的地方
            int indexFind = 0;

            //向上查找
            if (DocumentModel.IsFindUp)
            {
                indexStart = DocumentModel.TextBox.SelectionStart;
                indexFind = DocumentModel.TextBox.Text.LastIndexOf(
                    DocumentModel.FindText,
                    indexStart,
                    DocumentModel.IsFindCaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase
                    );

                if (indexFind == -1)
                {
                    if(DocumentModel.IsFindCirculated)
                    {
                        DocumentModel.TextBox.ScrollToEnd();
                        indexFind = DocumentModel.TextBox.Text.LastIndexOf(
                                           DocumentModel.FindText,
                                           DocumentModel.TextBox.Text.Length,
                                           DocumentModel.IsFindCaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase
                                           );
                    }
                }

            }
            //向下查找
            else
            {
                indexStart = DocumentModel.TextBox.SelectionStart+ DocumentModel.FindText.Length;
                indexFind = DocumentModel.TextBox.Text.IndexOf(
                    DocumentModel.FindText,
                    indexStart,
                    DocumentModel.IsFindCaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase
                    );

                if (indexFind == -1)
                {
                    if (DocumentModel.IsFindCirculated)
                    {
                        DocumentModel.TextBox.ScrollToHome();
                        indexFind = DocumentModel.TextBox.Text.IndexOf(
                                           DocumentModel.FindText,
                                           0,
                                           DocumentModel.IsFindCaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase
                                           );
                    }
                }
            }

            if(indexFind==-1)
            {
                MessageBox.Show("找不到“"+ DocumentModel.FindText + "”","记事本",MessageBoxButton.OK,MessageBoxImage.Information);
                return;
            }

            DocumentModel.TextBox.Select(indexFind, DocumentModel.FindText.Length);
            DocumentModel.TextBox.Focus();
        }

        public DocumentModel DocumentModel { get; }

        public RelayCommand<Button> FindTextCommand { get; }
    }
}
