using System;

namespace WpfNotepad.Interactivity
{
    public class PreviewInvokeEventArgs : EventArgs
    {
        public bool Cancelling { get; set; }
    }
}