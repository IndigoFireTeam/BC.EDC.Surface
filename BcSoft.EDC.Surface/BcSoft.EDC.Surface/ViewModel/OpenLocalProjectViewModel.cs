using DevExpress.Mvvm;
using Microsoft.Win32;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BcSoft.EDC.Surface.ViewModel
{
    public class OpenLocalProjectViewModel:DevExpress.Mvvm.BindableBase
    {

        #region 私有字段
        private string m_FileName;
        private string m_FolderPath;
        
        #endregion

        #region 绑定属性
        private string m_FilePath;

        public string FilePath
        {
            get { return m_FilePath; }
            set
            {
                m_FilePath = value;
                this.RaisePropertyChanged("FilePath");
            }
        }


        private string m_ProjectName;

        public string ProjectName
        {
            get { return m_ProjectName; }
            set
            {
                m_ProjectName = value;
                this.RaisePropertyChanged("ProjectName");
            }
        }
        #endregion

        #region 命令
        public DelegateCommand OpenProjectZipCommand { get; set; }

        public DelegateCommand LoadProjectCommand { get; set; }
        #endregion

        #region 构造函数
        public OpenLocalProjectViewModel()
        {
            InitCommand();
        }
        #endregion

        #region 命令方法
        private void OpenProjectZipExcute()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "数据包|*.zip|数据包|*.rar";
            openFile.Multiselect = false;
            if (openFile.ShowDialog() == true)
            {
                FilePath = openFile.FileName;
                m_FileName = Path.GetFileNameWithoutExtension(FilePath);
                m_FolderPath = Helper.PathUtils.GetTempDirectory() + m_FileName;
              //  m_FileName = openFile.SafeFileName;
            }
        }

        private void LoadProjectExcute()
        {
            if(string.IsNullOrEmpty(ProjectName) || string.IsNullOrEmpty(ProjectName.Trim()))
            {
                System.Windows.Forms.MessageBox.Show("工程名称不能为空!");
                return;
            }
            if(IsIncludeErrorChar(ProjectName))
            {
                System.Windows.Forms.MessageBox.Show("存在特殊字符!");
                return;
            }
            if(Directory.Exists(m_FolderPath))
            {
               System.Windows.Forms.DialogResult result =  System.Windows.Forms.MessageBox.Show("文件已经存在，是否覆盖?", "提示", System.Windows.Forms.MessageBoxButtons.YesNo);

                if (result == System.Windows.Forms.DialogResult.No)
                    return;
            }

            Helper.CompressHelper.UnZip(FilePath, m_FolderPath, string.Empty);
            string sql = string.Format(@"insert into Project ([Name],[Guid],[PreviewImage]) values ('{0}','{1}','{2}')", ProjectName.Trim(),m_FileName, "Images\\Project_Default.png");
            bool b= ApplicationContext.Instance.ConfigurationHelper.Insert(sql);
            
            if(b==true)
            {
                System.Windows.Forms.MessageBox.Show("导入完毕!");
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("导入失败!");
                Directory.Delete(m_FolderPath);
            }


        }
        #endregion

        #region 私有方法
        private void InitCommand()
        {
            OpenProjectZipCommand = new DelegateCommand(OpenProjectZipExcute);
            LoadProjectCommand = new DelegateCommand(LoadProjectExcute);

        }

        private  bool IsIncludeErrorChar(string value)
        {
            bool result = false;
            if (string.IsNullOrEmpty(value))
            {
                return result;
            }
            List<string> errorChars = new List<string>() { "\"", "?", "/", "*", "|", "\\", ":", "//", "<", ",", ">" };
            foreach (var item in errorChars)
            {
                if (value.Contains(item))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
        #endregion
    }
}
