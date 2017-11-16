using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BcSoft.EDC.Surface.Helper;

namespace BcSoft.EDC.Surface.ViewModel
{
    public class EngineViewModel : DevExpress.Mvvm.ViewModelBase
    {
        public EngineViewModel()
        {
            
        }

        #region Property
        public ApplicationContext Context
        {
            get
            {
                return ApplicationContext.Instance;
            }
        }
        #endregion
    }
}
