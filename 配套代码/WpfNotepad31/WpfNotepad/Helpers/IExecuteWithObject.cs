using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfNotepad.Helpers
{
    public interface IExecuteWithObject
    {
        object Target { get; }

        void ExecuteWithObject(object parameter);

        void MarkForDeletion();
    }
}
