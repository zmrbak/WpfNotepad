using System;
using System.Windows;

namespace WpfNotepad.Interactivity
{
    internal sealed class NameResolver
    {
        public event EventHandler<NameResolvedEventArgs> ResolvedElementChanged;

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                DependencyObject @object = this.Object;
                this.name = value;
                this.UpdateObjectFromName(@object);
            }
        }

        public DependencyObject Object
        {
            get
            {
                if (string.IsNullOrEmpty(this.Name) && this.HasAttempedResolve)
                {
                    return this.NameScopeReferenceElement;
                }
                return this.ResolvedObject;
            }
        }

        public FrameworkElement NameScopeReferenceElement
        {
            get
            {
                return this.nameScopeReferenceElement;
            }
            set
            {
                FrameworkElement oldNameScopeReference = this.NameScopeReferenceElement;
                this.nameScopeReferenceElement = value;
                this.OnNameScopeReferenceElementChanged(oldNameScopeReference);
            }
        }

        private FrameworkElement ActualNameScopeReferenceElement
        {
            get
            {
                if (this.NameScopeReferenceElement == null || !Interaction.IsElementLoaded(this.NameScopeReferenceElement))
                {
                    return null;
                }
                return this.GetActualNameScopeReference(this.NameScopeReferenceElement);
            }
        }

        private DependencyObject ResolvedObject { get; set; }

        private bool PendingReferenceElementLoad { get; set; }

        private bool HasAttempedResolve { get; set; }

        private void OnNameScopeReferenceElementChanged(FrameworkElement oldNameScopeReference)
        {
            if (this.PendingReferenceElementLoad)
            {
                oldNameScopeReference.Loaded -= this.OnNameScopeReferenceLoaded;
                this.PendingReferenceElementLoad = false;
            }
            this.HasAttempedResolve = false;
            this.UpdateObjectFromName(this.Object);
        }

        private void UpdateObjectFromName(DependencyObject oldObject)
        {
            DependencyObject resolvedObject = null;
            this.ResolvedObject = null;
            if (this.NameScopeReferenceElement != null)
            {
                if (!Interaction.IsElementLoaded(this.NameScopeReferenceElement))
                {
                    this.NameScopeReferenceElement.Loaded += this.OnNameScopeReferenceLoaded;
                    this.PendingReferenceElementLoad = true;
                    return;
                }
                if (!string.IsNullOrEmpty(this.Name))
                {
                    FrameworkElement actualNameScopeReferenceElement = this.ActualNameScopeReferenceElement;
                    if (actualNameScopeReferenceElement != null)
                    {
                        resolvedObject = (actualNameScopeReferenceElement.FindName(this.Name) as DependencyObject);
                    }
                }
            }
            this.HasAttempedResolve = true;
            this.ResolvedObject = resolvedObject;
            if (oldObject != this.Object)
            {
                this.OnObjectChanged(oldObject, this.Object);
            }
        }

        private void OnObjectChanged(DependencyObject oldTarget, DependencyObject newTarget)
        {
            if (this.ResolvedElementChanged != null)
            {
                this.ResolvedElementChanged(this, new NameResolvedEventArgs(oldTarget, newTarget));
            }
        }

        private FrameworkElement GetActualNameScopeReference(FrameworkElement initialReferenceElement)
        {
            FrameworkElement frameworkElement = initialReferenceElement;
            if (this.IsNameScope(initialReferenceElement))
            {
                frameworkElement = ((initialReferenceElement.Parent as FrameworkElement) ?? frameworkElement);
            }
            return frameworkElement;
        }

        private bool IsNameScope(FrameworkElement frameworkElement)
        {
            FrameworkElement frameworkElement2 = frameworkElement.Parent as FrameworkElement;
            if (frameworkElement2 != null)
            {
                object obj = frameworkElement2.FindName(this.Name);
                return obj != null;
            }
            return false;
        }

        private void OnNameScopeReferenceLoaded(object sender, RoutedEventArgs e)
        {
            this.PendingReferenceElementLoad = false;
            this.NameScopeReferenceElement.Loaded -= this.OnNameScopeReferenceLoaded;
            this.UpdateObjectFromName(this.Object);
        }

        private string name;

        private FrameworkElement nameScopeReferenceElement;
    }
}