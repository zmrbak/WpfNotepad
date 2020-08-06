using System;

namespace WpfNotepad.Interactivity
{
    internal sealed class NameResolvedEventArgs : EventArgs
    {
        public object OldObject
        {
            get
            {
                return this.oldObject;
            }
        }

        public object NewObject
        {
            get
            {
                return this.newObject;
            }
        }

        public NameResolvedEventArgs(object oldObject, object newObject)
        {
            this.oldObject = oldObject;
            this.newObject = newObject;
        }

        private object oldObject;

        private object newObject;
    }
}