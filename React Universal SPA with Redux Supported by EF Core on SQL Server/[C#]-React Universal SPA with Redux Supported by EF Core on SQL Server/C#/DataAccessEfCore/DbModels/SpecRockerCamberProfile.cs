using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessEfCore.DbModels
{
    public class SpecRockerCamberProfile
    {
        public byte RockerCamberProfileId { get; set; }

        [Required]
        [StringLength(50)]
        public string RockerCamberProfileSpec { get; set; }

        // relationships
        public ICollection<StyleRockerCamberProfile> StyleRockerCamberProfiles { get; set; }
    }
}
