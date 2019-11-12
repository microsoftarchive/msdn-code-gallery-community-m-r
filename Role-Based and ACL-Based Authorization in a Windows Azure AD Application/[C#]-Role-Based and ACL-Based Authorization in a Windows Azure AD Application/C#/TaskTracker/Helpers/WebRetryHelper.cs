using System;
using System.Globalization;
using System.IO;

namespace TaskTracker
{
    public class WebRetryHelper<T>
    {
        public T Value { get; private set; }
        public const int maxTries = 4;
        public const int baseInterval = 400;
        
        public WebRetryHelper(Func<T> func)
        {
            int retryInterval = baseInterval;
            for(int i = 0; i < maxTries; i ++)
            {
                try
                {
                    Value = func();
                    return;
                }
                catch(Exception ex)
                {
                    TextWriter tw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "log.txt", true);
                    tw.Write("\r\nLog Entry : ");
                    tw.WriteLine(DateTime.Now.ToLongTimeString() + " " + DateTime.Now.ToLongDateString() + ": " + ex.ToString());
                    tw.Flush();
                    tw.Close();
                }

                System.Threading.Thread.Sleep(retryInterval);
                retryInterval *=2;
            }
            throw new ApplicationException(String.Format(CultureInfo.InvariantCulture, "Exhausted retries"));
        }

    }
}