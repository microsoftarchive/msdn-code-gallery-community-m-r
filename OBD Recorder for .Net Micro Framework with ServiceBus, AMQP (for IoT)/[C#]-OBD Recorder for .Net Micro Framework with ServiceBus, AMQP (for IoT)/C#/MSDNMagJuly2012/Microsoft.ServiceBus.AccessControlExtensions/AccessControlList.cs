// 
//  (c) Microsoft Corporation. See LICENSE.TXT file for licensing details
//  
namespace Microsoft.ServiceBus.AccessControlExtensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.ServiceBus.AccessControlExtensions.AccessControlManagement;

    public class AccessControlList : IEnumerable<AccessControlRule>
    {
        const string DefaultRuleGroupName = "Default Rule Group for ServiceBus";
        readonly List<AccessControlRule> localRules;
        readonly bool newRelyingPartyRequired;
        readonly RelyingParty relyingParty;
        readonly Uri relyingPartyUri;
        readonly ManagementService serviceClient;
        List<SegmentRuleGroup> ruleGroups;

        internal AccessControlList(Uri uri, RelyingParty relyingParty, ManagementService serviceClient)
        {
            this.relyingPartyUri = uri;
            this.relyingParty = relyingParty;
            this.newRelyingPartyRequired = !(this.relyingParty.RelyingPartyAddresses.Any(i => i.Address == uri.AbsoluteUri));
            this.serviceClient = serviceClient;
            this.localRules = new List<AccessControlRule>();
            this.InitializeRules();
        }

        IEnumerable<AccessControlRule> Rules
        {
            get
            {
                if (this.ruleGroups == null)
                {
                    this.InitializeRules();
                }
                // enumerate all rules by querying except the last segment
                // which are the local rules
                for (var index = 0; index < this.ruleGroups.Count - 1; index++)
                {
                    var g = this.ruleGroups[index];
                    if (g.RuleGroup != null)
                    {
                        foreach (var rule in
                            from r in this.serviceClient.Rules.Expand("Issuer") where r.RuleGroupId == g.RuleGroup.Id select r)
                        {
                            yield return
                                new AccessControlRule(
                                    IdentityReference.CreateGenericReference(rule.Issuer.Name, rule.InputClaimType, rule.InputClaimValue),
                                    Right.CreateGenericRight(rule.OutputClaimType, rule.OutputClaimValue),
                                    true,
                                    g.RuleGroup,
                                    rule);
                        }
                    }
                }

                // return the local rules
                foreach (var accessControlRule in this.localRules)
                {
                    if (!accessControlRule.Deleted)
                    {
                        yield return accessControlRule;
                    }
                }
            }
        }


        public IEnumerator<AccessControlRule> GetEnumerator()
        {
            return this.Rules.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Rules.GetEnumerator();
        }

        void InitializeRules()
        {
            this.ruleGroups = new List<SegmentRuleGroup>();
            var prefix = new UriBuilder(this.relyingPartyUri.Scheme, this.relyingPartyUri.Host, -1);
            this.ruleGroups.Add(
                                new SegmentRuleGroup
                                    {
                                        Uri = prefix.Uri,
                                        SegmentName = null,
                                        RuleGroup =
                                            (from g in this.serviceClient.RuleGroups.Expand("RelyingPartyRuleGroups")
                                             where g.Name == prefix.Uri.AbsoluteUri || g.Name == DefaultRuleGroupName
                                             select g).FirstOrDefault()
                                    });

            for (var i = 1; i < this.relyingPartyUri.Segments.Length; i++)
            {
                var segment = this.relyingPartyUri.Segments[i];
                prefix.Path += (prefix.Path.EndsWith("/") ? "" : "/") + segment.Substring(0, segment.EndsWith("/") ? segment.Length - 1 : segment.Length);
                this.ruleGroups.Add(
                                    new SegmentRuleGroup
                                        {
                                            Uri = prefix.Uri,
                                            SegmentName = segment,
                                            RuleGroup =
                                                (from g in this.serviceClient.RuleGroups.Expand("RelyingPartyRuleGroups")
                                                 where g.Name == prefix.Uri.AbsoluteUri
                                                 select g).FirstOrDefault()
                                        });
            }

            var segmentRuleGroup = this.ruleGroups.Last();
            if (segmentRuleGroup.RuleGroup != null)
            {
                foreach (var rule in
                    from r in this.serviceClient.Rules.Expand("Issuer") where r.RuleGroupId == segmentRuleGroup.RuleGroup.Id select r)
                {
                    this.localRules.Add(
                                        new AccessControlRule(
                                            IdentityReference.CreateGenericReference(rule.Issuer.Name, rule.InputClaimType, rule.InputClaimValue),
                                            Right.CreateGenericRight(rule.OutputClaimType, rule.OutputClaimValue),
                                            !segmentRuleGroup.Uri.AbsoluteUri.Equals(this.relyingPartyUri.AbsoluteUri),
                                            segmentRuleGroup.RuleGroup,
                                            rule));
                }
            }
        }

        public void SaveChanges()
        {
            foreach (var segmentRuleGroup in this.ruleGroups)
            {
                if (segmentRuleGroup.RuleGroup == null)
                {
                    segmentRuleGroup.RuleGroup = this.serviceClient.CreateRuleGroup(segmentRuleGroup.Uri.AbsoluteUri);
                    this.serviceClient.SaveChanges();
                }
            }
            var ruleGroup = this.ruleGroups.Last().RuleGroup;
            foreach (var accessControlRule in this.localRules)
            {
                if (accessControlRule.Deleted)
                {
                    if (accessControlRule.Rule != null)
                    {
                        this.serviceClient.DeleteObject(accessControlRule.Rule);
                    }
                }
                else if (accessControlRule.Rule != null)
                {
                    if (accessControlRule.PrepareChanges())
                    {
                        this.serviceClient.UpdateObject(accessControlRule.Rule);
                    }
                }
                else
                {
                    this.serviceClient.CreateRule(
                                                  this.serviceClient.GetIssuerByName(accessControlRule.Condition.IssuerName),
                                                  accessControlRule.Condition.ClaimType,
                                                  accessControlRule.Condition.ClaimValue,
                                                  accessControlRule.Right.ClaimType,
                                                  accessControlRule.Right.ClaimValue,
                                                  ruleGroup,
                                                  accessControlRule.Description);
                }
            }

            this.serviceClient.SaveChanges();

            if (this.newRelyingPartyRequired)
            {
                var rpaddress = this.relyingPartyUri.AbsoluteUri;
                var rp = this.serviceClient.GetRelyingPartyByName(rpaddress, true);
                if (rp == null)
                {
                    rp = new RelyingParty();
                    rp.DisplayName = rp.Name = rpaddress;
                    rp.Description = String.Empty;
                    rp.TokenLifetime = this.relyingParty.TokenLifetime;
                    rp.TokenType = this.relyingParty.TokenType;
                    rp.AsymmetricTokenEncryptionRequired = this.relyingParty.AsymmetricTokenEncryptionRequired;
                    this.serviceClient.AddToRelyingParties(rp);
                }
                if (rp.RelyingPartyAddresses.Count == 0)
                {
                    var rpa = new RelyingPartyAddress {Address = rpaddress, EndpointType = "Realm"};
                    this.serviceClient.AddRelatedObject(rp, "RelyingPartyAddresses", rpa);
                }
                this.serviceClient.SaveChanges();
                foreach (var segmentRuleGroup in this.ruleGroups)
                {
                    var relyingPartyRuleGroup = new RelyingPartyRuleGroup();
                    relyingPartyRuleGroup.RelyingPartyId = rp.Id;
                    relyingPartyRuleGroup.RuleGroupId = segmentRuleGroup.RuleGroup.Id;
                    this.serviceClient.AddToRelyingPartyRuleGroups(relyingPartyRuleGroup);
                }
                this.serviceClient.SaveChanges();
            }
            else
            {
                foreach (var segmentRuleGroup in this.ruleGroups)
                {
                    var grpid = segmentRuleGroup.RuleGroup.Id;
                    if (!this.relyingParty.RelyingPartyRuleGroups.Any(i => i.RuleGroupId == grpid))
                    {
                        var relyingPartyRuleGroup = new RelyingPartyRuleGroup();
                        relyingPartyRuleGroup.RelyingPartyId = this.relyingParty.Id;
                        relyingPartyRuleGroup.RuleGroupId = segmentRuleGroup.RuleGroup.Id;
                        this.serviceClient.AddToRelyingPartyRuleGroups(relyingPartyRuleGroup);
                    }
                }
                this.serviceClient.SaveChanges();
            }
        }

        public AccessControlRule AddRule(AccessControlServiceIdentity identity, Right right)
        {
            return AddRule(IdentityReference.CreateServiceIdentityReference(identity), right);
        }

        public AccessControlRule AddRule(IdentityReference identity, Right right)
        {
            var rule = (from r in this.localRules
                        where
                            r.Condition.ClaimType == identity.ClaimType && r.Condition.ClaimValue == identity.ClaimValue &&
                            r.Condition.IssuerName == identity.IssuerName && r.Right.ClaimType == right.ClaimType && r.Right.ClaimValue == right.ClaimValue
                        select r).FirstOrDefault();
            if (rule == null)
            {
                rule = new AccessControlRule(identity, right, false, this.ruleGroups.Last().RuleGroup, null);
                this.localRules.Add(rule);
            }
            else
            {
                rule.Deleted = false;
            }
            return rule;
        }


        public void ClearRules()
        {
            foreach (var accessControlRule in this.localRules)
            {
                accessControlRule.Deleted = true;
            }
        }

        public bool RemoveRule(AccessControlRule item)
        {
            if (this.localRules.Contains(item))
            {
                item.Deleted = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        class SegmentRuleGroup
        {
            internal RuleGroup RuleGroup;
            internal string SegmentName;
            internal Uri Uri;
        }
    }
}