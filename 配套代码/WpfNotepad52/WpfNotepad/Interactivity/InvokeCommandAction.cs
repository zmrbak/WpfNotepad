using System;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace WpfNotepad.Interactivity
{
    public sealed class InvokeCommandAction : TriggerAction<DependencyObject>
    {
        public string CommandName
        {
            get
            {
                base.ReadPreamble();
                return this.commandName;
            }
            set
            {
                if (this.CommandName != value)
                {
                    base.WritePreamble();
                    this.commandName = value;
                    base.WritePostscript();
                }
            }
        }

        public ICommand Command
        {
            get
            {
                return (ICommand)base.GetValue(InvokeCommandAction.CommandProperty);
            }
            set
            {
                base.SetValue(InvokeCommandAction.CommandProperty, value);
            }
        }

        public object CommandParameter
        {
            get
            {
                return base.GetValue(InvokeCommandAction.CommandParameterProperty);
            }
            set
            {
                base.SetValue(InvokeCommandAction.CommandParameterProperty, value);
            }
        }

        protected override void Invoke(object parameter)
        {
            if (base.AssociatedObject != null)
            {
                ICommand command = this.ResolveCommand();
                if (command != null && command.CanExecute(this.CommandParameter))
                {
                    command.Execute(this.CommandParameter);
                }
            }
        }

        private ICommand ResolveCommand()
        {
            ICommand result = null;
            if (this.Command != null)
            {
                result = this.Command;
            }
            else if (base.AssociatedObject != null)
            {
                Type type = base.AssociatedObject.GetType();
                PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                foreach (PropertyInfo propertyInfo in properties)
                {
                    if (typeof(ICommand).IsAssignableFrom(propertyInfo.PropertyType) && string.Equals(propertyInfo.Name, this.CommandName, StringComparison.Ordinal))
                    {
                        result = (ICommand)propertyInfo.GetValue(base.AssociatedObject, null);
                    }
                }
            }
            return result;
        }

        private string commandName;

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(InvokeCommandAction), null);

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(InvokeCommandAction), null);
    }
}