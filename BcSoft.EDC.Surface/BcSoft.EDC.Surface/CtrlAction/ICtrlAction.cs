using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Mvvm;

namespace BcSoft.EDC.Surface.CtrlAction
{
    public interface ICtrlAction
    {
        IServiceContainer ServiceContainer { get; set; }
        void Run(ViewModelBase parentViewModel);
    }
}
