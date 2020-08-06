using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfNotepad.Helpers
{
    public interface ISimpleIoc : IServiceLocator
    {
        bool ContainsCreated<TClass>();

        bool ContainsCreated<TClass>(string key);

        bool IsRegistered<T>();

        bool IsRegistered<T>(string key);

        void Register<TInterface, TClass>()
            where TInterface : class
            where TClass : class, TInterface;

        void Register<TInterface, TClass>(bool createInstanceImmediately)
            where TInterface : class
            where TClass : class, TInterface;

        void Register<TClass>()
            where TClass : class;

        void Register<TClass>(bool createInstanceImmediately)
            where TClass : class;

        void Register<TClass>(Func<TClass> factory)
            where TClass : class;

        void Register<TClass>(Func<TClass> factory, bool createInstanceImmediately)
            where TClass : class;

        void Register<TClass>(Func<TClass> factory, string key)
            where TClass : class;

        void Register<TClass>(
            Func<TClass> factory,
            string key,
            bool createInstanceImmediately)
            where TClass : class;

        void Reset();

        void Unregister<TClass>()
            where TClass : class;

        void Unregister<TClass>(TClass instance)
            where TClass : class;

        void Unregister<TClass>(string key)
            where TClass : class;
    }
}
