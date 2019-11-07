using System;
using System.Collections.Generic;


namespace MyEvents.IoC
{
    /// <summary>
    /// IoCFactory  implementation 
    /// </summary>
    public sealed class IoCFactory
    {
        #region Singleton

        static readonly IoCFactory instance = new IoCFactory();

        /// <summary>
        /// Get singleton instance of IoCFactory
        /// </summary>
        public static IoCFactory Instance
        {
            get
            {
                return instance;
            }
        }

        #endregion

        #region Members

        IContainer _CurrentContainer;

        /// <summary>
        /// Get current configured IContainer
        /// <remarks>
        /// At this moment only IoCUnityContainer existss
        /// </remarks>
        /// </summary>
        public IContainer CurrentContainer
        {
            get
            {
                return _CurrentContainer;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Only for singleton pattern, remove before field init IL anotation
        /// </summary>
        static IoCFactory() { }
        IoCFactory()
        {
            _CurrentContainer = new Container();
        }
        
        #endregion
    }
}
