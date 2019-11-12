using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MyEvents.IoC
{
    /// <summary>
    /// Base contract for locator and register dependencies
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// Solve TService dependency
        /// </summary>
        /// <typeparam name="TService">Type of dependency</typeparam>
        /// <returns>instance of TService</returns>
        TService Resolve<TService>();

        /// <summary>
        /// Solve type construction and return the object as a TService instance
        /// </summary>
        /// <returns>instance of this type</returns>
        object Resolve(Type type);

        /// <summary>
        /// Solve type construction and return all the objects as a TService instance
        /// </summary>
        /// <returns>instance of this type</returns>
        IEnumerable<object> ResolveAll(Type type);

        /// <summary>
        /// Register type into service locator
        /// </summary>
        /// <param name="type">Type to register</param>
        void RegisterType(Type type);
    }
}
