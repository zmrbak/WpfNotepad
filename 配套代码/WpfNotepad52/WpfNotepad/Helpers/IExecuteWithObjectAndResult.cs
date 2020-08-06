using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfNotepad.Helpers
{
    public interface IExecuteWithObjectAndResult
    {
        object ExecuteWithObject(object parameter);
    }
}
