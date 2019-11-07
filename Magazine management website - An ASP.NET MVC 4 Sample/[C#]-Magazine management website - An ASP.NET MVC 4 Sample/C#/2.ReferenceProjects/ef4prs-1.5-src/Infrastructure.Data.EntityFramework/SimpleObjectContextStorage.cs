using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace Infrastructure.Data.EntityFramework
{
    /// <summary>
    /// Simple object context storage implementation
    /// </summary>
    public class SimpleObjectContextStorage : IObjectContextStorage
    {
        private Dictionary<string, ObjectContext> storage = new Dictionary<string, ObjectContext>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleObjectContextStorage"/> class.
        /// </summary>
        public SimpleObjectContextStorage() { }

        /// <summary>
        /// Returns the object context associated with the specified key or
		/// null if the specified key is not found.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public ObjectContext GetObjectContextForKey(string key)
        {
            ObjectContext context;
            if (!this.storage.TryGetValue(key, out context))
                return null;
            return context;
        }


        /// <summary>
        /// Stores the object context into a dictionary using the specified key.
        /// If an object context already exists by the specified key, 
        /// it gets overwritten by the new object context passed in.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="objectContext">The object context.</param>
        public void SetObjectContextForKey(string key, ObjectContext objectContext)
        {           
            this.storage.Add(key, objectContext);           
        }

        /// <summary>
        /// Returns all the values of the internal dictionary of object contexts.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ObjectContext> GetAllObjectContexts()
        {
            return this.storage.Values;
        }
    }
}
