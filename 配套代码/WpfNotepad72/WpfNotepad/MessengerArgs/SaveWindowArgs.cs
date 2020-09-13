using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfNotepad.MessengerArgs
{
    public class SaveWindowArgs:EventArgs
    {
        public bool IsOpenFile { get; set; } = false;
        public bool IsAppExit { get; set; } = false;
    }
}
