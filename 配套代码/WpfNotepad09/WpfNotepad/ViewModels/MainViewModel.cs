using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Resources;
using WpfNotepad.Helpers;
using WpfNotepad.Models;

namespace WpfNotepad.ViewModels
{
    public class MainViewModel
    {
        public DocumentModel DocumentModel { get; }

        public FileViewModel FileViewModel { get; }
        public EditViewModel EditViewModel { get; }
        public FormatViewModel FormatViewModel { get;}
        public ViewViewModel ViewViewModel { get; }
        public HelpViewModel HelpViewModel { get; }         

        public MainViewModel()
        {
            DocumentModel = new DocumentModel();

            FileViewModel = new FileViewModel(DocumentModel);
            EditViewModel = new EditViewModel(DocumentModel);
            FormatViewModel = new FormatViewModel(DocumentModel);
            ViewViewModel = new ViewViewModel(DocumentModel);
            HelpViewModel = new HelpViewModel();

        }      
    }
}
