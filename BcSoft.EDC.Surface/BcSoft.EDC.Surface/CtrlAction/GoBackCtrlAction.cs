using BcSoft.EDC.Surface.Model.UI;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BcSoft.EDC.Surface.Helper;

namespace BcSoft.EDC.Surface.CtrlAction
{
    public class GoBackCtrlAction: ICtrlAction
    {
        public IServiceContainer ServiceContainer { get; set; }

        public void Run(ViewModelBase parentViewModel)
        {
            SideFloatHelper.Instance.Service.Hide();

            Navigation.Instance.RefreshNavgations(Navigation.Instance.HomeNavigations);
            var navigationService = ServiceContainer.GetService<INavigationService>();
            navigationService.GoBack();
        }
    }
}
