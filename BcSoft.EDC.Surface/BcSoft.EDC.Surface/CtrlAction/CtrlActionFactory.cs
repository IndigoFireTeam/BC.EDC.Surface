using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace BcSoft.EDC.Surface.CtrlAction
{
    public class CtrlActionFactory
    {
        public static ICtrlAction Create(string typeName)
        {
            var ctrlActionType = Type.GetType(typeName);
            if (ctrlActionType == null)
            {
                return null;
            }

            return Activator.CreateInstance(ctrlActionType) as ICtrlAction;
        }
    }
}
