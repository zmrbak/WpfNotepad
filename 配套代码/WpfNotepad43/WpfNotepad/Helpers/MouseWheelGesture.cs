using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfNotepad.Helpers
{
    public class MouseWheelGesture : MouseGesture
    {
        public MouseWheelGesture() : base(MouseAction.WheelClick) { }
        public MouseWheelGesture(ModifierKeys modifierKeys) : base(MouseAction.WheelClick, modifierKeys) { }

        public WheelDirection Direction { set; get; }
        public static MouseWheelGesture CtrlUp => new MouseWheelGesture(ModifierKeys.Control) { Direction = WheelDirection.Up };
        public static MouseWheelGesture CtrlDown => new MouseWheelGesture(ModifierKeys.Control) { Direction = WheelDirection.Down };

        public override bool Matches(object targetElement, InputEventArgs inputEventArgs)
        {
            if (base.Matches(targetElement, inputEventArgs) == false)
            {
                return false;
            }

            if(inputEventArgs is MouseWheelEventArgs ==false)
            {
                return false;
            }

            var arg = inputEventArgs as MouseWheelEventArgs;
            switch (Direction)
            {
                case WheelDirection.None:
                    return arg.Delta == 0;
                case WheelDirection.Up:
                    return arg.Delta > 0;
                case WheelDirection.Down:
                    return arg.Delta < 0;
                default:
                    return false;
            }
        }
    }

    public enum WheelDirection
    {
        None,
        Up,
        Down
    }
}
