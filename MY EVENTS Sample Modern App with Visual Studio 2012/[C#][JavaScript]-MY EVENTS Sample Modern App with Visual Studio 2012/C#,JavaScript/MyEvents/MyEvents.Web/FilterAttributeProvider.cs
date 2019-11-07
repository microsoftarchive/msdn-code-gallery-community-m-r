using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyEvents.Web
{
    /// <summary>
    /// Filter attribute provider with ioc resolution.
    /// </summary>
    public class FilterAttributeProvider : FilterAttributeFilterProvider
    {
        private IUnityContainer _container;

        /// <summary>
        /// FilterAttributeProvider constructor.
        /// </summary>
        /// <param name="container"></param>
        public FilterAttributeProvider(IUnityContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// Gets a collection of controller attributes.
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="actionDescriptor"></param>
        /// <returns></returns>
        protected override IEnumerable<FilterAttribute> GetControllerAttributes(
                    ControllerContext controllerContext,
                    ActionDescriptor actionDescriptor)
        {

            var attributes = base.GetControllerAttributes(controllerContext,
                                                          actionDescriptor);
            BuildUpAttributes(attributes);
            return attributes;
        }

        /// <summary>
        /// Gets a collection of custom action attributes.
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="actionDescriptor"></param>
        /// <returns></returns>
        protected override IEnumerable<FilterAttribute> GetActionAttributes(
                    ControllerContext controllerContext,
                    ActionDescriptor actionDescriptor)
        {

            var attributes = base.GetActionAttributes(controllerContext,
                                                      actionDescriptor);
            BuildUpAttributes(attributes);
            return attributes;
        }

        private void BuildUpAttributes(IEnumerable<FilterAttribute> attributes)
        {
            foreach (var attribute in attributes)
            {
                _container.BuildUp(attribute.GetType(), attribute);
            }
        }
    }
}