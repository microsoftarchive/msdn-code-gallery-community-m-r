using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyEvents.Api.Client
{
    /// <summary>
    /// Material
    /// </summary>
    public class Material 
    {
        /// <summary>
        /// UniqueId
        /// </summary>
        public int MaterialId { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Content
        /// </summary>
        public byte[] Content { get; set; }

        /// <summary>
        /// Content type
        /// </summary>        
        public string ContentType { get; set; }

        /// <summary>
        /// SessionId associated with the material
        /// </summary>
        public int SessionId { get; set; }

        /// <summary>
        /// Session associated with the material
        /// </summary>
        public Session Session { get; set; }
    }
}
