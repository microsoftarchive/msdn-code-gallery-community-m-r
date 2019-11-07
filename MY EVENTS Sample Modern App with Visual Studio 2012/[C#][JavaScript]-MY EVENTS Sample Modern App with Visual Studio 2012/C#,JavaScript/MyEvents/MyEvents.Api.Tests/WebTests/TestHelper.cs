using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyEvents.Api.Tests.WebTests
{
    public static class TestHelper
    {
        public static void ValidateResult<T>(T expected, T actual, ManualResetEvent manualResetEvent, ref Exception exceptionResult)
        {
            try
            {
                Assert.AreEqual(expected, actual);
            }
            catch (Exception ex)
            {
                exceptionResult = ex;
            }
            finally
            {
                manualResetEvent.Set();
            }

        }

        public static void WaitAll(ManualResetEvent manualResetEvent, ref Exception exceptionResult)
        {
            Assert.IsTrue(manualResetEvent.WaitOne(10000));
            if (exceptionResult != null)
                throw exceptionResult;
        }
    }
}
