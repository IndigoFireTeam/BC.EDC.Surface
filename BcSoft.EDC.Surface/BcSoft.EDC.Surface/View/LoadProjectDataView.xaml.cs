using DevExpress.Xpf.Core;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BcSoft.EDC.Surface.View
{
    /// <summary>
    /// LoadDataPakgeView.xaml 的交互逻辑
    /// </summary>
    public partial class LoadProjectDataView : DXWindow
    {
        public LoadProjectDataView()
        {
            InitializeComponent();
            this.Loaded += (sender, e) =>
            {
                this.m_LocalProjectView.Content = new OpenLocalProjectView();
                this.m_DownProjectView.Content = new DownLoadProjectView();
            };
        }
      
    }
}
