using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightweightPrismSampleApp.Utils
{
    public class CanConvertAttribute : ValidationAttribute
    {
        private Type type;

        public CanConvertAttribute(Type type)
        {
            this.type = type;
        }

        public override bool IsValid(object value)
        {
            try
            {
                Convert.ChangeType(value, type);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
