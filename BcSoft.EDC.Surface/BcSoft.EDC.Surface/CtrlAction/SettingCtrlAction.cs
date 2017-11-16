using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BcSoft.EDC.Surface.ViewModel;
using BcSoft.EDC.Surface.Model.UI;

namespace BcSoft.EDC.Surface.CtrlAction
{
    public class SettingCtrlAction : ICtrlAction
    {
        public IServiceContainer ServiceContainer { get; set; }

        public void Run(ViewModelBase parentViewModel)
        {
            Navigation.Instance.RefreshNavgations(Navigation.Instance.SettingNavigations);

            var navigationService = ServiceContainer.GetService<INavigationService>();
            navigationService.Navigate("SettingView", null, parentViewModel);
        }
    }
}
