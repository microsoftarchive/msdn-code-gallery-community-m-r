using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessEfCore.DbModels
{
    public class SpecAbility
    {
        public byte AbilityId { get; set; }

        [Required]
        [StringLength(50)]
        public string AbilitySpec { get; set; }

        // relationships

        public ICollection<StyleAbility> StyleAbilities { get; set; }
    }
}
