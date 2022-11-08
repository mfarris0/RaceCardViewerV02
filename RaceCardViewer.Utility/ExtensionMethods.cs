using System;
using System.Collections.Generic;
using System.Text;

namespace RaceCardViewer.Utility
{
    public static class ExtensionMethods
    {
        public static bool IsNumeric(this string value)
        {
            bool result = false;

            if (int.TryParse(value, out _))
                result = true;

            if (result == false)
                if (double.TryParse(value, out _)) result = true;

            return result;
        }

    }
}
