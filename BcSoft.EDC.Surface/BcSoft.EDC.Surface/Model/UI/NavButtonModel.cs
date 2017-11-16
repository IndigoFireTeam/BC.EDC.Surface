using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml.Serialization;

namespace BcSoft.EDC.Surface.Model.UI
{
    public class NavButtonModel
    {
        [XmlAttribute()]
        public string Text { get; set; }
        [XmlAttribute()]
        public string Glyph { get; set; }
        [XmlAttribute()]
        public HorizontalAlignment HorizontalAlignment { get; set; }
        [XmlAttribute()]
        public VerticalAlignment VerticalAlignment { get; set; }
        [XmlAttribute()]
        public bool AllowGlyphTheming { get; set; }
        [XmlAttribute()]
        public string CtrlAction { get; set; }
    }
}
