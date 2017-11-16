using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace BcSoft.EDC.Surface.Model
{
    public class ViewPointModel : DevExpress.Mvvm.BindableBase
    {
        public ViewPointModel()
        {
            Children = new ObservableCollection<ViewPointModel>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Parent_Id { get; set; }
        public string Project_Id { get; set; }
        public string Type { get; set; }

        public ViewPointModel Parent { get; set; }
        public ObservableCollection<ViewPointModel> Children { get; set; }
    }
}
