

namespace MyCompany.Travel.Data.Infrastructure.Conventions
{
    using System;
    using System.Data.Entity.ModelConfiguration.Conventions;

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
