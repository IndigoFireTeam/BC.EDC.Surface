using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using BcSoft.EDC.Surface.Helper;
using System.Windows.Controls.Primitives;

namespace BcSoft.EDC.Surface.View
{
    /// <summary>
    /// SideFloatView.xaml 的交互逻辑
    /// </summary>
    public partial class SideFloatView : UserControl,ISideFloatService
    {
        #region Variables
        private string m_CurrentViewName;
        #endregion

        public SideFloatView()
        {
            InitializeComponent();

            this.RegisterSideFloatService();
            this.m_BtnBack.MouseLeftButtonDown += m_BtnBack_MouseLeftButtonDown;
        }

        #region Private Methods
        private void RegisterSideFloatService()
        {
            SideFloatHelper.Instance.RegisterSideFloatService(this);
        }

        private void ShowAnimation()
        {
            DoubleAnimation widthAnimation = new DoubleAnimation();
            widthAnimation.From = 0.0;
            widthAnimation.To = 350.0;
            widthAnimation.Duration = new Duration(new TimeSpan(0, 0, 0, 0, 300));
            
            m_PopupContainer.BeginAnimation(Popup.WidthProperty, widthAnimation);
        }

        private void HideAnimation()
        {
            DoubleAnimation widthAnimation = new DoubleAnimation();
            widthAnimation.From = 350.0;
            widthAnimation.To = 0.0;
            widthAnimation.Duration = new Duration(new TimeSpan(0, 0, 0, 0, 300));

            m_PopupContainer.BeginAnimation(Popup.WidthProperty, widthAnimation);
        }
        #endregion

        #region SideFloatService Members
        public void Hide()
        {
            m_CurrentViewName = string.Empty;

            if(m_PopupContainer.IsOpen)
            {
                m_PopupContainer.IsOpen = false;
            }
        }

        public void Show(string viewName, object view)
        {
            if(string.Equals(viewName,m_CurrentViewName))
            {
                if(!m_PopupContainer.IsOpen)
                {
                    m_PopupContainer.IsOpen = true;
                    m_PopupContainer.Height = this.ActualHeight;

                    this.ShowAnimation();
                }
            }
            else
            {
                m_PopupContainer.IsOpen = true;
                m_PopupContainer.Height = this.ActualHeight;

                m_ContentContainer.Content = view;
                m_CurrentViewName = viewName;

                this.ShowAnimation();
            }
        }
        #endregion

        #region Event Methods
        private void m_BtnBack_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
        }
        #endregion
    }
}
