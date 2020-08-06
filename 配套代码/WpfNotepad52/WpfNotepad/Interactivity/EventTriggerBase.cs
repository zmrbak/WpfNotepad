using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows;

namespace WpfNotepad.Interactivity
{
    public abstract class EventTriggerBase : TriggerBase
    {
        protected sealed override Type AssociatedObjectTypeConstraint
        {
            get
            {
                AttributeCollection attributes = TypeDescriptor.GetAttributes(base.GetType());
                TypeConstraintAttribute typeConstraintAttribute = attributes[typeof(TypeConstraintAttribute)] as TypeConstraintAttribute;
                if (typeConstraintAttribute != null)
                {
                    return typeConstraintAttribute.Constraint;
                }
                return typeof(DependencyObject);
            }
        }

        protected Type SourceTypeConstraint
        {
            get
            {
                return this.sourceTypeConstraint;
            }
        }

        public object SourceObject
        {
            get
            {
                return base.GetValue(EventTriggerBase.SourceObjectProperty);
            }
            set
            {
                base.SetValue(EventTriggerBase.SourceObjectProperty, value);
            }
        }

        public string SourceName
        {
            get
            {
                return (string)base.GetValue(EventTriggerBase.SourceNameProperty);
            }
            set
            {
                base.SetValue(EventTriggerBase.SourceNameProperty, value);
            }
        }

        public object Source
        {
            get
            {
                object obj = base.AssociatedObject;
                if (this.SourceObject != null)
                {
                    obj = this.SourceObject;
                }
                else if (this.IsSourceNameSet)
                {
                    obj = this.SourceNameResolver.Object;
                    if (obj != null && !this.SourceTypeConstraint.IsAssignableFrom(obj.GetType()))
                    {
                        throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, ExceptionStringTable.RetargetedTypeConstraintViolatedExceptionMessage, new object[]
                        {
                            base.GetType().Name,
                            obj.GetType(),
                            this.SourceTypeConstraint,
                            "Source"
                        }));
                    }
                }
                return obj;
            }
        }

        private NameResolver SourceNameResolver
        {
            get
            {
                return this.sourceNameResolver;
            }
        }

        private bool IsSourceChangedRegistered
        {
            get
            {
                return this.isSourceChangedRegistered;
            }
            set
            {
                this.isSourceChangedRegistered = value;
            }
        }

        private bool IsSourceNameSet
        {
            get
            {
                return !string.IsNullOrEmpty(this.SourceName) || base.ReadLocalValue(EventTriggerBase.SourceNameProperty) != DependencyProperty.UnsetValue;
            }
        }

        private bool IsLoadedRegistered { get; set; }

        internal EventTriggerBase(Type sourceTypeConstraint) : base(typeof(DependencyObject))
        {
            this.sourceTypeConstraint = sourceTypeConstraint;
            this.sourceNameResolver = new NameResolver();
            this.RegisterSourceChanged();
        }

        protected abstract string GetEventName();

        protected virtual void OnEvent(EventArgs eventArgs)
        {
            base.InvokeActions(eventArgs);
        }

        private void OnSourceChanged(object oldSource, object newSource)
        {
            if (base.AssociatedObject != null)
            {
                this.OnSourceChangedImpl(oldSource, newSource);
            }
        }

        internal virtual void OnSourceChangedImpl(object oldSource, object newSource)
        {
            if (string.IsNullOrEmpty(this.GetEventName()))
            {
                return;
            }
            if (string.Compare(this.GetEventName(), "Loaded", StringComparison.Ordinal) != 0)
            {
                if (oldSource != null && this.SourceTypeConstraint.IsAssignableFrom(oldSource.GetType()))
                {
                    this.UnregisterEvent(oldSource, this.GetEventName());
                }
                if (newSource != null && this.SourceTypeConstraint.IsAssignableFrom(newSource.GetType()))
                {
                    this.RegisterEvent(newSource, this.GetEventName());
                }
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            DependencyObject associatedObject = base.AssociatedObject;
            Behavior behavior = associatedObject as Behavior;
            FrameworkElement frameworkElement = associatedObject as FrameworkElement;
            this.RegisterSourceChanged();
            if (behavior != null)
            {
                associatedObject = ((IAttachedObject)behavior).AssociatedObject;
                behavior.AssociatedObjectChanged += this.OnBehaviorHostChanged;
            }
            else
            {
                if (this.SourceObject == null)
                {
                    if (frameworkElement != null)
                    {
                        goto IL_5C;
                    }
                }
                try
                {
                    this.OnSourceChanged(null, this.Source);
                    goto IL_68;
                }
                catch (InvalidOperationException)
                {
                    goto IL_68;
                }
            IL_5C:
                this.SourceNameResolver.NameScopeReferenceElement = frameworkElement;
            }
        IL_68:
            bool flag = string.Compare(this.GetEventName(), "Loaded", StringComparison.Ordinal) == 0;
            if (flag && frameworkElement != null && !Interaction.IsElementLoaded(frameworkElement))
            {
                this.RegisterLoaded(frameworkElement);
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            Behavior behavior = base.AssociatedObject as Behavior;
            FrameworkElement frameworkElement = base.AssociatedObject as FrameworkElement;
            try
            {
                this.OnSourceChanged(this.Source, null);
            }
            catch (InvalidOperationException)
            {
            }
            this.UnregisterSourceChanged();
            if (behavior != null)
            {
                behavior.AssociatedObjectChanged -= this.OnBehaviorHostChanged;
            }
            this.SourceNameResolver.NameScopeReferenceElement = null;
            bool flag = string.Compare(this.GetEventName(), "Loaded", StringComparison.Ordinal) == 0;
            if (flag && frameworkElement != null)
            {
                this.UnregisterLoaded(frameworkElement);
            }
        }

        private void OnBehaviorHostChanged(object sender, EventArgs e)
        {
            this.SourceNameResolver.NameScopeReferenceElement = (((IAttachedObject)sender).AssociatedObject as FrameworkElement);
        }

        private static void OnSourceObjectChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            EventTriggerBase eventTriggerBase = (EventTriggerBase)obj;
            object @object = eventTriggerBase.SourceNameResolver.Object;
            if (args.NewValue == null)
            {
                eventTriggerBase.OnSourceChanged(args.OldValue, @object);
                return;
            }
            if (args.OldValue == null && @object != null)
            {
                eventTriggerBase.UnregisterEvent(@object, eventTriggerBase.GetEventName());
            }
            eventTriggerBase.OnSourceChanged(args.OldValue, args.NewValue);
        }

        private static void OnSourceNameChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            EventTriggerBase eventTriggerBase = (EventTriggerBase)obj;
            eventTriggerBase.SourceNameResolver.Name = (string)args.NewValue;
        }

        private void RegisterSourceChanged()
        {
            if (!this.IsSourceChangedRegistered)
            {
                this.SourceNameResolver.ResolvedElementChanged += this.OnSourceNameResolverElementChanged;
                this.IsSourceChangedRegistered = true;
            }
        }

        private void UnregisterSourceChanged()
        {
            if (this.IsSourceChangedRegistered)
            {
                this.SourceNameResolver.ResolvedElementChanged -= this.OnSourceNameResolverElementChanged;
                this.IsSourceChangedRegistered = false;
            }
        }

        private void OnSourceNameResolverElementChanged(object sender, NameResolvedEventArgs e)
        {
            if (this.SourceObject == null)
            {
                this.OnSourceChanged(e.OldObject, e.NewObject);
            }
        }

        private void RegisterLoaded(FrameworkElement associatedElement)
        {
            if (!this.IsLoadedRegistered && associatedElement != null)
            {
                associatedElement.Loaded += new RoutedEventHandler(this.OnEventImpl);
                this.IsLoadedRegistered = true;
            }
        }

        private void UnregisterLoaded(FrameworkElement associatedElement)
        {
            if (this.IsLoadedRegistered && associatedElement != null)
            {
                associatedElement.Loaded -= new RoutedEventHandler(this.OnEventImpl);
                this.IsLoadedRegistered = false;
            }
        }

        private void RegisterEvent(object obj, string eventName)
        {
            Type type = obj.GetType();
            EventInfo @event = type.GetEvent(eventName);
            if (@event == null)
            {
                if (this.SourceObject != null)
                {
                    throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionStringTable.EventTriggerCannotFindEventNameExceptionMessage, new object[]
                    {
                        eventName,
                        obj.GetType().Name
                    }));
                }
                return;
            }
            else
            {
                if (EventTriggerBase.IsValidEvent(@event))
                {
                    this.eventHandlerMethodInfo = typeof(EventTriggerBase).GetMethod("OnEventImpl", BindingFlags.Instance | BindingFlags.NonPublic);
                    @event.AddEventHandler(obj, Delegate.CreateDelegate(@event.EventHandlerType, this, this.eventHandlerMethodInfo));
                    return;
                }
                if (this.SourceObject != null)
                {
                    throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionStringTable.EventTriggerBaseInvalidEventExceptionMessage, new object[]
                    {
                        eventName,
                        obj.GetType().Name
                    }));
                }
                return;
            }
        }

        private static bool IsValidEvent(EventInfo eventInfo)
        {
            Type eventHandlerType = eventInfo.EventHandlerType;
            if (typeof(Delegate).IsAssignableFrom(eventInfo.EventHandlerType))
            {
                MethodInfo method = eventHandlerType.GetMethod("Invoke");
                ParameterInfo[] parameters = method.GetParameters();
                return parameters.Length == 2 && typeof(object).IsAssignableFrom(parameters[0].ParameterType) && typeof(EventArgs).IsAssignableFrom(parameters[1].ParameterType);
            }
            return false;
        }

        private void UnregisterEvent(object obj, string eventName)
        {
            if (string.Compare(eventName, "Loaded", StringComparison.Ordinal) == 0)
            {
                FrameworkElement frameworkElement = obj as FrameworkElement;
                if (frameworkElement != null)
                {
                    this.UnregisterLoaded(frameworkElement);
                    return;
                }
            }
            else
            {
                this.UnregisterEventImpl(obj, eventName);
            }
        }

        private void UnregisterEventImpl(object obj, string eventName)
        {
            Type type = obj.GetType();
            if (this.eventHandlerMethodInfo == null)
            {
                return;
            }
            EventInfo @event = type.GetEvent(eventName);
            @event.RemoveEventHandler(obj, Delegate.CreateDelegate(@event.EventHandlerType, this, this.eventHandlerMethodInfo));
            this.eventHandlerMethodInfo = null;
        }

        private void OnEventImpl(object sender, EventArgs eventArgs)
        {
            this.OnEvent(eventArgs);
        }

        internal void OnEventNameChanged(string oldEventName, string newEventName)
        {
            if (base.AssociatedObject != null)
            {
                FrameworkElement frameworkElement = this.Source as FrameworkElement;
                if (frameworkElement != null && string.Compare(oldEventName, "Loaded", StringComparison.Ordinal) == 0)
                {
                    this.UnregisterLoaded(frameworkElement);
                }
                else if (!string.IsNullOrEmpty(oldEventName))
                {
                    this.UnregisterEvent(this.Source, oldEventName);
                }
                if (frameworkElement != null && string.Compare(newEventName, "Loaded", StringComparison.Ordinal) == 0)
                {
                    this.RegisterLoaded(frameworkElement);
                    return;
                }
                if (!string.IsNullOrEmpty(newEventName))
                {
                    this.RegisterEvent(this.Source, newEventName);
                }
            }
        }

        private Type sourceTypeConstraint;

        private bool isSourceChangedRegistered;

        private NameResolver sourceNameResolver;

        private MethodInfo eventHandlerMethodInfo;

        public static readonly DependencyProperty SourceObjectProperty = DependencyProperty.Register("SourceObject", typeof(object), typeof(EventTriggerBase), new PropertyMetadata(new PropertyChangedCallback(EventTriggerBase.OnSourceObjectChanged)));

        public static readonly DependencyProperty SourceNameProperty = DependencyProperty.Register("SourceName", typeof(string), typeof(EventTriggerBase), new PropertyMetadata(new PropertyChangedCallback(EventTriggerBase.OnSourceNameChanged)));
    }

    public abstract class EventTriggerBase<T> : EventTriggerBase where T : class
    {
        protected EventTriggerBase() : base(typeof(T))
        {
        }

        public new T Source
        {
            get
            {
                return (T)((object)base.Source);
            }
        }

        internal sealed override void OnSourceChangedImpl(object oldSource, object newSource)
        {
            base.OnSourceChangedImpl(oldSource, newSource);
            this.OnSourceChanged(oldSource as T, newSource as T);
        }

        protected virtual void OnSourceChanged(T oldSource, T newSource)
        {
        }
    }
}