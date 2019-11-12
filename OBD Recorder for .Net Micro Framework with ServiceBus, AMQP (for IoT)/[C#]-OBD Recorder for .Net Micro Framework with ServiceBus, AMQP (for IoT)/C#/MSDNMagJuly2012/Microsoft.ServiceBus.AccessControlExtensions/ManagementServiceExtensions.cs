// 
//  (c) Microsoft Corporation. See LICENSE.TXT file for licensing details
//  
namespace Microsoft.ServiceBus.AccessControlExtensions
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using Microsoft.ServiceBus.AccessControlExtensions.AccessControlManagement;

    /// <summary>
    ///   This class provides helper functions for common operations on the ACS Management Service.
    /// </summary>
    public static class ManagementServiceExtensions
    {
        /// <summary>
        ///   Finds an <see cref = "Issuer" /> with the given name.
        /// </summary>
        /// <param name = "name">The name of the <see cref = "Issuer" />.</param>
        /// <returns>Returns the <see cref = "Issuer" /> if found else returns null.</returns>
        public static Issuer GetIssuerByName(this ManagementService svc, string name)
        {
            var issuer = svc.Issuers.Where(m => m.Name == name).FirstOrDefault();

            return issuer;
        }


        /// <summary>
        ///   Creates  a new <see cref = " RelyingParty" />.
        /// </summary>
        /// <param name = "name">Name of this new <see cref = " RelyingParty" />.</param>
        /// <param name = "realm">Realm of the relying party.</param>
        /// <param name = "reply">ReplyTo address for the relying party. May be null.</param>
        /// <param name = "tokenType">The type of token that the relying party consumes.</param>
        /// <param name = "requireEncryption">Whether to require asymmetric token encryption.</param>
        /// <returns>The new <see cref = " RelyingParty" /> created.</returns>
        public static RelyingParty CreateRelyingParty(
            this ManagementService svc, string name, string realm, string reply, RelyingPartyTokenType tokenType, bool requireEncryption)
        {
            svc.DeleteRelyingPartyByRealmIfExists(realm);

            var relyingParty = new RelyingParty
                                   {AsymmetricTokenEncryptionRequired = requireEncryption, Name = name, TokenType = tokenType.ToString(), TokenLifetime = 3600,};

            svc.AddToRelyingParties(relyingParty);

            //
            // Create the Realm address
            //
            var realmAddress = new RelyingPartyAddress {Address = realm, EndpointType = RelyingPartyAddressType.Realm.ToString(),};

            svc.AddRelatedObject(relyingParty, "RelyingPartyAddresses", realmAddress);

            if (!string.IsNullOrEmpty(reply))
            {
                //
                // Create the ReplyTo address
                //
                var replyAddress = new RelyingPartyAddress {Address = reply, EndpointType = RelyingPartyAddressType.Reply.ToString(),};

                svc.AddRelatedObject(relyingParty, "RelyingPartyAddresses", replyAddress);
            }

            return relyingParty;
        }

        /// <summary>
        ///   Returns a RelyingParty with the given name. If the relying party does not exist, returns null.
        /// </summary>
        /// <param name = "name">The name of the RelyingParty.</param>
        /// <param name = "expandKeysAndAddresses">Whether the RelyingPartyKey and RelyingPartyAddress collections should be expanded and returned.</param>
        public static RelyingParty GetRelyingPartyByName(this ManagementService svc, string name, bool expandKeysAndAddresses)
        {
            if (expandKeysAndAddresses)
            {
                return svc.RelyingParties.Expand("RelyingPartyKeys,RelyingPartyAddresses").Where(m => m.Name == name).FirstOrDefault();
            }
            else
            {
                return svc.RelyingParties.Where(m => m.Name == name).FirstOrDefault();
            }
        }


        /// <summary>
        ///   Deletes a RelyingParty having the given name, if it exists.
        /// </summary>
        public static void DeleteRelyingPartyByRealmIfExists(this ManagementService svc, string realm)
        {
            // Retrieve Relying Party and then delete it if it exists.
            var rpAddress =
                svc.RelyingPartyAddresses.Expand("RelyingParty").Where(
                                                                       addr =>
                                                                       addr.Address == realm && addr.EndpointType == RelyingPartyAddressType.Realm.ToString()).
                    FirstOrDefault();

            if (rpAddress != null)
            {
                svc.DeleteObject(rpAddress.RelyingParty);
            }
        }

        #region Service Identity extensions

        /// <summary>
        ///   Creates  a new ServiceIdentity and an associated key of the value, type, and usage specified.
        /// </summary>
        public static ServiceIdentity CreateServiceIdentity(
            this ManagementService svc, string name, byte[] keyValue, ServiceIdentityKeyType keyType, ServiceIdentityKeyUsage keyUsage)
        {
            var sid = new ServiceIdentity {Name = name};

            DateTime startDate, endDate;

            if (keyType == ServiceIdentityKeyType.X509Certificate)
            {
                //
                // ACS requires that the key start and end dates be within the certificate's validity period.
                //
                var cert = new X509Certificate2(keyValue);

                startDate = cert.NotBefore.ToUniversalTime();
                endDate = cert.NotAfter.ToUniversalTime();
            }
            else
            {
                startDate = DateTime.UtcNow;
                endDate = DateTime.MaxValue;
            }

            var key = new ServiceIdentityKey
                          {
                              EndDate = endDate.ToUniversalTime(),
                              StartDate = startDate.ToUniversalTime(),
                              Type = keyType.ToString(),
                              Usage = keyUsage.ToString(),
                              Value = keyValue,
                              DisplayName = String.Format(CultureInfo.InvariantCulture, "{0} key for {1}", keyType, name)
                          };

            svc.AddToServiceIdentities(sid);
            svc.AddRelatedObject(sid, "ServiceIdentityKeys", key);

            return sid;
        }

        /// <summary>
        ///   Finds a ServiceIdentity with the given name.
        /// </summary>
        /// <param name = "name">The name of the <see cref = " ServiceIdentity" />.</param>
        /// <returns>Returns the <see cref = " ServiceIdentity" /> if found else returns null.</returns>
        public static ServiceIdentity GetServiceIdentityByName(this ManagementService svc, string name)
        {
            var identity = svc.ServiceIdentities.Expand("ServiceIdentityKeys").Where(sk => sk.Name == name).FirstOrDefault();

            return identity;
        }

        /// <summary>
        ///   Update the service identity key value.
        /// </summary>
        /// <param name = "name">Name of the <see cref = "ServiceIdentityKey" />.</param>
        /// <param name = "keyValue">The new key value.</param>
        /// <param name = "keyType">The new Key type.</param>
        public static void UpdateServiceIdentityKey(this ManagementService svc, string name, byte[] keyValue, ServiceIdentityKeyType keyType)
        {
            var ServiceIdentity = svc.GetServiceIdentityByName(name);

            if (ServiceIdentity != null)
            {
                foreach (ServiceIdentityKey key in ServiceIdentity.ServiceIdentityKeys)
                {
                    if (key.Type ==
                        keyType.ToString())
                    {
                        key.Value = keyValue;
                        svc.UpdateObject(key);
                    }
                }
            }
        }

        /// <summary>
        ///   Delete service identity by name if it exists - child objects, keys in this case, are automatically deleted.
        /// </summary>
        /// <param name = "name">The name of the <see cref = "ServiceIdentity" /> to delete.</param>
        public static void DeleteServiceIdentityIfExists(this ManagementService svc, string name)
        {
            var serviceIdentities = svc.ServiceIdentities.Where(si => si.Name == name);

            foreach (var si in serviceIdentities)
            {
                svc.DeleteObject(si);
            }
        }

        #endregion

        #region RuleGroup extensions

        /// <summary>
        ///   Creates a rule group with the given name.
        /// </summary>
        public static RuleGroup CreateRuleGroup(this ManagementService svc, string name)
        {
            svc.DeleteRuleGroupByNameIfExists(name);

            var ruleGroup = new RuleGroup {Name = name};

            svc.AddToRuleGroups(ruleGroup);

            return ruleGroup;
        }

        /// <summary>
        ///   Creates a rule and adds it to the given RuleGroup.
        /// </summary>
        /// <param name = "issuer">The input issuer for which to apply the rule.</param>
        /// <param name = "inputClaimType">The input claim type. Null for "all claim types".</param>
        /// <param name = "inputClaimValue">The input claim value. Null for "all claim values".</param>
        /// <param name = "outputClaimType">The output claim type. Null for "pass through input type".</param>
        /// <param name = "outputClaimValue">The output claim value. Null for "pass through input value".</param>
        /// <param name = "ruleGroup">The rule group to which the rule should be added.</param>
        /// <param name = "description">Optional human-readable rule description.</param>
        /// <returns></returns>
        public static Rule CreateRule(
            this ManagementService svc,
            Issuer issuer,
            string inputClaimType,
            string inputClaimValue,
            string outputClaimType,
            string outputClaimValue,
            RuleGroup ruleGroup,
            string description)
        {
            var rule = new Rule
                           {
                               Description = description,
                               InputClaimType = inputClaimType,
                               InputClaimValue = inputClaimValue,
                               OutputClaimType = outputClaimType,
                               OutputClaimValue = outputClaimValue,
                           };

            svc.AddToRules(rule);
            svc.SetLink(rule, "Issuer", issuer);
            svc.SetLink(rule, "RuleGroup", ruleGroup);

            return rule;
        }


        /// <summary>
        ///   Deletes a RuleGroup having the given name, if it exists.
        /// </summary>
        public static void DeleteRuleGroupByNameIfExists(this ManagementService svc, string name)
        {
            // Retrieve a Rule Group and then delete it if it exists.
            var ruleGroup = svc.RuleGroups.Where(m => m.Name == name).FirstOrDefault();
            if (ruleGroup != null)
            {
                svc.DeleteObject(ruleGroup);
            }
        }

        #endregion
    }
}