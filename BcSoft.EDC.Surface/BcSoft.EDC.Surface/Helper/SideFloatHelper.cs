using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace BcSoft.EDC.Surface.Helper
{
    public class SideFloatHelper
    {
        public static SideFloatHelper Instance { get; private set; }
        static SideFloatHelper()
        {
            Instance = new SideFloatHelper();
        }

        #region Property
        public ISideFloatService Service { get; private set; }
        #endregion

        #region Public Methods
        public void RegisterSideFloatService(ISideFloatService sideFloatService)
        {
            Service = sideFloatService;
        }
        #endregion
    }

    public interface ISideFloatService
    {
        void Show(string viewName, object view);
        void Hide();
    }
}
