using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace Infrastructure.Data.EntityFramework
{
    /// <summary>
    /// Stores object context
    /// </summary>
    public interface IObjectContextStorage
    {
        /// <summary>
        /// Gets the object context for key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        ObjectContext GetObjectContextForKey(string key);

        /// <summary>
        /// Sets the object context for key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="objectContext">The object context.</param>
        void SetObjectContextForKey(string key, ObjectContext objectContext);

        /// <summary>
        /// Gets all object contexts.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ObjectContext> GetAllObjectContexts();
    }
}
