
namespace DataAccessEfCore.DbModels
{
    public class StyleSnowCondition
    {
       public int StyleId { get; set; }

       public byte SnowConditionId { get; set; }

       // relationships

       public Style Style { get; set; }

       public SpecSnowCondition SnowCondition { get; set; }
    }
}
