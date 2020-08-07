using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfNotepad.Helpers
{
    public interface IMessenger
    {
        //1
        void Register<TMessage>( object recipient, Action<TMessage> action, bool keepTargetAlive = false);
        void Register<TMessage>( object recipient, object token, Action<TMessage> action, bool keepTargetAlive = false);
        void Register<TMessage>( object recipient, object token, bool receiveDerivedMessagesToo, Action<TMessage> action, bool keepTargetAlive = false);
        void Register<TMessage>( object recipient, bool receiveDerivedMessagesToo, Action<TMessage> action, bool keepTargetAlive = false); 

        //2
        void Send<TMessage>(TMessage message);
        void Send<TMessage, TTarget>(TMessage message);
        void Send<TMessage>(TMessage message, object token);

        //3
        void Unregister(object recipient);
        void Unregister<TMessage>(object recipient);
        void Unregister<TMessage>(object recipient, object token);
        void Unregister<TMessage>(object recipient, Action<TMessage> action);
        void Unregister<TMessage>(object recipient, object token, Action<TMessage> action);
    }
}
