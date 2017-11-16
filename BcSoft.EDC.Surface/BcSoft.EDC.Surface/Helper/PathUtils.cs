using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using BcSoft.EDC.Surface.Model;

namespace BcSoft.EDC.Surface.Helper
{
    public class PathUtils
    {
        private static string Configuration_DB_Name = @"Configuration\Configuration.db";
        private static string Project_DB_Name = @"DataBase\Project.db";
        private static string Scene_Directory = "SceneCache";
        private static string Document_Directory = "Doc";

        public static string GetWorkDirectory()
        {
            return Environment.CurrentDirectory;
        }

        public static string GetTempDirectory(string appendDirectory = null)
        {
            var tempPath = Path.GetTempPath();

            if (appendDirectory==null)
            {
                return tempPath;
            }
            else
            {
                return Path.Combine(tempPath, appendDirectory); 
            }
        }

        public static string GetConfigurationDBPath()
        {
            var workDirectory = GetWorkDirectory();
            var configurationDBPath = Path.Combine(workDirectory, Configuration_DB_Name);

            return configurationDBPath;
        }

        public static string GetProjectDBPath(string projectGuid)
        {
            var tempDirectory = GetTempDirectory();
            var projectDBPath = Path.Combine(tempDirectory, projectGuid, Project_DB_Name);

            return projectDBPath;
        }

        public static string GetProjectPackage(string projectGuid)
        {
            var workDirectory = GetWorkDirectory();
            var packagePath = string.Format(@"Datas\{0}.zip", projectGuid);
            var packageFullPath = Path.Combine(workDirectory, packagePath);

            return packageFullPath;
        }

        public static IEnumerable<SceneModel> SetScenesPath(string projectGuid, IEnumerable<SceneModel> sceneModels)
        {
            var tempDirectory = GetTempDirectory();
            var scenePath = Path.Combine(tempDirectory, projectGuid, Scene_Directory);

            foreach (var sceneModel in sceneModels)
            {
                var existedFileName = Path.Combine(scenePath, sceneModel.Id, "total.ive");
                if (File.Exists(existedFileName))
                {
                    sceneModel.ScenePath = existedFileName;
                }
            }

            return sceneModels;
        }

        public static string GetDocumentPath(string projectGuid, string fileName)
        {
            var tempDirectory = GetTempDirectory();
            var documentPath = Path.Combine(tempDirectory, projectGuid, Document_Directory, fileName);
            if(File.Exists(documentPath))
            {
                return documentPath;
            }

            return string.Empty;
        }
    }
}
