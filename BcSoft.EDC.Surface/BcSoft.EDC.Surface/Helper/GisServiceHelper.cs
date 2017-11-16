using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BcSoft.EDC.Surface.Helper
{
    public class GisServiceHelper
    {
        public static GisServiceHelper Instance;

        static GisServiceHelper()
        {
            Instance = new GisServiceHelper();
        }
        /// <summary>
        /// 是否需要启动本地GIS服务
        /// </summary>
        /// <returns></returns>
        public bool EnableLocalGISService()
        {
            return SystemConfigHelper.Instance.EnableLocalGISService();
        }
        /// <summary>
        /// 启动本地GIS服务
        /// </summary>
        public void StartLocalGISService()
        {
            string db, ip, port;
            if (!SystemConfigHelper.Instance.ReadLocalGISServiceParamers(out db, out ip, out port))
            {
                return;
            }
            Action<string, string, string> act = _StartLocalGISService;
            act.BeginInvoke(db, ip, port, null, act);
        }
        private void _StartLocalGISService(string leveldb, string Ip, string Port)
        {
            GisDataService.BootServer(leveldb, Ip, Port);
        }

        class GisDataService
        {
            /// <summary>
            /// 引擎组提供的本地GIS发布小程序，leveldbhttp.dll依赖cpprest140_2_9.dll
            /// 两个动态库均需放在运行环境下
            /// </summary>
            /// <param name="szleveldb"></param>
            /// <param name="szIp"></param>
            /// <param name="szPort"></param>
            /// <returns></returns>
            [DllImport(@"leveldbhttp.dll")]
            public static extern int BootServer(string szleveldb, string szIp, string szPort);
        }
    }
}
