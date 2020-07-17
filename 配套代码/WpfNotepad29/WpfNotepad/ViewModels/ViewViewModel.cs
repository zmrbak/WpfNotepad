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
    public class ViewViewModel
    {
        public ViewViewModel(/*DocumentModel documentModel*/)
        {
            DocumentModel = (Application.Current.Resources["Locator"] as ViewModelLocator).DocumentModel;


            ZoomInCommand = new RelayCommand(OnZoomInCommand);
            ZoomOutCommand = new RelayCommand(OnZoomOutCommand);
            ZoomDefaultCommand = new RelayCommand(OnZoomDefaultCommand);
        }

        private void OnZoomInCommand()
        {
            //限制在500%以内
            double fontSize = DocumentModel.FontSize + DocumentModel.FontSizeDefault * 10 / 100;
            if (fontSize > DocumentModel.FontSizeDefault * 5)
            {
                fontSize = DocumentModel.FontSizeDefault * 5;
            }

            DocumentModel.FontSize = fontSize;
        }

        private void OnZoomOutCommand()
        {
            //不小于10%
            double fontSize = DocumentModel.FontSize - DocumentModel.FontSizeDefault * 10 / 100;
            if (fontSize < DocumentModel.FontSizeDefault * 10 / 100)
            {
                fontSize = DocumentModel.FontSizeDefault * 10 / 100;
            }
            DocumentModel.FontSize = fontSize;
        }

        private void OnZoomDefaultCommand()
        {
            DocumentModel.FontSize = DocumentModel.FontSizeDefault;
        }

        public DocumentModel DocumentModel { get; }

        public RelayCommand ZoomInCommand { get; }
        public RelayCommand ZoomOutCommand { get; }
        public RelayCommand ZoomDefaultCommand { get; }
    }
}
