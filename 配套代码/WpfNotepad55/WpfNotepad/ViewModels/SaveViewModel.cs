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
    public class SaveViewModel
    {
        public SaveViewModel(DocumentModel documentModel)
        {
            DocumentModel = documentModel;

            DragWinCommand = new RelayCommand<Window>(OnDragWinCommand);
            CancelCommand = new RelayCommand<Window>(OnCancelCommand);
            SaveCommand = new RelayCommand<Window>(OnSaveCommand);
            NotSaveCommand = new RelayCommand<Window>(OnNotSaveCommand);
        }

        private void OnNotSaveCommand(Window window)
        {
            window.DialogResult = false;
            window.Tag = false;
            window.Close();
        }

        private void OnSaveCommand(Window window)
        {
            window.DialogResult = true;
            window.Tag = true;
            window.Close();
        }

        private void OnCancelCommand(Window window)
        {
            window.DialogResult = null;
            window.Tag = null;
            window.Close();
        }

        private void OnDragWinCommand(Window window)
        {
            window.DragMove();
        }

        public DocumentModel DocumentModel { get; }

        public RelayCommand<Window> DragWinCommand { get; }
        public RelayCommand<Window> CancelCommand { get; }
        public RelayCommand<Window> SaveCommand { get; }
        public RelayCommand<Window> NotSaveCommand { get; }

    }
}
