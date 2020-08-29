using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media.Animation;

namespace WpfNotepad.Interactivity
{
    public abstract class Behavior : Animatable, IAttachedObject
    {
        internal event EventHandler AssociatedObjectChanged;

        protected Type AssociatedType
        {
            get
            {
                base.ReadPreamble();
                return this.associatedType;
            }
        }

        protected DependencyObject AssociatedObject
        {
            get
            {
                base.ReadPreamble();
                return this.associatedObject;
            }
        }

        internal Behavior(Type associatedType)
        {
            this.associatedType = associatedType;
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

        private void OnAssociatedObjectChanged()
        {
            this.AssociatedObjectChanged?.Invoke(this, new EventArgs());
        }

        DependencyObject IAttachedObject.AssociatedObject => this.AssociatedObject;

        public void Attach(DependencyObject dependencyObject)
        {
            if (dependencyObject != this.AssociatedObject)
            {
                if (this.AssociatedObject != null)
                {
                    throw new InvalidOperationException(ExceptionStringTable.CannotHostBehaviorMultipleTimesExceptionMessage);
                }
                if (dependencyObject != null && !this.AssociatedType.IsAssignableFrom(dependencyObject.GetType()))
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, ExceptionStringTable.TypeConstraintViolatedExceptionMessage, new object[]
                    {
                        base.GetType().Name,
                        dependencyObject.GetType().Name,
                        this.AssociatedType.Name
                    }));
                }
                base.WritePreamble();
                this.associatedObject = dependencyObject;
                base.WritePostscript();
                this.OnAssociatedObjectChanged();
                this.OnAttached();
            }
        }

        public void Detach()
        {
            this.OnDetaching();
            base.WritePreamble();
            this.associatedObject = null;
            base.WritePostscript();
            this.OnAssociatedObjectChanged();
        }

        private Type associatedType;

        private DependencyObject associatedObject;
    }
}