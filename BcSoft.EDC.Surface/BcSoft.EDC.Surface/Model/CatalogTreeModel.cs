using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace BcSoft.EDC.Surface.Model
{
    public class CatalogTreeModel : DevExpress.Mvvm.BindableBase
    {
        public CatalogTreeModel()
        {
            Children = new ObservableCollection<CatalogTreeModel>();
        }

        public string Guid { get; set; }
        public string Project_Id { get; set; }
        public string Name { get; set; }
        public string Parent_Guid { get; set; }
        public string Globle_Id { get; set; }
        public string Type { get; set; }
        public string Scene_Id { get; set; }
        public string OriginPropertiesTree { get; set; }
        public CatalogTreeModel Parent { get; set; }
        public ObservableCollection<CatalogTreeModel> Children { get; set; }


        private bool m_IsHide;
        public bool IsHide
        {
            get
            {
                return m_IsHide;
            }
            set
            {
                m_IsHide = true;
                this.RaisePropertiesChanged("IsHide");
            }
        }
    }
}
