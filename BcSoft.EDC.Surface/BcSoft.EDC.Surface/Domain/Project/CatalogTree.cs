using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BcSoft.EDC.Surface.Domain
{
    public class CatalogTree
    {
        public string Guid { get; set; }
        public string Project_Id { get; set; }
        public string Name { get; set; }
        public string Parent_Guid { get; set; }
        public string Globle_Id { get; set; }
        public string Type { get; set; }
        public string Scene_Id { get; set; }
        public byte[] Property_Tree { get; set; }

        public string OriginPropertyTree
        {
            get
            {
                if (Property_Tree == null)
                {
                    return string.Empty;
                }

                return Encoding.UTF8.GetString(Property_Tree);
            }
        }
    }
}
