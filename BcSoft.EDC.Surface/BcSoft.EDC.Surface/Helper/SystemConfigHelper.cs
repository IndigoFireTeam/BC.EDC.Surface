using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BcSoft.EDC.Surface.Helper
{
    public class SystemConfigHelper
    {
        public static SystemConfigHelper Instance;
        string systemConfigXmlPath = "Configuration\\SystemConfig.xml";
        XmlDocument documnet;
        static SystemConfigHelper()
        {
            Instance = new SystemConfigHelper();
        }
        private SystemConfigHelper()
        {
            documnet = new XmlDocument();
            documnet.Load(systemConfigXmlPath);
        }
        /// <summary>
        /// 获得本地GIS服务的配置参数
        /// </summary>
        /// <param name="path">GIS数据路径</param>
        /// <param name="ip">服务的IP</param>
        /// <param name="port">服务的端口</param>
        /// <returns></returns>
        public bool ReadLocalGISServiceParamers(out string path,out string ip,out string port)
        {
            path = ip = port = "";
            XmlNode node = documnet.SelectSingleNode("SystemConfig/LocalGISService");
            if(node==null)
            { return false; }
            path = node.Attributes["path"].Value;
            ip = node.Attributes["ip"].Value;
            port = node.Attributes["port"].Value;
            return true;
        }
        /// <summary>
        /// 是否启用本地GIS服务
        /// </summary>
        /// <returns></returns>
        public bool EnableLocalGISService()
        {
            XmlNode node = documnet.SelectSingleNode("SystemConfig/LocalGISService");
            if (node == null)
            { return false; }
            return node.Attributes["Enable"].Value == "1";
        }
    }
}
