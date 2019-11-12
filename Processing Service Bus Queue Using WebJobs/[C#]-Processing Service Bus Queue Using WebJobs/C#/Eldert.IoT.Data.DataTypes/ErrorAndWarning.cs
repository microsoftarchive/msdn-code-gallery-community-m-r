using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eldert.IoT.Data.DataTypes
{
    [Table("ErrorAndWarning")]
    public class ErrorAndWarning
    {
        // By specifying the name ID, entity framework will know this should be an auto-incrementing PK
        public int ID { get; set; }

        [StringLength(50)]
        public string ShipName { get; set; }
        
        public string Message { get; set; }

        public DateTime CreatedDateTime { get; set; }
    }
}
