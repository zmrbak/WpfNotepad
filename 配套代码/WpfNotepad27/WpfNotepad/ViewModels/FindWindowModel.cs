using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfNotepad.Helpers;
using WpfNotepad.Models;

namespace WpfNotepad.ViewModels
{
    public class FindWindowModel
    {
        public FindWindowModel(/*DocumentModel documentModel*/)
        {
            DocumentModel = (Application.Current.Resources["Locator"] as ViewModelLocator).DocumentModel;

            FindTextCommand = new RelayCommand(OnFindTextCommand);
        }

        private void OnFindTextCommand()
        {
            throw new NotImplementedException();
        }

        public DocumentModel DocumentModel { get; }

        public RelayCommand FindTextCommand { get; }
    }
}
