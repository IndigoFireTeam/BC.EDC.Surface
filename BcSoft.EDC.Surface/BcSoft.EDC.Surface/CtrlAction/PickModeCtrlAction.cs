using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BcSoft.EDC.Surface.Helper;

namespace BcSoft.EDC.Surface.CtrlAction
{
    public class PickModeCtrlAction : ICtrlAction
    {
        public IServiceContainer ServiceContainer { get; set; }

        public void Run(ViewModelBase parentViewModel)
        {
            EngineHelper.Instance.SetPickMode();
        }
    }
}
