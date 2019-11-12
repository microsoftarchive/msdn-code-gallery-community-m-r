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
using Microsoft.Xrm.Sdk.Utility.Samples;

using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Microsoft.Xrm.Sdk.Query.Samples
{
    public enum ConditionOperator
    {
        Equal = 0,
        NotEqual = 1,
        GreaterThan = 2,
        LessThan = 3,
        GreaterEqual = 4,
        LessEqual = 5,
        Like = 6,
        NotLike = 7,
        In = 8,
        NotIn = 9,
        Between = 10,
        NotBetween = 11,
        Null = 12,
        NotNull = 13,
        Yesterday = 14,
        Today = 15,
        Tomorrow = 16,
        Last7Days = 17,
        Next7Days = 18,
        LastWeek = 19,
        ThisWeek = 20,
        NextWeek = 21,
        LastMonth = 22,
        ThisMonth = 23,
        NextMonth = 24,
        On = 25,
        OnOrBefore = 26,
        OnOrAfter = 27,
        LastYear = 28,
        ThisYear = 29,
        NextYear = 30,
        LastXHours = 31,
        NextXHours = 32,
        LastXDays = 33,
        NextXDays = 34,
        LastXWeeks = 35,
        NextXWeeks = 36,
        LastXMonths = 37,
        NextXMonths = 38,
        LastXYears = 39,
        NextXYears = 40,
        EqualUserId = 41,
        NotEqualUserId = 42,
        EqualBusinessId = 43,
        NotEqualBusinessId = 44,
        ChildOf = 45,
        Mask = 46,
        NotMask = 47,
        MasksSelect = 48,
        Contains = 49,
        DoesNotContain = 50,
        EqualUserLanguage = 51,
        NotOn = 52,
        OlderThanXMonths = 53,
        BeginsWith = 54,
        DoesNotBeginWith = 55,
        EndsWith = 56,
        DoesNotEndWith = 57,
        ThisFiscalYear = 58,
        ThisFiscalPeriod = 59,
        NextFiscalYear = 60,
        NextFiscalPeriod = 61,
        LastFiscalYear = 62,
        LastFiscalPeriod = 63,
        LastXFiscalYears = 64,
        LastXFiscalPeriods = 65,
        NextXFiscalYears = 66,
        NextXFiscalPeriods = 67,
        InFiscalYear = 68,
        InFiscalPeriod = 69,
        InFiscalPeriodAndYear = 70,
        InOrBeforeFiscalPeriodAndYear = 71,
        InOrAfterFiscalPeriodAndYear = 72,
        EqualUserTeams = 73,
        EqualUserOrUserTeams = 74
    }
    public enum JoinOperator
    {
        Inner = 0,
        LeftOuter = 1,
        Natural
    }
    public enum LogicalOperator
    {
        And,
        Or
    }
    public enum OrderType
    {
        Ascending,
        Descending
    }
    public sealed class ColumnSet
    {
        public bool AllColumns { get; set; }
        public DataCollection<string> Columns { get; set; }
        public ColumnSet()
        {
            this.Columns = new DataCollection<string>();
        }
        public ColumnSet(bool allColumns)
            : this()
        {
            this.AllColumns = allColumns;
        }
        public ColumnSet(params string[] columns)
            : this()
        {
            this.AddColumns(columns);
            this.AllColumns = false;
        }
        public void AddColumn(string column)
        {
            this.Columns.Add(column);
        }
        public void AddColumns(params string[] columns)
        {
            foreach (var item in columns)
            {
                this.Columns.Add(item);
            }
        }
        internal string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Util.ObjectToXml(AllColumns, "a:AllColumns", true));
            sb.Append(Util.ObjectToXml(Columns.ToArray(), "a:Columns", true));
            return sb.ToString();
        }
        static internal ColumnSet LoadFromXml(XElement item)
        {
            ColumnSet columnSet = new ColumnSet()
            {
                AllColumns = Util.LoadFromXml<bool>(item.Element(Util.ns.a + "AllColumns"))
            };
            foreach (XElement Column in item.Element(Util.ns.a + "Columns").Elements(Util.ns.f + "string"))
            {
                columnSet.Columns.Add(Column.Value);
            }
            return columnSet;
        }
    }
    public sealed class ConditionExpression
    {
        public string AttributeName { get; set; }
        public string EntityName { get; set; }
        public ConditionOperator Operator { get; set; }
        public DataCollection<Object> Values;
        public ConditionExpression()
        {
            Values = new DataCollection<object>();
        }
        public ConditionExpression(string attributeName, ConditionOperator conditionOperator, object value = null)
            : this()
        {
            this.AttributeName = attributeName;
            this.Operator = conditionOperator;
            if (value != null)
            {
                if (value.GetType().IsArray)
                {
                    foreach (var item in (Array)value)
                    {
                        Values.Add(item);
                    }
                }
                else
                    this.Values.Add(value);
            }
        }
        public ConditionExpression(string attributeName, ConditionOperator conditionOperator, params object[] values)
            : this()
        {
            this.AttributeName = attributeName;
            this.Operator = conditionOperator;
            foreach (var item in values)
                Values.Add(item);
        }
        public ConditionExpression(string entityName, string attributeName, ConditionOperator conditionOperator, object value = null) :
            this(attributeName, conditionOperator, value)
        {
            this.EntityName = entityName;
        }
        public ConditionExpression(string entityName, string attributeName, ConditionOperator conditionOperator, params object[] values) :
            this(attributeName, conditionOperator, values)
        {
            this.EntityName = entityName;
        }
        internal string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Util.ObjectToXml(AttributeName, "a:AttributeName", true));
            sb.Append(Util.ObjectToXml(Operator, "a:Operator", true));
            if (this.Values.Count == 0)
                sb.Append("<a:Values/>");
            else
            {
                sb.Append("<a:Values>");
                foreach (object item in Values)
                {
                    // If value is passed as List, not Array, then 
                    // Need to check here only when operation is In or NotIn
                    // No other possibilities???
                    if (this.Operator == ConditionOperator.In || this.Operator == ConditionOperator.NotIn)
                    {
                        if (item.GetType() == typeof(List<Guid>))
                        {
                            foreach (var o in (List<Guid>)item)
                            {
                                sb.Append(ValueToXml(o));
                            }
                        }
                        else if (item.GetType() == typeof(List<DateTime>))
                        {
                            foreach (var o in (List<DateTime>)item)
                            {
                                sb.Append(ValueToXml(o));
                            }
                        }
                        else if (item.GetType() == typeof(List<Decimal>))
                        {
                            foreach (var o in (List<Decimal>)item)
                            {
                                sb.Append(ValueToXml(o));
                            }
                        }
                        else if (item.GetType() == typeof(List<Double>))
                        {
                            foreach (var o in (List<Double>)item)
                            {
                                sb.Append(ValueToXml(o));
                            }
                        }
                        else if (item.GetType() == typeof(List<EntityReference>))
                        {
                            foreach (var o in (List<EntityReference>)item)
                            {
                                sb.Append(ValueToXml(o));
                            }
                        }
                        else if (item.GetType() == typeof(List<Int32>))
                        {
                            foreach (var o in (List<Int32>)item)
                            {
                                sb.Append(ValueToXml(o));
                            }
                        }
                        else if (item.GetType() == typeof(List<Int64>))
                        {
                            foreach (var o in (List<Int64>)item)
                            {
                                sb.Append(ValueToXml(o));
                            }
                        }
                        else if (item.GetType() == typeof(List<OptionSetValue>))
                        {
                            foreach (var o in (List<OptionSetValue>)item)
                            {
                                sb.Append(ValueToXml(o));
                            }
                        }
                        else if (item.GetType() == typeof(List<string>))
                        {
                            foreach (var o in (List<string>)item)
                            {
                                sb.Append(ValueToXml(o));
                            }
                        }
                        // If it is not List, then it should be single value.
                        else
                            sb.Append(ValueToXml(item));
                    }
                    else
                        sb.Append(ValueToXml(item));
                }
                sb.Append("</a:Values>");
            }
            sb.Append(Util.ObjectToXml(EntityName, "a:EntityName", true));
            return sb.ToString();
        }
        // As this is unique to ConditionExpression, I don't use each class's ToXml(), but do all job here.
        internal string ValueToXml(object item)
        {
            return Util.ObjectToXml(item, "f:anyType");//Attribute.AttributeValueToXml(item, "f:anyType");           
        }
        static internal ConditionExpression LoadFromXml(XElement item)
        {
            ConditionExpression conditionExpression = new ConditionExpression()
            {
                AttributeName = Util.LoadFromXml<string>(item.Element(Util.ns.a + "AttributeName")),
                EntityName = Util.LoadFromXml<string>(item.Element(Util.ns.a + "EntityName")),
                Operator = Util.LoadFromXml<ConditionOperator>(item.Element(Util.ns.a + "Operator"))
            };
            foreach (XElement value in item.Element(Util.ns.a + "Values").Elements(Util.ns.f + "anyType"))
            {
                conditionExpression.Values.Add(Util.ObjectFromXml(value));
            }
            return conditionExpression;
        }
    }
    public sealed class FetchExpression : QueryBase
    {
        public string Query { get; set; }
        public FetchExpression() { }
        public FetchExpression(string Query)
        {
            this.Query = Query;
        }
        internal string ToValueXml()
        {
            return Util.ObjectToXml(Query, "a:Query", true);
        }
    }
    public sealed class FilterExpression
    {
        public DataCollection<ConditionExpression> Conditions { get; set; }
        public LogicalOperator FilterOperator { get; set; }
        public DataCollection<FilterExpression> Filters { get; set; }
        public bool IsQuickFindFilter { get; set; }
        public FilterExpression()
        {
            this.Conditions = new DataCollection<ConditionExpression>();
            this.Filters = new DataCollection<FilterExpression>();
        }
        public FilterExpression(LogicalOperator filterOperator)
            : this()
        {
            this.FilterOperator = filterOperator;
        }
        public void AddCondition(ConditionExpression ConditionExpression)
        {
            this.Conditions.Add(ConditionExpression);
        }
        public void AddCondition(string attributeName, ConditionOperator conditionOperator, params Object[] values)
        {
            if (values.Count() == 0)
                this.Conditions.Add(new ConditionExpression(attributeName, conditionOperator));
            else
                this.Conditions.Add(new ConditionExpression(attributeName, conditionOperator, values));
        }
        public void AddCondition(string entityName, string attributeName, ConditionOperator conditionOperator, params Object[] values)
        {
            this.Conditions.Add(new ConditionExpression(entityName, attributeName, conditionOperator, values));
        }
        public void AddFilter(FilterExpression childFilter)
        {
            this.Filters.Add(childFilter);
        }
        public FilterExpression AddFilter(LogicalOperator logicalOperator)
        {
            FilterExpression filterExpression = new FilterExpression()
            {
                FilterOperator = logicalOperator
            };
            this.Filters.Add(filterExpression);
            return filterExpression;
        }
        internal string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Util.ObjectToXml(Conditions.ToArray(), "a:Conditions", true));
            sb.Append(Util.ObjectToXml(FilterOperator, "a:FilterOperator", true));
            sb.Append(Util.ObjectToXml(Filters.ToArray(), "a:Filters", true));
            sb.Append(Util.ObjectToXml(IsQuickFindFilter, "a:IsQuickFindFilter", true));
            return sb.ToString();
        }
        static internal FilterExpression LoadFromXml(XElement item)
        {
            FilterExpression filterExpression = new FilterExpression()
            {
                FilterOperator = Util.LoadFromXml<LogicalOperator>(item.Element(Util.ns.a + "FilterOperator"))
            };
            foreach (XElement condition in item.Element(Util.ns.a + "Conditions").Elements(Util.ns.a + "ConditionExpression"))
            {
                filterExpression.Conditions.Add(ConditionExpression.LoadFromXml(condition));
            }
            foreach (XElement filter in item.Element(Util.ns.a + "Filters").Elements(Util.ns.a + "FilterExpression"))
            {
                filterExpression.Filters.Add(FilterExpression.LoadFromXml(filter));
            }
            if (item.Element(Util.ns.a + "IsQuickFindFilter") != null)
                filterExpression.IsQuickFindFilter = Util.LoadFromXml<bool>(item.Element(Util.ns.a + "IsQuickFindFilter"));
            return filterExpression;
        }
    }
    public sealed class LinkEntity
    {
        public ColumnSet Columns { get; set; }
        public string EntityAlias { get; set; }
        public JoinOperator JoinOperator { get; set; }
        public FilterExpression LinkCriteria { get; set; }
        public DataCollection<LinkEntity> LinkEntities { get; set; }
        public string LinkFromAttributeName { get; set; }
        public string LinkFromEntityName { get; set; }
        public string LinkToAttributeName { get; set; }
        public string LinkToEntityName { get; set; }
        public LinkEntity()
        {
            Columns = new ColumnSet();
            LinkCriteria = new FilterExpression();
            LinkEntities = new DataCollection<LinkEntity>();
        }
        internal string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Util.ObjectToXml(Columns, "a:Columns", true));
            sb.Append(Util.ObjectToXml(EntityAlias, "a:EntityAlias", true));
            sb.Append(Util.ObjectToXml(JoinOperator, "a:JoinOperator", true));
            sb.Append(Util.ObjectToXml(LinkCriteria, "a:LinkCriteria", true));
            sb.Append(Util.ObjectToXml(LinkEntities.ToArray(), "a:LinkEntities", true));
            sb.Append(Util.ObjectToXml(LinkFromAttributeName, "a:LinkFromAttributeName", true));
            sb.Append(Util.ObjectToXml(LinkFromEntityName, "a:LinkFromEntityName", true));
            sb.Append(Util.ObjectToXml(LinkToAttributeName, "a:LinkToAttributeName", true));
            sb.Append(Util.ObjectToXml(LinkToEntityName, "a:LinkToEntityName", true));
            return sb.ToString();
        }
        static internal LinkEntity LoadFromXml(XElement item)
        {
            LinkEntity linkEntity = new LinkEntity()
            {
                Columns = ColumnSet.LoadFromXml(item.Element(Util.ns.a + "Columns")),
                EntityAlias = Util.LoadFromXml<string>(item.Element(Util.ns.a + "EntityAlias")),
                JoinOperator = Util.LoadFromXml<JoinOperator>(item.Element(Util.ns.a + "JoinOperator")),
                LinkCriteria = FilterExpression.LoadFromXml(item.Element(Util.ns.a + "LinkCriteria")),
                LinkFromAttributeName = Util.LoadFromXml<string>(item.Element(Util.ns.a + "LinkFromAttributeName")),
                LinkFromEntityName = Util.LoadFromXml<string>(item.Element(Util.ns.a + "LinkFromEntityName")),
                LinkToAttributeName = Util.LoadFromXml<string>(item.Element(Util.ns.a + "LinkToAttributeName")),
                LinkToEntityName = Util.LoadFromXml<string>(item.Element(Util.ns.a + "LinkToEntityName")),
            };
            foreach (XElement childLinkEntity in item.Element(Util.ns.a + "LinkEntities").Elements(Util.ns.a + "LinkEntity"))
            {
                linkEntity.LinkEntities.Add(LinkEntity.LoadFromXml(childLinkEntity));
            }
            return linkEntity;
        }
    }
    public sealed class OrderExpression
    {
        public string AttributeName { get; set; }
        public OrderType OrderType { get; set; }
        public OrderExpression() { }
        public OrderExpression(string attributeName, OrderType orderType)
        {
            this.AttributeName = attributeName;
            this.OrderType = orderType;
        }
        internal string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Util.ObjectToXml(AttributeName, "a:AttributeName", true));
            sb.Append(Util.ObjectToXml(OrderType, "a:OrderType", true));
            return sb.ToString();
        }
        static internal OrderExpression LoadFromXml(XElement item)
        {
            OrderExpression orderExpression = new OrderExpression()
            {
                AttributeName = Util.LoadFromXml<string>(item.Element(Util.ns.a + "AttributeName")),
                OrderType = Util.LoadFromXml<OrderType>(item.Element(Util.ns.a + "OrderType"))
            };
            return orderExpression;
        }
    }
    public sealed class PagingInfo
    {
        public int Count { get; set; }
        public int PageNumber { get; set; }
        public string PagingCookie { get; set; }
        public bool ReturnTotalRecordCount { get; set; }
        internal string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Util.ObjectToXml(Count, "a:Count", true));
            sb.Append(Util.ObjectToXml(PageNumber, "a:PageNumber", true));
            sb.Append(Util.ObjectToXml(PagingCookie, "a:PagingCookie", true));
            sb.Append(Util.ObjectToXml(ReturnTotalRecordCount, "a:ReturnTotalRecordCount", true));
            return sb.ToString();
        }
        static internal PagingInfo LoadFromXml(XElement item)
        {
            PagingInfo pagingInfo = new PagingInfo()
            {
                Count = Util.LoadFromXml<int>(item.Element(Util.ns.a + "Count")),
                PageNumber = Util.LoadFromXml<int>(item.Element(Util.ns.a + "PageNumber")),
                PagingCookie = Util.LoadFromXml<string>(item.Element(Util.ns.a + "PagingCookie")),
                ReturnTotalRecordCount = Util.LoadFromXml<bool>(item.Element(Util.ns.a + "ReturnTotalRecordCount"))
            };
            return pagingInfo;
        }
    }
    public abstract class QueryBase
    {
    }
    public sealed class QueryByAttribute : QueryBase
    {
        public DataCollection<string> Attributes { get; set; }
        public ColumnSet ColumnSet { get; set; }
        public string EntityName { get; set; }
        public DataCollection<OrderExpression> Orders { get; set; }
        public PagingInfo PageInfo { get; set; }
        public int? TopCount { get; set; }
        public DataCollection<Object> Values { get; set; }
        public QueryByAttribute()
        {
            Attributes = new DataCollection<string>();
            ColumnSet = new ColumnSet();
            Orders = new DataCollection<OrderExpression>();
            Values = new DataCollection<object>();
        }
        public QueryByAttribute(string entityName)
            : this()
        {
            EntityName = entityName;
        }
        public void AddAttributeValue(string attributeName, Object value)
        {
            this.Attributes.Add(attributeName);
            this.Values.Add(value);
        }
        public void AddOrder(string attributeName, OrderType orderType)
        {
            this.Orders.Add(new OrderExpression(attributeName, orderType));
        }
        internal string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Util.ObjectToXml(Attributes.ToArray(), "a:Attributes", true));
            sb.Append(Util.ObjectToXml(ColumnSet, "a:ColumnSet", true));
            sb.Append(Util.ObjectToXml(EntityName, "a:EntityName", true));
            sb.Append(Util.ObjectToXml(Orders.ToArray(), "a:Orders", true));
            if (this.Values.Count == 0)
            {
                sb.Append("<a:Values/>");
            }
            else
            {
                sb.Append("<a:Values>");
                foreach (object item in this.Values)
                {
                    sb.Append(Util.ObjectToXml(item, "f:anyType"));
                }
                sb.Append("</a:Values>");
            }
            sb.Append(Util.ObjectToXml(PageInfo, "a:PagingInfo", true));
            sb.Append(Util.ObjectToXml(TopCount, "a:TopCount", true));
            return sb.ToString();
        }
    }
    public sealed class QueryExpression : QueryBase
    {
        public ColumnSet ColumnSet { get; set; }
        public FilterExpression Criteria { get; set; }
        public bool Distinct { get; set; }
        public string EntityName { get; set; }
        public DataCollection<LinkEntity> LinkEntities { get; set; }
        public bool NoLock { get; set; }
        public DataCollection<OrderExpression> Orders { get; set; }
        public PagingInfo PageInfo { get; set; }
        public int? TopCount { get; set; }
        public QueryExpression(string EntityName = null, ColumnSet columnSet = null)
        {
            ColumnSet = new ColumnSet();
            if (columnSet != null)
                this.ColumnSet = columnSet;
            this.Criteria = new FilterExpression();
            this.LinkEntities = new DataCollection<LinkEntity>();
            this.Orders = new DataCollection<OrderExpression>();
            this.PageInfo = new PagingInfo();
            this.EntityName = EntityName;
        }
        public LinkEntity AddLink(string linkToEntityName, string linkFromAttributeName,
            string linkToAttributeName)
        {
            LinkEntity linkEntity = new LinkEntity()
            {
                LinkToEntityName = linkToEntityName,
                LinkFromAttributeName = linkFromAttributeName,
                LinkToAttributeName = linkToAttributeName
            };
            this.LinkEntities.Add(linkEntity);
            return linkEntity;
        }
        public LinkEntity AddLink(string linkToEntityName, string linkFromAttributeName,
            string linkToAttributeName, JoinOperator joinOperator)
        {
            LinkEntity linkEntity = new LinkEntity()
            {
                LinkToEntityName = linkToEntityName,
                LinkFromAttributeName = linkFromAttributeName,
                LinkToAttributeName = linkToAttributeName,
                JoinOperator = joinOperator
            };
            this.LinkEntities.Add(linkEntity);
            return linkEntity;
        }
        internal string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Util.ObjectToXml(ColumnSet, "a:ColumnSet", true));
            sb.Append(Util.ObjectToXml(Criteria, "a:Criteria", true));
            sb.Append(Util.ObjectToXml(Distinct, "a:Distinct", true));
            sb.Append(Util.ObjectToXml(EntityName, "a:EntityName", true));
            sb.Append(Util.ObjectToXml(LinkEntities.ToArray(), "a:LinkEntities", true));
            sb.Append(Util.ObjectToXml(Orders.ToArray(), "a:Orders", true));
            sb.Append(Util.ObjectToXml(PageInfo, "a:PageInfo", true));
            sb.Append(Util.ObjectToXml(NoLock, "a:NoLock", true));
            sb.Append(Util.ObjectToXml(TopCount, "a:TopCount", true));
            return sb.ToString();
        }
        static internal QueryExpression LoadFromXml(XElement item)
        {
            QueryExpression queryExpression = new QueryExpression()
            {
                ColumnSet = ColumnSet.LoadFromXml(item.Element(Util.ns.a + "ColumnSet")),
                Criteria = FilterExpression.LoadFromXml(item.Element(Util.ns.a + "Criteria")),
                Distinct = Util.LoadFromXml<bool>(item.Element(Util.ns.a + "Distinct")),
                EntityName = Util.LoadFromXml<string>(item.Element(Util.ns.a + "EntityName")),
                PageInfo = PagingInfo.LoadFromXml(item.Element(Util.ns.a + "PageInfo")),
                NoLock = Util.LoadFromXml<bool>(item.Element(Util.ns.a + "NoLock"))
            };
            foreach (XElement linkEntity in item.Element(Util.ns.a + "LinkEntities").Elements(Util.ns.a + "LinkEntity"))
            {
                queryExpression.LinkEntities.Add(LinkEntity.LoadFromXml(linkEntity));
            }
            foreach (XElement order in item.Element(Util.ns.a + "Orders").Elements(Util.ns.a + "OrderExpression"))
            {
                queryExpression.Orders.Add(OrderExpression.LoadFromXml(order));
            }
            if (item.Element(Util.ns.a + "TopCount") != null)
                queryExpression.TopCount = Util.LoadFromXml<int>(item.Element(Util.ns.a + "TopCount"));
            return queryExpression;
        }
    }
}