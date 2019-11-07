using System.ComponentModel.DataAnnotations;

namespace DataAccessEfCore.DbModels
{
    public class SpecMultiValue
    {
        public int StyleId { get; set; }

        [Required]
        public byte DisplayIndex_IdealFor { get; set; }

        public byte DisplayIndex_Ability { get; set; }

        public byte DisplayIndex_SnowCondition { get; set; }

        public byte DisplayIndex_Terrain { get; set; }

        public byte DisplayIndex_RockerCamberProfile { get; set; }
    }
}
