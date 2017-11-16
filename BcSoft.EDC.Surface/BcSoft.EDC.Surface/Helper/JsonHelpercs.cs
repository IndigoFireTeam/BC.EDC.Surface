using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BcSoft.EDC.Surface.Helper
{
    public class JsonHelpercs
    {
        public static T Deserialize<T>(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return default(T);
            }

            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }

        public static string Serialize(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
    }
}
