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
using Microsoft.Xrm.Sdk.Query.Samples;
using Microsoft.Xrm.Sdk.Utility.Samples;

using System;
using System.Collections.Generic;
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

namespace Microsoft.Xrm.Sdk.Metadata.Query.Samples
{
    public enum DeletedMetadataFilters
    {
        Default = 1,
        Entity = 1,
        Attribute = 2,
        Relationship = 4,
        Label = 8,
        OptionSet = 16,
        All = 31,
    }
    public enum MetadataConditionOperator
    {
        Equals = 0,
        NotEquals = 1,
        In = 2,
        NotIn = 3,
        GreaterThan = 4,
        LessThan = 5,
    }
    public sealed class AttributeQueryExpression : MetadataQueryExpression
    {
        internal new string ToValueXml()
        {
            return base.ToValueXml();
        }
    }
    public sealed class DeletedMetadataCollection : Dictionary<DeletedMetadataFilters, DataCollection<Guid>>
    {
        static internal DeletedMetadataCollection LoadFromXml(XElement doc)
        {
            DeletedMetadataCollection deletedMetadataCollection = new DeletedMetadataCollection();
            foreach (var item in doc.Elements(Util.ns.j + "KeyValuePairOfDeletedMetadataFiltersArrayOfguidPlUv_PKtF"))
            {
                DataCollection<Guid> value = new DataCollection<Guid>();
                foreach (var guid in item.Element(Util.ns.b + "value").Elements(Util.ns.f + "guid"))
                {
                    value.Add(Util.LoadFromXml<Guid>(guid));
                }
                deletedMetadataCollection.Add(Util.LoadFromXml<DeletedMetadataFilters>(item.Element(Util.ns.b + "key")), value);
            }
            return deletedMetadataCollection;
        }
    }
    public sealed class EntityQueryExpression : MetadataQueryExpression
    {
        public AttributeQueryExpression AttributeQuery { get; set; }
        public LabelQueryExpression LabelQuery { get; set; }
        public RelationshipQueryExpression RelationshipQuery { get; set; }
        internal new string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToValueXml());
            sb.Append(Util.ObjectToXml(AttributeQuery, "j:AttributeQuery", true));
            sb.Append(Util.ObjectToXml(LabelQuery, "j:LabelQuery", true));
            sb.Append(Util.ObjectToXml(RelationshipQuery, "j:RelationshipQuery", true));
            return sb.ToString();
        }
    }
    public sealed class LabelQueryExpression : MetadataQueryBase
    {
        public DataCollection<int> FilterLanguages { get; set; }
        public int? MissingLabelBehavior { get; set; }
        public LabelQueryExpression()
        {
            FilterLanguages = new DataCollection<int>();
        }
        internal string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Util.ObjectToXml(FilterLanguages.ToArray(), "j:FilterLanguages", true));
            sb.Append(Util.ObjectToXml(MissingLabelBehavior, "j:MissingLabelBehavior", true));
            return sb.ToString();
        }
    }
    public sealed class MetadataConditionExpression
    {
        public MetadataConditionOperator ConditionOperator { get; set; }
        public string PropertyName { get; set; }
        public Object Value { get; set; }
        public MetadataConditionExpression() { }
        public MetadataConditionExpression(string propertyName, MetadataConditionOperator conditionOperator, Object value)
        {
            this.PropertyName = propertyName;
            this.ConditionOperator = conditionOperator;
            this.Value = value;
        }
        internal string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Util.ObjectToXml(ConditionOperator, "j:ConditionOperator", true));
            sb.Append(Util.ObjectToXml(PropertyName, "j:PropertyName", true));
            sb.Append(Util.ObjectToXml(Value, "j:Value"));
            return sb.ToString();
        }
    }
    public sealed class MetadataFilterExpression
    {
        public DataCollection<MetadataConditionExpression> Conditions { get; set; }
        public LogicalOperator FilterOperator { get; set; }
        public DataCollection<MetadataFilterExpression> Filters { get; set; }
        public MetadataFilterExpression()
        {
            Conditions = new DataCollection<MetadataConditionExpression>();
            Filters = new DataCollection<MetadataFilterExpression>();
        }
        public MetadataFilterExpression(LogicalOperator filterOperator)
            : this()
        {
            this.FilterOperator = filterOperator;
        }
        internal string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Util.ObjectToXml(Conditions.ToArray(), "j:Conditions", true));
            sb.Append(Util.ObjectToXml(FilterOperator, "j:FilterOperator", true));
            sb.Append(Util.ObjectToXml(Filters.ToArray(), "j:Filters", true));
            return sb.ToString();
        }
    }
    public sealed class MetadataPropertiesExpression
    {
        public bool AllProperties { get; set; }
        public DataCollection<string> PropertyNames { get; set; }
        public MetadataPropertiesExpression()
        {
            PropertyNames = new DataCollection<string>();
        }
        public MetadataPropertiesExpression(params string[] propertyNames)
            : this()
        {
            this.PropertyNames.AddRange(propertyNames);
        }
        internal string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Util.ObjectToXml(AllProperties, "j:AllProperties", true));
            sb.Append(Util.ObjectToXml(PropertyNames.ToArray(), "j:PropertyNames", true));
            return sb.ToString();
        }
    }
    public abstract class MetadataQueryBase { }
    public abstract class MetadataQueryExpression : MetadataQueryBase
    {
        public MetadataFilterExpression Criteria { get; set; }
        public MetadataPropertiesExpression Properties { get; set; }
        internal string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Util.ObjectToXml(Criteria, "j:Criteria", true));
            sb.Append(Util.ObjectToXml(Properties, "j:Properties", true));
            return sb.ToString();
        }
    }
    public sealed class RelationshipQueryExpression : MetadataQueryExpression
    {
        internal new string ToValueXml()
        {
            return base.ToValueXml();
        }
    }
}