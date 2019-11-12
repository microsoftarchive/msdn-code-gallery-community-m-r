// 
//  (c) Microsoft Corporation. See LICENSE.TXT file for licensing details
//  
namespace Microsoft.ServiceBus.AccessControlExtensions.AccessControlManagement
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Data.Services.Client;
    using System.Data.Services.Common;

    /// <summary>
    ///   There are no comments for ManagementService in the schema.
    /// </summary>
    public partial class ManagementService : DataServiceContext
    {
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DataServiceQuery<ClaimType> _ClaimTypes;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DataServiceQuery<ConditionalRule> _ConditionalRules;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DataServiceQuery<Delegation> _Delegations;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DataServiceQuery<IdentityProviderAddress> _IdentityProviderAddresses;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DataServiceQuery<IdentityProviderClaimType> _IdentityProviderClaimTypes;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DataServiceQuery<IdentityProviderKey> _IdentityProviderKeys;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DataServiceQuery<IdentityProvider> _IdentityProviders;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DataServiceQuery<Issuer> _Issuers;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DataServiceQuery<RelyingParty> _RelyingParties;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DataServiceQuery<RelyingPartyAddress> _RelyingPartyAddresses;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DataServiceQuery<RelyingPartyIdentityProvider> _RelyingPartyIdentityProviders;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DataServiceQuery<RelyingPartyKey> _RelyingPartyKeys;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DataServiceQuery<RelyingPartyRuleGroup> _RelyingPartyRuleGroups;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DataServiceQuery<RuleGroup> _RuleGroups;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DataServiceQuery<Rule> _Rules;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DataServiceQuery<ServiceIdentity> _ServiceIdentities;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DataServiceQuery<ServiceIdentityKey> _ServiceIdentityKeys;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DataServiceQuery<ServiceKey> _ServiceKeys;

        /// <summary>
        ///   Initialize a new ManagementService object.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public ManagementService(Uri serviceRoot)
            : base(serviceRoot)
        {
            this.ResolveName = new Func<Type, string>(this.ResolveNameFromType);
            this.ResolveType = new Func<string, Type>(this.ResolveTypeFromName);
            this.OnContextCreated();
        }

        /// <summary>
        ///   There are no comments for ServiceKeys in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DataServiceQuery<ServiceKey> ServiceKeys
        {
            get
            {
                if ((this._ServiceKeys == null))
                {
                    this._ServiceKeys = base.CreateQuery<ServiceKey>("ServiceKeys");
                }
                return this._ServiceKeys;
            }
        }

        /// <summary>
        ///   There are no comments for IdentityProviders in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DataServiceQuery<IdentityProvider> IdentityProviders
        {
            get
            {
                if ((this._IdentityProviders == null))
                {
                    this._IdentityProviders = base.CreateQuery<IdentityProvider>("IdentityProviders");
                }
                return this._IdentityProviders;
            }
        }

        /// <summary>
        ///   There are no comments for IdentityProviderAddresses in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DataServiceQuery<IdentityProviderAddress> IdentityProviderAddresses
        {
            get
            {
                if ((this._IdentityProviderAddresses == null))
                {
                    this._IdentityProviderAddresses = base.CreateQuery<IdentityProviderAddress>("IdentityProviderAddresses");
                }
                return this._IdentityProviderAddresses;
            }
        }

        /// <summary>
        ///   There are no comments for IdentityProviderKeys in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DataServiceQuery<IdentityProviderKey> IdentityProviderKeys
        {
            get
            {
                if ((this._IdentityProviderKeys == null))
                {
                    this._IdentityProviderKeys = base.CreateQuery<IdentityProviderKey>("IdentityProviderKeys");
                }
                return this._IdentityProviderKeys;
            }
        }

        /// <summary>
        ///   There are no comments for ClaimTypes in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DataServiceQuery<ClaimType> ClaimTypes
        {
            get
            {
                if ((this._ClaimTypes == null))
                {
                    this._ClaimTypes = base.CreateQuery<ClaimType>("ClaimTypes");
                }
                return this._ClaimTypes;
            }
        }

        /// <summary>
        ///   There are no comments for IdentityProviderClaimTypes in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DataServiceQuery<IdentityProviderClaimType> IdentityProviderClaimTypes
        {
            get
            {
                if ((this._IdentityProviderClaimTypes == null))
                {
                    this._IdentityProviderClaimTypes = base.CreateQuery<IdentityProviderClaimType>("IdentityProviderClaimTypes");
                }
                return this._IdentityProviderClaimTypes;
            }
        }

        /// <summary>
        ///   There are no comments for Delegations in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DataServiceQuery<Delegation> Delegations
        {
            get
            {
                if ((this._Delegations == null))
                {
                    this._Delegations = base.CreateQuery<Delegation>("Delegations");
                }
                return this._Delegations;
            }
        }

        /// <summary>
        ///   There are no comments for RelyingParties in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DataServiceQuery<RelyingParty> RelyingParties
        {
            get
            {
                if ((this._RelyingParties == null))
                {
                    this._RelyingParties = base.CreateQuery<RelyingParty>("RelyingParties");
                }
                return this._RelyingParties;
            }
        }

        /// <summary>
        ///   There are no comments for RelyingPartyAddresses in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DataServiceQuery<RelyingPartyAddress> RelyingPartyAddresses
        {
            get
            {
                if ((this._RelyingPartyAddresses == null))
                {
                    this._RelyingPartyAddresses = base.CreateQuery<RelyingPartyAddress>("RelyingPartyAddresses");
                }
                return this._RelyingPartyAddresses;
            }
        }

        /// <summary>
        ///   There are no comments for RelyingPartyIdentityProviders in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DataServiceQuery<RelyingPartyIdentityProvider> RelyingPartyIdentityProviders
        {
            get
            {
                if ((this._RelyingPartyIdentityProviders == null))
                {
                    this._RelyingPartyIdentityProviders = base.CreateQuery<RelyingPartyIdentityProvider>("RelyingPartyIdentityProviders");
                }
                return this._RelyingPartyIdentityProviders;
            }
        }

        /// <summary>
        ///   There are no comments for RelyingPartyKeys in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DataServiceQuery<RelyingPartyKey> RelyingPartyKeys
        {
            get
            {
                if ((this._RelyingPartyKeys == null))
                {
                    this._RelyingPartyKeys = base.CreateQuery<RelyingPartyKey>("RelyingPartyKeys");
                }
                return this._RelyingPartyKeys;
            }
        }

        /// <summary>
        ///   There are no comments for ServiceIdentities in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DataServiceQuery<ServiceIdentity> ServiceIdentities
        {
            get
            {
                if ((this._ServiceIdentities == null))
                {
                    this._ServiceIdentities = base.CreateQuery<ServiceIdentity>("ServiceIdentities");
                }
                return this._ServiceIdentities;
            }
        }

        /// <summary>
        ///   There are no comments for ServiceIdentityKeys in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DataServiceQuery<ServiceIdentityKey> ServiceIdentityKeys
        {
            get
            {
                if ((this._ServiceIdentityKeys == null))
                {
                    this._ServiceIdentityKeys = base.CreateQuery<ServiceIdentityKey>("ServiceIdentityKeys");
                }
                return this._ServiceIdentityKeys;
            }
        }

        /// <summary>
        ///   There are no comments for RuleGroups in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DataServiceQuery<RuleGroup> RuleGroups
        {
            get
            {
                if ((this._RuleGroups == null))
                {
                    this._RuleGroups = base.CreateQuery<RuleGroup>("RuleGroups");
                }
                return this._RuleGroups;
            }
        }

        /// <summary>
        ///   There are no comments for RelyingPartyRuleGroups in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DataServiceQuery<RelyingPartyRuleGroup> RelyingPartyRuleGroups
        {
            get
            {
                if ((this._RelyingPartyRuleGroups == null))
                {
                    this._RelyingPartyRuleGroups = base.CreateQuery<RelyingPartyRuleGroup>("RelyingPartyRuleGroups");
                }
                return this._RelyingPartyRuleGroups;
            }
        }

        /// <summary>
        ///   There are no comments for Issuers in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DataServiceQuery<Issuer> Issuers
        {
            get
            {
                if ((this._Issuers == null))
                {
                    this._Issuers = base.CreateQuery<Issuer>("Issuers");
                }
                return this._Issuers;
            }
        }

        /// <summary>
        ///   There are no comments for Rules in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DataServiceQuery<Rule> Rules
        {
            get
            {
                if ((this._Rules == null))
                {
                    this._Rules = base.CreateQuery<Rule>("Rules");
                }
                return this._Rules;
            }
        }

        /// <summary>
        ///   There are no comments for ConditionalRules in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DataServiceQuery<ConditionalRule> ConditionalRules
        {
            get
            {
                if ((this._ConditionalRules == null))
                {
                    this._ConditionalRules = base.CreateQuery<ConditionalRule>("ConditionalRules");
                }
                return this._ConditionalRules;
            }
        }

        partial void OnContextCreated();

        /// <summary>
        ///   Since the namespace configured for this service reference
        ///   in Visual Studio is different from the one indicated in the
        ///   server schema, use type-mappers to map between the two.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        protected Type ResolveTypeFromName(string typeName)
        {
            if (typeName.StartsWith("Microsoft.Cloud.AccessControl.Management", StringComparison.Ordinal))
            {
                return
                    this.GetType().Assembly.GetType(
                                                    string.Concat(
                                                                  "Microsoft.ServiceBus.AccessControlExtensions.AccessControlManagement", typeName.Substring(40)),
                                                    false);
            }
            return null;
        }

        /// <summary>
        ///   Since the namespace configured for this service reference
        ///   in Visual Studio is different from the one indicated in the
        ///   server schema, use type-mappers to map between the two.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        protected string ResolveNameFromType(Type clientType)
        {
            if (clientType.Namespace.Equals("Microsoft.ServiceBus.AccessControlExtensions.AccessControlManagement", StringComparison.Ordinal))
            {
                return string.Concat("Microsoft.Cloud.AccessControl.Management.", clientType.Name);
            }
            return null;
        }

        /// <summary>
        ///   There are no comments for ServiceKeys in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public void AddToServiceKeys(ServiceKey serviceKey)
        {
            base.AddObject("ServiceKeys", serviceKey);
        }

        /// <summary>
        ///   There are no comments for IdentityProviders in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public void AddToIdentityProviders(IdentityProvider identityProvider)
        {
            base.AddObject("IdentityProviders", identityProvider);
        }

        /// <summary>
        ///   There are no comments for IdentityProviderAddresses in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public void AddToIdentityProviderAddresses(IdentityProviderAddress identityProviderAddress)
        {
            base.AddObject("IdentityProviderAddresses", identityProviderAddress);
        }

        /// <summary>
        ///   There are no comments for IdentityProviderKeys in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public void AddToIdentityProviderKeys(IdentityProviderKey identityProviderKey)
        {
            base.AddObject("IdentityProviderKeys", identityProviderKey);
        }

        /// <summary>
        ///   There are no comments for ClaimTypes in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public void AddToClaimTypes(ClaimType claimType)
        {
            base.AddObject("ClaimTypes", claimType);
        }

        /// <summary>
        ///   There are no comments for IdentityProviderClaimTypes in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public void AddToIdentityProviderClaimTypes(IdentityProviderClaimType identityProviderClaimType)
        {
            base.AddObject("IdentityProviderClaimTypes", identityProviderClaimType);
        }

        /// <summary>
        ///   There are no comments for Delegations in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public void AddToDelegations(Delegation delegation)
        {
            base.AddObject("Delegations", delegation);
        }

        /// <summary>
        ///   There are no comments for RelyingParties in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public void AddToRelyingParties(RelyingParty relyingParty)
        {
            base.AddObject("RelyingParties", relyingParty);
        }

        /// <summary>
        ///   There are no comments for RelyingPartyAddresses in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public void AddToRelyingPartyAddresses(RelyingPartyAddress relyingPartyAddress)
        {
            base.AddObject("RelyingPartyAddresses", relyingPartyAddress);
        }

        /// <summary>
        ///   There are no comments for RelyingPartyIdentityProviders in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public void AddToRelyingPartyIdentityProviders(RelyingPartyIdentityProvider relyingPartyIdentityProvider)
        {
            base.AddObject("RelyingPartyIdentityProviders", relyingPartyIdentityProvider);
        }

        /// <summary>
        ///   There are no comments for RelyingPartyKeys in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public void AddToRelyingPartyKeys(RelyingPartyKey relyingPartyKey)
        {
            base.AddObject("RelyingPartyKeys", relyingPartyKey);
        }

        /// <summary>
        ///   There are no comments for ServiceIdentities in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public void AddToServiceIdentities(ServiceIdentity serviceIdentity)
        {
            base.AddObject("ServiceIdentities", serviceIdentity);
        }

        /// <summary>
        ///   There are no comments for ServiceIdentityKeys in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public void AddToServiceIdentityKeys(ServiceIdentityKey serviceIdentityKey)
        {
            base.AddObject("ServiceIdentityKeys", serviceIdentityKey);
        }

        /// <summary>
        ///   There are no comments for RuleGroups in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public void AddToRuleGroups(RuleGroup ruleGroup)
        {
            base.AddObject("RuleGroups", ruleGroup);
        }

        /// <summary>
        ///   There are no comments for RelyingPartyRuleGroups in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public void AddToRelyingPartyRuleGroups(RelyingPartyRuleGroup relyingPartyRuleGroup)
        {
            base.AddObject("RelyingPartyRuleGroups", relyingPartyRuleGroup);
        }

        /// <summary>
        ///   There are no comments for Issuers in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public void AddToIssuers(Issuer issuer)
        {
            base.AddObject("Issuers", issuer);
        }

        /// <summary>
        ///   There are no comments for Rules in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public void AddToRules(Rule rule)
        {
            base.AddObject("Rules", rule);
        }

        /// <summary>
        ///   There are no comments for ConditionalRules in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public void AddToConditionalRules(ConditionalRule conditionalRule)
        {
            base.AddObject("ConditionalRules", conditionalRule);
        }
    }

    /// <summary>
    ///   There are no comments for Microsoft.Cloud.AccessControl.Management.ServiceKey in the schema.
    /// </summary>
    /// <KeyProperties>
    ///   Id
    /// </KeyProperties>
    [EntitySet("ServiceKeys")]
    [DataServiceKey("Id")]
    public partial class ServiceKey : INotifyPropertyChanged
    {
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _DisplayName;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DateTime _EndDate;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        long _Id;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        bool _IsPrimary;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        byte[] _Password;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DateTime _StartDate;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        bool _SystemReserved;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _Type;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _Usage;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        byte[] _Value;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        byte[] _Version;

        /// <summary>
        ///   There are no comments for Property Id in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public long Id
        {
            get { return this._Id; }
            set
            {
                this.OnIdChanging(value);
                this._Id = value;
                this.OnIdChanged();
                this.OnPropertyChanged("Id");
            }
        }

        /// <summary>
        ///   There are no comments for Property Usage in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string Usage
        {
            get { return this._Usage; }
            set
            {
                this.OnUsageChanging(value);
                this._Usage = value;
                this.OnUsageChanged();
                this.OnPropertyChanged("Usage");
            }
        }

        /// <summary>
        ///   There are no comments for Property Type in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string Type
        {
            get { return this._Type; }
            set
            {
                this.OnTypeChanging(value);
                this._Type = value;
                this.OnTypeChanged();
                this.OnPropertyChanged("Type");
            }
        }

        /// <summary>
        ///   There are no comments for Property Value in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public byte[] Value
        {
            get
            {
                if ((this._Value != null))
                {
                    return ((byte[]) (this._Value.Clone()));
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.OnValueChanging(value);
                this._Value = value;
                this.OnValueChanged();
                this.OnPropertyChanged("Value");
            }
        }

        /// <summary>
        ///   There are no comments for Property Password in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public byte[] Password
        {
            get
            {
                if ((this._Password != null))
                {
                    return ((byte[]) (this._Password.Clone()));
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.OnPasswordChanging(value);
                this._Password = value;
                this.OnPasswordChanged();
                this.OnPropertyChanged("Password");
            }
        }

        /// <summary>
        ///   There are no comments for Property IsPrimary in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public bool IsPrimary
        {
            get { return this._IsPrimary; }
            set
            {
                this.OnIsPrimaryChanging(value);
                this._IsPrimary = value;
                this.OnIsPrimaryChanged();
                this.OnPropertyChanged("IsPrimary");
            }
        }

        /// <summary>
        ///   There are no comments for Property SystemReserved in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public bool SystemReserved
        {
            get { return this._SystemReserved; }
            set
            {
                this.OnSystemReservedChanging(value);
                this._SystemReserved = value;
                this.OnSystemReservedChanged();
                this.OnPropertyChanged("SystemReserved");
            }
        }

        /// <summary>
        ///   There are no comments for Property StartDate in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DateTime StartDate
        {
            get { return this._StartDate; }
            set
            {
                this.OnStartDateChanging(value);
                this._StartDate = value;
                this.OnStartDateChanged();
                this.OnPropertyChanged("StartDate");
            }
        }

        /// <summary>
        ///   There are no comments for Property EndDate in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DateTime EndDate
        {
            get { return this._EndDate; }
            set
            {
                this.OnEndDateChanging(value);
                this._EndDate = value;
                this.OnEndDateChanged();
                this.OnPropertyChanged("EndDate");
            }
        }

        /// <summary>
        ///   There are no comments for Property DisplayName in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string DisplayName
        {
            get { return this._DisplayName; }
            set
            {
                this.OnDisplayNameChanging(value);
                this._DisplayName = value;
                this.OnDisplayNameChanged();
                this.OnPropertyChanged("DisplayName");
            }
        }

        /// <summary>
        ///   There are no comments for Property Version in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public byte[] Version
        {
            get
            {
                if ((this._Version != null))
                {
                    return ((byte[]) (this._Version.Clone()));
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.OnVersionChanging(value);
                this._Version = value;
                this.OnVersionChanged();
                this.OnPropertyChanged("Version");
            }
        }

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///   Create a new ServiceKey object.
        /// </summary>
        /// <param name = "ID">Initial value of Id.</param>
        /// <param name = "isPrimary">Initial value of IsPrimary.</param>
        /// <param name = "systemReserved">Initial value of SystemReserved.</param>
        /// <param name = "startDate">Initial value of StartDate.</param>
        /// <param name = "endDate">Initial value of EndDate.</param>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public static ServiceKey CreateServiceKey(long ID, bool isPrimary, bool systemReserved, DateTime startDate, DateTime endDate)
        {
            var serviceKey = new ServiceKey();
            serviceKey.Id = ID;
            serviceKey.IsPrimary = isPrimary;
            serviceKey.SystemReserved = systemReserved;
            serviceKey.StartDate = startDate;
            serviceKey.EndDate = endDate;
            return serviceKey;
        }

        partial void OnIdChanging(long value);
        partial void OnIdChanged();

        partial void OnUsageChanging(string value);
        partial void OnUsageChanged();

        partial void OnTypeChanging(string value);
        partial void OnTypeChanged();

        partial void OnValueChanging(byte[] value);
        partial void OnValueChanged();

        partial void OnPasswordChanging(byte[] value);
        partial void OnPasswordChanged();

        partial void OnIsPrimaryChanging(bool value);
        partial void OnIsPrimaryChanged();

        partial void OnSystemReservedChanging(bool value);
        partial void OnSystemReservedChanged();

        partial void OnStartDateChanging(DateTime value);
        partial void OnStartDateChanged();

        partial void OnEndDateChanging(DateTime value);
        partial void OnEndDateChanged();

        partial void OnDisplayNameChanging(string value);
        partial void OnDisplayNameChanged();

        partial void OnVersionChanging(byte[] value);
        partial void OnVersionChanged();

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }

    /// <summary>
    ///   There are no comments for Microsoft.Cloud.AccessControl.Management.IdentityProvider in the schema.
    /// </summary>
    /// <KeyProperties>
    ///   Id
    /// </KeyProperties>
    [EntitySet("IdentityProviders")]
    [DataServiceKey("Id")]
    public partial class IdentityProvider : INotifyPropertyChanged
    {
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _Description;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _DisplayName;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        long _Id;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DataServiceCollection<IdentityProviderAddress> _IdentityProviderAddresses = new DataServiceCollection<IdentityProviderAddress>(null, TrackingMode.None);

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DataServiceCollection<IdentityProviderClaimType> _IdentityProviderClaimTypes = new DataServiceCollection<IdentityProviderClaimType>(
            null, TrackingMode.None);

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DataServiceCollection<IdentityProviderKey> _IdentityProviderKeys = new DataServiceCollection<IdentityProviderKey>(null, TrackingMode.None);

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        Issuer _Issuer;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        long _IssuerId;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _LoginLinkName;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _LoginParameters;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _Realm;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DataServiceCollection<RelyingPartyIdentityProvider> _RelyingPartyIdentityProviders = new DataServiceCollection<RelyingPartyIdentityProvider>(
            null, TrackingMode.None);

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        bool _SystemReserved;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        byte[] _Version;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _WebSSOProtocolType;

        /// <summary>
        ///   There are no comments for Property Id in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public long Id
        {
            get { return this._Id; }
            set
            {
                this.OnIdChanging(value);
                this._Id = value;
                this.OnIdChanged();
                this.OnPropertyChanged("Id");
            }
        }

        /// <summary>
        ///   There are no comments for Property IssuerId in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public long IssuerId
        {
            get { return this._IssuerId; }
            set
            {
                this.OnIssuerIdChanging(value);
                this._IssuerId = value;
                this.OnIssuerIdChanged();
                this.OnPropertyChanged("IssuerId");
            }
        }

        /// <summary>
        ///   There are no comments for Property DisplayName in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string DisplayName
        {
            get { return this._DisplayName; }
            set
            {
                this.OnDisplayNameChanging(value);
                this._DisplayName = value;
                this.OnDisplayNameChanged();
                this.OnPropertyChanged("DisplayName");
            }
        }

        /// <summary>
        ///   There are no comments for Property Description in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string Description
        {
            get { return this._Description; }
            set
            {
                this.OnDescriptionChanging(value);
                this._Description = value;
                this.OnDescriptionChanged();
                this.OnPropertyChanged("Description");
            }
        }

        /// <summary>
        ///   There are no comments for Property WebSSOProtocolType in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string WebSSOProtocolType
        {
            get { return this._WebSSOProtocolType; }
            set
            {
                this.OnWebSSOProtocolTypeChanging(value);
                this._WebSSOProtocolType = value;
                this.OnWebSSOProtocolTypeChanged();
                this.OnPropertyChanged("WebSSOProtocolType");
            }
        }

        /// <summary>
        ///   There are no comments for Property Realm in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string Realm
        {
            get { return this._Realm; }
            set
            {
                this.OnRealmChanging(value);
                this._Realm = value;
                this.OnRealmChanged();
                this.OnPropertyChanged("Realm");
            }
        }

        /// <summary>
        ///   There are no comments for Property LoginLinkName in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string LoginLinkName
        {
            get { return this._LoginLinkName; }
            set
            {
                this.OnLoginLinkNameChanging(value);
                this._LoginLinkName = value;
                this.OnLoginLinkNameChanged();
                this.OnPropertyChanged("LoginLinkName");
            }
        }

        /// <summary>
        ///   There are no comments for Property LoginParameters in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string LoginParameters
        {
            get { return this._LoginParameters; }
            set
            {
                this.OnLoginParametersChanging(value);
                this._LoginParameters = value;
                this.OnLoginParametersChanged();
                this.OnPropertyChanged("LoginParameters");
            }
        }

        /// <summary>
        ///   There are no comments for Property SystemReserved in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public bool SystemReserved
        {
            get { return this._SystemReserved; }
            set
            {
                this.OnSystemReservedChanging(value);
                this._SystemReserved = value;
                this.OnSystemReservedChanged();
                this.OnPropertyChanged("SystemReserved");
            }
        }

        /// <summary>
        ///   There are no comments for Property Version in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public byte[] Version
        {
            get
            {
                if ((this._Version != null))
                {
                    return ((byte[]) (this._Version.Clone()));
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.OnVersionChanging(value);
                this._Version = value;
                this.OnVersionChanged();
                this.OnPropertyChanged("Version");
            }
        }

        /// <summary>
        ///   There are no comments for IdentityProviderAddresses in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DataServiceCollection<IdentityProviderAddress> IdentityProviderAddresses
        {
            get { return this._IdentityProviderAddresses; }
            set
            {
                this._IdentityProviderAddresses = value;
                this.OnPropertyChanged("IdentityProviderAddresses");
            }
        }

        /// <summary>
        ///   There are no comments for IdentityProviderClaimTypes in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DataServiceCollection<IdentityProviderClaimType> IdentityProviderClaimTypes
        {
            get { return this._IdentityProviderClaimTypes; }
            set
            {
                this._IdentityProviderClaimTypes = value;
                this.OnPropertyChanged("IdentityProviderClaimTypes");
            }
        }

        /// <summary>
        ///   There are no comments for IdentityProviderKeys in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DataServiceCollection<IdentityProviderKey> IdentityProviderKeys
        {
            get { return this._IdentityProviderKeys; }
            set
            {
                this._IdentityProviderKeys = value;
                this.OnPropertyChanged("IdentityProviderKeys");
            }
        }

        /// <summary>
        ///   There are no comments for Issuer in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public Issuer Issuer
        {
            get { return this._Issuer; }
            set
            {
                this._Issuer = value;
                this.OnPropertyChanged("Issuer");
            }
        }

        /// <summary>
        ///   There are no comments for RelyingPartyIdentityProviders in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DataServiceCollection<RelyingPartyIdentityProvider> RelyingPartyIdentityProviders
        {
            get { return this._RelyingPartyIdentityProviders; }
            set
            {
                this._RelyingPartyIdentityProviders = value;
                this.OnPropertyChanged("RelyingPartyIdentityProviders");
            }
        }

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///   Create a new IdentityProvider object.
        /// </summary>
        /// <param name = "ID">Initial value of Id.</param>
        /// <param name = "issuerId">Initial value of IssuerId.</param>
        /// <param name = "systemReserved">Initial value of SystemReserved.</param>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public static IdentityProvider CreateIdentityProvider(long ID, long issuerId, bool systemReserved)
        {
            var identityProvider = new IdentityProvider();
            identityProvider.Id = ID;
            identityProvider.IssuerId = issuerId;
            identityProvider.SystemReserved = systemReserved;
            return identityProvider;
        }

        partial void OnIdChanging(long value);
        partial void OnIdChanged();

        partial void OnIssuerIdChanging(long value);
        partial void OnIssuerIdChanged();

        partial void OnDisplayNameChanging(string value);
        partial void OnDisplayNameChanged();

        partial void OnDescriptionChanging(string value);
        partial void OnDescriptionChanged();

        partial void OnWebSSOProtocolTypeChanging(string value);
        partial void OnWebSSOProtocolTypeChanged();

        partial void OnRealmChanging(string value);
        partial void OnRealmChanged();

        partial void OnLoginLinkNameChanging(string value);
        partial void OnLoginLinkNameChanged();

        partial void OnLoginParametersChanging(string value);
        partial void OnLoginParametersChanged();

        partial void OnSystemReservedChanging(bool value);
        partial void OnSystemReservedChanged();

        partial void OnVersionChanging(byte[] value);
        partial void OnVersionChanged();

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }

    /// <summary>
    ///   There are no comments for Microsoft.Cloud.AccessControl.Management.IdentityProviderAddress in the schema.
    /// </summary>
    /// <KeyProperties>
    ///   Id
    /// </KeyProperties>
    [EntitySet("IdentityProviderAddresses")]
    [DataServiceKey("Id")]
    public partial class IdentityProviderAddress : INotifyPropertyChanged
    {
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _Address;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _EndpointType;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        long _Id;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        IdentityProvider _IdentityProvider;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        long _IdentityProviderId;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        bool _SystemReserved;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        byte[] _Version;

        /// <summary>
        ///   There are no comments for Property Id in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public long Id
        {
            get { return this._Id; }
            set
            {
                this.OnIdChanging(value);
                this._Id = value;
                this.OnIdChanged();
                this.OnPropertyChanged("Id");
            }
        }

        /// <summary>
        ///   There are no comments for Property IdentityProviderId in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public long IdentityProviderId
        {
            get { return this._IdentityProviderId; }
            set
            {
                this.OnIdentityProviderIdChanging(value);
                this._IdentityProviderId = value;
                this.OnIdentityProviderIdChanged();
                this.OnPropertyChanged("IdentityProviderId");
            }
        }

        /// <summary>
        ///   There are no comments for Property EndpointType in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string EndpointType
        {
            get { return this._EndpointType; }
            set
            {
                this.OnEndpointTypeChanging(value);
                this._EndpointType = value;
                this.OnEndpointTypeChanged();
                this.OnPropertyChanged("EndpointType");
            }
        }

        /// <summary>
        ///   There are no comments for Property Address in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string Address
        {
            get { return this._Address; }
            set
            {
                this.OnAddressChanging(value);
                this._Address = value;
                this.OnAddressChanged();
                this.OnPropertyChanged("Address");
            }
        }

        /// <summary>
        ///   There are no comments for Property SystemReserved in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public bool SystemReserved
        {
            get { return this._SystemReserved; }
            set
            {
                this.OnSystemReservedChanging(value);
                this._SystemReserved = value;
                this.OnSystemReservedChanged();
                this.OnPropertyChanged("SystemReserved");
            }
        }

        /// <summary>
        ///   There are no comments for Property Version in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public byte[] Version
        {
            get
            {
                if ((this._Version != null))
                {
                    return ((byte[]) (this._Version.Clone()));
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.OnVersionChanging(value);
                this._Version = value;
                this.OnVersionChanged();
                this.OnPropertyChanged("Version");
            }
        }

        /// <summary>
        ///   There are no comments for IdentityProvider in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public IdentityProvider IdentityProvider
        {
            get { return this._IdentityProvider; }
            set
            {
                this._IdentityProvider = value;
                this.OnPropertyChanged("IdentityProvider");
            }
        }

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///   Create a new IdentityProviderAddress object.
        /// </summary>
        /// <param name = "ID">Initial value of Id.</param>
        /// <param name = "identityProviderId">Initial value of IdentityProviderId.</param>
        /// <param name = "systemReserved">Initial value of SystemReserved.</param>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public static IdentityProviderAddress CreateIdentityProviderAddress(long ID, long identityProviderId, bool systemReserved)
        {
            var identityProviderAddress = new IdentityProviderAddress();
            identityProviderAddress.Id = ID;
            identityProviderAddress.IdentityProviderId = identityProviderId;
            identityProviderAddress.SystemReserved = systemReserved;
            return identityProviderAddress;
        }

        partial void OnIdChanging(long value);
        partial void OnIdChanged();

        partial void OnIdentityProviderIdChanging(long value);
        partial void OnIdentityProviderIdChanged();

        partial void OnEndpointTypeChanging(string value);
        partial void OnEndpointTypeChanged();

        partial void OnAddressChanging(string value);
        partial void OnAddressChanged();

        partial void OnSystemReservedChanging(bool value);
        partial void OnSystemReservedChanged();

        partial void OnVersionChanging(byte[] value);
        partial void OnVersionChanged();

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }

    /// <summary>
    ///   There are no comments for Microsoft.Cloud.AccessControl.Management.IdentityProviderKey in the schema.
    /// </summary>
    /// <KeyProperties>
    ///   Id
    /// </KeyProperties>
    [EntitySet("IdentityProviderKeys")]
    [DataServiceKey("Id")]
    public partial class IdentityProviderKey : INotifyPropertyChanged
    {
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _DisplayName;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DateTime _EndDate;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        long _Id;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        IdentityProvider _IdentityProvider;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        long _IdentityProviderId;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        byte[] _Password;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DateTime _StartDate;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        bool _SystemReserved;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _Type;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _Usage;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        byte[] _Value;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        byte[] _Version;

        /// <summary>
        ///   There are no comments for Property Id in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public long Id
        {
            get { return this._Id; }
            set
            {
                this.OnIdChanging(value);
                this._Id = value;
                this.OnIdChanged();
                this.OnPropertyChanged("Id");
            }
        }

        /// <summary>
        ///   There are no comments for Property IdentityProviderId in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public long IdentityProviderId
        {
            get { return this._IdentityProviderId; }
            set
            {
                this.OnIdentityProviderIdChanging(value);
                this._IdentityProviderId = value;
                this.OnIdentityProviderIdChanged();
                this.OnPropertyChanged("IdentityProviderId");
            }
        }

        /// <summary>
        ///   There are no comments for Property DisplayName in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string DisplayName
        {
            get { return this._DisplayName; }
            set
            {
                this.OnDisplayNameChanging(value);
                this._DisplayName = value;
                this.OnDisplayNameChanged();
                this.OnPropertyChanged("DisplayName");
            }
        }

        /// <summary>
        ///   There are no comments for Property Usage in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string Usage
        {
            get { return this._Usage; }
            set
            {
                this.OnUsageChanging(value);
                this._Usage = value;
                this.OnUsageChanged();
                this.OnPropertyChanged("Usage");
            }
        }

        /// <summary>
        ///   There are no comments for Property Type in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string Type
        {
            get { return this._Type; }
            set
            {
                this.OnTypeChanging(value);
                this._Type = value;
                this.OnTypeChanged();
                this.OnPropertyChanged("Type");
            }
        }

        /// <summary>
        ///   There are no comments for Property Value in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public byte[] Value
        {
            get
            {
                if ((this._Value != null))
                {
                    return ((byte[]) (this._Value.Clone()));
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.OnValueChanging(value);
                this._Value = value;
                this.OnValueChanged();
                this.OnPropertyChanged("Value");
            }
        }

        /// <summary>
        ///   There are no comments for Property Password in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public byte[] Password
        {
            get
            {
                if ((this._Password != null))
                {
                    return ((byte[]) (this._Password.Clone()));
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.OnPasswordChanging(value);
                this._Password = value;
                this.OnPasswordChanged();
                this.OnPropertyChanged("Password");
            }
        }

        /// <summary>
        ///   There are no comments for Property SystemReserved in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public bool SystemReserved
        {
            get { return this._SystemReserved; }
            set
            {
                this.OnSystemReservedChanging(value);
                this._SystemReserved = value;
                this.OnSystemReservedChanged();
                this.OnPropertyChanged("SystemReserved");
            }
        }

        /// <summary>
        ///   There are no comments for Property StartDate in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DateTime StartDate
        {
            get { return this._StartDate; }
            set
            {
                this.OnStartDateChanging(value);
                this._StartDate = value;
                this.OnStartDateChanged();
                this.OnPropertyChanged("StartDate");
            }
        }

        /// <summary>
        ///   There are no comments for Property EndDate in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DateTime EndDate
        {
            get { return this._EndDate; }
            set
            {
                this.OnEndDateChanging(value);
                this._EndDate = value;
                this.OnEndDateChanged();
                this.OnPropertyChanged("EndDate");
            }
        }

        /// <summary>
        ///   There are no comments for Property Version in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public byte[] Version
        {
            get
            {
                if ((this._Version != null))
                {
                    return ((byte[]) (this._Version.Clone()));
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.OnVersionChanging(value);
                this._Version = value;
                this.OnVersionChanged();
                this.OnPropertyChanged("Version");
            }
        }

        /// <summary>
        ///   There are no comments for IdentityProvider in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public IdentityProvider IdentityProvider
        {
            get { return this._IdentityProvider; }
            set
            {
                this._IdentityProvider = value;
                this.OnPropertyChanged("IdentityProvider");
            }
        }

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///   Create a new IdentityProviderKey object.
        /// </summary>
        /// <param name = "ID">Initial value of Id.</param>
        /// <param name = "identityProviderId">Initial value of IdentityProviderId.</param>
        /// <param name = "systemReserved">Initial value of SystemReserved.</param>
        /// <param name = "startDate">Initial value of StartDate.</param>
        /// <param name = "endDate">Initial value of EndDate.</param>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public static IdentityProviderKey CreateIdentityProviderKey(long ID, long identityProviderId, bool systemReserved, DateTime startDate, DateTime endDate)
        {
            var identityProviderKey = new IdentityProviderKey();
            identityProviderKey.Id = ID;
            identityProviderKey.IdentityProviderId = identityProviderId;
            identityProviderKey.SystemReserved = systemReserved;
            identityProviderKey.StartDate = startDate;
            identityProviderKey.EndDate = endDate;
            return identityProviderKey;
        }

        partial void OnIdChanging(long value);
        partial void OnIdChanged();

        partial void OnIdentityProviderIdChanging(long value);
        partial void OnIdentityProviderIdChanged();

        partial void OnDisplayNameChanging(string value);
        partial void OnDisplayNameChanged();

        partial void OnUsageChanging(string value);
        partial void OnUsageChanged();

        partial void OnTypeChanging(string value);
        partial void OnTypeChanged();

        partial void OnValueChanging(byte[] value);
        partial void OnValueChanged();

        partial void OnPasswordChanging(byte[] value);
        partial void OnPasswordChanged();

        partial void OnSystemReservedChanging(bool value);
        partial void OnSystemReservedChanged();

        partial void OnStartDateChanging(DateTime value);
        partial void OnStartDateChanged();

        partial void OnEndDateChanging(DateTime value);
        partial void OnEndDateChanged();

        partial void OnVersionChanging(byte[] value);
        partial void OnVersionChanged();

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }

    /// <summary>
    ///   There are no comments for Microsoft.Cloud.AccessControl.Management.ClaimType in the schema.
    /// </summary>
    /// <KeyProperties>
    ///   Id
    /// </KeyProperties>
    [EntitySet("ClaimTypes")]
    [DataServiceKey("Id")]
    public partial class ClaimType : INotifyPropertyChanged
    {
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        long _Id;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DataServiceCollection<IdentityProviderClaimType> _IdentityProviderClaimTypes = new DataServiceCollection<IdentityProviderClaimType>(
            null, TrackingMode.None);

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        bool _SystemReserved;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _Uri;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        byte[] _Version;

        /// <summary>
        ///   There are no comments for Property Id in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public long Id
        {
            get { return this._Id; }
            set
            {
                this.OnIdChanging(value);
                this._Id = value;
                this.OnIdChanged();
                this.OnPropertyChanged("Id");
            }
        }

        /// <summary>
        ///   There are no comments for Property Uri in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string Uri
        {
            get { return this._Uri; }
            set
            {
                this.OnUriChanging(value);
                this._Uri = value;
                this.OnUriChanged();
                this.OnPropertyChanged("Uri");
            }
        }

        /// <summary>
        ///   There are no comments for Property SystemReserved in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public bool SystemReserved
        {
            get { return this._SystemReserved; }
            set
            {
                this.OnSystemReservedChanging(value);
                this._SystemReserved = value;
                this.OnSystemReservedChanged();
                this.OnPropertyChanged("SystemReserved");
            }
        }

        /// <summary>
        ///   There are no comments for Property Version in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public byte[] Version
        {
            get
            {
                if ((this._Version != null))
                {
                    return ((byte[]) (this._Version.Clone()));
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.OnVersionChanging(value);
                this._Version = value;
                this.OnVersionChanged();
                this.OnPropertyChanged("Version");
            }
        }

        /// <summary>
        ///   There are no comments for IdentityProviderClaimTypes in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DataServiceCollection<IdentityProviderClaimType> IdentityProviderClaimTypes
        {
            get { return this._IdentityProviderClaimTypes; }
            set
            {
                this._IdentityProviderClaimTypes = value;
                this.OnPropertyChanged("IdentityProviderClaimTypes");
            }
        }

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///   Create a new ClaimType object.
        /// </summary>
        /// <param name = "ID">Initial value of Id.</param>
        /// <param name = "systemReserved">Initial value of SystemReserved.</param>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public static ClaimType CreateClaimType(long ID, bool systemReserved)
        {
            var claimType = new ClaimType();
            claimType.Id = ID;
            claimType.SystemReserved = systemReserved;
            return claimType;
        }

        partial void OnIdChanging(long value);
        partial void OnIdChanged();

        partial void OnUriChanging(string value);
        partial void OnUriChanged();

        partial void OnSystemReservedChanging(bool value);
        partial void OnSystemReservedChanged();

        partial void OnVersionChanging(byte[] value);
        partial void OnVersionChanged();

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }

    /// <summary>
    ///   There are no comments for Microsoft.Cloud.AccessControl.Management.IdentityProviderClaimType in the schema.
    /// </summary>
    /// <KeyProperties>
    ///   Id
    /// </KeyProperties>
    [EntitySet("IdentityProviderClaimTypes")]
    [DataServiceKey("Id")]
    public partial class IdentityProviderClaimType : INotifyPropertyChanged
    {
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        ClaimType _ClaimType;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        long _ClaimTypeId;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        long _Id;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        IdentityProvider _IdentityProvider;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        long _IdentityProviderId;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        bool _SystemReserved;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        byte[] _Version;

        /// <summary>
        ///   There are no comments for Property Id in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public long Id
        {
            get { return this._Id; }
            set
            {
                this.OnIdChanging(value);
                this._Id = value;
                this.OnIdChanged();
                this.OnPropertyChanged("Id");
            }
        }

        /// <summary>
        ///   There are no comments for Property ClaimTypeId in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public long ClaimTypeId
        {
            get { return this._ClaimTypeId; }
            set
            {
                this.OnClaimTypeIdChanging(value);
                this._ClaimTypeId = value;
                this.OnClaimTypeIdChanged();
                this.OnPropertyChanged("ClaimTypeId");
            }
        }

        /// <summary>
        ///   There are no comments for Property IdentityProviderId in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public long IdentityProviderId
        {
            get { return this._IdentityProviderId; }
            set
            {
                this.OnIdentityProviderIdChanging(value);
                this._IdentityProviderId = value;
                this.OnIdentityProviderIdChanged();
                this.OnPropertyChanged("IdentityProviderId");
            }
        }

        /// <summary>
        ///   There are no comments for Property SystemReserved in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public bool SystemReserved
        {
            get { return this._SystemReserved; }
            set
            {
                this.OnSystemReservedChanging(value);
                this._SystemReserved = value;
                this.OnSystemReservedChanged();
                this.OnPropertyChanged("SystemReserved");
            }
        }

        /// <summary>
        ///   There are no comments for Property Version in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public byte[] Version
        {
            get
            {
                if ((this._Version != null))
                {
                    return ((byte[]) (this._Version.Clone()));
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.OnVersionChanging(value);
                this._Version = value;
                this.OnVersionChanged();
                this.OnPropertyChanged("Version");
            }
        }

        /// <summary>
        ///   There are no comments for ClaimType in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public ClaimType ClaimType
        {
            get { return this._ClaimType; }
            set
            {
                this._ClaimType = value;
                this.OnPropertyChanged("ClaimType");
            }
        }

        /// <summary>
        ///   There are no comments for IdentityProvider in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public IdentityProvider IdentityProvider
        {
            get { return this._IdentityProvider; }
            set
            {
                this._IdentityProvider = value;
                this.OnPropertyChanged("IdentityProvider");
            }
        }

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///   Create a new IdentityProviderClaimType object.
        /// </summary>
        /// <param name = "ID">Initial value of Id.</param>
        /// <param name = "claimTypeId">Initial value of ClaimTypeId.</param>
        /// <param name = "identityProviderId">Initial value of IdentityProviderId.</param>
        /// <param name = "systemReserved">Initial value of SystemReserved.</param>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public static IdentityProviderClaimType CreateIdentityProviderClaimType(long ID, long claimTypeId, long identityProviderId, bool systemReserved)
        {
            var identityProviderClaimType = new IdentityProviderClaimType();
            identityProviderClaimType.Id = ID;
            identityProviderClaimType.ClaimTypeId = claimTypeId;
            identityProviderClaimType.IdentityProviderId = identityProviderId;
            identityProviderClaimType.SystemReserved = systemReserved;
            return identityProviderClaimType;
        }

        partial void OnIdChanging(long value);
        partial void OnIdChanged();

        partial void OnClaimTypeIdChanging(long value);
        partial void OnClaimTypeIdChanged();

        partial void OnIdentityProviderIdChanging(long value);
        partial void OnIdentityProviderIdChanged();

        partial void OnSystemReservedChanging(bool value);
        partial void OnSystemReservedChanged();

        partial void OnVersionChanging(byte[] value);
        partial void OnVersionChanged();

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }

    /// <summary>
    ///   There are no comments for Microsoft.Cloud.AccessControl.Management.Delegation in the schema.
    /// </summary>
    /// <KeyProperties>
    ///   Id
    /// </KeyProperties>
    [EntitySet("Delegations")]
    [DataServiceKey("Id")]
    public partial class Delegation : INotifyPropertyChanged
    {
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _AuthorizationCode;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        long _Id;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _IdentityProvider;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _NameIdentifier;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _Permissions;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        RelyingParty _RelyingParty;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        long _RelyingPartyId;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        ServiceIdentity _ServiceIdentity;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        long _ServiceIdentityId;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        bool _SystemReserved;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        byte[] _Version;

        /// <summary>
        ///   There are no comments for Property Id in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public long Id
        {
            get { return this._Id; }
            set
            {
                this.OnIdChanging(value);
                this._Id = value;
                this.OnIdChanged();
                this.OnPropertyChanged("Id");
            }
        }

        /// <summary>
        ///   There are no comments for Property ServiceIdentityId in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public long ServiceIdentityId
        {
            get { return this._ServiceIdentityId; }
            set
            {
                this.OnServiceIdentityIdChanging(value);
                this._ServiceIdentityId = value;
                this.OnServiceIdentityIdChanged();
                this.OnPropertyChanged("ServiceIdentityId");
            }
        }

        /// <summary>
        ///   There are no comments for Property RelyingPartyId in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public long RelyingPartyId
        {
            get { return this._RelyingPartyId; }
            set
            {
                this.OnRelyingPartyIdChanging(value);
                this._RelyingPartyId = value;
                this.OnRelyingPartyIdChanged();
                this.OnPropertyChanged("RelyingPartyId");
            }
        }

        /// <summary>
        ///   There are no comments for Property IdentityProvider in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string IdentityProvider
        {
            get { return this._IdentityProvider; }
            set
            {
                this.OnIdentityProviderChanging(value);
                this._IdentityProvider = value;
                this.OnIdentityProviderChanged();
                this.OnPropertyChanged("IdentityProvider");
            }
        }

        /// <summary>
        ///   There are no comments for Property NameIdentifier in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string NameIdentifier
        {
            get { return this._NameIdentifier; }
            set
            {
                this.OnNameIdentifierChanging(value);
                this._NameIdentifier = value;
                this.OnNameIdentifierChanged();
                this.OnPropertyChanged("NameIdentifier");
            }
        }

        /// <summary>
        ///   There are no comments for Property Permissions in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string Permissions
        {
            get { return this._Permissions; }
            set
            {
                this.OnPermissionsChanging(value);
                this._Permissions = value;
                this.OnPermissionsChanged();
                this.OnPropertyChanged("Permissions");
            }
        }

        /// <summary>
        ///   There are no comments for Property AuthorizationCode in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string AuthorizationCode
        {
            get { return this._AuthorizationCode; }
            set
            {
                this.OnAuthorizationCodeChanging(value);
                this._AuthorizationCode = value;
                this.OnAuthorizationCodeChanged();
                this.OnPropertyChanged("AuthorizationCode");
            }
        }

        /// <summary>
        ///   There are no comments for Property SystemReserved in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public bool SystemReserved
        {
            get { return this._SystemReserved; }
            set
            {
                this.OnSystemReservedChanging(value);
                this._SystemReserved = value;
                this.OnSystemReservedChanged();
                this.OnPropertyChanged("SystemReserved");
            }
        }

        /// <summary>
        ///   There are no comments for Property Version in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public byte[] Version
        {
            get
            {
                if ((this._Version != null))
                {
                    return ((byte[]) (this._Version.Clone()));
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.OnVersionChanging(value);
                this._Version = value;
                this.OnVersionChanged();
                this.OnPropertyChanged("Version");
            }
        }

        /// <summary>
        ///   There are no comments for RelyingParty in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public RelyingParty RelyingParty
        {
            get { return this._RelyingParty; }
            set
            {
                this._RelyingParty = value;
                this.OnPropertyChanged("RelyingParty");
            }
        }

        /// <summary>
        ///   There are no comments for ServiceIdentity in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public ServiceIdentity ServiceIdentity
        {
            get { return this._ServiceIdentity; }
            set
            {
                this._ServiceIdentity = value;
                this.OnPropertyChanged("ServiceIdentity");
            }
        }

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///   Create a new Delegation object.
        /// </summary>
        /// <param name = "ID">Initial value of Id.</param>
        /// <param name = "serviceIdentityId">Initial value of ServiceIdentityId.</param>
        /// <param name = "relyingPartyId">Initial value of RelyingPartyId.</param>
        /// <param name = "systemReserved">Initial value of SystemReserved.</param>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public static Delegation CreateDelegation(long ID, long serviceIdentityId, long relyingPartyId, bool systemReserved)
        {
            var delegation = new Delegation();
            delegation.Id = ID;
            delegation.ServiceIdentityId = serviceIdentityId;
            delegation.RelyingPartyId = relyingPartyId;
            delegation.SystemReserved = systemReserved;
            return delegation;
        }

        partial void OnIdChanging(long value);
        partial void OnIdChanged();

        partial void OnServiceIdentityIdChanging(long value);
        partial void OnServiceIdentityIdChanged();

        partial void OnRelyingPartyIdChanging(long value);
        partial void OnRelyingPartyIdChanged();

        partial void OnIdentityProviderChanging(string value);
        partial void OnIdentityProviderChanged();

        partial void OnNameIdentifierChanging(string value);
        partial void OnNameIdentifierChanged();

        partial void OnPermissionsChanging(string value);
        partial void OnPermissionsChanged();

        partial void OnAuthorizationCodeChanging(string value);
        partial void OnAuthorizationCodeChanged();

        partial void OnSystemReservedChanging(bool value);
        partial void OnSystemReservedChanged();

        partial void OnVersionChanging(byte[] value);
        partial void OnVersionChanged();

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }

    /// <summary>
    ///   There are no comments for Microsoft.Cloud.AccessControl.Management.RelyingParty in the schema.
    /// </summary>
    /// <KeyProperties>
    ///   Id
    /// </KeyProperties>
    [EntitySet("RelyingParties")]
    [DataServiceKey("Id")]
    public partial class RelyingParty : INotifyPropertyChanged
    {
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        bool _AsymmetricTokenEncryptionRequired;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DataServiceCollection<Delegation> _Delegations = new DataServiceCollection<Delegation>(null, TrackingMode.None);

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _Description;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _DisplayName;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        long _Id;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _Name;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DataServiceCollection<RelyingPartyAddress> _RelyingPartyAddresses = new DataServiceCollection<RelyingPartyAddress>(null, TrackingMode.None);

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DataServiceCollection<RelyingPartyIdentityProvider> _RelyingPartyIdentityProviders = new DataServiceCollection<RelyingPartyIdentityProvider>(
            null, TrackingMode.None);

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DataServiceCollection<RelyingPartyKey> _RelyingPartyKeys = new DataServiceCollection<RelyingPartyKey>(null, TrackingMode.None);

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DataServiceCollection<RelyingPartyRuleGroup> _RelyingPartyRuleGroups = new DataServiceCollection<RelyingPartyRuleGroup>(null, TrackingMode.None);

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        bool _SystemReserved;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        int _TokenLifetime;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _TokenType;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        byte[] _Version;

        /// <summary>
        ///   There are no comments for Property Id in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public long Id
        {
            get { return this._Id; }
            set
            {
                this.OnIdChanging(value);
                this._Id = value;
                this.OnIdChanged();
                this.OnPropertyChanged("Id");
            }
        }

        /// <summary>
        ///   There are no comments for Property Name in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string Name
        {
            get { return this._Name; }
            set
            {
                this.OnNameChanging(value);
                this._Name = value;
                this.OnNameChanged();
                this.OnPropertyChanged("Name");
            }
        }

        /// <summary>
        ///   There are no comments for Property DisplayName in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string DisplayName
        {
            get { return this._DisplayName; }
            set
            {
                this.OnDisplayNameChanging(value);
                this._DisplayName = value;
                this.OnDisplayNameChanged();
                this.OnPropertyChanged("DisplayName");
            }
        }

        /// <summary>
        ///   There are no comments for Property Description in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string Description
        {
            get { return this._Description; }
            set
            {
                this.OnDescriptionChanging(value);
                this._Description = value;
                this.OnDescriptionChanged();
                this.OnPropertyChanged("Description");
            }
        }

        /// <summary>
        ///   There are no comments for Property TokenType in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string TokenType
        {
            get { return this._TokenType; }
            set
            {
                this.OnTokenTypeChanging(value);
                this._TokenType = value;
                this.OnTokenTypeChanged();
                this.OnPropertyChanged("TokenType");
            }
        }

        /// <summary>
        ///   There are no comments for Property TokenLifetime in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public int TokenLifetime
        {
            get { return this._TokenLifetime; }
            set
            {
                this.OnTokenLifetimeChanging(value);
                this._TokenLifetime = value;
                this.OnTokenLifetimeChanged();
                this.OnPropertyChanged("TokenLifetime");
            }
        }

        /// <summary>
        ///   There are no comments for Property AsymmetricTokenEncryptionRequired in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public bool AsymmetricTokenEncryptionRequired
        {
            get { return this._AsymmetricTokenEncryptionRequired; }
            set
            {
                this.OnAsymmetricTokenEncryptionRequiredChanging(value);
                this._AsymmetricTokenEncryptionRequired = value;
                this.OnAsymmetricTokenEncryptionRequiredChanged();
                this.OnPropertyChanged("AsymmetricTokenEncryptionRequired");
            }
        }

        /// <summary>
        ///   There are no comments for Property SystemReserved in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public bool SystemReserved
        {
            get { return this._SystemReserved; }
            set
            {
                this.OnSystemReservedChanging(value);
                this._SystemReserved = value;
                this.OnSystemReservedChanged();
                this.OnPropertyChanged("SystemReserved");
            }
        }

        /// <summary>
        ///   There are no comments for Property Version in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public byte[] Version
        {
            get
            {
                if ((this._Version != null))
                {
                    return ((byte[]) (this._Version.Clone()));
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.OnVersionChanging(value);
                this._Version = value;
                this.OnVersionChanged();
                this.OnPropertyChanged("Version");
            }
        }

        /// <summary>
        ///   There are no comments for Delegations in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DataServiceCollection<Delegation> Delegations
        {
            get { return this._Delegations; }
            set
            {
                this._Delegations = value;
                this.OnPropertyChanged("Delegations");
            }
        }

        /// <summary>
        ///   There are no comments for RelyingPartyAddresses in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DataServiceCollection<RelyingPartyAddress> RelyingPartyAddresses
        {
            get { return this._RelyingPartyAddresses; }
            set
            {
                this._RelyingPartyAddresses = value;
                this.OnPropertyChanged("RelyingPartyAddresses");
            }
        }

        /// <summary>
        ///   There are no comments for RelyingPartyIdentityProviders in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DataServiceCollection<RelyingPartyIdentityProvider> RelyingPartyIdentityProviders
        {
            get { return this._RelyingPartyIdentityProviders; }
            set
            {
                this._RelyingPartyIdentityProviders = value;
                this.OnPropertyChanged("RelyingPartyIdentityProviders");
            }
        }

        /// <summary>
        ///   There are no comments for RelyingPartyKeys in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DataServiceCollection<RelyingPartyKey> RelyingPartyKeys
        {
            get { return this._RelyingPartyKeys; }
            set
            {
                this._RelyingPartyKeys = value;
                this.OnPropertyChanged("RelyingPartyKeys");
            }
        }

        /// <summary>
        ///   There are no comments for RelyingPartyRuleGroups in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DataServiceCollection<RelyingPartyRuleGroup> RelyingPartyRuleGroups
        {
            get { return this._RelyingPartyRuleGroups; }
            set
            {
                this._RelyingPartyRuleGroups = value;
                this.OnPropertyChanged("RelyingPartyRuleGroups");
            }
        }

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///   Create a new RelyingParty object.
        /// </summary>
        /// <param name = "ID">Initial value of Id.</param>
        /// <param name = "tokenLifetime">Initial value of TokenLifetime.</param>
        /// <param name = "asymmetricTokenEncryptionRequired">Initial value of AsymmetricTokenEncryptionRequired.</param>
        /// <param name = "systemReserved">Initial value of SystemReserved.</param>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public static RelyingParty CreateRelyingParty(long ID, int tokenLifetime, bool asymmetricTokenEncryptionRequired, bool systemReserved)
        {
            var relyingParty = new RelyingParty();
            relyingParty.Id = ID;
            relyingParty.TokenLifetime = tokenLifetime;
            relyingParty.AsymmetricTokenEncryptionRequired = asymmetricTokenEncryptionRequired;
            relyingParty.SystemReserved = systemReserved;
            return relyingParty;
        }

        partial void OnIdChanging(long value);
        partial void OnIdChanged();

        partial void OnNameChanging(string value);
        partial void OnNameChanged();

        partial void OnDisplayNameChanging(string value);
        partial void OnDisplayNameChanged();

        partial void OnDescriptionChanging(string value);
        partial void OnDescriptionChanged();

        partial void OnTokenTypeChanging(string value);
        partial void OnTokenTypeChanged();

        partial void OnTokenLifetimeChanging(int value);
        partial void OnTokenLifetimeChanged();

        partial void OnAsymmetricTokenEncryptionRequiredChanging(bool value);
        partial void OnAsymmetricTokenEncryptionRequiredChanged();

        partial void OnSystemReservedChanging(bool value);
        partial void OnSystemReservedChanged();

        partial void OnVersionChanging(byte[] value);
        partial void OnVersionChanged();

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }

    /// <summary>
    ///   There are no comments for Microsoft.Cloud.AccessControl.Management.RelyingPartyAddress in the schema.
    /// </summary>
    /// <KeyProperties>
    ///   Id
    /// </KeyProperties>
    [EntitySet("RelyingPartyAddresses")]
    [DataServiceKey("Id")]
    public partial class RelyingPartyAddress : INotifyPropertyChanged
    {
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _Address;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _EndpointType;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        long _Id;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        RelyingParty _RelyingParty;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        long _RelyingPartyId;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        bool _SystemReserved;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        byte[] _Version;

        /// <summary>
        ///   There are no comments for Property Id in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public long Id
        {
            get { return this._Id; }
            set
            {
                this.OnIdChanging(value);
                this._Id = value;
                this.OnIdChanged();
                this.OnPropertyChanged("Id");
            }
        }

        /// <summary>
        ///   There are no comments for Property RelyingPartyId in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public long RelyingPartyId
        {
            get { return this._RelyingPartyId; }
            set
            {
                this.OnRelyingPartyIdChanging(value);
                this._RelyingPartyId = value;
                this.OnRelyingPartyIdChanged();
                this.OnPropertyChanged("RelyingPartyId");
            }
        }

        /// <summary>
        ///   There are no comments for Property EndpointType in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string EndpointType
        {
            get { return this._EndpointType; }
            set
            {
                this.OnEndpointTypeChanging(value);
                this._EndpointType = value;
                this.OnEndpointTypeChanged();
                this.OnPropertyChanged("EndpointType");
            }
        }

        /// <summary>
        ///   There are no comments for Property Address in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string Address
        {
            get { return this._Address; }
            set
            {
                this.OnAddressChanging(value);
                this._Address = value;
                this.OnAddressChanged();
                this.OnPropertyChanged("Address");
            }
        }

        /// <summary>
        ///   There are no comments for Property SystemReserved in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public bool SystemReserved
        {
            get { return this._SystemReserved; }
            set
            {
                this.OnSystemReservedChanging(value);
                this._SystemReserved = value;
                this.OnSystemReservedChanged();
                this.OnPropertyChanged("SystemReserved");
            }
        }

        /// <summary>
        ///   There are no comments for Property Version in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public byte[] Version
        {
            get
            {
                if ((this._Version != null))
                {
                    return ((byte[]) (this._Version.Clone()));
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.OnVersionChanging(value);
                this._Version = value;
                this.OnVersionChanged();
                this.OnPropertyChanged("Version");
            }
        }

        /// <summary>
        ///   There are no comments for RelyingParty in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public RelyingParty RelyingParty
        {
            get { return this._RelyingParty; }
            set
            {
                this._RelyingParty = value;
                this.OnPropertyChanged("RelyingParty");
            }
        }

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///   Create a new RelyingPartyAddress object.
        /// </summary>
        /// <param name = "ID">Initial value of Id.</param>
        /// <param name = "relyingPartyId">Initial value of RelyingPartyId.</param>
        /// <param name = "systemReserved">Initial value of SystemReserved.</param>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public static RelyingPartyAddress CreateRelyingPartyAddress(long ID, long relyingPartyId, bool systemReserved)
        {
            var relyingPartyAddress = new RelyingPartyAddress();
            relyingPartyAddress.Id = ID;
            relyingPartyAddress.RelyingPartyId = relyingPartyId;
            relyingPartyAddress.SystemReserved = systemReserved;
            return relyingPartyAddress;
        }

        partial void OnIdChanging(long value);
        partial void OnIdChanged();

        partial void OnRelyingPartyIdChanging(long value);
        partial void OnRelyingPartyIdChanged();

        partial void OnEndpointTypeChanging(string value);
        partial void OnEndpointTypeChanged();

        partial void OnAddressChanging(string value);
        partial void OnAddressChanged();

        partial void OnSystemReservedChanging(bool value);
        partial void OnSystemReservedChanged();

        partial void OnVersionChanging(byte[] value);
        partial void OnVersionChanged();

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }

    /// <summary>
    ///   There are no comments for Microsoft.Cloud.AccessControl.Management.RelyingPartyIdentityProvider in the schema.
    /// </summary>
    /// <KeyProperties>
    ///   Id
    /// </KeyProperties>
    [EntitySet("RelyingPartyIdentityProviders")]
    [DataServiceKey("Id")]
    public partial class RelyingPartyIdentityProvider : INotifyPropertyChanged
    {
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        long _Id;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        IdentityProvider _IdentityProvider;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        long _IdentityProviderId;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        RelyingParty _RelyingParty;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        long _RelyingPartyId;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        bool _SystemReserved;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        byte[] _Version;

        /// <summary>
        ///   There are no comments for Property Id in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public long Id
        {
            get { return this._Id; }
            set
            {
                this.OnIdChanging(value);
                this._Id = value;
                this.OnIdChanged();
                this.OnPropertyChanged("Id");
            }
        }

        /// <summary>
        ///   There are no comments for Property IdentityProviderId in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public long IdentityProviderId
        {
            get { return this._IdentityProviderId; }
            set
            {
                this.OnIdentityProviderIdChanging(value);
                this._IdentityProviderId = value;
                this.OnIdentityProviderIdChanged();
                this.OnPropertyChanged("IdentityProviderId");
            }
        }

        /// <summary>
        ///   There are no comments for Property RelyingPartyId in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public long RelyingPartyId
        {
            get { return this._RelyingPartyId; }
            set
            {
                this.OnRelyingPartyIdChanging(value);
                this._RelyingPartyId = value;
                this.OnRelyingPartyIdChanged();
                this.OnPropertyChanged("RelyingPartyId");
            }
        }

        /// <summary>
        ///   There are no comments for Property SystemReserved in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public bool SystemReserved
        {
            get { return this._SystemReserved; }
            set
            {
                this.OnSystemReservedChanging(value);
                this._SystemReserved = value;
                this.OnSystemReservedChanged();
                this.OnPropertyChanged("SystemReserved");
            }
        }

        /// <summary>
        ///   There are no comments for Property Version in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public byte[] Version
        {
            get
            {
                if ((this._Version != null))
                {
                    return ((byte[]) (this._Version.Clone()));
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.OnVersionChanging(value);
                this._Version = value;
                this.OnVersionChanged();
                this.OnPropertyChanged("Version");
            }
        }

        /// <summary>
        ///   There are no comments for IdentityProvider in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public IdentityProvider IdentityProvider
        {
            get { return this._IdentityProvider; }
            set
            {
                this._IdentityProvider = value;
                this.OnPropertyChanged("IdentityProvider");
            }
        }

        /// <summary>
        ///   There are no comments for RelyingParty in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public RelyingParty RelyingParty
        {
            get { return this._RelyingParty; }
            set
            {
                this._RelyingParty = value;
                this.OnPropertyChanged("RelyingParty");
            }
        }

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///   Create a new RelyingPartyIdentityProvider object.
        /// </summary>
        /// <param name = "ID">Initial value of Id.</param>
        /// <param name = "identityProviderId">Initial value of IdentityProviderId.</param>
        /// <param name = "relyingPartyId">Initial value of RelyingPartyId.</param>
        /// <param name = "systemReserved">Initial value of SystemReserved.</param>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public static RelyingPartyIdentityProvider CreateRelyingPartyIdentityProvider(
            long ID, long identityProviderId, long relyingPartyId, bool systemReserved)
        {
            var relyingPartyIdentityProvider = new RelyingPartyIdentityProvider();
            relyingPartyIdentityProvider.Id = ID;
            relyingPartyIdentityProvider.IdentityProviderId = identityProviderId;
            relyingPartyIdentityProvider.RelyingPartyId = relyingPartyId;
            relyingPartyIdentityProvider.SystemReserved = systemReserved;
            return relyingPartyIdentityProvider;
        }

        partial void OnIdChanging(long value);
        partial void OnIdChanged();

        partial void OnIdentityProviderIdChanging(long value);
        partial void OnIdentityProviderIdChanged();

        partial void OnRelyingPartyIdChanging(long value);
        partial void OnRelyingPartyIdChanged();

        partial void OnSystemReservedChanging(bool value);
        partial void OnSystemReservedChanged();

        partial void OnVersionChanging(byte[] value);
        partial void OnVersionChanged();

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }

    /// <summary>
    ///   There are no comments for Microsoft.Cloud.AccessControl.Management.RelyingPartyKey in the schema.
    /// </summary>
    /// <KeyProperties>
    ///   Id
    /// </KeyProperties>
    [EntitySet("RelyingPartyKeys")]
    [DataServiceKey("Id")]
    public partial class RelyingPartyKey : INotifyPropertyChanged
    {
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _DisplayName;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DateTime _EndDate;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        long _Id;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        bool _IsPrimary;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        byte[] _Password;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        RelyingParty _RelyingParty;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        long _RelyingPartyId;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DateTime _StartDate;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        bool _SystemReserved;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _Type;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _Usage;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        byte[] _Value;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        byte[] _Version;

        /// <summary>
        ///   There are no comments for Property Id in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public long Id
        {
            get { return this._Id; }
            set
            {
                this.OnIdChanging(value);
                this._Id = value;
                this.OnIdChanged();
                this.OnPropertyChanged("Id");
            }
        }

        /// <summary>
        ///   There are no comments for Property RelyingPartyId in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public long RelyingPartyId
        {
            get { return this._RelyingPartyId; }
            set
            {
                this.OnRelyingPartyIdChanging(value);
                this._RelyingPartyId = value;
                this.OnRelyingPartyIdChanged();
                this.OnPropertyChanged("RelyingPartyId");
            }
        }

        /// <summary>
        ///   There are no comments for Property DisplayName in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string DisplayName
        {
            get { return this._DisplayName; }
            set
            {
                this.OnDisplayNameChanging(value);
                this._DisplayName = value;
                this.OnDisplayNameChanged();
                this.OnPropertyChanged("DisplayName");
            }
        }

        /// <summary>
        ///   There are no comments for Property Usage in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string Usage
        {
            get { return this._Usage; }
            set
            {
                this.OnUsageChanging(value);
                this._Usage = value;
                this.OnUsageChanged();
                this.OnPropertyChanged("Usage");
            }
        }

        /// <summary>
        ///   There are no comments for Property Type in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string Type
        {
            get { return this._Type; }
            set
            {
                this.OnTypeChanging(value);
                this._Type = value;
                this.OnTypeChanged();
                this.OnPropertyChanged("Type");
            }
        }

        /// <summary>
        ///   There are no comments for Property Value in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public byte[] Value
        {
            get
            {
                if ((this._Value != null))
                {
                    return ((byte[]) (this._Value.Clone()));
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.OnValueChanging(value);
                this._Value = value;
                this.OnValueChanged();
                this.OnPropertyChanged("Value");
            }
        }

        /// <summary>
        ///   There are no comments for Property Password in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public byte[] Password
        {
            get
            {
                if ((this._Password != null))
                {
                    return ((byte[]) (this._Password.Clone()));
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.OnPasswordChanging(value);
                this._Password = value;
                this.OnPasswordChanged();
                this.OnPropertyChanged("Password");
            }
        }

        /// <summary>
        ///   There are no comments for Property IsPrimary in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public bool IsPrimary
        {
            get { return this._IsPrimary; }
            set
            {
                this.OnIsPrimaryChanging(value);
                this._IsPrimary = value;
                this.OnIsPrimaryChanged();
                this.OnPropertyChanged("IsPrimary");
            }
        }

        /// <summary>
        ///   There are no comments for Property SystemReserved in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public bool SystemReserved
        {
            get { return this._SystemReserved; }
            set
            {
                this.OnSystemReservedChanging(value);
                this._SystemReserved = value;
                this.OnSystemReservedChanged();
                this.OnPropertyChanged("SystemReserved");
            }
        }

        /// <summary>
        ///   There are no comments for Property StartDate in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DateTime StartDate
        {
            get { return this._StartDate; }
            set
            {
                this.OnStartDateChanging(value);
                this._StartDate = value;
                this.OnStartDateChanged();
                this.OnPropertyChanged("StartDate");
            }
        }

        /// <summary>
        ///   There are no comments for Property EndDate in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DateTime EndDate
        {
            get { return this._EndDate; }
            set
            {
                this.OnEndDateChanging(value);
                this._EndDate = value;
                this.OnEndDateChanged();
                this.OnPropertyChanged("EndDate");
            }
        }

        /// <summary>
        ///   There are no comments for Property Version in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public byte[] Version
        {
            get
            {
                if ((this._Version != null))
                {
                    return ((byte[]) (this._Version.Clone()));
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.OnVersionChanging(value);
                this._Version = value;
                this.OnVersionChanged();
                this.OnPropertyChanged("Version");
            }
        }

        /// <summary>
        ///   There are no comments for RelyingParty in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public RelyingParty RelyingParty
        {
            get { return this._RelyingParty; }
            set
            {
                this._RelyingParty = value;
                this.OnPropertyChanged("RelyingParty");
            }
        }

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///   Create a new RelyingPartyKey object.
        /// </summary>
        /// <param name = "ID">Initial value of Id.</param>
        /// <param name = "relyingPartyId">Initial value of RelyingPartyId.</param>
        /// <param name = "isPrimary">Initial value of IsPrimary.</param>
        /// <param name = "systemReserved">Initial value of SystemReserved.</param>
        /// <param name = "startDate">Initial value of StartDate.</param>
        /// <param name = "endDate">Initial value of EndDate.</param>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public static RelyingPartyKey CreateRelyingPartyKey(
            long ID, long relyingPartyId, bool isPrimary, bool systemReserved, DateTime startDate, DateTime endDate)
        {
            var relyingPartyKey = new RelyingPartyKey();
            relyingPartyKey.Id = ID;
            relyingPartyKey.RelyingPartyId = relyingPartyId;
            relyingPartyKey.IsPrimary = isPrimary;
            relyingPartyKey.SystemReserved = systemReserved;
            relyingPartyKey.StartDate = startDate;
            relyingPartyKey.EndDate = endDate;
            return relyingPartyKey;
        }

        partial void OnIdChanging(long value);
        partial void OnIdChanged();

        partial void OnRelyingPartyIdChanging(long value);
        partial void OnRelyingPartyIdChanged();

        partial void OnDisplayNameChanging(string value);
        partial void OnDisplayNameChanged();

        partial void OnUsageChanging(string value);
        partial void OnUsageChanged();

        partial void OnTypeChanging(string value);
        partial void OnTypeChanged();

        partial void OnValueChanging(byte[] value);
        partial void OnValueChanged();

        partial void OnPasswordChanging(byte[] value);
        partial void OnPasswordChanged();

        partial void OnIsPrimaryChanging(bool value);
        partial void OnIsPrimaryChanged();

        partial void OnSystemReservedChanging(bool value);
        partial void OnSystemReservedChanged();

        partial void OnStartDateChanging(DateTime value);
        partial void OnStartDateChanged();

        partial void OnEndDateChanging(DateTime value);
        partial void OnEndDateChanged();

        partial void OnVersionChanging(byte[] value);
        partial void OnVersionChanged();

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }

    /// <summary>
    ///   There are no comments for Microsoft.Cloud.AccessControl.Management.ServiceIdentity in the schema.
    /// </summary>
    /// <KeyProperties>
    ///   Id
    /// </KeyProperties>
    [EntitySet("ServiceIdentities")]
    [DataServiceKey("Id")]
    public partial class ServiceIdentity : INotifyPropertyChanged
    {
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DataServiceCollection<Delegation> _Delegations = new DataServiceCollection<Delegation>(null, TrackingMode.None);

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _Description;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        long _Id;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _Name;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _RedirectAddress;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DataServiceCollection<ServiceIdentityKey> _ServiceIdentityKeys = new DataServiceCollection<ServiceIdentityKey>(null, TrackingMode.None);

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        bool _SystemReserved;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        byte[] _Version;

        /// <summary>
        ///   There are no comments for Property Id in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public long Id
        {
            get { return this._Id; }
            set
            {
                this.OnIdChanging(value);
                this._Id = value;
                this.OnIdChanged();
                this.OnPropertyChanged("Id");
            }
        }

        /// <summary>
        ///   There are no comments for Property Name in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string Name
        {
            get { return this._Name; }
            set
            {
                this.OnNameChanging(value);
                this._Name = value;
                this.OnNameChanged();
                this.OnPropertyChanged("Name");
            }
        }

        /// <summary>
        ///   There are no comments for Property Description in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string Description
        {
            get { return this._Description; }
            set
            {
                this.OnDescriptionChanging(value);
                this._Description = value;
                this.OnDescriptionChanged();
                this.OnPropertyChanged("Description");
            }
        }

        /// <summary>
        ///   There are no comments for Property RedirectAddress in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string RedirectAddress
        {
            get { return this._RedirectAddress; }
            set
            {
                this.OnRedirectAddressChanging(value);
                this._RedirectAddress = value;
                this.OnRedirectAddressChanged();
                this.OnPropertyChanged("RedirectAddress");
            }
        }

        /// <summary>
        ///   There are no comments for Property SystemReserved in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public bool SystemReserved
        {
            get { return this._SystemReserved; }
            set
            {
                this.OnSystemReservedChanging(value);
                this._SystemReserved = value;
                this.OnSystemReservedChanged();
                this.OnPropertyChanged("SystemReserved");
            }
        }

        /// <summary>
        ///   There are no comments for Property Version in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public byte[] Version
        {
            get
            {
                if ((this._Version != null))
                {
                    return ((byte[]) (this._Version.Clone()));
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.OnVersionChanging(value);
                this._Version = value;
                this.OnVersionChanged();
                this.OnPropertyChanged("Version");
            }
        }

        /// <summary>
        ///   There are no comments for Delegations in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DataServiceCollection<Delegation> Delegations
        {
            get { return this._Delegations; }
            set
            {
                this._Delegations = value;
                this.OnPropertyChanged("Delegations");
            }
        }

        /// <summary>
        ///   There are no comments for ServiceIdentityKeys in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DataServiceCollection<ServiceIdentityKey> ServiceIdentityKeys
        {
            get { return this._ServiceIdentityKeys; }
            set
            {
                this._ServiceIdentityKeys = value;
                this.OnPropertyChanged("ServiceIdentityKeys");
            }
        }

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///   Create a new ServiceIdentity object.
        /// </summary>
        /// <param name = "ID">Initial value of Id.</param>
        /// <param name = "systemReserved">Initial value of SystemReserved.</param>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public static ServiceIdentity CreateServiceIdentity(long ID, bool systemReserved)
        {
            var serviceIdentity = new ServiceIdentity();
            serviceIdentity.Id = ID;
            serviceIdentity.SystemReserved = systemReserved;
            return serviceIdentity;
        }

        partial void OnIdChanging(long value);
        partial void OnIdChanged();

        partial void OnNameChanging(string value);
        partial void OnNameChanged();

        partial void OnDescriptionChanging(string value);
        partial void OnDescriptionChanged();

        partial void OnRedirectAddressChanging(string value);
        partial void OnRedirectAddressChanged();

        partial void OnSystemReservedChanging(bool value);
        partial void OnSystemReservedChanged();

        partial void OnVersionChanging(byte[] value);
        partial void OnVersionChanged();

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }

    /// <summary>
    ///   There are no comments for Microsoft.Cloud.AccessControl.Management.ServiceIdentityKey in the schema.
    /// </summary>
    /// <KeyProperties>
    ///   Id
    /// </KeyProperties>
    [EntitySet("ServiceIdentityKeys")]
    [DataServiceKey("Id")]
    public partial class ServiceIdentityKey : INotifyPropertyChanged
    {
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _DisplayName;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DateTime _EndDate;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        long _Id;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        ServiceIdentity _ServiceIdentity;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        long _ServiceIdentityId;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DateTime _StartDate;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        bool _SystemReserved;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _Type;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _Usage;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        byte[] _Value;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        byte[] _Version;

        /// <summary>
        ///   There are no comments for Property Id in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public long Id
        {
            get { return this._Id; }
            set
            {
                this.OnIdChanging(value);
                this._Id = value;
                this.OnIdChanged();
                this.OnPropertyChanged("Id");
            }
        }

        /// <summary>
        ///   There are no comments for Property ServiceIdentityId in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public long ServiceIdentityId
        {
            get { return this._ServiceIdentityId; }
            set
            {
                this.OnServiceIdentityIdChanging(value);
                this._ServiceIdentityId = value;
                this.OnServiceIdentityIdChanged();
                this.OnPropertyChanged("ServiceIdentityId");
            }
        }

        /// <summary>
        ///   There are no comments for Property Usage in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string Usage
        {
            get { return this._Usage; }
            set
            {
                this.OnUsageChanging(value);
                this._Usage = value;
                this.OnUsageChanged();
                this.OnPropertyChanged("Usage");
            }
        }

        /// <summary>
        ///   There are no comments for Property Type in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string Type
        {
            get { return this._Type; }
            set
            {
                this.OnTypeChanging(value);
                this._Type = value;
                this.OnTypeChanged();
                this.OnPropertyChanged("Type");
            }
        }

        /// <summary>
        ///   There are no comments for Property Value in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public byte[] Value
        {
            get
            {
                if ((this._Value != null))
                {
                    return ((byte[]) (this._Value.Clone()));
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.OnValueChanging(value);
                this._Value = value;
                this.OnValueChanged();
                this.OnPropertyChanged("Value");
            }
        }

        /// <summary>
        ///   There are no comments for Property SystemReserved in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public bool SystemReserved
        {
            get { return this._SystemReserved; }
            set
            {
                this.OnSystemReservedChanging(value);
                this._SystemReserved = value;
                this.OnSystemReservedChanged();
                this.OnPropertyChanged("SystemReserved");
            }
        }

        /// <summary>
        ///   There are no comments for Property StartDate in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DateTime StartDate
        {
            get { return this._StartDate; }
            set
            {
                this.OnStartDateChanging(value);
                this._StartDate = value;
                this.OnStartDateChanged();
                this.OnPropertyChanged("StartDate");
            }
        }

        /// <summary>
        ///   There are no comments for Property EndDate in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DateTime EndDate
        {
            get { return this._EndDate; }
            set
            {
                this.OnEndDateChanging(value);
                this._EndDate = value;
                this.OnEndDateChanged();
                this.OnPropertyChanged("EndDate");
            }
        }

        /// <summary>
        ///   There are no comments for Property DisplayName in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string DisplayName
        {
            get { return this._DisplayName; }
            set
            {
                this.OnDisplayNameChanging(value);
                this._DisplayName = value;
                this.OnDisplayNameChanged();
                this.OnPropertyChanged("DisplayName");
            }
        }

        /// <summary>
        ///   There are no comments for Property Version in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public byte[] Version
        {
            get
            {
                if ((this._Version != null))
                {
                    return ((byte[]) (this._Version.Clone()));
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.OnVersionChanging(value);
                this._Version = value;
                this.OnVersionChanged();
                this.OnPropertyChanged("Version");
            }
        }

        /// <summary>
        ///   There are no comments for ServiceIdentity in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public ServiceIdentity ServiceIdentity
        {
            get { return this._ServiceIdentity; }
            set
            {
                this._ServiceIdentity = value;
                this.OnPropertyChanged("ServiceIdentity");
            }
        }

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///   Create a new ServiceIdentityKey object.
        /// </summary>
        /// <param name = "ID">Initial value of Id.</param>
        /// <param name = "serviceIdentityId">Initial value of ServiceIdentityId.</param>
        /// <param name = "systemReserved">Initial value of SystemReserved.</param>
        /// <param name = "startDate">Initial value of StartDate.</param>
        /// <param name = "endDate">Initial value of EndDate.</param>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public static ServiceIdentityKey CreateServiceIdentityKey(long ID, long serviceIdentityId, bool systemReserved, DateTime startDate, DateTime endDate)
        {
            var serviceIdentityKey = new ServiceIdentityKey();
            serviceIdentityKey.Id = ID;
            serviceIdentityKey.ServiceIdentityId = serviceIdentityId;
            serviceIdentityKey.SystemReserved = systemReserved;
            serviceIdentityKey.StartDate = startDate;
            serviceIdentityKey.EndDate = endDate;
            return serviceIdentityKey;
        }

        partial void OnIdChanging(long value);
        partial void OnIdChanged();

        partial void OnServiceIdentityIdChanging(long value);
        partial void OnServiceIdentityIdChanged();

        partial void OnUsageChanging(string value);
        partial void OnUsageChanged();

        partial void OnTypeChanging(string value);
        partial void OnTypeChanged();

        partial void OnValueChanging(byte[] value);
        partial void OnValueChanged();

        partial void OnSystemReservedChanging(bool value);
        partial void OnSystemReservedChanged();

        partial void OnStartDateChanging(DateTime value);
        partial void OnStartDateChanged();

        partial void OnEndDateChanging(DateTime value);
        partial void OnEndDateChanged();

        partial void OnDisplayNameChanging(string value);
        partial void OnDisplayNameChanged();

        partial void OnVersionChanging(byte[] value);
        partial void OnVersionChanged();

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }

    /// <summary>
    ///   There are no comments for Microsoft.Cloud.AccessControl.Management.RuleGroup in the schema.
    /// </summary>
    /// <KeyProperties>
    ///   Id
    /// </KeyProperties>
    [EntitySet("RuleGroups")]
    [DataServiceKey("Id")]
    public partial class RuleGroup : INotifyPropertyChanged
    {
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DataServiceCollection<ConditionalRule> _ConditionalRules = new DataServiceCollection<ConditionalRule>(null, TrackingMode.None);

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        long _Id;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _Name;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DataServiceCollection<RelyingPartyRuleGroup> _RelyingPartyRuleGroups = new DataServiceCollection<RelyingPartyRuleGroup>(null, TrackingMode.None);

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DataServiceCollection<Rule> _Rules = new DataServiceCollection<Rule>(null, TrackingMode.None);

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        bool _SystemReserved;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        byte[] _Version;

        /// <summary>
        ///   There are no comments for Property Id in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public long Id
        {
            get { return this._Id; }
            set
            {
                this.OnIdChanging(value);
                this._Id = value;
                this.OnIdChanged();
                this.OnPropertyChanged("Id");
            }
        }

        /// <summary>
        ///   There are no comments for Property Name in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string Name
        {
            get { return this._Name; }
            set
            {
                this.OnNameChanging(value);
                this._Name = value;
                this.OnNameChanged();
                this.OnPropertyChanged("Name");
            }
        }

        /// <summary>
        ///   There are no comments for Property SystemReserved in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public bool SystemReserved
        {
            get { return this._SystemReserved; }
            set
            {
                this.OnSystemReservedChanging(value);
                this._SystemReserved = value;
                this.OnSystemReservedChanged();
                this.OnPropertyChanged("SystemReserved");
            }
        }

        /// <summary>
        ///   There are no comments for Property Version in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public byte[] Version
        {
            get
            {
                if ((this._Version != null))
                {
                    return ((byte[]) (this._Version.Clone()));
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.OnVersionChanging(value);
                this._Version = value;
                this.OnVersionChanged();
                this.OnPropertyChanged("Version");
            }
        }

        /// <summary>
        ///   There are no comments for ConditionalRules in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DataServiceCollection<ConditionalRule> ConditionalRules
        {
            get { return this._ConditionalRules; }
            set
            {
                this._ConditionalRules = value;
                this.OnPropertyChanged("ConditionalRules");
            }
        }

        /// <summary>
        ///   There are no comments for RelyingPartyRuleGroups in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DataServiceCollection<RelyingPartyRuleGroup> RelyingPartyRuleGroups
        {
            get { return this._RelyingPartyRuleGroups; }
            set
            {
                this._RelyingPartyRuleGroups = value;
                this.OnPropertyChanged("RelyingPartyRuleGroups");
            }
        }

        /// <summary>
        ///   There are no comments for Rules in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DataServiceCollection<Rule> Rules
        {
            get { return this._Rules; }
            set
            {
                this._Rules = value;
                this.OnPropertyChanged("Rules");
            }
        }

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///   Create a new RuleGroup object.
        /// </summary>
        /// <param name = "ID">Initial value of Id.</param>
        /// <param name = "systemReserved">Initial value of SystemReserved.</param>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public static RuleGroup CreateRuleGroup(long ID, bool systemReserved)
        {
            var ruleGroup = new RuleGroup();
            ruleGroup.Id = ID;
            ruleGroup.SystemReserved = systemReserved;
            return ruleGroup;
        }

        partial void OnIdChanging(long value);
        partial void OnIdChanged();

        partial void OnNameChanging(string value);
        partial void OnNameChanged();

        partial void OnSystemReservedChanging(bool value);
        partial void OnSystemReservedChanged();

        partial void OnVersionChanging(byte[] value);
        partial void OnVersionChanged();

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }

    /// <summary>
    ///   There are no comments for Microsoft.Cloud.AccessControl.Management.RelyingPartyRuleGroup in the schema.
    /// </summary>
    /// <KeyProperties>
    ///   Id
    /// </KeyProperties>
    [EntitySet("RelyingPartyRuleGroups")]
    [DataServiceKey("Id")]
    public partial class RelyingPartyRuleGroup : INotifyPropertyChanged
    {
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        long _Id;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        RelyingParty _RelyingParty;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        long _RelyingPartyId;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        RuleGroup _RuleGroup;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        long _RuleGroupId;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        bool _SystemReserved;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        byte[] _Version;

        /// <summary>
        ///   There are no comments for Property Id in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public long Id
        {
            get { return this._Id; }
            set
            {
                this.OnIdChanging(value);
                this._Id = value;
                this.OnIdChanged();
                this.OnPropertyChanged("Id");
            }
        }

        /// <summary>
        ///   There are no comments for Property RelyingPartyId in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public long RelyingPartyId
        {
            get { return this._RelyingPartyId; }
            set
            {
                this.OnRelyingPartyIdChanging(value);
                this._RelyingPartyId = value;
                this.OnRelyingPartyIdChanged();
                this.OnPropertyChanged("RelyingPartyId");
            }
        }

        /// <summary>
        ///   There are no comments for Property RuleGroupId in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public long RuleGroupId
        {
            get { return this._RuleGroupId; }
            set
            {
                this.OnRuleGroupIdChanging(value);
                this._RuleGroupId = value;
                this.OnRuleGroupIdChanged();
                this.OnPropertyChanged("RuleGroupId");
            }
        }

        /// <summary>
        ///   There are no comments for Property SystemReserved in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public bool SystemReserved
        {
            get { return this._SystemReserved; }
            set
            {
                this.OnSystemReservedChanging(value);
                this._SystemReserved = value;
                this.OnSystemReservedChanged();
                this.OnPropertyChanged("SystemReserved");
            }
        }

        /// <summary>
        ///   There are no comments for Property Version in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public byte[] Version
        {
            get
            {
                if ((this._Version != null))
                {
                    return ((byte[]) (this._Version.Clone()));
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.OnVersionChanging(value);
                this._Version = value;
                this.OnVersionChanged();
                this.OnPropertyChanged("Version");
            }
        }

        /// <summary>
        ///   There are no comments for RelyingParty in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public RelyingParty RelyingParty
        {
            get { return this._RelyingParty; }
            set
            {
                this._RelyingParty = value;
                this.OnPropertyChanged("RelyingParty");
            }
        }

        /// <summary>
        ///   There are no comments for RuleGroup in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public RuleGroup RuleGroup
        {
            get { return this._RuleGroup; }
            set
            {
                this._RuleGroup = value;
                this.OnPropertyChanged("RuleGroup");
            }
        }

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///   Create a new RelyingPartyRuleGroup object.
        /// </summary>
        /// <param name = "ID">Initial value of Id.</param>
        /// <param name = "relyingPartyId">Initial value of RelyingPartyId.</param>
        /// <param name = "ruleGroupId">Initial value of RuleGroupId.</param>
        /// <param name = "systemReserved">Initial value of SystemReserved.</param>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public static RelyingPartyRuleGroup CreateRelyingPartyRuleGroup(long ID, long relyingPartyId, long ruleGroupId, bool systemReserved)
        {
            var relyingPartyRuleGroup = new RelyingPartyRuleGroup();
            relyingPartyRuleGroup.Id = ID;
            relyingPartyRuleGroup.RelyingPartyId = relyingPartyId;
            relyingPartyRuleGroup.RuleGroupId = ruleGroupId;
            relyingPartyRuleGroup.SystemReserved = systemReserved;
            return relyingPartyRuleGroup;
        }

        partial void OnIdChanging(long value);
        partial void OnIdChanged();

        partial void OnRelyingPartyIdChanging(long value);
        partial void OnRelyingPartyIdChanged();

        partial void OnRuleGroupIdChanging(long value);
        partial void OnRuleGroupIdChanged();

        partial void OnSystemReservedChanging(bool value);
        partial void OnSystemReservedChanged();

        partial void OnVersionChanging(byte[] value);
        partial void OnVersionChanged();

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }

    /// <summary>
    ///   There are no comments for Microsoft.Cloud.AccessControl.Management.Issuer in the schema.
    /// </summary>
    /// <KeyProperties>
    ///   Id
    /// </KeyProperties>
    [EntitySet("Issuers")]
    [DataServiceKey("Id")]
    public partial class Issuer : INotifyPropertyChanged
    {
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DataServiceCollection<ConditionalRule> _ConditionalRules = new DataServiceCollection<ConditionalRule>(null, TrackingMode.None);

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        long _Id;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DataServiceCollection<IdentityProvider> _IdentityProviders = new DataServiceCollection<IdentityProvider>(null, TrackingMode.None);

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _Name;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        DataServiceCollection<Rule> _Rules = new DataServiceCollection<Rule>(null, TrackingMode.None);

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        bool _SystemReserved;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        byte[] _Version;

        /// <summary>
        ///   There are no comments for Property Id in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public long Id
        {
            get { return this._Id; }
            set
            {
                this.OnIdChanging(value);
                this._Id = value;
                this.OnIdChanged();
                this.OnPropertyChanged("Id");
            }
        }

        /// <summary>
        ///   There are no comments for Property Name in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string Name
        {
            get { return this._Name; }
            set
            {
                this.OnNameChanging(value);
                this._Name = value;
                this.OnNameChanged();
                this.OnPropertyChanged("Name");
            }
        }

        /// <summary>
        ///   There are no comments for Property SystemReserved in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public bool SystemReserved
        {
            get { return this._SystemReserved; }
            set
            {
                this.OnSystemReservedChanging(value);
                this._SystemReserved = value;
                this.OnSystemReservedChanged();
                this.OnPropertyChanged("SystemReserved");
            }
        }

        /// <summary>
        ///   There are no comments for Property Version in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public byte[] Version
        {
            get
            {
                if ((this._Version != null))
                {
                    return ((byte[]) (this._Version.Clone()));
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.OnVersionChanging(value);
                this._Version = value;
                this.OnVersionChanged();
                this.OnPropertyChanged("Version");
            }
        }

        /// <summary>
        ///   There are no comments for ConditionalRules in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DataServiceCollection<ConditionalRule> ConditionalRules
        {
            get { return this._ConditionalRules; }
            set
            {
                this._ConditionalRules = value;
                this.OnPropertyChanged("ConditionalRules");
            }
        }

        /// <summary>
        ///   There are no comments for IdentityProviders in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DataServiceCollection<IdentityProvider> IdentityProviders
        {
            get { return this._IdentityProviders; }
            set
            {
                this._IdentityProviders = value;
                this.OnPropertyChanged("IdentityProviders");
            }
        }

        /// <summary>
        ///   There are no comments for Rules in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public DataServiceCollection<Rule> Rules
        {
            get { return this._Rules; }
            set
            {
                this._Rules = value;
                this.OnPropertyChanged("Rules");
            }
        }

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///   Create a new Issuer object.
        /// </summary>
        /// <param name = "ID">Initial value of Id.</param>
        /// <param name = "systemReserved">Initial value of SystemReserved.</param>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public static Issuer CreateIssuer(long ID, bool systemReserved)
        {
            var issuer = new Issuer();
            issuer.Id = ID;
            issuer.SystemReserved = systemReserved;
            return issuer;
        }

        partial void OnIdChanging(long value);
        partial void OnIdChanged();

        partial void OnNameChanging(string value);
        partial void OnNameChanged();

        partial void OnSystemReservedChanging(bool value);
        partial void OnSystemReservedChanged();

        partial void OnVersionChanging(byte[] value);
        partial void OnVersionChanged();

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }

    /// <summary>
    ///   There are no comments for Microsoft.Cloud.AccessControl.Management.Rule in the schema.
    /// </summary>
    /// <KeyProperties>
    ///   Id
    /// </KeyProperties>
    [EntitySet("Rules")]
    [DataServiceKey("Id")]
    public partial class Rule : INotifyPropertyChanged
    {
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _Description;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        long _Id;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _InputClaimType;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _InputClaimValue;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        Issuer _Issuer;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        long _IssuerId;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _OutputClaimType;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _OutputClaimValue;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        RuleGroup _RuleGroup;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        long _RuleGroupId;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        bool _SystemReserved;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        byte[] _Version;

        /// <summary>
        ///   There are no comments for Property Id in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public long Id
        {
            get { return this._Id; }
            set
            {
                this.OnIdChanging(value);
                this._Id = value;
                this.OnIdChanged();
                this.OnPropertyChanged("Id");
            }
        }

        /// <summary>
        ///   There are no comments for Property RuleGroupId in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public long RuleGroupId
        {
            get { return this._RuleGroupId; }
            set
            {
                this.OnRuleGroupIdChanging(value);
                this._RuleGroupId = value;
                this.OnRuleGroupIdChanged();
                this.OnPropertyChanged("RuleGroupId");
            }
        }

        /// <summary>
        ///   There are no comments for Property IssuerId in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public long IssuerId
        {
            get { return this._IssuerId; }
            set
            {
                this.OnIssuerIdChanging(value);
                this._IssuerId = value;
                this.OnIssuerIdChanged();
                this.OnPropertyChanged("IssuerId");
            }
        }

        /// <summary>
        ///   There are no comments for Property Description in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string Description
        {
            get { return this._Description; }
            set
            {
                this.OnDescriptionChanging(value);
                this._Description = value;
                this.OnDescriptionChanged();
                this.OnPropertyChanged("Description");
            }
        }

        /// <summary>
        ///   There are no comments for Property InputClaimType in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string InputClaimType
        {
            get { return this._InputClaimType; }
            set
            {
                this.OnInputClaimTypeChanging(value);
                this._InputClaimType = value;
                this.OnInputClaimTypeChanged();
                this.OnPropertyChanged("InputClaimType");
            }
        }

        /// <summary>
        ///   There are no comments for Property InputClaimValue in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string InputClaimValue
        {
            get { return this._InputClaimValue; }
            set
            {
                this.OnInputClaimValueChanging(value);
                this._InputClaimValue = value;
                this.OnInputClaimValueChanged();
                this.OnPropertyChanged("InputClaimValue");
            }
        }

        /// <summary>
        ///   There are no comments for Property OutputClaimType in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string OutputClaimType
        {
            get { return this._OutputClaimType; }
            set
            {
                this.OnOutputClaimTypeChanging(value);
                this._OutputClaimType = value;
                this.OnOutputClaimTypeChanged();
                this.OnPropertyChanged("OutputClaimType");
            }
        }

        /// <summary>
        ///   There are no comments for Property OutputClaimValue in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string OutputClaimValue
        {
            get { return this._OutputClaimValue; }
            set
            {
                this.OnOutputClaimValueChanging(value);
                this._OutputClaimValue = value;
                this.OnOutputClaimValueChanged();
                this.OnPropertyChanged("OutputClaimValue");
            }
        }

        /// <summary>
        ///   There are no comments for Property SystemReserved in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public bool SystemReserved
        {
            get { return this._SystemReserved; }
            set
            {
                this.OnSystemReservedChanging(value);
                this._SystemReserved = value;
                this.OnSystemReservedChanged();
                this.OnPropertyChanged("SystemReserved");
            }
        }

        /// <summary>
        ///   There are no comments for Property Version in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public byte[] Version
        {
            get
            {
                if ((this._Version != null))
                {
                    return ((byte[]) (this._Version.Clone()));
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.OnVersionChanging(value);
                this._Version = value;
                this.OnVersionChanged();
                this.OnPropertyChanged("Version");
            }
        }

        /// <summary>
        ///   There are no comments for Issuer in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public Issuer Issuer
        {
            get { return this._Issuer; }
            set
            {
                this._Issuer = value;
                this.OnPropertyChanged("Issuer");
            }
        }

        /// <summary>
        ///   There are no comments for RuleGroup in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public RuleGroup RuleGroup
        {
            get { return this._RuleGroup; }
            set
            {
                this._RuleGroup = value;
                this.OnPropertyChanged("RuleGroup");
            }
        }

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///   Create a new Rule object.
        /// </summary>
        /// <param name = "ID">Initial value of Id.</param>
        /// <param name = "ruleGroupId">Initial value of RuleGroupId.</param>
        /// <param name = "issuerId">Initial value of IssuerId.</param>
        /// <param name = "systemReserved">Initial value of SystemReserved.</param>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public static Rule CreateRule(long ID, long ruleGroupId, long issuerId, bool systemReserved)
        {
            var rule = new Rule();
            rule.Id = ID;
            rule.RuleGroupId = ruleGroupId;
            rule.IssuerId = issuerId;
            rule.SystemReserved = systemReserved;
            return rule;
        }

        partial void OnIdChanging(long value);
        partial void OnIdChanged();

        partial void OnRuleGroupIdChanging(long value);
        partial void OnRuleGroupIdChanged();

        partial void OnIssuerIdChanging(long value);
        partial void OnIssuerIdChanged();

        partial void OnDescriptionChanging(string value);
        partial void OnDescriptionChanged();

        partial void OnInputClaimTypeChanging(string value);
        partial void OnInputClaimTypeChanged();

        partial void OnInputClaimValueChanging(string value);
        partial void OnInputClaimValueChanged();

        partial void OnOutputClaimTypeChanging(string value);
        partial void OnOutputClaimTypeChanged();

        partial void OnOutputClaimValueChanging(string value);
        partial void OnOutputClaimValueChanged();

        partial void OnSystemReservedChanging(bool value);
        partial void OnSystemReservedChanged();

        partial void OnVersionChanging(byte[] value);
        partial void OnVersionChanged();

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }

    /// <summary>
    ///   There are no comments for Microsoft.Cloud.AccessControl.Management.ConditionalRule in the schema.
    /// </summary>
    /// <KeyProperties>
    ///   Id
    /// </KeyProperties>
    [EntitySet("ConditionalRules")]
    [DataServiceKey("Id")]
    public partial class ConditionalRule : INotifyPropertyChanged
    {
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _ConditionClaimType;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _ConditionClaimValue;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        long _ConditionIssuerId;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _Description;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        long _Id;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _InputClaimType;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _InputClaimValue;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        Issuer _Issuer;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        long _IssuerId;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _OutputClaimType;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        string _OutputClaimValue;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        RuleGroup _RuleGroup;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        long _RuleGroupId;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        bool _SystemReserved;

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        byte[] _Version;

        /// <summary>
        ///   There are no comments for Property Id in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public long Id
        {
            get { return this._Id; }
            set
            {
                this.OnIdChanging(value);
                this._Id = value;
                this.OnIdChanged();
                this.OnPropertyChanged("Id");
            }
        }

        /// <summary>
        ///   There are no comments for Property RuleGroupId in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public long RuleGroupId
        {
            get { return this._RuleGroupId; }
            set
            {
                this.OnRuleGroupIdChanging(value);
                this._RuleGroupId = value;
                this.OnRuleGroupIdChanged();
                this.OnPropertyChanged("RuleGroupId");
            }
        }

        /// <summary>
        ///   There are no comments for Property IssuerId in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public long IssuerId
        {
            get { return this._IssuerId; }
            set
            {
                this.OnIssuerIdChanging(value);
                this._IssuerId = value;
                this.OnIssuerIdChanged();
                this.OnPropertyChanged("IssuerId");
            }
        }

        /// <summary>
        ///   There are no comments for Property Description in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string Description
        {
            get { return this._Description; }
            set
            {
                this.OnDescriptionChanging(value);
                this._Description = value;
                this.OnDescriptionChanged();
                this.OnPropertyChanged("Description");
            }
        }

        /// <summary>
        ///   There are no comments for Property InputClaimType in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string InputClaimType
        {
            get { return this._InputClaimType; }
            set
            {
                this.OnInputClaimTypeChanging(value);
                this._InputClaimType = value;
                this.OnInputClaimTypeChanged();
                this.OnPropertyChanged("InputClaimType");
            }
        }

        /// <summary>
        ///   There are no comments for Property InputClaimValue in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string InputClaimValue
        {
            get { return this._InputClaimValue; }
            set
            {
                this.OnInputClaimValueChanging(value);
                this._InputClaimValue = value;
                this.OnInputClaimValueChanged();
                this.OnPropertyChanged("InputClaimValue");
            }
        }

        /// <summary>
        ///   There are no comments for Property ConditionIssuerId in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public long ConditionIssuerId
        {
            get { return this._ConditionIssuerId; }
            set
            {
                this.OnConditionIssuerIdChanging(value);
                this._ConditionIssuerId = value;
                this.OnConditionIssuerIdChanged();
                this.OnPropertyChanged("ConditionIssuerId");
            }
        }

        /// <summary>
        ///   There are no comments for Property ConditionClaimType in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string ConditionClaimType
        {
            get { return this._ConditionClaimType; }
            set
            {
                this.OnConditionClaimTypeChanging(value);
                this._ConditionClaimType = value;
                this.OnConditionClaimTypeChanged();
                this.OnPropertyChanged("ConditionClaimType");
            }
        }

        /// <summary>
        ///   There are no comments for Property ConditionClaimValue in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string ConditionClaimValue
        {
            get { return this._ConditionClaimValue; }
            set
            {
                this.OnConditionClaimValueChanging(value);
                this._ConditionClaimValue = value;
                this.OnConditionClaimValueChanged();
                this.OnPropertyChanged("ConditionClaimValue");
            }
        }

        /// <summary>
        ///   There are no comments for Property OutputClaimType in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string OutputClaimType
        {
            get { return this._OutputClaimType; }
            set
            {
                this.OnOutputClaimTypeChanging(value);
                this._OutputClaimType = value;
                this.OnOutputClaimTypeChanged();
                this.OnPropertyChanged("OutputClaimType");
            }
        }

        /// <summary>
        ///   There are no comments for Property OutputClaimValue in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public string OutputClaimValue
        {
            get { return this._OutputClaimValue; }
            set
            {
                this.OnOutputClaimValueChanging(value);
                this._OutputClaimValue = value;
                this.OnOutputClaimValueChanged();
                this.OnPropertyChanged("OutputClaimValue");
            }
        }

        /// <summary>
        ///   There are no comments for Property SystemReserved in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public bool SystemReserved
        {
            get { return this._SystemReserved; }
            set
            {
                this.OnSystemReservedChanging(value);
                this._SystemReserved = value;
                this.OnSystemReservedChanged();
                this.OnPropertyChanged("SystemReserved");
            }
        }

        /// <summary>
        ///   There are no comments for Property Version in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public byte[] Version
        {
            get
            {
                if ((this._Version != null))
                {
                    return ((byte[]) (this._Version.Clone()));
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.OnVersionChanging(value);
                this._Version = value;
                this.OnVersionChanged();
                this.OnPropertyChanged("Version");
            }
        }

        /// <summary>
        ///   There are no comments for Issuer in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public Issuer Issuer
        {
            get { return this._Issuer; }
            set
            {
                this._Issuer = value;
                this.OnPropertyChanged("Issuer");
            }
        }

        /// <summary>
        ///   There are no comments for RuleGroup in the schema.
        /// </summary>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public RuleGroup RuleGroup
        {
            get { return this._RuleGroup; }
            set
            {
                this._RuleGroup = value;
                this.OnPropertyChanged("RuleGroup");
            }
        }

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///   Create a new ConditionalRule object.
        /// </summary>
        /// <param name = "ID">Initial value of Id.</param>
        /// <param name = "ruleGroupId">Initial value of RuleGroupId.</param>
        /// <param name = "issuerId">Initial value of IssuerId.</param>
        /// <param name = "conditionIssuerId">Initial value of ConditionIssuerId.</param>
        /// <param name = "systemReserved">Initial value of SystemReserved.</param>
        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        public static ConditionalRule CreateConditionalRule(long ID, long ruleGroupId, long issuerId, long conditionIssuerId, bool systemReserved)
        {
            var conditionalRule = new ConditionalRule();
            conditionalRule.Id = ID;
            conditionalRule.RuleGroupId = ruleGroupId;
            conditionalRule.IssuerId = issuerId;
            conditionalRule.ConditionIssuerId = conditionIssuerId;
            conditionalRule.SystemReserved = systemReserved;
            return conditionalRule;
        }

        partial void OnIdChanging(long value);
        partial void OnIdChanged();

        partial void OnRuleGroupIdChanging(long value);
        partial void OnRuleGroupIdChanged();

        partial void OnIssuerIdChanging(long value);
        partial void OnIssuerIdChanged();

        partial void OnDescriptionChanging(string value);
        partial void OnDescriptionChanged();

        partial void OnInputClaimTypeChanging(string value);
        partial void OnInputClaimTypeChanged();

        partial void OnInputClaimValueChanging(string value);
        partial void OnInputClaimValueChanged();

        partial void OnConditionIssuerIdChanging(long value);
        partial void OnConditionIssuerIdChanged();

        partial void OnConditionClaimTypeChanging(string value);
        partial void OnConditionClaimTypeChanged();

        partial void OnConditionClaimValueChanging(string value);
        partial void OnConditionClaimValueChanged();

        partial void OnOutputClaimTypeChanging(string value);
        partial void OnOutputClaimTypeChanged();

        partial void OnOutputClaimValueChanging(string value);
        partial void OnOutputClaimValueChanged();

        partial void OnSystemReservedChanging(bool value);
        partial void OnSystemReservedChanged();

        partial void OnVersionChanging(byte[] value);
        partial void OnVersionChanged();

        [GeneratedCode("System.Data.Services.Design", "1.0.0")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}