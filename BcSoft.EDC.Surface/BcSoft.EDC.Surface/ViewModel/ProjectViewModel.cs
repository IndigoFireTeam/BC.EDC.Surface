using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using BcSoft.EDC.Surface.Model;
using BcSoft.EDC.Surface.Domain;
using BcSoft.EDC.Surface.Helper;
using DevExpress.Mvvm;
using BcSoft.EDC.Surface.Model.UI;

namespace BcSoft.EDC.Surface.ViewModel
{
    public class ProjectViewModel : DevExpress.Mvvm.ViewModelBase
    {
        public ProjectViewModel()
        {
            this.Init();
        }

        #region Commands
        public DelegateCommand<ProjectInfoModel> LoadProjectCommand { get; set; }
        #endregion

        #region Properties
        private ObservableCollection<ProjectInfoModel> m_Projects;
        public ObservableCollection<ProjectInfoModel> Projects
        {
            get
            {
                return m_Projects;
            }
            set
            {
                m_Projects = value;
                this.RaisePropertiesChanged("Projects");
            }
        }

        public ApplicationContext Context
        {
            get
            {
                return ApplicationContext.Instance;
            }
        }

        INavigationService NavigationService
        {
            get
            {
                return ServiceContainer.GetService<INavigationService>();
            }
        }
        #endregion

        #region Private Methods
        private void Init()
        {
            this.LoadProjects();
            this.InitCommand();
        }

        private void LoadProjects()
        {
            Projects = new ObservableCollection<ProjectInfoModel>();
            var projects = Context.ConfigurationHelper.Select<Project>("Select * From Project");
            if (projects != null)
            {
                foreach (var project in projects)
                {
                    var projectInfo = MapperHelper.Mapper<Project, ProjectInfoModel>(project);

                    Projects.Add(projectInfo);
                }
            }
        }
        
        private void InitCommand()
        {
            LoadProjectCommand = new DelegateCommand<ProjectInfoModel>(this.LoadProjectCommandExecute);
        }
        #endregion

        #region Command Methods
        private void LoadProjectCommandExecute(ProjectInfoModel projectInfo)
        {
            if (!Context.ProjectChanged(projectInfo))
            {
                return;
            }

            Navigation.Instance.RefreshNavgations(Navigation.Instance.PreviewNavigations);
            NavigationService.Navigate("EngineView");
        }
        #endregion
    }
}
