using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfNotepad.Models
{
    public enum FileEncodes
    {
        NONE=0,
        ANSI,
        UTF16LE,
        UTF16BE,
        UTF8,
        BOMUTF8
    }
}
