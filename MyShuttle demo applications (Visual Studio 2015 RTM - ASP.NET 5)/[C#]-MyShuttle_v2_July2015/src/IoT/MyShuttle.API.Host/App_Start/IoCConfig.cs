using Microsoft.Practices.Unity;
using MyShuttle.API.Host.IoC;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MyShuttle.API.Host
{
    public class IoCConfig
    {
        public static void Register(HttpConfiguration config)
        {

            var appSettings = ReadAppSettings();
            var container = new UnityContainer();
            UnityRegistration.RegisterTypes(container, appSettings);
            config.DependencyResolver = new UnityDependencyResolver(container);
        }

        private static IDictionary<string, string> ReadAppSettings()
        {
            var appSettings = new Dictionary<string, string>();
            ConfigurationManager.AppSettings.AllKeys.Select(k => new
            {
                Key = k,
                Value = ConfigurationManager.AppSettings[k]
            }).ToList().ForEach(kvp => appSettings.Add(kvp.Key, kvp.Value));
            return appSettings;
        }

    }
}