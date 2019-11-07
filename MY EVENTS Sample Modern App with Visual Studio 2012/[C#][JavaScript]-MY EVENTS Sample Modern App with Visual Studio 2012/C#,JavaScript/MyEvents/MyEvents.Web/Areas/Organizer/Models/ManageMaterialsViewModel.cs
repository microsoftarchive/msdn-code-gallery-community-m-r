using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyEvents.Model;

namespace MyEvents.Web.Areas.Organizer.Models
{
    /// <summary>
    /// Materials list viewModel
    /// </summary>
    public class ManageMaterialsViewModel
    {
        /// <summary>
        /// The session id.
        /// </summary>
        public int SessionId { get; set; }

        /// <summary>
        /// The materials list.
        /// </summary>
        public List<Material> Materials { get; set; }  
    }
}