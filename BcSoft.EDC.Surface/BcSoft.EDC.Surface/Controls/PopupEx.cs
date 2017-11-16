using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interop;

namespace BcSoft.EDC.Surface.Controls
{
    public class PopupEx : Popup
    {
        [DllImport("user32.dll")]
        static extern IntPtr SetActiveWindow(IntPtr hwnd);
        static PopupEx()
        {
            EventManager.RegisterClassHandler(typeof(PopupEx),
                Popup.PreviewMouseDownEvent,
                new MouseButtonEventHandler(OnMouseDown),
                false);
        }

        private static void OnMouseDown(object sender,MouseButtonEventArgs e)
        {
            var hwndSource = PresentationSource.FromVisual(sender as UIElement) as HwndSource;
            if(hwndSource!=null)
            {
                SetActiveWindow(hwndSource.Handle);
            }
        }
    }
}
