// 
//  (c) Microsoft Corporation. See LICENSE.TXT file for licensing details
//  
namespace Microsoft.ServiceBus.AccessControlExtensions
{
    public abstract class IdentityReference : Claim
    {
        public abstract string IssuerName { get; }

        public static IdentityReference CreateServiceIdentityReference(string accountName)
        {
            return new ServiceIdentityReference(accountName);
        }

        public static IdentityReference CreateServiceIdentityReference(AccessControlServiceIdentity identity)
        {
            return new ServiceIdentityReference(identity.Name);
        }

        public static IdentityReference CreateGenericReference(string issuerName, string claimType, string claimValue)
        {
            return new GenericIdentityReference(issuerName, claimType, claimValue);
        }

        public override bool Equals(object obj)
        {
            if (obj is IdentityReference)
            {
                var o = (IdentityReference) obj;
                return string.Compare(o.ClaimType, this.ClaimType) == 0 && string.Compare(o.ClaimValue, this.ClaimValue) == 0 &&
                       string.Compare(o.IssuerName, this.IssuerName) == 0;
            }
            return false;
        }

        public bool Equals(IdentityReference other)
        {
            return this.Equals((object) other);
        }

        public override int GetHashCode()
        {
            return (this.ClaimType != null ? this.ClaimType.GetHashCode() : 0) ^ (this.ClaimValue != null ? this.ClaimValue.GetHashCode() : 0) ^
                   (this.IssuerName != null ? this.IssuerName.GetHashCode() : 0);
        }

        class GenericIdentityReference : IdentityReference
        {
            readonly string claimType;
            readonly string claimValue;
            readonly string issuerName;

            public GenericIdentityReference(string issuerName, string claimType, string claimValue)
            {
                this.issuerName = issuerName;
                this.claimType = claimType;
                this.claimValue = claimValue;
            }

            public override string ClaimType { get { return this.claimType; } }

            public override string ClaimValue { get { return this.claimValue; } }

            public override string IssuerName { get { return this.issuerName; } }
        }

        class ServiceIdentityReference : IdentityReference
        {
            readonly string accountName;

            public ServiceIdentityReference(string accountName)
            {
                this.accountName = accountName;
            }

            public override string ClaimType { get { return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"; } }

            public override string ClaimValue { get { return this.accountName; } }

            public override string IssuerName { get { return "LOCAL AUTHORITY"; } }
        }
    }
}