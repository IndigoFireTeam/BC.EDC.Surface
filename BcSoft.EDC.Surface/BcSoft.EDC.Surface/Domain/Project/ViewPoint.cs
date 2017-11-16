using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BcSoft.EDC.Surface.Domain
{
    public class ViewPoint
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Parent_Id { get; set; }
        public string Project_Id { get; set; }
        public string Type { get; set; }
    }
}
