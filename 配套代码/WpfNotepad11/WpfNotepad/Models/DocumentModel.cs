using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfNotepad.Models
{
    public class DocumentModel:INotifyPropertyChanged
    {
        private double fontSize= 12;

        public double FontSize
        {
            get { return fontSize; }
            set {
                fontSize = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("FontSize"));
            }
        }

        public double FontSizeDefault => 12;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
