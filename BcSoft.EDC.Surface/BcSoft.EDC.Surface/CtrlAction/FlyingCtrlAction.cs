using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Mvvm;

namespace BcSoft.EDC.Surface.CtrlAction
{
    public class FlyingCtrlAction : ICtrlAction
    {
        public IServiceContainer ServiceContainer { get; set; }

        public void Run(ViewModelBase parentViewModel)
        {
            ApplicationContext.Instance.CurrentProject.Flying();
            //throw new NotImplementedException();
        }
    }
}
