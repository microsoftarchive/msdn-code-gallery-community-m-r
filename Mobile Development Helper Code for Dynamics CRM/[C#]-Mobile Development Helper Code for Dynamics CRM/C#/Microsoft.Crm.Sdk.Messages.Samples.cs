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
using System.Linq;
using System.Text;
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

// Messages
namespace Microsoft.Crm.Sdk.Messages.Samples
{
    #region AddItemCampaign
    public sealed class AddItemCampaignRequest : OrganizationRequest
    {
        public Guid CampaignId
        {
            get
            {
                if (Parameters.Contains("CampaignId"))
                    return (Guid)Parameters["CampaignId"];
                return default(Guid);
            }
            set { Parameters["CampaignId"] = value; }
        }
        public Guid EntityId
        {
            get
            {
                if (Parameters.Contains("EntityId"))
                    return (Guid)Parameters["EntityId"];
                return default(Guid);
            }
            set { Parameters["EntityId"] = value; }
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
        public AddItemCampaignRequest()
        {
            this.ResponseType = new AddItemCampaignResponse();
            this.RequestName = "AddItemCampaign";
        }
        internal override string GetRequestBody()
        {
            Parameters["CampaignId"] = CampaignId;
            Parameters["EntityId"] = EntityId;
            Parameters["EntityName"] = EntityName;
            return GetSoapBody();
        }
    }
    public sealed class AddItemCampaignResponse : OrganizationResponse
    {
        public Guid CampaignItemId { get; set; }

        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "CampaignItemId")
                    this.CampaignItemId = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion AddItemCampaign

    #region AddItemCampaignActivity
    public sealed class AddItemCampaignActivityRequest : OrganizationRequest
    {
        public Guid CampaignActivityId
        {
            get
            {
                if (Parameters.Contains("CampaignActivityId"))
                    return (Guid)Parameters["CampaignActivityId"];
                return default(Guid);
            }
            set { Parameters["CampaignActivityId"] = value; }
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
        public Guid ItemId
        {
            get
            {
                if (Parameters.Contains("ItemId"))
                    return (Guid)Parameters["ItemId"];
                return default(Guid);
            }
            set { Parameters["ItemId"] = value; }
        }
        public AddItemCampaignActivityRequest()
        {
            this.ResponseType = new AddItemCampaignActivityResponse();
            this.RequestName = "AddItemCampaignActivity";
        }
        internal override string GetRequestBody()
        {
            Parameters["CampaignActivityId"] = CampaignActivityId;
            Parameters["EntityName"] = EntityName;
            Parameters["ItemId"] = ItemId;
            return GetSoapBody();
        }
    }
    public sealed class AddItemCampaignActivityResponse : OrganizationResponse
    {
        public Guid CampaignActivityItemId { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "CampaignActivityItemId")
                    this.CampaignActivityItemId = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion AddItemCampaignActivity

    #region AddListMembersList
    public sealed class AddListMembersListRequest : OrganizationRequest
    {
        public Guid ListId
        {
            get
            {
                if (Parameters.Contains("ListId"))
                    return (Guid)Parameters["ListId"];
                return default(Guid);
            }
            set { Parameters["ListId"] = value; }
        }
        public Guid[] MemberIds
        {
            get
            {
                if (Parameters.Contains("MemberIds"))
                    return (Guid[])Parameters["MemberIds"];
                return default(Guid[]);
            }
            set { Parameters["MemberIds"] = value; }
        }
        public AddListMembersListRequest()
        {
            this.ResponseType = new AddListMembersListResponse();
            this.RequestName = "AddListMembersList";
        }
        internal override string GetRequestBody()
        {
            Parameters["ListId"] = ListId;
            Parameters["MemberIds"] = MemberIds;
            return GetSoapBody();
        }
    }
    public sealed class AddListMembersListResponse : OrganizationResponse
    {
    }

    #endregion AddListMembersList

    #region AddMemberList
    public sealed class AddMemberListRequest : OrganizationRequest
    {
        public Guid EntityId
        {
            get
            {
                if (Parameters.Contains("EntityId"))
                    return (Guid)Parameters["EntityId"];
                return default(Guid);
            }
            set { Parameters["EntityId"] = value; }
        }
        public Guid ListId
        {
            get
            {
                if (Parameters.Contains("ListId"))
                    return (Guid)Parameters["ListId"];
                return default(Guid);
            }
            set { Parameters["ListId"] = value; }
        }
        public AddMemberListRequest()
        {
            this.ResponseType = new AddMemberListResponse();
            this.RequestName = "AddMemberList";
        }
        internal override string GetRequestBody()
        {
            Parameters["EntityId"] = EntityId;
            Parameters["ListId"] = ListId;
            return GetSoapBody();
        }
    }
    public sealed class AddMemberListResponse : OrganizationResponse
    {
        public Guid Id { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "id")
                    this.Id = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion AddMemberList

    #region AddMembersTeam
    public sealed class AddMembersTeamRequest : OrganizationRequest
    {
        public Guid TeamId
        {
            get
            {
                if (Parameters.Contains("TeamId"))
                    return (Guid)Parameters["TeamId"];
                return default(Guid);
            }
            set { Parameters["TeamId"] = value; }
        }
        public Guid[] MemberIds
        {
            get
            {
                if (Parameters.Contains("MemberIds"))
                    return (Guid[])Parameters["MemberIds"];
                return default(Guid[]);
            }
            set { Parameters["MemberIds"] = value; }
        }
        public AddMembersTeamRequest()
        {
            this.ResponseType = new AddMembersTeamResponse();
            this.RequestName = "AddMembersTeam";
        }
        internal override string GetRequestBody()
        {
            Parameters["TeamId"] = TeamId;
            Parameters["MemberIds"] = MemberIds;
            return GetSoapBody();
        }
    }
    public sealed class AddMembersTeamResponse : OrganizationResponse
    {
    }

    #endregion AddMembersTeam

    #region AddPrincipalToQueue 
    public sealed class AddPrincipalToQueueRequest : OrganizationRequest
    {
        public Entity Principal
        {
            get
            {
                if (Parameters.Contains("Principal"))
                    return (Entity)Parameters["Principal"];
                return default(Entity);
            }
            set { Parameters["Principal"] = value; }
        }
        public Guid QueueId 
        {
            get
            {
                if (Parameters.Contains("QueueId"))
                    return (Guid)Parameters["QueueId"];
                return default(Guid);
            }
            set { Parameters["QueueId"] = value; }
        }
        public AddPrincipalToQueueRequest()
        {
            this.ResponseType = new AddPrincipalToQueueResponse();
            this.RequestName = "AddPrincipalToQueue";
        }
        internal override string GetRequestBody()
        {
            Parameters["Principal"] = Principal;
            Parameters["QueueId"] = QueueId;
            return GetSoapBody();
        }
    }
    public sealed class AddPrincipalToQueueResponse : OrganizationResponse
    {
    }

    #endregion AddPrincipalToQueue 

    #region AddPrivilegesRole
    public sealed class AddPrivilegesRoleRequest : OrganizationRequest
    {
        public RolePrivilege[] Privileges
        {
            get
            {
                if (Parameters.Contains("Privileges"))
                    return (RolePrivilege[])Parameters["Privileges"];
                return default(RolePrivilege[]);
            }
            set { Parameters["Privileges"] = value; }
        }
        public Guid RoleId
        {
            get
            {
                if (Parameters.Contains("RoleId"))
                    return (Guid)Parameters["RoleId"];
                return default(Guid);
            }
            set { Parameters["RoleId"] = value; }
        }
        public AddPrivilegesRoleRequest()
        {
            this.ResponseType = new AddPrivilegesRoleResponse();
            this.RequestName = "AddPrivilegesRole";
        }
        internal override string GetRequestBody()
        {
            Parameters["Privileges"] = Privileges;
            Parameters["RoleId"] = RoleId;
            return GetSoapBody();
        }
    }
    public sealed class AddPrivilegesRoleResponse : OrganizationResponse
    {
    }

    #endregion AddPrivilegesRole

    #region AddProductToKit
    public sealed class AddProductToKitRequest : OrganizationRequest
    {
        public Guid KitId
        {
            get
            {
                if (Parameters.Contains("KitId"))
                    return (Guid)Parameters["KitId"];
                return default(Guid);
            }
            set { Parameters["KitId"] = value; }
        }
        public Guid ProductId
        {
            get
            {
                if (Parameters.Contains("ProductId"))
                    return (Guid)Parameters["ProductId"];
                return default(Guid);
            }
            set { Parameters["ProductId"] = value; }
        }
        public AddProductToKitRequest()
        {
            this.ResponseType = new AddProductToKitResponse();
            this.RequestName = "AddProductToKit";
        }
        internal override string GetRequestBody()
        {
            Parameters["KitId"] = KitId;
            Parameters["ProductId"] = ProductId;
            return GetSoapBody();
        }
    }
    public sealed class AddProductToKitResponse : OrganizationResponse
    {
    }

    #endregion AddProductToKit

    #region AddRecurrence
    public sealed class AddRecurrenceRequest : OrganizationRequest
    {
        public Guid AppointmentId
        {
            get
            {
                if (Parameters.Contains("AppointmentId"))
                    return (Guid)Parameters["AppointmentId"];
                return default(Guid);
            }
            set { Parameters["AppointmentId"] = value; }
        }
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
        public AddRecurrenceRequest()
        {
            this.ResponseType = new AddRecurrenceResponse();
            this.RequestName = "AddRecurrence";
        }
        internal override string GetRequestBody()
        {
            Parameters["AppointmentId"] = AppointmentId;
            Parameters["Target"] = Target;
            return GetSoapBody();
        }
    }
    public sealed class AddRecurrenceResponse : OrganizationResponse
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

    #endregion AddRecurrence

    #region AddSolutionComponent
    public sealed class AddSolutionComponentRequest : OrganizationRequest
    {
        public bool AddRequiredComponents
        {
            get
            {
                if (Parameters.Contains("AddRequiredComponents"))
                    return (bool)Parameters["AddRequiredComponents"];
                return default(bool);
            }
            set { Parameters["AddRequiredComponents"] = value; }
        }
        public Guid ComponentId
        {
            get
            {
                if (Parameters.Contains("ComponentId"))
                    return (Guid)Parameters["ComponentId"];
                return default(Guid);
            }
            set { Parameters["ComponentId"] = value; }
        }
        public int ComponentType
        {
            get
            {
                if (Parameters.Contains("ComponentType"))
                    return (int)Parameters["ComponentType"];
                return default(int);
            }
            set { Parameters["ComponentType"] = value; }
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
        public AddSolutionComponentRequest()
        {
            this.ResponseType = new AddSolutionComponentResponse();
            this.RequestName = "AddSolutionComponent";
        }
        internal override string GetRequestBody()
        {
            Parameters["AddRequiredComponents"] = AddRequiredComponents;
            Parameters["ComponentId"] = ComponentId;
            Parameters["ComponentType"] = ComponentType;
            Parameters["SolutionUniqueName"] = SolutionUniqueName;
            return GetSoapBody();
        }
    }
    public sealed class AddSolutionComponentResponse : OrganizationResponse
    {
        public Guid id { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "id")
                    this.id = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion AddSolutionComponent

    #region AddSubstituteProduct
    public sealed class AddSubstituteProductRequest : OrganizationRequest
    {
        public Guid ProductId
        {
            get
            {
                if (Parameters.Contains("ProductId"))
                    return (Guid)Parameters["ProductId"];
                return default(Guid);
            }
            set { Parameters["ProductId"] = value; }
        }
        public Guid SubstituteId
        {
            get
            {
                if (Parameters.Contains("SubstituteId"))
                    return (Guid)Parameters["SubstituteId"];
                return default(Guid);
            }
            set { Parameters["SubstituteId"] = value; }
        }
        public AddSubstituteProductRequest()
        {
            this.ResponseType = new AddSubstituteProductResponse();
            this.RequestName = "AddSubstituteProduct";
        }
        internal override string GetRequestBody()
        {
            Parameters["ProductId"] = ProductId;
            Parameters["SubstituteId"] = SubstituteId;
            return GetSoapBody();
        }
    }
    public sealed class AddSubstituteProductResponse : OrganizationResponse
    {
    }

    #endregion AddSubstituteProduct

    #region AddToQueue
    public sealed class AddToQueueRequest : OrganizationRequest
    {
        public Guid DestinationQueueId
        {
            get
            {
                if (Parameters.Contains("DestinationQueueId"))
                    return (Guid)Parameters["DestinationQueueId"];
                return default(Guid);
            }
            set { Parameters["DestinationQueueId"] = value; }
        }
        public Guid SourceQueueId
        {
            get
            {
                if (Parameters.Contains("SourceQueueId"))
                    return (Guid)Parameters["SourceQueueId"];
                return default(Guid);
            }
            set { Parameters["SourceQueueId"] = value; }
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
        public Entity QueueItemProperties
        {
            get
            {
                if (Parameters.Contains("QueueItemProperties"))
                    return (Entity)Parameters["QueueItemProperties"];
                return default(Entity);
            }
            set { Parameters["QueueItemProperties"] = value; }
        }
        public AddToQueueRequest()
        {
            this.ResponseType = new AddToQueueResponse();
            this.RequestName = "AddToQueue";
        }
        internal override string GetRequestBody()
        {
            Parameters["DestinationQueueId"] = DestinationQueueId;
            Parameters["SourceQueueId"] = SourceQueueId;
            Parameters["Target"] = Target;
            Parameters["QueueItemProperties"] = QueueItemProperties;
            return GetSoapBody();
        }
    }
    public sealed class AddToQueueResponse : OrganizationResponse
    {
        public Guid QueueItemId { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "QueueItemId")
                    this.QueueItemId = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion AddToQueue

    #region AddUserToRecordTeam
    public sealed class AddUserToRecordTeamRequest : OrganizationRequest
    {
        public EntityReference Record
        {
            get
            {
                if (Parameters.Contains("Record"))
                    return (EntityReference)Parameters["Record"];
                return default(EntityReference);
            }
            set { Parameters["Record"] = value; }
        }
        public Guid SystemUserId
        {
            get
            {
                if (Parameters.Contains("SystemUserId"))
                    return (Guid)Parameters["SystemUserId"];
                return default(Guid);
            }
            set { Parameters["SystemUserId"] = value; }
        }
        public Guid TeamTemplateId
        {
            get
            {
                if (Parameters.Contains("TeamTemplateId"))
                    return (Guid)Parameters["TeamTemplateId"];
                return default(Guid);
            }
            set { Parameters["TeamTemplateId"] = value; }
        }
        public AddUserToRecordTeamRequest()
        {
            this.ResponseType = new AddUserToRecordTeamResponse();
            this.RequestName = "AddUserToRecordTeam";
        }
        internal override string GetRequestBody()
        {
            Parameters["Record"] = Record;
            Parameters["SystemUserId"] = SystemUserId;
            Parameters["TeamTemplateId"] = TeamTemplateId;
            return GetSoapBody();
        }
    }
    public sealed class AddUserToRecordTeamResponse : OrganizationResponse
    {
        public Guid AccessTeamId { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "AccessTeamId")
                    this.AccessTeamId = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion AddUserToRecordTeam

    #region ApplyRecordCreationAndUpdateRule
    public sealed class ApplyRecordCreationAndUpdateRuleRequest : OrganizationRequest
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
        public ApplyRecordCreationAndUpdateRuleRequest()
        {
            this.ResponseType = new ApplyRecordCreationAndUpdateRuleResponse();
            this.RequestName = "ApplyRecordCreationAndUpdateRule";
        }
        internal override string GetRequestBody()
        {
            Parameters["Target"] = Target;
            return GetSoapBody();
        }
    }
    public sealed class ApplyRecordCreationAndUpdateRuleResponse : OrganizationResponse
    {       
    }

    #endregion ApplyRecordCreationAndUpdateRule

    #region ApplyRoutingRule
    public sealed class ApplyRoutingRuleRequest : OrganizationRequest
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
       
        public ApplyRoutingRuleRequest()
        {
            this.ResponseType = new ApplyRoutingRuleResponse();
            this.RequestName = "ApplyRoutingRule";
        }
        internal override string GetRequestBody()
        {
            Parameters["Target"] = Target;
            return GetSoapBody();
        }
    }
    public sealed class ApplyRoutingRuleResponse : OrganizationResponse
    {
    }

    #endregion ApplyRoutingRule 

    #region Assign
    public sealed class AssignRequest : OrganizationRequest
    {
        public EntityReference Assignee
        {
            get
            {
                if (Parameters.Contains("Assignee"))
                    return (EntityReference)Parameters["Assignee"];
                return default(EntityReference);
            }
            set { Parameters["Assignee"] = value; }
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
        public AssignRequest()
        {
            this.ResponseType = new AssignResponse();
            this.RequestName = "Assign";
        }
        internal override string GetRequestBody()
        {
            Parameters["Assignee"] = Assignee;
            Parameters["Target"] = Target;
            return GetSoapBody();
        }
    }
    public sealed class AssignResponse : OrganizationResponse
    {
    }

    #endregion Assign

    #region AssociateEntities
    public sealed class AssociateEntitiesRequest : OrganizationRequest
    {
        public EntityReference Moniker1
        {
            get
            {
                if (Parameters.Contains("Moniker1"))
                    return (EntityReference)Parameters["Moniker1"];
                return default(EntityReference);
            }
            set { Parameters["Moniker1"] = value; }
        }
        public EntityReference Moniker2
        {
            get
            {
                if (Parameters.Contains("Moniker2"))
                    return (EntityReference)Parameters["Moniker2"];
                return default(EntityReference);
            }
            set { Parameters["Moniker2"] = value; }
        }
        public string RelationshipName
        {
            get
            {
                if (Parameters.Contains("RelationshipName"))
                    return (string)Parameters["RelationshipName"];
                return default(string);
            }
            set { Parameters["RelationshipName"] = value; }
        }
        public AssociateEntitiesRequest()
        {
            this.ResponseType = new AssociateEntitiesResponse();
            this.RequestName = "AssociateEntities";
        }
        internal override string GetRequestBody()
        {
            Parameters["Moniker1"] = Moniker1;
            Parameters["Moniker2"] = Moniker2;
            Parameters["RelationshipName"] = RelationshipName;
            return GetSoapBody();
        }
    }
    public sealed class AssociateEntitiesResponse : OrganizationResponse
    {
    }

    #endregion AssociateEntities

    #region AutoMapEntity
    public sealed class AutoMapEntityRequest : OrganizationRequest
    {
        public Guid EntityMapId
        {
            get
            {
                if (Parameters.Contains("EntityMapId"))
                    return (Guid)Parameters["EntityMapId"];
                return default(Guid);
            }
            set { Parameters["EntityMapId"] = value; }
        }
        public AutoMapEntityRequest()
        {
            this.ResponseType = new AutoMapEntityResponse();
            this.RequestName = "AutoMapEntity";
        }
        internal override string GetRequestBody()
        {
            Parameters["EntityMapId"] = EntityMapId;
            return GetSoapBody();
        }
    }
    public sealed class AutoMapEntityResponse : OrganizationResponse
    {
    }

    #endregion AutoMapEntity

    #region BackgroundSendEmail
    public sealed class BackgroundSendEmailRequest : OrganizationRequest
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
        public BackgroundSendEmailRequest()
        {
            this.ResponseType = new BackgroundSendEmailResponse();
            this.RequestName = "BackgroundSendEmail";
        }
        internal override string GetRequestBody()
        {
            Parameters["Query"] = Query;
            return GetSoapBody();
        }
    }
    public sealed class BackgroundSendEmailResponse : OrganizationResponse
    {
        public EntityCollection EntityCollection { get; set; }
        public bool[] HasAttachments { get; set; }

        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "EntityCollection")
                    EntityCollection = EntityCollection.LoadFromXml(result.Element(Util.ns.b + "value"));
                else if (result.Element(Util.ns.b + "key").Value == "HasAttachments")
                {
                    List<bool> list = new List<bool>();
                    foreach (XElement item in result.Element(Util.ns.b + "value").Elements(Util.ns.f + "boolean"))
                    {
                        list.Add(Util.LoadFromXml<bool>(item));
                    }
                    this.HasAttachments = list.ToArray();
                }
            }
        }
    }

    #endregion BackgroundSendEmail

    #region Book
    public sealed class BookRequest : OrganizationRequest
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
        public bool ReturnNotifications
        {
            get
            {
                if (Parameters.Contains("ReturnNotifications"))
                    return (bool)Parameters["ReturnNotifications"];
                return default(bool);
            }
            set { Parameters["ReturnNotifications"] = value; }
        }
        public BookRequest()
        {
            this.ResponseType = new BookResponse();
            this.RequestName = "Book";
        }
        internal override string GetRequestBody()
        {
            Parameters["Target"] = Target;
            Parameters["ReturnNotifications"] = ReturnNotifications;
            return GetSoapBody();
        }
    }
    public sealed class BookResponse : OrganizationResponse
    {
        public Object Notifications { get; set; }
        public ValidationResult ValidationResult { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "ValidationResult")
                    this.ValidationResult = ValidationResult.LoadFromXml(result.Element(Util.ns.b + "value"));
                else if (result.Element(Util.ns.b + "key").Value == "Notifications")
                    this.Notifications = result.Element(Util.ns.b + "value").Value;
            }
        }
    }

    #endregion Book

    #region BulkDelete
    public sealed class BulkDeleteRequest : OrganizationRequest
    {
        public Guid[] CCRecipients
        {
            get
            {
                if (Parameters.Contains("CCRecipients"))
                    return (Guid[])Parameters["CCRecipients"];
                return default(Guid[]);
            }
            set { Parameters["CCRecipients"] = value; }
        }
        public string JobName
        {
            get
            {
                if (Parameters.Contains("JobName"))
                    return (string)Parameters["JobName"];
                return default(string);
            }
            set { Parameters["JobName"] = value; }
        }
        public QueryExpression[] QuerySet
        {
            get
            {
                if (Parameters.Contains("QuerySet"))
                    return (QueryExpression[])Parameters["QuerySet"];
                return default(QueryExpression[]);
            }
            set { Parameters["QuerySet"] = value; }
        }
        public string RecurrencePattern
        {
            get
            {
                if (Parameters.Contains("RecurrencePattern"))
                    return (string)Parameters["RecurrencePattern"];
                return default(string);
            }
            set { Parameters["RecurrencePattern"] = value; }
        }
        public bool SendEmailNotification
        {
            get
            {
                if (Parameters.Contains("SendEmailNotification"))
                    return (bool)Parameters["SendEmailNotification"];
                return default(bool);
            }
            set { Parameters["SendEmailNotification"] = value; }
        }
        public Guid? SourceImportId
        {
            get
            {
                if (Parameters.Contains("SourceImportId"))
                    return (Guid?)Parameters["SourceImportId"];
                return default(Guid?);
            }
            set { Parameters["SourceImportId"] = value; }
        }
        public DateTime StartDateTime
        {
            get
            {
                if (Parameters.Contains("StartDateTime"))
                    return (DateTime)Parameters["StartDateTime"];
                return default(DateTime);
            }
            set { Parameters["StartDateTime"] = value; }
        }
        public Guid[] ToRecipients
        {
            get
            {
                if (Parameters.Contains("ToRecipients"))
                    return (Guid[])Parameters["ToRecipients"];
                return default(Guid[]);
            }
            set { Parameters["ToRecipients"] = value; }
        }
        public BulkDeleteRequest()
        {
            this.ResponseType = new BulkDeleteResponse();
            this.RequestName = "BulkDelete";
        }
        internal override string GetRequestBody()
        {
            Parameters["CCRecipients"] = CCRecipients;
            Parameters["JobName"] = JobName;
            Parameters["QuerySet"] = QuerySet;
            Parameters["RecurrencePattern"] = RecurrencePattern;
            Parameters["SendEmailNotification"] = SendEmailNotification;
            Parameters["SourceImportId"] = SourceImportId;
            Parameters["StartDateTime"] = StartDateTime;
            Parameters["ToRecipients"] = ToRecipients;
            return GetSoapBody();
        }
    }
    public sealed class BulkDeleteResponse : OrganizationResponse
    {
        public Guid JobId { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "JobId")
                    this.JobId = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion BulkDelete

    #region BulkDetectDuplicates
    public sealed class BulkDetectDuplicatesRequest : OrganizationRequest
    {
        public Guid[] CCRecipients
        {
            get
            {
                if (Parameters.Contains("CCRecipients"))
                    return (Guid[])Parameters["CCRecipients"];
                return default(Guid[]);
            }
            set { Parameters["CCRecipients"] = value; }
        }
        public string JobName
        {
            get
            {
                if (Parameters.Contains("JobName"))
                    return (string)Parameters["JobName"];
                return default(string);
            }
            set { Parameters["JobName"] = value; }
        }
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
        public string RecurrencePattern
        {
            get
            {
                if (Parameters.Contains("RecurrencePattern"))
                    return (string)Parameters["RecurrencePattern"];
                return default(string);
            }
            set { Parameters["RecurrencePattern"] = value; }
        }
        public DateTime RecurrenceStartTime
        {
            get
            {
                if (Parameters.Contains("RecurrenceStartTime"))
                    return (DateTime)Parameters["RecurrenceStartTime"];
                return default(DateTime);
            }
            set { Parameters["RecurrenceStartTime"] = value; }
        }
        public bool SendEmailNotification
        {
            get
            {
                if (Parameters.Contains("SendEmailNotification"))
                    return (bool)Parameters["SendEmailNotification"];
                return default(bool);
            }
            set { Parameters["SendEmailNotification"] = value; }
        }
        public Guid TemplateId
        {
            get
            {
                if (Parameters.Contains("TemplateId"))
                    return (Guid)Parameters["TemplateId"];
                return default(Guid);
            }
            set { Parameters["TemplateId"] = value; }
        }
        public Guid[] ToRecipients
        {
            get
            {
                if (Parameters.Contains("ToRecipients"))
                    return (Guid[])Parameters["ToRecipients"];
                return default(Guid[]);
            }
            set { Parameters["ToRecipients"] = value; }
        }
        public BulkDetectDuplicatesRequest()
        {
            this.ResponseType = new BulkDetectDuplicatesResponse();
            this.RequestName = "BulkDetectDuplicates";
        }
        internal override string GetRequestBody()
        {
            Parameters["CCRecipients"] = CCRecipients;
            Parameters["JobName"] = JobName;
            Parameters["Query"] = Query;
            Parameters["RecurrencePattern"] = RecurrencePattern;
            Parameters["RecurrenceStartTime"] = RecurrenceStartTime;
            Parameters["SendEmailNotification"] = SendEmailNotification;
            Parameters["TemplateId"] = TemplateId;
            Parameters["ToRecipients"] = ToRecipients;
            return GetSoapBody();
        }
    }
    public sealed class BulkDetectDuplicatesResponse : OrganizationResponse
    {
        public Guid JobId { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "JobId")
                    this.JobId = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion BulkDetectDuplicates

    #region BulkOperationStatusClose
    public sealed class BulkOperationStatusCloseRequest : OrganizationRequest
    {
        public Guid BulkOperationId
        {
            get
            {
                if (Parameters.Contains("BulkOperationId"))
                    return (Guid)Parameters["BulkOperationId"];
                return default(Guid);
            }
            set { Parameters["BulkOperationId"] = value; }
        }
        public int FailureCount
        {
            get
            {
                if (Parameters.Contains("FailureCount"))
                    return (int)Parameters["FailureCount"];
                return default(int);
            }
            set { Parameters["FailureCount"] = value; }
        }
        public int SuccessCount
        {
            get
            {
                if (Parameters.Contains("SuccessCount"))
                    return (int)Parameters["SuccessCount"];
                return default(int);
            }
            set { Parameters["SuccessCount"] = value; }
        }
        public int StatusReason
        {
            get
            {
                if (Parameters.Contains("StatusReason"))
                    return (int)Parameters["StatusReason"];
                return default(int);
            }
            set { Parameters["StatusReason"] = value; }
        }
        public int ErrorCode
        {
            get
            {
                if (Parameters.Contains("ErrorCode"))
                    return (int)Parameters["ErrorCode"];
                return default(int);
            }
            set { Parameters["ErrorCode"] = value; }
        }
        public BulkOperationStatusCloseRequest()
        {
            this.ResponseType = new BulkOperationStatusCloseResponse();
            this.RequestName = "BulkOperationStatusClose";
        }
        internal override string GetRequestBody()
        {
            Parameters["BulkOperationId"] = BulkOperationId;
            Parameters["FailureCount"] = FailureCount;
            Parameters["SuccessCount"] = SuccessCount;
            Parameters["StatusReason"] = StatusReason;
            Parameters["ErrorCode"] = ErrorCode;
            return GetSoapBody();
        }
    }
    public sealed class BulkOperationStatusCloseResponse : OrganizationResponse
    {
    }

    #endregion BulkOperationStatusClose

    #region CalculateActualValueOpportunity
    public sealed class CalculateActualValueOpportunityRequest : OrganizationRequest
    {
        public Guid OpportunityId
        {
            get
            {
                if (Parameters.Contains("OpportunityId"))
                    return (Guid)Parameters["OpportunityId"];
                return default(Guid);
            }
            set { Parameters["OpportunityId"] = value; }
        }
        public CalculateActualValueOpportunityRequest()
        {
            this.ResponseType = new CalculateActualValueOpportunityResponse();
            this.RequestName = "CalculateActualValueOpportunity";
        }
        internal override string GetRequestBody()
        {
            Parameters["OpportunityId"] = OpportunityId;
            return GetSoapBody();
        }
    }
    public sealed class CalculateActualValueOpportunityResponse : OrganizationResponse
    {
        public decimal Value { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "Value")
                    this.Value = Util.LoadFromXml<decimal>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion CalculateActualValueOpportunity

    #region CalculateRollupField
    public sealed class CalculateRollupFieldRequest : OrganizationRequest
    {
        public string FieldName
        {
            get
            {
                if (Parameters.Contains("FieldName"))
                    return (string)Parameters["FieldName"];
                return default(string);
            }
            set { Parameters["FieldName"] = value; }
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
        public CalculateRollupFieldRequest()
        {
            this.ResponseType = new CalculateRollupFieldResponse();
            this.RequestName = "CalculateRollupField";
        }
        internal override string GetRequestBody()
        {
            Parameters["FieldName"] = FieldName;
            Parameters["Target"] = Target;
            return GetSoapBody();
        }
    }
    public sealed class CalculateRollupFieldResponse : OrganizationResponse
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

    #endregion CalculateRollupField

    #region CalculateTotalTimeIncident
    public sealed class CalculateTotalTimeIncidentRequest : OrganizationRequest
    {
        public Guid IncidentId
        {
            get
            {
                if (Parameters.Contains("IncidentId"))
                    return (Guid)Parameters["IncidentId"];
                return default(Guid);
            }
            set { Parameters["IncidentId"] = value; }
        }
        public CalculateTotalTimeIncidentRequest()
        {
            this.ResponseType = new CalculateTotalTimeIncidentResponse();
            this.RequestName = "CalculateTotalTimeIncident";
        }
        internal override string GetRequestBody()
        {
            Parameters["IncidentId"] = IncidentId;
            return GetSoapBody();
        }
    }
    public sealed class CalculateTotalTimeIncidentResponse : OrganizationResponse
    {
        public long TotalTime { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "TotalTime")
                    this.TotalTime = Util.LoadFromXml<long>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion CalculateTotalTimeIncident

    #region CancelContract
    public sealed class CancelContractRequest : OrganizationRequest
    {
        public DateTime CancelDate
        {
            get
            {
                if (Parameters.Contains("CancelDate"))
                    return (DateTime)Parameters["CancelDate"];
                return default(DateTime);
            }
            set { Parameters["CancelDate"] = value; }
        }
        public Guid ContractId
        {
            get
            {
                if (Parameters.Contains("ContractId"))
                    return (Guid)Parameters["ContractId"];
                return default(Guid);
            }
            set { Parameters["ContractId"] = value; }
        }
        public OptionSetValue Status
        {
            get
            {
                if (Parameters.Contains("Status"))
                    return (OptionSetValue)Parameters["Status"];
                return default(OptionSetValue);
            }
            set { Parameters["Status"] = value; }
        }
        public CancelContractRequest()
        {
            this.ResponseType = new CancelContractResponse();
            this.RequestName = "CancelContract";
        }
        internal override string GetRequestBody()
        {
            Parameters["CancelDate"] = CancelDate;
            Parameters["ContractId"] = ContractId;
            Parameters["Status"] = Status;
            return GetSoapBody();
        }
    }
    public sealed class CancelContractResponse : OrganizationResponse
    {
    }

    #endregion CancelContract

    #region CancelSalesOrder
    public sealed class CancelSalesOrderRequest : OrganizationRequest
    {
        public Entity OrderClose
        {
            get
            {
                if (Parameters.Contains("OrderClose"))
                    return (Entity)Parameters["OrderClose"];
                return default(Entity);
            }
            set { Parameters["OrderClose"] = value; }
        }
        public OptionSetValue Status
        {
            get
            {
                if (Parameters.Contains("Status"))
                    return (OptionSetValue)Parameters["Status"];
                return default(OptionSetValue);
            }
            set { Parameters["Status"] = value; }
        }
        public CancelSalesOrderRequest()
        {
            this.ResponseType = new CancelSalesOrderResponse();
            this.RequestName = "CancelSalesOrder";
        }
        internal override string GetRequestBody()
        {
            Parameters["OrderClose"] = OrderClose;
            Parameters["Status"] = Status;
            return GetSoapBody();
        }
    }
    public sealed class CancelSalesOrderResponse : OrganizationResponse
    {
    }

    #endregion CancelSalesOrder

    #region CheckIncomingEmail
    public sealed class CheckIncomingEmailRequest : OrganizationRequest
    {
        public string Bcc
        {
            get
            {
                if (Parameters.Contains("Bcc"))
                    return (string)Parameters["Bcc"];
                return default(string);
            }
            set { Parameters["Bcc"] = value; }
        }
        public string Cc
        {
            get
            {
                if (Parameters.Contains("Cc"))
                    return (string)Parameters["Cc"];
                return default(string);
            }
            set { Parameters["Cc"] = value; }
        }
        public Entity ExtraProperties
        {
            get
            {
                if (Parameters.Contains("ExtraProperties"))
                    return (Entity)Parameters["ExtraProperties"];
                return default(Entity);
            }
            set { Parameters["ExtraProperties"] = value; }
        }
        public string From
        {
            get
            {
                if (Parameters.Contains("From"))
                    return (string)Parameters["From"];
                return default(string);
            }
            set { Parameters["From"] = value; }
        }
        public string MessageId
        {
            get
            {
                if (Parameters.Contains("MessageId"))
                    return (string)Parameters["MessageId"];
                return default(string);
            }
            set { Parameters["MessageId"] = value; }
        }
        public string Subject
        {
            get
            {
                if (Parameters.Contains("Subject"))
                    return (string)Parameters["Subject"];
                return default(string);
            }
            set { Parameters["Subject"] = value; }
        }
        public string To
        {
            get
            {
                if (Parameters.Contains("To"))
                    return (string)Parameters["To"];
                return default(string);
            }
            set { Parameters["To"] = value; }
        }
        public CheckIncomingEmailRequest()
        {
            this.ResponseType = new CheckIncomingEmailResponse();
            this.RequestName = "CheckIncomingEmail";
        }
        internal override string GetRequestBody()
        {
            Parameters["Bcc"] = Bcc;
            Parameters["Cc"] = Cc;
            Parameters["ExtraProperties"] = ExtraProperties;
            Parameters["From"] = From;
            Parameters["MessageId"] = MessageId;
            Parameters["Subject"] = Subject;
            Parameters["To"] = To;
            return GetSoapBody();
        }
    }
    public sealed class CheckIncomingEmailResponse : OrganizationResponse
    {
        public bool ShouldDeliver { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "ShouldDeliver")
                    this.ShouldDeliver = Util.LoadFromXml<bool>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion CheckIncomingEmail

    #region CheckPromoteEmail
    public sealed class CheckPromoteEmailRequest : OrganizationRequest
    {
        public int DirectionCode
        {
            get
            {
                if (Parameters.Contains("DirectionCode"))
                    return (int)Parameters["DirectionCode"];
                return default(int);
            }
            set { Parameters["DirectionCode"] = value; }
        }
        public string MessageId
        {
            get
            {
                if (Parameters.Contains("MessageId"))
                    return (string)Parameters["MessageId"];
                return default(string);
            }
            set { Parameters["MessageId"] = value; }
        }
        public string Subject
        {
            get
            {
                if (Parameters.Contains("Subject"))
                    return (string)Parameters["Subject"];
                return default(string);
            }
            set { Parameters["Subject"] = value; }
        }
        public CheckPromoteEmailRequest()
        {
            this.ResponseType = new CheckPromoteEmailResponse();
            this.RequestName = "CheckPromoteEmail";
        }
        internal override string GetRequestBody()
        {
            Parameters["DirectionCode"] = DirectionCode;
            Parameters["MessageId"] = MessageId;
            Parameters["Subject"] = Subject;
            return GetSoapBody();
        }
    }
    public sealed class CheckPromoteEmailResponse : OrganizationResponse
    {
        public bool ShouldPromote { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "ShouldPromote")
                    this.ShouldPromote = Util.LoadFromXml<bool>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion CheckPromoteEmail

    #region CleanUpBulkOperation
    public sealed class CleanUpBulkOperationRequest : OrganizationRequest
    {
        public Guid BulkOperationId
        {
            get
            {
                if (Parameters.Contains("BulkOperationId"))
                    return (Guid)Parameters["BulkOperationId"];
                return default(Guid);
            }
            set { Parameters["BulkOperationId"] = value; }
        }
        public int BulkOperationSource
        {
            get
            {
                if (Parameters.Contains("BulkOperationSource"))
                    return (int)Parameters["BulkOperationSource"];
                return default(int);
            }
            set { Parameters["BulkOperationSource"] = value; }
        }
        public CleanUpBulkOperationRequest()
        {
            this.ResponseType = new CleanUpBulkOperationResponse();
            this.RequestName = "CleanUpBulkOperation";
        }
        internal override string GetRequestBody()
        {
            Parameters["BulkOperationId"] = BulkOperationId;
            Parameters["BulkOperationSource"] = BulkOperationSource;
            return GetSoapBody();
        }
    }
    public sealed class CleanUpBulkOperationResponse : OrganizationResponse
    {
    }

    #endregion CleanUpBulkOperation

    #region CloneContract
    public sealed class CloneContractRequest : OrganizationRequest
    {
        public Guid ContractId
        {
            get
            {
                if (Parameters.Contains("ContractId"))
                    return (Guid)Parameters["ContractId"];
                return default(Guid);
            }
            set { Parameters["ContractId"] = value; }
        }
        public bool IncludeCanceledLines
        {
            get
            {
                if (Parameters.Contains("IncludeCanceledLines"))
                    return (bool)Parameters["IncludeCanceledLines"];
                return default(bool);
            }
            set { Parameters["IncludeCanceledLines"] = value; }
        }
        public CloneContractRequest()
        {
            this.ResponseType = new CloneContractResponse();
            this.RequestName = "CloneContract";
        }
        internal override string GetRequestBody()
        {
            Parameters["ContractId"] = ContractId;
            Parameters["IncludeCanceledLines"] = IncludeCanceledLines;
            return GetSoapBody();
        }
    }
    public sealed class CloneContractResponse : OrganizationResponse
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

    #endregion CloneContract

    #region CloneProduct
    public sealed class CloneProductRequest : OrganizationRequest
    {
        public EntityReference Source
        {
            get
            {
                if (Parameters.Contains("Source"))
                    return (EntityReference)Parameters["Source"];
                return default(EntityReference);
            }
            set { Parameters["Source"] = value; }
        }        
        public CloneProductRequest()
        {
            this.ResponseType = new CloneProductResponse();
            this.RequestName = "CloneProduct";
        }
        internal override string GetRequestBody()
        {
            Parameters["Source"] = Source;
            return GetSoapBody();
        }
    }
    public sealed class CloneProductResponse : OrganizationResponse
    {
        public EntityReference ClonedProduct { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "ClonedProduct")
                    ClonedProduct = EntityReference.LoadFromXml(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion CloneProduct

    #region CloseIncident
    public sealed class CloseIncidentRequest : OrganizationRequest
    {
        public Entity IncidentResolution
        {
            get
            {
                if (Parameters.Contains("IncidentResolution"))
                    return (Entity)Parameters["IncidentResolution"];
                return default(Entity);
            }
            set { Parameters["IncidentResolution"] = value; }
        }
        public OptionSetValue Status
        {
            get
            {
                if (Parameters.Contains("Status"))
                    return (OptionSetValue)Parameters["Status"];
                return default(OptionSetValue);
            }
            set { Parameters["Status"] = value; }
        }
        public CloseIncidentRequest()
        {
            this.ResponseType = new CloseIncidentResponse();
            this.RequestName = "CloseIncident";
        }
        internal override string GetRequestBody()
        {
            Parameters["IncidentResolution"] = IncidentResolution;
            Parameters["Status"] = Status;
            return GetSoapBody();
        }
    }
    public sealed class CloseIncidentResponse : OrganizationResponse
    {
    }

    #endregion CloseIncident

    #region CloseQuote
    public sealed class CloseQuoteRequest : OrganizationRequest
    {
        public Entity QuoteClose
        {
            get
            {
                if (Parameters.Contains("QuoteClose"))
                    return (Entity)Parameters["QuoteClose"];
                return default(Entity);
            }
            set { Parameters["QuoteClose"] = value; }
        }
        public OptionSetValue Status
        {
            get
            {
                if (Parameters.Contains("Status"))
                    return (OptionSetValue)Parameters["Status"];
                return default(OptionSetValue);
            }
            set { Parameters["Status"] = value; }
        }
        public CloseQuoteRequest()
        {
            this.ResponseType = new CloseQuoteResponse();
            this.RequestName = "CloseQuote";
        }
        internal override string GetRequestBody()
        {
            Parameters["QuoteClose"] = QuoteClose;
            Parameters["Status"] = Status;
            return GetSoapBody();
        }
    }
    public sealed class CloseQuoteResponse : OrganizationResponse
    {
    }

    #endregion CloseQuote

    #region CompoundCreate
    public sealed class CompoundCreateRequest : OrganizationRequest
    {
        public Entity Entity
        {
            get
            {
                if (Parameters.Contains("Entity"))
                    return (Entity)Parameters["Entity"];
                return default(Entity);
            }
            set { Parameters["Entity"] = value; }
        }
        public EntityCollection ChildEntities
        {
            get
            {
                if (Parameters.Contains("ChildEntities"))
                    return (EntityCollection)Parameters["ChildEntities"];
                return default(EntityCollection);
            }
            set { Parameters["ChildEntities"] = value; }
        }
        public CompoundCreateRequest()
        {
            this.ResponseType = new CompoundCreateResponse();
            this.RequestName = "CompoundCreate";
        }
        internal override string GetRequestBody()
        {
            Parameters["Entity"] = Entity;
            Parameters["ChildEntities"] = ChildEntities;
            return GetSoapBody();
        }
    }
    public sealed class CompoundCreateResponse : OrganizationResponse
    {
        public Guid Id { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "Id")
                    this.Id = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion CompoundCreate

    #region CompoundUpdate
    public sealed class CompoundUpdateRequest : OrganizationRequest
    {
        public Entity Entity
        {
            get
            {
                if (Parameters.Contains("Entity"))
                    return (Entity)Parameters["Entity"];
                return default(Entity);
            }
            set { Parameters["Entity"] = value; }
        }
        public EntityCollection ChildEntities
        {
            get
            {
                if (Parameters.Contains("ChildEntities"))
                    return (EntityCollection)Parameters["ChildEntities"];
                return default(EntityCollection);
            }
            set { Parameters["ChildEntities"] = value; }
        }
        public CompoundUpdateRequest()
        {
            this.ResponseType = new CompoundUpdateResponse();
            this.RequestName = "CompoundUpdate";
        }
        internal override string GetRequestBody()
        {
            Parameters["Entity"] = Entity;
            Parameters["ChildEntities"] = ChildEntities;
            return GetSoapBody();
        }
    }
    public sealed class CompoundUpdateResponse : OrganizationResponse
    {
    }

    #endregion CompoundUpdate

    #region CompoundUpdateDuplicateDetectionRule
    public sealed class CompoundUpdateDuplicateDetectionRuleRequest : OrganizationRequest
    {
        public Entity Entity
        {
            get
            {
                if (Parameters.Contains("Entity"))
                    return (Entity)Parameters["Entity"];
                return default(Entity);
            }
            set { Parameters["Entity"] = value; }
        }
        public EntityCollection ChildEntities
        {
            get
            {
                if (Parameters.Contains("ChildEntities"))
                    return (EntityCollection)Parameters["ChildEntities"];
                return default(EntityCollection);
            }
            set { Parameters["ChildEntities"] = value; }
        }
        public CompoundUpdateDuplicateDetectionRuleRequest()
        {
            this.ResponseType = new CompoundUpdateDuplicateDetectionRuleResponse();
            this.RequestName = "CompoundUpdateDuplicateDetectionRule";
        }
        internal override string GetRequestBody()
        {
            Parameters["Entity"] = Entity;
            Parameters["ChildEntities"] = ChildEntities;
            return GetSoapBody();
        }
    }
    public sealed class CompoundUpdateDuplicateDetectionRuleResponse : OrganizationResponse
    {
    }

    #endregion CompoundUpdateDuplicateDetectionRule

    #region ConvertDateAndTimeBehavior
    public sealed class ConvertDateAndTimeBehaviorRequest : OrganizationRequest
    {
        public EntityAttributeCollection Attributes
        {
            get
            {
                if (Parameters.Contains("Attributes"))
                    return (EntityAttributeCollection)Parameters["Attributes"];
                return default(EntityAttributeCollection);
            }
            set { Parameters["Attributes"] = value; }
        }
        public bool AutoConvert
        {
            get
            {
                if (Parameters.Contains("AutoConvert"))
                    return (bool)Parameters["AutoConvert"];
                return default(bool);
            }
            set { Parameters["AutoConvert"] = value; }
        }
        public string ConversionRule
        {
            get
            {
                if (Parameters.Contains("ConversionRule"))
                    return (string)Parameters["ConversionRule"];
                return default(string);
            }
            set { Parameters["ConversionRule"] = value; }
        }
        public int TimeZoneCode 
        {
            get
            {
                if (Parameters.Contains("TimeZoneCode"))
                    return (int)Parameters["TimeZoneCode"];
                return default(int);
            }
            set { Parameters["TimeZoneCode"] = value; }
        }
        public ConvertDateAndTimeBehaviorRequest()
        {
            this.ResponseType = new ConvertDateAndTimeBehaviorResponse();
            this.RequestName = "ConvertDateAndTimeBehavior";
        }
        internal override string GetRequestBody()
        {
            Parameters["Attributes"] = Attributes;
            Parameters["AutoConvert"] = AutoConvert;
            Parameters["ConversionRule"] = ConversionRule;
            Parameters["TimeZoneCode"] = TimeZoneCode;
            return GetSoapBody();
        }
    }
    public sealed class ConvertDateAndTimeBehaviorResponse : OrganizationResponse
    {
        public Guid JobId { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "JobId")
                    this.JobId = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion ConvertDateAndTimeBehavior

    #region ConvertKitToProduct
    public sealed class ConvertKitToProductRequest : OrganizationRequest
    {
        public Guid KitId
        {
            get
            {
                if (Parameters.Contains("KitId"))
                    return (Guid)Parameters["KitId"];
                return default(Guid);
            }
            set { Parameters["KitId"] = value; }
        }
        public ConvertKitToProductRequest()
        {
            this.ResponseType = new ConvertKitToProductResponse();
            this.RequestName = "ConvertKitToProduct";
        }
        internal override string GetRequestBody()
        {
            Parameters["KitId"] = KitId;
            return GetSoapBody();
        }
    }
    public sealed class ConvertKitToProductResponse : OrganizationResponse
    {
    }

    #endregion ConvertKitToProduct

    #region ConvertOwnerTeamToAccessTeam
    public sealed class ConvertOwnerTeamToAccessTeamRequest : OrganizationRequest
    {
        public Guid TeamId
        {
            get
            {
                if (Parameters.Contains("TeamId"))
                    return (Guid)Parameters["TeamId"];
                return default(Guid);
            }
            set { Parameters["TeamId"] = value; }
        }
        public ConvertOwnerTeamToAccessTeamRequest()
        {
            this.ResponseType = new ConvertOwnerTeamToAccessTeamResponse();
            this.RequestName = "ConvertOwnerTeamToAccessTeam";
        }
        internal override string GetRequestBody()
        {
            Parameters["TeamId"] = TeamId;
            return GetSoapBody();
        }
    }
    public sealed class ConvertOwnerTeamToAccessTeamResponse : OrganizationResponse
    {
    }

    #endregion ConvertOwnerTeamToAccessTeam

    #region ConvertProductToKit
    public sealed class ConvertProductToKitRequest : OrganizationRequest
    {
        public Guid ProductId
        {
            get
            {
                if (Parameters.Contains("ProductId"))
                    return (Guid)Parameters["ProductId"];
                return default(Guid);
            }
            set { Parameters["ProductId"] = value; }
        }
        public ConvertProductToKitRequest()
        {
            this.ResponseType = new ConvertProductToKitResponse();
            this.RequestName = "ConvertProductToKit";
        }
        internal override string GetRequestBody()
        {
            Parameters["ProductId"] = ProductId;
            return GetSoapBody();
        }
    }
    public sealed class ConvertProductToKitResponse : OrganizationResponse
    {
    }

    #endregion ConvertProductToKit

    #region ConvertQuoteToSalesOrder
    public sealed class ConvertQuoteToSalesOrderRequest : OrganizationRequest
    {
        public Guid QuoteId
        {
            get
            {
                if (Parameters.Contains("QuoteId"))
                    return (Guid)Parameters["QuoteId"];
                return default(Guid);
            }
            set { Parameters["QuoteId"] = value; }
        }
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
        public ConvertQuoteToSalesOrderRequest()
        {
            this.ResponseType = new ConvertQuoteToSalesOrderResponse();
            this.RequestName = "ConvertQuoteToSalesOrder";
        }
        internal override string GetRequestBody()
        {
            Parameters["QuoteId"] = QuoteId;
            Parameters["ColumnSet"] = ColumnSet;
            return GetSoapBody();
        }
    }
    public sealed class ConvertQuoteToSalesOrderResponse : OrganizationResponse
    {
        public Entity Entity { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "Entity")
                    this.Entity = Entity.LoadFromXml(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion ConvertQuoteToSalesOrder

    #region ConvertSalesOrderToInvoice
    public sealed class ConvertSalesOrderToInvoiceRequest : OrganizationRequest
    {
        public Guid SalesOrderId
        {
            get
            {
                if (Parameters.Contains("SalesOrderId"))
                    return (Guid)Parameters["SalesOrderId"];
                return default(Guid);
            }
            set { Parameters["SalesOrderId"] = value; }
        }
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
        public ConvertSalesOrderToInvoiceRequest()
        {
            this.ResponseType = new ConvertSalesOrderToInvoiceResponse();
            this.RequestName = "ConvertSalesOrderToInvoice";
        }
        internal override string GetRequestBody()
        {
            Parameters["SalesOrderId"] = SalesOrderId;
            Parameters["ColumnSet"] = ColumnSet;
            return GetSoapBody();
        }
    }
    public sealed class ConvertSalesOrderToInvoiceResponse : OrganizationResponse
    {
        public Entity Entity { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "Entity")
                    this.Entity = Entity.LoadFromXml(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion ConvertSalesOrderToInvoice

    #region CopyCampaign
    public sealed class CopyCampaignRequest : OrganizationRequest
    {
        public Guid BaseCampaign
        {
            get
            {
                if (Parameters.Contains("BaseCampaign"))
                    return (Guid)Parameters["BaseCampaign"];
                return default(Guid);
            }
            set { Parameters["BaseCampaign"] = value; }
        }
        public bool SaveAsTemplate
        {
            get
            {
                if (Parameters.Contains("SaveAsTemplate"))
                    return (bool)Parameters["SaveAsTemplate"];
                return default(bool);
            }
            set { Parameters["SaveAsTemplate"] = value; }
        }
        public CopyCampaignRequest()
        {
            this.ResponseType = new CopyCampaignResponse();
            this.RequestName = "CopyCampaign";
        }
        internal override string GetRequestBody()
        {
            Parameters["BaseCampaign"] = BaseCampaign;
            Parameters["SaveAsTemplate"] = SaveAsTemplate;
            return GetSoapBody();
        }
    }
    public sealed class CopyCampaignResponse : OrganizationResponse
    {
        public Guid CampaignCopyId { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "CampaignCopyId")
                    this.CampaignCopyId = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion CopyCampaign

    #region CopyCampaignResponse
    public sealed class CopyCampaignResponseRequest : OrganizationRequest
    {
        public EntityReference CampaignResponseId
        {
            get
            {
                if (Parameters.Contains("CampaignResponseId"))
                    return (EntityReference)Parameters["CampaignResponseId"];
                return default(EntityReference);
            }
            set { Parameters["CampaignResponseId"] = value; }
        }
        public CopyCampaignResponseRequest()
        {
            this.ResponseType = new CopyCampaignResponseResponse();
            this.RequestName = "CopyCampaignResponse";
        }
        internal override string GetRequestBody()
        {
            Parameters["CampaignResponseId"] = CampaignResponseId;
            return GetSoapBody();
        }
    }
    public sealed class CopyCampaignResponseResponse : OrganizationResponse
    {
        public Guid CampaignResponseId { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "CampaignResponseId")
                    this.CampaignResponseId = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion CopyCampaignResponse

    #region CopyDynamicListToStatic
    public sealed class CopyDynamicListToStaticRequest : OrganizationRequest
    {
        public Guid ListId
        {
            get
            {
                if (Parameters.Contains("ListId"))
                    return (Guid)Parameters["ListId"];
                return default(Guid);
            }
            set { Parameters["ListId"] = value; }
        }
        public CopyDynamicListToStaticRequest()
        {
            this.ResponseType = new CopyDynamicListToStaticResponse();
            this.RequestName = "CopyDynamicListToStatic";
        }
        internal override string GetRequestBody()
        {
            Parameters["ListId"] = ListId;
            return GetSoapBody();
        }
    }
    public sealed class CopyDynamicListToStaticResponse : OrganizationResponse
    {
        public Guid StaticListId { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "StaticListId")
                    this.StaticListId = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion CopyDynamicListToStatic

    #region CopyMembersList
    public sealed class CopyMembersListRequest : OrganizationRequest
    {
        public Guid SourceListId
        {
            get
            {
                if (Parameters.Contains("SourceListId"))
                    return (Guid)Parameters["SourceListId"];
                return default(Guid);
            }
            set { Parameters["SourceListId"] = value; }
        }
        public Guid TargetListId
        {
            get
            {
                if (Parameters.Contains("TargetListId"))
                    return (Guid)Parameters["TargetListId"];
                return default(Guid);
            }
            set { Parameters["TargetListId"] = value; }
        }
        public CopyMembersListRequest()
        {
            this.ResponseType = new CopyMembersListResponse();
            this.RequestName = "CopyMembersList";
        }
        internal override string GetRequestBody()
        {
            Parameters["SourceListId"] = SourceListId;
            Parameters["TargetListId"] = TargetListId;
            return GetSoapBody();
        }
    }
    public sealed class CopyMembersListResponse : OrganizationResponse
    {
    }

    #endregion CopyMembersList

    #region CopySystemForm
    public sealed class CopySystemFormRequest : OrganizationRequest
    {
        public Guid SourceId
        {
            get
            {
                if (Parameters.Contains("SourceId"))
                    return (Guid)Parameters["SourceId"];
                return default(Guid);
            }
            set { Parameters["SourceId"] = value; }
        }
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
        public CopySystemFormRequest()
        {
            this.ResponseType = new CopySystemFormResponse();
            this.RequestName = "CopySystemForm";
        }
        internal override string GetRequestBody()
        {
            Parameters["SourceId"] = SourceId;
            Parameters["Target"] = Target;
            return GetSoapBody();
        }
    }
    public sealed class CopySystemFormResponse : OrganizationResponse
    {
        public Guid Id { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "Id")
                    this.Id = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion CopySystemForm

    #region CreateActivitiesList
    public sealed class CreateActivitiesListRequest : OrganizationRequest
    {
        public Entity Activity
        {
            get
            {
                if (Parameters.Contains("Activity"))
                    return (Entity)Parameters["Activity"];
                return default(Entity);
            }
            set { Parameters["Activity"] = value; }
        }
        public string FriendlyName
        {
            get
            {
                if (Parameters.Contains("FriendlyName"))
                    return (string)Parameters["FriendlyName"];
                return default(string);
            }
            set { Parameters["FriendlyName"] = value; }
        }
        public Guid ListId
        {
            get
            {
                if (Parameters.Contains("ListId"))
                    return (Guid)Parameters["ListId"];
                return default(Guid);
            }
            set { Parameters["ListId"] = value; }
        }
        public EntityReference Owner
        {
            get
            {
                if (Parameters.Contains("Owner"))
                    return (EntityReference)Parameters["Owner"];
                return default(EntityReference);
            }
            set { Parameters["Owner"] = value; }
        }
        public PropagationOwnershipOptions OwnershipOptions
        {
            get
            {
                if (Parameters.Contains("OwnershipOptions"))
                    return (PropagationOwnershipOptions)Parameters["OwnershipOptions"];
                return default(PropagationOwnershipOptions);
            }
            set { Parameters["OwnershipOptions"] = value; }
        }
        public bool PostWorkflowEvent
        {
            get
            {
                if (Parameters.Contains("PostWorkflowEvent"))
                    return (bool)Parameters["PostWorkflowEvent"];
                return default(bool);
            }
            set { Parameters["PostWorkflowEvent"] = value; }
        }
        public bool Propagate
        {
            get
            {
                if (Parameters.Contains("Propagate"))
                    return (bool)Parameters["Propagate"];
                return default(bool);
            }
            set { Parameters["Propagate"] = value; }
        }
        public Guid QueueId
        {
            get
            {
                if (Parameters.Contains("QueueId"))
                    return (Guid)Parameters["QueueId"];
                return default(Guid);
            }
            set { Parameters["QueueId"] = value; }
        }
        public bool sendEmail
        {
            get
            {
                if (Parameters.Contains("sendEmail"))
                    return (bool)Parameters["sendEmail"];
                return default(bool);
            }
            set { Parameters["sendEmail"] = value; }
        }
        public Guid TemplateId
        {
            get
            {
                if (Parameters.Contains("TemplateId"))
                    return (Guid)Parameters["TemplateId"];
                return default(Guid);
            }
            set { Parameters["TemplateId"] = value; }
        }
        public CreateActivitiesListRequest()
        {
            this.ResponseType = new CreateActivitiesListResponse();
            this.RequestName = "CreateActivitiesList";
        }
        internal override string GetRequestBody()
        {
            Parameters["Activity"] = Activity;
            Parameters["FriendlyName"] = FriendlyName;
            Parameters["ListId"] = ListId;
            Parameters["Owner"] = Owner;
            Parameters["OwnershipOptions"] = OwnershipOptions;
            Parameters["PostWorkflowEvent"] = PostWorkflowEvent;
            Parameters["Propagate"] = Propagate;
            Parameters["QueueId"] = QueueId;
            Parameters["sendEmail"] = sendEmail;
            Parameters["TemplateId"] = TemplateId;
            return GetSoapBody();
        }
    }
    public sealed class CreateActivitiesListResponse : OrganizationResponse
    {
        public Guid BulkOperationId { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "BulkOperationId")
                    this.BulkOperationId = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion CreateActivitiesList
  
    #region CreateException
    public sealed class CreateExceptionRequest : OrganizationRequest
    {
        public bool IsDeleted
        {
            get
            {
                if (Parameters.Contains("IsDeleted"))
                    return (bool)Parameters["IsDeleted"];
                return default(bool);
            }
            set { Parameters["IsDeleted"] = value; }
        }
        public DateTime OriginalStartDate
        {
            get
            {
                if (Parameters.Contains("OriginalStartDate"))
                    return (DateTime)Parameters["OriginalStartDate"];
                return default(DateTime);
            }
            set { Parameters["OriginalStartDate"] = value; }
        }
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
        public CreateExceptionRequest()
        {
            this.ResponseType = new CreateExceptionResponse();
            this.RequestName = "CreateException";
        }
        internal override string GetRequestBody()
        {
            Parameters["IsDeleted"] = IsDeleted;
            Parameters["OriginalStartDate"] = OriginalStartDate;
            Parameters["Target"] = Target;
            return GetSoapBody();
        }
    }
    public sealed class CreateExceptionResponse : OrganizationResponse
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

    #endregion CreateException

    #region CreateInstance
    public sealed class CreateInstanceRequest : OrganizationRequest
    {
        public int Count
        {
            get
            {
                if (Parameters.Contains("Count"))
                    return (int)Parameters["Count"];
                return default(int);
            }
            set { Parameters["Count"] = value; }
        }
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
        public CreateInstanceRequest()
        {
            this.ResponseType = new CreateInstanceResponse();
            this.RequestName = "CreateInstance";
        }
        internal override string GetRequestBody()
        {
            Parameters["Count"] = Count;
            Parameters["Target"] = Target;
            return GetSoapBody();
        }
    }
    public sealed class CreateInstanceResponse : OrganizationResponse
    {
        public bool SeriesCanBeExpanded { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "SeriesCanBeExpanded")
                    this.SeriesCanBeExpanded = Util.LoadFromXml<bool>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion CreateInstance

    #region CreateWorkflowFromTemplate
    public sealed class CreateWorkflowFromTemplateRequest : OrganizationRequest
    {
        public string WorkflowName
        {
            get
            {
                if (Parameters.Contains("WorkflowName"))
                    return (string)Parameters["WorkflowName"];
                return default(string);
            }
            set { Parameters["WorkflowName"] = value; }
        }
        public Guid WorkflowTemplateId
        {
            get
            {
                if (Parameters.Contains("WorkflowTemplateId"))
                    return (Guid)Parameters["WorkflowTemplateId"];
                return default(Guid);
            }
            set { Parameters["WorkflowTemplateId"] = value; }
        }
        public CreateWorkflowFromTemplateRequest()
        {
            this.ResponseType = new CreateWorkflowFromTemplateResponse();
            this.RequestName = "CreateWorkflowFromTemplate";
        }
        internal override string GetRequestBody()
        {
            Parameters["WorkflowName"] = WorkflowName;
            Parameters["WorkflowTemplateId"] = WorkflowTemplateId;
            return GetSoapBody();
        }
    }
    public sealed class CreateWorkflowFromTemplateResponse : OrganizationResponse
    {
        public Guid Id { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "Id")
                    this.Id = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion CreateWorkflowFromTemplate

    #region DeleteAuditData
    public sealed class DeleteAuditDataRequest : OrganizationRequest
    {
        public DateTime EndDate
        {
            get
            {
                if (Parameters.Contains("EndDate"))
                    return (DateTime)Parameters["EndDate"];
                return default(DateTime);
            }
            set { Parameters["EndDate"] = value; }
        }
        public DeleteAuditDataRequest()
        {
            this.ResponseType = new DeleteAuditDataResponse();
            this.RequestName = "DeleteAuditData";
        }
        internal override string GetRequestBody()
        {
            Parameters["EndDate"] = EndDate;
            return GetSoapBody();
        }
    }
    public sealed class DeleteAuditDataResponse : OrganizationResponse
    {
        public int PartitionsDeleted { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "PartitionsDeleted")
                    this.PartitionsDeleted = Util.LoadFromXml<int>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion DeleteAuditData

    #region DeleteOpenInstances
    public sealed class DeleteOpenInstancesRequest : OrganizationRequest
    {
        public DateTime SeriesEndDate
        {
            get
            {
                if (Parameters.Contains("SeriesEndDate"))
                    return (DateTime)Parameters["SeriesEndDate"];
                return default(DateTime);
            }
            set { Parameters["SeriesEndDate"] = value; }
        }
        public int StateOfPastInstances
        {
            get
            {
                if (Parameters.Contains("StateOfPastInstances"))
                    return (int)Parameters["StateOfPastInstances"];
                return default(int);
            }
            set { Parameters["StateOfPastInstances"] = value; }
        }
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
        public DeleteOpenInstancesRequest()
        {
            this.ResponseType = new DeleteOpenInstancesResponse();
            this.RequestName = "DeleteOpenInstances";
        }
        internal override string GetRequestBody()
        {
            Parameters["SeriesEndDate"] = SeriesEndDate;
            Parameters["StateOfPastInstances"] = StateOfPastInstances;
            Parameters["Target"] = Target;
            return GetSoapBody();
        }
    }
    public sealed class DeleteOpenInstancesResponse : OrganizationResponse
    {
    }

    #endregion DeleteOpenInstances

    #region DeliverIncomingEmail
    public sealed class DeliverIncomingEmailRequest : OrganizationRequest
    {
        public EntityCollection Attachments
        {
            get
            {
                if (Parameters.Contains("Attachments"))
                    return (EntityCollection)Parameters["Attachments"];
                return default(EntityCollection);
            }
            set { Parameters["Attachments"] = value; }
        }
        public string Bcc
        {
            get
            {
                if (Parameters.Contains("Bcc"))
                    return (string)Parameters["Bcc"];
                return default(string);
            }
            set { Parameters["Bcc"] = value; }
        }
        public string Body
        {
            get
            {
                if (Parameters.Contains("Body"))
                    return (string)Parameters["Body"];
                return default(string);
            }
            set { Parameters["Body"] = value; }
        }
        public string Cc
        {
            get
            {
                if (Parameters.Contains("Cc"))
                    return (string)Parameters["Cc"];
                return default(string);
            }
            set { Parameters["Cc"] = value; }
        }
        public Entity ExtraProperties
        {
            get
            {
                if (Parameters.Contains("ExtraProperties"))
                    return (Entity)Parameters["ExtraProperties"];
                return default(Entity);
            }
            set { Parameters["ExtraProperties"] = value; }
        }
        public string From
        {
            get
            {
                if (Parameters.Contains("From"))
                    return (string)Parameters["From"];
                return default(string);
            }
            set { Parameters["From"] = value; }
        }
        public string Importance
        {
            get
            {
                if (Parameters.Contains("Importance"))
                    return (string)Parameters["Importance"];
                return default(string);
            }
            set { Parameters["Importance"] = value; }
        }
        public string MessageId
        {
            get
            {
                if (Parameters.Contains("MessageId"))
                    return (string)Parameters["MessageId"];
                return default(string);
            }
            set { Parameters["MessageId"] = value; }
        }
        public DateTime ReceivedOn
        {
            get
            {
                if (Parameters.Contains("ReceivedOn"))
                    return (DateTime)Parameters["ReceivedOn"];
                return default(DateTime);
            }
            set { Parameters["ReceivedOn"] = value; }
        }
        public string Subject
        {
            get
            {
                if (Parameters.Contains("Subject"))
                    return (string)Parameters["Subject"];
                return default(string);
            }
            set { Parameters["Subject"] = value; }
        }
        public string SubmittedBy
        {
            get
            {
                if (Parameters.Contains("SubmittedBy"))
                    return (string)Parameters["SubmittedBy"];
                return default(string);
            }
            set { Parameters["SubmittedBy"] = value; }
        }
        public string To
        {
            get
            {
                if (Parameters.Contains("To"))
                    return (string)Parameters["To"];
                return default(string);
            }
            set { Parameters["To"] = value; }
        }
        public bool ValidateBeforeCreate
        {
            get
            {
                if (Parameters.Contains("ValidateBeforeCreate"))
                    return (bool)Parameters["ValidateBeforeCreate"];
                return default(bool);
            }
            set { Parameters["ValidateBeforeCreate"] = value; }
        }
        public DeliverIncomingEmailRequest()
        {
            this.ResponseType = new DeliverIncomingEmailResponse();
            this.RequestName = "DeliverIncomingEmail";
        }
        internal override string GetRequestBody()
        {
            Parameters["Attachments"] = Attachments;
            Parameters["Bcc"] = Bcc;
            Parameters["Body"] = Body;
            Parameters["Cc"] = Cc;
            Parameters["ExtraProperties"] = ExtraProperties;
            Parameters["From"] = From;
            Parameters["Importance"] = Importance;
            Parameters["MessageId"] = MessageId;
            Parameters["ReceivedOn"] = ReceivedOn;
            Parameters["Subject"] = Subject;
            Parameters["SubmittedBy"] = SubmittedBy;
            Parameters["To"] = To;
            Parameters["ValidateBeforeCreate"] = ValidateBeforeCreate;
            return GetSoapBody();
        }
    }
    public sealed class DeliverIncomingEmailResponse : OrganizationResponse
    {
        public Guid EmailId { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "EmailId")
                    this.EmailId = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion DeliverIncomingEmail

    #region DeliverPromoteEmail
    public sealed class DeliverPromoteEmailRequest : OrganizationRequest
    {
        public EntityCollection Attachments
        {
            get
            {
                if (Parameters.Contains("Attachments"))
                    return (EntityCollection)Parameters["Attachments"];
                return default(EntityCollection);
            }
            set { Parameters["Attachments"] = value; }
        }
        public string Bcc
        {
            get
            {
                if (Parameters.Contains("Bcc"))
                    return (string)Parameters["Bcc"];
                return default(string);
            }
            set { Parameters["Bcc"] = value; }
        }
        public string Body
        {
            get
            {
                if (Parameters.Contains("Body"))
                    return (string)Parameters["Body"];
                return default(string);
            }
            set { Parameters["Body"] = value; }
        }
        public string Cc
        {
            get
            {
                if (Parameters.Contains("Cc"))
                    return (string)Parameters["Cc"];
                return default(string);
            }
            set { Parameters["Cc"] = value; }
        }
        public Guid EmailId
        {
            get
            {
                if (Parameters.Contains("EmailId"))
                    return (Guid)Parameters["EmailId"];
                return default(Guid);
            }
            set { Parameters["EmailId"] = value; }
        }
        public Entity ExtraProperties
        {
            get
            {
                if (Parameters.Contains("ExtraProperties"))
                    return (Entity)Parameters["ExtraProperties"];
                return default(Entity);
            }
            set { Parameters["ExtraProperties"] = value; }
        }
        public string From
        {
            get
            {
                if (Parameters.Contains("From"))
                    return (string)Parameters["From"];
                return default(string);
            }
            set { Parameters["From"] = value; }
        }
        public string Importance
        {
            get
            {
                if (Parameters.Contains("Importance"))
                    return (string)Parameters["Importance"];
                return default(string);
            }
            set { Parameters["Importance"] = value; }
        }
        public string MessageId
        {
            get
            {
                if (Parameters.Contains("MessageId"))
                    return (string)Parameters["MessageId"];
                return default(string);
            }
            set { Parameters["MessageId"] = value; }
        }
        public DateTime ReceivedOn
        {
            get
            {
                if (Parameters.Contains("ReceivedOn"))
                    return (DateTime)Parameters["ReceivedOn"];
                return default(DateTime);
            }
            set { Parameters["ReceivedOn"] = value; }
        }
        public string Subject
        {
            get
            {
                if (Parameters.Contains("Subject"))
                    return (string)Parameters["Subject"];
                return default(string);
            }
            set { Parameters["Subject"] = value; }
        }
        public string SubmittedBy
        {
            get
            {
                if (Parameters.Contains("SubmittedBy"))
                    return (string)Parameters["SubmittedBy"];
                return default(string);
            }
            set { Parameters["SubmittedBy"] = value; }
        }
        public string To
        {
            get
            {
                if (Parameters.Contains("To"))
                    return (string)Parameters["To"];
                return default(string);
            }
            set { Parameters["To"] = value; }
        }
        public DeliverPromoteEmailRequest()
        {
            this.ResponseType = new DeliverPromoteEmailResponse();
            this.RequestName = "DeliverPromoteEmail";
        }
        internal override string GetRequestBody()
        {
            this.Parameters["Attachments"] = Attachments;
            this.Parameters["Bcc"] = Bcc;
            this.Parameters["Body"] = Body;
            this.Parameters["Cc"] = Cc;
            this.Parameters["EmailId"] = EmailId;
            this.Parameters["ExtraProperties"] = ExtraProperties;
            this.Parameters["From"] = From;
            this.Parameters["Importance"] = Importance;
            this.Parameters["MessageId"] = MessageId;
            this.Parameters["ReceivedOn"] = ReceivedOn;
            this.Parameters["Subject"] = Subject;
            this.Parameters["SubmittedBy"] = SubmittedBy;
            this.Parameters["To"] = To;
            return GetSoapBody();
        }
    }
    public sealed class DeliverPromoteEmailResponse : OrganizationResponse
    {
        public Guid EmailId { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "EmailId")
                    this.EmailId = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion DeliverPromoteEmail

    #region DeprovisionLanguage
    public sealed class DeprovisionLanguageRequest : OrganizationRequest
    {
        public int Language
        {
            get
            {
                if (Parameters.Contains("Language"))
                    return (int)Parameters["Language"];
                return default(int);
            }
            set { Parameters["Language"] = value; }
        }
        public DeprovisionLanguageRequest()
        {
            this.ResponseType = new DeprovisionLanguageResponse();
            this.RequestName = "DeprovisionLanguage";
        }
        internal override string GetRequestBody()
        {
            Parameters["Language"] = Language;
            return GetSoapBody();
        }
    }
    public sealed class DeprovisionLanguageResponse : OrganizationResponse
    {
    }

    #endregion DeprovisionLanguage

    #region DisassociateEntities
    public sealed class DisassociateEntitiesRequest : OrganizationRequest
    {
        public EntityReference Moniker1
        {
            get
            {
                if (Parameters.Contains("Moniker1"))
                    return (EntityReference)Parameters["Moniker1"];
                return default(EntityReference);
            }
            set { Parameters["Moniker1"] = value; }
        }
        public EntityReference Moniker2
        {
            get
            {
                if (Parameters.Contains("Moniker2"))
                    return (EntityReference)Parameters["Moniker2"];
                return default(EntityReference);
            }
            set { Parameters["Moniker2"] = value; }
        }
        public string RelationshipName
        {
            get
            {
                if (Parameters.Contains("RelationshipName"))
                    return (string)Parameters["RelationshipName"];
                return default(string);
            }
            set { Parameters["RelationshipName"] = value; }
        }
        public DisassociateEntitiesRequest()
        {
            this.ResponseType = new DisassociateEntitiesResponse();
            this.RequestName = "DisassociateEntities";
        }
        internal override string GetRequestBody()
        {
            Parameters["Moniker1"] = Moniker1;
            Parameters["Moniker2"] = Moniker2;
            Parameters["RelationshipName"] = RelationshipName;
            return GetSoapBody();
        }
    }
    public sealed class DisassociateEntitiesResponse : OrganizationResponse
    {
    }

    #endregion DisassociateEntities

    #region DistributeCampaignActivity
    public sealed class DistributeCampaignActivityRequest : OrganizationRequest
    {
        public Entity Activity
        {
            get
            {
                if (Parameters.Contains("Activity"))
                    return (Entity)Parameters["Activity"];
                return default(Entity);
            }
            set { Parameters["Activity"] = value; }
        }
        public Guid CampaignActivityId
        {
            get
            {
                if (Parameters.Contains("CampaignActivityId"))
                    return (Guid)Parameters["CampaignActivityId"];
                return default(Guid);
            }
            set { Parameters["CampaignActivityId"] = value; }
        }
        public EntityReference Owner
        {
            get
            {
                if (Parameters.Contains("Owner"))
                    return (EntityReference)Parameters["Owner"];
                return default(EntityReference);
            }
            set { Parameters["Owner"] = value; }
        }
        public PropagationOwnershipOptions OwnershipOptions
        {
            get
            {
                if (Parameters.Contains("OwnershipOptions"))
                    return (PropagationOwnershipOptions)Parameters["OwnershipOptions"];
                return default(PropagationOwnershipOptions);
            }
            set { Parameters["OwnershipOptions"] = value; }
        }
        public bool PostWorkflowEvent
        {
            get
            {
                if (Parameters.Contains("PostWorkflowEvent"))
                    return (bool)Parameters["PostWorkflowEvent"];
                return default(bool);
            }
            set { Parameters["PostWorkflowEvent"] = value; }
        }
        public bool Propagate
        {
            get
            {
                if (Parameters.Contains("Propagate"))
                    return (bool)Parameters["Propagate"];
                return default(bool);
            }
            set { Parameters["Propagate"] = value; }
        }
        public Guid QueueId
        {
            get
            {
                if (Parameters.Contains("QueueId"))
                    return (Guid)Parameters["QueueId"];
                return default(Guid);
            }
            set { Parameters["QueueId"] = value; }
        }
        public bool SendEmail
        {
            get
            {
                if (Parameters.Contains("SendEmail"))
                    return (bool)Parameters["SendEmail"];
                return default(bool);
            }
            set { Parameters["SendEmail"] = value; }
        }
        public Guid TemplateId
        {
            get
            {
                if (Parameters.Contains("TemplateId"))
                    return (Guid)Parameters["TemplateId"];
                return default(Guid);
            }
            set { Parameters["TemplateId"] = value; }
        }
        public DistributeCampaignActivityRequest()
        {
            this.ResponseType = new DistributeCampaignActivityResponse();
            this.RequestName = "DistributeCampaignActivity";
        }
        internal override string GetRequestBody()
        {
            Parameters["Activity"] = Activity;
            Parameters["CampaignActivityId"] = CampaignActivityId;
            Parameters["Owner"] = Owner;
            Parameters["OwnershipOptions"] = OwnershipOptions;
            Parameters["PostWorkflowEvent"] = PostWorkflowEvent;
            Parameters["Propagate"] = Propagate;
            Parameters["QueueId"] = QueueId;
            Parameters["SendEmail"] = SendEmail;
            Parameters["TemplateId"] = TemplateId;
            return GetSoapBody();
        }
    }
    public sealed class DistributeCampaignActivityResponse : OrganizationResponse
    {
        public Guid BulkOperationId { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "BulkOperationId")
                    this.BulkOperationId = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion DistributeCampaignActivity

    #region DownloadReportDefinition
    public sealed class DownloadReportDefinitionRequest : OrganizationRequest
    {
        public Guid ReportId
        {
            get
            {
                if (Parameters.Contains("ReportId"))
                    return (Guid)Parameters["ReportId"];
                return default(Guid);
            }
            set { Parameters["ReportId"] = value; }
        }
        public DownloadReportDefinitionRequest()
        {
            this.ResponseType = new DownloadReportDefinitionResponse();
            this.RequestName = "DownloadReportDefinition";
        }
        internal override string GetRequestBody()
        {
            Parameters["ReportId"] = ReportId;
            return GetSoapBody();
        }
    }
    public sealed class DownloadReportDefinitionResponse : OrganizationResponse
    {
        public string BodyText { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "BodyText")
                    this.BodyText = result.Element(Util.ns.b + "value").Value;
            }
        }
    }

    #endregion DownloadReportDefinition

    #region ExecuteByIdSavedQuery
    public sealed class ExecuteByIdSavedQueryRequest : OrganizationRequest
    {
        public Guid EntityId
        {
            get
            {
                if (Parameters.Contains("EntityId"))
                    return (Guid)Parameters["EntityId"];
                return default(Guid);
            }
            set { Parameters["EntityId"] = value; }
        }
        public ExecuteByIdSavedQueryRequest()
        {
            this.ResponseType = new ExecuteByIdSavedQueryResponse();
            this.RequestName = "ExecuteByIdSavedQuery";
        }
        internal override string GetRequestBody()
        {
            Parameters["EntityId"] = EntityId;
            return GetSoapBody();
        }
    }
    public sealed class ExecuteByIdSavedQueryResponse : OrganizationResponse
    {
        public string String { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "String")
                    this.String = result.Element(Util.ns.b + "value").Value;
            }
        }
    }

    #endregion ExecuteByIdSavedQuery

    #region ExecuteByIdUserQuery
    public sealed class ExecuteByIdUserQueryRequest : OrganizationRequest
    {
        public EntityReference EntityId
        {
            get
            {
                if (Parameters.Contains("EntityId"))
                    return (EntityReference)Parameters["EntityId"];
                return default(EntityReference);
            }
            set { Parameters["EntityId"] = value; }
        }
        public ExecuteByIdUserQueryRequest()
        {
            this.ResponseType = new ExecuteByIdUserQueryResponse();
            this.RequestName = "ExecuteByIdUserQuery";
        }
        internal override string GetRequestBody()
        {
            Parameters["EntityId"] = EntityId;
            return GetSoapBody();
        }
    }
    public sealed class ExecuteByIdUserQueryResponse : OrganizationResponse
    {
        public string String { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "String")
                    this.String = result.Element(Util.ns.b + "value").Value;
            }
        }
    }

    #endregion ExecuteByIdUserQuery

    #region ExecuteFetch
    public sealed class ExecuteFetchRequest : OrganizationRequest
    {
        public string FetchXml
        {
            get
            {
                if (Parameters.Contains("FetchXml"))
                    return (string)Parameters["FetchXml"];
                return default(string);
            }
            set { Parameters["FetchXml"] = value; }
        }
        public ExecuteFetchRequest()
        {
            this.ResponseType = new ExecuteFetchResponse();
            this.RequestName = "ExecuteFetch";
        }
        internal override string GetRequestBody()
        {
            Parameters["FetchXml"] = FetchXml;
            return GetSoapBody();
        }
    }
    public sealed class ExecuteFetchResponse : OrganizationResponse
    {
        public string FetchXmlResult { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "FetchXmlResult")
                    this.FetchXmlResult = result.Element(Util.ns.b + "value").Value;
            }
        }
    }

    #endregion ExecuteFetch

    #region ExecuteWorkflow
    public sealed class ExecuteWorkflowRequest : OrganizationRequest
    {
        public Guid EntityId
        {
            get
            {
                if (Parameters.Contains("EntityId"))
                    return (Guid)Parameters["EntityId"];
                return default(Guid);
            }
            set { Parameters["EntityId"] = value; }
        }
        public Guid WorkflowId
        {
            get
            {
                if (Parameters.Contains("WorkflowId"))
                    return (Guid)Parameters["WorkflowId"];
                return default(Guid);
            }
            set { Parameters["WorkflowId"] = value; }
        }
        public ExecuteWorkflowRequest()
        {
            this.ResponseType = new ExecuteWorkflowResponse();
            this.RequestName = "ExecuteWorkflow";
        }
        internal override string GetRequestBody()
        {
            Parameters["EntityId"] = EntityId;
            Parameters["WorkflowId"] = WorkflowId;
            return GetSoapBody();
        }
    }
    public sealed class ExecuteWorkflowResponse : OrganizationResponse
    {
        public Guid Id { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "Id")
                    this.Id = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion ExecuteWorkflow

    #region ExpandCalendar
    public sealed class ExpandCalendarRequest : OrganizationRequest
    {
        public Guid CalendarId
        {
            get
            {
                if (Parameters.Contains("CalendarId"))
                    return (Guid)Parameters["CalendarId"];
                return default(Guid);
            }
            set { Parameters["CalendarId"] = value; }
        }
        public DateTime Start
        {
            get
            {
                if (Parameters.Contains("Start"))
                    return (DateTime)Parameters["Start"];
                return default(DateTime);
            }
            set { Parameters["Start"] = value; }
        }
        public DateTime End
        {
            get
            {
                if (Parameters.Contains("End"))
                    return (DateTime)Parameters["End"];
                return default(DateTime);
            }
            set { Parameters["End"] = value; }
        }
        public ExpandCalendarRequest()
        {
            this.ResponseType = new ExpandCalendarResponse();
            this.RequestName = "ExpandCalendar";
        }
        internal override string GetRequestBody()
        {
            Parameters["CalendarId"] = CalendarId;
            Parameters["Start"] = Start;
            Parameters["End"] = End;
            return GetSoapBody();
        }
    }
    public sealed class ExpandCalendarResponse : OrganizationResponse
    {
        public TimeInfo[] result { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "result")
                {
                    List<TimeInfo> list = new List<TimeInfo>();
                    foreach (XElement item in result.Element(Util.ns.b + "value").Elements(Util.ns.g + "TimeInfo"))
                    {
                        list.Add(TimeInfo.LoadFromXml(item));
                    }
                    this.result = list.ToArray();
                }
            }
        }
    }

    #endregion ExpandCalendar

    #region ExportFieldTranslation
    public sealed class ExportFieldTranslationRequest : OrganizationRequest
    {        
        public ExportFieldTranslationRequest()
        {
            this.ResponseType = new ExportFieldTranslationResponse();
            this.RequestName = "ExportFieldTranslation";
        }
        internal override string GetRequestBody()
        {           
            return GetSoapBody();
        }
    }
    public sealed class ExportFieldTranslationResponse : OrganizationResponse
    {
        public byte[] ExportTranslationFile { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "ExportTranslationFile")
                {
                    ExportTranslationFile = System.Convert.FromBase64String(result.Element(Util.ns.b + "value").Value);
                }
            }
        }
    }

    #endregion ExportFieldTranslation

    #region ExportMappingsImportMap
    public sealed class ExportMappingsImportMapRequest : OrganizationRequest
    {
        public bool ExportIds
        {
            get
            {
                if (Parameters.Contains("ExportIds"))
                    return (bool)Parameters["ExportIds"];
                return default(bool);
            }
            set { Parameters["ExportIds"] = value; }
        }
        public Guid ImportMapId
        {
            get
            {
                if (Parameters.Contains("ImportMapId"))
                    return (Guid)Parameters["ImportMapId"];
                return default(Guid);
            }
            set { Parameters["ImportMapId"] = value; }
        }
        public ExportMappingsImportMapRequest()
        {
            this.ResponseType = new ExportMappingsImportMapResponse();
            this.RequestName = "ExportMappingsImportMap";
        }
        internal override string GetRequestBody()
        {
            Parameters["ExportIds"] = ExportIds;
            Parameters["ImportMapId"] = ImportMapId;
            return GetSoapBody();
        }
    }
    public sealed class ExportMappingsImportMapResponse : OrganizationResponse
    {
        public string MappingsXml { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "MappingsXml")
                    this.MappingsXml = result.Element(Util.ns.b + "value").Value;
            }
        }
    }

    #endregion ExportMappingsImportMap

    #region ExportSolution
    public sealed class ExportSolutionRequest : OrganizationRequest
    {
        public bool ExportAutoNumberingSettings
        {
            get
            {
                if (Parameters.Contains("ExportAutoNumberingSettings"))
                    return (bool)Parameters["ExportAutoNumberingSettings"];
                return default(bool);
            }
            set { Parameters["ExportAutoNumberingSettings"] = value; }
        }
        public bool ExportCalendarSettings
        {
            get
            {
                if (Parameters.Contains("ExportCalendarSettings"))
                    return (bool)Parameters["ExportCalendarSettings"];
                return default(bool);
            }
            set { Parameters["ExportCalendarSettings"] = value; }
        }
        public bool ExportCustomizationSettings
        {
            get
            {
                if (Parameters.Contains("ExportCustomizationSettings"))
                    return (bool)Parameters["ExportCustomizationSettings"];
                return default(bool);
            }
            set { Parameters["ExportCustomizationSettings"] = value; }
        }
        public bool ExportEmailTrackingSettings
        {
            get
            {
                if (Parameters.Contains("ExportEmailTrackingSettings"))
                    return (bool)Parameters["ExportEmailTrackingSettings"];
                return default(bool);
            }
            set { Parameters["ExportEmailTrackingSettings"] = value; }
        }
        public bool ExportGeneralSettings
        {
            get
            {
                if (Parameters.Contains("ExportGeneralSettings"))
                    return (bool)Parameters["ExportGeneralSettings"];
                return default(bool);
            }
            set { Parameters["ExportGeneralSettings"] = value; }
        }
        public bool ExportIsvConfig
        {
            get
            {
                if (Parameters.Contains("ExportIsvConfig"))
                    return (bool)Parameters["ExportIsvConfig"];
                return default(bool);
            }
            set { Parameters["ExportIsvConfig"] = value; }
        }
        public bool ExportMarketingSettings
        {
            get
            {
                if (Parameters.Contains("ExportMarketingSettings"))
                    return (bool)Parameters["ExportMarketingSettings"];
                return default(bool);
            }
            set { Parameters["ExportMarketingSettings"] = value; }
        }
        public bool ExportOutlookSynchronizationSettings
        {
            get
            {
                if (Parameters.Contains("ExportOutlookSynchronizationSettings"))
                    return (bool)Parameters["ExportOutlookSynchronizationSettings"];
                return default(bool);
            }
            set { Parameters["ExportOutlookSynchronizationSettings"] = value; }
        }
        public bool ExportRelationshipRoles
        {
            get
            {
                if (Parameters.Contains("ExportRelationshipRoles"))
                    return (bool)Parameters["ExportRelationshipRoles"];
                return default(bool);
            }
            set { Parameters["ExportRelationshipRoles"] = value; }
        }
        public bool Managed
        {
            get
            {
                if (Parameters.Contains("Managed"))
                    return (bool)Parameters["Managed"];
                return default(bool);
            }
            set { Parameters["Managed"] = value; }
        }
        public string SolutionName
        {
            get
            {
                if (Parameters.Contains("SolutionName"))
                    return (string)Parameters["SolutionName"];
                return default(string);
            }
            set { Parameters["SolutionName"] = value; }
        }
        public ExportSolutionRequest()
        {
            this.ResponseType = new ExportSolutionResponse();
            this.RequestName = "ExportSolution";
        }
        internal override string GetRequestBody()
        {
            Parameters["ExportAutoNumberingSettings"] = ExportAutoNumberingSettings;
            Parameters["ExportCalendarSettings"] = ExportCalendarSettings;
            Parameters["ExportCustomizationSettings"] = ExportCustomizationSettings;
            Parameters["ExportEmailTrackingSettings"] = ExportEmailTrackingSettings;
            Parameters["ExportGeneralSettings"] = ExportGeneralSettings;
            Parameters["ExportIsvConfig"] = ExportIsvConfig;
            Parameters["ExportMarketingSettings"] = ExportMarketingSettings;
            Parameters["ExportOutlookSynchronizationSettings"] = ExportOutlookSynchronizationSettings;
            Parameters["ExportRelationshipRoles"] = ExportRelationshipRoles;
            Parameters["Managed"] = Managed;
            Parameters["SolutionName"] = SolutionName;
            return GetSoapBody();
        }
    }
    public sealed class ExportSolutionResponse : OrganizationResponse
    {
        public byte[] ExportSolutionFile { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "ExportSolutionFile")
                {
                    ExportSolutionFile = System.Convert.FromBase64String(result.Element(Util.ns.b + "value").Value);
                }
            }
        }
    }

    #endregion ExportSolution

    #region ExportTranslation
    public sealed class ExportTranslationRequest : OrganizationRequest
    {
        public string SolutionName
        {
            get
            {
                if (Parameters.Contains("SolutionName"))
                    return (string)Parameters["SolutionName"];
                return default(string);
            }
            set { Parameters["SolutionName"] = value; }
        }
        public ExportTranslationRequest()
        {
            this.ResponseType = new ExportTranslationResponse();
            this.RequestName = "ExportTranslation";
        }
        internal override string GetRequestBody()
        {
            Parameters["SolutionName"] = SolutionName;
            return GetSoapBody();
        }
    }
    public sealed class ExportTranslationResponse : OrganizationResponse
    {
        public byte[] ExportTranslationFile { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "ExportTranslationFile")
                {
                    ExportTranslationFile = System.Convert.FromBase64String(result.Element(Util.ns.b + "value").Value);
                }
            }
        }
    }

    #endregion ExportTranslation

    #region FetchXmlToQueryExpression
    public sealed class FetchXmlToQueryExpressionRequest : OrganizationRequest
    {
        public string FetchXml
        {
            get
            {
                if (Parameters.Contains("FetchXml"))
                    return (string)Parameters["FetchXml"];
                return default(string);
            }
            set { Parameters["FetchXml"] = value; }
        }
        public FetchXmlToQueryExpressionRequest()
        {
            this.ResponseType = new FetchXmlToQueryExpressionResponse();
            this.RequestName = "FetchXmlToQueryExpression";
        }
        internal override string GetRequestBody()
        {
            Parameters["FetchXml"] = FetchXml;
            return GetSoapBody();
        }
    }
    public sealed class FetchXmlToQueryExpressionResponse : OrganizationResponse
    {
        public QueryExpression Query { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "Query")
                    this.Query = QueryExpression.LoadFromXml(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion FetchXmlToQueryExpression

    #region FindParentResourceGroup
    public sealed class FindParentResourceGroupRequest : OrganizationRequest
    {
        public Guid[] ChildrenIds
        {
            get
            {
                if (Parameters.Contains("ChildrenIds"))
                    return (Guid[])Parameters["ChildrenIds"];
                return default(Guid[]);
            }
            set { Parameters["ChildrenIds"] = value; }
        }
        public Guid ParentId
        {
            get
            {
                if (Parameters.Contains("ParentId"))
                    return (Guid)Parameters["ParentId"];
                return default(Guid);
            }
            set { Parameters["ParentId"] = value; }
        }
        public FindParentResourceGroupRequest()
        {
            this.ResponseType = new FindParentResourceGroupResponse();
            this.RequestName = "FindParentResourceGroup";
        }
        internal override string GetRequestBody()
        {
            Parameters["ChildrenIds"] = ChildrenIds;
            Parameters["ParentId"] = ParentId;
            return GetSoapBody();
        }
    }
    public sealed class FindParentResourceGroupResponse : OrganizationResponse
    {
        public bool result { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "result")
                    this.result = Util.LoadFromXml<bool>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion FindParentResourceGroup

    #region FulfillSalesOrder
    public sealed class FulfillSalesOrderRequest : OrganizationRequest
    {
        public Entity OrderClose
        {
            get
            {
                if (Parameters.Contains("OrderClose"))
                    return (Entity)Parameters["OrderClose"];
                return default(Entity);
            }
            set { Parameters["OrderClose"] = value; }
        }
        public OptionSetValue Status
        {
            get
            {
                if (Parameters.Contains("Status"))
                    return (OptionSetValue)Parameters["Status"];
                return default(OptionSetValue);
            }
            set { Parameters["Status"] = value; }
        }
        public FulfillSalesOrderRequest()
        {
            this.ResponseType = new FulfillSalesOrderResponse();
            this.RequestName = "FulfillSalesOrder";
        }
        internal override string GetRequestBody()
        {
            Parameters["OrderClose"] = OrderClose;
            Parameters["Status"] = Status;
            return GetSoapBody();
        }
    }
    public sealed class FulfillSalesOrderResponse : OrganizationResponse
    {
    }

    #endregion FulfillSalesOrder

    #region GenerateInvoiceFromOpportunity
    public sealed class GenerateInvoiceFromOpportunityRequest : OrganizationRequest
    {
        public Guid OpportunityId
        {
            get
            {
                if (Parameters.Contains("OpportunityId"))
                    return (Guid)Parameters["OpportunityId"];
                return default(Guid);
            }
            set { Parameters["OpportunityId"] = value; }
        }
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
        public GenerateInvoiceFromOpportunityRequest()
        {
            this.ResponseType = new GenerateInvoiceFromOpportunityResponse();
            this.RequestName = "GenerateInvoiceFromOpportunity";
        }
        internal override string GetRequestBody()
        {
            Parameters["OpportunityId"] = OpportunityId;
            Parameters["ColumnSet"] = ColumnSet;
            return GetSoapBody();
        }
    }
    public sealed class GenerateInvoiceFromOpportunityResponse : OrganizationResponse
    {
        public Entity Entity { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "Entity")
                    this.Entity = Entity.LoadFromXml(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion GenerateInvoiceFromOpportunity

    #region GenerateQuoteFromOpportunity
    public sealed class GenerateQuoteFromOpportunityRequest : OrganizationRequest
    {
        public Guid OpportunityId
        {
            get
            {
                if (Parameters.Contains("OpportunityId"))
                    return (Guid)Parameters["OpportunityId"];
                return default(Guid);
            }
            set { Parameters["OpportunityId"] = value; }
        }
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
        public GenerateQuoteFromOpportunityRequest()
        {
            this.ResponseType = new GenerateQuoteFromOpportunityResponse();
            this.RequestName = "GenerateQuoteFromOpportunity";
        }
        internal override string GetRequestBody()
        {
            Parameters["OpportunityId"] = OpportunityId;
            Parameters["ColumnSet"] = ColumnSet;
            return GetSoapBody();
        }
    }
    public sealed class GenerateQuoteFromOpportunityResponse : OrganizationResponse
    {
        public Entity Entity { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "Entity")
                    this.Entity = Entity.LoadFromXml(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion GenerateQuoteFromOpportunity

    #region GenerateSalesOrderFromOpportunity
    public sealed class GenerateSalesOrderFromOpportunityRequest : OrganizationRequest
    {
        public Guid OpportunityId
        {
            get
            {
                if (Parameters.Contains("OpportunityId"))
                    return (Guid)Parameters["OpportunityId"];
                return default(Guid);
            }
            set { Parameters["OpportunityId"] = value; }
        }
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
        public GenerateSalesOrderFromOpportunityRequest()
        {
            this.ResponseType = new GenerateSalesOrderFromOpportunityResponse();
            this.RequestName = "GenerateSalesOrderFromOpportunity";
        }
        internal override string GetRequestBody()
        {
            Parameters["OpportunityId"] = OpportunityId;
            Parameters["ColumnSet"] = ColumnSet;
            return GetSoapBody();
        }
    }
    public sealed class GenerateSalesOrderFromOpportunityResponse : OrganizationResponse
    {
        public Entity Entity { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "Entity")
                    this.Entity = Entity.LoadFromXml(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion GenerateSalesOrderFromOpportunity

    #region GenerateSocialProfile
    public sealed class GenerateSocialProfileRequest : OrganizationRequest
    {
        public Entity Entity
        {
            get
            {
                if (Parameters.Contains("Entity"))
                    return (Entity)Parameters["Entity"];
                return default(Entity);
            }
            set { Parameters["OpportunityId"] = value; }
        }       
        public GenerateSocialProfileRequest()
        {
            this.ResponseType = new GenerateSocialProfileResponse();
            this.RequestName = "GenerateSocialProfile";
        }
        internal override string GetRequestBody()
        {
            Parameters["Entity"] = Entity;
            return GetSoapBody();
        }
    }
    public sealed class GenerateSocialProfileResponse : OrganizationResponse
    {
        public Entity Entity { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "Entity")
                    this.Entity = Entity.LoadFromXml(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion GenerateSocialProfile

    #region GetAllTimeZonesWithDisplayName
    public sealed class GetAllTimeZonesWithDisplayNameRequest : OrganizationRequest
    {
        public int LocaleId
        {
            get
            {
                if (Parameters.Contains("LocaleId"))
                    return (int)Parameters["LocaleId"];
                return default(int);
            }
            set { Parameters["LocaleId"] = value; }
        }
        public GetAllTimeZonesWithDisplayNameRequest()
        {
            this.ResponseType = new GetAllTimeZonesWithDisplayNameResponse();
            this.RequestName = "GetAllTimeZonesWithDisplayName";
        }
        internal override string GetRequestBody()
        {
            Parameters["LocaleId"] = LocaleId;
            return GetSoapBody();
        }
    }
    public sealed class GetAllTimeZonesWithDisplayNameResponse : OrganizationResponse
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

    #endregion GetAllTimeZonesWithDisplayName

    #region GetDecryptionKey
    public sealed class GetDecryptionKeyRequest : OrganizationRequest
    {
        public GetDecryptionKeyRequest()
        {
            this.ResponseType = new GetDecryptionKeyResponse();
            this.RequestName = "GetDecryptionKey";
        }
        internal override string GetRequestBody()
        {
            return GetSoapBody();
        }
    }
    public sealed class GetDecryptionKeyResponse : OrganizationResponse
    {
        public string Key { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "Key")
                    this.Key = result.Element(Util.ns.b + "value").Value;
            }
        }
    }

    #endregion GetDecryptionKey

    #region GetDefaultPriceLevel
    public sealed class GetDefaultPriceLevelRequest : OrganizationRequest
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
        public GetDefaultPriceLevelRequest()
        {
            this.ResponseType = new GetDefaultPriceLevelResponse();
            this.RequestName = "GetDefaultPriceLevel";
        }
        internal override string GetRequestBody()
        {
            Parameters["EntityName"] = EntityName;
            return GetSoapBody();
        }
    }
    public sealed class GetDefaultPriceLevelResponse : OrganizationResponse
    {
        public EntityCollection PriceLevels { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "PriceLevels")
                    this.PriceLevels = EntityCollection.LoadFromXml(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion GetDefaultPriceLevel

    #region GetDistinctValuesImportFile
    public sealed class GetDistinctValuesImportFileRequest : OrganizationRequest
    {
        public int columnNumber
        {
            get
            {
                if (Parameters.Contains("columnNumber"))
                    return (int)Parameters["columnNumber"];
                return default(int);
            }
            set { Parameters["columnNumber"] = value; }
        }
        public Guid ImportFileId
        {
            get
            {
                if (Parameters.Contains("ImportFileId"))
                    return (Guid)Parameters["ImportFileId"];
                return default(Guid);
            }
            set { Parameters["ImportFileId"] = value; }
        }
        public int pageNumber
        {
            get
            {
                if (Parameters.Contains("pageNumber"))
                    return (int)Parameters["pageNumber"];
                return default(int);
            }
            set { Parameters["pageNumber"] = value; }
        }
        public int recordsPerPage
        {
            get
            {
                if (Parameters.Contains("recordsPerPage"))
                    return (int)Parameters["recordsPerPage"];
                return default(int);
            }
            set { Parameters["recordsPerPage"] = value; }
        }
        public GetDistinctValuesImportFileRequest()
        {
            this.ResponseType = new GetDistinctValuesImportFileResponse();
            this.RequestName = "GetDistinctValuesImportFile";
        }
        internal override string GetRequestBody()
        {
            Parameters["columnNumber"] = columnNumber;
            Parameters["ImportFileId"] = ImportFileId;
            Parameters["pageNumber"] = pageNumber;
            Parameters["recordsPerPage"] = recordsPerPage;
            return GetSoapBody();
        }
    }
    public sealed class GetDistinctValuesImportFileResponse : OrganizationResponse
    {
        public string[] Values { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "Values")
                {
                    List<string> list = new List<string>();
                    foreach (XElement item in result.Element(Util.ns.b + "value").Elements(Util.ns.f + "string"))
                    {
                        list.Add(item.Value);
                    }
                    //this.TimeInfos = EntityCollection.LoadFromXml(result.Element(Util.ns.b + "value"));
                    this.Values = list.ToArray();
                }
            }
        }
    }

    #endregion GetDistinctValuesImportFile

    #region GetHeaderColumnsImportFile
    public sealed class GetHeaderColumnsImportFileRequest : OrganizationRequest
    {
        public Guid ImportFileId
        {
            get
            {
                if (Parameters.Contains("ImportFileId"))
                    return (Guid)Parameters["ImportFileId"];
                return default(Guid);
            }
            set { Parameters["ImportFileId"] = value; }
        }
        public GetHeaderColumnsImportFileRequest()
        {
            this.ResponseType = new GetHeaderColumnsImportFileResponse();
            this.RequestName = "GetHeaderColumnsImportFile";
        }
        internal override string GetRequestBody()
        {
            Parameters["ImportFileId"] = ImportFileId;
            return GetSoapBody();
        }
    }
    public sealed class GetHeaderColumnsImportFileResponse : OrganizationResponse
    {
        public string[] Columns { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "Columns")
                {
                    List<string> list = new List<string>();
                    foreach (XElement item in result.Element(Util.ns.b + "value").Elements(Util.ns.f + "string"))
                    {
                        list.Add(item.Value);
                    }
                    //this.TimeInfos = EntityCollection.LoadFromXml(result.Element(Util.ns.b + "value"));
                    this.Columns = list.ToArray();
                }
            }
        }
    }

    #endregion GetHeaderColumnsImportFile

    #region GetInvoiceProductsFromOpportunity
    public sealed class GetInvoiceProductsFromOpportunityRequest : OrganizationRequest
    {
        public Guid InvoiceId
        {
            get
            {
                if (Parameters.Contains("InvoiceId"))
                    return (Guid)Parameters["InvoiceId"];
                return default(Guid);
            }
            set { Parameters["InvoiceId"] = value; }
        }
        public Guid OpportunityId
        {
            get
            {
                if (Parameters.Contains("OpportunityId"))
                    return (Guid)Parameters["OpportunityId"];
                return default(Guid);
            }
            set { Parameters["OpportunityId"] = value; }
        }
        public GetInvoiceProductsFromOpportunityRequest()
        {
            this.ResponseType = new GetInvoiceProductsFromOpportunityResponse();
            this.RequestName = "GetInvoiceProductsFromOpportunity";
        }
        internal override string GetRequestBody()
        {
            Parameters["InvoiceId"] = InvoiceId;
            Parameters["OpportunityId"] = OpportunityId;
            return GetSoapBody();
        }
    }
    public sealed class GetInvoiceProductsFromOpportunityResponse : OrganizationResponse
    {
    }

    #endregion GetInvoiceProductsFromOpportunity

    #region GetQuantityDecimal
    public sealed class GetQuantityDecimalRequest : OrganizationRequest
    {
        public Guid ProductId
        {
            get
            {
                if (Parameters.Contains("ProductId"))
                    return (Guid)Parameters["ProductId"];
                return default(Guid);
            }
            set { Parameters["ProductId"] = value; }
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
        public Guid UoMId
        {
            get
            {
                if (Parameters.Contains("UoMId"))
                    return (Guid)Parameters["UoMId"];
                return default(Guid);
            }
            set { Parameters["UoMId"] = value; }
        }

        public GetQuantityDecimalRequest()
        {
            this.ResponseType = new GetQuantityDecimalResponse();
            this.RequestName = "GetQuantityDecimal";
        }
        internal override string GetRequestBody()
        {
            Parameters["ProductId"] = ProductId;
            Parameters["Target"] = Target;
            Parameters["UoMId"] = UoMId;
            return GetSoapBody();
        }
    }
    public sealed class GetQuantityDecimalResponse : OrganizationResponse
    {
        public int Quantity { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "Quantity")
                    this.Quantity = Util.LoadFromXml<int>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion GetQuantityDecimal

    #region GetQuoteProductsFromOpportunity
    public sealed class GetQuoteProductsFromOpportunityRequest : OrganizationRequest
    {
        public Guid QuoteId
        {
            get
            {
                if (Parameters.Contains("QuoteId"))
                    return (Guid)Parameters["QuoteId"];
                return default(Guid);
            }
            set { Parameters["QuoteId"] = value; }
        }
        public Guid OpportunityId
        {
            get
            {
                if (Parameters.Contains("OpportunityId"))
                    return (Guid)Parameters["OpportunityId"];
                return default(Guid);
            }
            set { Parameters["OpportunityId"] = value; }
        }
        public GetQuoteProductsFromOpportunityRequest()
        {
            this.ResponseType = new GetQuoteProductsFromOpportunityResponse();
            this.RequestName = "GetQuoteProductsFromOpportunity";
        }
        internal override string GetRequestBody()
        {
            Parameters["QuoteId"] = QuoteId;
            Parameters["OpportunityId"] = OpportunityId;
            return GetSoapBody();
        }
    }
    public sealed class GetQuoteProductsFromOpportunityResponse : OrganizationResponse
    {
    }

    #endregion GetQuoteProductsFromOpportunity

    #region GetReportHistoryLimit
    public sealed class GetReportHistoryLimitRequest : OrganizationRequest
    {
        public Guid ReportId
        {
            get
            {
                if (Parameters.Contains("ReportId"))
                    return (Guid)Parameters["ReportId"];
                return default(Guid);
            }
            set { Parameters["ReportId"] = value; }
        }
        public GetReportHistoryLimitRequest()
        {
            this.ResponseType = new GetReportHistoryLimitResponse();
            this.RequestName = "GetReportHistoryLimit";
        }
        internal override string GetRequestBody()
        {
            Parameters["ReportId"] = ReportId;
            return GetSoapBody();
        }
    }
    public sealed class GetReportHistoryLimitResponse : OrganizationResponse
    {
        public int HistoryLimit { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "HistoryLimit")
                    this.HistoryLimit = Util.LoadFromXml<int>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion GetReportHistoryLimit

    #region GetSalesOrderProductsFromOpportunity
    public sealed class GetSalesOrderProductsFromOpportunityRequest : OrganizationRequest
    {
        public Guid SalesOrderId
        {
            get
            {
                if (Parameters.Contains("SalesOrderId"))
                    return (Guid)Parameters["SalesOrderId"];
                return default(Guid);
            }
            set { Parameters["SalesOrderId"] = value; }
        }
        public Guid OpportunityId
        {
            get
            {
                if (Parameters.Contains("OpportunityId"))
                    return (Guid)Parameters["OpportunityId"];
                return default(Guid);
            }
            set { Parameters["OpportunityId"] = value; }
        }
        public GetSalesOrderProductsFromOpportunityRequest()
        {
            this.ResponseType = new GetSalesOrderProductsFromOpportunityResponse();
            this.RequestName = "GetSalesOrderProductsFromOpportunity";
        }
        internal override string GetRequestBody()
        {
            Parameters["SalesOrderId"] = SalesOrderId;
            Parameters["OpportunityId"] = OpportunityId;
            return GetSoapBody();
        }
    }
    public sealed class GetSalesOrderProductsFromOpportunityResponse : OrganizationResponse
    {
    }

    #endregion GetSalesOrderProductsFromOpportunity

    #region GetTimeZoneCodeByLocalizedName
    public sealed class GetTimeZoneCodeByLocalizedNameRequest : OrganizationRequest
    {
        public int LocaleId
        {
            get
            {
                if (Parameters.Contains("LocaleId"))
                    return (int)Parameters["LocaleId"];
                return default(int);
            }
            set { Parameters["LocaleId"] = value; }
        }
        public string LocalizedStandardName
        {
            get
            {
                if (Parameters.Contains("LocalizedStandardName"))
                    return (string)Parameters["LocalizedStandardName"];
                return default(string);
            }
            set { Parameters["LocalizedStandardName"] = value; }
        }
        public GetTimeZoneCodeByLocalizedNameRequest()
        {
            this.ResponseType = new GetTimeZoneCodeByLocalizedNameResponse();
            this.RequestName = "GetTimeZoneCodeByLocalizedName";
        }
        internal override string GetRequestBody()
        {
            Parameters["LocaleId"] = LocaleId;
            Parameters["LocalizedStandardName"] = LocalizedStandardName;
            return GetSoapBody();
        }
    }
    public sealed class GetTimeZoneCodeByLocalizedNameResponse : OrganizationResponse
    {
        public int TimeZoneCode { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "TimeZoneCode")
                    this.TimeZoneCode = (int)(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion GetTimeZoneCodeByLocalizedName

    #region GetTrackingTokenEmail
    public sealed class GetTrackingTokenEmailRequest : OrganizationRequest
    {
        public string Subject
        {
            get
            {
                if (Parameters.Contains("Subject"))
                    return (string)Parameters["Subject"];
                return default(string);
            }
            set { Parameters["Subject"] = value; }
        }
        public GetTrackingTokenEmailRequest()
        {
            this.ResponseType = new GetTrackingTokenEmailResponse();
            this.RequestName = "GetTrackingTokenEmail";
        }
        internal override string GetRequestBody()
        {
            Parameters["Subject"] = Subject;
            return GetSoapBody();
        }
    }
    public sealed class GetTrackingTokenEmailResponse : OrganizationResponse
    {
        public string TrackingToken { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "TrackingToken")
                    this.TrackingToken = result.Element(Util.ns.b + "value").Value;
            }
        }
    }

    #endregion GetTrackingTokenEmail

    #region GrantAccess
    public sealed class GrantAccessRequest : OrganizationRequest
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
        public PrincipalAccess PrincipalAccess
        {
            get
            {
                if (Parameters.Contains("PrincipalAccess"))
                    return (PrincipalAccess)Parameters["PrincipalAccess"];
                return default(PrincipalAccess);
            }
            set { Parameters["PrincipalAccess"] = value; }
        }
        public GrantAccessRequest()
        {
            this.ResponseType = new GrantAccessResponse();
            this.RequestName = "GrantAccess";
        }
        internal override string GetRequestBody()
        {
            Parameters["Target"] = Target;
            Parameters["PrincipalAccess"] = PrincipalAccess;
            return GetSoapBody();
        }
    }
    public sealed class GrantAccessResponse : OrganizationResponse
    {
    }

    #endregion GrantAccess

    #region ImportFieldTranslation
    public sealed class ImportFieldTranslationRequest : OrganizationRequest
    {
        public byte[] TranslationFile
        {
            get
            {
                if (Parameters.Contains("TranslationFile"))
                    return (byte[])Parameters["TranslationFile"];
                return default(byte[]);
            }
            set { Parameters["TranslationFile"] = value; }
        }
        public ImportFieldTranslationRequest()
        {
            this.ResponseType = new ImportFieldTranslationResponse();
            this.RequestName = "ImportFieldTranslation";
        }
        internal override string GetRequestBody()
        {
            Parameters["TranslationFile"] = TranslationFile;
            return GetSoapBody();
        }
    }
    public sealed class ImportFieldTranslationResponse : OrganizationResponse
    {
        public Guid JobId { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "JobId")
                    this.JobId = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion ImportFieldTranslation

    #region ImportMappingsImportMap
    public sealed class ImportMappingsImportMapRequest : OrganizationRequest
    {
        public string MappingsXml
        {
            get
            {
                if (Parameters.Contains("MappingsXml"))
                    return (string)Parameters["MappingsXml"];
                return default(string);
            }
            set { Parameters["MappingsXml"] = value; }
        }
        public bool ReplaceIds
        {
            get
            {
                if (Parameters.Contains("ReplaceIds"))
                    return (bool)Parameters["ReplaceIds"];
                return default(bool);
            }
            set { Parameters["ReplaceIds"] = value; }
        }
        public ImportMappingsImportMapRequest()
        {
            this.ResponseType = new ImportMappingsImportMapResponse();
            this.RequestName = "ImportMappingsImportMap";
        }
        internal override string GetRequestBody()
        {
            Parameters["MappingsXml"] = MappingsXml;
            Parameters["ReplaceIds"] = ReplaceIds;
            return GetSoapBody();
        }
    }
    public sealed class ImportMappingsImportMapResponse : OrganizationResponse
    {
        public Guid ImportMapId { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "ImportMapId")
                    this.ImportMapId = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion ImportMappingsImportMap

    #region ImportRecordsImport
    public sealed class ImportRecordsImportRequest : OrganizationRequest
    {
        public Guid ImportId
        {
            get
            {
                if (Parameters.Contains("ImportId"))
                    return (Guid)Parameters["ImportId"];
                return default(Guid);
            }
            set { Parameters["ImportId"] = value; }
        }
        public ImportRecordsImportRequest()
        {
            this.ResponseType = new ImportRecordsImportResponse();
            this.RequestName = "ImportRecordsImport";
        }
        internal override string GetRequestBody()
        {
            Parameters["ImportId"] = ImportId;
            return GetSoapBody();
        }
    }
    public sealed class ImportRecordsImportResponse : OrganizationResponse
    {
        public Guid AsyncOperationId { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "AsyncOperationId")
                    this.AsyncOperationId = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion ImportRecordsImport

    #region ImportSolution
    public sealed class ImportSolutionRequest : OrganizationRequest
    {
        public bool ConvertToManaged
        {
            get
            {
                if (Parameters.Contains("ConvertToManaged"))
                    return (bool)Parameters["ConvertToManaged"];
                return default(bool);
            }
            set { Parameters["ConvertToManaged"] = value; }
        }
        public byte[] CustomizationFile
        {
            get
            {
                if (Parameters.Contains("CustomizationFile"))
                    return (byte[])Parameters["CustomizationFile"];
                return default(byte[]);
            }
            set { Parameters["CustomizationFile"] = value; }
        }
        public Guid ImportJobId
        {
            get
            {
                if (Parameters.Contains("ImportJobId"))
                    return (Guid)Parameters["ImportJobId"];
                return default(Guid);
            }
            set { Parameters["ImportJobId"] = value; }
        }
        public bool OverwriteUnmanagedCustomizations
        {
            get
            {
                if (Parameters.Contains("OverwriteUnmanagedCustomizations"))
                    return (bool)Parameters["OverwriteUnmanagedCustomizations"];
                return default(bool);
            }
            set { Parameters["OverwriteUnmanagedCustomizations"] = value; }
        }
        public bool PublishWorkflows
        {
            get
            {
                if (Parameters.Contains("PublishWorkflows"))
                    return (bool)Parameters["PublishWorkflows"];
                return default(bool);
            }
            set { Parameters["PublishWorkflows"] = value; }
        }
        public bool SkipProductUpdateDependencies
        {
            get
            {
                if (Parameters.Contains("SkipProductUpdateDependencies"))
                    return (bool)Parameters["SkipProductUpdateDependencies"];
                return default(bool);
            }
            set { Parameters["SkipProductUpdateDependencies"] = value; }
        }
        public ImportSolutionRequest()
        {
            this.ResponseType = new ImportSolutionResponse();
            this.RequestName = "ImportSolution";
        }
        internal override string GetRequestBody()
        {
            Parameters["ConvertToManaged"] = ConvertToManaged;
            Parameters["CustomizationFile"] = CustomizationFile;
            Parameters["ImportJobId"] = ImportJobId;
            Parameters["OverwriteUnmanagedCustomizations"] = OverwriteUnmanagedCustomizations;
            Parameters["PublishWorkflows"] = PublishWorkflows;
            Parameters["SkipProductUpdateDependencies"] = SkipProductUpdateDependencies;
            return GetSoapBody();
        }
    }
    public sealed class ImportSolutionResponse : OrganizationResponse
    {
    }

    #endregion ImportSolution

    #region ImportTranslation
    public sealed class ImportTranslationRequest : OrganizationRequest
    {
        public byte[] TranslationFile
        {
            get
            {
                if (Parameters.Contains("TranslationFile"))
                    return (byte[])Parameters["TranslationFile"];
                return default(byte[]);
            }
            set { Parameters["TranslationFile"] = value; }
        }
        public Guid ImportJobId
        {
            get
            {
                if (Parameters.Contains("ImportJobId"))
                    return (Guid)Parameters["ImportJobId"];
                return default(Guid);
            }
            set { Parameters["ImportJobId"] = value; }
        }
        public ImportTranslationRequest()
        {
            this.ResponseType = new ImportTranslationResponse();
            this.RequestName = "ImportTranslation";
        }
        internal override string GetRequestBody()
        {
            Parameters["TranslationFile"] = TranslationFile;
            Parameters["ImportJobId"] = ImportJobId;
            return GetSoapBody();
        }
    }
    public sealed class ImportTranslationResponse : OrganizationResponse
    {
    }

    #endregion ImportTranslation

    #region InitializeFrom
    public sealed class InitializeFromRequest : OrganizationRequest
    {
        public EntityReference EntityMoniker
        {
            get
            {
                if (Parameters.Contains("EntityMoniker"))
                    return (EntityReference)Parameters["EntityMoniker"];
                return default(EntityReference);
            }
            set { Parameters["EntityMoniker"] = value; }
        }
        public string TargetEntityName
        {
            get
            {
                if (Parameters.Contains("TargetEntityName"))
                    return (string)Parameters["TargetEntityName"];
                return default(string);
            }
            set { Parameters["TargetEntityName"] = value; }
        }
        public TargetFieldType TargetFieldType
        {
            get
            {
                if (Parameters.Contains("TargetFieldType"))
                    return (TargetFieldType)Parameters["TargetFieldType"];
                return default(TargetFieldType);
            }
            set { Parameters["TargetFieldType"] = value; }
        }
        public InitializeFromRequest()
        {
            this.ResponseType = new InitializeFromResponse();
            this.RequestName = "InitializeFrom";
        }
        internal override string GetRequestBody()
        {
            Parameters["EntityMoniker"] = EntityMoniker;
            Parameters["TargetEntityName"] = TargetEntityName;
            Parameters["TargetFieldType"] = TargetFieldType;
            return GetSoapBody();
        }
    }
    public sealed class InitializeFromResponse : OrganizationResponse
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

    #endregion InitializeFrom

    #region InstallSampleData
    public sealed class InstallSampleDataRequest : OrganizationRequest
    {
        public InstallSampleDataRequest()
        {
            base.ResponseType = new InstallSampleDataResponse();
            base.RequestName = "InstallSampleData";
        }
        internal override string GetRequestBody()
        {
            return GetSoapBody();
        }
    }
    public sealed class InstallSampleDataResponse : OrganizationResponse
    {
    }

    #endregion InstallSampleData

    #region InstantiateFilters
    public sealed class InstantiateFiltersRequest : OrganizationRequest
    {
        public EntityReferenceCollection TemplateCollection
        {
            get
            {
                if (Parameters.Contains("TemplateCollection"))
                    return (EntityReferenceCollection)Parameters["TemplateCollection"];
                return default(EntityReferenceCollection);
            }
            set { Parameters["TemplateCollection"] = value; }
        }
        public Guid UserId
        {
            get
            {
                if (Parameters.Contains("UserId"))
                    return (Guid)Parameters["UserId"];
                return default(Guid);
            }
            set { Parameters["UserId"] = value; }
        }
        public InstantiateFiltersRequest()
        {
            this.ResponseType = new InstantiateFiltersResponse();
            this.RequestName = "InstantiateFilters";
        }
        internal override string GetRequestBody()
        {
            Parameters["TemplateCollection"] = TemplateCollection;
            Parameters["UserId"] = UserId;
            return GetSoapBody();
        }
    }
    public sealed class InstantiateFiltersResponse : OrganizationResponse
    {
    }

    #endregion InstantiateFilters

    #region InstantiateTemplate
    public sealed class InstantiateTemplateRequest : OrganizationRequest
    {
        public Guid ObjectId
        {
            get
            {
                if (Parameters.Contains("ObjectId"))
                    return (Guid)Parameters["ObjectId"];
                return default(Guid);
            }
            set { Parameters["ObjectId"] = value; }
        }
        public Guid TemplateId
        {
            get
            {
                if (Parameters.Contains("TemplateId"))
                    return (Guid)Parameters["TemplateId"];
                return default(Guid);
            }
            set { Parameters["TemplateId"] = value; }
        }
        public string ObjectType
        {
            get
            {
                if (Parameters.Contains("ObjectType"))
                    return (string)Parameters["ObjectType"];
                return default(string);
            }
            set { Parameters["ObjectType"] = value; }
        }
        public InstantiateTemplateRequest()
        {
            this.ResponseType = new InstantiateTemplateResponse();
            this.RequestName = "InstantiateTemplate";
        }
        internal override string GetRequestBody()
        {
            Parameters["ObjectId"] = ObjectId;
            Parameters["TemplateId"] = TemplateId;
            Parameters["ObjectType"] = ObjectType;
            return GetSoapBody();
        }
    }
    public sealed class InstantiateTemplateResponse : OrganizationResponse
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

    #endregion InstantiateTemplate

    #region IsBackOfficeInstalled
    public sealed class IsBackOfficeInstalledRequest : OrganizationRequest
    {
        public IsBackOfficeInstalledRequest()
        {
            this.ResponseType = new IsBackOfficeInstalledResponse();
            this.RequestName = "IsBackOfficeInstalled";
        }
        internal override string GetRequestBody()
        {
            return GetSoapBody();
        }
    }
    public sealed class IsBackOfficeInstalledResponse : OrganizationResponse
    {
        public bool IsInstalled { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "IsInstalled")
                    this.IsInstalled = Util.LoadFromXml<bool>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion IsBackOfficeInstalledResponse

    #region IsComponentCustomizable
    public sealed class IsComponentCustomizableRequest : OrganizationRequest
    {
        public Guid ComponentId
        {
            get
            {
                if (Parameters.Contains("ComponentId"))
                    return (Guid)Parameters["ComponentId"];
                return default(Guid);
            }
            set { Parameters["ComponentId"] = value; }
        }
        public int ComponentType
        {
            get
            {
                if (Parameters.Contains("ComponentType"))
                    return (int)Parameters["ComponentType"];
                return default(int);
            }
            set { Parameters["ComponentType"] = value; }
        }
        public IsComponentCustomizableRequest()
        {
            this.ResponseType = new IsComponentCustomizableResponse();
            this.RequestName = "IsComponentCustomizable";
        }
        internal override string GetRequestBody()
        {
            Parameters["ComponentId"] = ComponentId;
            Parameters["ComponentType"] = ComponentType;
            return GetSoapBody();
        }
    }
    public sealed class IsComponentCustomizableResponse : OrganizationResponse
    {
        public bool IsComponentCustomizable { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "IsComponentCustomizable")
                    this.IsComponentCustomizable = Util.LoadFromXml<bool>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion IsComponentCustomizableResponse

    #region IsValidStateTransition
    public sealed class IsValidStateTransitionRequest : OrganizationRequest
    {
        public EntityReference Entity
        {
            get
            {
                if (Parameters.Contains("Entity"))
                    return (EntityReference)Parameters["Entity"];
                return default(EntityReference);
            }
            set { Parameters["Entity"] = value; }
        }
        public string NewState
        {
            get
            {
                if (Parameters.Contains("NewState"))
                    return (string)Parameters["NewState"];
                return default(string);
            }
            set { Parameters["NewState"] = value; }
        }
        public int NewStatus
        {
            get
            {
                if (Parameters.Contains("NewStatus"))
                    return (int)Parameters["NewStatus"];
                return default(int);
            }
            set { Parameters["NewStatus"] = value; }
        }
        public IsValidStateTransitionRequest()
        {
            this.ResponseType = new IsValidStateTransitionResponse();
            this.RequestName = "IsValidStateTransition";
        }
        internal override string GetRequestBody()
        {
            Parameters["Entity"] = Entity;
            Parameters["NewState"] = NewState;
            Parameters["NewStatus"] = NewStatus;
            return GetSoapBody();
        }
    }
    public sealed class IsValidStateTransitionResponse : OrganizationResponse
    {
        public bool IsValid { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "IsValid")
                    IsValid = (bool)(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion IsValidStateTransition

    #region LocalTimeFromUtcTime
    public sealed class LocalTimeFromUtcTimeRequest : OrganizationRequest
    {
        public int TimeZoneCode
        {
            get
            {
                if (Parameters.Contains("TimeZoneCode"))
                    return (int)Parameters["TimeZoneCode"];
                return default(int);
            }
            set { Parameters["TimeZoneCode"] = value; }
        }
        public DateTime UtcTime
        {
            get
            {
                if (Parameters.Contains("UtcTime"))
                    return (DateTime)Parameters["UtcTime"];
                return default(DateTime);
            }
            set { Parameters["UtcTime"] = value; }
        }
        public LocalTimeFromUtcTimeRequest()
        {
            this.ResponseType = new LocalTimeFromUtcTimeResponse();
            this.RequestName = "LocalTimeFromUtcTime";
        }
        internal override string GetRequestBody()
        {
            Parameters["TimeZoneCode"] = TimeZoneCode;
            Parameters["UtcTime"] = UtcTime;
            return GetSoapBody();
        }
    }
    public sealed class LocalTimeFromUtcTimeResponse : OrganizationResponse
    {
        public DateTime LocalTime { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "LocalTime")
                    this.LocalTime = (DateTime)(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion LocalTimeFromUtcTime

    #region LockInvoicePricing
    public sealed class LockInvoicePricingRequest : OrganizationRequest
    {
        public Guid InvoiceId
        {
            get
            {
                if (Parameters.Contains("InvoiceId"))
                    return (Guid)Parameters["InvoiceId"];
                return default(Guid);
            }
            set { Parameters["InvoiceId"] = value; }
        }
        public LockInvoicePricingRequest()
        {
            this.ResponseType = new LockInvoicePricingResponse();
            this.RequestName = "LockInvoicePricing";
        }
        internal override string GetRequestBody()
        {
            Parameters["InvoiceId"] = InvoiceId;
            return GetSoapBody();
        }
    }
    public sealed class LockInvoicePricingResponse : OrganizationResponse
    {
    }

    #endregion LockInvoicePricing

    #region LockSalesOrderPricing
    public sealed class LockSalesOrderPricingRequest : OrganizationRequest
    {
        public Guid SalesOrderId
        {
            get
            {
                if (Parameters.Contains("SalesOrderId"))
                    return (Guid)Parameters["SalesOrderId"];
                return default(Guid);
            }
            set { Parameters["SalesOrderId"] = value; }
        }
        public LockSalesOrderPricingRequest()
        {
            this.ResponseType = new LockSalesOrderPricingResponse();
            this.RequestName = "LockSalesOrderPricing";
        }
        internal override string GetRequestBody()
        {
            Parameters["SalesOrderId"] = SalesOrderId;
            return GetSoapBody();
        }
    }
    public sealed class LockSalesOrderPricingResponse : OrganizationResponse
    {
    }

    #endregion LockSalesOrderPricing

    #region LogFailureBulkOperation
    public sealed class LogFailureBulkOperationRequest : OrganizationRequest
    {
        public Guid BulkOperationId
        {
            get
            {
                if (Parameters.Contains("BulkOperationId"))
                    return (Guid)Parameters["BulkOperationId"];
                return default(Guid);
            }
            set { Parameters["BulkOperationId"] = value; }
        }
        public Guid RegardingObjectId
        {
            get
            {
                if (Parameters.Contains("RegardingObjectId"))
                    return (Guid)Parameters["RegardingObjectId"];
                return default(Guid);
            }
            set { Parameters["RegardingObjectId"] = value; }
        }
        public int RegardingObjectTypeCode
        {
            get
            {
                if (Parameters.Contains("RegardingObjectTypeCode"))
                    return (int)Parameters["RegardingObjectTypeCode"];
                return default(int);
            }
            set { Parameters["RegardingObjectTypeCode"] = value; }
        }
        public int ErrorCode
        {
            get
            {
                if (Parameters.Contains("ErrorCode"))
                    return (int)Parameters["ErrorCode"];
                return default(int);
            }
            set { Parameters["ErrorCode"] = value; }
        }
        public string Message
        {
            get
            {
                if (Parameters.Contains("Message"))
                    return (string)Parameters["Message"];
                return default(string);
            }
            set { Parameters["Message"] = value; }
        }
        public string AdditionalInfo
        {
            get
            {
                if (Parameters.Contains("AdditionalInfo"))
                    return (string)Parameters["AdditionalInfo"];
                return default(string);
            }
            set { Parameters["AdditionalInfo"] = value; }
        }
        public LogFailureBulkOperationRequest()
        {
            this.ResponseType = new LogFailureBulkOperationResponse();
            this.RequestName = "LogFailureBulkOperation";
        }
        internal override string GetRequestBody()
        {
            Parameters["BulkOperationId"] = BulkOperationId;
            Parameters["RegardingObjectId"] = RegardingObjectId;
            Parameters["RegardingObjectTypeCode"] = RegardingObjectTypeCode;
            Parameters["ErrorCode"] = ErrorCode;
            Parameters["Message"] = Message;
            Parameters["AdditionalInfo"] = AdditionalInfo;
            return GetSoapBody();
        }
    }
    public sealed class LogFailureBulkOperationResponse : OrganizationResponse
    {
    }

    #endregion LogFailureBulkOperation

    #region LogSuccessBulkOperation
    public sealed class LogSuccessBulkOperationRequest : OrganizationRequest
    {
        public Guid BulkOperationId
        {
            get
            {
                if (Parameters.Contains("BulkOperationId"))
                    return (Guid)Parameters["BulkOperationId"];
                return default(Guid);
            }
            set { Parameters["BulkOperationId"] = value; }
        }
        public Guid RegardingObjectId
        {
            get
            {
                if (Parameters.Contains("RegardingObjectId"))
                    return (Guid)Parameters["RegardingObjectId"];
                return default(Guid);
            }
            set { Parameters["RegardingObjectId"] = value; }
        }
        public int RegardingObjectTypeCode
        {
            get
            {
                if (Parameters.Contains("RegardingObjectTypeCode"))
                    return (int)Parameters["RegardingObjectTypeCode"];
                return default(int);
            }
            set { Parameters["RegardingObjectTypeCode"] = value; }
        }
        public Guid CreatedObjectId
        {
            get
            {
                if (Parameters.Contains("CreatedObjectId"))
                    return (Guid)Parameters["CreatedObjectId"];
                return default(Guid);
            }
            set { Parameters["CreatedObjectId"] = value; }
        }
        public int CreatedObjectTypeCode
        {
            get
            {
                if (Parameters.Contains("CreatedObjectTypeCode"))
                    return (int)Parameters["CreatedObjectTypeCode"];
                return default(int);
            }
            set { Parameters["CreatedObjectTypeCode"] = value; }
        }
        public string AdditionalInfo
        {
            get
            {
                if (Parameters.Contains("AdditionalInfo"))
                    return (string)Parameters["AdditionalInfo"];
                return default(string);
            }
            set { Parameters["AdditionalInfo"] = value; }
        }
        public LogSuccessBulkOperationRequest()
        {
            this.ResponseType = new LogSuccessBulkOperationResponse();
            this.RequestName = "LogSuccessBulkOperation";
        }
        internal override string GetRequestBody()
        {
            Parameters["BulkOperationId"] = BulkOperationId;
            Parameters["RegardingObjectId"] = RegardingObjectId;
            Parameters["RegardingObjectTypeCode"] = RegardingObjectTypeCode;
            Parameters["CreatedObjectId"] = CreatedObjectId;
            Parameters["CreatedObjectTypeCode"] = CreatedObjectTypeCode;
            Parameters["AdditionalInfo"] = AdditionalInfo;
            return GetSoapBody();
        }
    }
    public sealed class LogSuccessBulkOperationResponse : OrganizationResponse
    {
    }

    #endregion LogSuccessBulkOperation

    #region LoseOpportunity
    public sealed class LoseOpportunityRequest : OrganizationRequest
    {
        public Entity OpportunityClose
        {
            get
            {
                if (Parameters.Contains("OpportunityClose"))
                    return (Entity)Parameters["OpportunityClose"];
                return default(Entity);
            }
            set { Parameters["OpportunityClose"] = value; }
        }
        public OptionSetValue Status
        {
            get
            {
                if (Parameters.Contains("Status"))
                    return (OptionSetValue)Parameters["Status"];
                return default(OptionSetValue);
            }
            set { Parameters["Status"] = value; }
        }
        public LoseOpportunityRequest()
        {
            this.ResponseType = new LoseOpportunityResponse();
            this.RequestName = "LoseOpportunity";
        }
        internal override string GetRequestBody()
        {
            Parameters["OpportunityClose"] = OpportunityClose;
            Parameters["Status"] = Status;
            return GetSoapBody();
        }
    }
    public sealed class LoseOpportunityResponse : OrganizationResponse
    {
    }

    #endregion LoseOpportunity

    #region MakeAvailableToOrganizationReport
    public sealed class MakeAvailableToOrganizationReportRequest : OrganizationRequest
    {
        public Guid ReportId
        {
            get
            {
                if (Parameters.Contains("ReportId"))
                    return (Guid)Parameters["ReportId"];
                return default(Guid);
            }
            set { Parameters["ReportId"] = value; }
        }
        public MakeAvailableToOrganizationReportRequest()
        {
            this.ResponseType = new MakeAvailableToOrganizationReportResponse();
            this.RequestName = "MakeAvailableToOrganizationReport";
        }
        internal override string GetRequestBody()
        {
            Parameters["ReportId"] = ReportId;
            return GetSoapBody();
        }
    }
    public sealed class MakeAvailableToOrganizationReportResponse : OrganizationResponse
    {
    }

    #endregion MakeAvailableToOrganizationReport

    #region MakeAvailableToOrganizationTemplate
    public sealed class MakeAvailableToOrganizationTemplateRequest : OrganizationRequest
    {
        public Guid TemplateId
        {
            get
            {
                if (Parameters.Contains("TemplateId"))
                    return (Guid)Parameters["TemplateId"];
                return default(Guid);
            }
            set { Parameters["TemplateId"] = value; }
        }
        public MakeAvailableToOrganizationTemplateRequest()
        {
            this.ResponseType = new MakeAvailableToOrganizationTemplateResponse();
            this.RequestName = "MakeAvailableToOrganizationTemplate";
        }
        internal override string GetRequestBody()
        {
            Parameters["TemplateId"] = TemplateId;
            return GetSoapBody();
        }
    }
    public sealed class MakeAvailableToOrganizationTemplateResponse : OrganizationResponse
    {
    }

    #endregion MakeAvailableToOrganizationTemplate

    #region MakeUnavailableToOrganizationReport
    public sealed class MakeUnavailableToOrganizationReportRequest : OrganizationRequest
    {
        public Guid ReportId
        {
            get
            {
                if (Parameters.Contains("ReportId"))
                    return (Guid)Parameters["ReportId"];
                return default(Guid);
            }
            set { Parameters["ReportId"] = value; }
        }
        public MakeUnavailableToOrganizationReportRequest()
        {
            this.ResponseType = new MakeUnavailableToOrganizationReportResponse();
            this.RequestName = "MakeUnavailableToOrganizationReport";
        }
        internal override string GetRequestBody()
        {
            Parameters["ReportId"] = ReportId;
            return GetSoapBody();
        }
    }
    public sealed class MakeUnavailableToOrganizationReportResponse : OrganizationResponse
    {
    }

    #endregion MakeUnavailableToOrganizationReport

    #region MakeUnavailableToOrganizationTemplate
    public sealed class MakeUnavailableToOrganizationTemplateRequest : OrganizationRequest
    {
        public Guid TemplateId
        {
            get
            {
                if (Parameters.Contains("TemplateId"))
                    return (Guid)Parameters["TemplateId"];
                return default(Guid);
            }
            set { Parameters["TemplateId"] = value; }
        }
        public MakeUnavailableToOrganizationTemplateRequest()
        {
            this.ResponseType = new MakeUnavailableToOrganizationTemplateResponse();
            this.RequestName = "MakeUnavailableToOrganizationTemplate";
        }
        internal override string GetRequestBody()
        {
            Parameters["TemplateId"] = TemplateId;
            return GetSoapBody();
        }
    }
    public sealed class MakeUnavailableToOrganizationTemplateResponse : OrganizationResponse
    {
    }

    #endregion MakeUnavailableToOrganizationTemplate

    #region Merge
    public sealed class MergeRequest : OrganizationRequest
    {
        public bool PerformParentingChecks
        {
            get
            {
                if (Parameters.Contains("PerformParentingChecks"))
                    return (bool)Parameters["PerformParentingChecks"];
                return default(bool);
            }
            set { Parameters["PerformParentingChecks"] = value; }
        }
        public Guid SubordinateId
        {
            get
            {
                if (Parameters.Contains("SubordinateId"))
                    return (Guid)Parameters["SubordinateId"];
                return default(Guid);
            }
            set { Parameters["SubordinateId"] = value; }
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
        public Entity UpdateContent
        {
            get
            {
                if (Parameters.Contains("UpdateContent"))
                    return (Entity)Parameters["UpdateContent"];
                return default(Entity);
            }
            set { Parameters["UpdateContent"] = value; }
        }
        public MergeRequest()
        {
            this.ResponseType = new MergeResponse();
            this.RequestName = "Merge";
        }
        internal override string GetRequestBody()
        {
            Parameters["PerformParentingChecks"] = PerformParentingChecks;
            Parameters["SubordinateId"] = SubordinateId;
            Parameters["Target"] = Target;
            Parameters["UpdateContent"] = UpdateContent;
            return GetSoapBody();
        }
    }
    public sealed class MergeResponse : OrganizationResponse
    {
    }

    #endregion Merge

    #region ModifyAccess
    public sealed class ModifyAccessRequest : OrganizationRequest
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
        public PrincipalAccess PrincipalAccess
        {
            get
            {
                if (Parameters.Contains("PrincipalAccess"))
                    return (PrincipalAccess)Parameters["PrincipalAccess"];
                return default(PrincipalAccess);
            }
            set { Parameters["PrincipalAccess"] = value; }
        }
        public ModifyAccessRequest()
        {
            this.ResponseType = new ModifyAccessResponse();
            this.RequestName = "ModifyAccess";
        }
        internal override string GetRequestBody()
        {
            Parameters["Target"] = Target;
            Parameters["PrincipalAccess"] = PrincipalAccess;
            return GetSoapBody();
        }
    }
    public sealed class ModifyAccessResponse : OrganizationResponse
    {
    }

    #endregion ModifyAccess

    #region ParseImport
    public sealed class ParseImportRequest : OrganizationRequest
    {
        public Guid ImportId
        {
            get
            {
                if (Parameters.Contains("ImportId"))
                    return (Guid)Parameters["ImportId"];
                return default(Guid);
            }
            set { Parameters["ImportId"] = value; }
        }
        public ParseImportRequest()
        {
            this.ResponseType = new ParseImportResponse();
            this.RequestName = "ParseImport";
        }
        internal override string GetRequestBody()
        {
            Parameters["ImportId"] = ImportId;
            return GetSoapBody();
        }
    }
    public sealed class ParseImportResponse : OrganizationResponse
    {
        public Guid AsyncOperationId { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "AsyncOperationId")
                    this.AsyncOperationId = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion ParseImport

    #region PickFromQueue
    public sealed class PickFromQueueRequest : OrganizationRequest
    {
        public Guid QueueItemId 
        {
            get
            {
                if (Parameters.Contains("QueueItemId"))
                    return (Guid)Parameters["QueueItemId"];
                return default(Guid);
            }
            set { Parameters["QueueItemId"] = value; }
        }
        public bool RemoveQueueItem 
        {
            get
            {
                if (Parameters.Contains("RemoveQueueItem"))
                    return (bool)Parameters["RemoveQueueItem"];
                return default(bool);
            }
            set { Parameters["RemoveQueueItem"] = value; }
        }
        public Guid WorkerId 
        {
            get
            {
                if (Parameters.Contains("WorkerId"))
                    return (Guid)Parameters["WorkerId"];
                return default(Guid);
            }
            set { Parameters["WorkerId"] = value; }
        }
        public PickFromQueueRequest()
        {
            this.ResponseType = new PickFromQueueResponse();
            this.RequestName = "PickFromQueue";
        }
        internal override string GetRequestBody()
        {
            Parameters["QueueItemId"] = QueueItemId;
            Parameters["RemoveQueueItem"] = RemoveQueueItem;
            Parameters["WorkerId"] = WorkerId;
            return GetSoapBody();
        }
    }
    public sealed class PickFromQueueResponse : OrganizationResponse
    {
    }

    #endregion PickFromQueue

    #region ProcessInboundEmail
    public sealed class ProcessInboundEmailRequest : OrganizationRequest
    {
        public Guid InboundEmailActivity
        {
            get
            {
                if (Parameters.Contains("InboundEmailActivity"))
                    return (Guid)Parameters["InboundEmailActivity"];
                return default(Guid);
            }
            set { Parameters["InboundEmailActivity"] = value; }
        }
        public ProcessInboundEmailRequest()
        {
            this.ResponseType = new ProcessInboundEmailResponse();
            this.RequestName = "ProcessInboundEmail";
        }
        internal override string GetRequestBody()
        {
            Parameters["InboundEmailActivity"] = InboundEmailActivity;
            return GetSoapBody();
        }
    }
    public sealed class ProcessInboundEmailResponse : OrganizationResponse
    {
    }

    #endregion ProcessInboundEmail

    #region ProcessOneMemberBulkOperation
    public sealed class ProcessOneMemberBulkOperationRequest : OrganizationRequest
    {
        public Guid BulkOperationId
        {
            get
            {
                if (Parameters.Contains("BulkOperationId"))
                    return (Guid)Parameters["BulkOperationId"];
                return default(Guid);
            }
            set { Parameters["BulkOperationId"] = value; }
        }
        public Entity Entity
        {
            get
            {
                if (Parameters.Contains("Entity"))
                    return (Entity)Parameters["Entity"];
                return default(Entity);
            }
            set { Parameters["Entity"] = value; }
        }
        public int BulkOperationSource
        {
            get
            {
                if (Parameters.Contains("BulkOperationSource"))
                    return (int)Parameters["BulkOperationSource"];
                return default(int);
            }
            set { Parameters["BulkOperationSource"] = value; }
        }
        public ProcessOneMemberBulkOperationRequest()
        {
            this.ResponseType = new ProcessOneMemberBulkOperationResponse();
            this.RequestName = "ProcessOneMemberBulkOperation";
        }
        internal override string GetRequestBody()
        {
            Parameters["BulkOperationId"] = BulkOperationId;
            Parameters["Entity"] = Entity;
            Parameters["BulkOperationSource"] = BulkOperationSource;
            return GetSoapBody();
        }
    }
    public sealed class ProcessOneMemberBulkOperationResponse : OrganizationResponse
    {
        public int ProcessResult { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "ProcessResult")
                    this.ProcessResult = Util.LoadFromXml<int>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion ProcessOneMemberBulkOperation

    #region PropagateByExpression
    public sealed class PropagateByExpressionRequest : OrganizationRequest
    {
        public Entity Activity
        {
            get
            {
                if (Parameters.Contains("Activity"))
                    return (Entity)Parameters["Activity"];
                return default(Entity);
            }
            set { Parameters["Activity"] = value; }
        }
        public bool ExecuteImmediately
        {
            get
            {
                if (Parameters.Contains("ExecuteImmediately"))
                    return (bool)Parameters["ExecuteImmediately"];
                return default(bool);
            }
            set { Parameters["ExecuteImmediately"] = value; }
        }
        public string FriendlyName
        {
            get
            {
                if (Parameters.Contains("FriendlyName"))
                    return (string)Parameters["FriendlyName"];
                return default(string);
            }
            set { Parameters["FriendlyName"] = value; }
        }
        public EntityReference Owner
        {
            get
            {
                if (Parameters.Contains("Owner"))
                    return (EntityReference)Parameters["Owner"];
                return default(EntityReference);
            }
            set { Parameters["Owner"] = value; }
        }
        public PropagationOwnershipOptions OwnershipOptions
        {
            get
            {
                if (Parameters.Contains("OwnershipOptions"))
                    return (PropagationOwnershipOptions)Parameters["OwnershipOptions"];
                return default(PropagationOwnershipOptions);
            }
            set { Parameters["OwnershipOptions"] = value; }
        }
        public bool PostWorkflowEvent
        {
            get
            {
                if (Parameters.Contains("PostWorkflowEvent"))
                    return (bool)Parameters["PostWorkflowEvent"];
                return default(bool);
            }
            set { Parameters["PostWorkflowEvent"] = value; }
        }
        public QueryBase QueryExpression
        {
            get
            {
                if (Parameters.Contains("QueryExpression"))
                    return (QueryBase)Parameters["QueryExpression"];
                return default(QueryBase);
            }
            set { Parameters["QueryExpression"] = value; }
        }
        public Guid QueueId
        {
            get
            {
                if (Parameters.Contains("QueueId"))
                    return (Guid)Parameters["QueueId"];
                return default(Guid);
            }
            set { Parameters["QueueId"] = value; }
        }
        public bool SendEmail
        {
            get
            {
                if (Parameters.Contains("SendEmail"))
                    return (bool)Parameters["SendEmail"];
                return default(bool);
            }
            set { Parameters["SendEmail"] = value; }
        }
        public Guid TemplateId
        {
            get
            {
                if (Parameters.Contains("TemplateId"))
                    return (Guid)Parameters["TemplateId"];
                return default(Guid);
            }
            set { Parameters["TemplateId"] = value; }
        }
        public PropagateByExpressionRequest()
        {
            this.ResponseType = new PropagateByExpressionResponse();
            this.RequestName = "PropagateByExpression";
        }
        internal override string GetRequestBody()
        {
            Parameters["Activity"] = Activity;
            Parameters["ExecuteImmediately"] = ExecuteImmediately;
            Parameters["FriendlyName"] = FriendlyName;
            Parameters["Owner"] = Owner;
            Parameters["OwnershipOptions"] = OwnershipOptions;
            Parameters["PostWorkflowEvent"] = PostWorkflowEvent;
            Parameters["QueryExpression"] = QueryExpression;
            Parameters["QueueId"] = QueueId;
            Parameters["SendEmail"] = SendEmail;
            Parameters["TemplateId"] = TemplateId;
            return GetSoapBody();
        }
    }
    public sealed class PropagateByExpressionResponse : OrganizationResponse
    {
        public Guid BulkOperationId { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "BulkOperationId")
                    this.BulkOperationId = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion PropagateByExpression

    #region ProvisionLanguage
    public sealed class ProvisionLanguageRequest : OrganizationRequest
    {
        public int Language
        {
            get
            {
                if (Parameters.Contains("Language"))
                    return (int)Parameters["Language"];
                return default(int);
            }
            set { Parameters["Language"] = value; }
        }
        public ProvisionLanguageRequest()
        {
            this.ResponseType = new ProvisionLanguageResponse();
            this.RequestName = "ProvisionLanguage";
        }
        internal override string GetRequestBody()
        {
            Parameters["Language"] = Language;
            return GetSoapBody();
        }
    }
    public sealed class ProvisionLanguageResponse : OrganizationResponse
    {
    }

    #endregion ProvisionLanguage

    #region PublishAllXml
    public sealed class PublishAllXmlRequest : OrganizationRequest
    {
        public PublishAllXmlRequest()
        {
            this.ResponseType = new PublishAllXmlResponse();
            this.RequestName = "PublishAllXml";
        }
        internal override string GetRequestBody()
        {
            return GetSoapBody();
        }
    }
    public sealed class PublishAllXmlResponse : OrganizationResponse
    {
    }

    #endregion PublishAllXml

    #region PublishDuplicateRule
    public sealed class PublishDuplicateRuleRequest : OrganizationRequest
    {
        public Guid DuplicateRuleId
        {
            get
            {
                if (Parameters.Contains("DuplicateRuleId"))
                    return (Guid)Parameters["DuplicateRuleId"];
                return default(Guid);
            }
            set { Parameters["DuplicateRuleId"] = value; }
        }
        public PublishDuplicateRuleRequest()
        {
            this.ResponseType = new PublishDuplicateRuleResponse();
            this.RequestName = "PublishDuplicateRule";
        }
        internal override string GetRequestBody()
        {
            Parameters["DuplicateRuleId"] = DuplicateRuleId;
            return GetSoapBody();
        }
    }
    public sealed class PublishDuplicateRuleResponse : OrganizationResponse
    {
        public Guid JobId { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "JobId")
                    this.JobId = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion PublishDuplicateRule

    #region PublishProductHierarchy
    public sealed class PublishProductHierarchyRequest : OrganizationRequest
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
        public PublishProductHierarchyRequest()
        {
            this.ResponseType = new PublishProductHierarchyResponse();
            this.RequestName = "PublishProductHierarchy";
        }
        internal override string GetRequestBody()
        {
            Parameters["Target"] = Target;
            return GetSoapBody();
        }
    }
    public sealed class PublishProductHierarchyResponse : OrganizationResponse
    {       
    }

    #endregion PublishProductHierarchy

    #region PublishTheme
    public sealed class PublishThemeRequest : OrganizationRequest
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
        public PublishThemeRequest()
        {
            this.ResponseType = new PublishThemeResponse();
            this.RequestName = "PublishTheme";
        }
        internal override string GetRequestBody()
        {
            Parameters["Target"] = Target;
            return GetSoapBody();
        }
    }
    public sealed class PublishThemeResponse : OrganizationResponse
    {       
    }

    #endregion PublishTheme

    #region PublishXml
    public sealed class PublishXmlRequest : OrganizationRequest
    {
        public string ParameterXml
        {
            get
            {
                if (Parameters.Contains("ParameterXml"))
                    return (string)Parameters["ParameterXml"];
                return default(string);
            }
            set { Parameters["ParameterXml"] = value; }
        }
        public PublishXmlRequest()
        {
            this.ResponseType = new PublishXmlResponse();
            this.RequestName = "PublishXml";
        }
        internal override string GetRequestBody()
        {
            Parameters["ParameterXml"] = ParameterXml;
            return GetSoapBody();
        }
    }
    public sealed class PublishXmlResponse : OrganizationResponse
    {
    }

    #endregion PublishXml

    #region QualifyLead
    public sealed class QualifyLeadRequest : OrganizationRequest
    {
        public bool CreateAccount
        {
            get
            {
                if (Parameters.Contains("CreateAccount"))
                    return (bool)Parameters["CreateAccount"];
                return default(bool);
            }
            set { Parameters["CreateAccount"] = value; }
        }
        public bool CreateContact
        {
            get
            {
                if (Parameters.Contains("CreateContact"))
                    return (bool)Parameters["CreateContact"];
                return default(bool);
            }
            set { Parameters["CreateContact"] = value; }
        }
        public bool CreateOpportunity
        {
            get
            {
                if (Parameters.Contains("CreateOpportunity"))
                    return (bool)Parameters["CreateOpportunity"];
                return default(bool);
            }
            set { Parameters["CreateOpportunity"] = value; }
        }
        public EntityReference LeadId
        {
            get
            {
                if (Parameters.Contains("LeadId"))
                    return (EntityReference)Parameters["LeadId"];
                return default(EntityReference);
            }
            set { Parameters["LeadId"] = value; }
        }
        public EntityReference OpportunityCurrencyId
        {
            get
            {
                if (Parameters.Contains("OpportunityCurrencyId"))
                    return (EntityReference)Parameters["OpportunityCurrencyId"];
                return default(EntityReference);
            }
            set { Parameters["OpportunityCurrencyId"] = value; }
        }
        public EntityReference OpportunityCustomerId
        {
            get
            {
                if (Parameters.Contains("OpportunityCustomerId"))
                    return (EntityReference)Parameters["OpportunityCustomerId"];
                return default(EntityReference);
            }
            set { Parameters["OpportunityCustomerId"] = value; }
        }
        public EntityReference SourceCampaignId
        {
            get
            {
                if (Parameters.Contains("SourceCampaignId"))
                    return (EntityReference)Parameters["SourceCampaignId"];
                return default(EntityReference);
            }
            set { Parameters["SourceCampaignId"] = value; }
        }
        public OptionSetValue Status
        {
            get
            {
                if (Parameters.Contains("Status"))
                    return (OptionSetValue)Parameters["Status"];
                return default(OptionSetValue);
            }
            set { Parameters["Status"] = value; }
        }
        public QualifyLeadRequest()
        {
            this.ResponseType = new QualifyLeadResponse();
            this.RequestName = "QualifyLead";
        }
        internal override string GetRequestBody()
        {
            Parameters["CreateAccount"] = CreateAccount;
            Parameters["CreateContact"] = CreateContact;
            Parameters["CreateOpportunity"] = CreateOpportunity;
            Parameters["LeadId"] = LeadId;
            Parameters["OpportunityCurrencyId"] = OpportunityCurrencyId;
            Parameters["OpportunityCustomerId"] = OpportunityCustomerId;
            Parameters["SourceCampaignId"] = SourceCampaignId;
            Parameters["Status"] = Status;
            return GetSoapBody();
        }
    }
    public sealed class QualifyLeadResponse : OrganizationResponse
    {
        public EntityReferenceCollection CreatedEntities { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "CreatedEntities")
                    this.CreatedEntities = EntityReferenceCollection.LoadFromXml(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion QualifyLead

    #region QualifyMemberList
    public sealed class QualifyMemberListRequest : OrganizationRequest
    {
        public Guid ListId
        {
            get
            {
                if (Parameters.Contains("ListId"))
                    return (Guid)Parameters["ListId"];
                return default(Guid);
            }
            set { Parameters["ListId"] = value; }
        }
        public Guid[] MembersId
        {
            get
            {
                if (Parameters.Contains("MembersId"))
                    return (Guid[])Parameters["MembersId"];
                return default(Guid[]);
            }
            set { Parameters["MembersId"] = value; }
        }
        public bool OverrideorRemove
        {
            get
            {
                if (Parameters.Contains("OverrideorRemove"))
                    return (bool)Parameters["OverrideorRemove"];
                return default(bool);
            }
            set { Parameters["OverrideorRemove"] = value; }
        }
        public QualifyMemberListRequest()
        {
            this.ResponseType = new QualifyMemberListResponse();
            this.RequestName = "QualifyMemberList";
        }
        internal override string GetRequestBody()
        {
            Parameters["ListId"] = ListId;
            Parameters["MembersId"] = MembersId;
            Parameters["OverrideorRemove"] = OverrideorRemove;
            return GetSoapBody();
        }
    }
    public sealed class QualifyMemberListResponse : OrganizationResponse
    {
    }

    #endregion QualifyMemberList

    #region QueryExpressionToFetchXml
    public sealed class QueryExpressionToFetchXmlRequest : OrganizationRequest
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
        public QueryExpressionToFetchXmlRequest()
        {
            this.ResponseType = new QueryExpressionToFetchXmlResponse();
            this.RequestName = "QueryExpressionToFetchXml";
        }
        internal override string GetRequestBody()
        {
            Parameters["Query"] = Query;
            return GetSoapBody();
        }
    }
    public sealed class QueryExpressionToFetchXmlResponse : OrganizationResponse
    {
        public string FetchXml { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "FetchXml")
                    this.FetchXml = result.Element(Util.ns.b + "value").Value;
            }
        }
    }

    #endregion QueryExpressionToFetchXml

    #region QueryMultipleSchedules
    public sealed class QueryMultipleSchedulesRequest : OrganizationRequest
    {
        public DateTime Start
        {
            get
            {
                if (Parameters.Contains("Start"))
                    return (DateTime)Parameters["Start"];
                return default(DateTime);
            }
            set { Parameters["Start"] = value; }
        }
        public DateTime End
        {
            get
            {
                if (Parameters.Contains("End"))
                    return (DateTime)Parameters["End"];
                return default(DateTime);
            }
            set { Parameters["End"] = value; }
        }
        public Guid[] ResourceIds
        {
            get
            {
                if (Parameters.Contains("ResourceIds"))
                    return (Guid[])Parameters["ResourceIds"];
                return default(Guid[]);
            }
            set { Parameters["ResourceIds"] = value; }
        }
        public TimeCode[] TimeCodes
        {
            get
            {
                if (Parameters.Contains("TimeCodes"))
                    return (TimeCode[])Parameters["TimeCodes"];
                return default(TimeCode[]);
            }
            set { Parameters["TimeCodes"] = value; }
        }
        public QueryMultipleSchedulesRequest()
        {
            this.ResponseType = new QueryMultipleSchedulesResponse();
            this.RequestName = "QueryMultipleSchedules";
        }
        internal override string GetRequestBody()
        {
            Parameters["Start"] = Start;
            Parameters["End"] = End;
            Parameters["ResourceIds"] = ResourceIds;
            Parameters["TimeCodes"] = TimeCodes;
            return GetSoapBody();
        }
    }
    public sealed class QueryMultipleSchedulesResponse : OrganizationResponse
    {
        public TimeInfo[][] TimeInfos { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "TimeInfos")
                {
                    List<List<TimeInfo>> list = new List<List<TimeInfo>>();
                    foreach (XElement item in result.Element(Util.ns.b + "value").Elements(Util.ns.g + "ArrayOfTimeInfo"))
                    {
                        List<TimeInfo> childlist = new List<TimeInfo>();
                        foreach (XElement timeInfo in item.Elements(Util.ns.g + "TimeInfo"))
                        {
                            childlist.Add(TimeInfo.LoadFromXml(timeInfo));
                        }
                        list.Add(childlist);
                    }
                    List<TimeInfo>[] childarray = list.ToArray();
                    this.TimeInfos = new TimeInfo[list.Count][];
                    for (int i = 0; i < list.Count; i++)
                    {
                        TimeInfos[i] = list[i].ToArray();
                    }
                    break;
                }
            }
        }
    }

    #endregion QueryMultipleSchedules

    #region QuerySchedule
    public sealed class QueryScheduleRequest : OrganizationRequest
    {
        public DateTime Start
        {
            get
            {
                if (Parameters.Contains("Start"))
                    return (DateTime)Parameters["Start"];
                return default(DateTime);
            }
            set { Parameters["Start"] = value; }
        }
        public DateTime End
        {
            get
            {
                if (Parameters.Contains("End"))
                    return (DateTime)Parameters["End"];
                return default(DateTime);
            }
            set { Parameters["End"] = value; }
        }
        public Guid ResourceId
        {
            get
            {
                if (Parameters.Contains("ResourceId"))
                    return (Guid)Parameters["ResourceId"];
                return default(Guid);
            }
            set { Parameters["ResourceId"] = value; }
        }
        public TimeCode[] TimeCodes
        {
            get
            {
                if (Parameters.Contains("TimeCodes"))
                    return (TimeCode[])Parameters["TimeCodes"];
                return default(TimeCode[]);
            }
            set { Parameters["TimeCodes"] = value; }
        }
        public QueryScheduleRequest()
        {
            this.ResponseType = new QueryScheduleResponse();
            this.RequestName = "QuerySchedule";
        }
        internal override string GetRequestBody()
        {
            Parameters["Start"] = Start;
            Parameters["End"] = End;
            Parameters["ResourceId"] = ResourceId;
            Parameters["TimeCodes"] = TimeCodes;
            return GetSoapBody();
        }
    }
    public sealed class QueryScheduleResponse : OrganizationResponse
    {
        public TimeInfo[] TimeInfos { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "TimeInfos")
                {
                    List<TimeInfo> list = new List<TimeInfo>();
                    foreach (XElement item in result.Element(Util.ns.b + "value").Elements(Util.ns.g + "TimeInfo"))
                    {
                        list.Add(TimeInfo.LoadFromXml(item));
                    }
                    //this.TimeInfos = EntityCollection.LoadFromXml(result.Element(Util.ns.b + "value"));
                    this.TimeInfos = list.ToArray();
                }
            }
        }
    }

    #endregion QuerySchedule

    #region ReassignObjectsOwner
    public sealed class ReassignObjectsOwnerRequest : OrganizationRequest
    {
        public EntityReference FromPrincipal
        {
            get
            {
                if (Parameters.Contains("FromPrincipal"))
                    return (EntityReference)Parameters["FromPrincipal"];
                return default(EntityReference);
            }
            set { Parameters["FromPrincipal"] = value; }
        }
        public EntityReference ToPrincipal
        {
            get
            {
                if (Parameters.Contains("ToPrincipal"))
                    return (EntityReference)Parameters["ToPrincipal"];
                return default(EntityReference);
            }
            set { Parameters["ToPrincipal"] = value; }
        }
        public ReassignObjectsOwnerRequest()
        {
            this.ResponseType = new ReassignObjectsOwnerResponse();
            this.RequestName = "ReassignObjectsOwner";
        }
        internal override string GetRequestBody()
        {
            Parameters["FromPrincipal"] = FromPrincipal;
            Parameters["ToPrincipal"] = ToPrincipal;
            return GetSoapBody();
        }
    }
    public sealed class ReassignObjectsOwnerResponse : OrganizationResponse
    {
    }

    #endregion ReassignObjectsOwner

    #region ReassignObjectsSystemUser
    public sealed class ReassignObjectsSystemUserRequest : OrganizationRequest
    {
        public EntityReference ReassignPrincipal
        {
            get
            {
                if (Parameters.Contains("ReassignPrincipal"))
                    return (EntityReference)Parameters["ReassignPrincipal"];
                return default(EntityReference);
            }
            set { Parameters["ReassignPrincipal"] = value; }
        }
        public Guid UserId
        {
            get
            {
                if (Parameters.Contains("UserId"))
                    return (Guid)Parameters["UserId"];
                return default(Guid);
            }
            set { Parameters["UserId"] = value; }
        }
        public ReassignObjectsSystemUserRequest()
        {
            this.ResponseType = new ReassignObjectsSystemUserResponse();
            this.RequestName = "ReassignObjectsSystemUser";
        }
        internal override string GetRequestBody()
        {
            Parameters["ReassignPrincipal"] = ReassignPrincipal;
            Parameters["UserId"] = UserId;
            return GetSoapBody();
        }
    }
    public sealed class ReassignObjectsSystemUserResponse : OrganizationResponse
    {
    }

    #endregion ReassignObjectsSystemUser

    #region Recalculate
    public sealed class RecalculateRequest : OrganizationRequest
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
        public RecalculateRequest()
        {
            this.ResponseType = new RecalculateResponse();
            this.RequestName = "Recalculate";
        }
        internal override string GetRequestBody()
        {
            Parameters["Target"] = Target;
            return GetSoapBody();
        }
    }
    public sealed class RecalculateResponse : OrganizationResponse
    {
    }

    #endregion Recalculate

    #region ReleaseToQueue
    public sealed class ReleaseToQueueRequest : OrganizationRequest
    {
        public Guid QueueItemId
        {
            get
            {
                if (Parameters.Contains("QueueItemId"))
                    return (Guid)Parameters["QueueItemId"];
                return default(Guid);
            }
            set { Parameters["QueueItemId"] = value; }
        }       
        public ReleaseToQueueRequest()
        {
            this.ResponseType = new ReleaseToQueueResponse();
            this.RequestName = "ReleaseToQueue";
        }
        internal override string GetRequestBody()
        {
            Parameters["QueueItemId"] = QueueItemId;
            return GetSoapBody();
        }
    }
    public sealed class ReleaseToQueueResponse : OrganizationResponse
    {
    }

    #endregion ReleaseToQueue

    #region RemoveFromQueue
    public sealed class RemoveFromQueueRequest : OrganizationRequest
    {
        public Guid QueueItemId
        {
            get
            {
                if (Parameters.Contains("QueueItemId"))
                    return (Guid)Parameters["QueueItemId"];
                return default(Guid);
            }
            set { Parameters["QueueItemId"] = value; }
        }
        public RemoveFromQueueRequest()
        {
            this.ResponseType = new RemoveFromQueueResponse();
            this.RequestName = "RemoveFromQueue";
        }
        internal override string GetRequestBody()
        {
            Parameters["QueueItemId"] = QueueItemId;
            return GetSoapBody();
        }
    }
    public sealed class RemoveFromQueueResponse : OrganizationResponse
    {
    }

    #endregion RemoveFromQueue

    #region RemoveItemCampaign
    public sealed class RemoveItemCampaignRequest : OrganizationRequest
    {
        public Guid CampaignId
        {
            get
            {
                if (Parameters.Contains("CampaignId"))
                    return (Guid)Parameters["CampaignId"];
                return default(Guid);
            }
            set { Parameters["CampaignId"] = value; }
        }
        public Guid EntityId
        {
            get
            {
                if (Parameters.Contains("EntityId"))
                    return (Guid)Parameters["EntityId"];
                return default(Guid);
            }
            set { Parameters["EntityId"] = value; }
        }
        public RemoveItemCampaignRequest()
        {
            this.ResponseType = new RemoveItemCampaignResponse();
            this.RequestName = "RemoveItemCampaign";
        }
        internal override string GetRequestBody()
        {
            Parameters["CampaignId"] = CampaignId;
            Parameters["EntityId"] = EntityId;
            return GetSoapBody();
        }
    }
    public sealed class RemoveItemCampaignResponse : OrganizationResponse
    {
    }

    #endregion RemoveItemCampaign

    #region RemoveItemCampaignActivity
    public sealed class RemoveItemCampaignActivityRequest : OrganizationRequest
    {
        public Guid CampaignActivityId
        {
            get
            {
                if (Parameters.Contains("CampaignActivityId"))
                    return (Guid)Parameters["CampaignActivityId"];
                return default(Guid);
            }
            set { Parameters["CampaignActivityId"] = value; }
        }
        public Guid ItemId
        {
            get
            {
                if (Parameters.Contains("ItemId"))
                    return (Guid)Parameters["ItemId"];
                return default(Guid);
            }
            set { Parameters["ItemId"] = value; }
        }
        public RemoveItemCampaignActivityRequest()
        {
            this.ResponseType = new RemoveItemCampaignActivityResponse();
            this.RequestName = "RemoveItemCampaignActivity";
        }
        internal override string GetRequestBody()
        {
            Parameters["CampaignActivityId"] = CampaignActivityId;
            Parameters["ItemId"] = ItemId;
            return GetSoapBody();
        }
    }
    public sealed class RemoveItemCampaignActivityResponse : OrganizationResponse
    {
    }

    #endregion RemoveItemCampaignActivity

    #region RemoveMemberList
    public sealed class RemoveMemberListRequest : OrganizationRequest
    {
        public Guid EntityId
        {
            get
            {
                if (Parameters.Contains("EntityId"))
                    return (Guid)Parameters["EntityId"];
                return default(Guid);
            }
            set { Parameters["EntityId"] = value; }
        }
        public Guid ListId
        {
            get
            {
                if (Parameters.Contains("ListId"))
                    return (Guid)Parameters["ListId"];
                return default(Guid);
            }
            set { Parameters["ListId"] = value; }
        }
        public RemoveMemberListRequest()
        {
            this.ResponseType = new RemoveMemberListResponse();
            this.RequestName = "RemoveMemberList";
        }
        internal override string GetRequestBody()
        {
            Parameters["EntityId"] = EntityId;
            Parameters["ListId"] = ListId;
            return GetSoapBody();
        }
    }
    public sealed class RemoveMemberListResponse : OrganizationResponse
    {
    }

    #endregion RemoveMemberList

    #region RemoveMembersTeam
    public sealed class RemoveMembersTeamRequest : OrganizationRequest
    {
        public Guid TeamId
        {
            get
            {
                if (Parameters.Contains("TeamId"))
                    return (Guid)Parameters["TeamId"];
                return default(Guid);
            }
            set { Parameters["TeamId"] = value; }
        }
        public Guid[] MemberIds
        {
            get
            {
                if (Parameters.Contains("MemberIds"))
                    return (Guid[])Parameters["MemberIds"];
                return default(Guid[]);
            }
            set { Parameters["MemberIds"] = value; }
        }
        public RemoveMembersTeamRequest()
        {
            this.ResponseType = new RemoveMembersTeamResponse();
            this.RequestName = "RemoveMembersTeam";
        }
        internal override string GetRequestBody()
        {
            Parameters["TeamId"] = TeamId;
            Parameters["MemberIds"] = MemberIds;
            return GetSoapBody();
        }
    }
    public sealed class RemoveMembersTeamResponse : OrganizationResponse
    {
    }

    #endregion RemoveMembersTeam

    #region RemoveParent
    public sealed class RemoveParentRequest : OrganizationRequest
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
        public RemoveParentRequest()
        {
            this.ResponseType = new RemoveParentResponse();
            this.RequestName = "RemoveParent";
        }
        internal override string GetRequestBody()
        {
            Parameters["Target"] = Target;
            return GetSoapBody();
        }
    }
    public sealed class RemoveParentResponse : OrganizationResponse
    {
    }

    #endregion RemoveParent

    #region RemovePrivilegeRole
    public sealed class RemovePrivilegeRoleRequest : OrganizationRequest
    {
        public Guid RoleId
        {
            get
            {
                if (Parameters.Contains("RoleId"))
                    return (Guid)Parameters["RoleId"];
                return default(Guid);
            }
            set { Parameters["RoleId"] = value; }
        }
        public Guid PrivilegeId
        {
            get
            {
                if (Parameters.Contains("PrivilegeId"))
                    return (Guid)Parameters["PrivilegeId"];
                return default(Guid);
            }
            set { Parameters["PrivilegeId"] = value; }
        }
        public RemovePrivilegeRoleRequest()
        {
            this.ResponseType = new RemovePrivilegeRoleResponse();
            this.RequestName = "RemovePrivilegeRole";
        }
        internal override string GetRequestBody()
        {
            Parameters["RoleId"] = RoleId;
            Parameters["PrivilegeId"] = PrivilegeId;
            return GetSoapBody();
        }
    }
    public sealed class RemovePrivilegeRoleResponse : OrganizationResponse
    {
    }

    #endregion RemovePrivilegeRole

    #region RemoveProductFromKit
    public sealed class RemoveProductFromKitRequest : OrganizationRequest
    {
        public Guid KitId
        {
            get
            {
                if (Parameters.Contains("KitId"))
                    return (Guid)Parameters["KitId"];
                return default(Guid);
            }
            set { Parameters["KitId"] = value; }
        }
        public Guid ProductId
        {
            get
            {
                if (Parameters.Contains("ProductId"))
                    return (Guid)Parameters["ProductId"];
                return default(Guid);
            }
            set { Parameters["ProductId"] = value; }
        }
        public RemoveProductFromKitRequest()
        {
            this.ResponseType = new RemoveProductFromKitResponse();
            this.RequestName = "RemoveProductFromKit";
        }
        internal override string GetRequestBody()
        {
            Parameters["KitId"] = KitId;
            Parameters["ProductId"] = ProductId;
            return GetSoapBody();
        }
    }
    public sealed class RemoveProductFromKitResponse : OrganizationResponse
    {
    }

    #endregion RemoveProductFromKit

    #region RemoveRelated
    public sealed class RemoveRelatedRequest : OrganizationRequest
    {
        public EntityReference[] Target
        {
            get
            {
                if (Parameters.Contains("Target"))
                    return (EntityReference[])Parameters["Target"];
                return default(EntityReference[]);
            }
            set { Parameters["Target"] = value; }
        }
        public RemoveRelatedRequest()
        {
            this.ResponseType = new RemoveRelatedResponse();
            this.RequestName = "RemoveRelated";
        }
        internal override string GetRequestBody()
        {
            Parameters["Target"] = Target;
            return GetSoapBody();
        }
    }
    public sealed class RemoveRelatedResponse : OrganizationResponse
    {
    }

    #endregion RemoveRelated

    #region RemoveSolutionComponent
    public sealed class RemoveSolutionComponentRequest : OrganizationRequest
    {
        public Guid ComponentId
        {
            get
            {
                if (Parameters.Contains("ComponentId"))
                    return (Guid)Parameters["ComponentId"];
                return default(Guid);
            }
            set { Parameters["ComponentId"] = value; }
        }
        public int ComponentType
        {
            get
            {
                if (Parameters.Contains("ComponentType"))
                    return (int)Parameters["ComponentType"];
                return default(int);
            }
            set { Parameters["ComponentType"] = value; }
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
        public RemoveSolutionComponentRequest()
        {
            this.ResponseType = new RemoveSolutionComponentResponse();
            this.RequestName = "RemoveSolutionComponent";
        }
        internal override string GetRequestBody()
        {
            Parameters["ComponentId"] = ComponentId;
            Parameters["ComponentType"] = ComponentType;
            Parameters["SolutionUniqueName"] = SolutionUniqueName;
            return GetSoapBody();
        }
    }
    public sealed class RemoveSolutionComponentResponse : OrganizationResponse
    {
        public Guid id { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "id")
                    this.id = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion RemoveSolutionComponent

    #region RemoveSubstituteProduct
    public sealed class RemoveSubstituteProductRequest : OrganizationRequest
    {
        public Guid ProductId
        {
            get
            {
                if (Parameters.Contains("ProductId"))
                    return (Guid)Parameters["ProductId"];
                return default(Guid);
            }
            set { Parameters["ProductId"] = value; }
        }
        public Guid SubstituteId
        {
            get
            {
                if (Parameters.Contains("SubstituteId"))
                    return (Guid)Parameters["SubstituteId"];
                return default(Guid);
            }
            set { Parameters["SubstituteId"] = value; }
        }
        public RemoveSubstituteProductRequest()
        {
            this.ResponseType = new RemoveSubstituteProductResponse();
            this.RequestName = "RemoveSubstituteProduct";
        }
        internal override string GetRequestBody()
        {
            Parameters["ProductId"] = ProductId;
            Parameters["SubstituteId"] = SubstituteId;
            return GetSoapBody();
        }
    }
    public sealed class RemoveSubstituteProductResponse : OrganizationResponse
    {
    }

    #endregion RemoveSubstituteProduct

    #region RemoveUserFromRecordTeam
    public sealed class RemoveUserFromRecordTeamRequest : OrganizationRequest
    {
        public EntityReference Record
        {
            get
            {
                if (Parameters.Contains("Record"))
                    return (EntityReference)Parameters["Record"];
                return default(EntityReference);
            }
            set { Parameters["Record"] = value; }
        }
        public Guid SystemUserId
        {
            get
            {
                if (Parameters.Contains("SystemUserId"))
                    return (Guid)Parameters["SystemUserId"];
                return default(Guid);
            }
            set { Parameters["SystemUserId"] = value; }
        }
        public Guid TeamTemplateId
        {
            get
            {
                if (Parameters.Contains("TeamTemplateId"))
                    return (Guid)Parameters["TeamTemplateId"];
                return default(Guid);
            }
            set { Parameters["TeamTemplateId"] = value; }
        }
        public RemoveUserFromRecordTeamRequest()
        {
            this.ResponseType = new RemoveUserFromRecordTeamResponse();
            this.RequestName = "RemoveUserFromRecordTeam";
        }
        internal override string GetRequestBody()
        {
            Parameters["Record"] = Record;
            Parameters["SystemUserId"] = SystemUserId;
            Parameters["TeamTemplateId"] = TeamTemplateId;
            return GetSoapBody();
        }
    }
    public sealed class RemoveUserFromRecordTeamResponse : OrganizationResponse
    {
        public Guid AccessTeamId { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "AccessTeamId")
                    this.AccessTeamId = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion RemoveUserFromRecordTeam

    #region RenewContract
    public sealed class RenewContractRequest : OrganizationRequest
    {
        public Guid ContractId
        {
            get
            {
                if (Parameters.Contains("ContractId"))
                    return (Guid)Parameters["ContractId"];
                return default(Guid);
            }
            set { Parameters["ContractId"] = value; }
        }
        public bool IncludeCanceledLines
        {
            get
            {
                if (Parameters.Contains("IncludeCanceledLines"))
                    return (bool)Parameters["IncludeCanceledLines"];
                return default(bool);
            }
            set { Parameters["IncludeCanceledLines"] = value; }
        }
        public int Status
        {
            get
            {
                if (Parameters.Contains("Status"))
                    return (int)Parameters["Status"];
                return default(int);
            }
            set { Parameters["Status"] = value; }
        }
        public RenewContractRequest()
        {
            this.ResponseType = new RenewContractResponse();
            this.RequestName = "RenewContract";
        }
        internal override string GetRequestBody()
        {
            Parameters["ContractId"] = ContractId;
            Parameters["IncludeCanceledLines"] = IncludeCanceledLines;
            Parameters["Status"] = Status;
            return GetSoapBody();
        }
    }
    public sealed class RenewContractResponse : OrganizationResponse
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

    #endregion RenewContract

    #region RenewEntitlement
    public sealed class RenewEntitlementRequest : OrganizationRequest
    {
        public Guid EntitlementId
        {
            get
            {
                if (Parameters.Contains("EntitlementId"))
                    return (Guid)Parameters["EntitlementId"];
                return default(Guid);
            }
            set { Parameters["EntitlementId"] = value; }
        }       
        public int Status
        {
            get
            {
                if (Parameters.Contains("Status"))
                    return (int)Parameters["Status"];
                return default(int);
            }
            set { Parameters["Status"] = value; }
        }
        public RenewEntitlementRequest()
        {
            this.ResponseType = new RenewEntitlementResponse();
            this.RequestName = "RenewEntitlement";
        }
        internal override string GetRequestBody()
        {
            Parameters["EntitlementId"] = EntitlementId;
            Parameters["Status"] = Status;
            return GetSoapBody();
        }
    }
    public sealed class RenewEntitlementResponse : OrganizationResponse
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

    #endregion RenewEntitlement

    #region ReplacePrivilegesRole
    public sealed class ReplacePrivilegesRoleRequest : OrganizationRequest
    {
        public Guid RoleId
        {
            get
            {
                if (Parameters.Contains("RoleId"))
                    return (Guid)Parameters["RoleId"];
                return default(Guid);
            }
            set { Parameters["RoleId"] = value; }
        }
        public RolePrivilege[] Privileges
        {
            get
            {
                if (Parameters.Contains("Privileges"))
                    return (RolePrivilege[])Parameters["Privileges"];
                return default(RolePrivilege[]);
            }
            set { Parameters["Privileges"] = value; }
        }
        public ReplacePrivilegesRoleRequest()
        {
            this.ResponseType = new ReplacePrivilegesRoleResponse();
            this.RequestName = "ReplacePrivilegesRole";
        }
        internal override string GetRequestBody()
        {
            Parameters["RoleId"] = RoleId;
            Parameters["Privileges"] = Privileges;
            return GetSoapBody();
        }
    }
    public sealed class ReplacePrivilegesRoleResponse : OrganizationResponse
    {
    }

    #endregion ReplacePrivilegesRole

    #region Reschedule
    public sealed class RescheduleRequest : OrganizationRequest
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
        public bool ReturnNotifications
        {
            get
            {
                if (Parameters.Contains("ReturnNotifications"))
                    return (bool)Parameters["ReturnNotifications"];
                return default(bool);
            }
            set { Parameters["ReturnNotifications"] = value; }
        }
        public RescheduleRequest()
        {
            this.ResponseType = new RescheduleResponse();
            this.RequestName = "Reschedule";
        }
        internal override string GetRequestBody()
        {
            Parameters["Target"] = Target;
            Parameters["ReturnNotifications"] = ReturnNotifications;
            return GetSoapBody();
        }
    }
    public sealed class RescheduleResponse : OrganizationResponse
    {
        public Object Notifications { get; set; }
        public ValidationResult ValidationResult { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "ValidationResult")
                    this.ValidationResult = ValidationResult.LoadFromXml(result.Element(Util.ns.b + "value"));
                else if (result.Element(Util.ns.b + "key").Value == "Notifications")
                    this.Notifications = result.Element(Util.ns.b + "value").Value;
            }
        }
    }

    #endregion Reschedule

    #region ResetUserFilters
    public sealed class ResetUserFiltersRequest : OrganizationRequest
    {
        public int QueryType
        {
            get
            {
                if (Parameters.Contains("QueryType"))
                    return (int)Parameters["QueryType"];
                return default(int);
            }
            set { Parameters["QueryType"] = value; }
        }
        public ResetUserFiltersRequest()
        {
            this.ResponseType = new ResetUserFiltersResponse();
            this.RequestName = "ResetUserFilters";
        }
        internal override string GetRequestBody()
        {
            Parameters["QueryType"] = QueryType;
            return GetSoapBody();
        }
    }
    public sealed class ResetUserFiltersResponse : OrganizationResponse
    {
    }

    #endregion ResetUserFilters

    #region RetrieveAbsoluteAndSiteCollectionUrl
    public sealed class RetrieveAbsoluteAndSiteCollectionUrlRequest : OrganizationRequest
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
        public RetrieveAbsoluteAndSiteCollectionUrlRequest()
        {
            this.ResponseType = new RetrieveAbsoluteAndSiteCollectionUrlResponse();
            this.RequestName = "RetrieveAbsoluteAndSiteCollectionUrl";
        }
        internal override string GetRequestBody()
        {
            Parameters["Target"] = Target;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveAbsoluteAndSiteCollectionUrlResponse : OrganizationResponse
    {
        public string AbsoluteUrl { get; set; }
        public string SiteCollectionUrl { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "AbsoluteUrl")
                    this.AbsoluteUrl = result.Element(Util.ns.b + "value").Value;
                else if (result.Element(Util.ns.b + "key").Value == "SiteCollectionUrl")
                    this.SiteCollectionUrl = result.Element(Util.ns.b + "value").Value;
            }
        }
    }

    #endregion RetrieveAbsoluteAndSiteCollectionUrl

    #region RetrieveAllChildUsersSystemUser
    public sealed class RetrieveAllChildUsersSystemUserRequest : OrganizationRequest
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
        public Guid EntityId
        {
            get
            {
                if (Parameters.Contains("EntityId"))
                    return (Guid)Parameters["EntityId"];
                return default(Guid);
            }
            set { Parameters["EntityId"] = value; }
        }
        public RetrieveAllChildUsersSystemUserRequest()
        {
            this.ResponseType = new RetrieveAllChildUsersSystemUserResponse();
            this.RequestName = "RetrieveAllChildUsersSystemUser";
        }
        internal override string GetRequestBody()
        {
            Parameters["ColumnSet"] = ColumnSet;
            Parameters["EntityId"] = EntityId;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveAllChildUsersSystemUserResponse : OrganizationResponse
    {
        public EntityCollection EntityCollection { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "EntityCollection")
                    EntityCollection = EntityCollection.LoadFromXml(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion RetrieveAllChildUsersSystemUser

    #region RetrieveApplicationRibbon
    public sealed class RetrieveApplicationRibbonRequest : OrganizationRequest
    {
        public RetrieveApplicationRibbonRequest()
        {
            this.ResponseType = new RetrieveApplicationRibbonResponse();
            this.RequestName = "RetrieveApplicationRibbon";
        }
        internal override string GetRequestBody()
        {
            return GetSoapBody();
        }
    }
    public sealed class RetrieveApplicationRibbonResponse : OrganizationResponse
    {
        public byte[] CompressedApplicationRibbonXml { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "CompressedApplicationRibbonXml")
                {
                    CompressedApplicationRibbonXml = System.Convert.FromBase64String(result.Element(Util.ns.b + "value").Value);
                }
            }
        }
    }

    #endregion RetrieveApplicationRibbon

    #region RetrieveAttributeChangeHistory
    public sealed class RetrieveAttributeChangeHistoryRequest : OrganizationRequest
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
        public PagingInfo PagingInfo
        {
            get
            {
                if (Parameters.Contains("PagingInfo"))
                    return (PagingInfo)Parameters["PagingInfo"];
                return default(PagingInfo);
            }
            set { Parameters["PagingInfo"] = value; }
        }
        public RetrieveAttributeChangeHistoryRequest()
        {
            this.ResponseType = new RetrieveAttributeChangeHistoryResponse();
            this.RequestName = "RetrieveAttributeChangeHistory";
        }
        internal override string GetRequestBody()
        {
            Parameters["Target"] = Target;
            Parameters["AttributeLogicalName"] = AttributeLogicalName;
            Parameters["PagingInfo"] = PagingInfo;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveAttributeChangeHistoryResponse : OrganizationResponse
    {
        public AuditDetailCollection AuditDetailCollection { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "AuditDetailCollection")
                    this.AuditDetailCollection = AuditDetailCollection.LoadFromXml(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion RetrieveAttributeChangeHistory

    #region RetrieveAuditDetails
    public sealed class RetrieveAuditDetailsRequest : OrganizationRequest
    {
        public Guid AuditId
        {
            get
            {
                if (Parameters.Contains("AuditId"))
                    return (Guid)Parameters["AuditId"];
                return default(Guid);
            }
            set { Parameters["AuditId"] = value; }
        }
        public RetrieveAuditDetailsRequest()
        {
            this.ResponseType = new RetrieveAuditDetailsResponse();
            this.RequestName = "RetrieveAuditDetails";
        }
        internal override string GetRequestBody()
        {
            Parameters["AuditId"] = AuditId;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveAuditDetailsResponse : OrganizationResponse
    {
        public AuditDetail AuditDetail { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "AuditDetail")
                    this.AuditDetail = AuditDetail.LoadFromXml(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion RetrieveAuditDetails

    #region RetrieveAuditPartitionList
    public sealed class RetrieveAuditPartitionListRequest : OrganizationRequest
    {
        public RetrieveAuditPartitionListRequest()
        {
            this.ResponseType = new RetrieveAuditPartitionListResponse();
            this.RequestName = "RetrieveAuditPartitionList";
        }
        internal override string GetRequestBody()
        {
            return GetSoapBody();
        }
    }
    public sealed class RetrieveAuditPartitionListResponse : OrganizationResponse
    {
        public AuditPartitionDetailCollection AuditPartitionDetailCollection { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "AuditPartitionDetailCollection")
                    this.AuditPartitionDetailCollection = AuditPartitionDetailCollection.LoadFromXml(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion RetrieveAuditPartitionList

    #region RetrieveAvailableLanguages
    public sealed class RetrieveAvailableLanguagesRequest : OrganizationRequest
    {
        public RetrieveAvailableLanguagesRequest()
        {
            this.ResponseType = new RetrieveAvailableLanguagesResponse();
            this.RequestName = "RetrieveAvailableLanguages";
        }
        internal override string GetRequestBody()
        {
            return GetSoapBody();
        }
    }
    public sealed class RetrieveAvailableLanguagesResponse : OrganizationResponse
    {
        public int[] LocaleIds { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "LocaleIds")
                {
                    List<int> list = new List<int>();
                    foreach (var item in result.Element(Util.ns.b + "value").Elements(Util.ns.g + "LocaleId"))
                    {
                        list.Add(Util.LoadFromXml<int>(item));
                    }
                    this.LocaleIds = list.ToArray();
                }
            }
        }
    }

    #endregion RetrieveAvailableLanguages

    #region RetrieveBusinessHierarchyBusinessUnit
    public sealed class RetrieveBusinessHierarchyBusinessUnitRequest : OrganizationRequest
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
        public Guid EntityId
        {
            get
            {
                if (Parameters.Contains("EntityId"))
                    return (Guid)Parameters["EntityId"];
                return default(Guid);
            }
            set { Parameters["EntityId"] = value; }
        }
        public RetrieveBusinessHierarchyBusinessUnitRequest()
        {
            this.ResponseType = new RetrieveBusinessHierarchyBusinessUnitResponse();
            this.RequestName = "RetrieveBusinessHierarchyBusinessUnit";
        }
        internal override string GetRequestBody()
        {
            Parameters["ColumnSet"] = ColumnSet;
            Parameters["EntityId"] = EntityId;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveBusinessHierarchyBusinessUnitResponse : OrganizationResponse
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

    #endregion RetrieveBusinessHierarchyBusinessUnit

    #region RetrieveByGroupResource
    public sealed class RetrieveByGroupResourceRequest : OrganizationRequest
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
        public Guid ResourceGroupId
        {
            get
            {
                if (Parameters.Contains("ResourceGroupId"))
                    return (Guid)Parameters["ResourceGroupId"];
                return default(Guid);
            }
            set { Parameters["ResourceGroupId"] = value; }
        }
        public RetrieveByGroupResourceRequest()
        {
            this.ResponseType = new RetrieveByGroupResourceResponse();
            this.RequestName = "RetrieveByGroupResource";
        }
        internal override string GetRequestBody()
        {
            Parameters["Query"] = Query;
            Parameters["ResourceGroupId"] = ResourceGroupId;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveByGroupResourceResponse : OrganizationResponse
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

    #endregion RetrieveByGroupResource

    #region RetrieveByResourceResourceGroupResource
    public sealed class RetrieveByResourceResourceGroupRequest : OrganizationRequest
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
        public Guid ResourceId
        {
            get
            {
                if (Parameters.Contains("ResourceId"))
                    return (Guid)Parameters["ResourceId"];
                return default(Guid);
            }
            set { Parameters["ResourceId"] = value; }
        }
        public RetrieveByResourceResourceGroupRequest()
        {
            this.ResponseType = new RetrieveByResourceResourceGroupResponse();
            this.RequestName = "RetrieveByResourceResourceGroupResource";
        }
        internal override string GetRequestBody()
        {
            Parameters["Query"] = Query;
            Parameters["ResourceId"] = ResourceId;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveByResourceResourceGroupResponse : OrganizationResponse
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

    #endregion RetrieveByResourceResourceGroup

    #region RetrieveByResourcesService
    public sealed class RetrieveByResourcesServiceRequest : OrganizationRequest
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
        public Guid[] ResourceIds
        {
            get
            {
                if (Parameters.Contains("ResourceIds"))
                    return (Guid[])Parameters["ResourceIds"];
                return default(Guid[]);
            }
            set { Parameters["ResourceIds"] = value; }
        }
        public RetrieveByResourcesServiceRequest()
        {
            this.ResponseType = new RetrieveByResourcesServiceResponse();
            this.RequestName = "RetrieveByResourcesService";
        }
        internal override string GetRequestBody()
        {
            Parameters["Query"] = Query;
            Parameters["ResourceIds"] = ResourceIds;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveByResourcesServiceResponse : OrganizationResponse
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

    #endregion RetrieveByResourcesService

    #region RetrieveByTopIncidentProductKbArticle
    public sealed class RetrieveByTopIncidentProductKbArticleRequest : OrganizationRequest
    {
        public Guid ProductId
        {
            get
            {
                if (Parameters.Contains("ProductId"))
                    return (Guid)Parameters["ProductId"];
                return default(Guid);
            }
            set { Parameters["ProductId"] = value; }
        }
        public RetrieveByTopIncidentProductKbArticleRequest()
        {
            this.ResponseType = new RetrieveByTopIncidentProductKbArticleResponse();
            this.RequestName = "RetrieveByTopIncidentProductKbArticle";
        }
        internal override string GetRequestBody()
        {
            Parameters["ProductId"] = ProductId;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveByTopIncidentProductKbArticleResponse : OrganizationResponse
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

    #endregion RetrieveByTopIncidentProductKbArticle

    #region RetrieveByTopIncidentSubjectKbArticle
    public sealed class RetrieveByTopIncidentSubjectKbArticleRequest : OrganizationRequest
    {
        public Guid SubjectId
        {
            get
            {
                if (Parameters.Contains("SubjectId"))
                    return (Guid)Parameters["SubjectId"];
                return default(Guid);
            }
            set { Parameters["SubjectId"] = value; }
        }
        public RetrieveByTopIncidentSubjectKbArticleRequest()
        {
            this.ResponseType = new RetrieveByTopIncidentSubjectKbArticleResponse();
            this.RequestName = "RetrieveByTopIncidentSubjectKbArticle";
        }
        internal override string GetRequestBody()
        {
            Parameters["SubjectId"] = SubjectId;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveByTopIncidentSubjectKbArticleResponse : OrganizationResponse
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

    #endregion RetrieveByTopIncidentSubjectKbArticle

    #region RetrieveDependenciesForDelete
    public sealed class RetrieveDependenciesForDeleteRequest : OrganizationRequest
    {
        public int ComponentType
        {
            get
            {
                if (Parameters.Contains("ComponentType"))
                    return (int)Parameters["ComponentType"];
                return default(int);
            }
            set { Parameters["ComponentType"] = value; }
        }
        public Guid ObjectId
        {
            get
            {
                if (Parameters.Contains("ObjectId"))
                    return (Guid)Parameters["ObjectId"];
                return default(Guid);
            }
            set { Parameters["ObjectId"] = value; }
        }
        public RetrieveDependenciesForDeleteRequest()
        {
            this.ResponseType = new RetrieveDependenciesForDeleteResponse();
            this.RequestName = "RetrieveDependenciesForDelete";
        }
        internal override string GetRequestBody()
        {
            Parameters["ComponentType"] = ComponentType;
            Parameters["ObjectId"] = ObjectId;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveDependenciesForDeleteResponse : OrganizationResponse
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

    #endregion RetrieveDependenciesForDelete

    #region RetrieveDependenciesForUninstall
    public sealed class RetrieveDependenciesForUninstallRequest : OrganizationRequest
    {
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
        public RetrieveDependenciesForUninstallRequest()
        {
            this.ResponseType = new RetrieveDependenciesForUninstallResponse();
            this.RequestName = "RetrieveDependenciesForUninstall";
        }
        internal override string GetRequestBody()
        {
            Parameters["SolutionUniqueName"] = SolutionUniqueName;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveDependenciesForUninstallResponse : OrganizationResponse
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

    #endregion RetrieveDependenciesForUninstall

    #region RetrieveDependentComponents
    public sealed class RetrieveDependentComponentsRequest : OrganizationRequest
    {
        public int ComponentType
        {
            get
            {
                if (Parameters.Contains("ComponentType"))
                    return (int)Parameters["ComponentType"];
                return default(int);
            }
            set { Parameters["ComponentType"] = value; }
        }
        public Guid ObjectId
        {
            get
            {
                if (Parameters.Contains("ObjectId"))
                    return (Guid)Parameters["ObjectId"];
                return default(Guid);
            }
            set { Parameters["ObjectId"] = value; }
        }
        public RetrieveDependentComponentsRequest()
        {
            this.ResponseType = new RetrieveDependentComponentsResponse();
            this.RequestName = "RetrieveDependentComponents";
        }
        internal override string GetRequestBody()
        {
            Parameters["ComponentType"] = ComponentType;
            Parameters["ObjectId"] = ObjectId;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveDependentComponentsResponse : OrganizationResponse
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

    #endregion RetrieveDependentComponents

    #region RetrieveDeploymentLicenseType
    public sealed class RetrieveDeploymentLicenseTypeRequest : OrganizationRequest
    {
        public RetrieveDeploymentLicenseTypeRequest()
        {
            this.ResponseType = new RetrieveDeploymentLicenseTypeResponse();
            this.RequestName = "RetrieveDeploymentLicenseType";
        }
        internal override string GetRequestBody()
        {
            return GetSoapBody();
        }
    }
    public sealed class RetrieveDeploymentLicenseTypeResponse : OrganizationResponse
    {
        public string licenseType { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "licenseType")
                    this.licenseType = result.Element(Util.ns.b + "value").Value;
            }
        }
    }

    #endregion RetrieveDeploymentLicenseType

    #region RetrieveDeprovisionedLanguages
    public sealed class RetrieveDeprovisionedLanguagesRequest : OrganizationRequest
    {
        public RetrieveDeprovisionedLanguagesRequest()
        {
            this.ResponseType = new RetrieveDeprovisionedLanguagesResponse();
            this.RequestName = "RetrieveDeprovisionedLanguages";
        }
        internal override string GetRequestBody()
        {
            return GetSoapBody();
        }
    }
    public sealed class RetrieveDeprovisionedLanguagesResponse : OrganizationResponse
    {
        public int[] RetrieveDeprovisionedLanguages { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "RetrieveDeprovisionedLanguages")
                {
                    List<int> list = new List<int>();
                    foreach (var item in result.Element(Util.ns.b + "value").Elements(Util.ns.g + "RetrieveDeprovisionedLanguage"))
                    {
                        list.Add(Util.LoadFromXml<int>(item));
                    }
                    this.RetrieveDeprovisionedLanguages = list.ToArray();
                }
            }
        }
    }

    #endregion RetrieveDeprovisionedLanguages

    #region RetrieveDuplicates
    public sealed class RetrieveDuplicatesRequest : OrganizationRequest
    {
        public Entity BusinessEntity
        {
            get
            {
                if (Parameters.Contains("BusinessEntity"))
                    return (Entity)Parameters["BusinessEntity"];
                return default(Entity);
            }
            set { Parameters["BusinessEntity"] = value; }
        }
        public string MatchingEntityName
        {
            get
            {
                if (Parameters.Contains("MatchingEntityName"))
                    return (string)Parameters["MatchingEntityName"];
                return default(string);
            }
            set { Parameters["MatchingEntityName"] = value; }
        }
        public PagingInfo PagingInfo
        {
            get
            {
                if (Parameters.Contains("PagingInfo"))
                    return (PagingInfo)Parameters["PagingInfo"];
                return default(PagingInfo);
            }
            set { Parameters["PagingInfo"] = value; }
        }
        public RetrieveDuplicatesRequest()
        {
            this.ResponseType = new RetrieveDuplicatesResponse();
            this.RequestName = "RetrieveDuplicates";
        }
        internal override string GetRequestBody()
        {
            Parameters["BusinessEntity"] = BusinessEntity;
            Parameters["MatchingEntityName"] = MatchingEntityName;
            Parameters["PagingInfo"] = PagingInfo;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveDuplicatesResponse : OrganizationResponse
    {
        public EntityCollection DuplicateCollection { get; set; }

        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "DuplicateCollection")
                    this.DuplicateCollection = EntityCollection.LoadFromXml(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion RetrieveDuplicates

    #region RetrieveEntityRibbon
    public sealed class RetrieveEntityRibbonRequest : OrganizationRequest
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
        public RibbonLocationFilters RibbonLocationFilter
        {
            get
            {
                if (Parameters.Contains("RibbonLocationFilter"))
                    return (RibbonLocationFilters)Parameters["RibbonLocationFilter"];
                return default(RibbonLocationFilters);
            }
            set { Parameters["RibbonLocationFilter"] = value; }
        }
        public RetrieveEntityRibbonRequest()
        {
            this.ResponseType = new RetrieveEntityRibbonResponse();
            this.RequestName = "RetrieveEntityRibbon";
        }
        internal override string GetRequestBody()
        {
            Parameters["EntityName"] = EntityName;
            Parameters["RibbonLocationFilter"] = RibbonLocationFilter;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveEntityRibbonResponse : OrganizationResponse
    {
        public byte[] CompressedEntityXml { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "CompressedEntityXml")
                {
                    CompressedEntityXml = System.Convert.FromBase64String(result.Element(Util.ns.b + "value").Value);
                }
            }
        }
    }

    #endregion RetrieveEntityRibbon

    #region RetrieveExchangeRate
    public sealed class RetrieveExchangeRateRequest : OrganizationRequest
    {
        public Guid TransactionCurrencyId
        {
            get
            {
                if (Parameters.Contains("TransactionCurrencyId"))
                    return (Guid)Parameters["TransactionCurrencyId"];
                return default(Guid);
            }
            set { Parameters["TransactionCurrencyId"] = value; }
        }
        public RetrieveExchangeRateRequest()
        {
            this.ResponseType = new RetrieveExchangeRateResponse();
            this.RequestName = "RetrieveExchangeRate";
        }
        internal override string GetRequestBody()
        {
            Parameters["TransactionCurrencyId"] = TransactionCurrencyId;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveExchangeRateResponse : OrganizationResponse
    {
        public float ExchangeRate { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "ExchangeRate")
                    ExchangeRate = (float)(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion RetrieveExchangeRate

    #region RetrieveFilteredForms
    public sealed class RetrieveFilteredFormsRequest : OrganizationRequest
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
        public OptionSetValue FormType
        {
            get
            {
                if (Parameters.Contains("FormType"))
                    return (OptionSetValue)Parameters["FormType"];
                return default(OptionSetValue);
            }
            set { Parameters["FormType"] = value; }
        }
        public Guid SystemUserId
        {
            get
            {
                if (Parameters.Contains("SystemUserId"))
                    return (Guid)Parameters["SystemUserId"];
                return default(Guid);
            }
            set { Parameters["SystemUserId"] = value; }
        }
        public RetrieveFilteredFormsRequest()
        {
            this.ResponseType = new RetrieveFilteredFormsResponse();
            this.RequestName = "RetrieveFilteredForms";
        }
        internal override string GetRequestBody()
        {
            Parameters["EntityLogicalName"] = EntityLogicalName;
            Parameters["FormType"] = FormType;
            Parameters["SystemUserId"] = SystemUserId;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveFilteredFormsResponse : OrganizationResponse
    {
        public EntityReferenceCollection SystemForms { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "SystemForms")
                    this.SystemForms = EntityReferenceCollection.LoadFromXml(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion RetrieveFilteredForms

    #region RetrieveFormattedImportJobResults
    public sealed class RetrieveFormattedImportJobResultsRequest : OrganizationRequest
    {
        public Guid ImportJobId
        {
            get
            {
                if (Parameters.Contains("ImportJobId"))
                    return (Guid)Parameters["ImportJobId"];
                return default(Guid);
            }
            set { Parameters["ImportJobId"] = value; }
        }
        public RetrieveFormattedImportJobResultsRequest()
        {
            this.ResponseType = new RetrieveFormattedImportJobResultsResponse();
            this.RequestName = "RetrieveFormattedImportJobResults";
        }
        internal override string GetRequestBody()
        {
            Parameters["ImportJobId"] = ImportJobId;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveFormattedImportJobResultsResponse : OrganizationResponse
    {
        public string FormattedResults { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "FormattedResults")
                    this.FormattedResults = result.Element(Util.ns.b + "value").Value;
            }
        }
    }

    #endregion RetrieveFormattedImportJobResults

    #region RetrieveFormXml
    public sealed class RetrieveFormXmlRequest : OrganizationRequest
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
        public RetrieveFormXmlRequest()
        {
            this.ResponseType = new RetrieveFormXmlResponse();
            this.RequestName = "RetrieveFormXml";
        }
        internal override string GetRequestBody()
        {
            Parameters["EntityName"] = EntityName;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveFormXmlResponse : OrganizationResponse
    {
        public string FormXml { get; set; }
        public int CustomizationLevel { get; set; }
        public int ComponentState { get; set; }
        public Guid SolutionId { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "FormXml")
                    this.FormXml = result.Element(Util.ns.b + "value").Value;
                if (result.Element(Util.ns.b + "key").Value == "CustomizationLevel")
                    this.CustomizationLevel = Util.LoadFromXml<int>(result.Element(Util.ns.b + "value"));
                if (result.Element(Util.ns.b + "key").Value == "ComponentState")
                    this.ComponentState = Util.LoadFromXml<int>(result.Element(Util.ns.b + "value"));
                if (result.Element(Util.ns.b + "key").Value == "SolutionId")
                    this.SolutionId = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion RetrieveFormXml

    #region RetrieveInstalledLanguagePacks
    public sealed class RetrieveInstalledLanguagePacksRequest : OrganizationRequest
    {
        public RetrieveInstalledLanguagePacksRequest()
        {
            this.ResponseType = new RetrieveInstalledLanguagePacksResponse();
            this.RequestName = "RetrieveInstalledLanguagePacks";
        }
        internal override string GetRequestBody()
        {
            return GetSoapBody();
        }
    }
    public sealed class RetrieveInstalledLanguagePacksResponse : OrganizationResponse
    {
        public int[] RetrieveInstalledLanguagePacks { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "RetrieveInstalledLanguagePacks")
                {
                    List<int> list = new List<int>();
                    foreach (var item in result.Element(Util.ns.b + "value").Elements(Util.ns.g + "RetrieveInstalledLanguagePack"))
                    {
                        list.Add(Util.LoadFromXml<int>(item));
                    }
                    this.RetrieveInstalledLanguagePacks = list.ToArray();
                }
            }
        }
    }

    #endregion RetrieveInstalledLanguagePacks

    #region RetrieveInstalledLanguagePackVersion
    public sealed class RetrieveInstalledLanguagePackVersionRequest : OrganizationRequest
    {
        public int Language
        {
            get
            {
                if (Parameters.Contains("Language"))
                    return (int)Parameters["Language"];
                return default(int);
            }
            set { Parameters["Language"] = value; }
        }
        public RetrieveInstalledLanguagePackVersionRequest()
        {
            this.ResponseType = new RetrieveInstalledLanguagePackVersionResponse();
            this.RequestName = "RetrieveInstalledLanguagePackVersion";
        }
        internal override string GetRequestBody()
        {
            Parameters["Language"] = Language;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveInstalledLanguagePackVersionResponse : OrganizationResponse
    {
        public string Version { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "Version")
                    this.Version = result.Element(Util.ns.b + "value").Value;
            }
        }
    }

    #endregion RetrieveInstalledLanguagePackVersion

    #region RetrieveLicenseInfo
    public sealed class RetrieveLicenseInfoRequest : OrganizationRequest
    {
        public int AccessMode
        {
            get
            {
                if (Parameters.Contains("AccessMode"))
                    return (int)Parameters["AccessMode"];
                return default(int);
            }
            set { Parameters["AccessMode"] = value; }
        }
        public RetrieveLicenseInfoRequest()
        {
            this.ResponseType = new RetrieveLicenseInfoResponse();
            this.RequestName = "RetrieveLicenseInfo";
        }
        internal override string GetRequestBody()
        {
            Parameters["AccessMode"] = AccessMode;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveLicenseInfoResponse : OrganizationResponse
    {
        public int AvailableCount { get; set; }
        public int GrantedLicenseCount { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "AvailableCount")
                    this.AvailableCount = Util.LoadFromXml<int>(result.Element(Util.ns.b + "value"));
                if (result.Element(Util.ns.b + "key").Value == "GrantedLicenseCount")
                    this.GrantedLicenseCount = Util.LoadFromXml<int>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion RetrieveLicenseInfo

    #region RetrieveLocLabels
    public sealed class RetrieveLocLabelsRequest : OrganizationRequest
    {
        public EntityReference EntityMoniker
        {
            get
            {
                if (Parameters.Contains("EntityMoniker"))
                    return (EntityReference)Parameters["EntityMoniker"];
                return default(EntityReference);
            }
            set { Parameters["EntityMoniker"] = value; }
        }
        public string AttributeName
        {
            get
            {
                if (Parameters.Contains("AttributeName"))
                    return (string)Parameters["AttributeName"];
                return default(string);
            }
            set { Parameters["AttributeName"] = value; }
        }
        public bool IncludeUnpublished
        {
            get
            {
                if (Parameters.Contains("IncludeUnpublished"))
                    return (bool)Parameters["IncludeUnpublished"];
                return default(bool);
            }
            set { Parameters["IncludeUnpublished"] = value; }
        }
        public RetrieveLocLabelsRequest()
        {
            this.ResponseType = new RetrieveLocLabelsResponse();
            this.RequestName = "RetrieveLocLabels";
        }
        internal override string GetRequestBody()
        {
            Parameters["EntityMoniker"] = EntityMoniker;
            Parameters["AttributeName"] = AttributeName;
            Parameters["IncludeUnpublished"] = IncludeUnpublished;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveLocLabelsResponse : OrganizationResponse
    {
        public Label Label { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "Label")
                    this.Label = Label.LoadFromXml(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion RetrieveLocLabels

    #region RetrieveMailboxTrackingFolders
    public sealed class RetrieveMailboxTrackingFoldersRequest : OrganizationRequest
    {
        public string MailboxId 
        {
            get
            {
                if (Parameters.Contains("MailboxId"))
                    return (string)Parameters["MailboxId"];
                return default(string);
            }
            set { Parameters["MailboxId"] = value; }
        }       
        public RetrieveMailboxTrackingFoldersRequest()
        {
            this.ResponseType = new RetrieveMailboxTrackingFoldersResponse();
            this.RequestName = "RetrieveMailboxTrackingFolders";
        }
        internal override string GetRequestBody()
        {
            Parameters["MailboxId"] = MailboxId;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveMailboxTrackingFoldersResponse : OrganizationResponse
    {
        public MailboxTrackingFolderMappingCollection MailboxTrackingFolderMappings { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "MailboxTrackingFolderMappings")
                    this.MailboxTrackingFolderMappings = MailboxTrackingFolderMappingCollection.LoadFromXml(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion RetrieveMailboxTrackingFolders

    #region RetrieveMembersBulkOperation
    public sealed class RetrieveMembersBulkOperationRequest : OrganizationRequest
    {
        public Guid BulkOperationId
        {
            get
            {
                if (Parameters.Contains("BulkOperationId"))
                    return (Guid)Parameters["BulkOperationId"];
                return default(Guid);
            }
            set { Parameters["BulkOperationId"] = value; }
        }
        public int BulkOperationSource
        {
            get
            {
                if (Parameters.Contains("BulkOperationSource"))
                    return (int)Parameters["BulkOperationSource"];
                return default(int);
            }
            set { Parameters["BulkOperationSource"] = value; }
        }
        public int EntitySource
        {
            get
            {
                if (Parameters.Contains("EntitySource"))
                    return (int)Parameters["EntitySource"];
                return default(int);
            }
            set { Parameters["EntitySource"] = value; }
        }
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
        public RetrieveMembersBulkOperationRequest()
        {
            this.ResponseType = new RetrieveMembersBulkOperationResponse();
            this.RequestName = "RetrieveMembersBulkOperation";
        }
        internal override string GetRequestBody()
        {
            Parameters["BulkOperationId"] = BulkOperationId;
            Parameters["BulkOperationSource"] = BulkOperationSource;
            Parameters["EntitySource"] = EntitySource;
            Parameters["Query"] = Query;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveMembersBulkOperationResponse : OrganizationResponse
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

    #endregion RetrieveMembersBulkOperation

    #region RetrieveMembersTeam
    public sealed class RetrieveMembersTeamRequest : OrganizationRequest
    {
        public Guid EntityId
        {
            get
            {
                if (Parameters.Contains("EntityId"))
                    return (Guid)Parameters["EntityId"];
                return default(Guid);
            }
            set { Parameters["EntityId"] = value; }
        }
        public ColumnSet MemberColumnSet
        {
            get
            {
                if (Parameters.Contains("MemberColumnSet"))
                    return (ColumnSet)Parameters["MemberColumnSet"];
                return default(ColumnSet);
            }
            set { Parameters["MemberColumnSet"] = value; }
        }
        public RetrieveMembersTeamRequest()
        {
            this.ResponseType = new RetrieveMembersTeamResponse();
            this.RequestName = "RetrieveMembersTeam";
        }
        internal override string GetRequestBody()
        {
            Parameters["EntityId"] = EntityId;
            Parameters["MemberColumnSet"] = MemberColumnSet;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveMembersTeamResponse : OrganizationResponse
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

    #endregion RetrieveMembersTeam

    #region RetrieveMissingComponents
    public sealed class RetrieveMissingComponentsRequest : OrganizationRequest
    {
        public byte[] CustomizationFile
        {
            get
            {
                if (Parameters.Contains("CustomizationFile"))
                    return (byte[])Parameters["CustomizationFile"];
                return default(byte[]);
            }
            set { Parameters["CustomizationFile"] = value; }
        }
        public RetrieveMissingComponentsRequest()
        {
            this.ResponseType = new RetrieveMissingComponentsResponse();
            this.RequestName = "RetrieveMissingComponents";
        }
        internal override string GetRequestBody()
        {
            Parameters["CustomizationFile"] = CustomizationFile;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveMissingComponentsResponse : OrganizationResponse
    {
        public MissingComponent[] MissingComponents { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "MissingComponents")
                {
                    List<MissingComponent> list = new List<MissingComponent>();
                    foreach (var item in result.Element(Util.ns.b + "value").Elements(Util.ns.g + "MissingComponent"))
                    {
                        list.Add(MissingComponent.LoadFromXml(item));
                    }
                    this.MissingComponents = list.ToArray();
                }
            }
        }
    }

    #endregion RetrieveMissingComponents

    #region RetrieveMissingDependencies
    public sealed class RetrieveMissingDependenciesRequest : OrganizationRequest
    {
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
        public RetrieveMissingDependenciesRequest()
        {
            this.ResponseType = new RetrieveMissingDependenciesResponse();
            this.RequestName = "RetrieveMissingDependencies";
        }
        internal override string GetRequestBody()
        {
            Parameters["SolutionUniqueName"] = SolutionUniqueName;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveMissingDependenciesResponse : OrganizationResponse
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

    #endregion RetrieveMissingDependencies

    #region RetrieveOrganizationResources
    public sealed class RetrieveOrganizationResourcesRequest : OrganizationRequest
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
        public RetrieveOrganizationResourcesRequest()
        {
            this.ResponseType = new RetrieveOrganizationResourcesResponse();
            this.RequestName = "RetrieveOrganizationResources";
        }
        internal override string GetRequestBody()
        {
            Parameters["Query"] = Query;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveOrganizationResourcesResponse : OrganizationResponse
    {
        public OrganizationResources OrganizationResources { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "OrganizationResources")
                    this.OrganizationResources = OrganizationResources.LoadFromXml(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion RetrieveOrganizationResources

    #region RetrieveParentGroupsResourceGroup
    public sealed class RetrieveParentGroupsResourceGroupRequest : OrganizationRequest
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
        public Guid ResourceGroupId
        {
            get
            {
                if (Parameters.Contains("ResourceGroupId"))
                    return (Guid)Parameters["ResourceGroupId"];
                return default(Guid);
            }
            set { Parameters["ResourceGroupId"] = value; }
        }
        public RetrieveParentGroupsResourceGroupRequest()
        {
            this.ResponseType = new RetrieveParentGroupsResourceGroupResponse();
            this.RequestName = "RetrieveParentGroupsResourceGroup";
        }
        internal override string GetRequestBody()
        {
            Parameters["Query"] = Query;
            Parameters["ResourceGroupId"] = ResourceGroupId;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveParentGroupsResourceGroupResponse : OrganizationResponse
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

    #endregion RetrieveParentGroupsResourceGroup

    #region RetrieveParsedDataImportFile
    public sealed class RetrieveParsedDataImportFileRequest : OrganizationRequest
    {
        public Guid ImportFileId
        {
            get
            {
                if (Parameters.Contains("ImportFileId"))
                    return (Guid)Parameters["ImportFileId"];
                return default(Guid);
            }
            set { Parameters["ImportFileId"] = value; }
        }
        public PagingInfo PagingInfo
        {
            get
            {
                if (Parameters.Contains("PagingInfo"))
                    return (PagingInfo)Parameters["PagingInfo"];
                return default(PagingInfo);
            }
            set { Parameters["PagingInfo"] = value; }
        }
        public RetrieveParsedDataImportFileRequest()
        {
            this.ResponseType = new RetrieveParsedDataImportFileResponse();
            this.RequestName = "RetrieveParsedDataImportFile";
        }
        internal override string GetRequestBody()
        {
            Parameters["ImportFileId"] = ImportFileId;
            Parameters["PagingInfo"] = PagingInfo;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveParsedDataImportFileResponse : OrganizationResponse
    {
        public string[][] Values { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "Values")
                {
                    List<List<string>> list = new List<List<string>>();
                    foreach (XElement item in result.Element(Util.ns.b + "value").Elements(Util.ns.f + "ArrayOfstring"))
                    {
                        List<string> childlist = new List<string>();
                        foreach (XElement value in item.Elements(Util.ns.f + "string"))
                        {
                            childlist.Add(value.Value);
                        }
                        list.Add(childlist);
                    }
                    List<string>[] childarray = list.ToArray();
                    this.Values = new string[list.Count][];
                    for (int i = 0; i < list.Count; i++)
                    {
                        Values[i] = list[i].ToArray();
                    }
                    break;
                }
            }
        }
    }

    #endregion RetrieveParsedDataImportFile

    #region RetrievePersonalWall
    public sealed class RetrievePersonalWallRequest : OrganizationRequest
    {
        public int PageNumber
        {
            get
            {
                if (Parameters.Contains("PageNumber"))
                    return (int)Parameters["PageNumber"];
                return default(int);
            }
            set { Parameters["PageNumber"] = value; }
        }
        public int PageSize
        {
            get
            {
                if (Parameters.Contains("PageSize"))
                    return (int)Parameters["PageSize"];
                return default(int);
            }
            set { Parameters["PageSize"] = value; }
        }
        public int CommentsPerPost
        {
            get
            {
                if (Parameters.Contains("CommentsPerPost"))
                    return (int)Parameters["CommentsPerPost"];
                return default(int);
            }
            set { Parameters["CommentsPerPost"] = value; }
        }
        public DateTime StartDate
        {
            get
            {
                if (Parameters.Contains("StartDate"))
                    return (DateTime)Parameters["StartDate"];
                return default(DateTime);
            }
            set { Parameters["StartDate"] = value; }
        }
        public DateTime EndDate
        {
            get
            {
                if (Parameters.Contains("EndDate"))
                    return (DateTime)Parameters["EndDate"];
                return default(DateTime);
            }
            set { Parameters["EndDate"] = value; }
        }
        public OptionSetValue Type
        {
            get
            {
                if (Parameters.Contains("Type"))
                    return (OptionSetValue)Parameters["Type"];
                return default(OptionSetValue);
            }
            set { Parameters["Type"] = value; }
        }
        public OptionSetValue Source
        {
            get
            {
                if (Parameters.Contains("Source"))
                    return (OptionSetValue)Parameters["Source"];
                return default(OptionSetValue);
            }
            set { Parameters["Source"] = value; }
        }
        public RetrievePersonalWallRequest()
        {
            this.ResponseType = new RetrievePersonalWallResponse();
            this.RequestName = "RetrievePersonalWall";
        }
        internal override string GetRequestBody()
        {
            Parameters["PageNumber"] = PageNumber;
            Parameters["PageSize"] = PageSize;
            Parameters["CommentsPerPost"] = CommentsPerPost;
            Parameters["StartDate"] = StartDate;
            Parameters["EndDate"] = EndDate;
            Parameters["Type"] = Type;
            Parameters["Source"] = Source;
            return GetSoapBody();
        }
    }
    public sealed class RetrievePersonalWallResponse : OrganizationResponse
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

    #endregion RetrievePersonalWall

    #region RetrievePrincipalAccess
    public sealed class RetrievePrincipalAccessRequest : OrganizationRequest
    {
        public EntityReference Principal
        {
            get
            {
                if (Parameters.Contains("Principal"))
                    return (EntityReference)Parameters["Principal"];
                return default(EntityReference);
            }
            set { Parameters["Principal"] = value; }
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
        public RetrievePrincipalAccessRequest()
        {
            this.ResponseType = new RetrievePrincipalAccessResponse();
            this.RequestName = "RetrievePrincipalAccess";
        }
        internal override string GetRequestBody()
        {
            Parameters["Principal"] = Principal;
            Parameters["Target"] = Target;
            return GetSoapBody();
        }
    }
    public sealed class RetrievePrincipalAccessResponse : OrganizationResponse
    {
        public AccessRights AccessRights { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "AccessRights")
                    AccessRights = Util.GetAccessRightsFromString(result.Element(Util.ns.b + "value").Value);
            }
        }
    }

    #endregion RetrievePrincipalAccess

    #region RetrievePrincipalAttributePrivileges
    public sealed class RetrievePrincipalAttributePrivilegesRequest : OrganizationRequest
    {
        public EntityReference Principal
        {
            get
            {
                if (Parameters.Contains("Principal"))
                    return (EntityReference)Parameters["Principal"];
                return default(EntityReference);
            }
            set { Parameters["Principal"] = value; }
        }
        public RetrievePrincipalAttributePrivilegesRequest()
        {
            this.ResponseType = new RetrievePrincipalAttributePrivilegesResponse();
            this.RequestName = "RetrievePrincipalAttributePrivileges";
        }
        internal override string GetRequestBody()
        {
            Parameters["Principal"] = Principal;
            return GetSoapBody();
        }
    }
    public sealed class RetrievePrincipalAttributePrivilegesResponse : OrganizationResponse
    {
        public AttributePrivilegeCollection AttributePrivileges { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "AttributePrivileges")
                    AttributePrivileges = AttributePrivilegeCollection.LoadFromXml(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion RetrievePrincipalAttributePrivileges

    #region RetrievePrincipalSyncAttributeMappings
    public sealed class RetrievePrincipalSyncAttributeMappingsRequest : OrganizationRequest
    {
        public EntityReference Principal
        {
            get
            {
                if (Parameters.Contains("Principal"))
                    return (EntityReference)Parameters["Principal"];
                return default(EntityReference);
            }
            set { Parameters["Principal"] = value; }
        }
        public RetrievePrincipalSyncAttributeMappingsRequest()
        {
            this.ResponseType = new RetrievePrincipalSyncAttributeMappingsResponse();
            this.RequestName = "RetrievePrincipalSyncAttributeMappings";
        }
        internal override string GetRequestBody()
        {
            Parameters["Principal"] = Principal;
            return GetSoapBody();
        }
    }
    public sealed class RetrievePrincipalSyncAttributeMappingsResponse : OrganizationResponse
    {
        public AttributeMappingCollection AttributeMappings { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "AttributeMappings")
                    AttributeMappings = AttributeMappingCollection.LoadFromXml(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion RetrievePrincipalSyncAttributeMappings

    #region RetrievePrivilegeSet
    public sealed class RetrievePrivilegeSetRequest : OrganizationRequest
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
        public RetrievePrivilegeSetRequest()
        {
            this.ResponseType = new RetrievePrivilegeSetResponse();
            this.RequestName = "RetrievePrivilegeSet";
        }
        internal override string GetRequestBody()
        {
            Parameters["Query"] = Query;
            return GetSoapBody();
        }
    }
    public sealed class RetrievePrivilegeSetResponse : OrganizationResponse
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

    #endregion RetrievePrivilegeSet

    #region RetrieveProductProperties
    public sealed class RetrieveProductPropertiesRequest : OrganizationRequest
    {
        public EntityReference ParentObject
        {
            get
            {
                if (Parameters.Contains("ParentObject"))
                    return (EntityReference)Parameters["ParentObject"];
                return default(EntityReference);
            }
            set { Parameters["ParentObject"] = value; }
        }        
        public RetrieveProductPropertiesRequest()
        {
            this.ResponseType = new RetrieveProductPropertiesResponse();
            this.RequestName = "RetrieveProductProperties";
        }
        internal override string GetRequestBody()
        {
            Parameters["ParentObject"] = ParentObject;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveProductPropertiesResponse : OrganizationResponse
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

    #endregion RetrieveProductProperties

    #region RetrieveProvisionedLanguagePackVersion
    public sealed class RetrieveProvisionedLanguagePackVersionRequest : OrganizationRequest
    {
        public int Language
        {
            get
            {
                if (Parameters.Contains("Language"))
                    return (int)Parameters["Language"];
                return default(int);
            }
            set { Parameters["Language"] = value; }
        }
        public RetrieveProvisionedLanguagePackVersionRequest()
        {
            this.ResponseType = new RetrieveProvisionedLanguagePackVersionResponse();
            this.RequestName = "RetrieveProvisionedLanguagePackVersion";
        }
        internal override string GetRequestBody()
        {
            Parameters["Language"] = Language;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveProvisionedLanguagePackVersionResponse : OrganizationResponse
    {
        public string Version { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "Version")
                    this.Version = result.Element(Util.ns.b + "value").Value;
            }
        }
    }

    #endregion RetrieveProvisionedLanguagePackVersion

    #region RetrieveProvisionedLanguages
    public sealed class RetrieveProvisionedLanguagesRequest : OrganizationRequest
    {
        public RetrieveProvisionedLanguagesRequest()
        {
            this.ResponseType = new RetrieveProvisionedLanguagesResponse();
            this.RequestName = "RetrieveProvisionedLanguages";
        }
        internal override string GetRequestBody()
        {
            return GetSoapBody();
        }
    }
    public sealed class RetrieveProvisionedLanguagesResponse : OrganizationResponse
    {
        public int[] RetrieveProvisionedLanguages { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "RetrieveProvisionedLanguages")
                {
                    List<int> list = new List<int>();
                    foreach (var item in result.Element(Util.ns.b + "value").Elements(Util.ns.g + "RetrieveProvisionedLanguage"))
                    {
                        list.Add(Util.LoadFromXml<int>(item));
                    }
                    this.RetrieveProvisionedLanguages = list.ToArray();
                }
            }
        }
    }

    #endregion RetrieveProvisionedLanguages

    #region RetrieveRecordChangeHistory
    public sealed class RetrieveRecordChangeHistoryRequest : OrganizationRequest
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
        public PagingInfo PagingInfo
        {
            get
            {
                if (Parameters.Contains("PagingInfo"))
                    return (PagingInfo)Parameters["PagingInfo"];
                return default(PagingInfo);
            }
            set { Parameters["PagingInfo"] = value; }
        }
        public RetrieveRecordChangeHistoryRequest()
        {
            this.ResponseType = new RetrieveRecordChangeHistoryResponse();
            this.RequestName = "RetrieveRecordChangeHistory";
        }
        internal override string GetRequestBody()
        {
            Parameters["Target"] = Target;
            Parameters["PagingInfo"] = PagingInfo;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveRecordChangeHistoryResponse : OrganizationResponse
    {
        public AuditDetailCollection AuditDetailCollection { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "AuditDetailCollection")
                    this.AuditDetailCollection = AuditDetailCollection.LoadFromXml(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion RetrieveRecordChangeHistory

    #region RetrieveRecordWall
    public sealed class RetrieveRecordWallRequest : OrganizationRequest
    {
        public EntityReference Entity
        {
            get
            {
                if (Parameters.Contains("Entity"))
                    return (EntityReference)Parameters["Entity"];
                return default(EntityReference);
            }
            set { Parameters["Entity"] = value; }
        }
        public int PageNumber
        {
            get
            {
                if (Parameters.Contains("PageNumber"))
                    return (int)Parameters["PageNumber"];
                return default(int);
            }
            set { Parameters["PageNumber"] = value; }
        }
        public int PageSize
        {
            get
            {
                if (Parameters.Contains("PageSize"))
                    return (int)Parameters["PageSize"];
                return default(int);
            }
            set { Parameters["PageSize"] = value; }
        }
        public int CommentsPerPost
        {
            get
            {
                if (Parameters.Contains("CommentsPerPost"))
                    return (int)Parameters["CommentsPerPost"];
                return default(int);
            }
            set { Parameters["CommentsPerPost"] = value; }
        }
        public DateTime StartDate
        {
            get
            {
                if (Parameters.Contains("StartDate"))
                    return (DateTime)Parameters["StartDate"];
                return default(DateTime);
            }
            set { Parameters["StartDate"] = value; }
        }
        public DateTime EndDate
        {
            get
            {
                if (Parameters.Contains("EndDate"))
                    return (DateTime)Parameters["EndDate"];
                return default(DateTime);
            }
            set { Parameters["EndDate"] = value; }
        }
        public OptionSetValue Type
        {
            get
            {
                if (Parameters.Contains("Type"))
                    return (OptionSetValue)Parameters["Type"];
                return default(OptionSetValue);
            }
            set { Parameters["Type"] = value; }
        }
        public OptionSetValue Source
        {
            get
            {
                if (Parameters.Contains("Source"))
                    return (OptionSetValue)Parameters["Source"];
                return default(OptionSetValue);
            }
            set { Parameters["Source"] = value; }
        }
        public RetrieveRecordWallRequest()
        {
            this.ResponseType = new RetrieveRecordWallResponse();
            this.RequestName = "RetrieveRecordWall";
        }
        internal override string GetRequestBody()
        {
            Parameters["Entity"] = Entity;
            Parameters["PageNumber"] = PageNumber;
            Parameters["PageSize"] = PageSize;
            Parameters["CommentsPerPost"] = CommentsPerPost;
            Parameters["StartDate"] = StartDate;
            Parameters["EndDate"] = EndDate;
            Parameters["Type"] = Type;
            Parameters["Source"] = Source;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveRecordWallResponse : OrganizationResponse
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

    #endregion RetrieveRecordWall

    #region RetrieveRequiredComponents
    public sealed class RetrieveRequiredComponentsRequest : OrganizationRequest
    {
        public Guid ObjectId
        {
            get
            {
                if (Parameters.Contains("ObjectId"))
                    return (Guid)Parameters["ObjectId"];
                return default(Guid);
            }
            set { Parameters["ObjectId"] = value; }
        }
        public int ComponentType
        {
            get
            {
                if (Parameters.Contains("ComponentType"))
                    return (int)Parameters["ComponentType"];
                return default(int);
            }
            set { Parameters["ComponentType"] = value; }
        }
        public RetrieveRequiredComponentsRequest()
        {
            this.ResponseType = new RetrieveRequiredComponentsResponse();
            this.RequestName = "RetrieveRequiredComponents";
        }
        internal override string GetRequestBody()
        {
            Parameters["ObjectId"] = ObjectId;
            Parameters["ComponentType"] = ComponentType;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveRequiredComponentsResponse : OrganizationResponse
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

    #endregion RetrieveRequiredComponents

    #region RetrieveRolePrivilegesRole
    public sealed class RetrieveRolePrivilegesRoleRequest : OrganizationRequest
    {
        public Guid RoleId
        {
            get
            {
                if (Parameters.Contains("RoleId"))
                    return (Guid)Parameters["RoleId"];
                return default(Guid);
            }
            set { Parameters["RoleId"] = value; }
        }
        public RetrieveRolePrivilegesRoleRequest()
        {
            this.ResponseType = new RetrieveRolePrivilegesRoleResponse();
            this.RequestName = "RetrieveRolePrivilegesRole";
        }
        internal override string GetRequestBody()
        {
            Parameters["RoleId"] = RoleId;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveRolePrivilegesRoleResponse : OrganizationResponse
    {
        public RolePrivilege[] RolePrivileges { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "RolePrivileges")
                {
                    List<RolePrivilege> list = new List<RolePrivilege>();
                    foreach (var item in result.Element(Util.ns.b + "value").Elements(Util.ns.g + "RolePrivilege"))
                    {
                        list.Add(RolePrivilege.LoadFromXml(item));
                    }
                    this.RolePrivileges = list.ToArray();
                }
            }
        }
    }

    #endregion RetrieveRolePrivilegesRole

    #region RetrieveSharedPrincipalsAndAccess
    public sealed class RetrieveSharedPrincipalsAndAccessRequest : OrganizationRequest
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
        public RetrieveSharedPrincipalsAndAccessRequest()
        {
            this.ResponseType = new RetrieveSharedPrincipalsAndAccessResponse();
            this.RequestName = "RetrieveSharedPrincipalsAndAccess";
        }
        internal override string GetRequestBody()
        {
            Parameters["Target"] = Target;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveSharedPrincipalsAndAccessResponse : OrganizationResponse
    {
        public PrincipalAccess[] PrincipalAccesses { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "PrincipalAccesses")
                {
                    List<PrincipalAccess> list = new List<PrincipalAccess>();
                    foreach (var item in result.Element(Util.ns.b + "value").Elements(Util.ns.g + "PrincipalAccess"))
                    {
                        list.Add(PrincipalAccess.LoadFromXml(item));
                    }
                    this.PrincipalAccesses = list.ToArray();
                }
            }
        }
    }

    #endregion RetrieveSharedPrincipalsAndAccess

    #region RetrieveSubGroupsResourceGroup
    public sealed class RetrieveSubGroupsResourceGroupRequest : OrganizationRequest
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
        public Guid ResourceGroupId
        {
            get
            {
                if (Parameters.Contains("ResourceGroupId"))
                    return (Guid)Parameters["ResourceGroupId"];
                return default(Guid);
            }
            set { Parameters["ResourceGroupId"] = value; }
        }
        public RetrieveSubGroupsResourceGroupRequest()
        {
            this.ResponseType = new RetrieveSubGroupsResourceGroupResponse();
            this.RequestName = "RetrieveSubGroupsResourceGroup";
        }
        internal override string GetRequestBody()
        {
            Parameters["Query"] = Query;
            Parameters["ResourceGroupId"] = ResourceGroupId;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveSubGroupsResourceGroupResponse : OrganizationResponse
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

    #endregion RetrieveSubGroupsResourceGroup

    #region RetrieveSubsidiaryTeamsBusinessUnit
    public sealed class RetrieveSubsidiaryTeamsBusinessUnitRequest : OrganizationRequest
    {
        public Guid EntityId
        {
            get
            {
                if (Parameters.Contains("EntityId"))
                    return (Guid)Parameters["EntityId"];
                return default(Guid);
            }
            set { Parameters["EntityId"] = value; }
        }
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
        public RetrieveSubsidiaryTeamsBusinessUnitRequest()
        {
            this.ResponseType = new RetrieveSubsidiaryTeamsBusinessUnitResponse();
            this.RequestName = "RetrieveSubsidiaryTeamsBusinessUnit";
        }
        internal override string GetRequestBody()
        {
            Parameters["EntityId"] = EntityId;
            Parameters["ColumnSet"] = ColumnSet;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveSubsidiaryTeamsBusinessUnitResponse : OrganizationResponse
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

    #endregion RetrieveSubsidiaryTeamsBusinessUnit

    #region RetrieveSubsidiaryUsersBusinessUnit
    public sealed class RetrieveSubsidiaryUsersBusinessUnitRequest : OrganizationRequest
    {
        public Guid EntityId
        {
            get
            {
                if (Parameters.Contains("EntityId"))
                    return (Guid)Parameters["EntityId"];
                return default(Guid);
            }
            set { Parameters["EntityId"] = value; }
        }
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
        public RetrieveSubsidiaryUsersBusinessUnitRequest()
        {
            this.ResponseType = new RetrieveSubsidiaryUsersBusinessUnitResponse();
            this.RequestName = "RetrieveSubsidiaryUsersBusinessUnit";
        }
        internal override string GetRequestBody()
        {
            Parameters["EntityId"] = EntityId;
            Parameters["ColumnSet"] = ColumnSet;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveSubsidiaryUsersBusinessUnitResponse : OrganizationResponse
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

    #endregion RetrieveSubsidiaryUsersBusinessUnit

    #region RetrieveTeamPrivileges
    public sealed class RetrieveTeamPrivilegesRequest : OrganizationRequest
    {
        public Guid TeamId
        {
            get
            {
                if (Parameters.Contains("TeamId"))
                    return (Guid)Parameters["TeamId"];
                return default(Guid);
            }
            set { Parameters["TeamId"] = value; }
        }
        public RetrieveTeamPrivilegesRequest()
        {
            this.ResponseType = new RetrieveTeamPrivilegesResponse();
            this.RequestName = "RetrieveTeamPrivileges";
        }
        internal override string GetRequestBody()
        {
            Parameters["TeamId"] = TeamId;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveTeamPrivilegesResponse : OrganizationResponse
    {
        public RolePrivilege[] RolePrivileges { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.            
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "RolePrivileges")
                {
                    List<RolePrivilege> list = new List<RolePrivilege>();
                    foreach (var item in result.Element(Util.ns.b + "value").Elements(Util.ns.g + "RolePrivilege"))
                    {
                        list.Add(RolePrivilege.LoadFromXml(item));
                    }
                    this.RolePrivileges = list.ToArray();
                }
            }
        }
    }

    #endregion RetrieveTeamPrivileges

    #region RetrieveTeamsSystemUser
    public sealed class RetrieveTeamsSystemUserRequest : OrganizationRequest
    {
        public Guid EntityId
        {
            get
            {
                if (Parameters.Contains("EntityId"))
                    return (Guid)Parameters["EntityId"];
                return default(Guid);
            }
            set { Parameters["EntityId"] = value; }
        }
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
        public RetrieveTeamsSystemUserRequest()
        {
            this.ResponseType = new RetrieveTeamsSystemUserResponse();
            this.RequestName = "RetrieveTeamsSystemUser";
        }
        internal override string GetRequestBody()
        {
            Parameters["EntityId"] = EntityId;
            Parameters["ColumnSet"] = ColumnSet;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveTeamsSystemUserResponse : OrganizationResponse
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

    #endregion RetrieveTeamsSystemUser

    #region RetrieveUnpublished
    public sealed class RetrieveUnpublishedRequest : OrganizationRequest
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
        public RetrieveUnpublishedRequest()
        {
            this.ResponseType = new RetrieveUnpublishedResponse();
            this.RequestName = "RetrieveUnpublished";
        }
        internal override string GetRequestBody()
        {
            Parameters["ColumnSet"] = ColumnSet;
            Parameters["Target"] = Target;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveUnpublishedResponse : OrganizationResponse
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

    #endregion RetrieveUnpublished

    #region RetrieveUnpublishedMultiple
    public sealed class RetrieveUnpublishedMultipleRequest : OrganizationRequest
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
        public RetrieveUnpublishedMultipleRequest()
        {
            this.ResponseType = new RetrieveUnpublishedMultipleResponse();
            this.RequestName = "RetrieveUnpublishedMultiple";
        }
        internal override string GetRequestBody()
        {
            Parameters["Query"] = Query;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveUnpublishedMultipleResponse : OrganizationResponse
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

    #endregion RetrieveUnpublishedMultiple

    #region RetrieveUserPrivileges
    public sealed class RetrieveUserPrivilegesRequest : OrganizationRequest
    {
        public Guid UserId
        {
            get
            {
                if (Parameters.Contains("UserId"))
                    return (Guid)Parameters["UserId"];
                return default(Guid);
            }
            set { Parameters["UserId"] = value; }
        }
        public RetrieveUserPrivilegesRequest()
        {
            this.ResponseType = new RetrieveUserPrivilegesResponse();
            this.RequestName = "RetrieveUserPrivileges";
        }
        internal override string GetRequestBody()
        {
            Parameters["UserId"] = UserId;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveUserPrivilegesResponse : OrganizationResponse
    {
        public RolePrivilege[] RolePrivileges { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.            
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "RolePrivileges")
                {
                    List<RolePrivilege> list = new List<RolePrivilege>();
                    foreach (var item in result.Element(Util.ns.b + "value").Elements(Util.ns.g + "RolePrivilege"))
                    {
                        list.Add(RolePrivilege.LoadFromXml(item));
                    }
                    this.RolePrivileges = list.ToArray();
                }
            }
        }
    }

    #endregion RetrieveUserPrivileges

    #region RetrieveUserSettingsSystemUser
    public sealed class RetrieveUserSettingsSystemUserRequest : OrganizationRequest
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
        public Guid EntityId
        {
            get
            {
                if (Parameters.Contains("EntityId"))
                    return (Guid)Parameters["EntityId"];
                return default(Guid);
            }
            set { Parameters["EntityId"] = value; }
        }
        public RetrieveUserSettingsSystemUserRequest()
        {
            this.ResponseType = new RetrieveUserSettingsSystemUserResponse();
            this.RequestName = "RetrieveUserSettingsSystemUser";
        }
        internal override string GetRequestBody()
        {
            Parameters["ColumnSet"] = ColumnSet;
            Parameters["EntityId"] = EntityId;
            return GetSoapBody();
        }
    }
    public sealed class RetrieveUserSettingsSystemUserResponse : OrganizationResponse
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

    #endregion RetrieveUserSettingsSystemUser

    #region RetrieveVersion
    public sealed class RetrieveVersionRequest : OrganizationRequest
    {
        public RetrieveVersionRequest()
        {
            this.ResponseType = new RetrieveVersionResponse();
            this.RequestName = "RetrieveVersion";
        }
        internal override string GetRequestBody()
        {
            return GetSoapBody();
        }
    }
    public sealed class RetrieveVersionResponse : OrganizationResponse
    {
        public string Version { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "Version")
                    this.Version = result.Element(Util.ns.b + "value").Value;
            }
        }
    }

    #endregion RetrieveVersion

    #region RevertProduct
    public sealed class RevertProductRequest : OrganizationRequest
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
        public RevertProductRequest()
        {
            this.ResponseType = new RevertProductResponse();
            this.RequestName = "RevertProduct";
        }
        internal override string GetRequestBody()
        {
            Parameters["Target"] = Target;           
            return GetSoapBody();
        }
    }
    public sealed class RevertProductResponse : OrganizationResponse
    {
    }

    #endregion RevertProduct

    #region ReviseQuote
    public sealed class ReviseQuoteRequest : OrganizationRequest
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
        public Guid QuoteId
        {
            get
            {
                if (Parameters.Contains("QuoteId"))
                    return (Guid)Parameters["QuoteId"];
                return default(Guid);
            }
            set { Parameters["QuoteId"] = value; }
        }
        public ReviseQuoteRequest()
        {
            this.ResponseType = new ReviseQuoteResponse();
            this.RequestName = "ReviseQuote";
        }
        internal override string GetRequestBody()
        {
            Parameters["ColumnSet"] = ColumnSet;
            Parameters["QuoteId"] = QuoteId;
            return GetSoapBody();
        }
    }
    public sealed class ReviseQuoteResponse : OrganizationResponse
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

    #endregion ReviseQuote

    #region RevokeAccess
    public sealed class RevokeAccessRequest : OrganizationRequest
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
        public EntityReference Revokee
        {
            get
            {
                if (Parameters.Contains("Revokee"))
                    return (EntityReference)Parameters["Revokee"];
                return default(EntityReference);
            }
            set { Parameters["Revokee"] = value; }
        }
        public RevokeAccessRequest()
        {
            this.ResponseType = new RevokeAccessResponse();
            this.RequestName = "RevokeAccess";
        }
        internal override string GetRequestBody()
        {
            Parameters["Target"] = Target;
            Parameters["Revokee"] = Revokee;
            return GetSoapBody();
        }
    }
    public sealed class RevokeAccessResponse : OrganizationResponse
    {
    }

    #endregion RevokeAccess

    #region Rollup
    public sealed class RollupRequest : OrganizationRequest
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
        public RollupType RollupType
        {
            get
            {
                if (Parameters.Contains("RollupType"))
                    return (RollupType)Parameters["RollupType"];
                return default(RollupType);
            }
            set { Parameters["RollupType"] = value; }
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
        public RollupRequest()
        {
            this.ResponseType = new RollupResponse();
            this.RequestName = "Rollup";
        }
        internal override string GetRequestBody()
        {
            Parameters["Query"] = Query;
            Parameters["RollupType"] = RollupType;
            Parameters["Target"] = Target;
            return GetSoapBody();
        }
    }
    public sealed class RollupResponse : OrganizationResponse
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

    #endregion Rollup

    #region RouteTo
    public sealed class RouteToRequest : OrganizationRequest
    {
        public Guid QueueItemId
        {
            get
            {
                if (Parameters.Contains("QueueItemId"))
                    return (Guid)Parameters["QueueItemId"];
                return default(Guid);
            }
            set { Parameters["QueueItemId"] = value; }
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

        public RouteToRequest()
        {
            this.ResponseType = new RouteToResponse();
            this.RequestName = "RouteTo";
        }
        internal override string GetRequestBody()
        {
            Parameters["QueueItemId"] = QueueItemId;
            Parameters["Target"] = Target;
            return GetSoapBody();
        }
    }
    public sealed class RouteToResponse : OrganizationResponse
    {
    }

    #endregion RouteTo 

    #region Search
    public sealed class SearchRequest : OrganizationRequest
    {
        public AppointmentRequest AppointmentRequest
        {
            get
            {
                if (Parameters.Contains("AppointmentRequest"))
                    return (AppointmentRequest)Parameters["AppointmentRequest"];
                return default(AppointmentRequest);
            }
            set { Parameters["AppointmentRequest"] = value; }
        }
        public SearchRequest()
        {
            this.ResponseType = new SearchResponse();
            this.RequestName = "Search";
        }
        internal override string GetRequestBody()
        {
            Parameters["AppointmentRequest"] = AppointmentRequest;
            return GetSoapBody();
        }
    }
    public sealed class SearchResponse : OrganizationResponse
    {
        public SearchResults SearchResults { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "SearchResults")
                    this.SearchResults = SearchResults.LoadFromXml(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion Search

    #region SearchByBodyKbArticle
    public sealed class SearchByBodyKbArticleRequest : OrganizationRequest
    {
        public QueryBase QueryExpression
        {
            get
            {
                if (Parameters.Contains("QueryExpression"))
                    return (QueryBase)Parameters["QueryExpression"];
                return default(QueryBase);
            }
            set { Parameters["QueryExpression"] = value; }
        }
        public string SearchText
        {
            get
            {
                if (Parameters.Contains("SearchText"))
                    return (string)Parameters["SearchText"];
                return default(string);
            }
            set { Parameters["SearchText"] = value; }
        }
        public Guid SubjectId
        {
            get
            {
                if (Parameters.Contains("SubjectId"))
                    return (Guid)Parameters["SubjectId"];
                return default(Guid);
            }
            set { Parameters["SubjectId"] = value; }
        }
        public bool UseInflection
        {
            get
            {
                if (Parameters.Contains("UseInflection"))
                    return (bool)Parameters["UseInflection"];
                return default(bool);
            }
            set { Parameters["UseInflection"] = value; }
        }
        public SearchByBodyKbArticleRequest()
        {
            this.ResponseType = new SearchByBodyKbArticleResponse();
            this.RequestName = "SearchByBodyKbArticle";
        }
        internal override string GetRequestBody()
        {
            Parameters["QueryExpression"] = QueryExpression;
            Parameters["SearchText"] = SearchText;
            Parameters["SubjectId"] = SubjectId;
            Parameters["UseInflection"] = UseInflection;
            return GetSoapBody();
        }
    }
    public sealed class SearchByBodyKbArticleResponse : OrganizationResponse
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

    #endregion SearchByBodyKbArticle

    #region SearchByKeywordsKbArticle
    public sealed class SearchByKeywordsKbArticleRequest : OrganizationRequest
    {
        public QueryBase QueryExpression
        {
            get
            {
                if (Parameters.Contains("QueryExpression"))
                    return (QueryBase)Parameters["QueryExpression"];
                return default(QueryBase);
            }
            set { Parameters["QueryExpression"] = value; }
        }
        public string SearchText
        {
            get
            {
                if (Parameters.Contains("SearchText"))
                    return (string)Parameters["SearchText"];
                return default(string);
            }
            set { Parameters["SearchText"] = value; }
        }
        public Guid SubjectId
        {
            get
            {
                if (Parameters.Contains("SubjectId"))
                    return (Guid)Parameters["SubjectId"];
                return default(Guid);
            }
            set { Parameters["SubjectId"] = value; }
        }
        public bool UseInflection
        {
            get
            {
                if (Parameters.Contains("UseInflection"))
                    return (bool)Parameters["UseInflection"];
                return default(bool);
            }
            set { Parameters["UseInflection"] = value; }
        }
        public SearchByKeywordsKbArticleRequest()
        {
            this.ResponseType = new SearchByKeywordsKbArticleResponse();
            this.RequestName = "SearchByKeywordsKbArticle";
        }
        internal override string GetRequestBody()
        {
            Parameters["QueryExpression"] = QueryExpression;
            Parameters["SearchText"] = SearchText;
            Parameters["SubjectId"] = SubjectId;
            Parameters["UseInflection"] = UseInflection;
            return GetSoapBody();
        }
    }
    public sealed class SearchByKeywordsKbArticleResponse : OrganizationResponse
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

    #endregion SearchByKeywordsKbArticle

    #region SearchByTitleKbArticle
    public sealed class SearchByTitleKbArticleRequest : OrganizationRequest
    {
        public QueryBase QueryExpression
        {
            get
            {
                if (Parameters.Contains("QueryExpression"))
                    return (QueryBase)Parameters["QueryExpression"];
                return default(QueryBase);
            }
            set { Parameters["QueryExpression"] = value; }
        }
        public string SearchText
        {
            get
            {
                if (Parameters.Contains("SearchText"))
                    return (string)Parameters["SearchText"];
                return default(string);
            }
            set { Parameters["SearchText"] = value; }
        }
        public Guid SubjectId
        {
            get
            {
                if (Parameters.Contains("SubjectId"))
                    return (Guid)Parameters["SubjectId"];
                return default(Guid);
            }
            set { Parameters["SubjectId"] = value; }
        }
        public bool UseInflection
        {
            get
            {
                if (Parameters.Contains("UseInflection"))
                    return (bool)Parameters["UseInflection"];
                return default(bool);
            }
            set { Parameters["UseInflection"] = value; }
        }
        public SearchByTitleKbArticleRequest()
        {
            this.ResponseType = new SearchByTitleKbArticleResponse();
            this.RequestName = "SearchByTitleKbArticle";
        }
        internal override string GetRequestBody()
        {
            Parameters["QueryExpression"] = QueryExpression;
            Parameters["SearchText"] = SearchText;
            Parameters["SubjectId"] = SubjectId;
            Parameters["UseInflection"] = UseInflection;
            return GetSoapBody();
        }
    }
    public sealed class SearchByTitleKbArticleResponse : OrganizationResponse
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

    #endregion SearchByTitleKbArticle

    #region SendBulkMail
    public sealed class SendBulkMailRequest : OrganizationRequest
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
        public Guid RegardingId
        {
            get
            {
                if (Parameters.Contains("RegardingId"))
                    return (Guid)Parameters["RegardingId"];
                return default(Guid);
            }
            set { Parameters["RegardingId"] = value; }
        }
        public string RegardingType
        {
            get
            {
                if (Parameters.Contains("RegardingType"))
                    return (string)Parameters["RegardingType"];
                return default(string);
            }
            set { Parameters["RegardingType"] = value; }
        }
        public EntityReference Sender
        {
            get
            {
                if (Parameters.Contains("Sender"))
                    return (EntityReference)Parameters["Sender"];
                return default(EntityReference);
            }
            set { Parameters["Sender"] = value; }
        }
        public Guid TemplateId
        {
            get
            {
                if (Parameters.Contains("TemplateId"))
                    return (Guid)Parameters["TemplateId"];
                return default(Guid);
            }
            set { Parameters["TemplateId"] = value; }
        }
        public SendBulkMailRequest()
        {
            this.ResponseType = new SendBulkMailResponse();
            this.RequestName = "SendBulkMail";
        }
        internal override string GetRequestBody()
        {
            Parameters["Query"] = Query;
            Parameters["RegardingId"] = RegardingId;
            Parameters["RegardingType"] = RegardingType;
            Parameters["Sender"] = Sender;
            Parameters["TemplateId"] = TemplateId;
            return GetSoapBody();
        }
    }
    public sealed class SendBulkMailResponse : OrganizationResponse
    {
    }

    #endregion SendBulkMail

    #region SendEmail
    public sealed class SendEmailRequest : OrganizationRequest
    {
        public Guid EmailId
        {
            get
            {
                if (Parameters.Contains("EmailId"))
                    return (Guid)Parameters["EmailId"];
                return default(Guid);
            }
            set { Parameters["EmailId"] = value; }
        }
        public bool IssueSend
        {
            get
            {
                if (Parameters.Contains("IssueSend"))
                    return (bool)Parameters["IssueSend"];
                return default(bool);
            }
            set { Parameters["IssueSend"] = value; }
        }
        public string TrackingToken
        {
            get
            {
                if (Parameters.Contains("TrackingToken"))
                    return (string)Parameters["TrackingToken"];
                return default(string);
            }
            set { Parameters["TrackingToken"] = value; }
        }
        public SendEmailRequest()
        {
            this.ResponseType = new SendEmailResponse();
            this.RequestName = "SendEmail";
        }
        internal override string GetRequestBody()
        {
            Parameters["EmailId"] = EmailId;
            Parameters["IssueSend"] = IssueSend;
            Parameters["TrackingToken"] = TrackingToken;
            return GetSoapBody();
        }
    }
    public sealed class SendEmailResponse : OrganizationResponse
    {
        public string Subject { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "Subject")
                    this.Subject = result.Element(Util.ns.b + "value").Value;
            }
        }
    }

    #endregion SendEmail

    #region SendEmailFromTemplate
    public sealed class SendEmailFromTemplateRequest : OrganizationRequest
    {
        public Guid RegardingId
        {
            get
            {
                if (Parameters.Contains("RegardingId"))
                    return (Guid)Parameters["RegardingId"];
                return default(Guid);
            }
            set { Parameters["RegardingId"] = value; }
        }
        public string RegardingType
        {
            get
            {
                if (Parameters.Contains("RegardingType"))
                    return (string)Parameters["RegardingType"];
                return default(string);
            }
            set { Parameters["RegardingType"] = value; }
        }
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
        public Guid TemplateId
        {
            get
            {
                if (Parameters.Contains("TemplateId"))
                    return (Guid)Parameters["TemplateId"];
                return default(Guid);
            }
            set { Parameters["TemplateId"] = value; }
        }
        public SendEmailFromTemplateRequest()
        {
            this.ResponseType = new SendEmailFromTemplateResponse();
            this.RequestName = "SendEmailFromTemplate";
        }
        internal override string GetRequestBody()
        {
            Parameters["RegardingId"] = RegardingId;
            Parameters["RegardingType"] = RegardingType;
            Parameters["Target"] = Target;
            Parameters["TemplateId"] = TemplateId;
            return GetSoapBody();
        }
    }
    public sealed class SendEmailFromTemplateResponse : OrganizationResponse
    {
        public Guid Id { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "Id")
                    this.Id = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion SendEmailFromTemplate

    #region SendFax
    public sealed class SendFaxRequest : OrganizationRequest
    {
        public Guid FaxId
        {
            get
            {
                if (Parameters.Contains("FaxId"))
                    return (Guid)Parameters["FaxId"];
                return default(Guid);
            }
            set { Parameters["FaxId"] = value; }
        }
        public bool IssueSend
        {
            get
            {
                if (Parameters.Contains("IssueSend"))
                    return (bool)Parameters["IssueSend"];
                return default(bool);
            }
            set { Parameters["IssueSend"] = value; }
        }
        public SendFaxRequest()
        {
            this.ResponseType = new SendFaxResponse();
            this.RequestName = "SendFax";
        }
        internal override string GetRequestBody()
        {
            Parameters["FaxId"] = FaxId;
            Parameters["IssueSend"] = IssueSend;
            return GetSoapBody();
        }
    }
    public sealed class SendFaxResponse : OrganizationResponse
    {
    }

    #endregion SendFax

    #region SendTemplate
    public sealed class SendTemplateRequest : OrganizationRequest
    {
        public OptionSetValue DeliveryPriorityCode
        {
            get
            {
                if (Parameters.Contains("DeliveryPriorityCode"))
                    return (OptionSetValue)Parameters["DeliveryPriorityCode"];
                return default(OptionSetValue);
            }
            set { Parameters["DeliveryPriorityCode"] = value; }
        }
        public Guid[] RecipientIds
        {
            get
            {
                if (Parameters.Contains("RecipientIds"))
                    return (Guid[])Parameters["RecipientIds"];
                return default(Guid[]);
            }
            set { Parameters["RecipientIds"] = value; }
        }
        public string RecipientType
        {
            get
            {
                if (Parameters.Contains("RecipientType"))
                    return (string)Parameters["RecipientType"];
                return default(string);
            }
            set { Parameters["RecipientType"] = value; }
        }
        public Guid RegardingId
        {
            get
            {
                if (Parameters.Contains("RegardingId"))
                    return (Guid)Parameters["RegardingId"];
                return default(Guid);
            }
            set { Parameters["RegardingId"] = value; }
        }
        public string RegardingType
        {
            get
            {
                if (Parameters.Contains("RegardingType"))
                    return (string)Parameters["RegardingType"];
                return default(string);
            }
            set { Parameters["RegardingType"] = value; }
        }
        public EntityReference Sender
        {
            get
            {
                if (Parameters.Contains("Sender"))
                    return (EntityReference)Parameters["Sender"];
                return default(EntityReference);
            }
            set { Parameters["Sender"] = value; }
        }
        public Guid TemplateId
        {
            get
            {
                if (Parameters.Contains("TemplateId"))
                    return (Guid)Parameters["TemplateId"];
                return default(Guid);
            }
            set { Parameters["TemplateId"] = value; }
        }
        public SendTemplateRequest()
        {
            this.ResponseType = new SendTemplateResponse();
            this.RequestName = "SendTemplate";
        }
        internal override string GetRequestBody()
        {
            Parameters["DeliveryPriorityCode"] = DeliveryPriorityCode;
            Parameters["RecipientIds"] = RecipientIds;
            Parameters["RecipientType"] = RecipientType;
            Parameters["RegardingId"] = RegardingId;
            Parameters["RegardingType"] = RegardingType;
            Parameters["Sender"] = Sender;
            Parameters["TemplateId"] = TemplateId;
            return GetSoapBody();
        }
    }
    public sealed class SendTemplateResponse : OrganizationResponse
    {
    }

    #endregion SendTemplate

    #region SetBusinessEquipment
    public sealed class SetBusinessEquipmentRequest : OrganizationRequest
    {
        public Guid BusinessUnitId
        {
            get
            {
                if (Parameters.Contains("BusinessUnitId"))
                    return (Guid)Parameters["BusinessUnitId"];
                return default(Guid);
            }
            set { Parameters["BusinessUnitId"] = value; }
        }
        public Guid EquipmentId
        {
            get
            {
                if (Parameters.Contains("EquipmentId"))
                    return (Guid)Parameters["EquipmentId"];
                return default(Guid);
            }
            set { Parameters["EquipmentId"] = value; }
        }
        public SetBusinessEquipmentRequest()
        {
            this.ResponseType = new SetBusinessEquipmentResponse();
            this.RequestName = "SetBusinessEquipment";
        }
        internal override string GetRequestBody()
        {
            Parameters["BusinessUnitId"] = BusinessUnitId;
            Parameters["EquipmentId"] = EquipmentId;
            return GetSoapBody();
        }
    }
    public sealed class SetBusinessEquipmentResponse : OrganizationResponse
    {
    }

    #endregion SetBusinessEquipment

    #region SetBusinessSystemUser
    public sealed class SetBusinessSystemUserRequest : OrganizationRequest
    {
        public Guid BusinessId
        {
            get
            {
                if (Parameters.Contains("BusinessId"))
                    return (Guid)Parameters["BusinessId"];
                return default(Guid);
            }
            set { Parameters["BusinessId"] = value; }
        }
        public EntityReference ReassignPrincipal
        {
            get
            {
                if (Parameters.Contains("ReassignPrincipal"))
                    return (EntityReference)Parameters["ReassignPrincipal"];
                return default(EntityReference);
            }
            set { Parameters["ReassignPrincipal"] = value; }
        }
        public Guid UserId
        {
            get
            {
                if (Parameters.Contains("UserId"))
                    return (Guid)Parameters["UserId"];
                return default(Guid);
            }
            set { Parameters["UserId"] = value; }
        }
        public SetBusinessSystemUserRequest()
        {
            this.ResponseType = new SetBusinessSystemUserResponse();
            this.RequestName = "SetBusinessSystemUser";
        }
        internal override string GetRequestBody()
        {
            Parameters["BusinessId"] = BusinessId;
            Parameters["ReassignPrincipal"] = ReassignPrincipal;
            Parameters["UserId"] = UserId;
            return GetSoapBody();
        }
    }
    public sealed class SetBusinessSystemUserResponse : OrganizationResponse
    {
    }

    #endregion SetBusinessSystemUser

    #region SetLocLabels
    public sealed class SetLocLabelsRequest : OrganizationRequest
    {
        public EntityReference EntityMoniker
        {
            get
            {
                if (Parameters.Contains("EntityMoniker"))
                    return (EntityReference)Parameters["EntityMoniker"];
                return default(EntityReference);
            }
            set { Parameters["EntityMoniker"] = value; }
        }
        public string AttributeName
        {
            get
            {
                if (Parameters.Contains("AttributeName"))
                    return (string)Parameters["AttributeName"];
                return default(string);
            }
            set { Parameters["AttributeName"] = value; }
        }
        public LocalizedLabel[] Labels
        {
            get
            {
                if (Parameters.Contains("Labels"))
                    return (LocalizedLabel[])Parameters["Labels"];
                return default(LocalizedLabel[]);
            }
            set { Parameters["Labels"] = value; }
        }
        public SetLocLabelsRequest()
        {
            this.ResponseType = new SetLocLabelsResponse();
            this.RequestName = "SetLocLabels";
        }
        internal override string GetRequestBody()
        {
            Parameters["EntityMoniker"] = EntityMoniker;
            Parameters["AttributeName"] = AttributeName;
            Parameters["Labels"] = Labels;
            return GetSoapBody();
        }
    }
    public sealed class SetLocLabelsResponse : OrganizationResponse
    {
    }

    #endregion SetLocLabels

    #region SetParentBusinessUnit
    public sealed class SetParentBusinessUnitRequest : OrganizationRequest
    {
        public Guid BusinessUnitId
        {
            get
            {
                if (Parameters.Contains("BusinessUnitId"))
                    return (Guid)Parameters["BusinessUnitId"];
                return default(Guid);
            }
            set { Parameters["BusinessUnitId"] = value; }
        }
        public Guid ParentId
        {
            get
            {
                if (Parameters.Contains("ParentId"))
                    return (Guid)Parameters["ParentId"];
                return default(Guid);
            }
            set { Parameters["ParentId"] = value; }
        }
        public SetParentBusinessUnitRequest()
        {
            this.ResponseType = new SetParentBusinessUnitResponse();
            this.RequestName = "SetParentBusinessUnit";
        }
        internal override string GetRequestBody()
        {
            Parameters["BusinessUnitId"] = BusinessUnitId;
            Parameters["ParentId"] = ParentId;
            return GetSoapBody();
        }
    }
    public sealed class SetParentBusinessUnitResponse : OrganizationResponse
    {
    }

    #endregion SetParentBusinessUnit

    #region SetParentSystemUser
    public sealed class SetParentSystemUserRequest : OrganizationRequest
    {
        public bool KeepChildUsers
        {
            get
            {
                if (Parameters.Contains("KeepChildUsers"))
                    return (bool)Parameters["KeepChildUsers"];
                return default(bool);
            }
            set { Parameters["KeepChildUsers"] = value; }
        }
        public Guid ParentId
        {
            get
            {
                if (Parameters.Contains("ParentId"))
                    return (Guid)Parameters["ParentId"];
                return default(Guid);
            }
            set { Parameters["ParentId"] = value; }
        }
        public Guid UserId
        {
            get
            {
                if (Parameters.Contains("UserId"))
                    return (Guid)Parameters["UserId"];
                return default(Guid);
            }
            set { Parameters["UserId"] = value; }
        }
        public SetParentSystemUserRequest()
        {
            this.ResponseType = new SetParentSystemUserResponse();
            this.RequestName = "SetParentSystemUser";
        }
        internal override string GetRequestBody()
        {
            Parameters["KeepChildUsers"] = KeepChildUsers;
            Parameters["ParentId"] = ParentId;
            Parameters["UserId"] = UserId;
            return GetSoapBody();
        }
    }
    public sealed class SetParentSystemUserResponse : OrganizationResponse
    {
    }

    #endregion SetParentSystemUser

    #region SetParentTeam
    public sealed class SetParentTeamRequest : OrganizationRequest
    {
        public Guid BusinessId
        {
            get
            {
                if (Parameters.Contains("BusinessId"))
                    return (Guid)Parameters["BusinessId"];
                return default(Guid);
            }
            set { Parameters["BusinessId"] = value; }
        }
        public Guid TeamId
        {
            get
            {
                if (Parameters.Contains("TeamId"))
                    return (Guid)Parameters["TeamId"];
                return default(Guid);
            }
            set { Parameters["TeamId"] = value; }
        }
        public SetParentTeamRequest()
        {
            this.ResponseType = new SetParentTeamResponse();
            this.RequestName = "SetParentTeam";
        }
        internal override string GetRequestBody()
        {
            Parameters["BusinessId"] = BusinessId;
            Parameters["TeamId"] = TeamId;
            return GetSoapBody();
        }
    }
    public sealed class SetParentTeamResponse : OrganizationResponse
    {
    }

    #endregion SetParentTeam

    #region SetRelated
    public sealed class SetRelatedRequest : OrganizationRequest
    {
        public EntityReference[] Target
        {
            get
            {
                if (Parameters.Contains("Target"))
                    return (EntityReference[])Parameters["Target"];
                return default(EntityReference[]);
            }
            set { Parameters["Target"] = value; }
        }
        public SetRelatedRequest()
        {
            this.ResponseType = new SetRelatedResponse();
            this.RequestName = "SetRelated";
        }
        internal override string GetRequestBody()
        {
            Parameters["Target"] = Target;
            return GetSoapBody();
        }
    }
    public sealed class SetRelatedResponse : OrganizationResponse
    {
    }

    #endregion SetRelated

    #region SetReportRelated
    public sealed class SetReportRelatedRequest : OrganizationRequest
    {
        public Guid ReportId
        {
            get
            {
                if (Parameters.Contains("ReportId"))
                    return (Guid)Parameters["ReportId"];
                return default(Guid);
            }
            set { Parameters["ReportId"] = value; }
        }
        public int[] Entities
        {
            get
            {
                if (Parameters.Contains("Entities"))
                    return (int[])Parameters["Entities"];
                return default(int[]);
            }
            set { Parameters["Entities"] = value; }
        }
        public int[] Categories
        {
            get
            {
                if (Parameters.Contains("Categories"))
                    return (int[])Parameters["Categories"];
                return default(int[]);
            }
            set { Parameters["Categories"] = value; }
        }
        public int[] Visibility
        {
            get
            {
                if (Parameters.Contains("Visibility"))
                    return (int[])Parameters["Visibility"];
                return default(int[]);
            }
            set { Parameters["Visibility"] = value; }
        }
        public SetReportRelatedRequest()
        {
            this.ResponseType = new SetReportRelatedResponse();
            this.RequestName = "SetReportRelated";
        }
        internal override string GetRequestBody()
        {
            Parameters["ReportId"] = ReportId;
            Parameters["Entities"] = Entities;
            Parameters["Categories"] = Categories;
            Parameters["Visibility"] = Visibility;
            return GetSoapBody();
        }
    }
    public sealed class SetReportRelatedResponse : OrganizationResponse
    {
    }

    #endregion SetReportRelated

    #region SetState
    public sealed class SetStateRequest : OrganizationRequest
    {
        public EntityReference EntityMoniker
        {
            get
            {
                if (Parameters.Contains("EntityMoniker"))
                    return (EntityReference)Parameters["EntityMoniker"];
                return default(EntityReference);
            }
            set { Parameters["EntityMoniker"] = value; }
        }
        public OptionSetValue State
        {
            get
            {
                if (Parameters.Contains("State"))
                    return (OptionSetValue)Parameters["State"];
                return default(OptionSetValue);
            }
            set { Parameters["State"] = value; }
        }
        public OptionSetValue Status
        {
            get
            {
                if (Parameters.Contains("Status"))
                    return (OptionSetValue)Parameters["Status"];
                return default(OptionSetValue);
            }
            set { Parameters["Status"] = value; }
        }
        public SetStateRequest()
        {
            this.ResponseType = new SetStateResponse();
            this.RequestName = "SetState";
        }
        internal override string GetRequestBody()
        {
            Parameters["EntityMoniker"] = EntityMoniker;
            Parameters["State"] = State;
            Parameters["Status"] = Status;
            return GetSoapBody();
        }
    }
    public sealed class SetStateResponse : OrganizationResponse
    {
    }

    #endregion SetState

    #region StatusUpdateBulkOperation
    public sealed class StatusUpdateBulkOperationRequest : OrganizationRequest
    {
        public Guid BulkOperationId
        {
            get
            {
                if (Parameters.Contains("BulkOperationId"))
                    return (Guid)Parameters["BulkOperationId"];
                return default(Guid);
            }
            set { Parameters["BulkOperationId"] = value; }
        }
        public int FailureCount
        {
            get
            {
                if (Parameters.Contains("FailureCount"))
                    return (int)Parameters["FailureCount"];
                return default(int);
            }
            set { Parameters["FailureCount"] = value; }
        }
        public int SuccessCount
        {
            get
            {
                if (Parameters.Contains("SuccessCount"))
                    return (int)Parameters["SuccessCount"];
                return default(int);
            }
            set { Parameters["SuccessCount"] = value; }
        }
        public StatusUpdateBulkOperationRequest()
        {
            this.ResponseType = new StatusUpdateBulkOperationResponse();
            this.RequestName = "StatusUpdateBulkOperation";
        }
        internal override string GetRequestBody()
        {
            Parameters["BulkOperationId"] = BulkOperationId;
            Parameters["FailureCount"] = FailureCount;
            Parameters["SuccessCount"] = SuccessCount;
            return GetSoapBody();
        }
    }
    public sealed class StatusUpdateBulkOperationResponse : OrganizationResponse
    {
    }

    #endregion StatusUpdateBulkOperation

    #region TransformImport
    public sealed class TransformImportRequest : OrganizationRequest
    {
        public Guid ImportId
        {
            get
            {
                if (Parameters.Contains("ImportId"))
                    return (Guid)Parameters["ImportId"];
                return default(Guid);
            }
            set { Parameters["ImportId"] = value; }
        }
        public TransformImportRequest()
        {
            this.ResponseType = new TransformImportResponse();
            this.RequestName = "TransformImport";
        }
        internal override string GetRequestBody()
        {
            Parameters["ImportId"] = ImportId;
            return GetSoapBody();
        }
    }
    public sealed class TransformImportResponse : OrganizationResponse
    {
        public Guid AsyncOperationId { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "AsyncOperationId")
                    this.AsyncOperationId = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion TransformImport

    #region TriggerServiceEndpointCheck
    public sealed class TriggerServiceEndpointCheckRequest : OrganizationRequest
    {
        public EntityReference Entity
        {
            get
            {
                if (Parameters.Contains("Entity"))
                    return (EntityReference)Parameters["Entity"];
                return default(EntityReference);
            }
            set { Parameters["Entity"] = value; }
        }
        public TriggerServiceEndpointCheckRequest()
        {
            this.ResponseType = new TriggerServiceEndpointCheckResponse();
            this.RequestName = "TriggerServiceEndpointCheck";
        }
        internal override string GetRequestBody()
        {
            Parameters["Entity"] = Entity;
            return GetSoapBody();
        }
    }
    public sealed class TriggerServiceEndpointCheckResponse : OrganizationResponse
    {
    }

    #endregion TriggerServiceEndpointCheck

    #region UninstallSampleData
    public sealed class UninstallSampleDataRequest : OrganizationRequest
    {
        public UninstallSampleDataRequest()
        {
            this.ResponseType = new UninstallSampleDataResponse();
            this.RequestName = "UninstallSampleData";
        }
        internal override string GetRequestBody()
        {
            return GetSoapBody();
        }
    }
    public sealed class UninstallSampleDataResponse : OrganizationResponse
    {
    }

    #endregion UninstallSampleData

    #region UnlockInvoicePricing
    public sealed class UnlockInvoicePricingRequest : OrganizationRequest
    {
        public Guid InvoiceId
        {
            get
            {
                if (Parameters.Contains("InvoiceId"))
                    return (Guid)Parameters["InvoiceId"];
                return default(Guid);
            }
            set { Parameters["InvoiceId"] = value; }
        }
        public UnlockInvoicePricingRequest()
        {
            this.ResponseType = new UnlockInvoicePricingResponse();
            this.RequestName = "UnlockInvoicePricing";
        }
        internal override string GetRequestBody()
        {
            Parameters["InvoiceId"] = InvoiceId;
            return GetSoapBody();
        }
    }
    public sealed class UnlockInvoicePricingResponse : OrganizationResponse
    {
    }

    #endregion UnlockInvoicePricing

    #region UnlockSalesOrderPricing
    public sealed class UnlockSalesOrderPricingRequest : OrganizationRequest
    {
        public Guid SalesOrderId
        {
            get
            {
                if (Parameters.Contains("SalesOrderId"))
                    return (Guid)Parameters["SalesOrderId"];
                return default(Guid);
            }
            set { Parameters["SalesOrderId"] = value; }
        }
        public UnlockSalesOrderPricingRequest()
        {
            this.ResponseType = new UnlockSalesOrderPricingResponse();
            this.RequestName = "UnlockSalesOrderPricing";
        }
        internal override string GetRequestBody()
        {
            Parameters["SalesOrderId"] = SalesOrderId;
            return GetSoapBody();
        }
    }
    public sealed class UnlockSalesOrderPricingResponse : OrganizationResponse
    {
    }

    #endregion UnlockSalesOrderPricing

    #region UnpublishDuplicateRule
    public sealed class UnpublishDuplicateRuleRequest : OrganizationRequest
    {
        public Guid DuplicateRuleId
        {
            get
            {
                if (Parameters.Contains("DuplicateRuleId"))
                    return (Guid)Parameters["DuplicateRuleId"];
                return default(Guid);
            }
            set { Parameters["DuplicateRuleId"] = value; }
        }
        public UnpublishDuplicateRuleRequest()
        {
            this.ResponseType = new UnpublishDuplicateRuleResponse();
            this.RequestName = "UnpublishDuplicateRule";
        }
        internal override string GetRequestBody()
        {
            Parameters["DuplicateRuleId"] = DuplicateRuleId;
            return GetSoapBody();
        }
    }
    public sealed class UnpublishDuplicateRuleResponse : OrganizationResponse
    {
    }

    #endregion UnpublishDuplicateRule

    #region UpdateProductProperties
    public sealed class UpdateProductPropertiesRequest : OrganizationRequest
    {
        public EntityCollection PropertyInstanceList
        {
            get
            {
                if (Parameters.Contains("PropertyInstanceList"))
                    return (EntityCollection)Parameters["PropertyInstanceList"];
                return default(EntityCollection);
            }
            set { Parameters["PropertyInstanceList"] = value; }
        }
        public UpdateProductPropertiesRequest()
        {
            this.ResponseType = new UpdateProductPropertiesResponse();
            this.RequestName = "UpdateProductProperties";
        }
        internal override string GetRequestBody()
        {
            Parameters["PropertyInstanceList"] = PropertyInstanceList;
            return GetSoapBody();
        }
    }
    public sealed class UpdateProductPropertiesResponse : OrganizationResponse
    {
    }

    #endregion UpdateProductProperties

    #region UpdateUserSettingsSystemUser
    public sealed class UpdateUserSettingsSystemUserRequest : OrganizationRequest
    {
        public Guid UserId
        {
            get
            {
                if (Parameters.Contains("UserId"))
                    return (Guid)Parameters["UserId"];
                return default(Guid);
            }
            set { Parameters["UserId"] = value; }
        }
        public Entity Settings
        {
            get
            {
                if (Parameters.Contains("Settings"))
                    return (Entity)Parameters["Settings"];
                return default(Entity);
            }
            set { Parameters["Settings"] = value; }
        }
        public UpdateUserSettingsSystemUserRequest()
        {
            this.ResponseType = new UpdateUserSettingsSystemUserResponse();
            this.RequestName = "UpdateUserSettingsSystemUser";
        }
        internal override string GetRequestBody()
        {
            Parameters["UserId"] = UserId;
            Parameters["Settings"] = Settings;
            return GetSoapBody();
        }
    }
    public sealed class UpdateUserSettingsSystemUserResponse : OrganizationResponse
    {
    }

    #endregion UpdateUserSettingsSystemUser

    #region UtcTimeFromLocalTime
    public sealed class UtcTimeFromLocalTimeRequest : OrganizationRequest
    {
        public DateTime LocalTime
        {
            get
            {
                if (Parameters.Contains("LocalTime"))
                    return (DateTime)Parameters["LocalTime"];
                return default(DateTime);
            }
            set { Parameters["LocalTime"] = value; }
        }
        public int TimeZoneCode
        {
            get
            {
                if (Parameters.Contains("TimeZoneCode"))
                    return (int)Parameters["TimeZoneCode"];
                return default(int);
            }
            set { Parameters["TimeZoneCode"] = value; }
        }
        public UtcTimeFromLocalTimeRequest()
        {
            this.ResponseType = new UtcTimeFromLocalTimeResponse();
            this.RequestName = "UtcTimeFromLocalTime";
        }
        internal override string GetRequestBody()
        {
            Parameters["LocalTime"] = LocalTime;
            Parameters["TimeZoneCode"] = TimeZoneCode;
            return GetSoapBody();
        }
    }
    public sealed class UtcTimeFromLocalTimeResponse : OrganizationResponse
    {
        public DateTime UtcTime { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "UtcTime")
                    this.UtcTime = (DateTime)(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion UtcTimeFromLocalTime

    #region Validate
    public sealed class ValidateRequest : OrganizationRequest
    {
        public EntityCollection Activities
        {
            get
            {
                if (Parameters.Contains("Activities"))
                    return (EntityCollection)Parameters["Activities"];
                return default(EntityCollection);
            }
            set { Parameters["Activities"] = value; }
        }
        public ValidateRequest()
        {
            this.ResponseType = new ValidateResponse();
            this.RequestName = "Validate";
        }
        internal override string GetRequestBody()
        {
            Parameters["Activities"] = Activities;
            return GetSoapBody();
        }
    }
    public sealed class ValidateResponse : OrganizationResponse
    {
        public ValidationResult[] Result { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "Result")
                {
                    List<ValidationResult> list = new List<ValidationResult>();
                    foreach (var item in result.Element(Util.ns.b + "value").Elements())
                    {
                        list.Add(ValidationResult.LoadFromXml(item));
                    }
                    this.Result = list.ToArray();
                }
            }
        }
    }

    #endregion Validate

    #region ValidateRecurrenceRule
    public sealed class ValidateRecurrenceRuleRequest : OrganizationRequest
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
        public ValidateRecurrenceRuleRequest()
        {
            this.ResponseType = new ValidateRecurrenceRuleResponse();
            this.RequestName = "ValidateRecurrenceRule";
        }
        internal override string GetRequestBody()
        {
            Parameters["Target"] = Target;
            return GetSoapBody();
        }
    }
    public sealed class ValidateRecurrenceRuleResponse : OrganizationResponse
    {
        public string Description { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "Description")
                    this.Description = result.Element(Util.ns.b + "value").Value;
            }
        }
    }

    #endregion ValidateRecurrenceRule

    #region ValidateSavedQuery
    public sealed class ValidateSavedQueryRequest : OrganizationRequest
    {
        public string FetchXml
        {
            get
            {
                if (Parameters.Contains("FetchXml"))
                    return (string)Parameters["FetchXml"];
                return default(string);
            }
            set { Parameters["FetchXml"] = value; }
        }
        public int QueryType
        {
            get
            {
                if (Parameters.Contains("QueryType"))
                    return (int)Parameters["QueryType"];
                return default(int);
            }
            set { Parameters["QueryType"] = value; }
        }
        public ValidateSavedQueryRequest()
        {
            this.ResponseType = new ValidateSavedQueryResponse();
            this.RequestName = "ValidateSavedQuery";
        }
        internal override string GetRequestBody()
        {
            Parameters["FetchXml"] = FetchXml;
            Parameters["QueryType"] = QueryType;
            return GetSoapBody();
        }
    }
    public sealed class ValidateSavedQueryResponse : OrganizationResponse
    {
    }

    #endregion ValidateSavedQuery

    #region VerifyProcessStateData
    public sealed class VerifyProcessStateDataRequest : OrganizationRequest
    {
        public string ProcessState
        {
            get
            {
                if (Parameters.Contains("ProcessState"))
                    return (string)Parameters["ProcessState"];
                return default(string);
            }
            set { Parameters["ProcessState"] = value; }
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
        public VerifyProcessStateDataRequest()
        {
            this.ResponseType = new VerifyProcessStateDataResponse();
            this.RequestName = "VerifyProcessStateData";
        }
        internal override string GetRequestBody()
        {
            Parameters["ProcessState"] = ProcessState;
            Parameters["Target"] = Target;
            return GetSoapBody();
        }
    }
    public sealed class VerifyProcessStateDataResponse : OrganizationResponse
    {
        public bool IsValid { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "IsValid")
                    this.IsValid = Util.LoadFromXml<bool>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion VerifyProcessStateData

    #region WhoAmI
    public sealed class WhoAmIRequest : OrganizationRequest
    {
        public WhoAmIRequest()
        {
            base.ResponseType = new WhoAmIResponse();
            base.RequestName = "WhoAmI";
        }
        internal override string GetRequestBody()
        {
            return GetSoapBody();
        }
    }
    public sealed class WhoAmIResponse : OrganizationResponse
    {
        public Guid UserId { get; set; }
        public Guid BusinessUnitId { get; set; }
        public Guid OrganizationId { get; set; }
        internal override void StoreResult(HttpResponseMessage httpResponse)
        {
            // Convert to XDocument
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            // Obtain Values from result.
            foreach (var result in xdoc.Descendants(Util.ns.a + "Results").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                if (result.Element(Util.ns.b + "key").Value == "UserId")
                    this.UserId = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
                else if (result.Element(Util.ns.b + "key").Value == "BusinessUnitId")
                    this.BusinessUnitId = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
                else if (result.Element(Util.ns.b + "key").Value == "OrganizationId")
                    this.OrganizationId = Util.LoadFromXml<Guid>(result.Element(Util.ns.b + "value"));
            }
        }
    }

    #endregion WhoAmI

    #region WinOpportunity
    public sealed class WinOpportunityRequest : OrganizationRequest
    {
        public Entity OpportunityClose
        {
            get
            {
                if (Parameters.Contains("OpportunityClose"))
                    return (Entity)Parameters["OpportunityClose"];
                return default(Entity);
            }
            set { Parameters["OpportunityClose"] = value; }
        }
        public OptionSetValue Status
        {
            get
            {
                if (Parameters.Contains("Status"))
                    return (OptionSetValue)Parameters["Status"];
                return default(OptionSetValue);
            }
            set { Parameters["Status"] = value; }
        }
        public WinOpportunityRequest()
        {
            this.ResponseType = new WinOpportunityResponse();
            this.RequestName = "WinOpportunity";
        }
        internal override string GetRequestBody()
        {
            Parameters["OpportunityClose"] = OpportunityClose;
            Parameters["Status"] = Status;
            return GetSoapBody();
        }
    }
    public sealed class WinOpportunityResponse : OrganizationResponse
    {
    }

    #endregion WinOpportunity

    #region WinQuote
    public sealed class WinQuoteRequest : OrganizationRequest
    {
        public Entity QuoteClose
        {
            get
            {
                if (Parameters.Contains("QuoteClose"))
                    return (Entity)Parameters["QuoteClose"];
                return default(Entity);
            }
            set { Parameters["QuoteClose"] = value; }
        }
        public OptionSetValue Status
        {
            get
            {
                if (Parameters.Contains("Status"))
                    return (OptionSetValue)Parameters["Status"];
                return default(OptionSetValue);
            }
            set { Parameters["Status"] = value; }
        }
        public WinQuoteRequest()
        {
            this.ResponseType = new WinQuoteResponse();
            this.RequestName = "WinQuote";
        }
        internal override string GetRequestBody()
        {
            Parameters["QuoteClose"] = QuoteClose;
            Parameters["Status"] = Status;
            return GetSoapBody();
        }
    }
    public sealed class WinQuoteResponse : OrganizationResponse
    {
    }

    #endregion WinQuote

}

// Other types
namespace Microsoft.Crm.Sdk.Messages.Samples
{
    public enum AccessRights
    {
        None = 0,
        ReadAccess = 1,
        WriteAccess = 2,
        AppendAccess = 4,
        AppendToAccess = 8,
        CreateAccess = 16,
        DeleteAccess = 32,
        ShareAccess = 64,
        AssignAccess = 128,
        All = 255
    }
    public enum BulkOperationSource
    {
        QuickCampaign = 0,
        CampaignActivity = 1
    }
    public enum EntitySource
    {
        Account = 0,
        Contact = 1,
        Lead = 2,
        All = 3
    }
    public enum PrivilegeDepth
    {
        Basic = 0,
        Local = 1,
        Deep = 2,
        Global = 3
    }
    public enum PropagationOwnershipOptions
    {
        None = 0,
        Caller = 1,
        ListMemberOwner = 2
    }
    public enum RibbonLocationFilters
    {
        Form = 1,
        HomepageGrid = 2,
        SubGrid = 4,
        All = 7,
        Default = 7,
    }
    public enum RollupType
    {
        None = 0,
        Related = 1,
        Extended = 2
    }
    public enum SearchDirection
    {
        Forward = 0,
        Backward = 1
    }
    public enum SubCode
    {
        Unspecified = 0,
        Schedulable = 1,
        Committed = 2,
        Uncommitted = 3,
        Break = 4,
        Holiday = 5,
        Vacation = 6,
        Appointment = 7,
        ResourceStartTime = 8,
        ResourceServiceRestriction = 9,
        ResourceCapacity = 10,
        ServiceRestriction = 11,
        ServiceCost = 12
    }
    public enum TargetFieldType
    {
        All = 0,
        ValidForCreate = 1,
        ValidForUpdate = 2,
        ValidForRead = 3
    }
    public enum TimeCode
    {
        Available = 0,
        Busy = 1,
        Unavailable = 2,
        Filter = 3
    }
    public sealed class AppointmentProposal
    {
        public DateTime? End { get; set; }
        public ProposalParty[] ProposalParties { get; set; }
        public Guid SiteId { get; set; }
        public string SiteName { get; set; }
        public DateTime? Start { get; set; }
        public AppointmentProposal()
        {
            ProposalParties = new List<ProposalParty>().ToArray();
        }
        static internal AppointmentProposal LoadFromXml(XElement item)
        {
            List<ProposalParty> list = new List<ProposalParty>();
            foreach (var proposalParties in item.Elements(Util.ns.g + "ProposalParties"))
            {
                foreach (var proposalParty in proposalParties.Elements(Util.ns.g + "ProposalParty"))
                {
                    list.Add(ProposalParty.LoadFromXml(proposalParty));
                }
            }
            AppointmentProposal appointmentProposal = new AppointmentProposal()
            {
                End = Util.LoadFromXml<DateTime?>(item.Element(Util.ns.g + "End")),
                ProposalParties = list.ToArray(),
                SiteId = Util.LoadFromXml<Guid>(item.Element(Util.ns.g + "SiteId")),
                SiteName = item.Element(Util.ns.g + "SiteName").Value,
                Start = Util.LoadFromXml<DateTime?>(item.Element(Util.ns.g + "Start"))
            };
            return appointmentProposal;
        }
    }
    public sealed class AppointmentRequest
    {
        public int AnchorOffset { get; set; }
        public AppointmentsToIgnore[] AppointmentsToIgnore { get; set; }
        public ConstraintRelation[] Constraints { get; set; }
        public SearchDirection Direction { get; set; }
        public int Duration { get; set; }
        public int NumberOfResults { get; set; }
        public ObjectiveRelation[] Objectives { get; set; }
        public int RecurrenceDuration { get; set; }
        public int RecurrenceTimeZoneCode { get; set; }
        public RequiredResource[] RequiredResources { get; set; }
        public string SearchRecurrenceRule { get; set; }
        public DateTime? SearchRecurrenceStart { get; set; }
        public DateTime? SearchWindowEnd { get; set; }
        public DateTime? SearchWindowStart { get; set; }
        public Guid ServiceId { get; set; }
        public Guid[] Sites { get; set; }
        public int UserTimeZoneCode { get; set; }
        public AppointmentRequest()
        {
            AppointmentsToIgnore = new List<AppointmentsToIgnore>().ToArray();
            Constraints = new List<ConstraintRelation>().ToArray();
            Objectives = new List<ObjectiveRelation>().ToArray();
            RequiredResources = new List<RequiredResource>().ToArray();
            Sites = new List<Guid>().ToArray();
        }
        internal string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Util.ObjectToXml(AnchorOffset, "g:AnchorOffset", true));
            sb.Append(Util.ObjectToXml(AppointmentsToIgnore, "g:AppointmentsToIgnore", true));
            sb.Append(Util.ObjectToXml(Constraints, "g:Constraints", true));
            sb.Append(Util.ObjectToXml(Direction, "g:Direction", true));
            sb.Append(Util.ObjectToXml(Duration, "g:Duration", true));
            sb.Append(Util.ObjectToXml(NumberOfResults, "g:NumberOfResults", true));
            sb.Append(Util.ObjectToXml(Objectives, "g:Objectives", true));
            sb.Append(Util.ObjectToXml(RecurrenceDuration, "g:RecurrenceDuration", true));
            sb.Append(Util.ObjectToXml(RecurrenceTimeZoneCode, "g:RecurrenceTimeZoneCode", true));
            sb.Append(Util.ObjectToXml(RequiredResources, "g:RequiredResources", true));
            sb.Append(Util.ObjectToXml(SearchRecurrenceRule, "g:SearchRecurrenceRule", true));
            sb.Append(Util.ObjectToXml(SearchRecurrenceStart, "g:SearchRecurrenceStart", true));
            sb.Append(Util.ObjectToXml(SearchWindowEnd, "g:SearchWindowEnd", true));
            sb.Append(Util.ObjectToXml(SearchWindowStart, "g:SearchWindowStart", true));
            sb.Append(Util.ObjectToXml(ServiceId, "g:ServiceId", true));
            sb.Append(Util.ObjectToXml(Sites, "g:Sites", true));
            sb.Append(Util.ObjectToXml(UserTimeZoneCode, "g:UserTimeZoneCode", true));
            return sb.ToString();
        }
    }
    public sealed class AppointmentsToIgnore
    {
        public Guid[] Appointments { get; set; }
        public Guid ResourceId { get; set; }
        public AppointmentsToIgnore()
        {
            Appointments = new List<Guid>().ToArray();
        }
        internal string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Util.ObjectToXml(ResourceId, "g:ResourceId", true));
            sb.Append(Util.ObjectToXml(Appointments, "g:Appointments", true));
            return sb.ToString();
        }
    }
    public sealed class AttributeAuditDetail : AuditDetail
    {
        public Dictionary<int, string> DeletedAttributes { get; set; }
        public DataCollection<string> InvalidNewValueAttributes { get; set; }
        public Entity NewValue { get; set; }
        public Entity OldValue { get; set; }
        public AttributeAuditDetail()
        {
            DeletedAttributes = new Dictionary<int, string>();
            InvalidNewValueAttributes = new DataCollection<string>();
        }
        static internal new AttributeAuditDetail LoadFromXml(XElement item)
        {
            AttributeAuditDetail attributeAuditDetail = new AttributeAuditDetail();
            AuditDetail.LoadFromXml(item, attributeAuditDetail);
            foreach (XElement value in item.Elements(Util.ns.g + "InvalidNewValueAttributes").Elements(Util.ns.f + "string"))
            {
                attributeAuditDetail.InvalidNewValueAttributes.Add(value.Value);
            }
            foreach (var value in item.Elements(Util.ns.g + "DeletedAttributes"))
            {
                if (value.Elements().Count() == 0)
                    continue;
                attributeAuditDetail.DeletedAttributes.Add(
                    Util.LoadFromXml<int>(value.Element(Util.ns.b + "key")),
                    value.Element(Util.ns.b + "value").Value);
            }
            attributeAuditDetail.NewValue = Entity.LoadFromXml(item.Element(Util.ns.g + "NewValue"));
            attributeAuditDetail.OldValue = Entity.LoadFromXml(item.Element(Util.ns.g + "OldValue"));
            return attributeAuditDetail;
        }
    }
    public class AuditDetail
    {
        public Entity AuditRecord { get; set; }
        static internal AuditDetail LoadFromXml(XElement item)
        {
            AuditDetail auditDetail = new AuditDetail();
            string type = (item.Attribute(Util.ns.i + "type") == null) ? "AuditDetail" :
                        item.Attribute(Util.ns.i + "type").Value.Substring(2);
            switch (type)
            {
                case "AttributeAuditDetail":
                    auditDetail = AttributeAuditDetail.LoadFromXml(item);
                    break;
                case "RelationshipAuditDetail":
                    auditDetail = RelationshipAuditDetail.LoadFromXml(item);
                    break;
                case "RolePrivilegeAuditDetail":
                    auditDetail = RolePrivilegeAuditDetail.LoadFromXml(item);
                    break;
                case "ShareAuditDetail":
                    auditDetail = ShareAuditDetail.LoadFromXml(item);
                    break;
                case "UserAccessAuditDetail":
                    auditDetail = UserAccessAuditDetail.LoadFromXml(item);
                    break;
                default:
                    AuditDetail.LoadFromXml(item, auditDetail);
                    break;
            }
            return auditDetail;
        }
        static internal void LoadFromXml(XElement item, AuditDetail audit)
        {
            if (item.Elements().Count() == 0)
                return;
            audit.AuditRecord = Entity.LoadFromXml(item.Element(Util.ns.g + "AuditRecord"));
        }
    }
    public sealed class AuditDetailCollection
    {
        public DataCollection<AuditDetail> AuditDetails { get; set; }
        public AuditDetail this[int index]
        {
            get
            {
                return AuditDetails[index];
            }
            set
            {
                AuditDetails[index] = value;
            }
        }
        public int Count { get; set; }
        public bool MoreRecords { get; set; }
        public string PagingCookie { get; set; }
        public int TotalRecordCount { get; set; }
        public AuditDetailCollection()
        {
            AuditDetails = new DataCollection<AuditDetail>();
        }
        static internal AuditDetailCollection LoadFromXml(XElement item)
        {
            AuditDetailCollection auditDetailCollection = new AuditDetailCollection()
            {
                MoreRecords = Util.LoadFromXml<bool>(item.Element(Util.ns.g + "MoreRecords")),
                PagingCookie = item.Element(Util.ns.g + "PagingCookie").Value,
                TotalRecordCount = Util.LoadFromXml<int>(item.Element(Util.ns.g + "TotalRecordCount"))
            };

            foreach (var auditDetail in item.Element(Util.ns.g + "AuditDetails").Elements(Util.ns.g + "AuditDetail"))
            {
                auditDetailCollection.AuditDetails.Add(AuditDetail.LoadFromXml(auditDetail));
            }
            if (auditDetailCollection.AuditDetails.Count > 0)
                auditDetailCollection.Count = auditDetailCollection.AuditDetails.Count;
            return auditDetailCollection;
        }
    }
    public sealed class AuditPartitionDetail
    {
        public DateTime? EndDate { get; set; }
        public int PartitionNumber { get; set; }
        public long Size { get; set; }
        public DateTime? StartDate { get; set; }
        static internal AuditPartitionDetail LoadFromXml(XElement item)
        {
            AuditPartitionDetail auditPartitionDetail = new AuditPartitionDetail()
            {
                EndDate = Util.LoadFromXml<DateTime?>(item.Element(Util.ns.g + "EndDate")),
                PartitionNumber = Util.LoadFromXml<int>(item.Element(Util.ns.g + "PartitionNumber")),
                Size = Util.LoadFromXml<long>(item.Element(Util.ns.g + "Size")),
                StartDate = Util.LoadFromXml<DateTime?>(item.Element(Util.ns.g + "StartDate"))
            };
            return auditPartitionDetail;
        }
    }
    public sealed class AuditPartitionDetailCollection : DataCollection<AuditPartitionDetail>
    {
        public bool IsLogicalCollection { get; set; }
        static internal AuditPartitionDetailCollection LoadFromXml(XElement item)
        {
            // Omit IsLogicalCollection parsing as service doesn't return the result.
            AuditPartitionDetailCollection auditPartitionDetailCollection = new AuditPartitionDetailCollection()
            {
                IsLogicalCollection = false
            };
            foreach (var auditPartitionDetail in item.Elements(Util.ns.g + "AuditPartitionDetail"))
            {
                auditPartitionDetailCollection.Add(AuditPartitionDetail.LoadFromXml(auditPartitionDetail));
            }
            return auditPartitionDetailCollection;
        }
    }
    public sealed class ComponentDetail
    {
        public string DisplayName { get; set; }
        public Guid Id { get; set; }
        public string ParentDisplayName { get; set; }
        public Guid ParentId { get; set; }
        public string ParentSchemaName { get; set; }
        public string SchemaName { get; set; }
        public string Solution { get; set; }
        public int Type { get; set; }
        static internal ComponentDetail LoadFromXml(XElement item)
        {
            ComponentDetail componentDetail = new ComponentDetail()
            {
                DisplayName = item.Element(Util.ns.a + "DisplayName").Value,
                Id = Util.LoadFromXml<Guid>(item.Element(Util.ns.a + "Id")),
                ParentDisplayName = item.Element(Util.ns.a + "ParentDisplayName").Value,
                ParentId = Util.LoadFromXml<Guid>(item.Element(Util.ns.a + "ParentId")),
                ParentSchemaName = item.Element(Util.ns.a + "ParentSchemaName").Value,
                SchemaName = item.Element(Util.ns.a + "SchemaName").Value,
                Solution = item.Element(Util.ns.a + "Solution").Value,
                Type = Util.LoadFromXml<int>(item.Element(Util.ns.a + "Type"))
            };
            return componentDetail;
        }
    }
    public sealed class ConstraintRelation
    {
        public string Constraints { get; set; }
        public string ConstraintType { get; set; }
        public Guid ObjectId { get; set; }
        internal string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Util.ObjectToXml(ObjectId, "g:ObjectId", true));
            sb.Append(Util.ObjectToXml(ConstraintType, "g:ConstraintType", true));
            sb.Append(Util.ObjectToXml(Constraints, "g:Constraints", true));
            return sb.ToString();
        }
    }
    public sealed class ErrorInfo
    {
        public string ErrorCode { get; set; }
        public ResourceInfo[] ResourceList { get; set; }
        public ErrorInfo()
        {
            ResourceList = new List<ResourceInfo>().ToArray();

        }
        static internal ErrorInfo LoadFromXml(XElement item)
        {
            List<ResourceInfo> list = new List<ResourceInfo>();
            foreach (var resourceList in item.Element(Util.ns.g + "ResourceList").Elements())
            {
                list.Add(ResourceInfo.LoadFromXml(resourceList));
            }
            ErrorInfo errorInfo = new ErrorInfo()
            {
                ErrorCode = item.Element(Util.ns.g + "ErrorCode").Value,
                ResourceList = list.ToArray()
            };
            return errorInfo;
        }
    }
    public sealed class MissingComponent
    {
        public ComponentDetail DependentComponent { get; set; }
        public ComponentDetail RequiredComponent { get; set; }
        static internal MissingComponent LoadFromXml(XElement item)
        {
            MissingComponent missingComponent = new MissingComponent()
            {
                DependentComponent = ComponentDetail.LoadFromXml(item.Element(Util.ns.a + "DependentComponent")),
                RequiredComponent = ComponentDetail.LoadFromXml(item.Element(Util.ns.a + "RequiredComponent")),
            };
            return missingComponent;
        }
    }
    public sealed class ObjectiveRelation
    {
        public string ObjectiveExpression { get; set; }
        public Guid ResourceSpecId { get; set; }
        internal string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Util.ObjectToXml(ResourceSpecId, "g:ResourceSpecId", true));
            sb.Append(Util.ObjectToXml(ObjectiveExpression, "g:ObjectiveExpression", true));
            return sb.ToString();
        }
    }
    public sealed class OrganizationResources
    {
        public int CurrentNumberOfActiveUsers { get; set; }
        public int MaxNumberOfActiveUsers { get; set; }
        public int CurrentNumberOfNonInteractiveUsers { get; set; }
        public int MaxNumberOfNonInteractiveUsers { get; set; }
        public int CurrentNumberOfCustomEntities { get; set; }
        public int MaxNumberOfCustomEntities { get; set; }
        public int CurrentNumberOfPublishedWorkflows { get; set; }
        public int MaxNumberOfPublishedWorkflows { get; set; }
        public int CurrentStorage { get; set; }
        public int MaxStorage { get; set; }
        static internal OrganizationResources LoadFromXml(XElement item)
        {
            OrganizationResources OrganizationResources = new OrganizationResources()
            {
                CurrentNumberOfActiveUsers = Util.LoadFromXml<int>(item.Element(Util.ns.a + "CurrentNumberOfActiveUsers")),
                MaxNumberOfActiveUsers = Util.LoadFromXml<int>(item.Element(Util.ns.a + "MaxNumberOfActiveUsers")),
                CurrentNumberOfNonInteractiveUsers = Util.LoadFromXml<int>(item.Element(Util.ns.a + "CurrentNumberOfNonInteractiveUsers")),
                MaxNumberOfNonInteractiveUsers = Util.LoadFromXml<int>(item.Element(Util.ns.a + "MaxNumberOfNonInteractiveUsers")),
                CurrentNumberOfCustomEntities = Util.LoadFromXml<int>(item.Element(Util.ns.a + "CurrentNumberOfCustomEntities")),
                MaxNumberOfCustomEntities = Util.LoadFromXml<int>(item.Element(Util.ns.a + "MaxNumberOfCustomEntities")),
                CurrentNumberOfPublishedWorkflows = Util.LoadFromXml<int>(item.Element(Util.ns.a + "CurrentNumberOfPublishedWorkflows")),
                MaxNumberOfPublishedWorkflows = Util.LoadFromXml<int>(item.Element(Util.ns.a + "MaxNumberOfPublishedWorkflows")),
                CurrentStorage = Util.LoadFromXml<int>(item.Element(Util.ns.a + "CurrentStorage")),
                MaxStorage = Util.LoadFromXml<int>(item.Element(Util.ns.a + "MaxStorage")),
            };
            return OrganizationResources;
        }
    }
    public sealed class PrincipalAccess
    {
        public AccessRights AccessMask { get; set; }
        private EntityReference _principal;
        public EntityReference Principal
        {
            get
            {
                return _principal;
            }
            set
            {
                if (value.LogicalName != "systemuser" && value.LogicalName != "team")
                {
                    throw new Exception("Only system user or team is allowed as Principal");
                }
                _principal = value;
            }
        }
        internal string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Util.ObjectToXml(AccessMask, "g:AccessMask", true));
            sb.Append(Util.ObjectToXml(Principal, "g:Principal", true));
            return sb.ToString();
        }
        static internal PrincipalAccess LoadFromXml(XElement item)
        {
            PrincipalAccess principalAccess = new PrincipalAccess()
            {
                Principal = EntityReference.LoadFromXml(item.Element(Util.ns.g + "Principal")),
                AccessMask = Util.GetAccessRightsFromString(item.Element(Util.ns.g + "AccessMask").Value)
            };
            return principalAccess;
        }
    }
    public sealed class ProposalParty
    {
        public string DisplayName { get; set; }
        public double EffortRequired { get; set; }
        public string EntityName { get; set; }
        public Guid ResourceId { get; set; }
        public Guid ResourceSpecId { get; set; }
        static internal ProposalParty LoadFromXml(XElement item)
        {
            ProposalParty proposalParty = new ProposalParty()
            {
                DisplayName = item.Element(Util.ns.g + "DisplayName").Value,
                EffortRequired = Util.LoadFromXml<double>(item.Element(Util.ns.g + "EffortRequired")),
                EntityName = item.Element(Util.ns.g + "EntityName").Value,
                ResourceId = Util.LoadFromXml<Guid>(item.Element(Util.ns.g + "ResourceId")),
                ResourceSpecId = Util.LoadFromXml<Guid>(item.Element(Util.ns.g + "ResourceSpecId"))
            };
            return proposalParty;
        }
    }
    public sealed class RelationshipAuditDetail : AuditDetail
    {
        public string RelationshipName { get; set; }
        public DataCollection<EntityReference> TargetRecords { get; set; }
        public RelationshipAuditDetail()
        {
            TargetRecords = new DataCollection<EntityReference>();
        }
        static internal new RelationshipAuditDetail LoadFromXml(XElement item)
        {
            RelationshipAuditDetail relationshipAuditDetail = new RelationshipAuditDetail();
            AuditDetail.LoadFromXml(item, relationshipAuditDetail);
            foreach (var value in item.Elements(Util.ns.g + "TargetRecords"))
            {
                relationshipAuditDetail.TargetRecords.Add(EntityReference.LoadFromXml(value));
            }
            relationshipAuditDetail.RelationshipName = item.Element(Util.ns.g + "RelationshipName").Value;
            return relationshipAuditDetail;
        }
    }
    public sealed class RequiredResource
    {
        public Guid ResourceId { get; set; }
        public Guid ResourceSpecId { get; set; }
        internal string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Util.ObjectToXml(ResourceId, "g:ResourceId", true));
            sb.Append(Util.ObjectToXml(ResourceSpecId, "g:ResourceSpecId", true));
            return sb.ToString();
        }
    }
    public sealed class ResourceInfo
    {
        public string DisplayName { get; set; }
        public string EntityName { get; set; }
        public Guid Id { get; set; }
        static internal ResourceInfo LoadFromXml(XElement item)
        {
            ResourceInfo resourceInfo = new ResourceInfo()
            {
                Id = Util.LoadFromXml<Guid>(item.Element(Util.ns.g + "Id")),
                DisplayName = item.Element(Util.ns.g + "DisplayName").Value,
                EntityName = item.Element(Util.ns.g + "EntityName").Value
            };
            return resourceInfo;
        }
    }
    public sealed class RolePrivilege
    {
        public Guid BusinessUnitId { get; set; }
        public PrivilegeDepth Depth { get; set; }
        public Guid PrivilegeId { get; set; }
        public RolePrivilege()
        {
        }
        public RolePrivilege(int Depth, Guid PrivilegeId)
        {
            this.Depth = (PrivilegeDepth)Depth;
            this.PrivilegeId = PrivilegeId;
        }
        internal string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Util.ObjectToXml(BusinessUnitId, "g:BusinessUnitId", true));
            sb.Append(Util.ObjectToXml(Depth, "g:Depth", true));
            sb.Append(Util.ObjectToXml(PrivilegeId, "g:PrivilegeId", true));
            return sb.ToString();
        }
        static public RolePrivilege LoadFromXml(XElement item)
        {
            if (item.Elements().Count() == 0)
                return null;
            RolePrivilege rolePrivilege = new RolePrivilege()
            {
                BusinessUnitId = Util.LoadFromXml<Guid>(item.Element(Util.ns.g + "BusinessUnitId")),
                Depth = Util.LoadFromXml<PrivilegeDepth>(item.Element(Util.ns.g + "Depth")),
                PrivilegeId = Util.LoadFromXml<Guid>(item.Element(Util.ns.g + "PrivilegeId"))
            };
            return rolePrivilege;
        }
    }
    public sealed class RolePrivilegeAuditDetail : AuditDetail
    {
        public DataCollection<Guid> InvalidNewPrivileges { get; set; }
        public RolePrivilege[] NewRolePrivileges { get; set; }
        public RolePrivilege[] OldRolePrivileges { get; set; }
        public RolePrivilegeAuditDetail()
        {
            InvalidNewPrivileges = new DataCollection<Guid>();
            NewRolePrivileges = new List<RolePrivilege>().ToArray();
            OldRolePrivileges = new List<RolePrivilege>().ToArray();
        }
        static internal new RolePrivilegeAuditDetail LoadFromXml(XElement item)
        {
            RolePrivilegeAuditDetail rolePrivilegeAuditDetail = new RolePrivilegeAuditDetail();
            AuditDetail.LoadFromXml(item, rolePrivilegeAuditDetail);
            foreach (var value in item.Elements(Util.ns.g + "InvalidNewPrivileges"))
            {
                if (value.Value == "")
                    continue;
                rolePrivilegeAuditDetail.InvalidNewPrivileges.Add(Util.LoadFromXml<Guid>(value));
            }
            List<RolePrivilege> newRolePrivileges = new List<RolePrivilege>();
            foreach (var value in item.Elements(Util.ns.g + "NewRolePrivileges").Elements(Util.ns.g + "RolePrivilege"))
            {
                if (value.Elements().Count() == 0)
                    continue;
                newRolePrivileges.Add(RolePrivilege.LoadFromXml(value));
            }
            rolePrivilegeAuditDetail.NewRolePrivileges = newRolePrivileges.ToArray();
            List<RolePrivilege> oldRolePrivileges = new List<RolePrivilege>();
            foreach (var value in item.Elements(Util.ns.g + "OldRolePrivileges").Elements(Util.ns.g + "RolePrivilege"))
            {
                if (value.Elements().Count() == 0)
                    continue;
                oldRolePrivileges.Add(RolePrivilege.LoadFromXml(value));
            }
            rolePrivilegeAuditDetail.OldRolePrivileges = oldRolePrivileges.ToArray();
            return rolePrivilegeAuditDetail;
        }
    }
    public sealed class SearchResults
    {
        public AppointmentProposal[] Proposals { get; set; }
        public TraceInfo TraceInfo { get; set; }
        public SearchResults()
        {
            Proposals = new List<AppointmentProposal>().ToArray();
        }
        static internal SearchResults LoadFromXml(XElement item)
        {
            List<AppointmentProposal> list = new List<AppointmentProposal>();
            foreach (var proposals in item.Elements(Util.ns.g + "Proposals"))
            {
                foreach (var appointmentProposal in proposals.Elements(Util.ns.g + "AppointmentProposal"))
                {
                    list.Add(AppointmentProposal.LoadFromXml(appointmentProposal));
                }
            }
            SearchResults searchResults = new SearchResults()
            {
                Proposals = list.ToArray(),
                TraceInfo = TraceInfo.LoadFromXml(item.Element(Util.ns.g + "TraceInfo"))
            };
            return searchResults;
        }
    }
    public sealed class ShareAuditDetail : AuditDetail
    {
        public AccessRights NewPrivileges { get; set; }
        public AccessRights OldPrivileges { get; set; }
        public EntityReference Principal { get; set; }
        static internal new ShareAuditDetail LoadFromXml(XElement item)
        {
            ShareAuditDetail shareAuditDetail = new ShareAuditDetail();
            AuditDetail.LoadFromXml(item, shareAuditDetail);
            shareAuditDetail.NewPrivileges =
                Util.GetAccessRightsFromString(item.Element(Util.ns.g + "NewPrivileges").Value);
            shareAuditDetail.OldPrivileges =
                Util.GetAccessRightsFromString(item.Element(Util.ns.g + "OldPrivileges").Value);
            shareAuditDetail.Principal = EntityReference.LoadFromXml(item.Element(Util.ns.g + "Principal"));
            return shareAuditDetail;
        }
    }
    public sealed class TimeInfo
    {
        public int ActivityStatusCode { get; set; }
        public Guid CalendarId { get; set; }
        public string DisplayText { get; set; }
        public double Effort { get; set; }
        public DateTime? End { get; set; }
        public bool IsActivity { get; set; }
        public Guid SourceId { get; set; }
        public int SourceTypeCode { get; set; }
        public DateTime? Start { get; set; }
        public SubCode SubCode { get; set; }
        public TimeCode TimeCode { get; set; }
        static internal TimeInfo LoadFromXml(XElement item)
        {
            TimeInfo timeInfo = new TimeInfo()
            {
                ActivityStatusCode = Util.LoadFromXml<int>(item.Element(Util.ns.g + "ActivityStatusCode")),
                CalendarId = Util.LoadFromXml<Guid>(item.Element(Util.ns.g + "CalendarId")),
                DisplayText = item.Element(Util.ns.g + "DisplayText").Value,
                Effort = Util.LoadFromXml<double>(item.Element(Util.ns.g + "Effort")),
                End = Util.LoadFromXml<DateTime>(item.Element(Util.ns.g + "End")),
                IsActivity = Util.LoadFromXml<bool>(item.Element(Util.ns.g + "IsActivity")),
                SourceId = Util.LoadFromXml<Guid>(item.Element(Util.ns.g + "SourceId")),
                SourceTypeCode = Util.LoadFromXml<int>(item.Element(Util.ns.g + "SourceTypeCode")),
                Start = Util.LoadFromXml<DateTime>(item.Element(Util.ns.g + "Start")),
                SubCode = Util.LoadFromXml<SubCode>(item.Element(Util.ns.g + "SubCode")),
                TimeCode = Util.LoadFromXml<TimeCode>(item.Element(Util.ns.g + "TimeCode")),
            };
            return timeInfo;
        }
    }
    public sealed class TraceInfo
    {
        public ErrorInfo[] ErrorInfoList { get; set; }
        public TraceInfo()
        {
            ErrorInfoList = new List<ErrorInfo>().ToArray();
        }
        static internal TraceInfo LoadFromXml(XElement item)
        {
            TraceInfo traceInfo = new TraceInfo();
            List<ErrorInfo> list = new List<ErrorInfo>();
            foreach (var errorInfo in item.Element(Util.ns.g + "ErrorInfoList").Elements())
            {
                list.Add(ErrorInfo.LoadFromXml(errorInfo));
            }

            traceInfo.ErrorInfoList = list.ToArray();
            return traceInfo;
        }
    }
    public sealed class UserAccessAuditDetail : AuditDetail
    {
        public DateTime AccessTime { get; set; }
        public int Interval { get; set; }
        static internal new UserAccessAuditDetail LoadFromXml(XElement item)
        {
            UserAccessAuditDetail userAccessAuditDetail = new UserAccessAuditDetail();
            AuditDetail.LoadFromXml(item, userAccessAuditDetail);
            userAccessAuditDetail.AccessTime = Util.LoadFromXml<DateTime>(item.Element(Util.ns.h + "AccessTime"));
            userAccessAuditDetail.Interval = Util.LoadFromXml<int>(item.Element(Util.ns.h + "Interval"));
            return userAccessAuditDetail;
        }
    }
    public sealed class ValidationResult
    {
        public Guid ActivityId { get; set; }
        public TraceInfo TraceInfo { get; set; }
        public bool ValidationSuccess { get; set; }
        static internal ValidationResult LoadFromXml(XElement item)
        {
            ValidationResult validationResult = new ValidationResult()
            {
                ActivityId = Util.LoadFromXml<Guid>(item.Element(Util.ns.g + "ActivityId")),
                TraceInfo = TraceInfo.LoadFromXml(item.Element(Util.ns.g + "TraceInfo")),
                ValidationSuccess = Util.LoadFromXml<bool>(item.Element(Util.ns.g + "ValidationSuccess"))
            };
            return validationResult;
        }
    }
}

