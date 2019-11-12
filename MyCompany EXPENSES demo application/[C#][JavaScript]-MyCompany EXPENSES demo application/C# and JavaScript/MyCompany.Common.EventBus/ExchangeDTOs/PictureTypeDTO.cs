using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCompany.Common.EventBus
{
    /// <summary>
    /// Picture Type
    /// </summary>
    [Serializable]
    public enum PictureTypeDTO
    {
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Small
        /// </summary>
        Small = 1,
        /// <summary>
        /// Big
        /// </summary>
        Big = 2
    }
}