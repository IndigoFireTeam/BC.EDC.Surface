using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using BcSoft.EDC.Surface.Domain.Json;
using BcSoft.EDC.Surface.Domain;
using System.IO;
using DevExpress.Mvvm;

namespace BcSoft.EDC.Surface.ViewModel
{
    public class DownLoadProjectViewModel: DevExpress.Mvvm.BindableBase
    {
        private ObservableCollection<Project> m_ProjectInfos;

        public ObservableCollection<Project> ProjectInfos
        {
            get { return m_ProjectInfos; }
            set { m_ProjectInfos = value; }
        }

        private Project m_SelectedProject;

        public Project SelectedProject
        {
            get { return m_SelectedProject; }
            set
            {
                m_SelectedProject = value;
                RaisePropertyChanged("SelectedProject");
            }
        }

        public DelegateCommand DownLoadProjectCommand { get; set; }
        

        public DownLoadProjectViewModel()
        {
            ProjectInfos = new ObservableCollection<Project>();
            DownLoadProjectCommand = new DelegateCommand(DownLoadProjectExcute);
            InitData();
           


        }

        private void DownLoadProjectExcute()
        {
            if(SelectedProject == null)
            {
                System.Windows.Forms.MessageBox.Show("请选中工程!");
                return;
            }

            string serverAddress = ApplicationContext.Instance.SystemConfig.ServerAddress;
            if (string.IsNullOrEmpty(serverAddress))
               return;
            string url = serverAddress + @"/bimdata/downLoad/"+SelectedProject.Guid;
            Helper.HttpServer.CreateInstance().DownLoadFile(url);
        }
       async private void InitData()
        {
            string serverAddress = ApplicationContext.Instance.SystemConfig.ServerAddress;
            if (string.IsNullOrEmpty(serverAddress))
                return ;
            string url = serverAddress + @"/bimdata/getProjects/lizhen01";
            List<ProjectJson> projects = await Helper.HttpServer.CreateInstance().AsyncGetProjects(url);

            foreach (var item in projects)
            {
                Project pj = new Project();
                pj.Guid = item.project_id;
                pj.Name = item.project_name;
                pj.PreviewImage = @"Images\Project_Default.png";
                ProjectInfos.Add(pj);
            }

            //url = serverAddress + @"bimdata/downLoad/054932ff-f6ac-475f-99e5-46f20cdf00ef";


            //System.IO.Stream stream = await Helper.HttpServer.CreateInstance().AsyncDownLoadData(url);
            //if(stream!=null)
            //{
            //    string fileName = @"E:\pdata\054932ff-f6ac-475f-99e5-46f20cdf00ef.zip";
            //    if(File.Exists(fileName))
            //    {
            //        File.Delete(fileName);
                   
            //    }
            //   // File.Create(fileName);
            //    byte[] bytes = new byte[stream.Length];
            //    stream.Read(bytes, 0, bytes.Length);

            //    stream.Seek(0, SeekOrigin.Begin);
               
            //    FileStream fs = new FileStream(fileName, FileMode.Create);
            //    BinaryWriter bw = new BinaryWriter(fs);
            //    bw.Write(bytes);
            //    bw.Close();
            //    fs.Close();
            //}
        }
    }
}
