using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls.Primitives;
using BcSoft.EDC.Surface.Controls;
using BcSoft.EDC.Surface.View;
using System.Windows;
using System.Windows.Media;

namespace BcSoft.EDC.Surface.Helper
{
    public class DocumentViewerHelper
    {
        static readonly PopupEx ViewerContainer;

        static DocumentViewerHelper()
        {
            ViewerContainer = new PopupEx();
            ViewerContainer.AllowsTransparency = true;
            ViewerContainer.Placement = PlacementMode.Center;
            ViewerContainer.PopupAnimation = PopupAnimation.Slide;
        }

        public static void Show(string fileName,string filePath)
        {
            ViewerContainer.Child = new DocumentViewer(fileName, filePath);
            ViewerContainer.IsOpen = true;
            //解决Popup不能全屏的问题
            SetPopupWorkArea(ViewerContainer.Child);
        }

        public static void Close()
        {
            ViewerContainer.IsOpen = false;
        }

        #region Private Methods
        private static void SetPopupWorkArea(DependencyObject parent)
        {
            do
            {
                parent = VisualTreeHelper.GetParent(parent);
                if (parent != null
                    && parent.ToString() == "System.Windows.Controls.Primitives.PopupRoot")
                {
                    var element = parent as FrameworkElement;
                    element.Height = SystemParameters.PrimaryScreenHeight;
                    element.Width = SystemParameters.PrimaryScreenWidth;

                    break;
                }
            }
            while (parent != null);
        }
        #endregion
    }
}
