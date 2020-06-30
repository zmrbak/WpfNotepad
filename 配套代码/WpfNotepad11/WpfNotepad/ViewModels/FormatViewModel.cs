using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfNotepad.Helpers;
using WpfNotepad.Models;

namespace WpfNotepad.ViewModels
{
    public class FormatViewModel
    {
        public FormatViewModel(DocumentModel documentModel)
        {
            DocumentModel = documentModel;
            AutoWrapCommand = new RelayCommand(OnAutoWrapCommand);
            FontCommand = new RelayCommand(OnFontCommand);
        }

        private void OnAutoWrapCommand()
        {
            throw new NotImplementedException();
        }

        private void OnFontCommand()
        {
            throw new NotImplementedException();
        }

        public DocumentModel DocumentModel { get; }

        public RelayCommand AutoWrapCommand { get; }
        public RelayCommand FontCommand { get; }
    }
}
