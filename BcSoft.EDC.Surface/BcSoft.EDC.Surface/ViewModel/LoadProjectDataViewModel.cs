using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BcSoft.EDC.Surface.ViewModel
{
    public class LoadProjectDataViewModel:DevExpress.Mvvm.BindableBase
    {
        private object m_LoaclProjectDataView;

        public object LoaclProjectDataView
        {
            get { return m_LoaclProjectDataView; }
            set
            {
                m_LoaclProjectDataView = value;
                this.RaisePropertyChanged("LoaclProjectDataView");
            }
        }

        private object m_DownLoadProjectDataView;

        public object DownLoadProjectDataVie
        {
            get { return m_DownLoadProjectDataView; }
            set
            {
                m_DownLoadProjectDataView = value;
                this.RaisePropertyChanged("DownLoadProjectDataVie");
            }
        }

    }
}
