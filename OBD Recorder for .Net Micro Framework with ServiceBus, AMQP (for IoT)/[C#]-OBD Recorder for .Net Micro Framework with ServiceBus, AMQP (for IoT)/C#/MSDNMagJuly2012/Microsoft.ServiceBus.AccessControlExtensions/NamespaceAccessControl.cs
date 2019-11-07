// 
//  (c) Microsoft Corporation. See LICENSE.TXT file for licensing details
//  
namespace Microsoft.ServiceBus.AccessControlExtensions
{
    using System;
    using System.Linq;
    using Microsoft.ServiceBus.AccessControlExtensions.AccessControlManagement;

    public static class NamespaceAccessControl
    {
        public static void ResetNamespace(Uri rpAddress, AccessControlSettings settings)
        {
            rpAddress = new UriBuilder(rpAddress) {Scheme = "http", Port = -1}.Uri;
            var serviceClient = ManagementServiceHelper.CreateManagementServiceClient(settings);
            foreach (var g1 in from g in serviceClient.RuleGroups where g.Name.StartsWith(rpAddress.AbsoluteUri) select g)
            {
                serviceClient.DeleteRuleGroupByNameIfExists(g1.Name);
            }
            serviceClient.SaveChanges();
        }

        public static AccessControlList GetAccessControlList(Uri relyingPartyUri, AccessControlSettings settings)
        {
            var localPath = relyingPartyUri.LocalPath;
            relyingPartyUri =
                new UriBuilder(relyingPartyUri)
                    {Scheme = "http", Port = -1, Path = localPath.Substring(0, localPath.EndsWith("/") ? localPath.Length - 1 : localPath.Length)}.Uri;

            var relyingPartyAddress = relyingPartyUri.AbsoluteUri;
            var serviceClient = ManagementServiceHelper.CreateManagementServiceClient(settings);
            var longestPrefixRpAddress = GetLongestPrefixRelyingPartyAddress(serviceClient, relyingPartyAddress);
            if (longestPrefixRpAddress != null)
            {
                var relyingParty = GetRelyingPartyByAddress(serviceClient, longestPrefixRpAddress);
                if (relyingParty != null)
                {
                    return new AccessControlList(relyingPartyUri, relyingParty, serviceClient);
                }
            }
            throw new InvalidOperationException();
        }

        static RelyingParty GetRelyingPartyByAddress(ManagementService serviceClient, RelyingPartyAddress longestPrefixRpAddress)
        {
            return (from rp in serviceClient.RelyingParties.Expand("RelyingPartyAddresses,RelyingPartyIdentityProviders,RelyingPartyRuleGroups")
                    where rp.Id == longestPrefixRpAddress.RelyingPartyId
                    select rp).FirstOrDefault();
        }

        static RelyingPartyAddress GetLongestPrefixRelyingPartyAddress(ManagementService serviceClient, string relyingPartyAddress)
        {
            return
                (from a in serviceClient.RelyingPartyAddresses where relyingPartyAddress.IndexOf(a.Address) == 0 select a).OrderByDescending(
                                                                                                                                             a =>
                                                                                                                                             a.Address.Length).
                    FirstOrDefault();
        }
    }
}