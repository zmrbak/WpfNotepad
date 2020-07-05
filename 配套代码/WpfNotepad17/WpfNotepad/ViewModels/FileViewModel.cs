using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfNotepad.Helpers;
using WpfNotepad.Models;

namespace WpfNotepad.ViewModels
{
    public class FileViewModel
    {
        public FileViewModel(DocumentModel documentModel)
        {
            DocumentModel = documentModel;
            NewCommand = new RelayCommand(OnNewCommand);
            NewWindowCommand = new RelayCommand(OnNewWindowCommand);
            OpenCommand = new RelayCommand(OnOpenCommand);
            SaveCommand = new RelayCommand(OnSaveCommand);
            SaveAsCommand = new RelayCommand(OnSaveAsCommand);
            PageSettingCommand = new RelayCommand(OnPageSettingCommand);
            PrintCommand = new RelayCommand(OnPrintCommand);
            ExitCommand = new RelayCommand(OnExitCommand);
        }

        private void OnNewCommand()
        {
            throw new NotImplementedException();
        }

        private void OnNewWindowCommand()
        {
            throw new NotImplementedException();
        }

        private void OnOpenCommand()
        {
            throw new NotImplementedException();
        }

        private void OnSaveCommand()
        {
            throw new NotImplementedException();
        }

        private void OnSaveAsCommand()
        {
            throw new NotImplementedException();
        }

        private void OnPageSettingCommand()
        {
            throw new NotImplementedException();
        }

        private void OnPrintCommand()
        {
            throw new NotImplementedException();
        }

        private void OnExitCommand()
        {
            throw new NotImplementedException();
        }

        public DocumentModel DocumentModel { get; }

        public RelayCommand NewCommand { get; }
        public RelayCommand NewWindowCommand { get; }
        public RelayCommand OpenCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand SaveAsCommand { get; }
        public RelayCommand PageSettingCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand ExitCommand { get; }

    }
}
