namespace MVVMCalc.Common
{
    using System.ComponentModel.DataAnnotations;

    public class DoubleAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var input = value as string;
            if (input == null)
            {
                return false;
            }

            var tmp = default(double);
            return double.TryParse(input, out tmp);
        }
    }
}
