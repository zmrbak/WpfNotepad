using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfNotepad.Helpers;
using WpfNotepad.Models;

namespace WpfNotepad.ViewModels
{
    public class EditViewModel
    {
        public EditViewModel(DocumentModel documentModel)
        {
            DocumentModel = documentModel;

            BingCommand = new RelayCommand(OnBingCommand);
            FindCommand = new RelayCommand(OnFindCommand);
            FindDownCommand = new RelayCommand(OnFindDownCommand);
            FindUpCommand = new RelayCommand(OnFindUpCommand);
            ReplaceCommand = new RelayCommand(OnReplaceCommand);
            GotoCommand = new RelayCommand(OnGotoCommand);
            TimeDateCommand = new RelayCommand(OnTimeDateCommand);
        }

     

        private void OnBingCommand()
        {
            throw new NotImplementedException();
        }

        private void OnFindCommand()
        {
            throw new NotImplementedException();
        }

        private void OnFindDownCommand()
        {
            throw new NotImplementedException();
        }

        private void OnFindUpCommand()
        {
            throw new NotImplementedException();
        }

        private void OnReplaceCommand()
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

        
        public RelayCommand BingCommand { get; }
        public RelayCommand FindCommand { get; }
        public RelayCommand FindDownCommand { get; }
        public RelayCommand FindUpCommand { get; }
        public RelayCommand ReplaceCommand { get; }
        public RelayCommand GotoCommand { get; }
        public RelayCommand TimeDateCommand { get; }
    }
}
