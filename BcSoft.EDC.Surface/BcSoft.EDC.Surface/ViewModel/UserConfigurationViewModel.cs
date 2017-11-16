using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Windows;

namespace BcSoft.EDC.Surface.ViewModel
{
    public class UserConfigurationViewModel : DevExpress.Mvvm.ViewModelBase
    {

        #region 绑定属性
        private string m_DeviceCode;

        public string DeviceCode
        {
            get { return m_DeviceCode; }
            set
            {
                m_DeviceCode = value;
                RaisePropertyChanged("DeviceCode");
            }
        }

        private string m_ServerAddress;

        public string ServerAddress
        {
            get { return m_ServerAddress; }
            set
            {
                m_ServerAddress = value;
                RaisePropertyChanged("ServerAddress");
            }
        }
        private string m_DeviceState = "未激活";

        public string DeviceState
        {
            get { return m_DeviceState; }
            set
            {
                m_DeviceState = value;
                RaisePropertyChanged("DeviceState");

            }
        }

        private string m_UserName;

        public string UserName
        {
            get { return m_UserName; }
            set
            {
                m_UserName = value;
                RaisePropertyChanged("UserName");
            }
        }

        private string m_LoginState;

        public string LoginState
        {
            get { return m_LoginState; }
            set
            {
                m_LoginState = value;
                RaisePropertyChanged("LoginState");
            }
        }

        private string m_PassWord;

        public string PassWord
        {
            get { return m_PassWord; }
            set
            {
                m_PassWord = value;
                RaisePropertyChanged("PassWord");
            }
        }

        #endregion

        #region 命令
        public DelegateCommand ActivateDeviceCommand { get; set; }
        public DelegateCommand LoginUserCommand { get; set; }
        
        #endregion


        #region 构造
        public UserConfigurationViewModel()
        {
            ActivateDeviceCommand = new DelegateCommand(ActivateDeviceExcute);
            LoginUserCommand = new DelegateCommand(LoginUserExcute);
            InitDeviceCode();
            ServerAddress = ApplicationContext.Instance.SystemConfig.ServerAddress;
        }
        #endregion


        #region 命令方法
        private void ActivateDeviceExcute()
        {
            if(ServerAddress != ApplicationContext.Instance.SystemConfig.ServerAddress)
            {
                ApplicationContext.Instance.SystemConfig.ServerAddress = ServerAddress;
                string sql = string.Format(@"UPDATE Config SET ServerAddress = '{0}'", ServerAddress);
                ApplicationContext.Instance.ConfigurationHelper.Update(sql);
            }
        }

        private void LoginUserExcute()
        {
            if (!IsVaildCheck())
                return;


            
        }
        #endregion

        #region 私有方法
        private void InitDeviceCode()
        {
            string mac = "";
            try
            {
                ManagementClass mClass = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mClass.GetInstances();
                foreach (ManagementObject item in moc)
                {
                    if ((bool)item["IPEnabled"] == true)
                    {
                        mac = item["MacAddress"].ToString();
                        break;
                    }
                }
                moc = null;
                mClass = null;
                DeviceCode = mac;
            }
            catch (Exception ex )
            {
                
            }
        }

        private bool IsVaildCheck()
        {
            if (string.IsNullOrEmpty(DeviceState) || DeviceState != "已激活")
            {
                MessageBox.Show("请激活设备!");
                return false;
            }

            if (string.IsNullOrEmpty(UserName.Trim()))
            {
                MessageBox.Show("用户名不能为空!");
                return false;
            }
            if (string.IsNullOrEmpty(UserName.Trim()))
            {
                MessageBox.Show("用户名不能为空!");
                return false;
            }
            return true;
        }
        #endregion




    }
}
