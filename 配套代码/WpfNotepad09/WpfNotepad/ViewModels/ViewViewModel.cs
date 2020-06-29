using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfNotepad.Helpers;
using WpfNotepad.Models;

namespace WpfNotepad.ViewModels
{
    public class ViewViewModel
    {
        public ViewViewModel(DocumentModel documentModel)
        {
            DocumentModel = documentModel;

            ZoomInCommand = new RelayCommand(OnZoomInCommand);
            ZoomOutCommand = new RelayCommand(OnZoomOutCommand);
            ZoomDefaultCommand = new RelayCommand(OnZoomDefaultCommand);
        }

        private void OnZoomInCommand()
        {
            throw new NotImplementedException();
        }

        private void OnZoomOutCommand()
        {
            throw new NotImplementedException();
        }

        private void OnZoomDefaultCommand()
        {
            throw new NotImplementedException();
        }

        public DocumentModel DocumentModel { get; }

        public RelayCommand ZoomInCommand { get; }
        public RelayCommand ZoomOutCommand { get; }
        public RelayCommand ZoomDefaultCommand { get; }
    }
}
