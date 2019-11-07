using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessEfCore.DbModels
{
    public class IdealFor
    {
        public byte IdealForId { get; set; }

        [Required]
        [StringLength(50)]
        public string IdealForSpec { get; set; }

        // relationships

        public ICollection<StyleIdealFor> StyleIdealFors { get; set; }
    }
}
