using Microsoft.Maps.MapControl.WPF;
using System.Collections.Generic;

namespace ClusterEngine
{
    /// <summary>
    /// An object used for 
    /// </summary>
    public class ClusteredPoint
    {
        #region Public Properties

        /// <summary>
        /// Zoom level that the clustered point is for.
        /// </summary>
        public int Zoom { get; set; }

        /// <summary>
        /// Location that the clustered point represents
        /// </summary>
        public Location Location { get; set; }

        /// <summary>
        /// A list of Entity ID's
        /// </summary>
        public IList<int> EntityIds { get; set; }

        #endregion

        #region Internal Properties

        internal double Left { get; set; }

        internal double Right { get; set; }

        internal double Top { get; set; }

        internal double Bottom { get; set; }

        #endregion
    }
}
