// =====================================================================
//  This file is part of the Microsoft Dynamics CRM SDK code samples.
//
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//
//  This source code is intended only as a supplement to Microsoft
//  Development Tools and/or on-line documentation.  See these other
//  materials for detailed information regarding Microsoft code samples.
//
//  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//  PARTICULAR PURPOSE.
// =====================================================================

using Microsoft.Xrm.Sdk.Samples;
using Microsoft.Xrm.Sdk.Metadata.Query.Samples;
using Microsoft.Xrm.Sdk.Utility.Samples;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Linq;

// Implementation notes:
// Some SDK classes have the following methods.
// 1. LoadFromXml - It takes an XElement and returns its class instance, which is
// a static method so that other classes can call it without instantiating the class.
// 2. ToXml - It returns XML data for the SOAP request. This is not a static method
// as it needs the actual instance data to XML.
// 3. Some classes have a ToValueXml method, which is a core part of ToXml. The reason 
// is that different methods may need a different tag.

// There is some duplicate code in places which may be consolidated if necessary.
// The implementation is similar to that used in Sdk.Soap.js with slight changes 
// due to the language differences.
// For more information about Sdk.Soap.js, see http://code.msdn.microsoft.com/SdkSoapjs-9b51b99a

namespace Microsoft.Xrm.Sdk.Metadata.Samples
{
    public enum AssociatedMenuBehavior
    {
        UseCollectionName = 0,
        UseLabel = 1,
        DoNotDisplay = 2,
    }
    public enum AssociatedMenuGroup
    {
        Details = 0,
        Sales = 1,
        Service = 2,
        Marketing = 3,
    }
    public enum AttributeRequiredLevel
    {
        None = 0,
        SystemRequired = 1,
        ApplicationRequired = 2,
        Recommended = 3
    }
    public enum AttributeTypeCode
    {
        Boolean = 0,
        Customer = 1,
        DateTime = 2,
        Decimal = 3,
        Double = 4,
        Integer = 5,
        Lookup = 6,
        Memo = 7,
        Money = 8,
        Owner = 9,
        PartyList = 10,
        Picklist = 11,
        State = 12,
        Status = 13,
        String = 14,
        Uniqueidentifier = 15,
        CalendarRules = 16,
        Virtual = 17,
        BigInt = 18,
        ManagedProperty = 19,
        EntityName = 20,
    }
    public enum CascadeType
    {
        NoCascade = 0,
        Cascade = 1,
        Active = 2,
        UserOwned = 3,
        RemoveLink = 4,
        Restrict = 5
    }
    public enum DateTimeFormat
    {
        DateOnly = 0,
        DateAndTime = 1,
    }
    public enum EntityFilters
    {
        Default = 1,
        Entity = 1,
        Attributes = 2,
        Privileges = 4,
        Relationships = 8,
        All = 15
    }
    public enum ImeMode
    {
        Auto = 0,
        Inactive = 1,
        Active = 2,
        Disabled = 3,
    }
    public enum IntegerFormat
    {
        None = 0,
        Duration = 1,
        TimeZone = 2,
        Language = 3,
        Locale = 4
    }
    public enum ManagedPropertyEvaluationPriority
    {
        None,
        Low,
        Normal,
        High,
        Essential
    }
    public enum ManagedPropertyOperation
    {
        None,
        Create,
        Update,
        Delete = 4,
        CreateUpdate = 3,
        UpdateDelete = 6,
        All
    }
    public enum ManagedPropertyType
    {
        Operation,
        Attribute,
        CustomEvaluator,
        Custom
    }
    public enum OptionSetType
    {
        Picklist = 0,
        State = 1,
        Status = 2,
        Boolean = 3
    }
    public enum OwnershipTypes
    {
        None = 0,
        UserOwned = 1,
        TeamOwned = 2,
        BusinessOwned = 4,
        OrganizationOwned = 8,
        BusinessParented = 16,
    }
    public enum PrivilegeType
    {
        None = 0,
        Create = 1,
        Read = 2,
        Write = 3,
        Delete = 4,
        Assign = 5,
        Share = 6,
        Append = 7,
        AppendTo = 8,
    }
    public enum RelationshipType
    {
        Default = 0,
        OneToManyRelationship = 0,
        ManyToManyRelationship = 1,
    }
    public enum SecurityTypes
    {
        None = 0,
        Append = 1,
        ParentChild = 2,
        Pointer = 4,
        Inheritance = 8,
    }
    public enum StringFormat
    {
        Email = 0,
        Text = 1,
        TextArea = 2,
        Url = 3,
        TickerSymbol = 4,
        PhoneticGuide = 5,
        VersionNumber = 6,
        Phone
    }
    public sealed class AssociatedMenuConfiguration
    {
        public AssociatedMenuBehavior? Behavior { get; set; }
        public AssociatedMenuGroup? Group { get; set; }
        public Label Label { get; set; }
        public int? Order { get; set; }
        internal string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Util.ObjectToXml(Behavior, "h:Behavior", true));
            sb.Append(Util.ObjectToXml(Group, "h:Group", true));
            sb.Append(Util.ObjectToXml(Label, "h:Label", true));
            sb.Append(Util.ObjectToXml(Order, "h:Order", true));
            return sb.ToString();
        }
        static internal AssociatedMenuConfiguration LoadFromXml(XElement item)
        {
            if (item.Elements().Count() == 0)
                return new AssociatedMenuConfiguration();
            AssociatedMenuConfiguration associatedMenuConfiguration = new AssociatedMenuConfiguration()
            {
                Behavior = Util.LoadFromXml<AssociatedMenuBehavior?>(item.Element(Util.ns.h + "Behavior")),
                Group = Util.LoadFromXml<AssociatedMenuGroup?>(item.Element(Util.ns.h + "Group")),
                Label = Label.LoadFromXml(item.Element(Util.ns.h + "Label")),
                Order = Util.LoadFromXml<int?>(item.Element(Util.ns.h + "Order"))
            };
            return associatedMenuConfiguration;
        }
    }

    [KnownType(typeof(BooleanAttributeMetadata))]
    [KnownType(typeof(DateTimeAttributeMetadata))]
    [KnownType(typeof(DecimalAttributeMetadata))]
    [KnownType(typeof(DoubleAttributeMetadata))]
    [KnownType(typeof(EntityNameAttributeMetadata))]
    [KnownType(typeof(ImageAttributeMetadata))]
    [KnownType(typeof(IntegerAttributeMetadata))]
    [KnownType(typeof(BigIntAttributeMetadata))]
    [KnownType(typeof(LookupAttributeMetadata))]
    [KnownType(typeof(MemoAttributeMetadata))]
    [KnownType(typeof(MoneyAttributeMetadata))]
    [KnownType(typeof(PicklistAttributeMetadata))]
    [KnownType(typeof(StateAttributeMetadata))]
    [KnownType(typeof(StatusAttributeMetadata))]
    [KnownType(typeof(StringAttributeMetadata))]
    [KnownType(typeof(ManagedPropertyAttributeMetadata))]
    public class AttributeMetadata : MetadataBase
    {
        #region member
        public string AttributeOf { get; set; }
        public AttributeTypeCode? AttributeType { get; set; }
        public AttributeTypeDisplayName AttributeTypeName { get; set; }
        public bool? CanBeSecuredForCreate { get; set; }
        public bool? CanBeSecuredForRead { get; set; }
        public bool? CanBeSecuredForUpdate { get; set; }
        public BooleanManagedProperty CanModifyAdditionalSettings { get; set; }
        public int? ColumnNumber { get; set; }
        public string DeprecatedVersion { get; set; }
        public Label Description { get; set; }
        public Label DisplayName { get; set; }
        public string EntityLogicalName { get; set; }
        public string IntroducedVersion { get; set; }
        public BooleanManagedProperty IsAuditEnabled { get; set; }
        public bool? IsCustomAttribute { get; set; }
        public BooleanManagedProperty IsCustomizable { get; set; }
        public bool? IsLogical { get; set; }
        public bool? IsManaged { get; set; }
        public bool? IsPrimaryId { get; set; }
        public bool? IsPrimaryName { get; set; }
        public BooleanManagedProperty IsRenameable { get; set; }
        public bool? IsSecured { get; set; }
        public BooleanManagedProperty IsValidForAdvancedFind { get; set; }
        public bool? IsValidForCreate { get; set; }
        public bool? IsValidForRead { get; set; }
        public bool? IsValidForUpdate { get; set; }
        public Guid? LinkedAttributeId { get; set; }
        public string LogicalName { get; set; }
        public AttributeRequiredLevelManagedProperty RequiredLevel { get; set; }
        public string SchemaName { get; set; }
        public int? SourceType { get; set; }
        #endregion member
        public AttributeMetadata() { }
        protected AttributeMetadata(AttributeTypeCode attributeType)
        {
            this.AttributeType = attributeType;
            this.AttributeTypeName = GetAttributeTypeDisplayName(attributeType);
        }
        protected AttributeMetadata(AttributeTypeCode attributeType, string schemaName)
            : this(attributeType)
        {
            this.SchemaName = schemaName;
        }
        private AttributeTypeDisplayName GetAttributeTypeDisplayName(AttributeTypeCode attributeType)
        {
            switch (attributeType)
            {
                case AttributeTypeCode.Boolean:
                    return AttributeTypeDisplayName.BooleanType;
                case AttributeTypeCode.Customer:
                    return AttributeTypeDisplayName.CustomerType;
                case AttributeTypeCode.DateTime:
                    return AttributeTypeDisplayName.DateTimeType;
                case AttributeTypeCode.Decimal:
                    return AttributeTypeDisplayName.DecimalType;
                case AttributeTypeCode.Double:
                    return AttributeTypeDisplayName.DoubleType;
                case AttributeTypeCode.Integer:
                    return AttributeTypeDisplayName.IntegerType;
                case AttributeTypeCode.Lookup:
                    return AttributeTypeDisplayName.LookupType;
                case AttributeTypeCode.Memo:
                    return AttributeTypeDisplayName.MemoType;
                case AttributeTypeCode.Money:
                    return AttributeTypeDisplayName.MoneyType;
                case AttributeTypeCode.Owner:
                    return AttributeTypeDisplayName.OwnerType;
                case AttributeTypeCode.PartyList:
                    return AttributeTypeDisplayName.PartyListType;
                case AttributeTypeCode.Picklist:
                    return AttributeTypeDisplayName.PicklistType;
                case AttributeTypeCode.State:
                    return AttributeTypeDisplayName.StateType;
                case AttributeTypeCode.Status:
                    return AttributeTypeDisplayName.StatusType;
                case AttributeTypeCode.String:
                    return AttributeTypeDisplayName.StringType;
                case AttributeTypeCode.Uniqueidentifier:
                    return AttributeTypeDisplayName.UniqueidentifierType;
                case AttributeTypeCode.CalendarRules:
                    return AttributeTypeDisplayName.CalendarRulesType;
                case AttributeTypeCode.Virtual:
                    return AttributeTypeDisplayName.VirtualType;
                case AttributeTypeCode.BigInt:
                    return AttributeTypeDisplayName.BigIntType;
                case AttributeTypeCode.ManagedProperty:
                    return AttributeTypeDisplayName.ManagedPropertyType;
                case AttributeTypeCode.EntityName:
                    return AttributeTypeDisplayName.EntityNameType;
            }
            return null;
        }
        internal new string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToValueXml());
            sb.Append(Util.ObjectToXml(AttributeOf, "h:AttributeOf", true));
            sb.Append(Util.ObjectToXml(AttributeType, "h:AttributeType", true));
            sb.Append(Util.ObjectToXml(CanBeSecuredForCreate, "h:CanBeSecuredForCreate", true));
            sb.Append(Util.ObjectToXml(CanBeSecuredForRead, "h:CanBeSecuredForRead", true));
            sb.Append(Util.ObjectToXml(CanBeSecuredForUpdate, "h:CanBeSecuredForUpdate", true));
            sb.Append(Util.ObjectToXml(CanModifyAdditionalSettings, "h:CanModifyAdditionalSettings", true));
            sb.Append(Util.ObjectToXml(ColumnNumber, "h:ColumnNumber", true));
            sb.Append(Util.ObjectToXml(DeprecatedVersion, "h:DeprecatedVersion", true));
            sb.Append(Util.ObjectToXml(Description, "h:Description", true));
            sb.Append(Util.ObjectToXml(DisplayName, "h:DisplayName", true));
            sb.Append(Util.ObjectToXml(EntityLogicalName, "h:EntityLogicalName", true));
            sb.Append(Util.ObjectToXml(IsAuditEnabled, "h:IsAuditEnabled", true));
            sb.Append(Util.ObjectToXml(IsCustomAttribute, "h:IsCustomAttribute", true));
            sb.Append(Util.ObjectToXml(IsCustomizable, "h:IsCustomizable", true));
            sb.Append(Util.ObjectToXml(IsLogical, "h:IsLogical", true));
            sb.Append(Util.ObjectToXml(IsManaged, "h:IsManaged", true));
            sb.Append(Util.ObjectToXml(IsPrimaryId, "h:IsPrimaryId", true));
            sb.Append(Util.ObjectToXml(IsPrimaryName, "h:IsPrimaryName", true));
            sb.Append(Util.ObjectToXml(IsRenameable, "h:IsRenameable", true));
            sb.Append(Util.ObjectToXml(IsSecured, "h:IsSecured", true));
            sb.Append(Util.ObjectToXml(IsValidForAdvancedFind, "h:IsValidForAdvancedFind", true));
            sb.Append(Util.ObjectToXml(IsValidForCreate, "h:IsValidForCreate", true));
            sb.Append(Util.ObjectToXml(IsValidForRead, "h:IsValidForRead", true));
            sb.Append(Util.ObjectToXml(IsValidForUpdate, "h:IsValidForUpdate", true));
            sb.Append(Util.ObjectToXml(LinkedAttributeId, "h:LinkedAttributeId", true));
            sb.Append(Util.ObjectToXml(LogicalName, "h:LogicalName", true));
            sb.Append(Util.ObjectToXml(RequiredLevel, "h:RequiredLevel", true));
            sb.Append(Util.ObjectToXml(SchemaName, "h:SchemaName", true));
            sb.Append(Util.ObjectToXml(SourceType, "h:SourceType", true));
            sb.Append(Util.ObjectToXml(AttributeTypeName, "h:AttributeTypeName", true));
            sb.Append(Util.ObjectToXml(IntroducedVersion, "h:IntroducedVersion", true));
            return sb.ToString();
        }
        internal string ToXml(AttributeMetadata meta, string action = null)
        {
            string s = "";
            switch (meta.GetType().Name)
            {
                case "ImageAttributeMetadata":
                    s = Util.ObjectToXml((ImageAttributeMetadata)meta, action);
                    break;
                case "MoneyAttributeMetadata":
                    s = Util.ObjectToXml((MoneyAttributeMetadata)meta, action);
                    break;
                case "PicklistAttributeMetadata":
                    s = Util.ObjectToXml((PicklistAttributeMetadata)meta, action);
                    break;
                case "MemoAttributeMetadata":
                    s = Util.ObjectToXml((MemoAttributeMetadata)meta, action);
                    break;
                case "ManagedPropertyAttributeMetadata":
                    s = Util.ObjectToXml((ManagedPropertyAttributeMetadata)meta, action);
                    break;
                case "BooleanAttributeMetadata":
                    s = Util.ObjectToXml((BooleanAttributeMetadata)meta, action);
                    break;
                case "DateTimeAttributeMetadata":
                    s = Util.ObjectToXml((DateTimeAttributeMetadata)meta, action);
                    break;
                case "DecimalAttributeMetadata":
                    s = Util.ObjectToXml((DecimalAttributeMetadata)meta, action);
                    break;
                case "DoubleAttributeMetadata":
                    s = Util.ObjectToXml((DoubleAttributeMetadata)meta, action);
                    break;
                case "EntityNameAttributeMetadata":
                    s = Util.ObjectToXml((EntityNameAttributeMetadata)meta, action);
                    break;
                case "IntegerAttributeMetadata":
                    s = Util.ObjectToXml((IntegerAttributeMetadata)meta, action);
                    break;
                case "BigIntAttributeMetadata":
                    s = Util.ObjectToXml((BigIntAttributeMetadata)meta, action);
                    break;
                case "LookupAttributeMetadata":
                    s = Util.ObjectToXml((LookupAttributeMetadata)meta, action);
                    break;
                case "StateAttributeMetadata":
                    s = Util.ObjectToXml((StateAttributeMetadata)meta, action);
                    break;
                case "StatusAttributeMetadata":
                    s = Util.ObjectToXml((StatusAttributeMetadata)meta, action);
                    break;
                case "StringAttributeMetadata":
                    s = Util.ObjectToXml((StringAttributeMetadata)meta, action);
                    break;
            }
            return s;
        }
        static internal AttributeMetadata LoadFromXml(XElement item)
        {
            AttributeMetadata attributeMetadata = new AttributeMetadata();
            string type = (item.Attribute(Util.ns.i + "type") == null) ? "AttributeMetadata" :
                        item.Attribute(Util.ns.i + "type").Value.Substring(2);
            switch (type)
            {
                case "ImageAttributeMetadata":
                    attributeMetadata = ImageAttributeMetadata.LoadFromXml(item);
                    break;
                case "MoneyAttributeMetadata":
                    attributeMetadata = MoneyAttributeMetadata.LoadFromXml(item);
                    break;
                case "PicklistAttributeMetadata":
                    attributeMetadata = PicklistAttributeMetadata.LoadFromXml(item);
                    break;
                case "MemoAttributeMetadata":
                    attributeMetadata = MemoAttributeMetadata.LoadFromXml(item);
                    break;
                case "ManagedPropertyAttributeMetadata":
                    attributeMetadata = ManagedPropertyAttributeMetadata.LoadFromXml(item);
                    break;
                case "BooleanAttributeMetadata":
                    attributeMetadata = BooleanAttributeMetadata.LoadFromXml(item);
                    break;
                case "DateTimeAttributeMetadata":
                    attributeMetadata = DateTimeAttributeMetadata.LoadFromXml(item);
                    break;
                case "DecimalAttributeMetadata":
                    attributeMetadata = DecimalAttributeMetadata.LoadFromXml(item);
                    break;
                case "DoubleAttributeMetadata":
                    attributeMetadata = DoubleAttributeMetadata.LoadFromXml(item);
                    break;
                case "EntityNameAttributeMetadata":
                    attributeMetadata = EntityNameAttributeMetadata.LoadFromXml(item);
                    break;
                case "IntegerAttributeMetadata":
                    attributeMetadata = IntegerAttributeMetadata.LoadFromXml(item);
                    break;
                case "BigIntAttributeMetadata":
                    attributeMetadata = BigIntAttributeMetadata.LoadFromXml(item);
                    break;
                case "LookupAttributeMetadata":
                    attributeMetadata = LookupAttributeMetadata.LoadFromXml(item);
                    break;
                case "StateAttributeMetadata":
                    attributeMetadata = StateAttributeMetadata.LoadFromXml(item);
                    break;
                case "StatusAttributeMetadata":
                    attributeMetadata = StatusAttributeMetadata.LoadFromXml(item);
                    break;
                case "StringAttributeMetadata":
                    attributeMetadata = StringAttributeMetadata.LoadFromXml(item);
                    break;
                default:
                    AttributeMetadata.LoadFromXml(item, attributeMetadata);
                    break;
            }
            return attributeMetadata;
        }
        static internal void LoadFromXml(XElement item, AttributeMetadata meta)
        {
            if (item.Elements().Count() == 0)
                return;
            MetadataBase.LoadFromXml(item, meta);
            meta.AttributeOf = Util.LoadFromXml<string>(item.Element(Util.ns.h + "AttributeOf"));
            meta.AttributeTypeName = AttributeTypeDisplayName.LoadFromXml(item.Element(Util.ns.h + "AttributeTypeName"));
            meta.AttributeType = Util.LoadFromXml<AttributeTypeCode?>(item.Element(Util.ns.h + "AttributeType"));
            meta.CanBeSecuredForCreate = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "CanBeSecuredForCreate"));
            meta.CanBeSecuredForRead = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "CanBeSecuredForRead"));
            meta.CanBeSecuredForUpdate = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "CanBeSecuredForUpdate"));
            meta.CanModifyAdditionalSettings = BooleanManagedProperty.LoadFromXml(item.Element(Util.ns.h + "CanModifyAdditionalSettings"));
            meta.ColumnNumber = Util.LoadFromXml<int?>(item.Element(Util.ns.h + "ColumnNumber"));
            meta.DeprecatedVersion = Util.LoadFromXml<string>(item.Element(Util.ns.h + "DeprecatedVersion"));
            meta.Description = Label.LoadFromXml(item.Element(Util.ns.h + "Description"));
            meta.DisplayName = Label.LoadFromXml(item.Element(Util.ns.h + "DisplayName"));
            meta.EntityLogicalName = Util.LoadFromXml<string>(item.Element(Util.ns.h + "EntityLogicalName"));
            meta.IntroducedVersion = Util.LoadFromXml<string>(item.Element(Util.ns.h + "IntroducedVersion"));
            meta.IsAuditEnabled = BooleanManagedProperty.LoadFromXml(item.Element(Util.ns.h + "IsAuditEnabled"));
            meta.IsCustomAttribute = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "IsCustomAttribute"));
            meta.IsCustomizable = BooleanManagedProperty.LoadFromXml(item.Element(Util.ns.h + "IsCustomizable"));
            meta.IsLogical = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "IsLogical"));
            meta.IsManaged = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "IsManaged"));
            meta.IsPrimaryId = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "IsPrimaryId"));
            meta.IsPrimaryName = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "IsPrimaryName"));
            meta.IsRenameable = BooleanManagedProperty.LoadFromXml(item.Element(Util.ns.h + "IsRenameable"));
            meta.IsSecured = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "IsSecured"));
            meta.IsValidForAdvancedFind = BooleanManagedProperty.LoadFromXml(item.Element(Util.ns.h + "IsValidForAdvancedFind"));
            meta.IsValidForCreate = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "IsValidForCreate"));
            meta.IsValidForRead = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "IsValidForRead"));
            meta.IsValidForUpdate = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "IsValidForUpdate"));
            meta.LinkedAttributeId = Util.LoadFromXml<Guid?>(item.Element(Util.ns.h + "LinkedAttributeId"));
            meta.LogicalName = Util.LoadFromXml<string>(item.Element(Util.ns.h + "LogicalName"));
            meta.RequiredLevel = AttributeRequiredLevelManagedProperty.LoadFromXml(item.Element(Util.ns.h + "RequiredLevel"));
            meta.SchemaName = Util.LoadFromXml<string>(item.Element(Util.ns.h + "SchemaName"));
            meta.SourceType = Util.LoadFromXml<int?>(item.Element(Util.ns.h + "SourceType"));
        }
    }
    public sealed class AttributeRequiredLevelManagedProperty : ManagedProperty<AttributeRequiredLevel>
    {
        public AttributeRequiredLevelManagedProperty() { }
        public AttributeRequiredLevelManagedProperty(AttributeRequiredLevel value)
        {
            this.Value = value;
        }
        internal new string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToValueXml());
            sb.Append(Util.ObjectToXml(Value, "a:Value", true));
            return sb.ToString();
        }
        static internal AttributeRequiredLevelManagedProperty LoadFromXml(XElement item)
        {
            if (item.Elements().Count() == 0)
                return new AttributeRequiredLevelManagedProperty();
            AttributeRequiredLevelManagedProperty attributeRequiredLevelManagedProperty = new AttributeRequiredLevelManagedProperty();
            ManagedProperty<AttributeRequiredLevel>.LoadFromXml(item, attributeRequiredLevelManagedProperty);
            attributeRequiredLevelManagedProperty.Value = Util.LoadFromXml<AttributeRequiredLevel>(item.Element(Util.ns.a + "Value"));
            return attributeRequiredLevelManagedProperty;
        }
    }
    public sealed class AttributeTypeDisplayName : ConstantsBase<string>
    {
        public static readonly AttributeTypeDisplayName BigIntType;
        public static readonly AttributeTypeDisplayName CustomerType;
        public static readonly AttributeTypeDisplayName BooleanType;
        public static readonly AttributeTypeDisplayName CalendarRulesType;
        public static readonly AttributeTypeDisplayName CustomType;
        public static readonly AttributeTypeDisplayName DateTimeType;
        public static readonly AttributeTypeDisplayName DecimalType;
        public static readonly AttributeTypeDisplayName DoubleType;
        public static readonly AttributeTypeDisplayName EntityNameType;
        public static readonly AttributeTypeDisplayName ImageType;
        public static readonly AttributeTypeDisplayName IntegerType;
        public static readonly AttributeTypeDisplayName LookupType;
        public static readonly AttributeTypeDisplayName ManagedPropertyType;
        public static readonly AttributeTypeDisplayName MemoType;
        public static readonly AttributeTypeDisplayName MoneyType;
        public static readonly AttributeTypeDisplayName OwnerType;
        public static readonly AttributeTypeDisplayName PartyListType;
        public static readonly AttributeTypeDisplayName PicklistType;
        public static readonly AttributeTypeDisplayName StateType;
        public static readonly AttributeTypeDisplayName StatusType;
        public static readonly AttributeTypeDisplayName StringType;
        public static readonly AttributeTypeDisplayName UniqueidentifierType;
        public static readonly AttributeTypeDisplayName VirtualType;
        public AttributeTypeDisplayName() { }
        static AttributeTypeDisplayName()
        {
            AttributeTypeDisplayName.BooleanType = Add<AttributeTypeDisplayName>("BooleanType");
            AttributeTypeDisplayName.CustomerType = Add<AttributeTypeDisplayName>("CustomerType");
            AttributeTypeDisplayName.DateTimeType = Add<AttributeTypeDisplayName>("DateTimeType");
            AttributeTypeDisplayName.DecimalType = Add<AttributeTypeDisplayName>("DecimalType");
            AttributeTypeDisplayName.DoubleType = Add<AttributeTypeDisplayName>("DoubleType");
            AttributeTypeDisplayName.IntegerType = Add<AttributeTypeDisplayName>("IntegerType");
            AttributeTypeDisplayName.LookupType = Add<AttributeTypeDisplayName>("LookupType");
            AttributeTypeDisplayName.MemoType = Add<AttributeTypeDisplayName>("MemoType");
            AttributeTypeDisplayName.MoneyType = Add<AttributeTypeDisplayName>("MoneyType");
            AttributeTypeDisplayName.OwnerType = Add<AttributeTypeDisplayName>("OwnerType");
            AttributeTypeDisplayName.PartyListType = Add<AttributeTypeDisplayName>("PartyListType");
            AttributeTypeDisplayName.PicklistType = Add<AttributeTypeDisplayName>("PicklistType");
            AttributeTypeDisplayName.StateType = Add<AttributeTypeDisplayName>("StateType");
            AttributeTypeDisplayName.StatusType = Add<AttributeTypeDisplayName>("StatusType");
            AttributeTypeDisplayName.StringType = Add<AttributeTypeDisplayName>("StringType");
            AttributeTypeDisplayName.UniqueidentifierType = Add<AttributeTypeDisplayName>("UniqueidentifierType");
            AttributeTypeDisplayName.CalendarRulesType = Add<AttributeTypeDisplayName>("CalendarRulesType");
            AttributeTypeDisplayName.VirtualType = Add<AttributeTypeDisplayName>("VirtualType");
            AttributeTypeDisplayName.BigIntType = Add<AttributeTypeDisplayName>("BigIntType");
            AttributeTypeDisplayName.ManagedPropertyType = Add<AttributeTypeDisplayName>("ManagedPropertyType");
            AttributeTypeDisplayName.EntityNameType = Add<AttributeTypeDisplayName>("EntityNameType");
            AttributeTypeDisplayName.ImageType = Add<AttributeTypeDisplayName>("ImageType");
        }
        protected override bool ValueExistsInList(String value)
        {
            return ValidValues.Contains(value, StringComparer.OrdinalIgnoreCase);
        }
        internal string ToValueXml()
        {
            return Util.ObjectToXml(Value, "k:Value", true);
        }
        static internal AttributeTypeDisplayName LoadFromXml(XElement item)
        {
            AttributeTypeDisplayName attributeTypeDisplayName = new AttributeTypeDisplayName();
            if (item.Elements().Count() == 0)
                return attributeTypeDisplayName;

            attributeTypeDisplayName.Value = Util.LoadFromXml<string>(item.Element(Util.ns.k + "Value"));
            return attributeTypeDisplayName;
        }
    }
    public sealed class BigIntAttributeMetadata : AttributeMetadata
    {
        public const long MaxSupportedValue = 9223372036854775807;
        public const long MinSupportedValue = -9223372036854775808;
        private long? _maxValue;
        public long? MaxValue
        {
            get { return _maxValue; }
            set
            {
                if (value < MinSupportedValue || value > MaxSupportedValue)
                    // Should throw error?
                    return;
                if (_minValue != null && value < _minValue)
                    return;
                _maxValue = value;
            }
        }
        private long? _minValue;
        public long? MinValue
        {
            get { return _minValue; }
            set
            {
                if (value < MinSupportedValue || value > MaxSupportedValue)
                    // Should throw error?
                    return;
                if (_maxValue != null && value > _maxValue)
                    return;
                _minValue = value;
            }
        }
        public BigIntAttributeMetadata() : this(null) { }
        public BigIntAttributeMetadata(string schemaName)
            : base(AttributeTypeCode.BigInt, schemaName) { }
        internal new string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToValueXml());
            sb.Append(Util.ObjectToXml(MaxValue, "h:MaxValue", true));
            sb.Append(Util.ObjectToXml(MinValue, "h:MinValue", true));
            return sb.ToString();
        }
        static internal new BigIntAttributeMetadata LoadFromXml(XElement item)
        {
            BigIntAttributeMetadata bigIntAttributeMetadata = new BigIntAttributeMetadata();
            AttributeMetadata.LoadFromXml(item, bigIntAttributeMetadata);
            bigIntAttributeMetadata.MaxValue = Util.LoadFromXml<long?>(item.Element(Util.ns.h + "MaxValue"));
            bigIntAttributeMetadata.MinValue = Util.LoadFromXml<long?>(item.Element(Util.ns.h + "MinValue"));
            return bigIntAttributeMetadata;
        }
    }
    public sealed class BooleanAttributeMetadata : AttributeMetadata
    {
        public bool? DefaultValue { get; set; }
        public string FormulaDefinition { get; set; }
        public BooleanOptionSetMetadata OptionSet { get; set; }
        public int? SourceTypeMask { get; set; }
        public BooleanAttributeMetadata() : this(null) { }
        public BooleanAttributeMetadata(string schemaName, BooleanOptionSetMetadata optionSet = null)
            : base(AttributeTypeCode.Boolean, schemaName)
        {
            this.OptionSet = optionSet;
        }
        internal new string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToValueXml());
            sb.Append(Util.ObjectToXml(DefaultValue, "h:DefaultValue", true));
            sb.Append(Util.ObjectToXml(FormulaDefinition, "h:FormulaDefinition", true));
            sb.Append(Util.ObjectToXml(OptionSet, "h:OptionSet", true));
            sb.Append(Util.ObjectToXml(SourceTypeMask, "h:SourceTypeMask", true));
            return sb.ToString();
        }
        static internal new BooleanAttributeMetadata LoadFromXml(XElement item)
        {
            BooleanAttributeMetadata booleanAttributeMetadata = new BooleanAttributeMetadata();
            AttributeMetadata.LoadFromXml(item, booleanAttributeMetadata);
            booleanAttributeMetadata.DefaultValue = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "DefaultValue"));
            booleanAttributeMetadata.FormulaDefinition = Util.LoadFromXml<string>(item.Element(Util.ns.h + "FormulaDefinition"));
            booleanAttributeMetadata.OptionSet = BooleanOptionSetMetadata.LoadFromXml(item.Element(Util.ns.h + "OptionSet"));
            booleanAttributeMetadata.SourceTypeMask = Util.LoadFromXml<int?>(item.Element(Util.ns.h + "SourceTypeMask"));
            return booleanAttributeMetadata;
        }
    }
    public sealed class BooleanOptionSetMetadata : OptionSetMetadataBase
    {
        public OptionMetadata FalseOption { get; set; }
        public OptionMetadata TrueOption { get; set; }
        public BooleanOptionSetMetadata() { }
        public BooleanOptionSetMetadata(OptionMetadata trueOption, OptionMetadata falseOption)
        {
            this.FalseOption = falseOption;
            this.TrueOption = trueOption;
        }
        internal new string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToValueXml());
            sb.Append(Util.ObjectToXml(FalseOption, "h:FalseOption", true));
            sb.Append(Util.ObjectToXml(TrueOption, "h:TrueOption", true));
            return sb.ToString();
        }
        static internal new BooleanOptionSetMetadata LoadFromXml(XElement item)
        {
            BooleanOptionSetMetadata booleanOptionSetMetadata = new BooleanOptionSetMetadata();
            BooleanOptionSetMetadata.LoadFromXml(item, booleanOptionSetMetadata);
            return booleanOptionSetMetadata;
        }
        static internal void LoadFromXml(XElement item, BooleanOptionSetMetadata meta)
        {
            if (item.Elements().Count() == 0)
                return;
            OptionSetMetadataBase.LoadFromXml(item, meta);
            meta.FalseOption = OptionMetadata.LoadFromXml(item.Element(Util.ns.h + "FalseOption"));
            meta.TrueOption = OptionMetadata.LoadFromXml(item.Element(Util.ns.h + "TrueOption"));
        }
    }
    public sealed class CascadeConfiguration
    {
        public CascadeType? Assign { get; set; }
        public CascadeType? Delete { get; set; }
        public CascadeType? Merge { get; set; }
        public CascadeType? Reparent { get; set; }
        public CascadeType? Share { get; set; }
        public CascadeType? Unshare { get; set; }
        internal string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Util.ObjectToXml(Assign, "h:Assign", true));
            sb.Append(Util.ObjectToXml(Delete, "h:Delete", true));
            sb.Append(Util.ObjectToXml(Merge, "h:Merge", true));
            sb.Append(Util.ObjectToXml(Reparent, "h:Reparent", true));
            sb.Append(Util.ObjectToXml(Share, "h:Share", true));
            sb.Append(Util.ObjectToXml(Unshare, "h:Unshare", true));
            return sb.ToString();
        }
        static internal CascadeConfiguration LoadFromXml(XElement item)
        {
            if (item.Elements().Count() == 0)
                return new CascadeConfiguration();
            CascadeConfiguration cascadeConfiguration = new CascadeConfiguration()
            {
                Assign = Util.LoadFromXml<CascadeType?>(item.Element(Util.ns.h + "Assign")),
                Delete = Util.LoadFromXml<CascadeType?>(item.Element(Util.ns.h + "Delete")),
                Merge = Util.LoadFromXml<CascadeType?>(item.Element(Util.ns.h + "Merge")),
                Reparent = Util.LoadFromXml<CascadeType?>(item.Element(Util.ns.h + "Reparent")),
                Share = Util.LoadFromXml<CascadeType?>(item.Element(Util.ns.h + "Share")),
                Unshare = Util.LoadFromXml<CascadeType?>(item.Element(Util.ns.h + "Unshare")),
            };
            return cascadeConfiguration;
        }
    }
    public abstract class ConstantsBase<T>
    {
        protected static readonly IList<T> ValidValues = new List<T>();
        private T _value;
        public T Value
        {
            get { return _value; }
            set
            {
                _value = value;
            }
        }
        protected static T2 Add<T2>(T value) where T2 : ConstantsBase<T>, new()
        {
            ValidValues.Add(value);
            return Create<T2>(value);
        }
        protected static T2 Create<T2>(T value) where T2 : ConstantsBase<T>, new()
        {
            return new T2 { _value = value };
        }
        protected abstract bool ValueExistsInList(T value);
    }
    public sealed class DateTimeAttributeMetadata : AttributeMetadata
    {
        public DateTimeFormat? Format { get; set; }
        public string FormulaDefinition { get; set; }
        public ImeMode? ImeMode { get; set; }
        public static DateTime MaxSupportedValue { get; set; }
        public static DateTime MinSupportedValue { get; set; }
        public int? SourceTypeMask { get; set; }
        public DateTimeAttributeMetadata() : this(null, null) { }
        public DateTimeAttributeMetadata(DateTimeFormat? format) : this(format, null) { }
        public DateTimeAttributeMetadata(DateTimeFormat? format, string schemaName)
            : base(AttributeTypeCode.DateTime, schemaName)
        {
            this.Format = format;
        }
        internal new string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToValueXml());
            sb.Append(Util.ObjectToXml(Format, "h:Format", true));
            sb.Append(Util.ObjectToXml(FormulaDefinition, "h:FormulaDefinition", true));
            sb.Append(Util.ObjectToXml(ImeMode, "h:ImeMode", true));
            sb.Append(Util.ObjectToXml(MaxSupportedValue, "h:MaxSupportedValue", true));
            sb.Append(Util.ObjectToXml(MinSupportedValue, "h:MinSupportedValue", true));
            sb.Append(Util.ObjectToXml(SourceTypeMask, "h:SourceTypeMask", true));
            return sb.ToString();
        }
        static internal new DateTimeAttributeMetadata LoadFromXml(XElement item)
        {
            DateTimeAttributeMetadata dateTimeAttributeMetadata = new DateTimeAttributeMetadata();
            AttributeMetadata.LoadFromXml(item, dateTimeAttributeMetadata);
            dateTimeAttributeMetadata.Format = Util.LoadFromXml<DateTimeFormat?>(item.Element(Util.ns.h + "Format"));
            dateTimeAttributeMetadata.FormulaDefinition = Util.LoadFromXml<string>(item.Element(Util.ns.h + "FormulaDefinition"));
            dateTimeAttributeMetadata.ImeMode = Util.LoadFromXml<ImeMode?>(item.Element(Util.ns.h + "ImeMode"));
            if (item.Element(Util.ns.h + "MaxSupportedValue") != null)
                DateTimeAttributeMetadata.MaxSupportedValue = Util.LoadFromXml<DateTime>(item.Element(Util.ns.h + "MaxSupportedValue"));
            if (item.Element(Util.ns.h + "MinSupportedValue") != null)
                DateTimeAttributeMetadata.MinSupportedValue = Util.LoadFromXml<DateTime>(item.Element(Util.ns.h + "MinSupportedValue"));
            dateTimeAttributeMetadata.SourceTypeMask = Util.LoadFromXml<int?>(item.Element(Util.ns.h + "SourceTypeMask"));
            return dateTimeAttributeMetadata;
        }
    }
    public sealed class DecimalAttributeMetadata : AttributeMetadata
    {
       
        public const int MaxSupportedPrecision = 10;
        public const int MinSupportedPrecision = 0;
        public const decimal MaxSupportedValue = 100000000000;
        public const decimal MinSupportedValue = -100000000000;
        public string FormulaDefinition { get; set; }
        public ImeMode? ImeMode { get; set; }
        private decimal? _maxValue;
        public decimal? MaxValue
        {
            get { return _maxValue; }
            set
            {
                if (value < MinSupportedValue || value > MaxSupportedValue)
                    // Should throw error?
                    return;
                if (_minValue != null && value < _minValue)
                    _maxValue = value;
            }
        }
        private decimal? _minValue;
        public decimal? MinValue
        {
            get { return _minValue; }
            set
            {
                if (value < MinSupportedValue || value > MaxSupportedValue)
                    // Should throw error?
                    return;
                if (_maxValue != null && value > _maxValue)
                    return;
                _minValue = value;
            }
        }
        private int? _precision;
        public int? Precision
        {
            get { return _precision; }
            set
            {
                if (value < MinSupportedPrecision || value > MaxSupportedPrecision)
                    // Should throw error?
                    return;
                _precision = value;
            }
        }
        public int? SourceTypeMask { get; set; }
        public DecimalAttributeMetadata() : this(null) { }
        public DecimalAttributeMetadata(string schemaName)
            : base(AttributeTypeCode.Decimal, schemaName) { }
        internal new string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToValueXml());
            sb.Append(Util.ObjectToXml(ImeMode, "h:FormulaDefinition", true));
            sb.Append(Util.ObjectToXml(ImeMode, "h:ImeMode", true));
            sb.Append(Util.ObjectToXml(MaxValue, "h:MaxValue", true));
            sb.Append(Util.ObjectToXml(MinValue, "h:MinValue", true));
            sb.Append(Util.ObjectToXml(Precision, "h:Precision", true));
            sb.Append(Util.ObjectToXml(SourceTypeMask, "h:SourceTypeMask", true));
            return sb.ToString();
        }
        static internal new DecimalAttributeMetadata LoadFromXml(XElement item)
        {
            DecimalAttributeMetadata decimalAttributeMetadata = new DecimalAttributeMetadata();
            AttributeMetadata.LoadFromXml(item, decimalAttributeMetadata);
            decimalAttributeMetadata.FormulaDefinition = Util.LoadFromXml<string>(item.Element(Util.ns.h + "FormulaDefinition"));
            decimalAttributeMetadata.ImeMode = Util.LoadFromXml<ImeMode?>(item.Element(Util.ns.h + "ImeMode"));
            decimalAttributeMetadata.MaxValue = Util.LoadFromXml<decimal?>(item.Element(Util.ns.h + "MaxValue"));
            decimalAttributeMetadata.MinValue = Util.LoadFromXml<decimal?>(item.Element(Util.ns.h + "MinValue"));
            decimalAttributeMetadata.Precision = Util.LoadFromXml<int?>(item.Element(Util.ns.h + "Precision"));
            decimalAttributeMetadata.SourceTypeMask = Util.LoadFromXml<int?>(item.Element(Util.ns.h + "SourceTypeMask"));
            return decimalAttributeMetadata;
        }
    }
    public sealed class DoubleAttributeMetadata : AttributeMetadata
    {
        public const int MaxSupportedPrecision = 5;
        public const int MinSupportedPrecision = 0;
        public const double MaxSupportedValue = 100000000000;
        public const double MinSupportedValue = -100000000000;
        public ImeMode? ImeMode { get; set; }
        private double? _maxValue;
        public double? MaxValue
        {
            get { return _maxValue; }
            set
            {
                if (value < MinSupportedValue || value > MaxSupportedValue)
                    // Should throw error?
                    return;
                if (_minValue != null && value < _minValue)
                    _maxValue = value;
            }
        }
        private double? _minValue;
        public double? MinValue
        {
            get { return _minValue; }
            set
            {
                if (value < MinSupportedValue || value > MaxSupportedValue)
                    // Should throw error?
                    return;
                if (_maxValue != null && value > _maxValue)
                    return;
                _minValue = value;
            }
        }
        private int? _precision;
        public int? Precision
        {
            get { return _precision; }
            set
            {
                if (value < MinSupportedPrecision || value > MaxSupportedPrecision)
                    // Should throw error?
                    return;
                _precision = value;
            }
        }
        public DoubleAttributeMetadata() : this(null) { }
        public DoubleAttributeMetadata(string schemaName)
            : base(AttributeTypeCode.Double, schemaName) { }
        internal new string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToValueXml());
            sb.Append(Util.ObjectToXml(ImeMode, "h:ImeMode", true));
            sb.Append(Util.ObjectToXml(MaxValue, "h:MaxValue", true));
            sb.Append(Util.ObjectToXml(MinValue, "h:MinValue", true));
            sb.Append(Util.ObjectToXml(Precision, "h:Precision", true));
            return sb.ToString();
        }
        static internal new DoubleAttributeMetadata LoadFromXml(XElement item)
        {
            DoubleAttributeMetadata doubleAttributeMetadata = new DoubleAttributeMetadata();
            AttributeMetadata.LoadFromXml(item, doubleAttributeMetadata);
            doubleAttributeMetadata.ImeMode = Util.LoadFromXml<ImeMode?>(item.Element(Util.ns.h + "ImeMode"));
            doubleAttributeMetadata.MaxValue = Util.LoadFromXml<double?>(item.Element(Util.ns.h + "MaxValue"));
            doubleAttributeMetadata.MinValue = Util.LoadFromXml<double?>(item.Element(Util.ns.h + "MinValue"));
            doubleAttributeMetadata.Precision = Util.LoadFromXml<int?>(item.Element(Util.ns.h + "Precision"));
            return doubleAttributeMetadata;
        }
    }
    public sealed class EntityKeyMetadata : MetadataBase
    {
        public EntityReference AsyncJob { get; set; }
        public Label DisplayName { get; set; }
        public EntityKeyIndexStatus EntityKeyIndexStatus { get; set; }
        public string EntityLogicalName { get; set; }
        public string IntroducedVersion { get; set; }
        public BooleanManagedProperty IsCustomizable { get; set; }
        public bool? IsManaged { get; set; }
        public string[] KeyAttributes { get; set; }
        public string LogicalName { get; set; }
        public string SchemaName { get; set; }
        public EntityKeyMetadata()
        {
            KeyAttributes = new List<string>().ToArray();
        }
        internal new string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToValueXml());
            sb.Append(Util.ObjectToXml(DisplayName, "n:DisplayName", true));
            sb.Append(Util.ObjectToXml(EntityKeyIndexStatus, "n:EntityKeyIndexStatus", true));
            sb.Append(Util.ObjectToXml(EntityLogicalName, "n:EntityLogicalName", true));
            sb.Append(Util.ObjectToXml(IntroducedVersion, "n:IntroducedVersion", true));
            sb.Append(Util.ObjectToXml(IsCustomizable, "n:IsCustomizable", true));
            sb.Append(Util.ObjectToXml(IsManaged, "n:IsManaged", true));
            sb.Append(Util.ObjectToXml(KeyAttributes, "n:KeyAttributes", true));
            sb.Append(Util.ObjectToXml(LogicalName, "n:LogicalName", true));
            sb.Append(Util.ObjectToXml(SchemaName, "n:SchemaName", true));

            return sb.ToString();
        }
        static internal EntityKeyMetadata LoadFromXml(XElement item)
        {
            EntityKeyMetadata entityKeyMetadata = new EntityKeyMetadata()
            {
                AsyncJob = Util.LoadFromXml<EntityReference>(item.Element(Util.ns.n + "AsyncJob")),
                DisplayName = Util.LoadFromXml<Label>(item.Element(Util.ns.n + "DisplayName")),
                EntityKeyIndexStatus = Util.LoadFromXml<EntityKeyIndexStatus>(item.Element(Util.ns.n + "EntityKeyIndexStatus")),
                EntityLogicalName = Util.LoadFromXml<string>(item.Element(Util.ns.n + "EntityLogicalName")),
                IntroducedVersion = Util.LoadFromXml<string>(item.Element(Util.ns.n + "IntroducedVersion")),
                IsCustomizable = Util.LoadFromXml<BooleanManagedProperty>(item.Element(Util.ns.n + "IsCustomizable")),
                IsManaged = Util.LoadFromXml<bool?>(item.Element(Util.ns.n + "IsManaged")),
                KeyAttributes = Util.LoadFromXml<string[]>(item.Element(Util.ns.n + "KeyAttributes")),
                LogicalName = Util.LoadFromXml<string>(item.Element(Util.ns.n + "LogicalName")),
                SchemaName = Util.LoadFromXml<string>(item.Element(Util.ns.n + "SchemaName")),              
            };
            MetadataBase.LoadFromXml(item, entityKeyMetadata);
            return entityKeyMetadata;
        }
    }
    public sealed class EntityMetadata : MetadataBase
    {
        #region members
        public int? ActivityTypeMask { get; set; }
        public AttributeMetadata[] Attributes { get; set; }
        public bool? AutoCreateAccessTeams { get; set; }
        public bool? AutoRouteToOwnerQueue { get; set; }
        public BooleanManagedProperty CanBeInManyToMany { get; set; }
        public BooleanManagedProperty CanBePrimaryEntityInRelationship { get; set; }
        public BooleanManagedProperty CanBeRelatedEntityInRelationship { get; set; }
        public BooleanManagedProperty CanCreateAttributes { get; set; }
        public BooleanManagedProperty CanCreateCharts { get; set; }
        public BooleanManagedProperty CanCreateForms { get; set; }
        public BooleanManagedProperty CanCreateViews { get; set; }
        public BooleanManagedProperty CanModifyAdditionalSettings { get; set; }
        public bool? CanTriggerWorkflow { get; set; }
        public Label Description { get; set; }
        public Label DisplayCollectionName { get; set; }
        public Label DisplayName { get; set; }
        public string IconLargeName { get; set; }
        public string IconMediumName { get; set; }
        public string IconSmallName { get; set; }
        public string IntroducedVersion { get; set; }
        public bool? IsActivity { get; set; }
        public bool? IsActivityParty { get; set; }
        public bool? IsAIRUpdated { get; set; }
        public BooleanManagedProperty IsAuditEnabled { get; set; }
        public bool? IsAvailableOffline { get; set; }
        public bool? IsBusinessProcessEnabled { get; set; }
        public bool? IsChildEntity { get; set; }
        public BooleanManagedProperty IsConnectionsEnabled { get; set; }
        public bool? IsCustomEntity { get; set; }
        public BooleanManagedProperty IsCustomizable { get; set; }
        public bool? IsDocumentManagementEnabled { get; set; }
        public BooleanManagedProperty IsDuplicateDetectionEnabled { get; set; }
        public bool? IsEnabledForCharts { get; set; }
        public bool? IsEnabledForTrace { get; set; }
        public bool? IsImportable { get; set; }
        public bool? IsIntersect { get; set; }
        public BooleanManagedProperty IsMailMergeEnabled { get; set; }
        public bool? IsManaged { get; set; }
        public BooleanManagedProperty IsMappable { get; set; }
        public bool? IsQuickCreateEnabled { get; set; }
        public bool? IsReadingPaneEnabled { get; set; }
        public BooleanManagedProperty IsReadOnlyInMobileClient { get; set; }
        public BooleanManagedProperty IsRenameable { get; set; }
        public bool? IsValidForAdvancedFind { get; set; }
        public BooleanManagedProperty IsValidForQueue { get; set; }
        public BooleanManagedProperty IsVisibleInMobile { get; set; }
        public BooleanManagedProperty IsVisibleInMobileClient { get; set; }
        public string LogicalName { get; set; }
        public ManyToManyRelationshipMetadata[] ManyToManyRelationships { get; set; }
        public OneToManyRelationshipMetadata[] ManyToOneRelationships { get; set; }
        public int? ObjectTypeCode { get; set; }
        public OneToManyRelationshipMetadata[] OneToManyRelationships { get; set; }
        public OwnershipTypes? OwnershipType { get; set; }
        public string PrimaryIdAttribute { get; set; }
        public string PrimaryImageAttribute { get; set; }
        public string PrimaryNameAttribute { get; set; }
        public SecurityPrivilegeMetadata[] Privileges { get; set; }
        public string RecurrenceBaseEntityLogicalName { get; set; }
        public string ReportViewName { get; set; }
        public string SchemaName { get; set; }
        #endregion members
        public EntityMetadata()
        {
            Attributes = new List<AttributeMetadata>().ToArray();
            ManyToManyRelationships = new List<ManyToManyRelationshipMetadata>().ToArray();
            ManyToOneRelationships = new List<OneToManyRelationshipMetadata>().ToArray();
            OneToManyRelationships = new List<OneToManyRelationshipMetadata>().ToArray();
            Privileges = new List<SecurityPrivilegeMetadata>().ToArray();
        }
        internal new string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToValueXml());
            sb.Append(Util.ObjectToXml(ActivityTypeMask, "h:ActivityTypeMask", true));
            if (this.Attributes == null)
                sb.Append("<h:Attributes i:nil='true'/>");
            else
            {
                sb.Append("<h:Attributes>");
                foreach (AttributeMetadata Attribute in Attributes)
                {
                    sb.Append(Attribute.ToXml(Attribute, "h:AttributeMetadata"));
                }
                sb.Append("</h:Attributes>");
            }
            sb.Append(Util.ObjectToXml(AutoCreateAccessTeams, "h:AutoCreateAccessTeams", true));
            sb.Append(Util.ObjectToXml(AutoRouteToOwnerQueue, "h:AutoRouteToOwnerQueue", true));
            sb.Append(Util.ObjectToXml(CanBeInManyToMany, "h:CanBeInManyToMany", true));
            sb.Append(Util.ObjectToXml(CanBePrimaryEntityInRelationship, "h:CanBePrimaryEntityInRelationship", true));
            sb.Append(Util.ObjectToXml(CanBeRelatedEntityInRelationship, "h:CanBeRelatedEntityInRelationship", true));
            sb.Append(Util.ObjectToXml(CanCreateAttributes, "h:CanCreateAttributes", true));
            sb.Append(Util.ObjectToXml(CanCreateCharts, "h:CanCreateCharts", true));
            sb.Append(Util.ObjectToXml(CanCreateForms, "h:CanCreateForms", true));
            sb.Append(Util.ObjectToXml(CanCreateViews, "h:CanCreateViews", true));
            sb.Append(Util.ObjectToXml(CanModifyAdditionalSettings, "h:CanModifyAdditionalSettings", true));
            sb.Append(Util.ObjectToXml(CanTriggerWorkflow, "h:CanTriggerWorkflow", true));
            sb.Append(Util.ObjectToXml(Description, "h:Description", true));
            sb.Append(Util.ObjectToXml(DisplayCollectionName, "h:DisplayCollectionName", true));
            sb.Append(Util.ObjectToXml(DisplayName, "h:DisplayName", true));
            sb.Append(Util.ObjectToXml(IconLargeName, "h:IconLargeName", true));
            sb.Append(Util.ObjectToXml(IconMediumName, "h:IconMediumName", true));
            sb.Append(Util.ObjectToXml(IconSmallName, "h:IconSmallName", true));
            sb.Append(Util.ObjectToXml(IsAIRUpdated, "h:IsAIRUpdated", true));
            sb.Append(Util.ObjectToXml(IsActivity, "h:IsActivity", true));
            sb.Append(Util.ObjectToXml(IsActivityParty, "h:IsActivityParty", true));
            sb.Append(Util.ObjectToXml(IsAuditEnabled, "h:IsAuditEnabled", true));
            sb.Append(Util.ObjectToXml(IsAvailableOffline, "h:IsAvailableOffline", true));
            sb.Append(Util.ObjectToXml(IsBusinessProcessEnabled, "h:IsBusinessProcessEnabled", true));
            sb.Append(Util.ObjectToXml(IsChildEntity, "h:IsChildEntity", true));
            sb.Append(Util.ObjectToXml(IsConnectionsEnabled, "h:IsConnectionsEnabled", true));
            sb.Append(Util.ObjectToXml(IsCustomEntity, "h:IsCustomEntity", true));
            sb.Append(Util.ObjectToXml(IsCustomizable, "h:IsCustomizable", true));
            sb.Append(Util.ObjectToXml(IsDocumentManagementEnabled, "h:IsDocumentManagementEnabled", true));
            sb.Append(Util.ObjectToXml(IsDuplicateDetectionEnabled, "h:IsDuplicateDetectionEnabled", true));
            sb.Append(Util.ObjectToXml(IsEnabledForCharts, "h:IsEnabledForCharts", true));
            sb.Append(Util.ObjectToXml(IsEnabledForTrace, "h:IsEnabledForTrace", true));
            sb.Append(Util.ObjectToXml(IsImportable, "h:IsImportable", true));
            sb.Append(Util.ObjectToXml(IsIntersect, "h:IsIntersect", true));
            sb.Append(Util.ObjectToXml(IsMailMergeEnabled, "h:IsMailMergeEnabled", true));
            sb.Append(Util.ObjectToXml(IsManaged, "h:IsManaged", true));
            sb.Append(Util.ObjectToXml(IsMappable, "h:IsMappable", true));
            sb.Append(Util.ObjectToXml(IsQuickCreateEnabled, "h:IsQuickCreateEnabled", true));
            sb.Append(Util.ObjectToXml(IsReadOnlyInMobileClient, "h:IsReadOnlyInMobileClient", true));
            sb.Append(Util.ObjectToXml(IsReadingPaneEnabled, "h:IsReadingPaneEnabled", true));
            sb.Append(Util.ObjectToXml(IsRenameable, "h:IsRenameable", true));
            sb.Append(Util.ObjectToXml(IsValidForAdvancedFind, "h:IsValidForAdvancedFind", true));
            sb.Append(Util.ObjectToXml(IsValidForQueue, "h:IsValidForQueue", true));
            sb.Append(Util.ObjectToXml(IsVisibleInMobile, "h:IsVisibleInMobile", true));
            sb.Append(Util.ObjectToXml(IsVisibleInMobileClient, "h:IsVisibleInMobileClient", true));
            sb.Append(Util.ObjectToXml(LogicalName, "h:LogicalName", true));
            sb.Append(Util.ObjectToXml(ManyToManyRelationships, "h:ManyToManyRelationships", true));
            sb.Append(Util.ObjectToXml(ManyToManyRelationships, "h:ManyToOneRelationships", true));
            sb.Append(Util.ObjectToXml(ObjectTypeCode, "h:ObjectTypeCode", true));
            sb.Append(Util.ObjectToXml(ManyToManyRelationships, "h:OneToManyRelationships", true));
            sb.Append(Util.ObjectToXml(OwnershipType, "h:OwnershipType", true));
            sb.Append(Util.ObjectToXml(PrimaryIdAttribute, "h:PrimaryIdAttribute", true));
            sb.Append(Util.ObjectToXml(PrimaryNameAttribute, "h:PrimaryNameAttribute", true));
            sb.Append(Util.ObjectToXml(Privileges, "h:Privileges", true));
            sb.Append(Util.ObjectToXml(RecurrenceBaseEntityLogicalName, "h:RecurrenceBaseEntityLogicalName", true));
            sb.Append(Util.ObjectToXml(ReportViewName, "h:ReportViewName", true));
            sb.Append(Util.ObjectToXml(SchemaName, "h:SchemaName", true));
            sb.Append(Util.ObjectToXml(PrimaryImageAttribute, "h:PrimaryImageAttribute", true));
            sb.Append(Util.ObjectToXml(IntroducedVersion, "h:IntroducedVersion", true));
            return sb.ToString();
        }
        static internal EntityMetadata LoadFromXml(XElement item)
        {
            List<AttributeMetadata> attributeMetadataList = new List<AttributeMetadata>();
            foreach (var attributeMetadatas in item.Elements(Util.ns.h + "Attributes"))
            {
                foreach (var attributeMetadata in attributeMetadatas.Elements(Util.ns.h + "AttributeMetadata"))
                {
                    attributeMetadataList.Add(AttributeMetadata.LoadFromXml(attributeMetadata));
                }
            }
            List<ManyToManyRelationshipMetadata> manyToManyRelationshipMetadataList = new List<ManyToManyRelationshipMetadata>();
            foreach (var manyToManyRelationships in item.Elements(Util.ns.h + "ManyToManyRelationships"))
            {
                foreach (var manyToManyRelationship in manyToManyRelationships.Elements(Util.ns.h + "ManyToManyRelationshipMetadata"))
                {
                    manyToManyRelationshipMetadataList.Add(ManyToManyRelationshipMetadata.LoadFromXml(manyToManyRelationship));
                }
            }
            List<OneToManyRelationshipMetadata> manyToOneRelationshipMetadataList = new List<OneToManyRelationshipMetadata>();
            foreach (var manyToOneRelationships in item.Elements(Util.ns.h + "ManyToOneRelationships"))
            {
                foreach (var oneToManyRelationshipMetadata in manyToOneRelationships.Elements(Util.ns.h + "OneToManyRelationshipMetadata"))
                {
                    manyToOneRelationshipMetadataList.Add(OneToManyRelationshipMetadata.LoadFromXml(oneToManyRelationshipMetadata));
                }
            }
            List<OneToManyRelationshipMetadata> oneToManyRelationshipMetadataList = new List<OneToManyRelationshipMetadata>();
            foreach (var manyToOneRelationships in item.Elements(Util.ns.h + "OneToManyRelationships"))
            {
                foreach (var oneToManyRelationshipMetadata in manyToOneRelationships.Elements(Util.ns.h + "OneToManyRelationshipMetadata"))
                {
                    oneToManyRelationshipMetadataList.Add(OneToManyRelationshipMetadata.LoadFromXml(oneToManyRelationshipMetadata));
                }
            }
            List<SecurityPrivilegeMetadata> securityPrivilegeMetadataList = new List<SecurityPrivilegeMetadata>();
            foreach (var securityPrivilegeMetadatas in item.Elements(Util.ns.h + "Privileges"))
            {
                foreach (var securityPrivilegeMetadata in securityPrivilegeMetadatas.Elements(Util.ns.h + "SecurityPrivilegeMetadata"))
                {
                    securityPrivilegeMetadataList.Add(SecurityPrivilegeMetadata.LoadFromXml(securityPrivilegeMetadata));
                }
            }
            EntityMetadata entityMetadata = new EntityMetadata()
            {
                Attributes = attributeMetadataList.ToArray(),
                ActivityTypeMask = Util.LoadFromXml<int?>(item.Element(Util.ns.h + "ActivityTypeMask")),
                AutoCreateAccessTeams = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "AutoCreateAccessTeams")),
                AutoRouteToOwnerQueue = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "AutoRouteToOwnerQueue")),
                CanBeInManyToMany = BooleanManagedProperty.LoadFromXml(item.Element(Util.ns.h + "CanBeInManyToMany")),
                CanBePrimaryEntityInRelationship = BooleanManagedProperty.LoadFromXml(item.Element(Util.ns.h + "CanBePrimaryEntityInRelationship")),
                CanBeRelatedEntityInRelationship = BooleanManagedProperty.LoadFromXml(item.Element(Util.ns.h + "CanBeRelatedEntityInRelationship")),
                CanCreateAttributes = BooleanManagedProperty.LoadFromXml(item.Element(Util.ns.h + "CanCreateAttributes")),
                CanCreateCharts = BooleanManagedProperty.LoadFromXml(item.Element(Util.ns.h + "CanCreateCharts")),
                CanCreateForms = BooleanManagedProperty.LoadFromXml(item.Element(Util.ns.h + "CanCreateForms")),
                CanCreateViews = BooleanManagedProperty.LoadFromXml(item.Element(Util.ns.h + "CanCreateViews")),
                CanModifyAdditionalSettings = BooleanManagedProperty.LoadFromXml(item.Element(Util.ns.h + "CanModifyAdditionalSettings")),
                CanTriggerWorkflow = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "CanTriggerWorkflow")),
                Description = Label.LoadFromXml(item.Element(Util.ns.h + "Description")),
                DisplayCollectionName = Label.LoadFromXml(item.Element(Util.ns.h + "DisplayCollectionName")),
                DisplayName = Label.LoadFromXml(item.Element(Util.ns.h + "DisplayName")),
                IconLargeName = Util.LoadFromXml<string>(item.Element(Util.ns.h + "IconLargeName")),
                IconMediumName = Util.LoadFromXml<string>(item.Element(Util.ns.h + "IconMediumName")),
                IconSmallName = Util.LoadFromXml<string>(item.Element(Util.ns.h + "IconSmallName")),
                IntroducedVersion = Util.LoadFromXml<string>(item.Element(Util.ns.h + "IntroducedVersion")),
                IsActivity = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "IsActivity")),
                IsActivityParty = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "IsActivityParty")),
                IsAIRUpdated = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "IsAIRUpdated")),
                IsAuditEnabled = BooleanManagedProperty.LoadFromXml(item.Element(Util.ns.h + "IsAuditEnabled")),
                IsAvailableOffline = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "IsAvailableOffline")),
                IsBusinessProcessEnabled = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "IsBusinessProcessEnabled")),
                IsChildEntity = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "IsChildEntity")),
                IsConnectionsEnabled = BooleanManagedProperty.LoadFromXml(item.Element(Util.ns.h + "IsConnectionsEnabled")),
                IsCustomEntity = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "IsCustomEntity")),
                IsCustomizable = BooleanManagedProperty.LoadFromXml(item.Element(Util.ns.h + "IsCustomizable")),
                IsDocumentManagementEnabled = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "IsDocumentManagementEnabled")),
                IsDuplicateDetectionEnabled = BooleanManagedProperty.LoadFromXml(item.Element(Util.ns.h + "IsDuplicateDetectionEnabled")),
                IsEnabledForCharts = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "IsEnabledForCharts")),
                IsEnabledForTrace = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "IsEnabledForTrace")),
                IsImportable = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "IsImportable")),
                IsIntersect = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "IsIntersect")),
                IsMailMergeEnabled = BooleanManagedProperty.LoadFromXml(item.Element(Util.ns.h + "IsMailMergeEnabled")),
                IsManaged = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "IsManaged")),
                IsMappable = BooleanManagedProperty.LoadFromXml(item.Element(Util.ns.h + "IsMappable")),
                IsQuickCreateEnabled = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "IsQuickCreateEnabled")),
                IsReadingPaneEnabled = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "IsReadingPaneEnabled")),
                IsReadOnlyInMobileClient = BooleanManagedProperty.LoadFromXml(item.Element(Util.ns.h + "IsReadOnlyInMobileClient")),
                IsRenameable = BooleanManagedProperty.LoadFromXml(item.Element(Util.ns.h + "IsRenameable")),
                IsValidForAdvancedFind = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "IsValidForAdvancedFind")),
                IsValidForQueue = BooleanManagedProperty.LoadFromXml(item.Element(Util.ns.h + "IsValidForQueue")),
                IsVisibleInMobile = BooleanManagedProperty.LoadFromXml(item.Element(Util.ns.h + "IsVisibleInMobile")),
                IsVisibleInMobileClient = BooleanManagedProperty.LoadFromXml(item.Element(Util.ns.h + "IsVisibleInMobileClient")),
                LogicalName = Util.LoadFromXml<string>(item.Element(Util.ns.h + "LogicalName")),
                ManyToManyRelationships = manyToManyRelationshipMetadataList.ToArray(),
                ManyToOneRelationships = manyToOneRelationshipMetadataList.ToArray(),
                ObjectTypeCode = Util.LoadFromXml<int?>(item.Element(Util.ns.h + "ObjectTypeCode")),
                OneToManyRelationships = oneToManyRelationshipMetadataList.ToArray(),
                OwnershipType = Util.LoadFromXml<OwnershipTypes?>(item.Element(Util.ns.h + "OwnershipType")),
                PrimaryIdAttribute = Util.LoadFromXml<string>(item.Element(Util.ns.h + "PrimaryIdAttribute")),
                PrimaryImageAttribute = Util.LoadFromXml<string>(item.Element(Util.ns.h + "PrimaryImageAttribute")),
                PrimaryNameAttribute = Util.LoadFromXml<string>(item.Element(Util.ns.h + "PrimaryNameAttribute")),
                Privileges = securityPrivilegeMetadataList.ToArray(),
                RecurrenceBaseEntityLogicalName = Util.LoadFromXml<string>(item.Element(Util.ns.h + "RecurrenceBaseEntityLogicalName")),
                ReportViewName = Util.LoadFromXml<string>(item.Element(Util.ns.h + "ReportViewName")),
                SchemaName = Util.LoadFromXml<string>(item.Element(Util.ns.h + "SchemaName")),
            };
            MetadataBase.LoadFromXml(item, entityMetadata);
            return entityMetadata;
        }
    }
    public sealed class EntityMetadataCollection : DataCollection<EntityMetadata>
    {
        static internal EntityMetadataCollection LoadFromXml(XElement item)
        {
            EntityMetadataCollection entityMetadataCollection = new EntityMetadataCollection();
            foreach (var entity in item.Elements(Util.ns.a + "EntityMetadata"))
            {
                entityMetadataCollection.Add(EntityMetadata.LoadFromXml(entity));
            }
            return entityMetadataCollection;
        }
    }
    public sealed class EntityNameAttributeMetadata : EnumAttributeMetadata
    {
        public EntityNameAttributeMetadata() : this(null) { }
        public EntityNameAttributeMetadata(string schemaName)
            : base(AttributeTypeCode.EntityName, schemaName) { }
        internal new string ToValueXml()
        {
            return base.ToValueXml();
        }
        static internal new EntityNameAttributeMetadata LoadFromXml(XElement item)
        {
            EntityNameAttributeMetadata entityNameAttributeMetadata = new EntityNameAttributeMetadata();
            EnumAttributeMetadata.LoadFromXml(item, entityNameAttributeMetadata);
            return entityNameAttributeMetadata;
        }
    }
    public abstract class EnumAttributeMetadata : AttributeMetadata
    {
        public int? DefaultFormValue { get; set; }
        public OptionSetMetadata OptionSet { get; set; }
        public EnumAttributeMetadata() { }
        public EnumAttributeMetadata(AttributeTypeCode attributeType, string schemaName)
            : base(attributeType, schemaName) { }
        internal new string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToValueXml());
            sb.Append(Util.ObjectToXml(DefaultFormValue, "h:DefaultFormValue", true));
            sb.Append(Util.ObjectToXml(OptionSet, "h:OptionSet", true));
            return sb.ToString();
        }
        static internal void LoadFromXml(XElement item, EnumAttributeMetadata meta)
        {
            if (item.Elements().Count() == 0)
                return;
            AttributeMetadata.LoadFromXml(item, meta);
            meta.DefaultFormValue = Util.LoadFromXml<int?>(item.Element(Util.ns.h + "DefaultFormValue"));
            if (item.Element(Util.ns.h + "OptionSet").Elements().Count() > 0)
                meta.OptionSet = OptionSetMetadata.LoadFromXml(item.Element(Util.ns.h + "OptionSet"));
        }
    }
    public sealed class ImageAttributeMetadata : AttributeMetadata
    {
        public bool? IsPrimaryImage { get; set; }
        public short? MaxHeight { get; set; }
        public short? MaxWidth { get; set; }
        public ImageAttributeMetadata() : this(null) { }
        public ImageAttributeMetadata(string schemaName)
        {
            AttributeType = AttributeTypeCode.Virtual;
            AttributeTypeName = AttributeTypeDisplayName.ImageType;
            SchemaName = schemaName;
        }
        internal new string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToValueXml());
            sb.Append(Util.ObjectToXml(IsPrimaryImage, "k:IsPrimaryImage", true));
            sb.Append(Util.ObjectToXml(MaxHeight, "k:MaxHeight", true));
            sb.Append(Util.ObjectToXml(MaxWidth, "k:MaxWidth", true));
            return sb.ToString();
        }
        static internal new ImageAttributeMetadata LoadFromXml(XElement item)
        {
            ImageAttributeMetadata imageAttributeMetadata = new ImageAttributeMetadata();
            AttributeMetadata.LoadFromXml(item, imageAttributeMetadata);
            imageAttributeMetadata.IsPrimaryImage = Util.LoadFromXml<bool?>(item.Element(Util.ns.k + "IsPrimaryImage"));
            imageAttributeMetadata.MaxHeight = Util.LoadFromXml<short?>(item.Element(Util.ns.k + "MaxHeight"));
            imageAttributeMetadata.MaxWidth = Util.LoadFromXml<short?>(item.Element(Util.ns.k + "MaxWidth"));
            return imageAttributeMetadata;
        }
    }
    public sealed class IntegerAttributeMetadata : AttributeMetadata
    {
        public const int MaxSupportedValue = 2147483647;
        public const int MinSupportedValue = -2147483648;
        public IntegerFormat? Format { get; set; }
        public string FormulaDefinition { get; set; }
        private int? _maxValue;
        public int? MaxValue
        {
            get { return _maxValue; }
            set
            {
                if (value < MinSupportedValue || value > MaxSupportedValue)
                    // Should throw error?
                    return;
                if (_minValue != null && value < _minValue)
                    return;
                _maxValue = value;
            }
        }
        private int? _minValue;
        public int? MinValue
        {
            get { return _minValue; }
            set
            {
                if (value < MinSupportedValue || value > MaxSupportedValue)
                    // Should throw error?
                    return;
                if (_maxValue != null && value > _maxValue)
                    return;
                _minValue = value;
            }
        }
        public int? SourceTypeMask { get; set; }
        public IntegerAttributeMetadata() : this(null) { }
        public IntegerAttributeMetadata(string schemaName)
            : base(AttributeTypeCode.Integer, schemaName) { }
        internal new string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToValueXml());
            sb.Append(Util.ObjectToXml(Format, "h:Format", true));
            sb.Append(Util.ObjectToXml(FormulaDefinition, "h:FormulaDefinition", true));
            sb.Append(Util.ObjectToXml(MaxValue, "h:MaxValue", true));
            sb.Append(Util.ObjectToXml(MinValue, "h:MinValue", true));
            sb.Append(Util.ObjectToXml(SourceTypeMask, "h:SourceTypeMask", true));
            return sb.ToString();
        }
        static internal new IntegerAttributeMetadata LoadFromXml(XElement item)
        {
            IntegerAttributeMetadata integerAttributeMetadata = new IntegerAttributeMetadata();
            AttributeMetadata.LoadFromXml(item, integerAttributeMetadata);
            integerAttributeMetadata.Format = Util.LoadFromXml<IntegerFormat?>(item.Element(Util.ns.h + "Format"));
            integerAttributeMetadata.FormulaDefinition = Util.LoadFromXml<string>(item.Element(Util.ns.h + "FormulaDefinition"));
            integerAttributeMetadata.MaxValue = Util.LoadFromXml<int?>(item.Element(Util.ns.h + "MaxValue"));
            integerAttributeMetadata.MinValue = Util.LoadFromXml<int?>(item.Element(Util.ns.h + "MinValue"));
            integerAttributeMetadata.SourceTypeMask = Util.LoadFromXml<int?>(item.Element(Util.ns.h + "SourceTypeMask"));
            return integerAttributeMetadata;
        }
    }
    public sealed class LookupAttributeMetadata : AttributeMetadata
    {
        public string[] Targets { get; set; }
        public LookupAttributeMetadata()
            : base(AttributeTypeCode.Lookup)
        {
            Targets = new List<string>().ToArray();
        }
        internal new string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToValueXml());
            sb.Append(Util.ObjectToXml(Targets, "d:Targets", true));
            return sb.ToString();
        }
        static internal new LookupAttributeMetadata LoadFromXml(XElement item)
        {
            LookupAttributeMetadata lookupAttributeMetadata = new LookupAttributeMetadata();
            AttributeMetadata.LoadFromXml(item, lookupAttributeMetadata);
            List<string> list = new List<string>();
            if (item.Elements(Util.ns.h + "Targets").Elements().Count() > 0)
            {
                foreach (XElement Target in item.Elements(Util.ns.h + "Targets").Elements())
                {
                    list.Add(Target.Value);
                }
            }
            lookupAttributeMetadata.Targets = list.ToArray();
            return lookupAttributeMetadata;
        }
    }
    public sealed class ManagedPropertyAttributeMetadata : AttributeMetadata
    {
        public string ManagedPropertyLogicalName { get; set; }
        public string ParentAttributeName { get; set; }
        public int? ParentComponentType { get; set; }
        public AttributeTypeCode ValueAttributeTypeCode { get; set; }
        public ManagedPropertyAttributeMetadata() : this(null) { }
        public ManagedPropertyAttributeMetadata(string schemaName)
            : base(AttributeTypeCode.ManagedProperty, schemaName) { }
        internal new string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToValueXml());
            sb.Append(Util.ObjectToXml(ManagedPropertyLogicalName, "h:ManagedPropertyLogicalName", true));
            sb.Append(Util.ObjectToXml(ParentAttributeName, "h:ParentAttributeName", true));
            sb.Append(Util.ObjectToXml(ParentComponentType, "h:ParentComponentType", true));
            sb.Append(Util.ObjectToXml(ValueAttributeTypeCode, "h:ValueAttributeTypeCode", true));
            return sb.ToString();
        }
        static internal new ManagedPropertyAttributeMetadata LoadFromXml(XElement item)
        {
            ManagedPropertyAttributeMetadata managedPropertyAttributeMetadata = new ManagedPropertyAttributeMetadata();
            AttributeMetadata.LoadFromXml(item, managedPropertyAttributeMetadata);
            managedPropertyAttributeMetadata.ManagedPropertyLogicalName = Util.LoadFromXml<string>(item.Element(Util.ns.h + "ManagedPropertyLogicalName"));
            managedPropertyAttributeMetadata.ParentAttributeName = Util.LoadFromXml<string>(item.Element(Util.ns.h + "ParentAttributeName"));
            managedPropertyAttributeMetadata.ParentComponentType = Util.LoadFromXml<int?>(item.Element(Util.ns.h + "ParentComponentType"));
            managedPropertyAttributeMetadata.ValueAttributeTypeCode =
                Util.LoadFromXml<AttributeTypeCode>(item.Element(Util.ns.h + "ValueAttributeTypeCode"));
            return managedPropertyAttributeMetadata;
        }
    }
    public sealed class ManagedPropertyMetadata : MetadataBase
    {
        public string LogicalName { get; set; }
        public Label DisplayName { get; set; }
        public Label Description { get; set; }
        public ManagedPropertyType? ManagedPropertyType { get; set; }
        public ManagedPropertyOperation? Operation { get; set; }
        public ManagedPropertyEvaluationPriority? EvaluationPriority { get; set; }
        public string EnablesAttributeName { get; set; }
        public string EnablesEntityName { get; set; }
        public int? ErrorCode { get; set; }
        public bool? IsPrivate { get; set; }
        public bool? IsGlobalForOperation { get; set; }
        public string IntroducedVersion { get; set; }
        static internal ManagedPropertyMetadata LoadFromXml(XElement item)
        {
            ManagedPropertyMetadata managedPropertyMetadata = new ManagedPropertyMetadata()
            {
                LogicalName = Util.LoadFromXml<string>(item.Element(Util.ns.h + "LogicalName")),
                DisplayName = Label.LoadFromXml(item.Element(Util.ns.h + "DisplayName")),
                Description = Label.LoadFromXml(item.Element(Util.ns.h + "Description")),
                ManagedPropertyType = Util.LoadFromXml<ManagedPropertyType>(item.Element(Util.ns.h + "ManagedPropertyType")),
                Operation = Util.LoadFromXml<ManagedPropertyOperation>(item.Element(Util.ns.h + "Operation")),
                EvaluationPriority = Util.LoadFromXml<ManagedPropertyEvaluationPriority>(item.Element(Util.ns.h + "EvaluationPriority")),
                EnablesAttributeName = Util.LoadFromXml<string>(item.Element(Util.ns.h + "EnablesAttributeName")),
                EnablesEntityName = Util.LoadFromXml<string>(item.Element(Util.ns.h + "EnablesEntityName")),
                ErrorCode = Util.LoadFromXml<int?>(item.Element(Util.ns.h + "ErrorCode")),
                IsPrivate = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "IsPrivate")),
                IsGlobalForOperation = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "IsGlobalForOperation")),
                IntroducedVersion = Util.LoadFromXml<string>(item.Element(Util.ns.h + "IntroducedVersion")),
            };
            MetadataBase.LoadFromXml(item, managedPropertyMetadata);
            return managedPropertyMetadata;
        }
    }
    public sealed class ManyToManyRelationshipMetadata : RelationshipMetadataBase
    {
        public AssociatedMenuConfiguration Entity1AssociatedMenuConfiguration { get; set; }
        public string Entity1IntersectAttribute { get; set; }
        public string Entity1LogicalName { get; set; }
        public AssociatedMenuConfiguration Entity2AssociatedMenuConfiguration { get; set; }
        public string Entity2IntersectAttribute { get; set; }
        public string Entity2LogicalName { get; set; }
        public string IntersectEntityName { get; set; }
        internal new string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToValueXml());
            sb.Append(Util.ObjectToXml(Entity1AssociatedMenuConfiguration, "h:Entity1AssociatedMenuConfiguration", true));
            sb.Append(Util.ObjectToXml(Entity1IntersectAttribute, "h:Entity1IntersectAttribute", true));
            sb.Append(Util.ObjectToXml(Entity1LogicalName, "h:Entity1LogicalName", true));
            sb.Append(Util.ObjectToXml(Entity2AssociatedMenuConfiguration, "h:Entity2AssociatedMenuConfiguration", true));
            sb.Append(Util.ObjectToXml(Entity2IntersectAttribute, "h:Entity2IntersectAttribute", true));
            sb.Append(Util.ObjectToXml(Entity2LogicalName, "h:Entity2LogicalName", true));
            sb.Append(Util.ObjectToXml(IntersectEntityName, "h:IntersectEntityName", true));
            return sb.ToString();
        }
        static internal new ManyToManyRelationshipMetadata LoadFromXml(XElement item)
        {
            ManyToManyRelationshipMetadata manyToManyRelationshipMetadata = new ManyToManyRelationshipMetadata()
            {
                Entity1AssociatedMenuConfiguration = AssociatedMenuConfiguration.LoadFromXml(item.Element(Util.ns.h + "Entity1AssociatedMenuConfiguration")),
                Entity1IntersectAttribute = Util.LoadFromXml<string>(item.Element(Util.ns.h + "Entity1IntersectAttribute")),
                Entity1LogicalName = Util.LoadFromXml<string>(item.Element(Util.ns.h + "Entity1LogicalName")),
                Entity2AssociatedMenuConfiguration = AssociatedMenuConfiguration.LoadFromXml(item.Element(Util.ns.h + "Entity2AssociatedMenuConfiguration")),
                Entity2IntersectAttribute = Util.LoadFromXml<string>(item.Element(Util.ns.h + "Entity2IntersectAttribute")),
                Entity2LogicalName = Util.LoadFromXml<string>(item.Element(Util.ns.h + "Entity2LogicalName")),
                IntersectEntityName = Util.LoadFromXml<string>(item.Element(Util.ns.h + "IntersectEntityName")),
            };
            RelationshipMetadataBase.LoadFromXml(item, manyToManyRelationshipMetadata);
            return manyToManyRelationshipMetadata;
        }
    }
    public sealed class MemoAttributeMetadata : AttributeMetadata
    {
        public const int MaxSupportedLength = 1048576;
        public const int MinSupportedLength = 1;
        public StringFormat? Format { get; set; }
        public ImeMode? ImeMode { get; set; }
        private int? _maxLength;
        public int? MaxLength
        {
            get { return _maxLength; }
            set
            {
                if (value < MinSupportedLength || value > MaxSupportedLength)
                    // Should throw error?
                    return;
                _maxLength = value;
            }
        }
        public MemoAttributeMetadata() : this(null) { }
        public MemoAttributeMetadata(string schemaName)
            : base(AttributeTypeCode.Memo, schemaName) { }
        internal new string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToValueXml());
            sb.Append(Util.ObjectToXml(Format, "h:Format", true));
            sb.Append(Util.ObjectToXml(ImeMode, "h:ImeMode", true));
            sb.Append(Util.ObjectToXml(MaxLength, "h:MaxLength", true));
            return sb.ToString();
        }
        static internal new MemoAttributeMetadata LoadFromXml(XElement item)
        {
            MemoAttributeMetadata memoAttributeMetadata = new MemoAttributeMetadata();
            AttributeMetadata.LoadFromXml(item, memoAttributeMetadata);
            memoAttributeMetadata.Format = Util.LoadFromXml<StringFormat?>(item.Element(Util.ns.h + "Format"));
            memoAttributeMetadata.ImeMode = Util.LoadFromXml<ImeMode?>(item.Element(Util.ns.h + "ImeMode"));
            memoAttributeMetadata.MaxLength = Util.LoadFromXml<int?>(item.Element(Util.ns.h + "MaxLength"));
            return memoAttributeMetadata;
        }
    }
    public abstract class MetadataBase
    {
        public bool? HasChanged { get; set; }
        public Guid? MetadataId { get; set; }
        // As there is no suitable place for this, I put inside EntityMetadata
        static internal string GetDeletedMetadataFiltersAsString(DeletedMetadataFilters enumValue)
        {
            List<string> valueArray = new List<string>();
            string returnValue = "None";

            if (enumValue.HasFlag(DeletedMetadataFilters.Entity) || enumValue.HasFlag(DeletedMetadataFilters.All))
            {
                valueArray.Add("Entity");
            }
            if (enumValue.HasFlag(DeletedMetadataFilters.Attribute) || enumValue.HasFlag(DeletedMetadataFilters.All))
            {
                valueArray.Add("Attribute");
            }
            if (enumValue.HasFlag(DeletedMetadataFilters.Relationship) || enumValue.HasFlag(DeletedMetadataFilters.All))
            {
                valueArray.Add("Relationship");
            }
            if (enumValue.HasFlag(DeletedMetadataFilters.Label) || enumValue.HasFlag(DeletedMetadataFilters.All))
            {
                valueArray.Add("Label");
            }
            if (enumValue.HasFlag(DeletedMetadataFilters.OptionSet) || enumValue.HasFlag(DeletedMetadataFilters.All))
            {
                valueArray.Add("OptionSet");
            }
            returnValue = String.Join(" ", valueArray.ToArray());

            return returnValue;
        }
        internal string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Util.ObjectToXml(HasChanged, "h:HasChanged", true));
            sb.Append(Util.ObjectToXml(MetadataId, "h:MetadataId", true));
            return sb.ToString();
        }
        static internal void LoadFromXml(XElement item, MetadataBase meta)
        {
            if (item.Elements().Count() == 0)
                return;
            meta.HasChanged = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "HasChanged"));
            meta.MetadataId = Util.LoadFromXml<Guid?>(item.Element(Util.ns.h + "MetadataId"));
        }
    }
    public sealed class MoneyAttributeMetadata : AttributeMetadata
    {
        public const int MaxSupportedPrecision = 4;
        public const int MinSupportedPrecision = 0;
        public const double MaxSupportedValue = 922337000000000;
        public const double MinSupportedValue = -922337000000000;
        public string CalculationOf { get; set; }
        public string FormulaDefinition { get; set; }
        public ImeMode? ImeMode { get; set; }
        public bool? IsBaseCurrency { get; set; }
        private double? _maxValue;
        public double? MaxValue
        {
            get { return _maxValue; }
            set
            {
                if (value < MinSupportedValue || value > MaxSupportedValue)
                    // Should throw error?
                    return;
                if (_minValue != null && value < _minValue)
                    _maxValue = value;
            }
        }
        private double? _minValue;
        public double? MinValue
        {
            get { return _minValue; }
            set
            {
                if (value < MinSupportedValue || value > MaxSupportedValue)
                    // Should throw error?
                    return;
                if (_maxValue != null && value > _maxValue)
                    return;
                _minValue = value;
            }
        }
        private int? _precision;
        public int? Precision
        {
            get { return _precision; }
            set
            {
                if (value < MinSupportedPrecision || value > MaxSupportedPrecision)
                    // Should throw error?
                    return;
                _precision = value;
            }
        }
        public int? PrecisionSource { get; set; }
        public int? SourceTypeMask { get; set; }

        public MoneyAttributeMetadata() : this(null) { }
        public MoneyAttributeMetadata(string schemaName)
            : base(AttributeTypeCode.Money, schemaName) { }
        internal new string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToValueXml());
            sb.Append(Util.ObjectToXml(CalculationOf, "h:CalculationOf", true));
            sb.Append(Util.ObjectToXml(FormulaDefinition, "h:FormulaDefinition", true));
            sb.Append(Util.ObjectToXml(ImeMode, "h:ImeMode", true));
            sb.Append(Util.ObjectToXml(IsBaseCurrency, "h:IsBaseCurrency", true));
            sb.Append(Util.ObjectToXml(MaxValue, "h:MaxValue", true));
            sb.Append(Util.ObjectToXml(MinValue, "h:MinValue", true));
            sb.Append(Util.ObjectToXml(Precision, "h:Precision", true));
            sb.Append(Util.ObjectToXml(PrecisionSource, "h:PrecisionSource", true));
            sb.Append(Util.ObjectToXml(SourceTypeMask, "h:SourceTypeMask", true));
            return sb.ToString();
        }
        static internal new MoneyAttributeMetadata LoadFromXml(XElement item)
        {
            MoneyAttributeMetadata moneyAttributeMetadata = new MoneyAttributeMetadata();
            AttributeMetadata.LoadFromXml(item, moneyAttributeMetadata);
            moneyAttributeMetadata.FormulaDefinition = Util.LoadFromXml<string>(item.Element(Util.ns.h + "FormulaDefinition"));
            moneyAttributeMetadata.ImeMode = Util.LoadFromXml<ImeMode?>(item.Element(Util.ns.h + "ImeMode"));
            moneyAttributeMetadata.IsBaseCurrency = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "IsBaseCurrency"));
            moneyAttributeMetadata.MaxValue = Util.LoadFromXml<double?>(item.Element(Util.ns.h + "MaxValue"));
            moneyAttributeMetadata.MinValue = Util.LoadFromXml<double?>(item.Element(Util.ns.h + "MinValue"));
            moneyAttributeMetadata.Precision = Util.LoadFromXml<int?>(item.Element(Util.ns.h + "Precision"));
            moneyAttributeMetadata.PrecisionSource = Util.LoadFromXml<int?>(item.Element(Util.ns.h + "PrecisionSource"));
            moneyAttributeMetadata.SourceTypeMask = Util.LoadFromXml<int?>(item.Element(Util.ns.h + "SourceTypeMask"));
            return moneyAttributeMetadata;
        }
    }
    public sealed class OneToManyRelationshipMetadata : RelationshipMetadataBase
    {
        public AssociatedMenuConfiguration AssociatedMenuConfiguration { get; set; }
        public CascadeConfiguration CascadeConfiguration { get; set; }
        public string ReferencedAttribute { get; set; }
        public string ReferencedEntity { get; set; }
        public string ReferencingAttribute { get; set; }
        public string ReferencingEntity { get; set; }
        public OneToManyRelationshipMetadata() : base(RelationshipType.OneToManyRelationship) { }
        internal new string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToValueXml());
            sb.Append(Util.ObjectToXml(AssociatedMenuConfiguration, "h:AssociatedMenuConfiguration", true));
            sb.Append(Util.ObjectToXml(CascadeConfiguration, "h:CascadeConfiguration", true));
            sb.Append(Util.ObjectToXml(ReferencedAttribute, "h:ReferencedAttribute", true));
            sb.Append(Util.ObjectToXml(ReferencedEntity, "h:ReferencedEntity", true));
            sb.Append(Util.ObjectToXml(ReferencingAttribute, "h:ReferencingAttribute", true));
            sb.Append(Util.ObjectToXml(ReferencingEntity, "h:ReferencingEntity", true));
            return sb.ToString();
        }
        static internal new OneToManyRelationshipMetadata LoadFromXml(XElement item)
        {
            OneToManyRelationshipMetadata oneToManyRelationshipMetadata = new OneToManyRelationshipMetadata()
            {
                AssociatedMenuConfiguration = AssociatedMenuConfiguration.LoadFromXml(item.Element(Util.ns.h + "AssociatedMenuConfiguration")),
                CascadeConfiguration = CascadeConfiguration.LoadFromXml(item.Element(Util.ns.h + "CascadeConfiguration")),
                ReferencedAttribute = Util.LoadFromXml<string>(item.Element(Util.ns.h + "ReferencedAttribute")),
                ReferencedEntity = Util.LoadFromXml<string>(item.Element(Util.ns.h + "ReferencedEntity")),
                ReferencingAttribute = Util.LoadFromXml<string>(item.Element(Util.ns.h + "ReferencingAttribute")),
                ReferencingEntity = Util.LoadFromXml<string>(item.Element(Util.ns.h + "ReferencingEntity")),
            };
            RelationshipMetadataBase.LoadFromXml(item, oneToManyRelationshipMetadata);
            return oneToManyRelationshipMetadata;
        }
    }
    public class OptionMetadata : MetadataBase
    {
        public Label Description { get; set; }
        public bool? IsManaged { get; set; }
        public Label Label { get; set; }
        public int? Value { get; set; }
        public OptionMetadata() { }
        public OptionMetadata(Label label, int? value)
        {
            this.Label = label;
            this.Value = value;
        }
        public OptionMetadata(int value)
        {
            this.Value = value;
        }
        internal new string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToValueXml());
            sb.Append(Util.ObjectToXml(Description, "h:Description", true));
            sb.Append(Util.ObjectToXml(IsManaged, "h:IsManaged", true));
            sb.Append(Util.ObjectToXml(Label, "h:Label", true));
            sb.Append(Util.ObjectToXml(Value, "h:Value", true));
            return sb.ToString();
        }
        static internal OptionMetadata LoadFromXml(XElement item)
        {
            OptionMetadata optionMetadata = new OptionMetadata();
            OptionMetadata.LoadFromXml(item, optionMetadata);
            return optionMetadata;
        }
        static internal void LoadFromXml(XElement item, OptionMetadata meta)
        {
            if (item.Elements().Count() == 0)
                return;
            MetadataBase.LoadFromXml(item, meta);
            meta.Description = Label.LoadFromXml(item.Element(Util.ns.h + "Description"));
            meta.IsManaged = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "IsManaged"));
            meta.Label = Label.LoadFromXml(item.Element(Util.ns.h + "Label"));
            meta.Value = Util.LoadFromXml<int?>(item.Element(Util.ns.h + "Value"));
        }
    }
    public sealed class OptionMetadataCollection : DataCollection<OptionMetadata>
    {
        public OptionMetadataCollection() { }
        public OptionMetadataCollection(IList<OptionMetadata> list)
        {
            this.AddRange(list);
        }
        internal string ToValueXml()
        {
            return Util.ObjectToXml(this.ToArray(), "h:OptionMetadata", true);
        }
        static internal OptionMetadataCollection LoadFromXml(XElement item)
        {
            OptionMetadataCollection optionMetadataCollection = new OptionMetadataCollection();
            foreach (var optionMetadata in item.Elements(Util.ns.h + "OptionMetadata"))
            {
                optionMetadataCollection.Add(OptionMetadata.LoadFromXml(optionMetadata));
            }
            return optionMetadataCollection;
        }
    }
    public sealed class OptionSetMetadata : OptionSetMetadataBase
    {
        public OptionMetadataCollection Options { get; set; }
        public OptionSetMetadata()
        {
            this.Options = new OptionMetadataCollection();
        }
        public OptionSetMetadata(OptionMetadataCollection options)
            : this()
        {
            this.Options = options;
        }
        internal new string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToValueXml());
            sb.Append(Util.ObjectToXml(Options, "h:Options", true));
            return sb.ToString();
        }
        static internal new OptionSetMetadata LoadFromXml(XElement item)
        {
            OptionSetMetadata optionSetMetadata = new OptionSetMetadata();
            OptionSetMetadata.LoadFromXml(item, optionSetMetadata);
            return optionSetMetadata;
        }
        static internal void LoadFromXml(XElement item, OptionSetMetadata meta)
        {
            if (item.Elements().Count() == 0)
                return;
            OptionSetMetadataBase.LoadFromXml(item, meta);
            meta.Options = OptionMetadataCollection.LoadFromXml(item.Element(Util.ns.h + "Options"));
        }
    }
    public class OptionSetMetadataBase : MetadataBase
    {
        public Label Description { get; set; }
        public Label DisplayName { get; set; }
        public string IntroducedVersion { get; set; }
        public BooleanManagedProperty IsCustomizable { get; set; }
        public bool? IsCustomOptionSet { get; set; }
        public bool? IsGlobal { get; set; }
        public bool? IsManaged { get; set; }
        public string Name { get; set; }
        public OptionSetType? OptionSetType { get; set; }
        internal new string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToValueXml());
            sb.Append(Util.ObjectToXml(Description, "h:Description", true));
            sb.Append(Util.ObjectToXml(DisplayName, "h:DisplayName", true));
            sb.Append(Util.ObjectToXml(IsCustomizable, "h:IsCustomizable", true));
            sb.Append(Util.ObjectToXml(IsCustomOptionSet, "h:IsCustomOptionSet", true));
            sb.Append(Util.ObjectToXml(IsGlobal, "h:IsGlobal", true));
            sb.Append(Util.ObjectToXml(IsManaged, "h:IsManaged", true));
            sb.Append(Util.ObjectToXml(Name, "h:Name", true));
            sb.Append(Util.ObjectToXml(IntroducedVersion, "h:IntroducedVersion", true));
            sb.Append(Util.ObjectToXml(OptionSetType, "h:OptionSetType", true));
            return sb.ToString();
        }
        static internal OptionSetMetadataBase LoadFromXml(XElement item)
        {
            OptionSetMetadataBase optionSetMetadataBase = new OptionSetMetadataBase();
            string type = (item.Attribute(Util.ns.i + "type") == null) ? "OptionSetMetadataBase" :
                        item.Attribute(Util.ns.i + "type").Value.Substring(2);
            switch (type)
            {
                case "OptionSetMetadata":
                    optionSetMetadataBase = OptionSetMetadata.LoadFromXml(item);
                    break;
                case "BooleanOptionSetMetadata":
                    optionSetMetadataBase = BooleanOptionSetMetadata.LoadFromXml(item);
                    break;
                default:
                    OptionSetMetadataBase.LoadFromXml(item, optionSetMetadataBase);
                    break;
            }
            return optionSetMetadataBase;
        }
        static internal void LoadFromXml(XElement item, OptionSetMetadataBase meta)
        {
            if (item.Elements().Count() == 0)
                return;
            MetadataBase.LoadFromXml(item, meta);
            meta.Description = Label.LoadFromXml(item.Element(Util.ns.h + "Description"));
            meta.DisplayName = Label.LoadFromXml(item.Element(Util.ns.h + "DisplayName"));
            meta.IntroducedVersion = Util.LoadFromXml<string>(item.Element(Util.ns.h + "IntroducedVersion"));
            meta.IsCustomizable = BooleanManagedProperty.LoadFromXml(item.Element(Util.ns.h + "IsCustomizable"));
            meta.IsCustomOptionSet = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "IsCustomOptionSet"));
            meta.IsGlobal = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "IsGlobal"));
            meta.IsManaged = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "IsManaged"));
        }
    }
    public sealed class PicklistAttributeMetadata : EnumAttributeMetadata
    {
        public PicklistAttributeMetadata() : this(null) { }
        public PicklistAttributeMetadata(string schemaName)
            : base(AttributeTypeCode.Picklist, schemaName) { }
        internal new string ToValueXml()
        {
            return base.ToValueXml();
        }
        static internal new PicklistAttributeMetadata LoadFromXml(XElement item)
        {
            PicklistAttributeMetadata picklistAttributeMetadata = new PicklistAttributeMetadata();
            EnumAttributeMetadata.LoadFromXml(item, picklistAttributeMetadata);
            return picklistAttributeMetadata;
        }
    }
    public class RelationshipMetadataBase : MetadataBase
    {
        public string IntroducedVersion { get; set; }
        public BooleanManagedProperty IsCustomizable { get; set; }
        public bool? IsCustomRelationship { get; set; }
        public bool? IsManaged { get; set; }
        public bool? IsValidForAdvancedFind { get; set; }
        public RelationshipType RelationshipType { get; set; }
        public string SchemaName { get; set; }
        public SecurityTypes? SecurityTypes { get; set; }
        public RelationshipMetadataBase() { }
        protected RelationshipMetadataBase(RelationshipType type)
        {
            this.RelationshipType = type;
        }
        internal new string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToValueXml());
            sb.Append(Util.ObjectToXml(IsCustomizable, "h:IsCustomizable", true));
            sb.Append(Util.ObjectToXml(IsCustomRelationship, "h:IsCustomRelationship", true));
            sb.Append(Util.ObjectToXml(IsManaged, "h:IsManaged", true));
            sb.Append(Util.ObjectToXml(IsValidForAdvancedFind, "h:IsValidForAdvancedFind", true));
            sb.Append(Util.ObjectToXml(SchemaName, "h:SchemaName", true));
            sb.Append(Util.ObjectToXml(SecurityTypes, "h:SecurityTypes", true));
            sb.Append(Util.ObjectToXml(IntroducedVersion, "h:IntroducedVersion", true));
            sb.Append(Util.ObjectToXml(RelationshipType, "h:RelationshipType", true));
            return sb.ToString();
        }
        static internal RelationshipMetadataBase LoadFromXml(XElement item)
        {
            RelationshipMetadataBase relationshipMetadataBase = new RelationshipMetadataBase();
            string type = (item.Attribute(Util.ns.i + "type") == null) ? "RelationshipMetadataBase" :
                        item.Attribute(Util.ns.i + "type").Value.Substring(2);
            switch (type)
            {
                case "OneToManyRelationshipMetadata":
                    relationshipMetadataBase = OneToManyRelationshipMetadata.LoadFromXml(item);
                    break;
                case "ManyToManyRelationshipMetadata":
                    relationshipMetadataBase = ManyToManyRelationshipMetadata.LoadFromXml(item);
                    break;
                default:
                    RelationshipMetadataBase.LoadFromXml(item, relationshipMetadataBase);
                    break;
            }
            return relationshipMetadataBase;
        }
        static internal void LoadFromXml(XElement item, RelationshipMetadataBase meta)
        {
            if (item.Elements().Count() == 0)
                return;
            MetadataBase.LoadFromXml(item, meta);
            meta.IntroducedVersion = Util.LoadFromXml<string>(item.Element(Util.ns.h + "IntroducedVersion"));
            meta.IsCustomizable = BooleanManagedProperty.LoadFromXml(item.Element(Util.ns.h + "IsCustomizable"));
            meta.IsCustomRelationship = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "IsCustomRelationship"));
            meta.IsManaged = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "IsManaged"));
            meta.IsValidForAdvancedFind = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "IsValidForAdvancedFind"));
            meta.SchemaName = Util.LoadFromXml<string>(item.Element(Util.ns.h + "SchemaName"));
            meta.SecurityTypes = Util.LoadFromXml<SecurityTypes?>(item.Element(Util.ns.h + "SecurityTypes"));
        }
    }
    public sealed class SecurityPrivilegeMetadata
    {
        public bool CanBeBasic { get; set; }
        public bool CanBeDeep { get; set; }
        public bool CanBeGlobal { get; set; }
        public bool CanBeLocal { get; set; }
        public string Name { get; set; }
        public Guid PrivilegeId { get; set; }
        public PrivilegeType PrivilegeType { get; set; }
        internal string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Util.ObjectToXml(CanBeBasic, "h:CanBeBasic", true));
            sb.Append(Util.ObjectToXml(CanBeDeep, "h:CanBeDeep", true));
            sb.Append(Util.ObjectToXml(CanBeGlobal, "h:CanBeGlobal", true));
            sb.Append(Util.ObjectToXml(CanBeLocal, "h:CanBeLocal", true));
            sb.Append(Util.ObjectToXml(Name, "h:Name", true));
            sb.Append(Util.ObjectToXml(PrivilegeId, "h:PrivilegeId", true));
            sb.Append(Util.ObjectToXml(PrivilegeType, "h:PrivilegeType", true));
            return sb.ToString();
        }
        static internal SecurityPrivilegeMetadata LoadFromXml(XElement item)
        {
            SecurityPrivilegeMetadata securityPrivilegeMetadata = new SecurityPrivilegeMetadata()
            {
                CanBeBasic = Util.LoadFromXml<bool>(item.Element(Util.ns.h + "CanBeBasic")),
                CanBeDeep = Util.LoadFromXml<bool>(item.Element(Util.ns.h + "CanBeDeep")),
                CanBeGlobal = Util.LoadFromXml<bool>(item.Element(Util.ns.h + "CanBeGlobal")),
                CanBeLocal = Util.LoadFromXml<bool>(item.Element(Util.ns.h + "CanBeLocal")),
                Name = Util.LoadFromXml<string>(item.Element(Util.ns.h + "Name")),
                PrivilegeId = Util.LoadFromXml<Guid>(item.Element(Util.ns.h + "PrivilegeId")),
                PrivilegeType = Util.LoadFromXml<PrivilegeType>(item.Element(Util.ns.h + "PrivilegeType"))
            };
            return securityPrivilegeMetadata;
        }
    }
    public sealed class StateAttributeMetadata : EnumAttributeMetadata
    {
        public StateAttributeMetadata()
            : base(AttributeTypeCode.State, null) { }
        internal new string ToValueXml()
        {
            return base.ToValueXml();
        }
        static internal new StateAttributeMetadata LoadFromXml(XElement item)
        {
            StateAttributeMetadata stateAttributeMetadata = new StateAttributeMetadata();
            EnumAttributeMetadata.LoadFromXml(item, stateAttributeMetadata);
            return stateAttributeMetadata;
        }
    }
    public sealed class StateOptionMetadata : OptionMetadata
    {
        public int? DefaultStatus { get; set; }
        public string InvariantName { get; set; }

    }
    public sealed class StatusAttributeMetadata : EnumAttributeMetadata
    {
        public StatusAttributeMetadata()
            : base(AttributeTypeCode.Status, null) { }
        internal new string ToValueXml()
        {
            return base.ToValueXml();
        }
        static internal new StatusAttributeMetadata LoadFromXml(XElement item)
        {
            StatusAttributeMetadata statusAttributeMetadata = new StatusAttributeMetadata();
            EnumAttributeMetadata.LoadFromXml(item, statusAttributeMetadata);
            return statusAttributeMetadata;
        }
    }
    public sealed class StatusOptionMetadata : OptionMetadata
    {
        public int? State { get; set; }
        public StatusOptionMetadata() { }
        public StatusOptionMetadata(int value, int? state)
        {
            this.Value = value;
            this.State = state;
        }
    }
    public sealed class StringAttributeMetadata : AttributeMetadata
    {
        public const int MaxSupportedLength = 4000;
        public const int MinSupportedLength = 1;
        public StringFormat? Format { get; set; }
        public StringFormatName FormatName { get; set; }
        public string FormulaDefinition { get; set; }
        public ImeMode? ImeMode { get; set; }
        public bool? IsLocalizable { get; set; }
        private int? _maxLength;
        public int? MaxLength
        {
            get { return _maxLength; }
            set
            {
                if (value < MinSupportedLength || value > MaxSupportedLength)
                    // Should throw error?
                    return;
                _maxLength = value;
            }
        }
        public int? SourceTypeMask { get; set; }
        public string YomiOf { get; set; }
        public StringAttributeMetadata() : this(null) { }
        public StringAttributeMetadata(string schemaName)
            : base(AttributeTypeCode.String, schemaName) { }
        internal new string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToValueXml());
            sb.Append(Util.ObjectToXml(Format, "h:Format", true));
            sb.Append(Util.ObjectToXml(FormatName, "h:FormatName", true));
            sb.Append(Util.ObjectToXml(FormulaDefinition, "h:FormulaDefinition", true));
            sb.Append(Util.ObjectToXml(ImeMode, "h:ImeMode", true));
            sb.Append(Util.ObjectToXml(IsLocalizable, "h:IsLocalizable", true));
            sb.Append(Util.ObjectToXml(MaxLength, "h:MaxLength", true));
            sb.Append(Util.ObjectToXml(SourceTypeMask, "h:SourceTypeMask", true));
            sb.Append(Util.ObjectToXml(YomiOf, "h:YomiOf", true));
            return sb.ToString();
        }
        static internal new StringAttributeMetadata LoadFromXml(XElement item)
        {
            StringAttributeMetadata stringAttributeMetadata = new StringAttributeMetadata();
            AttributeMetadata.LoadFromXml(item, stringAttributeMetadata);
            stringAttributeMetadata.Format = Util.LoadFromXml<StringFormat?>(item.Element(Util.ns.h + "Format"));
            stringAttributeMetadata.FormatName = StringFormatName.LoadFromXml(item.Element(Util.ns.h + "FormatName"));
            stringAttributeMetadata.FormulaDefinition = Util.LoadFromXml<string>(item.Element(Util.ns.h + "FormulaDefinition"));
            stringAttributeMetadata.ImeMode = Util.LoadFromXml<ImeMode?>(item.Element(Util.ns.h + "ImeMode"));
            stringAttributeMetadata.IsLocalizable = Util.LoadFromXml<bool?>(item.Element(Util.ns.h + "IsLocalizable"));
            stringAttributeMetadata.MaxLength = Util.LoadFromXml<int?>(item.Element(Util.ns.h + "MaxLength"));
            stringAttributeMetadata.SourceTypeMask = Util.LoadFromXml<int?>(item.Element(Util.ns.h + "SourceTypeMask"));
            stringAttributeMetadata.YomiOf = Util.LoadFromXml<string>(item.Element(Util.ns.h + "YomiOf"));
            return stringAttributeMetadata;
        }
    }
    public sealed class StringFormatName : ConstantsBase<string>
    {
        public static readonly StringFormatName Email;
        public static readonly StringFormatName Phone;
        public static readonly StringFormatName PhoneticGuide;
        public static readonly StringFormatName Text;
        public static readonly StringFormatName TextArea;
        public static readonly StringFormatName TickerSymbol;
        public static readonly StringFormatName Url;
        public static readonly StringFormatName VersionNumber;
        static StringFormatName()
        {
            Email = Add<StringFormatName>("Email");
            Text = Add<StringFormatName>("Text");
            TextArea = Add<StringFormatName>("TextArea");
            Url = Add<StringFormatName>("Url");
            TickerSymbol = Add<StringFormatName>("TickerSymbol");
            PhoneticGuide = Add<StringFormatName>("PhoneticGuide");
            VersionNumber = Add<StringFormatName>("VersionNumber");
            Phone = Add<StringFormatName>("Phone");
        }
        protected override bool ValueExistsInList(String value)
        {
            return ValidValues.Contains(value, StringComparer.OrdinalIgnoreCase);
        }
        internal string ToValueXml()
        {
            return Util.ObjectToXml(Value, "k:Value", true);
        }
        static internal StringFormatName LoadFromXml(XElement item)
        {
            if (item.Elements().Count() == 0)
                return null;
            StringFormatName stringFormatName = new StringFormatName()
            {
                Value = Util.LoadFromXml<string>(item.Element(Util.ns.k + "Value"))
            };
            return stringFormatName;
        }
    }
}