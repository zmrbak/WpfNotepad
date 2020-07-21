using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfNotepad.Helpers;
using WpfNotepad.Models;

namespace WpfNotepad.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<DocumentModel>();

            SimpleIoc.Default.Register<FileViewModel>();
            SimpleIoc.Default.Register<EditViewModel>();
            SimpleIoc.Default.Register<FormatViewModel>();
            SimpleIoc.Default.Register<ViewViewModel>();
            SimpleIoc.Default.Register<HelpViewModel>();

            SimpleIoc.Default.Register<FindWindowModel>();

            SetDocumentModelTextBoxCommand = new RelayCommand<TextBox>(OnSetDocumentModelTextBoxCommand);
        }

        private void OnSetDocumentModelTextBoxCommand(TextBox textBox)
        {
            DocumentModel.TextBox = textBox;
        }

        public DocumentModel DocumentModel => ServiceLocator.Current.GetInstance<DocumentModel>();

        public FileViewModel FileViewModel => ServiceLocator.Current.GetInstance<FileViewModel>();
        public EditViewModel EditViewModel => ServiceLocator.Current.GetInstance<EditViewModel>();
        public FormatViewModel FormatViewModel => ServiceLocator.Current.GetInstance<FormatViewModel>();
        public ViewViewModel ViewViewModel => ServiceLocator.Current.GetInstance<ViewViewModel>();
        public HelpViewModel HelpViewModel => ServiceLocator.Current.GetInstance<HelpViewModel>();

        public FindWindowModel FindWindowModel => ServiceLocator.Current.GetInstance<FindWindowModel>();


        public RelayCommand<TextBox> SetDocumentModelTextBoxCommand { get; }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
