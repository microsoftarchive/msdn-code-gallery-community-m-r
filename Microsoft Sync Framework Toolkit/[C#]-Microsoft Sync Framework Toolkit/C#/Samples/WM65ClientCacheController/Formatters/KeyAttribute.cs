using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Samples.Synchronization.ClientServices
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class KeyAttribute : Attribute
    {
    }
}
