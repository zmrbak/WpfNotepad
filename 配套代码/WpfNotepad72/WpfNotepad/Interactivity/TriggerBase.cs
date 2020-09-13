using System;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media.Animation;

namespace WpfNotepad.Interactivity
{
    [ContentProperty("Actions")]
    public abstract class TriggerBase : Animatable, IAttachedObject
    {
        internal TriggerBase(Type associatedObjectTypeConstraint)
        {
            this.associatedObjectTypeConstraint = associatedObjectTypeConstraint;
            TriggerActionCollection value = new TriggerActionCollection();
            base.SetValue(TriggerBase.ActionsPropertyKey, value);
        }

        protected DependencyObject AssociatedObject
        {
            get
            {
                base.ReadPreamble();
                return this.associatedObject;
            }
        }

        protected virtual Type AssociatedObjectTypeConstraint
        {
            get
            {
                base.ReadPreamble();
                return this.associatedObjectTypeConstraint;
            }
        }

        public TriggerActionCollection Actions
        {
            get
            {
                return (TriggerActionCollection)base.GetValue(TriggerBase.ActionsProperty);
            }
        }

        public event EventHandler<PreviewInvokeEventArgs> PreviewInvoke;

        protected void InvokeActions(object parameter)
        {
            if (this.PreviewInvoke != null)
            {
                PreviewInvokeEventArgs previewInvokeEventArgs = new PreviewInvokeEventArgs();
                this.PreviewInvoke(this, previewInvokeEventArgs);
                if (previewInvokeEventArgs.Cancelling)
                {
                    return;
                }
            }
            foreach (TriggerAction triggerAction in this.Actions)
            {
                triggerAction.CallInvoke(parameter);
            }
        }

        protected virtual void OnAttached()
        {
        }

        protected virtual void OnDetaching()
        {
        }

        protected override Freezable CreateInstanceCore()
        {
            Type type = base.GetType();
            return (Freezable)Activator.CreateInstance(type);
        }

        DependencyObject IAttachedObject.AssociatedObject
        {
            get
            {
                return this.AssociatedObject;
            }
        }

        public void Attach(DependencyObject dependencyObject)
        {
            if (dependencyObject != this.AssociatedObject)
            {
                if (this.AssociatedObject != null)
                {
                    throw new InvalidOperationException(ExceptionStringTable.CannotHostTriggerMultipleTimesExceptionMessage);
                }
                if (dependencyObject != null && !this.AssociatedObjectTypeConstraint.IsAssignableFrom(dependencyObject.GetType()))
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, ExceptionStringTable.TypeConstraintViolatedExceptionMessage, new object[]
                    {
                        base.GetType().Name,
                        dependencyObject.GetType().Name,
                        this.AssociatedObjectTypeConstraint.Name
                    }));
                }
                base.WritePreamble();
                this.associatedObject = dependencyObject;
                base.WritePostscript();
                this.Actions.Attach(dependencyObject);
                this.OnAttached();
            }
        }

        public void Detach()
        {
            this.OnDetaching();
            base.WritePreamble();
            this.associatedObject = null;
            base.WritePostscript();
            this.Actions.Detach();
        }

        private DependencyObject associatedObject;

        private Type associatedObjectTypeConstraint;

        private static readonly DependencyPropertyKey ActionsPropertyKey = DependencyProperty.RegisterReadOnly("Actions", typeof(TriggerActionCollection), typeof(TriggerBase), new FrameworkPropertyMetadata());

        public static readonly DependencyProperty ActionsProperty = TriggerBase.ActionsPropertyKey.DependencyProperty;
    }
}