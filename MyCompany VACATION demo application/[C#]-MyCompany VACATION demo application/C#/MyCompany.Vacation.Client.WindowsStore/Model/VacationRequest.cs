using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Vacation.Client.WindowsStore.Model
{
    /// <summary>
    /// Vacation request entity
    /// </summary>
    public class VacationRequest
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// From date.
        /// </summary>
        [JsonProperty(PropertyName = "from")]
        public DateTime From { get; set; }
        
        /// <summary>
        /// To Date.
        /// </summary>
        [JsonProperty(PropertyName = "to")]
        public DateTime To { get; set; }
        
        /// <summary>
        /// Comment
        /// </summary>
        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; }
    }
}
