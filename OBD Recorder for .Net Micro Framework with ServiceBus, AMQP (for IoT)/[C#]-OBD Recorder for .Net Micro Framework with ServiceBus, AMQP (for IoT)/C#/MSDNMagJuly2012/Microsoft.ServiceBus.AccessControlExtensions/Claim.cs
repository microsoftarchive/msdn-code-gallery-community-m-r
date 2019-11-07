// 
//  (c) Microsoft Corporation. See LICENSE.TXT file for licensing details
//  
namespace Microsoft.ServiceBus.AccessControlExtensions
{
    public abstract class Claim
    {
        public abstract string ClaimType { get; }
        public abstract string ClaimValue { get; }

        public override bool Equals(object obj)
        {
            return ((Claim) obj).ClaimType == this.ClaimType && ((Claim) obj).ClaimValue == this.ClaimValue;
        }

        public override int GetHashCode()
        {
            return this.ClaimType.GetHashCode() ^ this.ClaimValue.GetHashCode();
        }

        public static bool operator ==(Claim c1, Claim c2)
        {
            return Equals(c1, c2);
        }

        public static bool operator !=(Claim c1, Claim c2)
        {
            return !Equals(c1, c2);
        }
    }
}