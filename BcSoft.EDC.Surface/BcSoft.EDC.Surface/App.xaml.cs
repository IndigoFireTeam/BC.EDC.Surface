using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Threading;
using BcSoft.EDC.Surface.Helper;
using System.Diagnostics;

namespace BcSoft.EDC.Surface
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        System.Threading.Mutex mutex;        

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            bool ret;
            mutex = new Mutex(true, "BcSoft.EDC.Surface", out ret);
            if (!ret)
            {
                MessageBox.Show("已经有一个程序正在运行！");
                Environment.Exit(0);
            }

            //启动本地GIS服务
            if(GisServiceHelper.Instance.EnableLocalGISService())
            {
                GisServiceHelper.Instance.StartLocalGISService();
            }
        }
    }
}
