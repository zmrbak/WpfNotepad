using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MyCommand = new RelayCommand();

            this.DataContext = this;
        }

        public RelayCommand MyCommand { get; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void textBox_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "文本文档(*.txt)|*.txt|所有文件|*.*";
            saveFileDialog.FileName = "*.txt";
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.Title = "另存为";
            saveFileDialog.AddExtension = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.CustomPlaces.Add(new FileDialogCustomPlace(@"C:\app"));
            saveFileDialog.CustomPlaces.Add(new FileDialogCustomPlace(@"C:\app\Zmrbak"));
            saveFileDialog.CustomPlaces.Add(new FileDialogCustomPlace(@"C:\app\Zmrbak\cfgtoollogs\netca"));
            saveFileDialog.DereferenceLinks = true;
            saveFileDialog.InitialDirectory = "D:\\";
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.ValidateNames = true;
            saveFileDialog.ShowDialog();
            var aaa= saveFileDialog.CustomPlaces;
        }
    }
}
