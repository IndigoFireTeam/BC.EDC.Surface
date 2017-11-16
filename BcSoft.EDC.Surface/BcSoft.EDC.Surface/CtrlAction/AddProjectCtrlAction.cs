using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BcSoft.EDC.Surface.CtrlAction
{
    public class AddProjectCtrlAction : ICtrlAction
    {
        public IServiceContainer ServiceContainer { get; set; }

        public void Run(ViewModelBase parentViewModel)
        {
            View.LoadProjectDataView view = new View.LoadProjectDataView();
            view.ShowDialog();   
        }
    }
}
