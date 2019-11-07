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

using Microsoft.Crm.Sdk.Messages.Samples;
using Microsoft.Xrm.Sdk.Samples;
using Microsoft.Xrm.Sdk.Metadata.Samples;
using Microsoft.Xrm.Sdk.Metadata.Query.Samples;
using Microsoft.Xrm.Sdk.Query.Samples;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using System.Globalization;

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

namespace Microsoft.Xrm.Sdk.Utility.Samples
{
    #region Utility class
    // Utility Class
    static internal class Util
    {
        // NameSpaces for SOAP. Need to consider to migrate with class members of OrganizationDataWebServiceProxy
        // This NameSpaces are exactly same as Soap.js - http://code.msdn.microsoft.com/SdkSoapjs-9b51b99a
        static internal class ns
        {
            static public XNamespace a = "http://schemas.microsoft.com/xrm/2011/Contracts";
            static public XNamespace s = "http://schemas.xmlsoap.org/soap/envelope/";
            static public XNamespace i = "http://www.w3.org/2001/XMLSchema-instance";
            static public XNamespace b = "http://schemas.datacontract.org/2004/07/System.Collections.Generic";
            static public XNamespace c = "http://www.w3.org/2001/XMLSchema";
            static public XNamespace d = "http://schemas.microsoft.com/xrm/2011/Contracts/Services";
            static public XNamespace e = "http://schemas.microsoft.com/2003/10/Serialization/";
            static public XNamespace f = "http://schemas.microsoft.com/2003/10/Serialization/Arrays";
            static public XNamespace g = "http://schemas.microsoft.com/crm/2011/Contracts";
            static public XNamespace h = "http://schemas.microsoft.com/xrm/2011/Metadata";
            static public XNamespace j = "http://schemas.microsoft.com/xrm/2011/Metadata/Query";
            static public XNamespace k = "http://schemas.microsoft.com/xrm/2013/Metadata";
            static public XNamespace l = "http://schemas.microsoft.com/xrm/2012/Contracts";
            static public XNamespace m = "http://schemas.microsoft.com/xrm/2014/Contracts";
            static public XNamespace n = "http://schemas.microsoft.com/xrm/7.1/Metadata";
            static public XNamespace o = "http://schemas.microsoft.com/xrm/7.1/Contracts";
        }
        /// <summary>
        /// Encode XML string, mainly for FetchXML.
        /// </summary>
        /// <param name="data">XML data.</param>
        /// <returns>encoded string</returns>
        static internal string EncodeXML(string data)
        {
            return data.
                Replace("&", "&amp;").
                Replace("<", "&lt;").
                Replace(">", "&gt;").
                Replace("\"", "&quot;").
                Replace("'", "&apos;");
        }
        /// <summary>
        /// This mehod genearte xml for Soap request from object value
        /// </summary>
        /// <param name="item">value</param>
        /// <param name="action">you can specify name like a:value, d:entity for header xml</param>
        /// <param name="elementOnly">pass true when you only need xml without header</param>
        /// <returns></returns>
        static internal string ObjectToXml(object item, string action, bool? elementOnly = null)
        {
            // If there is no value, then return nil with correct node name.
            if (item == null)
                return String.Format("<{0} i:nil='true' />", action);
            // If the value is array type but no item in it, then return empty.
            // When action is b:value, then you need to do extra.
            if (item.GetType().IsArray && ((Array)item).Length == 0 && action != "b:value")
                return String.Format("<{0} />", action);

            // type holds item's type
            string type = "";
            string value = "";
            // Firstly, check if it is Enum Type.
            if (item.GetType().GetTypeInfo().IsEnum)
            {
                //if its Enum, then get value by using helper methods or simply obtain it's name
                switch (item.GetType().Name)
                {
                    case "DeletedMetadataFilters":
                        type = "j:" + item.GetType().Name;
                        value = GetDeletedMetadataFiltersAsString((DeletedMetadataFilters)item);
                        break;
                    case "EntityFilters":
                        type = "h:" + item.GetType().Name;
                        value = GetEntityFiltersAsString((EntityFilters)item);
                        break;
                    case "AccessRights":
                        type = "g:" + item.GetType().Name;
                        value = GetAccessRightsAsString((AccessRights)item);
                        break;
                    case "RibbonLocationFilters":
                        type = "g:" + item.GetType().Name;
                        value = GetRibbonLocationFiltersAsString((RibbonLocationFilters)item);
                        break;
                    case "PrivilegeDepth":
                    case "PropagationOwnershipOptions":
                    case "RollupType":
                    case "SearchDirection":
                    case "TargetFieldType":
                    case "TimeCode":
                        type = "g:" + item.GetType().Name;
                        break;
                    case "AssociatedMenuBehavior":
                    case "AssociatedMenuGroup":
                    case "AttributeTypeCode":
                    case "CascadeType":
                    case "DateTimeFormat":
                    case "ImeMode":
                    case "OptionSetType":
                    case "OwnershipTypes":
                    case "PrivilegeType":
                    case "StringFormat":
                        type = "h:" + item.GetType().Name;
                        break;
                    case "ConditionOperator":
                    case "EntityRole":
                    case "EntityState":
                    case "JoinOperator":
                    case "LogicalOperator":
                    case "OrderType":
                    case "RelationshipType":
                    case "SecurityTypes":
                        type = "a:" + item.GetType().Name;
                        break;
                    case "MetadataConditionOperator":
                        type = "j:" + item.GetType().Name;
                        break;
                    case "ConcurrencyBehavior":
                        type = "o:" + item.GetType().Name;
                        break;    
                }
                // Set value for types which does not have spcecial rule.
                if (value == "")
                    value = item.ToString();
            }
            // if its not Enum, then check if it is Array type.
            else if (item.GetType().IsArray)
            {
                //if it is Array type, then check what array it is.
                string arrayType = ""; // Specify Array Type
                bool valueOnlyForDataCollection = false; // True for Class inherited from DataCollection
                //check if it is Enum Array.
                if (item.GetType().GetTypeInfo().IsEnum)
                    type = "a:ArrayOf" + ReturnTypeNameForArray(item);
                // if it is not Enum Array then check actual class.
                switch (item.GetType().Name)
                {
                    case "ActivityParty[]":
                        //Firstly convert ActivityParty[] to EntityCollection
                        EntityCollection entityCollection = new EntityCollection();
                        foreach (Entity record in (Array)item)
                        {
                            entityCollection.Entities.Add(record);
                        }
                        //Then ToXml()
                        return Util.ObjectToXml(entityCollection, "b:value");
                    case "ConditionExpression[]":
                    case "Entity[]":
                    case "FilterExpression[]":
                    case "LinkEntity[]":
                    case "Money[]":
                    case "OptionSetValue[]":
                    case "OrderExpression[]":
                    case "QueryExpression[]":
                        type = "a:ArrayOf" + ReturnTypeNameForArray(item);
                        arrayType = "a:" + ReturnTypeNameForArray(item);
                        break;
                    case "EntityReference[]":
                    case "LocalizedLabel[]":
                        arrayType = "a:" + ReturnTypeNameForArray(item);
                        valueOnlyForDataCollection = true;
                        break;
                    case "AppointmentsToIgnore[]":
                    case "ConstraintRelation[]":
                    case "ObjectiveRelation[]":
                    case "RequiredResource[]":
                    case "RolePrivilege[]":
                    case "TimeCode[]":
                        type = "g:ArrayOf" + ReturnTypeNameForArray(item);
                        arrayType = "g:" + ReturnTypeNameForArray(item);
                        break;
                    case "ManyToManyRelationshipMetadata[]":
                    case "OneToManyRelationshipMetadata[]":
                    case "SecurityPrivilegeMetadata[]":
                        type = "h:ArrayOf" + ReturnTypeNameForArray(item);
                        arrayType = "h:" + ReturnTypeNameForArray(item);
                        break;
                    case "OptionMetadata[]":
                        arrayType = "h:" + ReturnTypeNameForArray(item);
                        valueOnlyForDataCollection = true;
                        break;
                    case "MetadataConditionExpression[]":
                    case "MetadataFilterExpression[]":
                        type = "j:ArrayOf" + ReturnTypeNameForArray(item);
                        arrayType = "j:" + ReturnTypeNameForArray(item);
                        break;
                    case "Byte[]":
                        type = "c:base64Binary";
                        value = System.Convert.ToBase64String((byte[])item);
                        break;
                    case "Boolean[]":
                        type = "f:ArrayOfboolean";
                        arrayType = "f:boolean";
                        break;
                    case "DateTime[]":
                        type = "f:ArrayOfdateTime";
                        arrayType = "f:dateTime";
                        break;
                    case "Int32[]":
                        type = "f:ArrayOfint";
                        arrayType = "f:int";
                        break;
                    case "Int64[]":
                        type = "f:ArrayOflong";
                        arrayType = "f:long";
                        break;
                    default: // string, guid
                        type = "f:ArrayOf" + ReturnTypeNameForArray(item).ToLower();
                        arrayType = "f:" + ReturnTypeNameForArray(item).ToLower();
                        break;
                }
                // If there is no item in it, return no items value
                if (((Array)item).Length == 0)
                    return String.Format("<{0} i:type='{1}' />", action, type);
                // If arrayType has value, then you need to get XML value of each item in it.
                if (arrayType != "")
                {
                    foreach (var obj in (Array)item)
                    {
                        // call ObjectToXml method again, but get it without header.
                        value += ObjectToXml(obj, arrayType, true);
                    }
                    if (valueOnlyForDataCollection)
                        return value;
                }
            }
            // if it is not Enum nor Array.
            else
            {
                string objType = item.GetType().Name;
                // Check if it's Early Bound Entity Class, which inherit from Entity class
                if (item.GetType().GetTypeInfo().BaseType == typeof(Entity))
                {
                    // Then set objType as Entity
                    objType = "Entity";
                }
                switch (objType)
                {
                    case "AppointmentRequest":
                        type = "g:AppointmentRequest";
                        value = ((AppointmentRequest)item).ToValueXml();
                        break;
                    case "AppointmentsToIgnore":
                        type = "g:AppointmentsToIgnore";
                        value = ((AppointmentsToIgnore)item).ToValueXml();
                        break;
                    case "AssociatedMenuConfiguration":
                        type = "h:AssociatedMenuConfiguration";
                        value = ((AssociatedMenuConfiguration)item).ToValueXml();
                        break;
                    case "AttributeQueryExpression":
                        type = "j:AttributeQueryExpression";
                        value = ((AttributeQueryExpression)item).ToValueXml();
                        break;
                    case "AttributeRequiredLevelManagedProperty":
                        type = "a:AttributeRequiredLevelManagedProperty";
                        value = ((AttributeRequiredLevelManagedProperty)item).ToValueXml();
                        break;
                    case "AttributeTypeDisplayName":
                        type = "k:AttributeTypeDisplayName";
                        value = ((AttributeTypeDisplayName)item).ToValueXml();
                        break;
                    case "BigIntAttributeMetadata":
                        type = "h:BigIntAttributeMetadata";
                        value = ((BigIntAttributeMetadata)item).ToValueXml();
                        break;
                    case "BooleanAttributeMetadata":
                        type = "h:BooleanAttributeMetadata";
                        value = ((BooleanAttributeMetadata)item).ToValueXml();
                        break;
                    case "BooleanOptionSetMetadata":
                        type = "h:BooleanOptionSetMetadata";
                        value = ((BooleanOptionSetMetadata)item).ToValueXml();
                        break;
                    case "BooleanManagedProperty":
                        type = "a:BooleanManagedProperty";
                        value = ((BooleanManagedProperty)item).ToValueXml();
                        break;
                    case "CascadeConfiguration":
                        type = "h:CascadeConfiguration";
                        value = ((CascadeConfiguration)item).ToValueXml();
                        break;
                    case "ColumnSet":
                        type = "a:ColumnSet";
                        value = ((ColumnSet)item).ToValueXml();
                        break;
                    case "ConstraintRelation":
                        type = "g:ConstraintRelation";
                        value = ((ConstraintRelation)item).ToValueXml();
                        break;
                    case "ConditionExpression":
                        type = "a:ConditionExpression";
                        value = ((ConditionExpression)item).ToValueXml();
                        break;
                    case "DateTimeAttributeMetadata":
                        type = "h:DateTimeAttributeMetadata";
                        value = ((DateTimeAttributeMetadata)item).ToValueXml();
                        break;
                    case "DecimalAttributeMetadata":
                        type = "h:DecimalAttributeMetadata";
                        value = ((DecimalAttributeMetadata)item).ToValueXml();
                        break;
                    case "DoubleAttributeMetadata":
                        type = "h:DoubleAttributeMetadata";
                        value = ((DoubleAttributeMetadata)item).ToValueXml();
                        break;
                    case "Entity":
                        type = "a:Entity";
                        value = ((Entity)item).ToValueXml();
                        break;
                    case "EntityAttributeCollection":
                        type = "o:EntityAttributeCollection";
                        value = ((EntityAttributeCollection)item).ToValueXml();
                        break;
                    case "EntityKeyMetadata":
                        type = "n:EntityKeyMetadata";
                        value = ((EntityKeyMetadata)item).ToValueXml();
                        break;
                    case "EntityCollection":
                        type = "a:EntityCollection";
                        value = ((EntityCollection)item).ToValueXml();
                        break;
                    case "EntityNameAttributeMetadata":
                        type = "h:EntityNameAttributeMetadata";
                        value = ((EntityNameAttributeMetadata)item).ToValueXml();
                        break;
                    case "EntityMetadata":
                        type = "h:EntityMetadata";
                        value = ((EntityMetadata)item).ToValueXml();
                        break;
                    case "EntityQueryExpression":
                        type = "j:EntityQueryExpression";
                        value = ((EntityQueryExpression)item).ToValueXml();
                        break;
                    case "EntityReference":
                        type = "a:EntityReference";
                        value = ((EntityReference)item).ToValueXml();
                        break;
                    case "EntityReferenceCollection":
                        type = "a:EntityReferenceCollection";
                        // if there is no item in it, then return no items value
                        if (((EntityReferenceCollection)item).Count == 0)
                        {
                            if (action == "b:value")
                                return String.Format("<{0} i:type='{1}' />", action, type);
                            else
                                return String.Format("<{0} />", action);

                        }
                        else
                            value = ((EntityReferenceCollection)item).ToValueXml();
                        break;
                    case "FetchExpression":
                        type = "a:FetchExpression";
                        value = ((FetchExpression)item).ToValueXml();
                        break;
                    case "FilterExpression":
                        type = "a:FilterExpression";
                        value = ((FilterExpression)item).ToValueXml();
                        break;
                    case "ImageAttributeMetadata":
                        type = "k:ImageAttributeMetadata";
                        value = ((ImageAttributeMetadata)item).ToValueXml();
                        break;
                    case "IntegerAttributeMetadata":
                        type = "h:IntegerAttributeMetadata";
                        value = ((IntegerAttributeMetadata)item).ToValueXml();
                        break;
                    case "Label":
                        type = "a:Label";
                        value = ((Label)item).ToValueXml();
                        break;
                    case "LabelQueryExpression":
                        type = "j:LabelQueryExpression";
                        value = ((LabelQueryExpression)item).ToValueXml();
                        break;
                    case "LinkEntity":
                        type = "a:LinkEntity";
                        value = ((LinkEntity)item).ToValueXml();
                        break;
                    case "LocalizedLabel":
                        type = "a:LocalizedLabel";
                        value = ((LocalizedLabel)item).ToValueXml();
                        break;
                    case "LocalizedLabelCollection":
                        type = "a:LocalizedLabelCollection";
                        // if there is no item in it, then return no items value
                        if (((LocalizedLabelCollection)item).Count == 0)
                        {
                            if (action == "b:value")
                                return String.Format("<{0} i:type='{1}' />", action, type);
                            else
                                return String.Format("<{0} />", action);

                        }
                        else
                            value = ((LocalizedLabelCollection)item).ToValueXml();
                        break;
                    case "LookupAttributeMetadata":
                        type = "h:LookupAttributeMetadata";
                        value = ((LookupAttributeMetadata)item).ToValueXml();
                        break;
                    case "ManagedPropertyAttributeMetadata":
                        type = "h:ManagedPropertyAttributeMetadata";
                        value = ((ManagedPropertyAttributeMetadata)item).ToValueXml();
                        break;
                    case "ManyToManyRelationshipMetadata":
                        type = "h:ManyToManyRelationshipMetadata";
                        value = ((ManyToManyRelationshipMetadata)item).ToValueXml();
                        break;
                    case "MetadataConditionExpression":
                        type = "j:MetadataConditionExpression";
                        value = ((MetadataConditionExpression)item).ToValueXml();
                        break;
                    case "MetadataFilterExpression":
                        type = "j:MetadataFilterExpression";
                        value = ((MetadataFilterExpression)item).ToValueXml();
                        break;
                    case "MetadataPropertiesExpression":
                        type = "j:MetadataPropertiesExpression";
                        value = ((MetadataPropertiesExpression)item).ToValueXml();
                        break;
                    case "MemoAttributeMetadata":
                        type = "h:MemoAttributeMetadata";
                        value = ((MemoAttributeMetadata)item).ToValueXml();
                        break;
                    case "Money":
                        type = "a:Money";
                        value = ((Money)item).ToValueXml();
                        break;
                    case "MoneyAttributeMetadata":
                        type = "h:MoneyAttributeMetadata";
                        value = ((MoneyAttributeMetadata)item).ToValueXml();
                        break;
                    case "OneToManyRelationshipMetadata":
                        type = "h:OneToManyRelationshipMetadata";
                        value = ((OneToManyRelationshipMetadata)item).ToValueXml();
                        break;
                    case "ObjectiveRelation":
                        type = "g:ObjectiveRelation";
                        value = ((ObjectiveRelation)item).ToValueXml();
                        break;
                    case "OptionMetadata":
                        type = "h:OptionMetadata";
                        value = ((OptionMetadata)item).ToValueXml();
                        break;
                    case "OptionMetadataCollection":
                        type = "h:OptionMetadataCollection";
                        // if there is no item in it, then return no items value
                        if (((OptionMetadataCollection)item).Count == 0)
                        {
                            if (action == "b:value")
                                return String.Format("<{0} i:type='{1}' />", action, type);
                            else
                                return String.Format("<{0} />", action);

                        }
                        else
                            value = ((OptionMetadataCollection)item).ToValueXml();
                        break;
                    case "OptionSetMetadata":
                        type = "h:OptionSetMetadata";
                        value = ((OptionSetMetadata)item).ToValueXml();
                        break;
                    case "OptionSetValue":
                        type = "a:OptionSetValue";
                        value = ((OptionSetValue)item).ToValueXml();
                        break;
                    case "OrderExpression":
                        type = "a:OrderExpression";
                        value = ((OrderExpression)item).ToValueXml();
                        break;
                    //case "OrganizationRequestCollection":
                    //    type = "l:OrganizationRequestCollection";
                    //    // if there is no item in it, then return no items value
                    //    if (((OrganizationRequestCollection)item).Count == 0)
                    //    {
                    //        if (action == "b:value")
                    //            return String.Format("<{0} i:type='{1}' />", action, type);
                    //        else
                    //            return String.Format("<{0} />", action);

                    //    }
                    //    else
                    //        value = ((OrganizationRequestCollection)item).ToValueXml();
                    //    break;
                    case "PagingInfo":
                        type = "a:PagingInfo";
                        value = ((PagingInfo)item).ToValueXml();
                        break;
                    case "QueryByAttribute":
                        type = "a:QueryByAttribute";
                        value = ((QueryByAttribute)item).ToValueXml();
                        break;
                    case "QueryExpression":
                        type = "a:QueryExpression";
                        value = ((QueryExpression)item).ToValueXml();
                        break;
                    case "PicklistAttributeMetadata":
                        type = "h:PicklistAttributeMetadata";
                        value = ((PicklistAttributeMetadata)item).ToValueXml();
                        break;
                    case "PrincipalAccess":
                        type = "g:PrincipalAccess";
                        value = ((PrincipalAccess)item).ToValueXml();
                        break;
                    case "Relationship":
                        type = "a:Relationship";
                        value = ((Relationship)item).ToValueXml();
                        break;
                    case "RelationshipQueryExpression":
                        type = "j:RelationshipQueryExpression";
                        value = ((RelationshipQueryExpression)item).ToValueXml();
                        break;
                    case "RequiredResource":
                        type = "g:RequiredResource";
                        value = ((RequiredResource)item).ToValueXml();
                        break;
                    case "RolePrivilege":
                        type = "g:RolePrivilege";
                        value = ((RolePrivilege)item).ToValueXml();
                        break;
                    case "SecurityPrivilegeMetadata":
                        type = "h:SecurityPrivilegeMetadata";
                        value = ((SecurityPrivilegeMetadata)item).ToValueXml();
                        break;
                    case "StateAttributeMetadata":
                        type = "h:StateAttributeMetadata";
                        value = ((StateAttributeMetadata)item).ToValueXml();
                        break;
                    case "StatusAttributeMetadata":
                        type = "h:StatusAttributeMetadata";
                        value = ((StatusAttributeMetadata)item).ToValueXml();
                        break;
                    case "StringAttributeMetadata":
                        type = "h:StringAttributeMetadata";
                        value = ((StringAttributeMetadata)item).ToValueXml();
                        break;
                    case "StringFormatName":
                        type = "k:StringFormatName";
                        value = ((StringFormatName)item).ToValueXml();
                        break;
                    case "DateTime":
                        type = "c:dateTime";
                        value = ((DateTime)item).ToString("o");
                        break;
                    case "Boolean":
                        type = "c:boolean";
                        value = item.ToString().ToLower();
                        break;
                    case "Guid":
                        type = "e:guid";
                        break;
                    case "Int16":
                        type = "c:int";
                        break;
                    case "Int32":
                        type = "c:int";
                        break;
                    case "Int64":
                        type = "c:long";
                        break;
                    case "Decimal":
                        type = "c:decimal";
                        break;
                    case "String":
                        type = "c:string";
                        // value might be xml. so encode it just in case.
                        value = Util.EncodeXML(item.ToString());
                        break;
                    default:
                        type = "c:" + item.GetType().Name.ToLower();
                        break;
                }
                // Set value for types which does not have spcecial rule.
                if (value == "")
                    value = item.ToString();
            }
            
            // return just value without type information
            if (elementOnly != null && (bool)elementOnly)
            {
                return String.Format("<{0}>{1}</{0}>", action, value);
            }
            // return with type information
            else
            {
                return String.Format("<{0} i:type='{1}'>{2}</{0}>", action, type, value);
            }
        }

        /// <summary>
        /// This method returns typename for array like TimeCode[] to TimeCode
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        static internal string ReturnTypeNameForArray(object item)
        {
            // Remove last 2 letters ([])
            return item.GetType().Name.Substring(0, item.GetType().Name.Length - 2);
        }

        /// <summary>
        /// This method deserialize xml data to object if you already know which type to be deserialized
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        static internal T LoadFromXml<T>(XElement item)
        {
            if (item == null)
                return default(T);

            // Handle Nullable first if item has no value.
            if (typeof(T).Name == "Nullable`1" && item.Value == "")
                return default(T);

            if(typeof(T) == typeof(string))
            {
                return (T)Convert.ChangeType(item.Value, typeof(T));
            }
            // Handle primitive types with culture
            if (typeof(T) == typeof(long) || typeof(T) == typeof(long?) || typeof(T) == typeof(DateTime) || typeof(T) == typeof(DateTime?) ||
                typeof(T) == typeof(decimal) || typeof(T) == typeof(decimal?) || typeof(T) == typeof(double) || typeof(T) == typeof(double?) ||
                typeof(T) == typeof(int) || typeof(T) == typeof(int?))
            {
                T obj = default(T);
                Type type = typeof(T);
                // In case it is Nullable, then check GenericTypeArguments for it's type.
                if (typeof(T).GenericTypeArguments.Length > 0)
                    type = typeof(T).GenericTypeArguments[0];
                // Get MinValue
                FieldInfo fieldInfo = type.GetTypeInfo().GetDeclaredField("MinValue");
                // If type has minvalue, then set it to obj, otherwise leave the default.
                if (fieldInfo != null)
                    obj = (T)fieldInfo.GetValue(null);
                MethodInfo methodInfo = type.GetRuntimeMethod("Parse", new Type[] { typeof(string), typeof(IFormatProvider) });

                return (item.Value == "") ? obj : (T)methodInfo.Invoke(null, new object[] { item.Value, CultureInfo.InvariantCulture });
            }
            // Handle other primitive types
            if (typeof(T) == typeof(bool) || typeof(T) == typeof(bool?))
            {
                T obj = default(T);
                Type type = typeof(T);
                // In case it is Nullable, then check GenericTypeArguments for it's type.
                if (typeof(T).GenericTypeArguments.Length > 0)
                    type = typeof(T).GenericTypeArguments[0];
               
                MethodInfo methodInfo = type.GetRuntimeMethod("Parse", new Type[] { typeof(string)});

                return (item.Value == "") ? obj : (T)methodInfo.Invoke(null, new object[] { item.Value});
            }
            // Handle Guid explicitly to workaround Xamarin limitation
            // http://developer.xamarin.com/guides/android/advanced_topics/linking/
            if (typeof(T) == typeof(Guid) || typeof(T) == typeof(Guid?))
                return (T)(Object)Guid.Parse(item.Value);
            // Handle Enum. 
            if (typeof(T).GetTypeInfo().IsEnum)
            {
                if (typeof(T) == typeof(SecurityTypes) && item.Value.Contains("Inheritance"))
                    return (T)Enum.Parse(typeof(T), "Inheritance");
                else
                    return (T)Enum.Parse(typeof(T), item.Value);
            }
            // In case it is Nullable, then check GenericTypeArguments for it's type.
            if (typeof(T).GenericTypeArguments.Length > 0 &&
                (typeof(T).GenericTypeArguments[0]).GetTypeInfo().IsEnum)
            {
                Type enumType = typeof(T).GenericTypeArguments[0];
                if (enumType == typeof(SecurityTypes) && item.Value.Contains("Inheritance"))
                    return (T)Enum.Parse(typeof(T), "Inheritance");
                else
                    return (T)Enum.Parse(enumType, item.Value);
            }
            // Everything else which should have LoadFromXml method
            MethodInfo loadMethod = typeof(T).GetTypeInfo().GetDeclaredMethod("LoadFromXml");
            // if there is no LoadFromXml method or item does not contains elements, then return null
            if (loadMethod == null || item.Elements().Count() == 0)
                return default(T);
            T record = (T)loadMethod.Invoke(null, new object[] { item });
            return record;
        }
        /// <summary>
        /// This method deserialize xml data to object. Use this method when you don't know the type yet.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        static internal object ObjectFromXml(XElement item)
        {
            if (item == null || item.Value == "")
                return null;
            Object value = "";
            // Obtain CrmType. I haven't cover all types yet.
            string CrmType = item.Attribute(Util.ns.i + "type").Value.Substring(2);
            switch (CrmType)
            {
                case "long":
                    value = Util.LoadFromXml<long>(item);
                    break;
                case "decimal":
                    value = Util.LoadFromXml<decimal>(item);
                    break;
                case "string":
                    value = item.Value;
                    break;
                case "base64Binary":
                    value = Convert.FromBase64String(item.Value);
                    break;
                case "int":
                    value = Util.LoadFromXml<int>(item);
                    break;
                case "double":
                    value = Util.LoadFromXml<double>(item);
                    break;
                case "dateTime":
                    value = Util.LoadFromXml<DateTime>(item);
                    break;
                case "guid":
                    value = Util.LoadFromXml<Guid>(item);
                    break;
                case "boolean":
                    value = Util.LoadFromXml<bool>(item);
                    break;
                case "EntityReference":
                    value = EntityReference.LoadFromXml(item);
                    break;
                case "AliasedValue":
                    value = AliasedValue.LoadFromXml(item);
                    break;
                case "OptionSetValue":
                    value = OptionSetValue.LoadFromXml(item);
                    break;
                case "Money":
                    value = Money.LoadFromXml(item);
                    break;
                case "EntityCollection":
                    value = EntityCollection.LoadFromXml(item);
                    break;
                default:
                    break;
            }
            return value;
        }

        #region Enum helper
        
        // Enums needs its own way to convert to string.
        static internal string GetAccessMaskAsString(AccessRights enumValue)
        {
            List<string> valueArray = new List<string>();
            string returnValue = "None";

            if (enumValue.HasFlag(AccessRights.ReadAccess) || enumValue.HasFlag(AccessRights.All))
            {
                valueArray.Add("ReadAccess");
            }
            if (enumValue.HasFlag(AccessRights.WriteAccess) || enumValue.HasFlag(AccessRights.All))
            {
                valueArray.Add("WriteAccess");
            }
            if (enumValue.HasFlag(AccessRights.ShareAccess) || enumValue.HasFlag(AccessRights.All))
            {
                valueArray.Add("ShareAccess");
            }
            if (enumValue.HasFlag(AccessRights.AssignAccess) || enumValue.HasFlag(AccessRights.All))
            {
                valueArray.Add("AssignAccess");
            }
            if (enumValue.HasFlag(AccessRights.AppendAccess) || enumValue.HasFlag(AccessRights.All))
            {
                valueArray.Add("AppendAccess");
            }
            if (enumValue.HasFlag(AccessRights.AppendToAccess) || enumValue.HasFlag(AccessRights.All))
            {
                valueArray.Add("AppendToAccess");
            }
            if (enumValue.HasFlag(AccessRights.CreateAccess) || enumValue.HasFlag(AccessRights.All))
            {
                valueArray.Add("CreateAccess");
            }
            if (enumValue.HasFlag(AccessRights.DeleteAccess) || enumValue.HasFlag(AccessRights.All))
            {
                valueArray.Add("DeleteAccess");
            }
            returnValue = String.Join(" ", valueArray.ToArray());

            return returnValue;
        }
        static internal string GetAccessRightsAsString(AccessRights enumValue)
        {
            List<string> valueArray = new List<string>();
            string returnValue = "None";

            if (enumValue.HasFlag(AccessRights.ReadAccess) || enumValue.HasFlag(AccessRights.All))
            {
                valueArray.Add("ReadAccess");
            }
            if (enumValue.HasFlag(AccessRights.WriteAccess) || enumValue.HasFlag(AccessRights.All))
            {
                valueArray.Add("WriteAccess");
            }
            if (enumValue.HasFlag(AccessRights.ShareAccess) || enumValue.HasFlag(AccessRights.All))
            {
                valueArray.Add("ShareAccess");
            }
            if (enumValue.HasFlag(AccessRights.AssignAccess) || enumValue.HasFlag(AccessRights.All))
            {
                valueArray.Add("AssignAccess");
            }
            if (enumValue.HasFlag(AccessRights.AppendAccess) || enumValue.HasFlag(AccessRights.All))
            {
                valueArray.Add("AppendAccess");
            }
            if (enumValue.HasFlag(AccessRights.AppendToAccess) || enumValue.HasFlag(AccessRights.All))
            {
                valueArray.Add("AppendToAccess");
            }
            if (enumValue.HasFlag(AccessRights.CreateAccess) || enumValue.HasFlag(AccessRights.All))
            {
                valueArray.Add("CreateAccess");
            }
            if (enumValue.HasFlag(AccessRights.DeleteAccess) || enumValue.HasFlag(AccessRights.All))
            {
                valueArray.Add("DeleteAccess");
            }
            returnValue = String.Join(" ", valueArray.ToArray());

            return returnValue;
        }
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
            if (returnValue == "None")
                valueArray.Add("Entity");
            returnValue = String.Join(" ", valueArray.ToArray());

            return returnValue;
        }
        static internal string GetEntityFiltersAsString(EntityFilters enumValue)
        {
            List<string> valueArray = new List<string>();
            string returnValue = "None";

            if (enumValue.HasFlag(EntityFilters.Entity) || enumValue.HasFlag(EntityFilters.All))
            {
                valueArray.Add("Entity");
            }
            if (enumValue.HasFlag(EntityFilters.Attributes) || enumValue.HasFlag(EntityFilters.All))
            {
                valueArray.Add("Attributes");
            }
            if (enumValue.HasFlag(EntityFilters.Privileges) || enumValue.HasFlag(EntityFilters.All))
            {
                valueArray.Add("Privileges");
            }
            if (enumValue.HasFlag(EntityFilters.Relationships) || enumValue.HasFlag(EntityFilters.All))
            {
                valueArray.Add("Relationships");
            }
            if (returnValue == "None")
                valueArray.Add("Entity");
            returnValue = String.Join(" ", valueArray.ToArray());

            return returnValue;
        }
        static internal string GetRibbonLocationFiltersAsString(RibbonLocationFilters enumValue)
        {
            List<string> valueArray = new List<string>();
            string returnValue = "None";

            if (enumValue.HasFlag(RibbonLocationFilters.Form) || enumValue.HasFlag(RibbonLocationFilters.All))
            {
                valueArray.Add("Form");
            }
            if (enumValue.HasFlag(RibbonLocationFilters.HomepageGrid) || enumValue.HasFlag(RibbonLocationFilters.All))
            {
                valueArray.Add("HomepageGrid");
            }
            if (enumValue.HasFlag(RibbonLocationFilters.SubGrid) || enumValue.HasFlag(RibbonLocationFilters.All))
            {
                valueArray.Add("SubGrid");
            }
            if (returnValue == "None")
                valueArray.AddRange(new List<string>() { "Form", "HomepageGrid", "SubGrid" });
            returnValue = String.Join(" ", valueArray.ToArray());

            return returnValue;
        }
        static internal AccessRights GetAccessRightsFromString(string value)
        {
            AccessRights accessRights = AccessRights.None;
            foreach (var accessRight in value.Split(' '))
            {
                switch (accessRight)
                {
                    case "ReadAccess":
                        accessRights |= AccessRights.ReadAccess;
                        break;
                    case "WriteAccess":
                        accessRights |= AccessRights.WriteAccess;
                        break;
                    case "ShareAccess":
                        accessRights |= AccessRights.ShareAccess;
                        break;
                    case "AssignAccess":
                        accessRights |= AccessRights.AssignAccess;
                        break;
                    case "AppendAccess":
                        accessRights |= AccessRights.AppendAccess;
                        break;
                    case "AppendToAccess":
                        accessRights |= AccessRights.AppendToAccess;
                        break;
                    case "CreateAccess":
                        accessRights |= AccessRights.CreateAccess;
                        break;
                    case "DeleteAccess":
                        accessRights |= AccessRights.DeleteAccess;
                        break;
                }
            }

            return accessRights;
        }
        static internal EntityFilters GetEntityFiltersFromString(string value)
        {
            EntityFilters entityFilters = EntityFilters.Default;
            foreach (var entityFilter in value.Split(' '))
            {
                switch (entityFilter)
                {
                    case "Entity":
                        entityFilters |= EntityFilters.Entity;
                        break;
                    case "Attributes":
                        entityFilters |= EntityFilters.Attributes;
                        break;
                    case "Privileges":
                        entityFilters |= EntityFilters.Privileges;
                        break;
                    case "Relationships":
                        entityFilters |= EntityFilters.Relationships;
                        break;
                }
            }
            return entityFilters;
        }

        #endregion
    }

    #endregion Utility class
}