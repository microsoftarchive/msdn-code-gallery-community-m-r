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
using Microsoft.Xrm.Sdk.Metadata.Samples;
using Microsoft.Xrm.Sdk.Metadata.Query.Samples;
using Microsoft.Xrm.Sdk.Query.Samples;
using Microsoft.Xrm.Sdk.Utility.Samples;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Xml.Linq;

// Implementation notes:

// 1. When a request is instantiated, it also instantiates its
// correspoding response and passes it to the Execute method, so that
// it's easy to return the correct response type.

// 2. The request class has an override GetRequestBody method, which 
// returns XML data for the SOAP request.

// 3. Some response classes have an override StoreResult method, which 
// extracts data from HTTPResponse.    

// This namespace implements SOAP versions of the message request/response classes in the
// Microsoft.Xrm.Sdk.Messages namespace of the Dynamics CRM SDK. 

namespace Microsoft.Xrm.Sdk.Messages.Samples
{
    #region Associate
    public sealed class AssociateRequest : OrganizationRequest
    {
        public EntityReference Target
        {
            get
            {
                if (Parameters.Contains("Target"))
                    return (EntityReference)Parameters["Target"];
                return default(EntityReference);
            }
            set { Parameters["Target"] = value; }
        }
        public EntityReferenceCollection RelatedEntities
        {
            get
            {
                if (Parameters.Contains("RelatedEntities"))
                    return (EntityReferenceCollection)Parameters["RelatedEntities"];
                return default(EntityReferenceCollection);
            }
            set { Parameters["RelatedEntities"] = value; }
        }
        public Relationship Relationship
        {
            get
            {
                if (Parameters.Contains("Relationship"))
                    return (Relationship)Parameters["Relationship"];
                return default(Relationship);
            }
            set { Parameters["Relationship"] = value; }
        }
        public AssociateRequest()
        {
            this.ResponseType = new AssociateResponse();
            this.RequestName = "Associate";
        }
        internal override string GetRequestBody()
        {
            Parameters["Target"] = Target;
            Parameters["RelatedEntities"] = RelatedEntities;
            Parameters["Relationship"] = Relationship;
            return GetSoapBody();
        }
    }
    public sealed class AssociateResponse : OrganizationResponse
    {
    }

    #endregion Associate

    #region CanBeReferenced
    public sealed class CanBeReferencedRequest : OrganizationRequest
    {
        public string EntityName
        {
            get
            {
                if (Parameters.Contains("EntityName"))
                    return (string)Parameters["EntityName"];
                return default(string);
            }
            set { Parameters["EntityName"] = value; }
        }
        public CanBeReferencedRequest()
        {
            this.ResponseType = new CanBeReferencedResponse();
            this.RequestName = "CanBeReferenced";
        }
        internal override string GetRequestBody()
        {
            Parameters["EntityName"] = EntityName;
            return GetSoapBody();
        }
    }
    public sealed class CanBeReferencedResponse : OrganizationResponse
    {
        public bool CanBeReferenced { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "CanBeReferenced")
                    this.CanBeReferenced = Util.LoadFromXml<bool>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion CanBeReferenced

    #region CanBeReferencing
    public sealed class CanBeReferencingRequest : OrganizationRequest
    {
        public string EntityName
        {
            get
            {
                if (Parameters.Contains("EntityName"))
                    return (string)Parameters["EntityName"];
                return default(string);
            }
            set { Parameters["EntityName"] = value; }
        }
        public CanBeReferencingRequest()
        {
            this.ResponseType = new CanBeReferencingResponse();
            this.RequestName = "CanBeReferencing";
        }
        internal override string GetRequestBody()
        {
            Parameters["EntityName"] = EntityName;
            return GetSoapBody();
        }
    }
    public sealed class CanBeReferencingResponse : OrganizationResponse
    {
        public bool CanBeReferencing { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "CanBeReferencing")
                    this.CanBeReferencing = Util.LoadFromXml<bool>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion CanBeReferencingResponse

    #region CanManyToMany
    public sealed class CanManyToManyRequest : OrganizationRequest
    {
        public string EntityName
        {
            get
            {
                if (Parameters.Contains("EntityName"))
                    return (string)Parameters["EntityName"];
                return default(string);
            }
            set { Parameters["EntityName"] = value; }
        }
        public CanManyToManyRequest()
        {
            this.ResponseType = new CanManyToManyResponse();
            this.RequestName = "CanManyToMany";

        }
        internal override string GetRequestBody()
        {
            Parameters["EntityName"] = EntityName;
            return GetSoapBody();
        }
    }
    public sealed class CanManyToManyResponse : OrganizationResponse
    {
        public bool CanManyToMany { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "CanManyToMany")
                    this.CanManyToMany = Util.LoadFromXml<bool>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion CanManyToMany

    #region Create
    public sealed class CreateRequest : OrganizationRequest
    {
        public Entity Target
        {
            get
            {
                if (Parameters.Contains("Target"))
                    return (Entity)Parameters["Target"];
                return default(Entity);
            }
            set { Parameters["Target"] = value; }
        }
        public CreateRequest()
        {
            this.ResponseType = new CreateResponse();
            this.RequestName = "Create";
        }
        internal override string GetRequestBody()
        {
            Parameters["Target"] = Target;
            return GetSoapBody();
        }
    }
    public sealed class CreateResponse : OrganizationResponse
    {
        public Guid id { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "id")
                    this.id = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion Create

    #region CreateAttribute
    public sealed class CreateAttributeRequest : OrganizationRequest
    {
        public AttributeMetadata Attribute
        {
            get
            {
                if (Parameters.Contains("Attribute"))
                    return (AttributeMetadata)Parameters["Attribute"];
                return default(AttributeMetadata);
            }
            set { Parameters["Attribute"] = value; }
        }
        public string EntityName
        {
            get
            {
                if (Parameters.Contains("EntityName"))
                    return (string)Parameters["EntityName"];
                return default(string);
            }
            set { Parameters["EntityName"] = value; }
        }
        public string SolutionUniqueName
        {
            get
            {
                if (Parameters.Contains("SolutionUniqueName"))
                    return (string)Parameters["SolutionUniqueName"];
                return default(string);
            }
            set { Parameters["SolutionUniqueName"] = value; }
        }
        public CreateAttributeRequest()
        {
            this.ResponseType = new CreateAttributeResponse();
            this.RequestName = "CreateAttribute";
        }
        internal override string GetRequestBody()
        {
            Parameters["Attribute"] = Attribute;
            Parameters["EntityName"] = EntityName;
            Parameters["SolutionUniqueName"] = SolutionUniqueName;
            return GetSoapBody();
        }
    }
    public sealed class CreateAttributeResponse : OrganizationResponse
    {
        public Guid AttributeId { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "AttributeId")
                    this.AttributeId = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion CreateAttribute

    #region CreateEntity
    public sealed class CreateEntityRequest : OrganizationRequest
    {
        public EntityMetadata Entity
        {
            get
            {
                if (Parameters.Contains("Entity"))
                    return (EntityMetadata)Parameters["Entity"];
                return default(EntityMetadata);
            }
            set { Parameters["Entity"] = value; }
        }
        public bool HasActivities
        {
            get
            {
                if (Parameters.Contains("HasActivities"))
                    return (bool)Parameters["HasActivities"];
                return default(bool);
            }
            set { Parameters["HasActivities"] = value; }
        }
        public bool HasNotes
        {
            get
            {
                if (Parameters.Contains("HasNotes"))
                    return (bool)Parameters["HasNotes"];
                return default(bool);
            }
            set { Parameters["HasNotes"] = value; }
        }
        public StringAttributeMetadata PrimaryAttribute
        {
            get
            {
                if (Parameters.Contains("PrimaryAttribute"))
                    return (StringAttributeMetadata)Parameters["PrimaryAttribute"];
                return default(StringAttributeMetadata);
            }
            set { Parameters["PrimaryAttribute"] = value; }
        }
        public string SolutionUniqueName
        {
            get
            {
                if (Parameters.Contains("SolutionUniqueName"))
                    return (string)Parameters["SolutionUniqueName"];
                return default(string);
            }
            set { Parameters["SolutionUniqueName"] = value; }
        }
        public CreateEntityRequest()
        {
            this.ResponseType = new CreateEntityResponse();
            this.RequestName = "CreateEntity";
        }
        internal override string GetRequestBody()
        {
            Parameters["Entity"] = Entity;
            Parameters["HasActivities"] = HasActivities;
            Parameters["HasNotes"] = HasNotes;
            Parameters["PrimaryAttribute"] = PrimaryAttribute;
            Parameters["SolutionUniqueName"] = SolutionUniqueName;
            return GetSoapBody();
        }
    }
    public sealed class CreateEntityResponse : OrganizationResponse
    {
        public Guid AttributeId { get; set; }
        public Guid EntityId { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "AttributeId")
                    this.AttributeId = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
                if (result.Element(Util.ns.b + "key").Value == "EntityId")
                    this.EntityId = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion CreateEntity

    #region CreateEntityKey
    public sealed class CreateEntityKeyRequest : OrganizationRequest
    {
        public EntityKeyMetadata EntityKey
        {
            get
            {
                if (Parameters.Contains("EntityKey"))
                    return (EntityKeyMetadata)Parameters["EntityKey"];
                return default(EntityKeyMetadata);
            }
            set { Parameters["EntityKey"] = value; }
        }
        public string EntityName
        {
            get
            {
                if (Parameters.Contains("EntityName"))
                    return (string)Parameters["EntityName"];
                return default(string);
            }
            set { Parameters["EntityName"] = value; }
        }
        public string SolutionUniqueName
        {
            get
            {
                if (Parameters.Contains("SolutionUniqueName"))
                    return (string)Parameters["SolutionUniqueName"];
                return default(string);
            }
            set { Parameters["SolutionUniqueName"] = value; }
        }
        public CreateEntityKeyRequest()
        {
            this.ResponseType = new CreateEntityKeyResponse();
            this.RequestName = "CreateEntityKey";
        }
        internal override string GetRequestBody()
        {
            Parameters["EntityKey"] = EntityKey;
            Parameters["EntityName"] = EntityName;
            Parameters["SolutionUniqueName"] = SolutionUniqueName;
            return GetSoapBody();
        }
    }
    public sealed class CreateEntityKeyResponse : OrganizationResponse
    {
        public Guid EntityKeyId { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "EntityKeyId")
                    this.EntityKeyId = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion CreateEntityKey

    #region CreateManyToMany
    public sealed class CreateManyToManyRequest : OrganizationRequest
    {
        public string IntersectEntitySchemaName
        {
            get
            {
                if (Parameters.Contains("IntersectEntitySchemaName"))
                    return (string)Parameters["IntersectEntitySchemaName"];
                return default(string);
            }
            set { Parameters["IntersectEntitySchemaName"] = value; }
        }
        public ManyToManyRelationshipMetadata ManyToManyRelationship
        {
            get
            {
                if (Parameters.Contains("ManyToManyRelationship"))
                    return (ManyToManyRelationshipMetadata)Parameters["ManyToManyRelationship"];
                return default(ManyToManyRelationshipMetadata);
            }
            set { Parameters["ManyToManyRelationship"] = value; }
        }
        public string SolutionUniqueName
        {
            get
            {
                if (Parameters.Contains("SolutionUniqueName"))
                    return (string)Parameters["SolutionUniqueName"];
                return default(string);
            }
            set { Parameters["SolutionUniqueName"] = value; }
        }
        public CreateManyToManyRequest()
        {
            this.ResponseType = new CreateManyToManyResponse();
            this.RequestName = "CreateManyToMany";
        }
        internal override string GetRequestBody()
        {
            Parameters["IntersectEntitySchemaName"] = IntersectEntitySchemaName;
            Parameters["ManyToManyRelationship"] = ManyToManyRelationship;
            Parameters["SolutionUniqueName"] = SolutionUniqueName;
            return GetSoapBody();
        }
    }
    public sealed class CreateManyToManyResponse : OrganizationResponse
    {
        public Guid ManyToManyRelationshipId { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "ManyToManyRelationshipId ")
                    this.ManyToManyRelationshipId = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion CreateManyToMany

    #region CreateOneToMany
    public sealed class CreateOneToManyRequest : OrganizationRequest
    {
        public LookupAttributeMetadata Lookup
        {
            get
            {
                if (Parameters.Contains("Lookup"))
                    return (LookupAttributeMetadata)Parameters["Lookup"];
                return default(LookupAttributeMetadata);
            }
            set { Parameters["Lookup"] = value; }
        }
        public OneToManyRelationshipMetadata OneToManyRelationship
        {
            get
            {
                if (Parameters.Contains("OneToManyRelationship"))
                    return (OneToManyRelationshipMetadata)Parameters["OneToManyRelationship"];
                return default(OneToManyRelationshipMetadata);
            }
            set { Parameters["OneToManyRelationship"] = value; }
        }
        public string SolutionUniqueName
        {
            get
            {
                if (Parameters.Contains("SolutionUniqueName"))
                    return (string)Parameters["SolutionUniqueName"];
                return default(string);
            }
            set { Parameters["SolutionUniqueName"] = value; }
        }
        public CreateOneToManyRequest()
        {
            this.ResponseType = new CreateOneToManyResponse();
            this.RequestName = "CreateOneToMany";
        }
        internal override string GetRequestBody()
        {
            Parameters["Lookup"] = Lookup;
            Parameters["OneToManyRelationship"] = OneToManyRelationship;
            Parameters["SolutionUniqueName"] = SolutionUniqueName;
            return GetSoapBody();
        }
    }
    public sealed class CreateOneToManyResponse : OrganizationResponse
    {
        public Guid AttributeId { get; set; }
        public Guid RelationshipId { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "AttributeId")
                    this.AttributeId = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
                if (result.Element(Util.ns.b + "key").Value == "RelationshipId")
                    this.RelationshipId = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion CreateOneToMany

    #region CreateOptionSet
    public sealed class CreateOptionSetRequest : OrganizationRequest
    {
        public OptionSetMetadataBase OptionSet
        {
            get
            {
                if (Parameters.Contains("OptionSet"))
                    return (OptionSetMetadataBase)Parameters["OptionSet"];
                return default(OptionSetMetadataBase);
            }
            set { Parameters["OptionSet"] = value; }
        }
        public string SolutionUniqueName
        {
            get
            {
                if (Parameters.Contains("SolutionUniqueName"))
                    return (string)Parameters["SolutionUniqueName"];
                return default(string);
            }
            set { Parameters["SolutionUniqueName"] = value; }
        }
        public CreateOptionSetRequest()
        {
            this.ResponseType = new CreateOptionSetResponse();
            this.RequestName = "CreateOptionSet";
        }
        internal override string GetRequestBody()
        {
            Parameters["OptionSet"] = OptionSet;
            Parameters["SolutionUniqueName"] = SolutionUniqueName;
            return GetSoapBody();
        }
    }
    public sealed class CreateOptionSetResponse : OrganizationResponse
    {
        public Guid OptionSetId { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "OptionSetId")
                    this.OptionSetId = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion CreateOptionSet

    #region Delete
    public sealed class DeleteRequest : OrganizationRequest
    {
        public EntityReference Target
        {
            get
            {
                if (Parameters.Contains("Target"))
                    return (EntityReference)Parameters["Target"];
                return default(EntityReference);
            }
            set { Parameters["Target"] = value; }
        }
        public ConcurrencyBehavior ConcurrencyBehavior
        {
            get
            {
                if (Parameters.Contains("ConcurrencyBehavior"))
                    return (ConcurrencyBehavior)Parameters["ConcurrencyBehavior"];
                return default(ConcurrencyBehavior);
            }
            set { Parameters["ConcurrencyBehavior"] = value; }
        }
        public DeleteRequest()
        {
            this.ResponseType = new DeleteResponse();
            this.RequestName = "Delete";
        }
        internal override string GetRequestBody()
        {
            Parameters["Target"] = Target;
            Parameters["ConcurrencyBehavior"] = ConcurrencyBehavior;
            return GetSoapBody();
        }
    }
    public sealed class DeleteResponse : OrganizationResponse
    {
    }

    #endregion Delete

    #region DeleteAttribute
    public sealed class DeleteAttributeRequest : OrganizationRequest
    {
        public string EntityLogicalName
        {
            get
            {
                if (Parameters.Contains("EntityLogicalName"))
                    return (string)Parameters["EntityLogicalName"];
                return default(string);
            }
            set { Parameters["EntityLogicalName"] = value; }
        }
        public string LogicalName
        {
            get
            {
                if (Parameters.Contains("LogicalName"))
                    return (string)Parameters["LogicalName"];
                return default(string);
            }
            set { Parameters["LogicalName"] = value; }
        }
        public DeleteAttributeRequest()
        {
            this.ResponseType = new DeleteAttributeResponse();
            this.RequestName = "DeleteAttribute";
        }
        internal override string GetRequestBody()
        {
            Parameters["EntityLogicalName"] = EntityLogicalName;
            Parameters["LogicalName"] = LogicalName;
            return GetSoapBody();
        }
    }
    public sealed class DeleteAttributeResponse : OrganizationResponse
    {
    }

    #endregion DeleteAttribute

    #region DeleteEntity
    public sealed class DeleteEntityRequest : OrganizationRequest
    {
        public string LogicalName
        {
            get
            {
                if (Parameters.Contains("LogicalName"))
                    return (string)Parameters["LogicalName"];
                return default(string);
            }
            set { Parameters["LogicalName"] = value; }
        }
        public DeleteEntityRequest()
        {
            this.ResponseType = new DeleteEntityResponse();
            this.RequestName = "DeleteEntity";
        }
        internal override string GetRequestBody()
        {
            Parameters["LogicalName"] = LogicalName;
            return GetSoapBody();
        }
    }
    public sealed class DeleteEntityResponse : OrganizationResponse
    {
    }

    #endregion DeleteEntity

    #region DeleteEntityKey
    public sealed class DeleteEntityKeyRequest : OrganizationRequest
    {       
        public string EntityLogicalName
        {
            get
            {
                if (Parameters.Contains("EntityLogicalName"))
                    return (string)Parameters["EntityLogicalName"];
                return default(string);
            }
            set { Parameters["EntityLogicalName"] = value; }
        }
        public string Name
        {
            get
            {
                if (Parameters.Contains("Name"))
                    return (string)Parameters["Name"];
                return default(string);
            }
            set { Parameters["Name"] = value; }
        }
        public DeleteEntityKeyRequest()
        {
            this.ResponseType = new DeleteEntityKeyResponse();
            this.RequestName = "DeleteEntityKey";
        }
        internal override string GetRequestBody()
        {
            Parameters["EntityLogicalName"] = EntityLogicalName;
            Parameters["Name"] = Name;
            return GetSoapBody();
        }
    }
    public sealed class DeleteEntityKeyResponse : OrganizationResponse
    {
    }

    #endregion DeleteEntityKey

    #region DeleteOptionSet
    public sealed class DeleteOptionSetRequest : OrganizationRequest
    {
        public string Name
        {
            get
            {
                if (Parameters.Contains("Name"))
                    return (string)Parameters["Name"];
                return default(string);
            }
            set { Parameters["Name"] = value; }
        }
        public DeleteOptionSetRequest()
        {
            this.ResponseType = new DeleteOptionSetResponse();
            this.RequestName = "DeleteOptionSet";
        }
        internal override string GetRequestBody()
        {
            Parameters["Name"] = Name;
            return GetSoapBody();
        }
    }
    public sealed class DeleteOptionSetResponse : OrganizationResponse
    {
    }

    #endregion DeleteOptionSet

    #region DeleteOptionValue
    public sealed class DeleteOptionValueRequest : OrganizationRequest
    {
        public string AttributeLogicalName
        {
            get
            {
                if (Parameters.Contains("AttributeLogicalName"))
                    return (string)Parameters["AttributeLogicalName"];
                return default(string);
            }
            set { Parameters["AttributeLogicalName"] = value; }
        }
        public string EntityLogicalName
        {
            get
            {
                if (Parameters.Contains("EntityLogicalName"))
                    return (string)Parameters["EntityLogicalName"];
                return default(string);
            }
            set { Parameters["EntityLogicalName"] = value; }
        }
        public string OptionSetName
        {
            get
            {
                if (Parameters.Contains("OptionSetName"))
                    return (string)Parameters["OptionSetName"];
                return default(string);
            }
            set { Parameters["OptionSetName"] = value; }
        }
        public string SolutionUniqueName
        {
            get
            {
                if (Parameters.Contains("SolutionUniqueName"))
                    return (string)Parameters["SolutionUniqueName"];
                return default(string);
            }
            set { Parameters["SolutionUniqueName"] = value; }
        }
        public int Value
        {
            get
            {
                if (Parameters.Contains("Value"))
                    return (int)Parameters["Value"];
                return default(int);
            }
            set { Parameters["Value"] = value; }
        }
        public DeleteOptionValueRequest()
        {
            this.ResponseType = new DeleteOptionValueResponse();
            this.RequestName = "DeleteOptionValue";
        }
        internal override string GetRequestBody()
        {
            Parameters["AttributeLogicalName"] = AttributeLogicalName;
            Parameters["EntityLogicalName"] = EntityLogicalName;
            Parameters["OptionSetName"] = OptionSetName;
            Parameters["SolutionUniqueName"] = SolutionUniqueName;
            Parameters["Value"] = Value;
            return GetSoapBody();
        }
    }
    public sealed class DeleteOptionValueResponse : OrganizationResponse
    {
    }

    #endregion DeleteOptionValue

    #region DeleteRelationship
    public sealed class DeleteRelationshipRequest : OrganizationRequest
    {
        public string Name
        {
            get
            {
                if (Parameters.Contains("Name"))
                    return (string)Parameters["Name"];
                return default(string);
            }
            set { Parameters["Name"] = value; }
        }
        public DeleteRelationshipRequest()
        {
            this.ResponseType = new DeleteRelationshipResponse();
            this.RequestName = "DeleteRelationship";
        }
        internal override string GetRequestBody()
        {
            Parameters["Name"] = Name;
            return GetSoapBody();
        }
    }
    public sealed class DeleteRelationshipResponse : OrganizationResponse
    {
    }

    #endregion DeleteRelationship

    #region Disassociate
    public sealed class DisassociateRequest : OrganizationRequest
    {
        public EntityReference Target
        {
            get
            {
                if (Parameters.Contains("Target"))
                    return (EntityReference)Parameters["Target"];
                return default(EntityReference);
            }
            set { Parameters["Target"] = value; }
        }
        public EntityReferenceCollection RelatedEntities
        {
            get
            {
                if (Parameters.Contains("RelatedEntities"))
                    return (EntityReferenceCollection)Parameters["RelatedEntities"];
                return default(EntityReferenceCollection);
            }
            set { Parameters["RelatedEntities"] = value; }
        }
        public Relationship Relationship
        {
            get
            {
                if (Parameters.Contains("Relationship"))
                    return (Relationship)Parameters["Relationship"];
                return default(Relationship);
            }
            set { Parameters["Relationship"] = value; }
        }
        public DisassociateRequest()
        {
            this.ResponseType = new DisassociateResponse();
            this.RequestName = "Disassociate";
        }
        internal override string GetRequestBody()
        {
            Parameters["Target"] = Target;
            Parameters["RelatedEntities"] = RelatedEntities;
            Parameters["Relationship"] = Relationship;
            return GetSoapBody();
        }
    }
    public sealed class DisassociateResponse : OrganizationResponse
    {
    }

    #endregion Disassociate

    #region GetValidManyToMany
    public sealed class GetValidManyToManyRequest : OrganizationRequest
    {
        public GetValidManyToManyRequest()
        {
            this.ResponseType = new GetValidManyToManyResponse();
            this.RequestName = "GetValidManyToMany";
        }
        internal override string GetRequestBody()
        {
            return GetSoapBody();
        }
    }
    public sealed class GetValidManyToManyResponse : OrganizationResponse
    {
        public string[] EntityNames { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "EntityNames")
                {
                    List<string> list = new List<string>();
                    foreach (XElement item in result.Element(Util.ns.b + "value").Elements(Util.ns.f + "string"))
                    {
                        list.Add(item.Value);
                    }
                    //this.TimeInfos = EntityCollection.LoadFromXml(result.Element(Util.ns.b + "value"));
                    this.EntityNames = list.ToArray();
                }
            }
        }
    }

    #endregion GetValidManyToMany

    #region GetValidReferencedEntities
    public sealed class GetValidReferencedEntitiesRequest : OrganizationRequest
    {
        public string ReferencingEntityName
        {
            get
            {
                if (Parameters.Contains("ReferencingEntityName"))
                    return (string)Parameters["ReferencingEntityName"];
                return default(string);
            }
            set { Parameters["ReferencingEntityName"] = value; }
        }
        public GetValidReferencedEntitiesRequest()
        {
            this.ResponseType = new GetValidReferencedEntitiesResponse();
            this.RequestName = "GetValidReferencedEntities";
        }
        internal override string GetRequestBody()
        {
            Parameters["ReferencingEntityName"] = ReferencingEntityName;
            return GetSoapBody();
        }
    }
    public sealed class GetValidReferencedEntitiesResponse : OrganizationResponse
    {
        public string[] EntityNames { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "EntityNames")
                {
                    List<string> list = new List<string>();
                    foreach (XElement item in result.Element(Util.ns.b + "value").Elements(Util.ns.f + "string"))
                    {
                        list.Add(item.Value);
                    }
                    //this.TimeInfos = EntityCollection.LoadFromXml(result.Element(Util.ns.b + "value"));
                    this.EntityNames = list.ToArray();
                }
            }
        }
    }

    #endregion GetValidReferencedEntities

    #region GetValidReferencingEntities
    public sealed class GetValidReferencingEntitiesRequest : OrganizationRequest
    {
        public string ReferencedEntityName
        {
            get
            {
                if (Parameters.Contains("ReferencedEntityName"))
                    return (string)Parameters["ReferencedEntityName"];
                return default(string);
            }
            set { Parameters["ReferencedEntityName"] = value; }
        }
        public GetValidReferencingEntitiesRequest()
        {
            this.ResponseType = new GetValidReferencingEntitiesResponse();
            this.RequestName = "GetValidReferencingEntities";
        }
        internal override string GetRequestBody()
        {
            Parameters["ReferencedEntityName"] = ReferencedEntityName;
            return GetSoapBody();
        }
    }
    public sealed class GetValidReferencingEntitiesResponse : OrganizationResponse
    {
        public string[] EntityNames { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "EntityNames")
                {
                    List<string> list = new List<string>();
                    foreach (XElement item in result.Element(Util.ns.b + "value").Elements(Util.ns.f + "string"))
                    {
                        list.Add(item.Value);
                    }
                    //this.TimeInfos = EntityCollection.LoadFromXml(result.Element(Util.ns.b + "value"));
                    this.EntityNames = list.ToArray();
                }
            }
        }
    }

    #endregion GetValidReferencingEntities

    #region InsertOptionValue
    public sealed class InsertOptionValueRequest : OrganizationRequest
    {
        public string AttributeLogicalName
        {
            get
            {
                if (Parameters.Contains("AttributeLogicalName"))
                    return (string)Parameters["AttributeLogicalName"];
                return default(string);
            }
            set { Parameters["AttributeLogicalName"] = value; }
        }
        public Label Description
        {
            get
            {
                if (Parameters.Contains("Description"))
                    return (Label)Parameters["Description"];
                return default(Label);
            }
            set { Parameters["Description"] = value; }
        }
        public string EntityLogicalName
        {
            get
            {
                if (Parameters.Contains("EntityLogicalName"))
                    return (string)Parameters["EntityLogicalName"];
                return default(string);
            }
            set { Parameters["EntityLogicalName"] = value; }
        }
        public Label Label
        {
            get
            {
                if (Parameters.Contains("Label"))
                    return (Label)Parameters["Label"];
                return default(Label);
            }
            set { Parameters["Label"] = value; }
        }
        public string OptionSetName
        {
            get
            {
                if (Parameters.Contains("OptionSetName"))
                    return (string)Parameters["OptionSetName"];
                return default(string);
            }
            set { Parameters["OptionSetName"] = value; }
        }
        public string SolutionUniqueName
        {
            get
            {
                if (Parameters.Contains("SolutionUniqueName"))
                    return (string)Parameters["SolutionUniqueName"];
                return default(string);
            }
            set { Parameters["SolutionUniqueName"] = value; }
        }
        public int? Value
        {
            get
            {
                if (Parameters.Contains("Value"))
                    return (int?)Parameters["Value"];
                return default(int?);
            }
            set { Parameters["Value"] = value; }
        }
        public InsertOptionValueRequest()
        {
            this.ResponseType = new InsertOptionValueResponse();
            this.RequestName = "InsertOptionValue";
        }
        internal override string GetRequestBody()
        {
            Parameters["AttributeLogicalName"] = AttributeLogicalName;
            Parameters["Description"] = Description;
            Parameters["EntityLogicalName"] = EntityLogicalName;
            Parameters["Label"] = Label;
            Parameters["OptionSetName"] = OptionSetName;
            Parameters["SolutionUniqueName"] = SolutionUniqueName;
            Parameters["Value"] = Value;
            return GetSoapBody();
        }
    }
    public sealed class InsertOptionValueResponse : OrganizationResponse
    {
        public int NewOptionValue { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "NewOptionValue")
                    this.NewOptionValue = Util.LoadFromXml<int>(result.Element(Util.ns.b + "value")); 
            }
        }
    }

    #endregion InsertOptionValue

    #region InsertStatusValue
    public sealed class InsertStatusValueRequest : OrganizationRequest
    {
        public string AttributeLogicalName
        {
            get
            {
                if (Parameters.Contains("AttributeLogicalName"))
                    return (string)Parameters["AttributeLogicalName"];
                return default(string);
            }
            set { Parameters["AttributeLogicalName"] = value; }
        }
        public Label Description
        {
            get
            {
                if (Parameters.Contains("Description"))
                    return (Label)Parameters["Description"];
                return default(Label);
            }
            set { Parameters["Description"] = value; }
        }
        public string EntityLogicalName
        {
            get
            {
                if (Parameters.Contains("EntityLogicalName"))
                    return (string)Parameters["EntityLogicalName"];
                return default(string);
            }
            set { Parameters["EntityLogicalName"] = value; }
        }
        public Label Label
        {
            get
            {
                if (Parameters.Contains("Label"))
                    return (Label)Parameters["Label"];
                return default(Label);
            }
            set { Parameters["Label"] = value; }
        }
        public string OptionSetName
        {
            get
            {
                if (Parameters.Contains("OptionSetName"))
                    return (string)Parameters["OptionSetName"];
                return default(string);
            }
            set { Parameters["OptionSetName"] = value; }
        }
        public string SolutionUniqueName
        {
            get
            {
                if (Parameters.Contains("SolutionUniqueName"))
                    return (string)Parameters["SolutionUniqueName"];
                return default(string);
            }
            set { Parameters["SolutionUniqueName"] = value; }
        }
        public int StateCode
        {
            get
            {
                if (Parameters.Contains("StateCode"))
                    return (int)Parameters["StateCode"];
                return default(int);
            }
            set { Parameters["StateCode"] = value; }
        }
        public int? Value
        {
            get
            {
                if (Parameters.Contains("Value"))
                    return (int?)Parameters["Value"];
                return default(int?);
            }
            set { Parameters["Value"] = value; }
        }
        public InsertStatusValueRequest()
        {
            this.ResponseType = new InsertStatusValueResponse();
            this.RequestName = "InsertStatusValue";
        }
        internal override string GetRequestBody()
        {
            Parameters["AttributeLogicalName"] = AttributeLogicalName;
            Parameters["Description"] = Description;
            Parameters["EntityLogicalName"] = EntityLogicalName;
            Parameters["Label"] = Label;
            Parameters["OptionSetName"] = OptionSetName;
            Parameters["SolutionUniqueName"] = SolutionUniqueName;
            Parameters["StateCode"] = StateCode;
            Parameters["Value"] = Value;
            return GetSoapBody();
        }
    }
    public sealed class InsertStatusValueResponse : OrganizationResponse
    {
        public int NewOptionValue { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "NewOptionValue")
                    this.NewOptionValue = Util.LoadFromXml<int>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion InsertStatusValue

    #region IsDataEncryptionActive
    public sealed class IsDataEncryptionActiveRequest : OrganizationRequest
    {
        public IsDataEncryptionActiveRequest()
        {
            this.ResponseType = new IsDataEncryptionActiveResponse();
            this.RequestName = "IsDataEncryptionActive";
        }
        internal override string GetRequestBody()
        {
            return GetSoapBody();
        }
    }
    public sealed class IsDataEncryptionActiveResponse : OrganizationResponse
    {
        public bool IsActive { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "IsActive")
                    IsActive = (bool)(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion IsDataEncryptionActive

    #region OrderOption
    public sealed class OrderOptionRequest : OrganizationRequest
    {
        public string AttributeLogicalName
        {
            get
            {
                if (Parameters.Contains("AttributeLogicalName"))
                    return (string)Parameters["AttributeLogicalName"];
                return default(string);
            }
            set { Parameters["AttributeLogicalName"] = value; }
        }
        public string EntityLogicalName
        {
            get
            {
                if (Parameters.Contains("EntityLogicalName"))
                    return (string)Parameters["EntityLogicalName"];
                return default(string);
            }
            set { Parameters["EntityLogicalName"] = value; }
        }
        public string OptionSetName
        {
            get
            {
                if (Parameters.Contains("OptionSetName"))
                    return (string)Parameters["OptionSetName"];
                return default(string);
            }
            set { Parameters["OptionSetName"] = value; }
        }
        public string SolutionUniqueName
        {
            get
            {
                if (Parameters.Contains("SolutionUniqueName"))
                    return (string)Parameters["SolutionUniqueName"];
                return default(string);
            }
            set { Parameters["SolutionUniqueName"] = value; }
        }
        public int[] Values
        {
            get
            {
                if (Parameters.Contains("Values"))
                    return (int[])Parameters["Values"];
                return default(int[]);
            }
            set { Parameters["Values"] = value; }
        }
        public OrderOptionRequest()
        {
            this.ResponseType = new OrderOptionResponse();
            this.RequestName = "OrderOption";
        }
        internal override string GetRequestBody()
        {
            Parameters["AttributeLogicalName"] = AttributeLogicalName;
            Parameters["EntityLogicalName"] = EntityLogicalName;
            Parameters["OptionSetName"] = OptionSetName;
            Parameters["SolutionUniqueName"] = SolutionUniqueName;
            Parameters["Values"] = Values;
            return GetSoapBody();
        }
    }
    public sealed class OrderOptionResponse : OrganizationResponse
    {
    }

    #endregion OrderOption

    #region ReactivateEntityKey
    public sealed class ReactivateEntityKeyRequest : OrganizationRequest
    {
        public string EntityKeyLogicalName 
        {
            get
            {
                if (Parameters.Contains("EntityKeyLogicalName"))
                    return (string)Parameters["EntityKeyLogicalName"];
                return default(string);
            }
            set { Parameters["EntityKeyLogicalName"] = value; }
        }
        public string EntityLogicalName 
        {
            get
            {
                if (Parameters.Contains("EntityLogicalName"))
                    return (string)Parameters["EntityLogicalName"];
                return default(string);
            }
            set { Parameters["EntityLogicalName"] = value; }
        }       
        public ReactivateEntityKeyRequest()
        {
            this.ResponseType = new ReactivateEntityKeyResponse();
            this.RequestName = "ReactivateEntityKey";
        }
        internal override string GetRequestBody()
        {
            Parameters["EntityKeyLogicalName"] = EntityKeyLogicalName;
            Parameters["EntityLogicalName"] = EntityLogicalName;
            return GetSoapBody();
        }
    }
    public sealed class ReactivateEntityKeyResponse : OrganizationResponse
    {
    }

    #endregion ReactivateEntityKey

    #region Retrieve
    public sealed class RetrieveRequest : OrganizationRequest
    {
        public ColumnSet ColumnSet
        {
            get
            {
                if (Parameters.Contains("ColumnSet"))
                    return (ColumnSet)Parameters["ColumnSet"];
                return default(ColumnSet);
            }
            set { Parameters["ColumnSet"] = value; }
        }
        public EntityReference Target
        {
            get
            {
                if (Parameters.Contains("Target"))
                    return (EntityReference)Parameters["Target"];
                return default(EntityReference);
            }
            set { Parameters["Target"] = value; }
        }
        public RetrieveRequest()
        {
            this.ResponseType = new RetrieveResponse();
            this.RequestName = "Retrieve";
        }
        internal override string GetRequestBody()
        {
            Parameters["ColumnSet"] = ColumnSet;
            Parameters["Target"] = Target;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveResponse : OrganizationResponse
    {
        public Entity Entity { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "Entity")
                    Entity = Entity.LoadFromXml(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion Retrieve

    #region RetrieveAllEntities
    public sealed class RetrieveAllEntitiesRequest : OrganizationRequest
    {
        public EntityFilters EntityFilters
        {
            get
            {
                if (Parameters.Contains("EntityFilters"))
                    return (EntityFilters)Parameters["EntityFilters"];
                return default(EntityFilters);
            }
            set { Parameters["EntityFilters"] = value; }
        }
        public bool RetrieveAsIfPublished
        {
            get
            {
                if (Parameters.Contains("RetrieveAsIfPublished"))
                    return (bool)Parameters["RetrieveAsIfPublished"];
                return default(bool);
            }
            set { Parameters["RetrieveAsIfPublished"] = value; }
        }
        public RetrieveAllEntitiesRequest()
        {
            this.ResponseType = new RetrieveAllEntitiesResponse();
            this.RequestName = "RetrieveAllEntities";
        }
        internal override string GetRequestBody()
        {
            Parameters["EntityFilters"] = EntityFilters;
            Parameters["RetrieveAsIfPublished"] = RetrieveAsIfPublished;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveAllEntitiesResponse : OrganizationResponse
    {
        public EntityMetadata[] EntityMetadata { get; set; }
        public string Timestamp { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "EntityMetadata")
                {
                    List<EntityMetadata> list = new List<EntityMetadata>();
                    foreach (var item in result.Element(Util.ns.b + "value").Elements(Util.ns.h + "EntityMetadata"))
                    {
                        list.Add(Microsoft.Xrm.Sdk.Metadata.Samples.EntityMetadata.LoadFromXml(item));
                    }
                    EntityMetadata = list.ToArray();
                }
                if (result.Element(Util.ns.b + "key").Value == "Timestamp")
                    this.Timestamp = result.Element(Util.ns.b + "value").Value;
            }
        }
    }

    #endregion RetrieveAllEntities

    #region RetrieveAllManagedProperties
    public sealed class RetrieveAllManagedPropertiesRequest : OrganizationRequest
    {
        public RetrieveAllManagedPropertiesRequest()
        {
            this.ResponseType = new RetrieveAllManagedPropertiesResponse();
            this.RequestName = "RetrieveAllManagedProperties";
        }
        internal override string GetRequestBody()
        {
            return GetSoapBody();
        }
    }
    public sealed class RetrieveAllManagedPropertiesResponse : OrganizationResponse
    {
        public ManagedPropertyMetadata[] ManagedPropertyMetadata { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "ManagedPropertyMetadata")
                {
                    List<ManagedPropertyMetadata> list = new List<ManagedPropertyMetadata>();
                    foreach (var item in result.Element(Util.ns.b + "value").Elements(Util.ns.h + "ManagedPropertyMetadata"))
                    {
                        list.Add(Microsoft.Xrm.Sdk.Metadata.Samples.ManagedPropertyMetadata.LoadFromXml(item));
                    }
                    ManagedPropertyMetadata = list.ToArray();
                }
            }
        }
    }

    #endregion RetrieveAllManagedProperties

    #region RetrieveAllOptionSets
    public sealed class RetrieveAllOptionSetsRequest : OrganizationRequest
    {
        public bool RetrieveAsIfPublished
        {
            get
            {
                if (Parameters.Contains("RetrieveAsIfPublished"))
                    return (bool)Parameters["RetrieveAsIfPublished"];
                return default(bool);
            }
            set { Parameters["RetrieveAsIfPublished"] = value; }
        }
        public RetrieveAllOptionSetsRequest()
        {
            this.ResponseType = new RetrieveAllOptionSetsResponse();
            this.RequestName = "RetrieveAllOptionSets";
        }
        internal override string GetRequestBody()
        {
            Parameters["RetrieveAsIfPublished"] = RetrieveAsIfPublished;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveAllOptionSetsResponse : OrganizationResponse
    {
        public OptionSetMetadataBase[] OptionSetMetadata { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "OptionSetMetadata")
                {
                    List<OptionSetMetadataBase> list = new List<OptionSetMetadataBase>();
                    foreach (var item in result.Element(Util.ns.b + "value").Elements(Util.ns.h + "OptionSetMetadataBase"))
                    {
                        list.Add(OptionSetMetadataBase.LoadFromXml(item));
                    }
                    OptionSetMetadata = list.ToArray();
                }
            }
        }
    }

    #endregion RetrieveAllOptionSets

    #region RetrieveAttribute
    public sealed class RetrieveAttributeRequest : OrganizationRequest
    {
        public int ColumnNumber
        {
            get
            {
                if (Parameters.Contains("ColumnNumber"))
                    return (int)Parameters["ColumnNumber"];
                return default(int);
            }
            set { Parameters["ColumnNumber"] = value; }
        }
        public string EntityLogicalName
        {
            get
            {
                if (Parameters.Contains("EntityLogicalName"))
                    return (string)Parameters["EntityLogicalName"];
                return default(string);
            }
            set { Parameters["EntityLogicalName"] = value; }
        }
        public string LogicalName
        {
            get
            {
                if (Parameters.Contains("LogicalName"))
                    return (string)Parameters["LogicalName"];
                return default(string);
            }
            set { Parameters["LogicalName"] = value; }
        }
        public Guid MetadataId
        {
            get
            {
                if (Parameters.Contains("MetadataId"))
                    return (Guid)Parameters["MetadataId"];
                return default(Guid);
            }
            set { Parameters["MetadataId"] = value; }
        }
        public bool RetrieveAsIfPublished
        {
            get
            {
                if (Parameters.Contains("RetrieveAsIfPublished"))
                    return (bool)Parameters["RetrieveAsIfPublished"];
                return default(bool);
            }
            set { Parameters["RetrieveAsIfPublished"] = value; }
        }
        public RetrieveAttributeRequest()
        {
            this.ResponseType = new RetrieveAttributeResponse();
            this.RequestName = "RetrieveAttribute";
        }
        internal override string GetRequestBody()
        {
            Parameters["ColumnNumber"] = ColumnNumber;
            Parameters["EntityLogicalName"] = EntityLogicalName;
            Parameters["LogicalName"] = LogicalName;
            Parameters["MetadataId"] = MetadataId;
            Parameters["RetrieveAsIfPublished"] = RetrieveAsIfPublished;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveAttributeResponse : OrganizationResponse
    {
        public AttributeMetadata AttributeMetadata { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "AttributeMetadata")
                    this.AttributeMetadata = AttributeMetadata.LoadFromXml(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion RetrieveAttribute

    #region RetrieveDataEncryptionKey
    public sealed class RetrieveDataEncryptionKeyRequest : OrganizationRequest
    {
        public RetrieveDataEncryptionKeyRequest()
        {
            this.ResponseType = new RetrieveDataEncryptionKeyResponse();
            this.RequestName = "RetrieveDataEncryptionKey";
        }
        internal override string GetRequestBody()
        {
            return GetSoapBody();
        }
    }
    public sealed class RetrieveDataEncryptionKeyResponse : OrganizationResponse
    {
        public string EncryptionKey { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "EncryptionKey")
                    this.EncryptionKey = result.Element(Util.ns.b + "value").Value;
            }
        }
    }

    #endregion RetrieveDataEncryptionKey

    #region RetrieveEntity
    public sealed class RetrieveEntityRequest : OrganizationRequest
    {
        public EntityFilters EntityFilters
        {
            get
            {
                if (Parameters.Contains("EntityFilters"))
                    return (EntityFilters)Parameters["EntityFilters"];
                return default(EntityFilters);
            }
            set { Parameters["EntityFilters"] = value; }
        }
        public string LogicalName
        {
            get
            {
                if (Parameters.Contains("LogicalName"))
                    return (string)Parameters["LogicalName"];
                return default(string);
            }
            set { Parameters["LogicalName"] = value; }
        }
        public Guid MetadataId
        {
            get
            {
                if (Parameters.Contains("MetadataId"))
                    return (Guid)Parameters["MetadataId"];
                return default(Guid);
            }
            set { Parameters["MetadataId"] = value; }
        }
        public bool RetrieveAsIfPublished
        {
            get
            {
                if (Parameters.Contains("RetrieveAsIfPublished"))
                    return (bool)Parameters["RetrieveAsIfPublished"];
                return default(bool);
            }
            set { Parameters["RetrieveAsIfPublished"] = value; }
        }
        public RetrieveEntityRequest()
        {
            this.ResponseType = new RetrieveEntityResponse();
            this.RequestName = "RetrieveEntity";
        }
        internal override string GetRequestBody()
        {
            Parameters["EntityFilters"] = EntityFilters;
            Parameters["LogicalName"] = LogicalName;
            Parameters["MetadataId"] = MetadataId;
            Parameters["RetrieveAsIfPublished"] = RetrieveAsIfPublished;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveEntityResponse : OrganizationResponse
    {
        public EntityMetadata EntityMetadata { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "EntityMetadata")
                    this.EntityMetadata = EntityMetadata.LoadFromXml(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion RetrieveEntity

    #region RetrieveEntityChanges
    public sealed class RetrieveEntityChangesRequest : OrganizationRequest
    {
        public ColumnSet Columns 
        {
            get
            {
                if (Parameters.Contains("Columns"))
                    return (ColumnSet)Parameters["Columns"];
                return default(ColumnSet);
            }
            set { Parameters["Columns"] = value; }
        }
        public string DataVersion
        {
            get
            {
                if (Parameters.Contains("DataVersion"))
                    return (string)Parameters["DataVersion"];
                return default(string);
            }
            set { Parameters["DataVersion"] = value; }
        }
        public string EntityName
        {
            get
            {
                if (Parameters.Contains("EntityName"))
                    return (string)Parameters["EntityName"];
                return default(string);
            }
            set { Parameters["EntityName"] = value; }
        }
        public PagingInfo PageInfo 
        {
            get
            {
                if (Parameters.Contains("PageInfo"))
                    return (PagingInfo)Parameters["PageInfo"];
                return default(PagingInfo);
            }
            set { Parameters["PageInfo"] = value; }
        }
        public RetrieveEntityChangesRequest()
        {
            this.ResponseType = new RetrieveEntityChangesResponse();
            this.RequestName = "RetrieveEntityChanges";
        }
        internal override string GetRequestBody()
        {
            Parameters["Columns"] = Columns;
            Parameters["DataVersion"] = DataVersion;
            Parameters["EntityName"] = EntityName;
            Parameters["PageInfo"] = PageInfo;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveEntityChangesResponse : OrganizationResponse
    {
        public BusinessEntityChanges EntityChanges { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "EntityChanges")
                    this.EntityChanges = BusinessEntityChanges.LoadFromXml(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion RetrieveEntityChanges

    #region RetrieveEntityKey
    public sealed class RetrieveEntityKeyRequest : OrganizationRequest
    {
        public string EntityLogicalName 
        {
            get
            {
                if (Parameters.Contains("EntityLogicalName"))
                    return (string)Parameters["EntityLogicalName"];
                return default(string);
            }
            set { Parameters["EntityLogicalName"] = value; }
        }
        public string LogicalName
        {
            get
            {
                if (Parameters.Contains("LogicalName"))
                    return (string)Parameters["LogicalName"];
                return default(string);
            }
            set { Parameters["LogicalName"] = value; }
        }
        public Guid MetadataId 
        {
            get
            {
                if (Parameters.Contains("MetadataId"))
                    return (Guid)Parameters["MetadataId"];
                return default(Guid);
            }
            set { Parameters["MetadataId"] = value; }
        }
        public bool RetrieveAsIfPublished 
        {
            get
            {
                if (Parameters.Contains("RetrieveAsIfPublished"))
                    return (bool)Parameters["RetrieveAsIfPublished"];
                return default(bool);
            }
            set { Parameters["RetrieveAsIfPublished"] = value; }
        }
        public RetrieveEntityKeyRequest()
        {
            this.ResponseType = new RetrieveEntityKeyResponse();
            this.RequestName = "RetrieveEntityKey";
        }
        internal override string GetRequestBody()
        {
            Parameters["EntityLogicalName"] = EntityLogicalName;
            Parameters["LogicalName"] = LogicalName;
            Parameters["MetadataId"] = MetadataId;
            Parameters["RetrieveAsIfPublished"] = RetrieveAsIfPublished;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveEntityKeyResponse : OrganizationResponse
    {
        public EntityKeyMetadata EntityKeyMetadata { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "EntityKeyMetadata")
                    this.EntityKeyMetadata = EntityKeyMetadata.LoadFromXml(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion RetrieveEntityKey

    #region RetrieveManagedProperty
    public sealed class RetrieveManagedPropertyRequest : OrganizationRequest
    {
        public string LogicalName
        {
            get
            {
                if (Parameters.Contains("LogicalName"))
                    return (string)Parameters["LogicalName"];
                return default(string);
            }
            set { Parameters["LogicalName"] = value; }
        }
        public Guid MetadataId
        {
            get
            {
                if (Parameters.Contains("MetadataId"))
                    return (Guid)Parameters["MetadataId"];
                return default(Guid);
            }
            set { Parameters["MetadataId"] = value; }
        }
        public RetrieveManagedPropertyRequest()
        {
            this.ResponseType = new RetrieveManagedPropertyResponse();
            this.RequestName = "RetrieveManagedProperty";
        }
        internal override string GetRequestBody()
        {
            Parameters["LogicalName"] = LogicalName;
            Parameters["MetadataId"] = MetadataId;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveManagedPropertyResponse : OrganizationResponse
    {
        public ManagedPropertyMetadata ManagedPropertyMetadata { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "ManagedPropertyMetadata")
                    this.ManagedPropertyMetadata = ManagedPropertyMetadata.LoadFromXml(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion RetrieveManagedProperty

    #region RetrieveMetadataChanges
    public sealed class RetrieveMetadataChangesRequest : OrganizationRequest
    {
        public string ClientVersionStamp
        {
            get
            {
                if (Parameters.Contains("ClientVersionStamp"))
                    return (string)Parameters["ClientVersionStamp"];
                return default(string);
            }
            set { Parameters["ClientVersionStamp"] = value; }
        }
        public DeletedMetadataFilters DeletedMetadataFilters
        {
            get
            {
                if (Parameters.Contains("DeletedMetadataFilters"))
                    return (DeletedMetadataFilters)Parameters["DeletedMetadataFilters"];
                return default(DeletedMetadataFilters);
            }
            set { Parameters["DeletedMetadataFilters"] = value; }
        }
        public EntityQueryExpression Query
        {
            get
            {
                if (Parameters.Contains("Query"))
                    return (EntityQueryExpression)Parameters["Query"];
                return default(EntityQueryExpression);
            }
            set { Parameters["Query"] = value; }
        }
        public RetrieveMetadataChangesRequest()
        {
            this.ResponseType = new RetrieveMetadataChangesResponse();
            this.RequestName = "RetrieveMetadataChanges";
        }
        internal override string GetRequestBody()
        {
            Parameters["ClientVersionStamp"] = ClientVersionStamp;
            Parameters["DeletedMetadataFilters"] = DeletedMetadataFilters;
            Parameters["Query"] = Query;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveMetadataChangesResponse : OrganizationResponse
    {
        public DeletedMetadataCollection DeletedMetadata { get; set; }
        public EntityMetadataCollection EntityMetadata { get; set; }
        public string ServerVersionStamp { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "DeletedMetadata")
                    this.DeletedMetadata = DeletedMetadataCollection.LoadFromXml(result.Element(Util.ns.b + "value"));
                if (result.Element(Util.ns.b + "key").Value == "EntityMetadata")
                    this.EntityMetadata = EntityMetadataCollection.LoadFromXml(result.Element(Util.ns.b + "value"));
                if (result.Element(Util.ns.b + "key").Value == "ServerVersionStamp")
                    this.ServerVersionStamp = result.Element(Util.ns.b + "value").Value;
            }
        }
    }

    #endregion RetrieveMetadataChanges

    #region RetrieveMultiple
    public sealed class RetrieveMultipleRequest : OrganizationRequest
    {
        public QueryBase Query
        {
            get
            {
                if (Parameters.Contains("Query"))
                    return (QueryBase)Parameters["Query"];
                return default(QueryBase);
            }
            set { Parameters["Query"] = value; }
        }
        public RetrieveMultipleRequest()
        {
            this.ResponseType = new RetrieveMultipleResponse();
            this.RequestName = "RetrieveMultiple";
        }
        internal override string GetRequestBody()
        {
            Parameters["Query"] = Query;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveMultipleResponse : OrganizationResponse
    {
        public EntityCollection EntityCollection { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "EntityCollection")
                    this.EntityCollection = EntityCollection.LoadFromXml(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion RetrieveMultiple

    #region RetrieveOptionSet
    public sealed class RetrieveOptionSetRequest : OrganizationRequest
    {
        public Guid MetadataId
        {
            get
            {
                if (Parameters.Contains("MetadataId"))
                    return (Guid)Parameters["MetadataId"];
                return default(Guid);
            }
            set { Parameters["MetadataId"] = value; }
        }
        public string Name
        {
            get
            {
                if (Parameters.Contains("Name"))
                    return (string)Parameters["Name"];
                return default(string);
            }
            set { Parameters["Name"] = value; }
        }
        public bool RetrieveAsIfPublished
        {
            get
            {
                if (Parameters.Contains("RetrieveAsIfPublished"))
                    return (bool)Parameters["RetrieveAsIfPublished"];
                return default(bool);
            }
            set { Parameters["RetrieveAsIfPublished"] = value; }
        }
        public RetrieveOptionSetRequest()
        {
            this.ResponseType = new RetrieveOptionSetResponse();
            this.RequestName = "RetrieveOptionSet";
        }
        internal override string GetRequestBody()
        {
            Parameters["MetadataId"] = MetadataId;
            Parameters["Name"] = Name;
            Parameters["RetrieveAsIfPublished"] = RetrieveAsIfPublished;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveOptionSetResponse : OrganizationResponse
    {
        public OptionSetMetadataBase OptionSetMetadata { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "OptionSetMetadata")
                    this.OptionSetMetadata = OptionSetMetadataBase.LoadFromXml(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion RetrieveOptionSet

    #region RetrieveRelationship
    public sealed class RetrieveRelationshipRequest : OrganizationRequest
    {
        public Guid MetadataId
        {
            get
            {
                if (Parameters.Contains("MetadataId"))
                    return (Guid)Parameters["MetadataId"];
                return default(Guid);
            }
            set { Parameters["MetadataId"] = value; }
        }
        public string Name
        {
            get
            {
                if (Parameters.Contains("Name"))
                    return (string)Parameters["Name"];
                return default(string);
            }
            set { Parameters["Name"] = value; }
        }
        public bool RetrieveAsIfPublished
        {
            get
            {
                if (Parameters.Contains("RetrieveAsIfPublished"))
                    return (bool)Parameters["RetrieveAsIfPublished"];
                return default(bool);
            }
            set { Parameters["RetrieveAsIfPublished"] = value; }
        }
        public RetrieveRelationshipRequest()
        {
            this.ResponseType = new RetrieveRelationshipResponse();
            this.RequestName = "RetrieveRelationship";
        }
        internal override string GetRequestBody()
        {
            Parameters["MetadataId"] = MetadataId;
            Parameters["Name"] = Name;
            Parameters["RetrieveAsIfPublished"] = RetrieveAsIfPublished;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveRelationshipResponse : OrganizationResponse
    {
        public RelationshipMetadataBase RelationshipMetadata { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "RelationshipMetadata")
                    this.RelationshipMetadata = RelationshipMetadataBase.LoadFromXml(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion RetrieveRelationship

    #region RetrieveTimestamp
    public sealed class RetrieveTimestampRequest : OrganizationRequest
    {
        public RetrieveTimestampRequest()
        {
            this.ResponseType = new RetrieveTimestampResponse();
            this.RequestName = "RetrieveTimestamp";
        }
        internal override string GetRequestBody()
        {
            return GetSoapBody();
        }
    }
    public sealed class RetrieveTimestampResponse : OrganizationResponse
    {
        public string Timestamp { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "Timestamp")
                    this.Timestamp = result.Element(Util.ns.b + "value").Value;
            }
        }
    }

    #endregion RetrieveTimestamp

    #region SetDataEncryptionKey
    public sealed class SetDataEncryptionKeyRequest : OrganizationRequest
    {
        public string EncryptionKey
        {
            get
            {
                if (Parameters.Contains("EncryptionKey"))
                    return (string)Parameters["EncryptionKey"];
                return default(string);
            }
            set { Parameters["EncryptionKey"] = value; }
        }
        public bool ChangeEncryptionKey
        {
            get
            {
                if (Parameters.Contains("ChangeEncryptionKey"))
                    return (bool)Parameters["ChangeEncryptionKey"];
                return default(bool);
            }
            set { Parameters["ChangeEncryptionKey"] = value; }
        }
        public SetDataEncryptionKeyRequest()
        {
            this.ResponseType = new SetDataEncryptionKeyResponse();
            this.RequestName = "SetDataEncryptionKey";
        }
        internal override string GetRequestBody()
        {
            Parameters["EncryptionKey"] = EncryptionKey;
            Parameters["ChangeEncryptionKey"] = ChangeEncryptionKey;
            return GetSoapBody();
        }
    }
    public sealed class SetDataEncryptionKeyResponse : OrganizationResponse
    {
    }

    #endregion SetDataEncryptionKey

    #region Update
    public sealed class UpdateRequest : OrganizationRequest
    {
        public Entity Target
        {
            get
            {
                if (Parameters.Contains("Target"))
                    return (Entity)Parameters["Target"];
                return default(Entity);
            }
            set { Parameters["Target"] = value; }
        }
        public ConcurrencyBehavior ConcurrencyBehavior
        {
            get
            {
                if (Parameters.Contains("ConcurrencyBehavior"))
                    return (ConcurrencyBehavior)Parameters["ConcurrencyBehavior"];
                return default(ConcurrencyBehavior);
            }
            set { Parameters["ConcurrencyBehavior"] = value; }
        }
        public UpdateRequest()
        {
            this.ResponseType = new UpdateResponse();
            this.RequestName = "Update";
        }
        internal override string GetRequestBody()
        {
            Parameters["Target"] = Target;
            Parameters["ConcurrencyBehavior"] = ConcurrencyBehavior;
            return GetSoapBody();
        }
    }
    public sealed class UpdateResponse : OrganizationResponse
    {
    }

    #endregion Update

    #region UpdateAttribute
    public sealed class UpdateAttributeRequest : OrganizationRequest
    {
        public AttributeMetadata Attribute
        {
            get
            {
                if (Parameters.Contains("Attribute"))
                    return (AttributeMetadata)Parameters["Attribute"];
                return default(AttributeMetadata);
            }
            set { Parameters["Attribute"] = value; }
        }
        public string EntityName
        {
            get
            {
                if (Parameters.Contains("EntityName"))
                    return (string)Parameters["EntityName"];
                return default(string);
            }
            set { Parameters["EntityName"] = value; }
        }
        public bool MergeLabels
        {
            get
            {
                if (Parameters.Contains("MergeLabels"))
                    return (bool)Parameters["MergeLabels"];
                return default(bool);
            }
            set { Parameters["MergeLabels"] = value; }
        }
        public string SolutionUniqueName
        {
            get
            {
                if (Parameters.Contains("SolutionUniqueName"))
                    return (string)Parameters["SolutionUniqueName"];
                return default(string);
            }
            set { Parameters["SolutionUniqueName"] = value; }
        }
        public UpdateAttributeRequest()
        {
            this.ResponseType = new UpdateAttributeResponse();
            this.RequestName = "UpdateAttribute";
        }
        internal override string GetRequestBody()
        {
            Parameters["Attribute"] = Attribute;
            Parameters["EntityName"] = EntityName;
            Parameters["MergeLabels"] = MergeLabels;
            Parameters["SolutionUniqueName"] = SolutionUniqueName;
            return GetSoapBody();
        }
    }
    public sealed class UpdateAttributeResponse : OrganizationResponse
    {
    }

    #endregion UpdateAttribute

    #region UpdateEntity
    public sealed class UpdateEntityRequest : OrganizationRequest
    {
        public EntityMetadata Entity
        {
            get
            {
                if (Parameters.Contains("Entity"))
                    return (EntityMetadata)Parameters["Entity"];
                return default(EntityMetadata);
            }
            set { Parameters["Entity"] = value; }
        }
        public bool? HasActivities
        {
            get
            {
                if (Parameters.Contains("HasActivities"))
                    return (bool?)Parameters["HasActivities"];
                return default(bool?);
            }
            set { Parameters["HasActivities"] = value; }
        }
        public bool? HasNotes
        {
            get
            {
                if (Parameters.Contains("HasNotes"))
                    return (bool?)Parameters["HasNotes"];
                return default(bool?);
            }
            set { Parameters["HasNotes"] = value; }
        }
        public bool MergeLabels
        {
            get
            {
                if (Parameters.Contains("MergeLabels"))
                    return (bool)Parameters["MergeLabels"];
                return default(bool);
            }
            set { Parameters["MergeLabels"] = value; }
        }
        public string SolutionUniqueName
        {
            get
            {
                if (Parameters.Contains("SolutionUniqueName"))
                    return (string)Parameters["SolutionUniqueName"];
                return default(string);
            }
            set { Parameters["SolutionUniqueName"] = value; }
        }
        public UpdateEntityRequest()
        {
            this.ResponseType = new UpdateEntityResponse();
            this.RequestName = "UpdateEntity";
        }
        internal override string GetRequestBody()
        {
            Parameters["Entity"] = Entity;
            Parameters["HasActivities"] = HasActivities;
            Parameters["HasNotes"] = HasNotes;
            Parameters["MergeLabels"] = MergeLabels;
            Parameters["SolutionUniqueName"] = SolutionUniqueName;
            return GetSoapBody();
        }
    }
    public sealed class UpdateEntityResponse : OrganizationResponse
    {
    }

    #endregion UpdateEntity

    #region UpdateOptionSet
    public sealed class UpdateOptionSetRequest : OrganizationRequest
    {
        public bool MergeLabels
        {
            get
            {
                if (Parameters.Contains("MergeLabels"))
                    return (bool)Parameters["MergeLabels"];
                return default(bool);
            }
            set { Parameters["MergeLabels"] = value; }
        }
        public OptionSetMetadataBase OptionSet
        {
            get
            {
                if (Parameters.Contains("OptionSet"))
                    return (OptionSetMetadataBase)Parameters["OptionSet"];
                return default(OptionSetMetadataBase);
            }
            set { Parameters["OptionSet"] = value; }
        }
        public string SolutionUniqueName
        {
            get
            {
                if (Parameters.Contains("SolutionUniqueName"))
                    return (string)Parameters["SolutionUniqueName"];
                return default(string);
            }
            set { Parameters["SolutionUniqueName"] = value; }
        }
        public UpdateOptionSetRequest()
        {
            this.ResponseType = new UpdateOptionSetResponse();
            this.RequestName = "UpdateOptionSet";
        }
        internal override string GetRequestBody()
        {
            Parameters["MergeLabels"] = MergeLabels;
            Parameters["OptionSet"] = OptionSet;
            Parameters["SolutionUniqueName"] = SolutionUniqueName;
            return GetSoapBody();
        }
    }
    public sealed class UpdateOptionSetResponse : OrganizationResponse
    {
    }

    #endregion UpdateOptionSet

    #region UpdateOptionValue
    public sealed class UpdateOptionValueRequest : OrganizationRequest
    {
        public string AttributeLogicalName
        {
            get
            {
                if (Parameters.Contains("AttributeLogicalName"))
                    return (string)Parameters["AttributeLogicalName"];
                return default(string);
            }
            set { Parameters["AttributeLogicalName"] = value; }
        }
        public Label Description
        {
            get
            {
                if (Parameters.Contains("Description"))
                    return (Label)Parameters["Description"];
                return default(Label);
            }
            set { Parameters["Description"] = value; }
        }
        public string EntityLogicalName
        {
            get
            {
                if (Parameters.Contains("EntityLogicalName"))
                    return (string)Parameters["EntityLogicalName"];
                return default(string);
            }
            set { Parameters["EntityLogicalName"] = value; }
        }
        public Label Label
        {
            get
            {
                if (Parameters.Contains("Label"))
                    return (Label)Parameters["Label"];
                return default(Label);
            }
            set { Parameters["Label"] = value; }
        }
        public bool MergeLabels
        {
            get
            {
                if (Parameters.Contains("MergeLabels"))
                    return (bool)Parameters["MergeLabels"];
                return default(bool);
            }
            set { Parameters["MergeLabels"] = value; }
        }
        public string OptionSetName
        {
            get
            {
                if (Parameters.Contains("OptionSetName"))
                    return (string)Parameters["OptionSetName"];
                return default(string);
            }
            set { Parameters["OptionSetName"] = value; }
        }
        public string SolutionUniqueName
        {
            get
            {
                if (Parameters.Contains("SolutionUniqueName"))
                    return (string)Parameters["SolutionUniqueName"];
                return default(string);
            }
            set { Parameters["SolutionUniqueName"] = value; }
        }
        public int Value
        {
            get
            {
                if (Parameters.Contains("Value"))
                    return (int)Parameters["Value"];
                return default(int);
            }
            set { Parameters["Value"] = value; }
        }
        public UpdateOptionValueRequest()
        {
            this.ResponseType = new UpdateOptionValueResponse();
            this.RequestName = "UpdateOptionValue";
        }
        internal override string GetRequestBody()
        {
            Parameters["AttributeLogicalName"] = AttributeLogicalName;
            Parameters["Description"] = Description;
            Parameters["EntityLogicalName"] = EntityLogicalName;
            Parameters["Label"] = Label;
            Parameters["MergeLabels"] = MergeLabels;
            Parameters["OptionSetName"] = OptionSetName;
            Parameters["SolutionUniqueName"] = SolutionUniqueName;
            Parameters["Value"] = Value;
            return GetSoapBody();
        }
    }
    public sealed class UpdateOptionValueResponse : OrganizationResponse
    {
    }

    #endregion UpdateOptionValue

    #region UpdateRelationship
    public sealed class UpdateRelationshipRequest : OrganizationRequest
    {
        public bool MergeLabels
        {
            get
            {
                if (Parameters.Contains("MergeLabels"))
                    return (bool)Parameters["MergeLabels"];
                return default(bool);
            }
            set { Parameters["MergeLabels"] = value; }
        }
        public RelationshipMetadataBase Relationship
        {
            get
            {
                if (Parameters.Contains("Relationship"))
                    return (RelationshipMetadataBase)Parameters["Relationship"];
                return default(RelationshipMetadataBase);
            }
            set { Parameters["Relationship"] = value; }
        }
        public string SolutionUniqueName
        {
            get
            {
                if (Parameters.Contains("SolutionUniqueName"))
                    return (string)Parameters["SolutionUniqueName"];
                return default(string);
            }
            set { Parameters["SolutionUniqueName"] = value; }
        }
        public UpdateRelationshipRequest()
        {
            this.ResponseType = new UpdateRelationshipResponse();
            this.RequestName = "UpdateRelationship";
        }
        internal override string GetRequestBody()
        {
            Parameters["MergeLabels"] = MergeLabels;
            Parameters["Relationship"] = Relationship;
            Parameters["SolutionUniqueName"] = SolutionUniqueName;
            return GetSoapBody();
        }
    }
    public sealed class UpdateRelationshipResponse : OrganizationResponse
    {
    }

    #endregion UpdateRelationship

    #region UpdateStateValue
    public sealed class UpdateStateValueRequest : OrganizationRequest
    {
        public string AttributeLogicalName
        {
            get
            {
                if (Parameters.Contains("AttributeLogicalName"))
                    return (string)Parameters["AttributeLogicalName"];
                return default(string);
            }
            set { Parameters["AttributeLogicalName"] = value; }
        }
        public int? DefaultStatusCode
        {
            get
            {
                if (Parameters.Contains("DefaultStatusCode"))
                    return (int?)Parameters["DefaultStatusCode"];
                return default(int?);
            }
            set { Parameters["DefaultStatusCode"] = value; }
        }
        public Label Description
        {
            get
            {
                if (Parameters.Contains("Description"))
                    return (Label)Parameters["Description"];
                return default(Label);
            }
            set { Parameters["Description"] = value; }
        }
        public string EntityLogicalName
        {
            get
            {
                if (Parameters.Contains("EntityLogicalName"))
                    return (string)Parameters["EntityLogicalName"];
                return default(string);
            }
            set { Parameters["EntityLogicalName"] = value; }
        }
        public Label Label
        {
            get
            {
                if (Parameters.Contains("Label"))
                    return (Label)Parameters["Label"];
                return default(Label);
            }
            set { Parameters["Label"] = value; }
        }
        public bool MergeLabels
        {
            get
            {
                if (Parameters.Contains("MergeLabels"))
                    return (bool)Parameters["MergeLabels"];
                return default(bool);
            }
            set { Parameters["MergeLabels"] = value; }
        }
        public string OptionSetName
        {
            get
            {
                if (Parameters.Contains("OptionSetName"))
                    return (string)Parameters["OptionSetName"];
                return default(string);
            }
            set { Parameters["OptionSetName"] = value; }
        }
        public int Value
        {
            get
            {
                if (Parameters.Contains("Value"))
                    return (int)Parameters["Value"];
                return default(int);
            }
            set { Parameters["Value"] = value; }
        }
        public UpdateStateValueRequest()
        {
            this.ResponseType = new UpdateStateValueResponse();
            this.RequestName = "UpdateStateValue";
        }
        internal override string GetRequestBody()
        {
            Parameters["Value"] = Value;
            Parameters["MergeLabels"] = MergeLabels;
            Parameters["AttributeLogicalName"] = AttributeLogicalName;
            Parameters["EntityLogicalName"] = EntityLogicalName;
            Parameters["Label"] = Label;
            Parameters["Description"] = Description;
            Parameters["OptionSetName"] = OptionSetName;
            Parameters["DefaultStatusCode"] = DefaultStatusCode;
            return GetSoapBody();
        }
    }
    public sealed class UpdateStateValueResponse : OrganizationResponse
    {
    }

    #endregion UpdateStateValue

    #region Upsert
    public sealed class UpsertRequest : OrganizationRequest
    {
        public Entity Target
        {
            get
            {
                if (Parameters.Contains("Target"))
                    return (Entity)Parameters["Target"];
                return default(Entity);
            }
            set { Parameters["Target"] = value; }
        }
        public UpsertRequest()
        {
            this.ResponseType = new UpsertResponse();
            this.RequestName = "Upsert";
        }
        internal override string GetRequestBody()
        {
            Parameters["Target"] = Target;
            return GetSoapBody();
        }
    }
    public sealed class UpsertResponse : OrganizationResponse
    {
        public bool RecordCreated { get; set; }
        public EntityReference Target { get; set; }

        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "RecordCreated")
                    this.RecordCreated = Util.LoadFromXml<bool>(result.Element(Util.ns.b + "value"));
                else if (result.Element(Util.ns.b + "key").Value == "Target")
                    this.Target = EntityReference.LoadFromXml(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion Upsert
}
