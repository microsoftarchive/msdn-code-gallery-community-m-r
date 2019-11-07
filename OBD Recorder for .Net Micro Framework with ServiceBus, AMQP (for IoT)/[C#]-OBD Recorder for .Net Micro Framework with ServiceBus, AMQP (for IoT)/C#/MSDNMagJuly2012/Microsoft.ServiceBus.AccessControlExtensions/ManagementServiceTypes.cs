// 
//  (c) Microsoft Corporation. See LICENSE.TXT file for licensing details
//  
namespace Microsoft.ServiceBus.AccessControlExtensions
{
    public enum IdentityProviderKeyType
    {
        ApplicationKey,
        Symmetric,
        X509Certificate
    }

    public enum IdentityProviderKeyUsage
    {
        ApplicationId,
        ApplicationSecret,
        Signing
    }

    public enum IdentityProviderProtocolType
    {
        Facebook,
        OpenId,
        WsFederation
    }

    public enum IdentityProviderEndpointType
    {
        EmailDomain,
        FedMetadataUrl,
        ImageUrl,
        SignIn,
        SignOut
    }

    public enum RelyingPartyTokenType
    {
        SAML_1_1,
        SAML_2_0,
        SWT
    } ;

    public enum RelyingPartyKeyType
    {
        Symmetric,
        X509Certificate
    } ;

    public enum RelyingPartyKeyUsage
    {
        Encrypting,
        Signing
    } ;

    public enum RelyingPartyAddressType
    {
        Error,
        Realm,
        Reply
    }

    public enum ServiceIdentityKeyType
    {
        Password,
        Symmetric,
        X509Certificate
    } ;

    /// <summary>
    ///   The valid list of key usages
    /// </summary>
    public enum ServiceIdentityKeyUsage
    {
        Password,
        Signing
    }

    public enum ServiceKeyType
    {
        X509Certificate,
        Password,
        Symmetric
    }

    public enum ServiceKeyUsage
    {
        Signing,
        Management,
        Encrypting,
    }
}