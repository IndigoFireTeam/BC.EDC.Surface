using BcSoft.EDC.Surface.Domain.Configuration;
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
            //View.LoadProjectDataView view = new View.LoadProjectDataView();
            //view.ShowDialog();
            //List<ProjectJson> projects = Helper.HttpServer.CreateInstance().GetProject();
            //string serverAddress = ApplicationContext.Instance.SystemConfig.ServerAddress;
            //if (string.IsNullOrEmpty(serverAddress))
            //    return;
            //string url = serverAddress + @"bimdata/getProjects/admin";
            // System.Collections.ObjectModel.ObservableCollection<ProjectJson> projects =  Helper.HttpServer.CreateInstance().GetProjects(url);
        }
    }
}
