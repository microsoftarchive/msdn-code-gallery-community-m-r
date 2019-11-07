
namespace DataAccessEfCore.DbModels
{
    public class StyleAbility
    {
        public int StyleId { get; set; }

        public byte AbilityId { get; set; }

        // relationships

        public Style Style { get; set; }

        public SpecAbility Ability { get; set; }
    }
}
