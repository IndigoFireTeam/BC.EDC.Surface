using BcSoft.EDC.Surface.CtrlAction;
using BcSoft.EDC.Surface.Model.UI;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BcSoft.EDC.Surface.ViewModel
{
    public class SettingViewModel : DevExpress.Mvvm.ViewModelBase
    {
        public SettingViewModel()
        {
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
        #endregion

        #region Commands
        public DelegateCommand UserAccountCommand { get; set; }
        public DelegateCommand UpdateInfoCommand { get; set; }
        public DelegateCommand ContractCommand { get; set; }
        #endregion

        #region Private Methods

        private void InitCommand()
        {
            this.UserAccountCommand = new DelegateCommand(this.UserAccountCommandExecute);
            this.UpdateInfoCommand = new DelegateCommand(this.UpdateInfoCommandExecute);
            this.ContractCommand = new DelegateCommand(this.ContractCommandExecute);
        }
        #endregion

        #region Command Methods
        private void UserAccountCommandExecute()
        {
            NavigationService.Navigate("UserConfigurationView");
        }

        private void UpdateInfoCommandExecute()
        {
            NavigationService.Navigate("UpdateInfoView");
        }

        private void ContractCommandExecute()
        {
            NavigationService.Navigate("ContractView");
        }
        #endregion
    }
}
