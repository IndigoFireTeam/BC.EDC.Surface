using BcSoft.EDC.Surface.Helper;
using BcSoft.EDC.Surface.View;
using BcSoft.EDC.Surface.ViewModel;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BcSoft.EDC.Surface.CtrlAction
{
    public class DocumentCtrlAction : ICtrlAction
    {
        public IServiceContainer ServiceContainer { get; set; }

        public void Run(ViewModelBase parentViewModel)
        {
            SideFloatHelper.Instance.Service.Show("DocumentView", new DocumentView() { DataContext = new DocumentViewModel() });
        }
    }
}
