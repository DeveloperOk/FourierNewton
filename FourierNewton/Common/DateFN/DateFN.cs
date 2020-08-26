using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FourierNewton.Common.DateFN
{
    public class DateFN
    {

        public static string GetCurrentDate()
        {

            string currentDate = DateTime.Now.ToString(DateFNConstants.DateFormatFN);

            return currentDate;

        }

    }
}
