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

namespace WpfNotepad.ViewModels
{
    public class MainViewModel
    {
        public HelpViewModel HelpViewModel { get; }
        public MainViewModel()
        {
            HelpViewModel = new HelpViewModel();
        }      
    }
}
