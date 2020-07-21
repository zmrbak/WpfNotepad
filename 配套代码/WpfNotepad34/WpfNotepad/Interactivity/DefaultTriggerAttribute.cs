using System;
using System.Collections;
using System.Globalization;

namespace WpfNotepad.Interactivity
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
    public sealed class DefaultTriggerAttribute : Attribute
    {
        public Type TargetType
        {
            get
            {
                return this.targetType;
            }
        }

        public Type TriggerType
        {
            get
            {
                return this.triggerType;
            }
        }

        public IEnumerable Parameters
        {
            get
            {
                return this.parameters;
            }
        }

        public DefaultTriggerAttribute(Type targetType, Type triggerType, object parameter) : this(targetType, triggerType, new object[]
        {
            parameter
        })
        {
        }

        public DefaultTriggerAttribute(Type targetType, Type triggerType, params object[] parameters)
        {
            if (!typeof(TriggerBase).IsAssignableFrom(triggerType))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionStringTable.DefaultTriggerAttributeInvalidTriggerTypeSpecifiedExceptionMessage, new object[]
                {
                    triggerType.Name
                }));
            }
            this.targetType = targetType;
            this.triggerType = triggerType;
            this.parameters = parameters;
        }

        public TriggerBase Instantiate()
        {
            object obj = null;
            try
            {
                obj = Activator.CreateInstance(this.TriggerType, this.parameters);
            }
            catch
            {
            }
            return (TriggerBase)obj;
        }

        private Type targetType;

        private Type triggerType;

        private object[] parameters;
    }
}