using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfNotepad.Helpers;

namespace WpfNotepad.Models
{
    public class DocumentModel : ObservableObject
    {
        public double FontSizeDefault => 12;

        private double fontSize = 12;
        public double FontSize { get => fontSize; set => Set(ref fontSize, value); }

        private bool isStatusBarShow = true;
        public bool IsStatusBarShow { get => isStatusBarShow; set => Set(ref isStatusBarShow, value); }



    }
}
