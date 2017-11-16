using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BcSoft.EDC.Surface.Model
{
    public class PropertyModel
    {
        public string i { get; set; }
        public string k { get; set; }
        public string v { get; set; }
        public List<PropertyModel> c { get; set; }
    }
}
