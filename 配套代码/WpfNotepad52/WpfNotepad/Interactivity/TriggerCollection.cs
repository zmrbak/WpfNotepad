using System.Windows;

namespace WpfNotepad.Interactivity
{
    public sealed class TriggerCollection : AttachableCollection<TriggerBase>
    {
        internal TriggerCollection()
        {
        }

        protected override void OnAttached()
        {
            foreach (TriggerBase triggerBase in this)
            {
                triggerBase.Attach(base.AssociatedObject);
            }
        }

        protected override void OnDetaching()
        {
            foreach (TriggerBase triggerBase in this)
            {
                triggerBase.Detach();
            }
        }

        internal override void ItemAdded(TriggerBase item)
        {
            if (base.AssociatedObject != null)
            {
                item.Attach(base.AssociatedObject);
            }
        }

        internal override void ItemRemoved(TriggerBase item)
        {
            if (((IAttachedObject)item).AssociatedObject != null)
            {
                item.Detach();
            }
        }

        protected override Freezable CreateInstanceCore()
        {
            return new TriggerCollection();
        }
    }
}