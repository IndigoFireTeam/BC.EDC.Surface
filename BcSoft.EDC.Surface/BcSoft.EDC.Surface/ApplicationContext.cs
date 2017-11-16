using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BcSoft.EDC.Surface.Helper;
using System.IO;
using BcSoft.EDC.Surface.Model;
using BcSoft.EDC.Surface.Domain;

namespace BcSoft.EDC.Surface
{
    public class ApplicationContext
    {
        public static ApplicationContext Instance { get; private set; }

        static ApplicationContext()
        {
            Instance = new ApplicationContext();
        }

        private ApplicationContext()
        {
            this.Init();
        }

        #region Property
        public DBHelper ProjectHelper { get; private set; }
        public DBHelper ConfigurationHelper { get; private set; }

        public ProjectInfoModel CurrentProject { get; private set; }

        public Config SystemConfig { get; private set; }
        #endregion

        #region Private Methods
        private void Init()
        {
            this.InitConfigurationHelper();
            this.InitSystemConfig();
        }

        private void InitConfigurationHelper()
        {
            var configurationDbPath = PathUtils.GetConfigurationDBPath();
            ConfigurationHelper = DBHelper.Create(configurationDbPath);
        }

        private void InitSystemConfig()
        {
            SystemConfig = ConfigurationHelper.SelectSingle<Config>("Select * from Config");
        }
        #endregion

        #region Public Methods
        public bool ProjectChanged(ProjectInfoModel projectInfo)
        {
            CurrentProject = null;
            ProjectHelper = null;

            if (projectInfo == null)
            {
                return false;
            }

            var projectDbPath = PathUtils.GetProjectDBPath(projectInfo.Guid);
            if(!File.Exists(projectDbPath))
            {
                return false;
            }

            CurrentProject = projectInfo;
            CurrentProject.IsLoaded = false;

            ProjectHelper = DBHelper.Create(projectDbPath);

            return true;
        }
        #endregion
    }
}
