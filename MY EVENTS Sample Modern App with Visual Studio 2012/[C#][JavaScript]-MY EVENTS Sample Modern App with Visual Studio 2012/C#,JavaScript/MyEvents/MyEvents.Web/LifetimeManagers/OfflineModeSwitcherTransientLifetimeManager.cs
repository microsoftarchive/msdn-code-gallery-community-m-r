using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MyEvents.Web.LifetimeManagers
{
    /// <summary>
    /// Lifetime manager that returns the fallback type if application mode is offline.
    /// </summary>
    public class OfflineModeSwitcherTransientLifetimeManager : TransientLifetimeManager
    {
        Type _offlineFallback;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offlineFallback"></param>
        public OfflineModeSwitcherTransientLifetimeManager(Type offlineFallback)
        { 
            _offlineFallback = offlineFallback;
        }

        /// <summary>
        /// If the application mode is online returns the registered type, if not returns the fallback type.
        /// </summary>
        /// <returns></returns>
        public override object GetValue()
        {
            bool isOfflineMode = bool.Parse(ConfigurationManager.AppSettings["OfflineMode"]);
            if (isOfflineMode)
                return System.Activator.CreateInstance(_offlineFallback);
                
            return base.GetValue();
        }
    }
}