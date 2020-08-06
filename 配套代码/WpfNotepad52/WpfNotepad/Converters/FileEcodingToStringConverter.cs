using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WpfNotepad.Models;

namespace WpfNotepad.Converters
{
    public class FileEcodingToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "";
            FileEncodes fileEncodes = (FileEncodes)value;
            switch (fileEncodes)
            {
                case FileEncodes.NONE:
                    return "未知";
                case FileEncodes.ANSI:
                    return "ANSI";
                case FileEncodes.UTF16LE:
                    return "UTF-16 LE";
                case FileEncodes.UTF16BE:
                    return "UTF-16 BE";
                case FileEncodes.UTF8:
                    return "UTF-8";
                case FileEncodes.BOMUTF8:
                    return "带有 BOM 的 UTF-8";
                default:
                    return "未知";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
