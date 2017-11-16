using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using BcSoft.EDC.Surface.Domain;
using BcSoft.EDC.Surface.Domain.Configuration;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;


namespace BcSoft.EDC.Surface.Helper
{
    public class HttpServer
    {

        private static HttpServer m_HttpService;

        private HttpClient m_HttpClientService;

        public HttpClient HttpClientService
        {
            get { return m_HttpClientService; }
            set { m_HttpClientService = value; }
        }


        private HttpServer()
        {
            if (m_HttpClientService == null)
                m_HttpClientService = new HttpClient();
        }

        public static HttpServer CreateInstance()
        {
            if (m_HttpService == null)
                m_HttpService = new HttpServer();
            return m_HttpService;
        }

        public ObservableCollection<ProjectJson> GetProject()
        {
            ObservableCollection<ProjectJson> projects = new ObservableCollection<ProjectJson>();
            string serverAddress = ApplicationContext.Instance.SystemConfig.ServerAddress;
            if (string.IsNullOrEmpty(serverAddress))
                return projects;
            string url = serverAddress + @"bimdata/getProjects/admin";
            HttpClientService.GetAsync(url).ContinueWith((requestTask) =>
               {
                   HttpResponseMessage response = requestTask.Result;
                   response.EnsureSuccessStatusCode();
                   string projectJson = response.Content.ReadAsStringAsync().Result;
                   projectJson = projectJson.TrimStart('[');
                   projectJson = projectJson.TrimEnd(']');
                   string[] chars = { "}," };
                   string[] projectsArray = projectJson.Split(chars, StringSplitOptions.RemoveEmptyEntries);

                   foreach (string item in projectsArray)
                   {
                       string strProject = "";
                       if (item.Last() != '}')
                       {
                           strProject = item + "}";
                       }
                       else
                       {
                           strProject = item;
                       }

                       var project = Helper.JsonHelpercs.Deserialize<ProjectJson>(strProject);
                       projects.Add(project);
                   }
               }
           );

            return projects;
        }


        public async Task<ObservableCollection<ProjectJson>>GetProjects(string url)
        {
            ObservableCollection<ProjectJson> projects = new ObservableCollection<ProjectJson>();
            HttpResponseMessage response = await  HttpClientService.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string projectJson = response.Content.ReadAsStringAsync().Result;
            projectJson = projectJson.TrimStart('[');
            projectJson = projectJson.TrimEnd(']');
            string[] chars = { "}," };
            string[] projectsArray = projectJson.Split(chars, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in projectsArray)
            {
                string strProject = "";
                if (item.Last() != '}')
                {
                    strProject = item + "}";
                }
                else
                {
                    strProject = item;
                }

                var project = Helper.JsonHelpercs.Deserialize<ProjectJson>(strProject);
                projects.Add(project);
            }
            return projects;
        }
    }
}
