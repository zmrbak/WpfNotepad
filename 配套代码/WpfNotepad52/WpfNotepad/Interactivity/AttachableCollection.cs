using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;

namespace WpfNotepad.Interactivity
{
    public abstract class AttachableCollection<T> : FreezableCollection<T>, IAttachedObject where T : DependencyObject, IAttachedObject
    {
        protected DependencyObject AssociatedObject
        {
            get
            {
                base.ReadPreamble();
                return this.associatedObject;
            }
        }

        internal AttachableCollection()
        {
            ((INotifyCollectionChanged)this).CollectionChanged += this.OnCollectionChanged;
            this.snapshot = new Collection<T>();
        }

        protected abstract void OnAttached();

        protected abstract void OnDetaching();

        internal abstract void ItemAdded(T item);

        internal abstract void ItemRemoved(T item);

        [Conditional("DEBUG")]
        private void VerifySnapshotIntegrity()
        {
            bool flag = base.Count == this.snapshot.Count;
            if (flag)
            {
                for (int i = 0; i < base.Count; i++)
                {
                    if (base[i] != this.snapshot[i])
                    {
                        return;
                    }
                }
            }
        }

        private void VerifyAdd(T item)
        {
            if (this.snapshot.Contains(item))
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, ExceptionStringTable.DuplicateItemInCollectionExceptionMessage, new object[]
                {
                    typeof(T).Name,
                    base.GetType().Name
                }));
            }
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:

                    {
                        IEnumerator enumerator = e.NewItems.GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            object obj = enumerator.Current;
                            T t = (T)((object)obj);
                            try
                            {
                                this.VerifyAdd(t);
                                this.ItemAdded(t);
                            }
                            finally
                            {
                                this.snapshot.Insert(base.IndexOf(t), t);
                            }
                        }
                        return;
                    }

                case NotifyCollectionChangedAction.Remove:
                    goto IL_13A;
                case NotifyCollectionChangedAction.Replace:
                    break;

                case NotifyCollectionChangedAction.Move:
                    return;

                case NotifyCollectionChangedAction.Reset:
                    goto IL_18D;
                default:
                    return;
            }
            foreach (object obj2 in e.OldItems)
            {
                T item = (T)((object)obj2);
                this.ItemRemoved(item);
                this.snapshot.Remove(item);
            }

            {
                IEnumerator enumerator3 = e.NewItems.GetEnumerator();
                while (enumerator3.MoveNext())
                {
                    object obj3 = enumerator3.Current;
                    T t2 = (T)((object)obj3);
                    try
                    {
                        this.VerifyAdd(t2);
                        this.ItemAdded(t2);
                    }
                    finally
                    {
                        this.snapshot.Insert(base.IndexOf(t2), t2);
                    }
                }
                return;
            }
        IL_13A:

            {
                IEnumerator enumerator4 = e.OldItems.GetEnumerator();
                while (enumerator4.MoveNext())
                {
                    object obj4 = enumerator4.Current;
                    T item2 = (T)((object)obj4);
                    this.ItemRemoved(item2);
                    this.snapshot.Remove(item2);
                }
                return;
            }
        IL_18D:
            foreach (T item3 in this.snapshot)
            {
                this.ItemRemoved(item3);
            }
            this.snapshot = new Collection<T>();
            foreach (T item4 in this)
            {
                this.VerifyAdd(item4);
                this.ItemAdded(item4);
            }
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
                    throw new InvalidOperationException();
                }
                if (Interaction.ShouldRunInDesignMode || !(bool)base.GetValue(DesignerProperties.IsInDesignModeProperty))
                {
                    base.WritePreamble();
                    this.associatedObject = dependencyObject;
                    base.WritePostscript();
                }
                this.OnAttached();
            }
        }

        public void Detach()
        {
            this.OnDetaching();
            base.WritePreamble();
            this.associatedObject = null;
            base.WritePostscript();
        }

        private Collection<T> snapshot;

        private DependencyObject associatedObject;
    }
}