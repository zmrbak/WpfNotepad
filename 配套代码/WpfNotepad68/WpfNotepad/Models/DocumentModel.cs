using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfNotepad.Helpers;

namespace WpfNotepad.Models
{
    public class DocumentModel : ObservableObject
    {
        private bool isStatusBarShow = true;
        public bool IsStatusBarShow { get => isStatusBarShow; set => Set(ref isStatusBarShow, value); }

        private bool isWrapped = true;
        public bool IsWrapped { get => isWrapped; set => Set(ref isWrapped, value); }

        //FontFamily="Microsoft YaHei UI Light"
        private string fontFamily = "微软雅黑";
        public string FontFamily { get => fontFamily; set => Set(ref fontFamily, value); }

        //FontSize="16"
        public double FontSizeDefault => 16;
        private double fontSize = 16;
        public double FontSize { get => fontSize; set => Set(ref fontSize, value); }

        //FontStyle="Italic"
        private string fontStyle = "Normal";
        public string FontStyle { get => fontStyle; set => Set(ref fontStyle, value); }

        //FontWeight="Bold"
        private string fontWeight = "Normal";
        public string FontWeight { get => fontWeight; set => Set(ref fontWeight, value); }

        private string text;
        public string Text
        {
            get => text;
            set
            {
                Set(ref text, value);
                IsDirty = true;
            }
        }

        private string findText = "";
        public string FindText { get => findText; set => Set(ref findText, value); }

        private bool isFindCaseSensitive = false;
        public bool IsFindCaseSensitive { get => isFindCaseSensitive; set => Set(ref isFindCaseSensitive, value); }

        private bool isFindCirculated = false;
        public bool IsFindCirculated { get => isFindCirculated; set => Set(ref isFindCirculated, value); }

        private bool isFindUp = false;
        public bool IsFindUp { get => isFindUp; set => Set(ref isFindUp, value); }

        private string replacedText = "";
        public string ReplacedText { get => replacedText; set => Set(ref replacedText, value); }

        //主文本框
        public TextBox TextBox { set; get; }

        public Boolean IsFindWindowOpened { get; set; } = false;

        private int gotoLine = 1;
        public int GotoLine { get => gotoLine; set => Set(ref gotoLine, value); }

        private string fileName = "无标题";
        public string FileName { get => fileName; set => Set(ref fileName, value); }

        private string filePath;
        public string FilePath { get => filePath; set => Set(ref filePath, value); }

        private string fileExt;
        public string FileExt { get => fileExt; set => Set(ref fileExt, value); }

        private FileEncodes fileEncoding = FileEncodes.UTF8;
        public FileEncodes FileEncoding { get => fileEncoding; set => Set(ref fileEncoding, value); }


        private bool isDirty = false;
        public bool IsDirty { get => isDirty; set => Set(ref isDirty, value); }

        //打印设置
        private int paperSizeRawKind = 9;
        public int PaperSizeRawKind { get { return paperSizeRawKind; } set { paperSizeRawKind = value; } }

        private int paperSourceRawKind = 15;
        public int PaperSourceRawKind { get { return paperSourceRawKind; } set { paperSourceRawKind = value; } }

        private bool paperLandscape = false;
        public bool PaperLandscape { get { return paperLandscape; } set { paperLandscape = value; } }

        private Margins pagerMargins = new Margins() { Bottom = 1000, Top = 1000, Left = 1000, Right = 1000 };
        public Margins PagerMargins { get { return pagerMargins; } set { pagerMargins = value; } }

        private string paperHeader = "";
        public string PaperHeader { get { return paperHeader; } set { paperHeader = value; } }

        private string paperFooter = "";
        public string PaperFooter { get { return paperFooter; } set { paperFooter = value; } }


        public void Clear()
        {
            Text = "";
            FileName = "无标题";
            FilePath = "";
            FileExt = "";
            FileEncoding = FileEncodes.UTF8;
            IsDirty = false;
        }


        public DocumentModel()
        {
            LoadSettings();
        }

        //保存设置
        string appDataFileName = "";
        public void SaveSettings()
        {
            SavedSettings savedSettings = new SavedSettings();
            savedSettings.FindText = FindText;
            savedSettings.IsFindCaseSensitive = IsFindCaseSensitive;
            savedSettings.IsFindCirculated = IsFindCirculated;
            savedSettings.IsFindUp = IsFindUp;
            savedSettings.ReplacedText = ReplacedText;

            using (FileStream fileStream = new FileStream(appDataFileName, FileMode.Create))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, savedSettings);
            }
        }

        //从磁盘上恢复
        public void LoadSettings()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            AssemblyProductAttribute product = assembly.GetCustomAttribute<AssemblyProductAttribute>();

            appDataFileName = Path.Combine(
               Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
               "Zmrbak",
               product.Product,
               product.Product + ".Settings"
                );
            string appDataPath = Path.GetDirectoryName(appDataFileName);

            if (Directory.Exists(appDataPath) == false)
            {
                Directory.CreateDirectory(appDataPath);
                return;
            }

            if (File.Exists(appDataFileName) == false) return;

            using (FileStream fileStream = new FileStream(appDataFileName, FileMode.Open))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                SavedSettings savedSettings = binaryFormatter.Deserialize(fileStream) as SavedSettings;
                if (savedSettings == null) return;

                FindText = savedSettings.FindText;
                IsFindCaseSensitive = savedSettings.IsFindCaseSensitive;
                IsFindCirculated = savedSettings.IsFindCirculated;
                IsFindUp = savedSettings.IsFindUp;
                ReplacedText = savedSettings.ReplacedText;
            }
        }
    }
}
