using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Collections.ObjectModel;

namespace BcSoft.EDC.Surface.Model.UI
{
    [XmlRoot("Navigation")]
    public class Navigation
    {
        private const string Navigation_Config_Path = @"Configuration\Navigation.xml";

        public static Navigation Instance { get; private set; }

        static Navigation()
        {
            Instance = Navigation.LoadNavigation();
        }
        
        public Navigation()
        {
            CurrentNavigations = new ObservableCollection<NavButtonModel>();
        }

        public List<NavButtonModel> HomeNavigations { get; set; }

        public List<NavButtonModel> SettingNavigations { get; set; }

        public List<NavButtonModel> PreviewNavigations { get; set; }

        [XmlIgnore()]
        public ObservableCollection<NavButtonModel> CurrentNavigations { get; private set; }

        #region Private Methods
        private static Navigation LoadNavigation()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Navigation));

            using (FileStream fileStream = new FileStream(Navigation_Config_Path, FileMode.Open))
            {
              return  serializer.Deserialize(fileStream) as Navigation;
            }
        }
        #endregion

        #region Public Methods
        public void RefreshNavgations(List<NavButtonModel> navigations)
        {
            CurrentNavigations.Clear();

            if (navigations != null)
            {
                foreach (var navigation in navigations)
                {
                    CurrentNavigations.Add(navigation);
                }
            }
        }
        #endregion
    }
}
