using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BcSoft.EDC.Surface.Model.UI;
using DevExpress.Mvvm;
using BcSoft.EDC.Surface.CtrlAction;
using System.Collections.ObjectModel;

namespace BcSoft.EDC.Surface.ViewModel
{
    public class MainWindowViewModel : DevExpress.Mvvm.ViewModelBase
    {
        public MainWindowViewModel()
        {
            this.Init();
            this.InitCommand();
        }

        #region Properties
        INavigationService NavigationService
        {
            get
            {
                return ServiceContainer.GetService<INavigationService>();
            }
        }

        private ObservableCollection<NavButtonModel> m_Navigations;
        public ObservableCollection<NavButtonModel> Navigations
        {
            get
            {
                return m_Navigations;
            }
            set
            {
                m_Navigations = value;
                this.RaisePropertiesChanged("Navigations");
            }
        }
        #endregion

        #region Commands
        public  DelegateCommand<NavButtonModel> NavigationCommand { get; set; }
        public DelegateCommand ViewLoadedCommand { get; set; }
        #endregion

        #region Private Methods
        private void Init()
        {
            Navigations = Navigation.Instance.CurrentNavigations;

            Navigation.Instance.RefreshNavgations(Navigation.Instance.HomeNavigations);
        }

        private void InitCommand()
        {
            this.NavigationCommand = new DelegateCommand<NavButtonModel>(this.NavigationCommandExecute);
            this.ViewLoadedCommand = new DelegateCommand(this.ViewLoadedCommandExecute);
        }
        #endregion

        #region Command Methods
        private void NavigationCommandExecute(NavButtonModel parameter)
        {
            if (parameter == null || string.IsNullOrEmpty(parameter.CtrlAction))
            {
                return;
            }

            var ctrlAction = CtrlActionFactory.Create(parameter.CtrlAction);
            if (ctrlAction == null)
            {
                return;
            }
            ctrlAction.ServiceContainer = ServiceContainer;
            ctrlAction.Run(this);
        }

        private void ViewLoadedCommandExecute()
        {
            NavigationService.Navigate("ProjectView", null, this);
        }
        #endregion
    }
}
