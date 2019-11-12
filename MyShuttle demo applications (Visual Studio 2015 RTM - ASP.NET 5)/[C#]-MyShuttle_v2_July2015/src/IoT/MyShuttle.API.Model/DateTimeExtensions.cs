using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShuttle.API.Model
{
    public static class DateTimeExtensions
    {
        public static string ToDocumentDbString(this DateTime dt)
        {
            return string.Format("{0:D4}-{1:D2}-{2:D2}T{3:D2}:{4:D2}:{5:D2}",
                dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
        }

        public static DateTime FromDocumentDbDateTimeString(this string str)
        {
            var year = int.Parse(str.Substring(0, 4));
            var month = int.Parse(str.Substring(5, 2));
            var day = int.Parse(str.Substring(8, 2));
            var hour = int.Parse(str.Substring(11, 2));
            var minute = int.Parse(str.Substring(14, 2));
            var seconds = int.Parse(str.Substring(17, 2));

            return new DateTime(year, month, day, hour, minute, seconds);
        }
    }
}
