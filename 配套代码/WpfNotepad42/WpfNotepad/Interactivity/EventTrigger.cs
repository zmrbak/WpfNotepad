using System.Windows;

namespace WpfNotepad.Interactivity
{
    public class EventTrigger : EventTriggerBase<object>
    {
        public EventTrigger()
        {
        }

        public EventTrigger(string eventName)
        {
            this.EventName = eventName;
        }

        public string EventName
        {
            get
            {
                return (string)base.GetValue(EventTrigger.EventNameProperty);
            }
            set
            {
                base.SetValue(EventTrigger.EventNameProperty, value);
            }
        }

        protected override string GetEventName()
        {
            return this.EventName;
        }

        private static void OnEventNameChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            ((EventTrigger)sender).OnEventNameChanged((string)args.OldValue, (string)args.NewValue);
        }

        public static readonly DependencyProperty EventNameProperty = DependencyProperty.Register("EventName", typeof(string), typeof(EventTrigger), new FrameworkPropertyMetadata("Loaded", new PropertyChangedCallback(EventTrigger.OnEventNameChanged)));
    }
}