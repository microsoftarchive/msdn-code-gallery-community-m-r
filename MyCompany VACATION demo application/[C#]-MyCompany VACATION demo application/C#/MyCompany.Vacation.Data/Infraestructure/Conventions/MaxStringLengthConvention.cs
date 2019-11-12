
namespace MyCompany.Vacation.Data.Infrastructure.Conventions
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class MaxStringLengthConvention
        :Convention
    {
        public MaxStringLengthConvention()
        {
            this.Properties<String>()
                .Configure(c => c.HasMaxLength(255));
        }
    }
}
