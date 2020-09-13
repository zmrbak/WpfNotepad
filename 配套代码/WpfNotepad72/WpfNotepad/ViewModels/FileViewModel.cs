using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Printing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Xps.Packaging;
using WpfNotepad.Helpers;
using WpfNotepad.MessengerArgs;
using WpfNotepad.Models;
using WpfNotepad.Utility;
using ZmrbakFileDialog;

namespace WpfNotepad.ViewModels
{
    public class FileViewModel
    {
        public FileViewModel(DocumentModel documentModel)
        {
            //DocumentModel = (Application.Current.Resources["Locator"] as ViewModelLocator).DocumentModel;
            DocumentModel = documentModel;

            //NewCommand = new RelayCommand(OnNewCommand);
            NewWindowCommand = new RelayCommand(OnNewWindowCommand);
            //OpenCommand = new RelayCommand(OnOpenCommand);
            SaveCommand = new RelayCommand(OnSaveCommand1);
            SaveAsCommand = new RelayCommand(OnSaveAsCommand1);
            PageSettingCommand = new RelayCommand(OnPageSettingCommand);
            PrintCommand = new RelayCommand(OnPrintCommand);
            ExitCommand = new RelayCommand(OnExitCommand);

            var newCommandBinding = new CommandBinding(ApplicationCommands.New, OnNewCommand, CanOnNewCommand);
            CommandManager.RegisterClassCommandBinding(typeof(MainWindow), newCommandBinding);

            var openCommandBinding = new CommandBinding(ApplicationCommands.Open, OnOpenCommand, CanOnOpenCommand);
            CommandManager.RegisterClassCommandBinding(typeof(MainWindow), openCommandBinding);
        }

        private void CanOnOpenCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OnOpenCommand(object sender, ExecutedRoutedEventArgs e)
        {
            if (DocumentModel.IsDirty == true)
            {
                Messenger.Default.Send(new SaveWindowArgs() { IsOpenFile = true });
            }
            else
            {
                OpenFile();
            }
        }

        unsafe public void OpenFile()
        {
            FileOpenDialog fileOpenDialog = new FileOpenDialog();
            fileOpenDialog.fileName = (char*)Marshal.StringToCoTaskMemUni("*.txt");
            fileOpenDialog.selectedItemIndex = (uint)FileEncodes.NONE;
            if (fileOpenDialog.ShowDialog(0) == true)
            {
                string fileName = Marshal.PtrToStringUni((IntPtr)fileOpenDialog.fileName);
                DocumentModel.FileExt = Path.GetExtension(fileName);
                DocumentModel.FilePath = Path.GetDirectoryName(fileName);
                DocumentModel.FileName = Path.GetFileNameWithoutExtension(fileName);

                int selectedItemIndex = (int)fileOpenDialog.selectedItemIndex;
                DocumentModel.FileEncoding = (FileEncodes)selectedItemIndex;
                switch (DocumentModel.FileEncoding)
                {
                    case FileEncodes.NONE:
                        using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                        using (BinaryReader br = new BinaryReader(fileStream))
                        {
                            byte[] buffer = br.ReadBytes(3);

                            //Unicode16 LE
                            byte[] u16le = new byte[] { 0xFF, 0xFE };

                            //Unicode16 BE
                            byte[] u16be = new byte[] { 0xFE, 0xFF };

                            //BOM UTF8
                            byte[] utf8Bom = new byte[] { 0xEF, 0xBB, 0xBF };

                            //BOM UTF8
                            if (buffer[0] == utf8Bom[0] && buffer[1] == utf8Bom[1] && buffer[2] == utf8Bom[2])
                            {
                                DocumentModel.Text = File.ReadAllText(fileName, new UTF8Encoding(true));
                                DocumentModel.FileEncoding = FileEncodes.BOMUTF8;
                                break;
                            }

                            //Unicode16 LE
                            if (buffer[0] == u16le[0] && buffer[1] == u16le[1])
                            {
                                DocumentModel.Text = File.ReadAllText(fileName, Encoding.Unicode);
                                DocumentModel.FileEncoding = FileEncodes.UTF16LE;
                                break;
                            }

                            //Unicode16 BE
                            if (buffer[0] == u16be[0] && buffer[1] == u16be[1])
                            {
                                DocumentModel.Text = File.ReadAllText(fileName, Encoding.BigEndianUnicode);
                                DocumentModel.FileEncoding = FileEncodes.UTF16BE;
                                break;
                            }

                            //UTF8 还是 Default
                            {
                                long i = fileStream.Length;
                                buffer = br.ReadBytes((int)i);
                                if (IsUTF8Bytes(buffer) == true)
                                {
                                    DocumentModel.Text = File.ReadAllText(fileName, new UTF8Encoding(false));
                                    DocumentModel.FileEncoding = FileEncodes.UTF8;
                                    break;
                                }
                                else
                                {
                                    DocumentModel.Text = File.ReadAllText(fileName, Encoding.Default);
                                    DocumentModel.FileEncoding = FileEncodes.ANSI;
                                    break;
                                }
                            }
                        }
                    case FileEncodes.ANSI:
                        DocumentModel.Text = File.ReadAllText(fileName, Encoding.Default);
                        break;
                    case FileEncodes.UTF16LE:
                        DocumentModel.Text = File.ReadAllText(fileName, Encoding.Unicode);
                        break;
                    case FileEncodes.UTF16BE:
                        DocumentModel.Text = File.ReadAllText(fileName, Encoding.BigEndianUnicode);
                        break;
                    case FileEncodes.UTF8:
                        DocumentModel.Text = File.ReadAllText(fileName, new UTF8Encoding(false));
                        break;
                    case FileEncodes.BOMUTF8:
                        DocumentModel.Text = File.ReadAllText(fileName, new UTF8Encoding(true));
                        break;
                }

                DocumentModel.IsDirty = false;
            }
        }

        private bool IsUTF8Bytes(byte[] data)
        {
            //计算当前正分析的字符应还有的字节数 
            int charByteCounter = 1;

            //当前分析的字节.
            byte curByte;
            for (int i = 0; i < data.Length; i++)
            {
                curByte = data[i];
                if (charByteCounter == 1)
                {
                    if (curByte >= 0x80)
                    {
                        //判断当前 
                        while (((curByte <<= 1) & 0x80) != 0)
                        {
                            charByteCounter++;
                        }
                        //标记位首位若为非0 则至少以2个1开始 如:110XXXXX...........1111110X 
                        if (charByteCounter == 1 || charByteCounter > 6)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    //若是UTF-8 此时第一位必须为1 
                    if ((curByte & 0xC0) != 0x80)
                    {
                        return false;
                    }
                    charByteCounter--;
                }
            }
            if (charByteCounter > 1)
            {
                throw new Exception("非预期的byte格式");
            }
            return true;
        }

        private void OnSaveAsCommand1()
        {
            OnSaveAsCommand();
        }

        private void OnSaveCommand1()
        {
            OnSaveCommand();
        }

        private void CanOnNewCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OnNewCommand(object sender, ExecutedRoutedEventArgs e)
        {
            if (DocumentModel.IsDirty == true)
            {
                //MessageBox.Show(
                //    "你像讲更改保存到 " + DocumentModel.FileName + " 吗?",
                //    "记事本",
                //    MessageBoxButton.YesNoCancel,
                //    MessageBoxImage.None,
                //    MessageBoxResult.OK,
                //    MessageBoxOptions.DefaultDesktopOnly
                //    );

                Messenger.Default.Send(new SaveWindowArgs());
            }
            else
            {
                DocumentModel.Clear();
            }
        }



        private void OnNewWindowCommand()
        {
            Process.Start(Process.GetCurrentProcess().MainModule.FileName);
        }

        //private void OnOpenCommand()
        //{
        //    throw new NotImplementedException();
        //}

        public bool OnSaveCommand()
        {
            if (String.IsNullOrEmpty(DocumentModel.FilePath) == true)
            {
                return OnSaveAsCommand();
            }
            else
            {
                return SaveFile();
            }
        }

        unsafe private bool OnSaveAsCommand()
        {
            FileSaveDialog fileSaveDialog = new FileSaveDialog();
            fileSaveDialog.fileName = (char*)Marshal.StringToCoTaskMemUni(DocumentModel.FileName);
            fileSaveDialog.selectedItemIndex = (uint)DocumentModel.FileEncoding;
            if (fileSaveDialog.ShowDialog(0) == true)
            {
                string fileName = Marshal.PtrToStringUni((IntPtr)fileSaveDialog.fileName);
                DocumentModel.FileExt = Path.GetExtension(fileName);
                DocumentModel.FilePath = Path.GetDirectoryName(fileName);
                DocumentModel.FileName = Path.GetFileNameWithoutExtension(fileName);

                int selectedItemIndex = (int)fileSaveDialog.selectedItemIndex;
                DocumentModel.FileEncoding = (FileEncodes)selectedItemIndex;

                return SaveFile();
            }

            return false;
        }

        private bool SaveFile()
        {
            string fileFullPath = Path.Combine(DocumentModel.FilePath, DocumentModel.FileName + DocumentModel.FileExt);
            Encoding encoding = null;
            switch (DocumentModel.FileEncoding)
            {
                case FileEncodes.NONE:
                    encoding = Encoding.UTF8;
                    break;
                case FileEncodes.ANSI:
                    encoding = Encoding.Default;
                    break;
                case FileEncodes.UTF16LE:
                    encoding = Encoding.Unicode;
                    break;
                case FileEncodes.UTF16BE:
                    encoding = Encoding.BigEndianUnicode;
                    break;
                case FileEncodes.UTF8:
                    encoding = new UTF8Encoding(false);
                    break;
                case FileEncodes.BOMUTF8:
                    encoding = new UTF8Encoding(true);
                    break;
            }

            File.WriteAllText(fileFullPath, DocumentModel.Text, encoding);

            DocumentModel.IsDirty = false;

            return true;
        }

        unsafe private void OnPageSettingCommand()
        {
            PageSetupDialog pageSetupDialog = new PageSetupDialog();
            pageSetupDialog.PaperSize = (uint)DocumentModel.PaperSizeRawKind;
            pageSetupDialog.DefaultSource = (uint)DocumentModel.PaperSourceRawKind;
            pageSetupDialog.Landscape = DocumentModel.PaperLandscape == true ? 2U : 1U;

            pageSetupDialog.MarginBottom = (uint)DocumentModel.PagerMargins.Bottom;
            pageSetupDialog.MarginLeft = (uint)DocumentModel.PagerMargins.Left;
            pageSetupDialog.MarginRight = (uint)DocumentModel.PagerMargins.Right;
            pageSetupDialog.MarginTop = (uint)DocumentModel.PagerMargins.Top;

            pageSetupDialog.PaperHeader = (char*)Marshal.StringToCoTaskMemUni(DocumentModel.PaperHeader);
            pageSetupDialog.PaperFooter = (char*)Marshal.StringToCoTaskMemUni(DocumentModel.PaperFooter);

            IntPtr ipWnd = new WindowInteropHelper(Application.Current.MainWindow).Handle;
            if (pageSetupDialog.ShowDialog((int)ipWnd) == true)
            {
                DocumentModel.PaperSizeRawKind = (int)pageSetupDialog.PaperSize;
                DocumentModel.PaperSourceRawKind = (int)pageSetupDialog.DefaultSource;
                DocumentModel.PaperLandscape = pageSetupDialog.Landscape == 1 ? false : true;

                DocumentModel.PagerMargins.Bottom = (int)pageSetupDialog.MarginBottom;
                DocumentModel.PagerMargins.Top = (int)pageSetupDialog.MarginTop;
                DocumentModel.PagerMargins.Left = (int)pageSetupDialog.MarginLeft;
                DocumentModel.PagerMargins.Right = (int)pageSetupDialog.MarginRight;

                DocumentModel.PaperHeader = Marshal.PtrToStringUni((IntPtr)pageSetupDialog.PaperHeader);
                DocumentModel.PaperFooter = Marshal.PtrToStringUni((IntPtr)pageSetupDialog.PaperFooter);
            }
        }

        private void OnPrintCommand()
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.PrintQueue = LocalPrintServer.GetDefaultPrintQueue();
            printDialog.PrintTicket = printDialog.PrintQueue.DefaultPrintTicket;
            PrintCapabilities printCapabilities = printDialog.PrintQueue.GetPrintCapabilities();

            //DocumentModel.PaperLandscape
            if (DocumentModel.PaperLandscape == true)
            {
                if (printCapabilities.PageOrientationCapability.Contains(PageOrientation.Landscape) == true)
                {
                    printDialog.PrintTicket.PageOrientation = PageOrientation.Landscape;
                }
            }
            else
            {
                if (printCapabilities.PageOrientationCapability.Contains(PageOrientation.Portrait) == true)
                {
                    printDialog.PrintTicket.PageOrientation = PageOrientation.Portrait;
                }
            }


            // DocumentModel.PaperSizeRawKind
            PrinterSettings printerSettings = new PrinterSettings();
            foreach (PaperSize item in printerSettings.PaperSizes)
            {
                if (item.RawKind == DocumentModel.PaperSizeRawKind)
                {
                    PageMediaSize pageMediaSize = new PageMediaSize(item.Kind.ToPageMediaSizeName());
                    int count = printCapabilities.PageMediaSizeCapability.Where(x => x.PageMediaSizeName == pageMediaSize.PageMediaSizeName).Count();
                    if (count > 0)
                    {
                        printDialog.PrintTicket.PageMediaSize = pageMediaSize;
                    }
                    break;
                }
            }

            //DocumentModel.PaperSourceRawKind
            foreach (PaperSource item in printerSettings.PaperSources)
            {
                if (item.RawKind == DocumentModel.PaperSourceRawKind)
                {
                    if (item.Kind == PaperSourceKind.FormSource)
                    {
                        if (printCapabilities.InputBinCapability.Contains(InputBin.AutoSelect))
                        {
                            printDialog.PrintTicket.InputBin = InputBin.AutoSelect;
                        }
                    }
                    else
                    {
                        if (printCapabilities.InputBinCapability.Contains(InputBin.Unknown))
                        {
                            printDialog.PrintTicket.InputBin = InputBin.Unknown;
                        }
                    }
                    break;
                }
            }

            if (printDialog.ShowDialog() == true)
            {
                //构造流文档
                FlowDocument flowDocument = new FlowDocument();

                //页面大小
                flowDocument.PageWidth = printDialog.PrintableAreaWidth;
                flowDocument.PageHeight = printDialog.PrintableAreaHeight;

                //列宽
                flowDocument.ColumnWidth = printDialog.PrintableAreaWidth;

                //边框
                Margins margins = PrinterUnitConvert.Convert(
                    DocumentModel.PagerMargins,
                    PrinterUnit.HundredthsOfAMillimeter,
                    PrinterUnit.ThousandthsOfAnInch);

                flowDocument.PagePadding = new Thickness()
                {
                    Bottom = margins.Bottom / 9.6,
                    Left = margins.Left / 9.6,
                    Right = margins.Right / 9.6,
                    Top = margins.Top / 9.6
                };

                //如果有页眉页脚的话，留出位置
                //默认微软雅黑，粗体，五号字,字高16像素
                //Typeface typeface = new Typeface(new FontFamily("微软雅黑"), FontStyles.Normal, FontWeights.Bold, FontStretches.Normal);
                if (DocumentModel.PaperHeader.Length > 0)
                {
                    flowDocument.PagePadding = new Thickness()
                    {
                        Bottom = margins.Bottom / 9.6,
                        Left = margins.Left / 9.6,
                        Right = margins.Right / 9.6,
                        Top = margins.Top / 9.6 + 20
                    };
                }

                //页脚
                if (DocumentModel.PaperFooter.Length > 0)
                {
                    flowDocument.PagePadding = new Thickness()
                    {
                        Bottom = margins.Bottom / 9.6 + 20,
                        Left = margins.Left / 9.6,
                        Right = margins.Right / 9.6,
                        Top = flowDocument.PagePadding.Top
                    };
                }

                //字体
                flowDocument.FontFamily = new System.Windows.Media.FontFamily(DocumentModel.FontFamily);
                flowDocument.FontSize = DocumentModel.FontSize;
                flowDocument.FontStyle = DocumentModel.FontStyle == "Italic" ? FontStyles.Italic : FontStyles.Normal;
                flowDocument.FontWeight = DocumentModel.FontWeight == "Bold" ? FontWeights.Bold : FontWeights.Normal;

                //添加内容
                Paragraph paragraph = new Paragraph(new Run(DocumentModel.Text));
                flowDocument.Blocks.Add(paragraph);

                //打印
                printDialog.PrintDocument(
                    new PaginatorHeaderFooter(((IDocumentPaginatorSource)flowDocument).DocumentPaginator, DocumentModel),
                    DocumentModel.FileName
                    );
            }
        }

        private void OnExitCommand()
        {
            //保存配置
            DocumentModel.SaveSettings();

            //保存文件
            if(DocumentModel.IsDirty==true)
            {
                Messenger.Default.Send(new SaveWindowArgs() { IsAppExit = true });
                return;
            }

            Application.Current.Shutdown();
        }

        public DocumentModel DocumentModel { get; }

        //public RelayCommand NewCommand { get; }
        public RelayCommand NewWindowCommand { get; }
        //public RelayCommand OpenCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand SaveAsCommand { get; }
        public RelayCommand PageSettingCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand ExitCommand { get; }

    }
}
