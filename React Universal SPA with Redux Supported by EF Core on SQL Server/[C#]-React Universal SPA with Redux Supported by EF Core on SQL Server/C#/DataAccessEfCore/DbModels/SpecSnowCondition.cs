using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessEfCore.DbModels
{
    public class SpecSnowCondition
    {
        public byte SnowConditionId { get; set; }

        [Required]
        [StringLength(50)]
        public string SnowConditionSpec { get; set; }

        // relationships

        public ICollection<StyleSnowCondition> StyleSnowConditions { get; set; }
    }
}
