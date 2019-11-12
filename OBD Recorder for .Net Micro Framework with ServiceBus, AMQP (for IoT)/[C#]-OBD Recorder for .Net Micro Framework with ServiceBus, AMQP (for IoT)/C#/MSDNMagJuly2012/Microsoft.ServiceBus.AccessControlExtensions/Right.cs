// 
//  (c) Microsoft Corporation. See LICENSE.TXT file for licensing details
//  
namespace Microsoft.ServiceBus.AccessControlExtensions
{
    public abstract class Right : Claim
    {
        public static Right CreateGenericRight(string outputClaimType, string outputClaimValue)
        {
            return new GenericRight(outputClaimType, outputClaimValue);
        }

        protected class GenericRight : Right
        {
            readonly string claimType;
            readonly string claimValue;

            public GenericRight(string claimType, string claimValue)
            {
                this.claimType = claimType;
                this.claimValue = claimValue;
            }

            public override string ClaimType { get { return this.claimType; } }

            public override string ClaimValue { get { return this.claimValue; } }
        }
    }
}