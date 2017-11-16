using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BcSoft.EDC.Surface.Helper
{
    public static class StringExtension
    {
        public static float RaiseFloat(this string value)
        {
            if(string.IsNullOrEmpty(value))
            {
                return 0.0f;
            }

            float raiseValue;
            if(float.TryParse(value,out raiseValue))
            {
                return raiseValue;
            }
            else
            {
                return 0.0f;
            }
        }

        public static int PaiseInt(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return 0;
            }

            int raiseValue;
            if (int.TryParse(value, out raiseValue))
            {
                return raiseValue;
            }
            else
            {
                return 0;
            }
        }
    }
}
