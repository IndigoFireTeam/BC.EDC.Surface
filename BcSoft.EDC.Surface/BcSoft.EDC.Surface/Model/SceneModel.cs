using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BcSoft.EDC.Surface.Model
{
    public class SceneModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Project_Id { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Altitude { get; set; }
        public string Rotate { get; set; }
        public string ScenePath { get; set; }
    }
}
