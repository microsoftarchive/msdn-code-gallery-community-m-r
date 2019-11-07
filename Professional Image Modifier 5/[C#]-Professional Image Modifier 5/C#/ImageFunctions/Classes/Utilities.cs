using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageFunctions.Classes
{
    class Utilities
    {

        public static String BytesToString(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
        }

        // Convert byte[][] to string
        public static string ConvertToString(byte[][] p)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte[] b in p)
            {
                sb.Append(System.Text.Encoding.Default.GetString(b));
            }
            return sb.ToString();
        }
    }
}
