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
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Resources;
using System.Windows.Shapes;

namespace WpfNotepad
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var a1 = ApplicationCommands.CancelPrint;
            var a2 = ComponentCommands.ExtendSelectionDown;
            var a4 = EditingCommands.AlignCenter;
            var a5 = MediaCommands.BoostBass;
            var a6 = NavigationCommands.BrowseBack;
            var a7 = Slider.ActualHeightProperty;
            var a8 = DocumentViewer.ActualHeightProperty;
        }
    }
}
