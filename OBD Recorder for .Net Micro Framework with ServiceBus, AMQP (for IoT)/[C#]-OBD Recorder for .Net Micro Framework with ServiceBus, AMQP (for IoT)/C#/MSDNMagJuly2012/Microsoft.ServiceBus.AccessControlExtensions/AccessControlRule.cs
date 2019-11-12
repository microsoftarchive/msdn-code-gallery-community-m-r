// 
//  (c) Microsoft Corporation. See LICENSE.TXT file for licensing details
//  
namespace Microsoft.ServiceBus.AccessControlExtensions
{
    using System;
    using Microsoft.ServiceBus.AccessControlExtensions.AccessControlManagement;

    public class AccessControlRule
    {
        readonly IdentityReference condition;
        readonly bool inherited;
        readonly Right right;
        string description;
        Rule rule;

        internal AccessControlRule(IdentityReference condition, Right right, bool inherited, RuleGroup group, Rule rule)
        {
            this.condition = condition;
            this.right = right;
            this.inherited = inherited;
            this.Group = group;
            this.rule = rule;
        }

        internal AccessControlRule(AccessControlServiceIdentity identity, Right right, bool inherited, RuleGroup group, Rule rule)
        {
            this.condition = IdentityReference.CreateServiceIdentityReference(identity);
            this.right = right;
            this.inherited = inherited;
            this.Group = group;
            this.rule = rule;
        }

        internal Rule Rule { get { return this.rule; } set { this.rule = value; } }

        internal RuleGroup Group { get; set; }

        public IdentityReference Condition { get { return this.condition; } }

        public Right Right { get { return this.right; } }

        public string Description
        {
            get { return this.description; }
            set
            {
                if (this.inherited)
                    throw new InvalidOperationException();
                this.description = value;
            }
        }

        public bool Inherited { get { return this.inherited; } }

        internal bool Deleted { get; set; }

        public bool PrepareChanges()
        {
            if (this.rule != null &&
                (this.rule.InputClaimType != this.Condition.ClaimType || this.rule.InputClaimValue != this.Condition.ClaimValue ||
                 this.rule.OutputClaimType != this.Right.ClaimType || this.rule.OutputClaimValue != this.Right.ClaimValue))
            {
                this.rule.InputClaimType = this.Condition.ClaimType;
                this.rule.InputClaimValue = this.Condition.ClaimValue;
                this.rule.OutputClaimType = this.Right.ClaimType;
                this.rule.OutputClaimValue = this.Right.ClaimValue;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}