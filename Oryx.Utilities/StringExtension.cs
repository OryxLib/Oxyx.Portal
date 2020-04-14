using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Utilities
{
    public static class StringExtension
    {
        public static int ToInt(this string source)
        {
            int output;
            if (int.TryParse(source, out output))
            {
                return output;
            }
            return 0;
        }
    }
}
