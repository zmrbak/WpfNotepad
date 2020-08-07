using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfNotepad.Helpers;
using WpfNotepad.MessengerArgs;
using WpfNotepad.Models;
using ZmrbakFileDialog;

namespace WpfNotepad.ViewModels
{
    public class FileViewModel
    {
        public FileViewModel(DocumentModel documentModel)
        {
            //DocumentModel = (Application.Current.Resources["Locator"] as ViewModelLocator).DocumentModel;
            DocumentModel = documentModel;

            //NewCommand = new RelayCommand(OnNewCommand);
            NewWindowCommand = new RelayCommand(OnNewWindowCommand);
            OpenCommand = new RelayCommand(OnOpenCommand);
            SaveCommand = new RelayCommand(OnSaveCommand1);
            SaveAsCommand = new RelayCommand(OnSaveAsCommand1);
            PageSettingCommand = new RelayCommand(OnPageSettingCommand);
            PrintCommand = new RelayCommand(OnPrintCommand);
            ExitCommand = new RelayCommand(OnExitCommand);

            var newCommandBinding = new CommandBinding(ApplicationCommands.New, OnNewCommand, CanOnNewCommand);
            CommandManager.RegisterClassCommandBinding(typeof(MainWindow), newCommandBinding);
        }

        private void OnSaveAsCommand1()
        {
            OnSaveAsCommand();
        }

        private void OnSaveCommand1()
        {
            OnSaveCommand();
        }

        private void CanOnNewCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OnNewCommand(object sender, ExecutedRoutedEventArgs e)
        {
            if (DocumentModel.IsDirty == true)
            {
                //MessageBox.Show(
                //    "你像讲更改保存到 " + DocumentModel.FileName + " 吗?",
                //    "记事本",
                //    MessageBoxButton.YesNoCancel,
                //    MessageBoxImage.None,
                //    MessageBoxResult.OK,
                //    MessageBoxOptions.DefaultDesktopOnly
                //    );

                Messenger.Default.Send(new SaveWindowArgs());
            }
            else
            {
                DocumentModel.Clear();
            }
        }

       

        private void OnNewWindowCommand()
        {
            Process.Start(Process.GetCurrentProcess().MainModule.FileName);
        }

        private void OnOpenCommand()
        {
            throw new NotImplementedException();
        }

        public bool OnSaveCommand()
        {
            if (String.IsNullOrEmpty(DocumentModel.FilePath) == true)
            {
               return OnSaveAsCommand();
            }
            else
            {
              return  SaveFile();
            }
        }

        unsafe private bool OnSaveAsCommand()
        {
            FileSaveDialog fileSaveDialog = new FileSaveDialog();
            fileSaveDialog.fileName = (char*)Marshal.StringToCoTaskMemUni(DocumentModel.FileName);
            fileSaveDialog.selectedItemIndex = (uint)DocumentModel.FileEncoding;
            if (fileSaveDialog.ShowDialog(0) == true)
            {
                string fileName = Marshal.PtrToStringUni((IntPtr)fileSaveDialog.fileName);
                DocumentModel.FileExt = Path.GetExtension(fileName);
                DocumentModel.FilePath = Path.GetDirectoryName(fileName);
                DocumentModel.FileName = Path.GetFileNameWithoutExtension(fileName);

                int selectedItemIndex = (int)fileSaveDialog.selectedItemIndex;
                DocumentModel.FileEncoding = (FileEncodes)selectedItemIndex;

               return SaveFile();
            }

            return false;
        }

        private bool SaveFile()
        {
            string fileFullPath = Path.Combine(DocumentModel.FilePath, DocumentModel.FileName + DocumentModel.FileExt);
            Encoding encoding = null;
            switch (DocumentModel.FileEncoding)
            {
                case FileEncodes.NONE:
                    encoding = Encoding.UTF8;
                    break;
                case FileEncodes.ANSI:
                    encoding = Encoding.Default;
                    break;
                case FileEncodes.UTF16LE:
                    encoding = Encoding.Unicode;
                    break;
                case FileEncodes.UTF16BE:
                    encoding = Encoding.BigEndianUnicode;
                    break;
                case FileEncodes.UTF8:
                    encoding = new UTF8Encoding(false);
                    break;
                case FileEncodes.BOMUTF8:
                    encoding = new UTF8Encoding(true);
                    break;
            }

            File.WriteAllText(fileFullPath, DocumentModel.Text, encoding);

            DocumentModel.IsDirty = false;

            return true;
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

        //public RelayCommand NewCommand { get; }
        public RelayCommand NewWindowCommand { get; }
        public RelayCommand OpenCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand SaveAsCommand { get; }
        public RelayCommand PageSettingCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand ExitCommand { get; }

    }
}
