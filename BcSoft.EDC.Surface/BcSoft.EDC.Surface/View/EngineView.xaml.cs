using System.Windows;
using BcSoft.EDC.Surface.Helper;
using BcSoft.EDC.Surface.Domain;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BcSoft.EDC.Surface.Model;
using AxBcUgOcxLib;
using BcSoft.EDC.Surface.ViewModel;

namespace BcSoft.EDC.Surface.View
{
    /// <summary>
    /// EngineView.xaml 的交互逻辑
    /// </summary>
    public partial class EngineView : DevExpress.Xpf.WindowsUI.NavigationPage
    {
        public EngineView()
        {
            InitializeComponent();

            this.m_WinFormHost.Child = EngineHelper.Instance.EngineView;
            this.m_WinFormHost.Loaded += m_WinFormHost_Loaded;
            this.m_WinFormHost.Unloaded += m_WinFormHost_Unloaded;

            this.InitExerciser();
        }

        #region Properties
        public ApplicationContext Context
        {
            get
            {
                return ApplicationContext.Instance;
            }
        }
        #endregion

        #region Private Methods
        private void SetExerciserLocation()
        {
            m_ExerciserContainer.IsOpen = true;
            m_ExerciserContainer.StaysOpen = true;

            m_ExerciserContainer.VerticalOffset = this.ActualHeight;
        }

        private void InitExerciser()
        {
            this.SetExerciserLocation();
            this.m_Exerciser.SendXYAct += (x, y) =>
              {
                  EngineHelper.Instance.Move(-x, -y);
              };
        }
        #endregion

        #region Event Methods
        private void m_WinFormHost_Loaded(object sender, RoutedEventArgs e)
        {
            EngineHelper.Instance.CreateEarthScene();
            EngineHelper.Instance.RegisterSelectionSetChangedCallback(this.SelectionSetChanged);

            this.SetExerciserLocation();
        }

        private void m_WinFormHost_Unloaded(object sender, RoutedEventArgs e)
        {
            EngineHelper.Instance.ClearScene();//退出地球场景是清楚当前工程模型
            EngineHelper.Instance.InitEarthViewPoint();//初始化地球场景视点

            m_ExerciserContainer.IsOpen = false;
        }

        private void SelectionSetChanged(string tags)
        {
            PropertyView propertyView = new PropertyView(tags);
            propertyView.ShowDialog();
        }
        #endregion
    }
}
