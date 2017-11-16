using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Mvvm;
using BcSoft.EDC.Surface.Helper;
using BcSoft.EDC.Surface.View;
using BcSoft.EDC.Surface.ViewModel;
using BcSoft.EDC.Surface.Domain;
using BcSoft.EDC.Surface.Model;

namespace BcSoft.EDC.Surface.CtrlAction
{
    public class ModelCtrlAction : ICtrlAction
    {
        public IServiceContainer ServiceContainer { get; set; }

        public void Run(ViewModelBase parentViewModel)
        {
            SideFloatHelper.Instance.Service.Show("SceneView", new SceneView() { DataContext = new SceneViewModel() });
            ApplicationContext.Instance.CurrentProject.LoadScene();
        }
    }
}
