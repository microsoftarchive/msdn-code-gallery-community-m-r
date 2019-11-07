using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyCompany.Common.EventBus.Tests
{
    [TestClass]
    public class EventBusTests
    {
        [TestMethod]
        public void LoadPluginsTest()
        {
            EventBusPluginLoader plugins = new EventBusPluginLoader();
            plugins.DynamicInitialize();
        }
    }
}
