using System;
using System.Windows;

namespace WpfNotepad.Interactivity
{
    public class TriggerActionCollection : AttachableCollection<TriggerAction>
    {
        internal TriggerActionCollection()
        {
        }

        protected override void OnAttached()
        {
            foreach (TriggerAction triggerAction in this)
            {
                triggerAction.Attach(base.AssociatedObject);
            }
        }

        protected override void OnDetaching()
        {
            foreach (TriggerAction triggerAction in this)
            {
                triggerAction.Detach();
            }
        }

        internal override void ItemAdded(TriggerAction item)
        {
            if (item.IsHosted)
            {
                throw new InvalidOperationException(ExceptionStringTable.CannotHostTriggerActionMultipleTimesExceptionMessage);
            }
            if (base.AssociatedObject != null)
            {
                item.Attach(base.AssociatedObject);
            }
            item.IsHosted = true;
        }

        internal override void ItemRemoved(TriggerAction item)
        {
            if (((IAttachedObject)item).AssociatedObject != null)
            {
                item.Detach();
            }
            item.IsHosted = false;
        }

        protected override Freezable CreateInstanceCore()
        {
            return new TriggerActionCollection();
        }
    }
}