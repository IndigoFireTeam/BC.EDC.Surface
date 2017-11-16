using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BcSoft.EDC.Surface.Domain
{
    public class Document
    {
        public string Id { get; set; }
        public string File_Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Guid { get; set; }
        public string Project_Id { get; set; }
    }
}
