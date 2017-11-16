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
using System.IO;

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

        //public void GetProject()
        //{
        //    string serverAddress = ApplicationContext.Instance.SystemConfig.ServerAddress;
        //    if (string.IsNullOrEmpty(serverAddress))
        //        return ;
        //    string url = serverAddress + @"bimdata/getProjects/admin";
        //    HttpClientService.GetAsync(url).ContinueWith((requestTask) =>
        //       {
        //           HttpResponseMessage response = requestTask.Result;
        //           response.EnsureSuccessStatusCode();
        //           string projectJson = response.Content.ReadAsStringAsync().Result;
        //           projectJson = projectJson.TrimStart('[');
        //           projectJson = projectJson.TrimEnd(']');
        //           string[] chars = { "}," };
        //           string[] projectsArray = projectJson.Split(chars, StringSplitOptions.RemoveEmptyEntries);

        //           foreach (string item in projectsArray)
        //           {
        //               string strProject = "";
        //               if (item.Last() != '}')
        //               {
        //                   strProject = item + "}";
        //               }
        //               else
        //               {
        //                   strProject = item;
        //               }

        //               var project = Helper.JsonHelpercs.Deserialize<ProjectJson>(strProject);
        //           }
        //       }
        //   );

        //}


        async public Task<int> LoginForm(string url, string userName, string passWord)
        {
            int state = 3;
            HttpContent postContent = new FormUrlEncodedContent(new Dictionary<string, string>()

            {
                {"username",userName },
                {"password" ,passWord}          
            });

            

            return state;
        }

        async public Task<List<ProjectJson>> AsyncGetProjects(string url)
        {
            List<ProjectJson> projects = new List<ProjectJson>();
            HttpResponseMessage response = await HttpClientService.GetAsync(url);
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

        public void DownLoadFile(string url)
        {
            // 创建一个异步GET请求，当请求返回时继续处理  
            HttpClientService.GetAsync(url).ContinueWith(
                (requestTask) =>
                {
                    HttpResponseMessage response = requestTask.Result;

                    // 确认响应成功，否则抛出异常  
                    // response.EnsureSuccessStatusCode();  

                    // 异步读取响应为字符串  
                    response.Content.DownloadAsFileAsync(@"E:\pdata\054932ff-f6ac-475f-99e5-46f20cdf00ef.zip", true).ContinueWith(
                        (readTask) => Console.WriteLine("文件下载完成！"));
                });
        }




    }
}
