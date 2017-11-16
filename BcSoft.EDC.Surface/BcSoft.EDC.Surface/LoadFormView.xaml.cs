using DevExpress.Xpf.Core;
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
using System.Net.Http;
using System.Net.Http.Headers;

namespace BcSoft.EDC.Surface
{
    /// <summary>
    /// LoadFormView.xaml 的交互逻辑
    /// </summary>
    public partial class LoadFormView : DXWindow
    {
        public LoadFormView()
        {
            InitializeComponent();
        }

        private void LoadForm_Click(object sender, RoutedEventArgs e)
        {
            //if(string.IsNullOrEmpty(m_UserName.Text.Trim()))
            //{
            //    MessageBox.Show("用户名不能为空!");
            //    return;
            //}
            //if (string.IsNullOrEmpty(m_PassWord.Password.Trim()))
            //{
            //    MessageBox.Show("密码不能为空!");
            //    return;
            //}
            //HttpClient httpClient = new HttpClient();
            //httpClient.PostAsync("http://10.10.11.98:8084/bimdata/login",
            //    System.Net.Http.HttpContent);

            MainWindow window = new MainWindow();
            this.Close();
            window.Show();
        }
    }
}
