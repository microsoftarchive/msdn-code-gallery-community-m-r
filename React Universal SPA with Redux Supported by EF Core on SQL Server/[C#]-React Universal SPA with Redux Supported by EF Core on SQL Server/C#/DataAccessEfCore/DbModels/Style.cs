using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessEfCore.DbModels
{
    public class Style
    {
        public int StyleId { get; set; }

        [Required]
        [StringLength(6, MinimumLength = 6)]
        [RegularExpression(@"^[0-9]{6}$")]
        public string StyleNo { get; set; }

        [Required]
        public short BrandId { get; set; }

        [Required]
        public byte CategoryId { get; set; }

        [Required]
        public byte GenderId { get; set; }

        [Required]
        [StringLength(200)]
        public string StyleName { get; set; }

        [Required]
        [StringLength(300)]
        public string ImageBig { get; set; }

        [Required]
        [StringLength(300)]
        public string ImageSmall { get; set; }

        [Required]
        public decimal PriceCurrent { get; set; }

        [Required]
        public decimal PriceRegular { get; set; }

        public bool SoftDeleted { get; set; }

        // relationships

        public Brand Brand { get; set; }

        public Category Category { get; set; }

        public Gender Gender { get; set; }

        public StyleState StyleState { get; set; }

        public ICollection<Sku> Skus { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public ICollection<Description> Descriptions { get; set; }

        public ICollection<StyleIdealFor> StyleIdealFors { get; set;  }

        public ICollection<StyleAbility> StyleAbilities { get; set; }

        public ICollection<StyleRockerCamberProfile> StyleRockerCamberProfiles { get; set; }

        public ICollection<StyleSnowCondition> StyleSnowConditions { get; set; }

        public ICollection<StyleTerrain> StyleTerrains { get; set; }

    }

    internal class StyleNoValidationAttribute : ValidationAttribute
    {

    }
}
