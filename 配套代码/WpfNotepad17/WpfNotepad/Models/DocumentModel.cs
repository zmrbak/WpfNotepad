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
        public string Text { get => text; set => Set(ref text, value); }
    }
}
