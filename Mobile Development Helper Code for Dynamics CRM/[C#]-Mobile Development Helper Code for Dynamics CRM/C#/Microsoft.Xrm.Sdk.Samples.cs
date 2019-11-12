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

using Microsoft.Xrm.Sdk.Metadata.Samples;
using Microsoft.Xrm.Sdk.Query.Samples;
using Microsoft.Xrm.Sdk.Utility.Samples;
using Newtonsoft.Json;      // Used in the REST methods
using Newtonsoft.Json.Linq; // Used in the REST methods
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.ApplicationModel;

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

namespace Microsoft.Xrm.Sdk.Samples
{
    public class OrganizationDataWebServiceProxy
    {
        #region class members

        private const string webEndpoint = "/XRMServices/2011/Organization.svc/web";
        private const string restEndpoint = "/XRMServices/2011/OrganizationData.svc/";
        // Modify the version depending on your needs.
        private const string sdkClientVersion = "7.1.0.1085";
        public string ServiceUrl;
        public string AccessToken; // can be private, but not sure if user want to access it.
        public Guid CallerId;
        public int Timeout;

        static private IEnumerable<TypeInfo> types;

        #endregion class members

        #region Soap Methods

        // Provide same methods as IOrganizationService with same parameter and types
        // so that developer can use this class without confusion.

        /// <summary>
        /// Creates a link between records.
        /// </summary>
        /// <param name="entityName">The logical name of the entity that is specified in the entityId parameter.</param>
        /// <param name="entityId">The ID of the record to which the related records are associated.</param>
        /// <param name="relationship">The name of the relationship to be used to create the link.</param>
        /// <param name="relatedEntities">A collection of entity references (references to records) to be associated.</param>
        public async Task Associate(string entityName, Guid entityId, Relationship relationship,
            EntityReferenceCollection relatedEntities)
        {
            // Create HttpClient with Compression enabled.
            using (HttpClient httpClient = new HttpClient(new HttpClientHandler() { AutomaticDecompression = System.Net.DecompressionMethods.GZip }))
            {
                string SOAPAction = "http://schemas.microsoft.com/xrm/2011/Contracts/Services/IOrganizationService/Associate";

                StringBuilder content = new StringBuilder();
                content.Append(GetEnvelopeHeader());
                content.Append("<s:Body>");
                content.Append("<d:Associate>");
                content.Append("<d:entityName>" + entityName + "</d:entityName>");
                content.Append("<d:entityId>" + entityId.ToString() + "</d:entityId>");
                content.Append(Util.ObjectToXml(relationship, "d:relationship", true));
                content.Append(Util.ObjectToXml(relatedEntities, "d:relatedEntities", true));
                content.Append("</d:Associate>");
                content.Append("</s:Body>");
                content.Append("</s:Envelope>");

                // Send the request asychronously and wait for the response.
                HttpResponseMessage httpResponse;
                httpResponse = await SendRequest(httpClient, SOAPAction, content.ToString());

                if (httpResponse.IsSuccessStatusCode)
                {
                    //do nothing
                }
                else
                {
                    OrganizationServiceFault fault = RestoreError(httpResponse);
                    if (!String.IsNullOrEmpty(fault.Message))
                        throw fault;
                    else
                        throw new Exception(httpResponse.Content.ReadAsStringAsync().Result);
                }
            }
        }
        /// <summary>
        /// Creates a record.
        /// </summary>
        /// <param name="entity">An entity instance that contains the properties to set in the newly created record.</param>
        /// <returns>The ID of the newly created record.</returns>
        public async Task<Guid> Create(Entity entity)
        {
            // Create HttpClient with Compression enabled.
            using (HttpClient httpClient = new HttpClient(new HttpClientHandler() { AutomaticDecompression = System.Net.DecompressionMethods.GZip }))
            {
                string SOAPAction = "http://schemas.microsoft.com/xrm/2011/Contracts/Services/IOrganizationService/Create";

                StringBuilder content = new StringBuilder();
                content.Append(GetEnvelopeHeader());
                content.Append("<s:Body>");
                content.Append("<d:Create>");
                content.Append(Util.ObjectToXml(entity, "d:entity", true));
                content.Append("</d:Create>");
                content.Append("</s:Body>");
                content.Append("</s:Envelope>");

                // Send the request asychronously and wait for the response.
                HttpResponseMessage httpResponse;
                httpResponse = await SendRequest(httpClient, SOAPAction, content.ToString());

                Guid createdRecordId = Guid.Empty;
                if (httpResponse.IsSuccessStatusCode)
                {
                    // Obtain Guid values from result.
                    XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
                    foreach (var result in xdoc.Descendants(Util.ns.d + "CreateResponse"))
                    {
                        createdRecordId = Util.LoadFromXml<Guid>(result);
                    }
                }
                else
                {
                    OrganizationServiceFault fault = RestoreError(httpResponse);
                    if (!String.IsNullOrEmpty(fault.Message))
                        throw fault;
                    else
                        throw new Exception(httpResponse.Content.ReadAsStringAsync().Result);
                }

                return createdRecordId;
            }
        }
        /// <summary>
        /// Deletes a record.
        /// </summary>
        /// <param name="entityName">The logical name of the entity that is specified in the entityId parameter.</param>
        /// <param name="id">The ID of the record that you want to delete.</param>
        public async Task Delete(string entityName, Guid id)
        {
            // Create HttpClient with Compression enabled.
            using (HttpClient httpClient = new HttpClient(new HttpClientHandler() { AutomaticDecompression = System.Net.DecompressionMethods.GZip }))
            {
                string SOAPAction = "http://schemas.microsoft.com/xrm/2011/Contracts/Services/IOrganizationService/Delete";

                StringBuilder content = new StringBuilder();
                content.Append(GetEnvelopeHeader());
                content.Append("<s:Body>");
                content.Append("<d:Delete>");
                content.Append("<d:entityName>" + entityName + "</d:entityName>");
                content.Append("<d:id>" + id.ToString() + "</d:id>");
                content.Append("</d:Delete>");
                content.Append("</s:Body>");
                content.Append("</s:Envelope>");

                // Send the request asychronously and wait for the response.
                HttpResponseMessage httpResponse;
                httpResponse = await SendRequest(httpClient, SOAPAction, content.ToString());

                if (httpResponse.IsSuccessStatusCode)
                {
                    // Do nothing as it returns void.
                }
                else
                {
                    OrganizationServiceFault fault = RestoreError(httpResponse);
                    if (!String.IsNullOrEmpty(fault.Message))
                        throw fault;
                    else
                        throw new Exception(httpResponse.Content.ReadAsStringAsync().Result);
                }
            }
        }
        /// <summary>
        /// Deletes a link between records.
        /// </summary>
        /// <param name="entityName">The logical name of the entity that is specified in the entityId parameter.</param>
        /// <param name="entityId">The ID of the record from which the related records are disassociated.</param>
        /// <param name="relationship">The name of the relationship to be used to remove the link.</param>
        /// <param name="relatedEntities">A collection of entity references (references to records) to be disassociated.</param>
        public async Task Disassociate(string entityName, Guid entityId, Relationship relationship,
            EntityReferenceCollection relatedEntities)
        {
            // Create HttpClient with Compression enabled.
            using (HttpClient httpClient = new HttpClient(new HttpClientHandler() { AutomaticDecompression = System.Net.DecompressionMethods.GZip }))
            {
                string SOAPAction = "http://schemas.microsoft.com/xrm/2011/Contracts/Services/IOrganizationService/Disassociate";

                StringBuilder content = new StringBuilder();
                content.Append(GetEnvelopeHeader());
                content.Append("<s:Body>");
                content.Append("<d:Disassociate>");
                content.Append("<d:entityName>" + entityName + "</d:entityName>");
                content.Append("<d:entityId>" + entityId.ToString() + "</d:entityId>");
                content.Append(Util.ObjectToXml(relationship, "d:relationship", true));
                content.Append(Util.ObjectToXml(relatedEntities, "d:relatedEntities", true));
                content.Append("</d:Disassociate>");
                content.Append("</s:Body>");
                content.Append("</s:Envelope>");

                // Send the request asychronously and wait for the response.
                HttpResponseMessage httpResponse;
                httpResponse = await SendRequest(httpClient, SOAPAction, content.ToString());

                if (httpResponse.IsSuccessStatusCode)
                {
                    //do nothing
                }
                else
                {
                    OrganizationServiceFault fault = RestoreError(httpResponse);
                    if (!String.IsNullOrEmpty(fault.Message))
                        throw fault;
                    else
                        throw new Exception(httpResponse.Content.ReadAsStringAsync().Result);
                }


            }
        }
        /// <summary>
        /// Executes a message in the form of a request, and returns a response.
        /// </summary>
        /// <param name="request">A request instance that defines the action to be performed.</param>
        /// <returns>The response from the request. You must cast the return value of this method to 
        /// the specific instance of the response that corresponds to the Request parameter.</returns>
        public async Task<OrganizationResponse> Execute(OrganizationRequest request)
        {
            // Create HttpClient with Compression enabled.
            using (HttpClient httpClient = new HttpClient(new HttpClientHandler() { AutomaticDecompression = System.Net.DecompressionMethods.GZip }))
            {
                string SOAPAction = "http://schemas.microsoft.com/xrm/2011/Contracts/Services/IOrganizationService/Execute";

                StringBuilder content = new StringBuilder();
                content.Append(GetEnvelopeHeader());
                content.Append("<s:Body>");
                content.Append("<d:Execute>");
                content.Append(request.GetRequestBody());
                content.Append("</d:Execute>");
                content.Append("</s:Body>");
                content.Append("</s:Envelope>");

                // Send the request asychronously and wait for the response.
                HttpResponseMessage httpResponse;
                httpResponse = await SendRequest(httpClient, SOAPAction, content.ToString());

                if (httpResponse.IsSuccessStatusCode)
                {
                    // 1. Request contains instance of its corresponding Response.
                    // 2. Response has override StoreResult method to extract data
                    // from result if necessary.
                    request.ResponseType.StoreResult(httpResponse);
                    return request.ResponseType;
                }
                else
                {
                    OrganizationServiceFault fault = RestoreError(httpResponse);
                    if (!String.IsNullOrEmpty(fault.Message))
                        throw fault;
                    else
                        throw new Exception(httpResponse.Content.ReadAsStringAsync().Result);
                }
            }
        }
        /// <summary>
        /// Retrieves a record.
        /// </summary>
        /// <param name="entityName">The logical name of the entity that is specified in the entityId parameter.</param>
        /// <param name="id">The ID of the record that you want to retrieve.</param>
        /// <param name="columnSet">A query that specifies the set of columns, or attributes, to retrieve.</param>
        /// <returns>The requested entity. If EnableProxyTypes called, returns early bound type.</returns>
        public async Task<Entity> Retrieve(string entityName, Guid id, ColumnSet columnSet)
        {
            // Create HttpClient with Compression enabled.
            using (HttpClient httpClient = new HttpClient(new HttpClientHandler() { AutomaticDecompression = System.Net.DecompressionMethods.GZip }))
            {
                string SOAPAction = "http://schemas.microsoft.com/xrm/2011/Contracts/Services/IOrganizationService/Retrieve";

                StringBuilder content = new StringBuilder();
                content.Append(GetEnvelopeHeader());
                content.Append("<s:Body>");
                content.Append("<d:Retrieve>");
                content.Append("<d:entityName>" + entityName + "</d:entityName>");
                content.Append("<d:id>" + id.ToString() + "</d:id>");
                content.Append(Util.ObjectToXml(columnSet, "d:columnSet", true));
                content.Append("</d:Retrieve>");
                content.Append("</s:Body>");
                content.Append("</s:Envelope>");

                // Send the request asychronously and wait for the response.
                HttpResponseMessage httpResponse;
                httpResponse = await SendRequest(httpClient, SOAPAction, content.ToString());

                Entity Entity = new Entity();

                // Chech the returned code
                if (httpResponse.IsSuccessStatusCode)
                {
                    // Extract Entity from result.
                    XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
                    foreach (var result in xdoc.Descendants(Util.ns.d + "RetrieveResult"))
                    {
                        Entity = Util.LoadFromXml<Entity>(result);
                    }
                }
                else
                {
                    OrganizationServiceFault fault = RestoreError(httpResponse);
                    if (!String.IsNullOrEmpty(fault.Message))
                        throw fault;
                    else
                        throw new Exception(httpResponse.Content.ReadAsStringAsync().Result);
                }
                
                // If Entity if not casted yet, then try to cast to early-bound
                if(Entity.GetType().Equals(typeof(Entity)))
                    Entity = ConvertToEarlyBound(Entity);

                return Entity;
            }
        }
        /// <summary>
        /// Retrieves a collection of records.
        /// </summary>
        /// <param name="query">A query that determines the set of records to retrieve.</param>
        /// <returns>The collection of entities returned from the query. If EnableProxyTypes called, returns early bound type.</returns>
        public async Task<EntityCollection> RetrieveMultiple(QueryBase query)
        {
            // Create HttpClient with Compression enabled.
            using (HttpClient httpClient = new HttpClient(new HttpClientHandler() { AutomaticDecompression = System.Net.DecompressionMethods.GZip }))
            {
                string SOAPAction = "http://schemas.microsoft.com/xrm/2011/Contracts/Services/IOrganizationService/RetrieveMultiple";

                StringBuilder content = new StringBuilder();
                content.Append(GetEnvelopeHeader());
                content.Append("<s:Body>");
                content.Append("<d:RetrieveMultiple>");
                content.Append(Util.ObjectToXml(query, "d:query"));
                content.Append("</d:RetrieveMultiple>");
                content.Append("</s:Body>");
                content.Append("</s:Envelope>");

                // Send the request asychronously and wait for the response.
                HttpResponseMessage httpResponse;
                httpResponse = await SendRequest(httpClient, SOAPAction, content.ToString());

                EntityCollection entityCollection = null;

                // Check the returned code
                if (httpResponse.IsSuccessStatusCode)
                {
                    // Extract EntityCollection from result.
                    XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
                    foreach (var results in xdoc.Descendants(Util.ns.d + "RetrieveMultipleResult"))
                    {
                        entityCollection = EntityCollection.LoadFromXml(results);
                    }
                }
                else
                {
                    OrganizationServiceFault fault = RestoreError(httpResponse);
                    if (!String.IsNullOrEmpty(fault.Message))
                        throw fault;
                    else
                        throw new Exception(httpResponse.Content.ReadAsStringAsync().Result);
                }

                return entityCollection;
            }
        }
        /// <summary>
        /// Updates an existing record.
        /// </summary>
        /// <param name="entity">An entity instance that has one or more properties set to be updated in the record.</param>
        public async Task Update(Entity entity)
        {
            // Create HttpClient with Compression enabled.
            using (HttpClient httpClient = new HttpClient(new HttpClientHandler() { AutomaticDecompression = System.Net.DecompressionMethods.GZip }))
            {
                string SOAPAction = "http://schemas.microsoft.com/xrm/2011/Contracts/Services/IOrganizationService/Update";

                StringBuilder content = new StringBuilder();
                content.Append(GetEnvelopeHeader());
                content.Append("<s:Body>");
                content.Append("<d:Update>");
                content.Append(Util.ObjectToXml(entity, "d:entity", true));
                content.Append("</d:Update>");
                content.Append("</s:Body>");
                content.Append("</s:Envelope>");

                // Send the request asychronously and wait for the response.
                HttpResponseMessage httpResponse;
                httpResponse = await SendRequest(httpClient, SOAPAction, content.ToString());

                if (httpResponse.IsSuccessStatusCode)
                {
                    // Do nothing as it returns void.
                }
                else
                {
                    OrganizationServiceFault fault = RestoreError(httpResponse);
                    if (!String.IsNullOrEmpty(fault.Message))
                        throw fault;
                    else
                        throw new Exception(httpResponse.Content.ReadAsStringAsync().Result);
                }
            }
        }

        #endregion methods

        #region Rest Methods

        /// <summary>
        /// create record
        /// </summary>
        /// <param name="entity">record to create</param>
        /// <returns></returns>
        public async Task<Guid> RestCreate(Entity entity)
        {
            // Create HttpClient with Compression enabled.
            using (HttpClient httpClient = new HttpClient(new HttpClientHandler() { AutomaticDecompression = System.Net.DecompressionMethods.GZip }))
            {
                DataContractJsonSerializer jasonSerializer = new DataContractJsonSerializer(entity.GetType());
                string json;
                using (MemoryStream ms = new MemoryStream())
                {
                    jasonSerializer.WriteObject(ms, entity);
                    json = Encoding.UTF8.GetString(ms.ToArray(), 0, (int)ms.Length);
                }
                StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                string ODataAction = entity.GetType().GetRuntimeField("SchemaName").ToString() + "Set";

                // Build and send the HTTP request.
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Use PostAsync to Post data.
                HttpResponseMessage response = await httpClient.PostAsync(ServiceUrl + restEndpoint + ODataAction, content);

                // Check the response result.
                if (response.IsSuccessStatusCode)
                {
                    Entity result;
                    // Deserialize response to JToken 
                    byte[] resultbytes = Encoding.UTF8.GetBytes(response.Content.ReadAsStringAsync().Result);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        result = (Entity)jasonSerializer.ReadObject(ms);
                    }
                    return result.Id;
                }
                else
                    throw new Exception("REST Create failed.");
            }
        }

        /// <summary>
        /// Deletes a record.
        /// </summary>
        /// <param name="schemaName">The Schema name of the entity that is specified in the entityId parameter.</param>
        /// <param name="id">The ID of the record that you want to delete.</param>
        public async Task RestDelete(string schemaName, Guid id)
        {
            // Create HttpClient with Compression enabled.
            using (HttpClient httpClient = new HttpClient(new HttpClientHandler() { AutomaticDecompression = System.Net.DecompressionMethods.GZip }))
            {
                string ODataAction = schemaName + "Set(guid'" + id + "')";

                // Build and send the HTTP request.
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Use DeleteAsync to Post data.
                HttpResponseMessage response = await httpClient.DeleteAsync(ServiceUrl + restEndpoint + ODataAction);

                if (!response.IsSuccessStatusCode)
                    throw new Exception("REST Delete failed.");
            }
        }

        /// <summary>
        /// Retrieve a record
        /// </summary>
        /// <param name="schemaName">Entity SchemaName.</param>
        /// <param name="id">id of record to be retireved</param>
        /// <param name="columnSet">retrieved columns</param>
        /// <returns></returns>
        public async Task<Entity> RestRetrieve(string schemaName, Guid id, ColumnSet columnSet)
        {
            // Create HttpClient with Compression enabled.
            using (HttpClient httpClient = new HttpClient(new HttpClientHandler() { AutomaticDecompression = System.Net.DecompressionMethods.GZip }))
            {
                StringBuilder select = new StringBuilder();
                foreach (string column in columnSet.Columns)
                {
                    select.Append("," + column);
                }

                // The URL for the OData organization web service.
                string ODataAction = schemaName + "Set(guid'" + id + "')?$select=" + select.ToString().Remove(0, 1) + "";

                // Build and send the HTTP request.            
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Wait for the web service response.
                HttpResponseMessage response = await httpClient.GetAsync(ServiceUrl + restEndpoint + ODataAction);

                // Check the response result.
                if (response.IsSuccessStatusCode)
                {
                    // Check type.
                    TypeInfo currentType;
                    if (types == null)
                        throw new Exception("Early-bound types must be enabled for a REST Retrieve.");
                    else
                    {
                        currentType = types.Where(x => x.Name.ToLower() == schemaName.ToLower()).FirstOrDefault();
                        if (currentType == null)
                            throw new Exception("Early-bound types must be enabled for a REST Retrieve.");
                    }
                    // Deserialize response to JToken 
                    JToken jtoken = JObject.Parse(response.Content.ReadAsStringAsync().Result)["d"];
                    return (Entity)JsonConvert.DeserializeObject(jtoken.ToString(), currentType.AsType());
                }
                else
                    throw new Exception("REST Retrieve failed.");
            }
        }

        /// <summary>
        /// Need to consider how to implement this. Let developer create URL or implement convert method form QueryBase to URL?
        /// For now, just let user pass schemaName and ColumnSet.
        /// </summary>
        /// <param name="schemaName"></param>
        /// <param name="columnSet"></param>
        /// <returns></returns>
        public async Task<EntityCollection> RestRetrieveMultiple(string schemaName, ColumnSet columnSet)
        {
            // Create HttpClient with Compression enabled.
            using (HttpClient httpClient = new HttpClient(new HttpClientHandler() { AutomaticDecompression = System.Net.DecompressionMethods.GZip }))
            {
                StringBuilder select = new StringBuilder();
                foreach (string column in columnSet.Columns)
                {
                    select.Append("," + column);
                }

                // The URL for the OData organization web service.
                string ODataAction = schemaName + "Set?$select=" + select.ToString().Remove(0, 1) + "";

                // Build and send the HTTP request.
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Wait for the web service response.
                HttpResponseMessage response = await httpClient.GetAsync(ServiceUrl + restEndpoint + ODataAction);

                // Check the response result.
                if (response.IsSuccessStatusCode)
                {
                    EntityCollection results = new EntityCollection();
                    results.EntityName = schemaName.ToLower();

                    // Check type.
                    TypeInfo currentType;
                    if (types == null)
                        throw new Exception("Early-bound types must be enabled for a REST RetrieveMultiple.");
                    else
                    {
                        currentType = types.Where(x => x.Name.ToLower() == schemaName.ToLower()).FirstOrDefault();
                        if (currentType == null)
                            throw new Exception("Early-bound types must be enabled for a REST RetrieveMultiple.");
                    }

                    // Deserialize response to JToken IList
                    IList<JToken> jTokens = JObject.Parse(response.Content.ReadAsStringAsync().Result)["d"]["results"].Children().ToList();
                    foreach (JToken jToken in jTokens)
                    {
                        // Deserialize result to Type T
                        results.Entities.Add((Entity)JsonConvert.DeserializeObject(jToken.ToString(), currentType.AsType()));
                    }
                    results.TotalRecordCount = jTokens.Count();
                    return results;
                }
                else
                    throw new Exception("REST RetrieveMultiple failed.");
            }
        }

        /// <summary>
        /// Update a record
        /// </summary>
        /// <param name="entity">record to update</param>
        /// <returns></returns>
        public async Task RestUpdate(Entity entity)
        {
            // Create HttpClient with Compression enabled.
            using (HttpClient httpClient = new HttpClient(new HttpClientHandler() { AutomaticDecompression = System.Net.DecompressionMethods.GZip }))
            {
                DataContractJsonSerializer jasonSerializer = new DataContractJsonSerializer(entity.GetType());
                MemoryStream ms = new MemoryStream();
                jasonSerializer.WriteObject(ms, entity);
                string json = Encoding.UTF8.GetString(ms.ToArray(), 0, (int)ms.Length);
                StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                // The URL for the OData organization web service.
                string ODataAction = entity.GetType().GetRuntimeField("SchemaName").ToString() + "Set(guid'" + entity.Id + "')";

                // Build and send the HTTP request.
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
                // Specify MERGE to update record
                httpClient.DefaultRequestHeaders.Add("X-HTTP-Method", "MERGE");

                // Use PostAsync to Post data.
                HttpResponseMessage response = await httpClient.PostAsync(ServiceUrl + restEndpoint + ODataAction, content);

                // Check the response result.
                if (!response.IsSuccessStatusCode)
                    throw new Exception("REST Update failed.");
            }
        }
        #endregion

        #region helpercode

        // To make this project Xamarin compatible, you need to comment out this method.
        public async Task EnableProxyTypes()
        {
            List<TypeInfo> typeList = new List<TypeInfo>();
            // Obtain folder of executing application.
            var folder = Package.Current.InstalledLocation;
            foreach (var file in await folder.GetFilesAsync())
            {
                // not only .dll but .exe also contains types.
                if (file.FileType == ".dll" || file.FileType == ".exe")
                {
                    var assemblyName = new AssemblyName(file.DisplayName);
                    var assembly = Assembly.Load(assemblyName);
                    foreach (TypeInfo type in assembly.DefinedTypes)
                    {
                        // Store only CRM Entities.
                        if (type.BaseType == typeof(Entity))
                            typeList.Add(type);
                    }
                }
            }
            types = typeList.ToArray();
        }

        /// <summary>
        /// Create HTTPRequest and returns the HTTPRequestMessage.
        /// </summary>
        /// <param name="httpClient">httpClient instance.</param>
        /// <param name="SOAPAction">Action for the endpoint, like Execute, Retrieve, etc.</param>
        /// <param name="content">Request Body</param>
        /// <returns>HTTPRequest</returns>
        private async Task<HttpResponseMessage> SendRequest(HttpClient httpClient, string SOAPAction, string content)
        {
            // Set the HTTP authorization header using the access token.
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));           
            
            if (Timeout > 0)
                httpClient.Timeout = new TimeSpan(0, 0, 0, Timeout, 0);
            // Finish setting up the HTTP request.
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, ServiceUrl + webEndpoint);
            req.Headers.Add("SOAPAction", SOAPAction);
            req.Method = HttpMethod.Post;
            req.Content = new StringContent(content);
            req.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("text/xml; charset=utf-8");

            // Send the request asychronously and wait for the response.            
            return await httpClient.SendAsync(req);
        }

        /// <summary>
        /// Create Envelope for SOAP request.
        /// </summary>
        /// <returns>SOAP Envelope with namespaces.</returns>
        /// <summary>
        /// Enable Early Bound by storing all Types in the application.
        /// </summary>
        /// <returns>SOAP Envelope with namespaces.</returns>
        ///       
        private string GetEnvelopeHeader()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<s:Envelope xmlns:s='http://schemas.xmlsoap.org/soap/envelope/' xmlns:a='http://schemas.microsoft.com/xrm/2011/Contracts' xmlns:i='http://www.w3.org/2001/XMLSchema-instance' xmlns:b='http://schemas.datacontract.org/2004/07/System.Collections.Generic' xmlns:c='http://www.w3.org/2001/XMLSchema' xmlns:d='http://schemas.microsoft.com/xrm/2011/Contracts/Services' xmlns:e='http://schemas.microsoft.com/2003/10/Serialization/' xmlns:f='http://schemas.microsoft.com/2003/10/Serialization/Arrays' xmlns:g='http://schemas.microsoft.com/crm/2011/Contracts' xmlns:h='http://schemas.microsoft.com/xrm/2011/Metadata' xmlns:j='http://schemas.microsoft.com/xrm/2011/Metadata/Query' xmlns:k='http://schemas.microsoft.com/xrm/2013/Metadata' xmlns:l='http://schemas.microsoft.com/xrm/2012/Contracts' xmlns:m='http://schemas.microsoft.com/xrm/2014/Contracts' xmlns:n='http://schemas.microsoft.com/xrm/7.1/Metadata' xmlns:o='http://schemas.microsoft.com/xrm/7.1/Contracts'>");
            sb.Append("<s:Header>");
            if (this.CallerId != null && this.CallerId != Guid.Empty)
                sb.Append("<a:CallerId>" + this.CallerId.ToString() + "</a:CallerId>");
            sb.Append("<a:SdkClientVersion>" + sdkClientVersion + "</a:SdkClientVersion>");
            sb.Append("</s:Header>");
            return sb.ToString();
        }
        private OrganizationServiceFault RestoreError(HttpResponseMessage httpResponse)
        {
            XDocument xdoc = XDocument.Parse(httpResponse.Content.ReadAsStringAsync().Result, LoadOptions.None);
            return OrganizationServiceFault.LoadFromXml(xdoc.Descendants(Util.ns.a + "OrganizationServiceFault").First());
        }
        static internal Entity ConvertToEarlyBound(Entity entity)
        {
            if (types == null)
                return entity;
            TypeInfo currentType = types.Where(x => x.Name.ToLower() == entity.LogicalName).FirstOrDefault();
            if (currentType == null)
                return entity;
            else
                // Then convert it by using Entity.ToEntity<T> method.
                return (Entity)typeof(Entity).GetRuntimeMethod("ToEntity", new Type[] { }).
                    MakeGenericMethod(currentType.AsType()).Invoke(entity, null);
        }

        #endregion helpercode
    }
    public enum ChangeType
    {
        NewOrUpdated = 0,
        RemoveOrDeleted=1,
    }
    public enum ConcurrencyBehavior
    {
        Default = 0,
        IfRowVersionMatches = 1,
        AlwaysOverwrite = 2,
    }
    public enum EntityKeyIndexStatus
    {
        Pending = 0,
        Changed = 2,
        InProgress = 1,
        Failed = 3
    }
    public enum EntityRole
    {
        Referenced = 1,
        Referencing = 0
    }
    public enum EntityState
    {
        Unchanged = 0,
        Created = 1,
        Changed = 2
    }
    public enum OperationStatus
    {
        Failed,
        Canceled,
        Retry,
        Suspended,
        Succeeded
    }
    public interface IChangedItem
    {
        ChangeType Type { get; set; }
    }
    public sealed class AliasedValue
    {
        public string AttributeLogicalName { get; set; }
        public string EntityLogicalName { get; set; }
        public Object Value { get; set; }
        static public AliasedValue LoadFromXml(XElement item)
        {
            AliasedValue aliasedValue = new AliasedValue()
            {
                AttributeLogicalName = Util.LoadFromXml<string>(item.Element(Util.ns.a + "AttributeLogicalName")),
                EntityLogicalName = Util.LoadFromXml<string>(item.Element(Util.ns.a + "EntityLogicalName")),
                Value = Util.ObjectFromXml(item.Element(Util.ns.a + "Value"))
            };
            return aliasedValue;
        }
    }
    public sealed class AttributeCollection : DataCollection<string, object>
    {
        internal string ToXml()
        {
            StringBuilder sb = new StringBuilder();

            if (this.Count == 0)
            {
                return sb.Append("<a:Attributes/>").ToString();
            }
            sb.Append("<a:Attributes>");
            foreach (var item in this)
            {
                sb.Append(AttributeToXml(item));
            }
            sb.Append("</a:Attributes>");
            return sb.ToString();
        }
        internal string AttributeToXml(KeyValuePair<string, object> item)
        {
            return "<a:KeyValuePairOfstringanyType>"
                + Util.ObjectToXml(item.Key, "b:key", true)
                + Util.ObjectToXml(item.Value, "b:value")
                + "</a:KeyValuePairOfstringanyType>";
        }
        internal void LoadFromXml(XElement item)
        {
            foreach (var att in item.Elements(Util.ns.a + "Attributes").Elements(Util.ns.a + "KeyValuePairOfstringanyType"))
            {
                AttributeLoadFromXml(att);
            }
        }
        internal void AttributeLoadFromXml(XElement item)
        {
            string key = Util.LoadFromXml<string>(item.Element(Util.ns.b + "key"));
            object value = Util.ObjectFromXml(item.Element(Util.ns.b + "value"));
            this.Add(key, value);
        }
    }
    public sealed class AttributeLogicalNameAttribute : Attribute
    {
        public string LogicalName { get; set; }

        public AttributeLogicalNameAttribute(string logicalName)
        {
            if (String.IsNullOrEmpty(logicalName))
                throw new ArgumentNullException("logicalName");
            LogicalName = logicalName;
        }
    }
    public sealed class AttributeMapping
    {
        public int AllowedSyncDirection { get; set; }
        public string AttributeCrmDisplayName { get; set; }
        public string AttributeCrmName { get; set; }
        public string AttributeExchangeDisplayName { get; set; }
        public string AttributeExchangeName { get; set; }
        public Guid AttributeMappingId { get; set; }
        public DataCollection<string> ComputedProperties { get; set; }
        public int DefaultSyncDirection { get; set; }
        public int EntityTypeCode { get; set; }
        public bool IsComputed { get; set; }
        public string MappingName { get; set; }
        public int SyncDirection { get; set; }
        public AttributeMapping() 
        {
            ComputedProperties = new DataCollection<string>();
        }
        public AttributeMapping(Guid attributeMappingId, string mappingName, string attributeCrmName,
            string attributeExchangeName, int entityTypeCode, int syncDirection, int defaultSyncDirection,
            int allowedSyncDirection, bool isComputed, DataCollection<string> computedProperties,
            string attributeCrmDisplayName, string attributeExchangeDisplayName) : base()
        {
            this.AttributeMappingId = attributeMappingId;
            this.MappingName = mappingName;
            this.AttributeCrmName = attributeCrmName;
            this.AttributeExchangeDisplayName = attributeExchangeDisplayName;
            this.EntityTypeCode = entityTypeCode;
            this.SyncDirection = syncDirection;
            this.DefaultSyncDirection = defaultSyncDirection;
            this.AllowedSyncDirection = allowedSyncDirection;
            this.IsComputed = isComputed;
            this.ComputedProperties = computedProperties;
            this.AttributeCrmDisplayName = attributeCrmDisplayName;
            this.AttributeExchangeDisplayName = attributeExchangeDisplayName;
        }
        static internal AttributeMapping LoadFromXml(XElement item)
        {
            AttributeMapping attributeMapping = new AttributeMapping()
            {
                AllowedSyncDirection = Util.LoadFromXml<int>(item.Element(Util.ns.m + "AllowedSyncDirection")),
                AttributeCrmDisplayName = Util.LoadFromXml<string>(item.Element(Util.ns.m + "AttributeCrmDisplayName")),
                AttributeCrmName = Util.LoadFromXml<string>(item.Element(Util.ns.m + "AttributeCrmName")),
                AttributeExchangeDisplayName = Util.LoadFromXml<string>(item.Element(Util.ns.c + "AttributeExchangeDisplayName")),
                AttributeExchangeName = Util.LoadFromXml<string>(item.Element(Util.ns.m + "AttributeExchangeName")),
                AttributeMappingId = Util.LoadFromXml<Guid>(item.Element(Util.ns.m + "AttributeMappingId")),
                //ComputedProperties = Util.LoadFromXml<DataCollection<string>>(item.Element(Util.ns.m + "ComputedProperties")),
                DefaultSyncDirection = Util.LoadFromXml<int>(item.Element(Util.ns.m + "DefaultSyncDirection")),
                EntityTypeCode = Util.LoadFromXml<int>(item.Element(Util.ns.m + "EntityTypeCode")),
                IsComputed = Util.LoadFromXml<bool>(item.Element(Util.ns.m + "IsComputed")),
                MappingName = Util.LoadFromXml<string>(item.Element(Util.ns.m + "MappingName")),
                SyncDirection = Util.LoadFromXml<int>(item.Element(Util.ns.m + "SyncDirection"))
            };
            foreach (XElement value in item.Elements(Util.ns.m + "ComputedProperties").Elements(Util.ns.f + "string"))
            {
                attributeMapping.ComputedProperties.Add(value.Value);
            }
            return attributeMapping;
        }
    }
    public sealed class AttributeMappingCollection : DataCollection<AttributeMapping>
    {
        public AttributeMappingCollection() { }
        public AttributeMappingCollection(IList<AttributeMapping> list)
        {
            this.AddRange(list);
        }
        static internal AttributeMappingCollection LoadFromXml(XElement item)
        {
            AttributeMappingCollection attributeMappingCollection = new AttributeMappingCollection();
            foreach (var attributeMapping in item.Elements(Util.ns.a + "AttributeMapping"))
            {
                attributeMappingCollection.Add(AttributeMapping.LoadFromXml(attributeMapping));
            }
            return attributeMappingCollection;
        }
    }
    public sealed class AttributePrivilege
    {
        public Guid AttributeId { get; set; }
        public int CanCreate { get; set; }
        public int CanRead { get; set; }
        public int CanUpdate { get; set; }
        public AttributePrivilege() { }
        public AttributePrivilege(Guid attributeId, int canCreate, int canRead, int canUpdate)
        {
            AttributeId = attributeId;
            CanCreate = canCreate;
            CanRead = canRead;
            CanUpdate = canUpdate;
        }
        static internal AttributePrivilege LoadFromXml(XElement item)
        {
            AttributePrivilege attributePrivilege = new AttributePrivilege()
            {
                AttributeId = Util.LoadFromXml<Guid>(item.Element(Util.ns.a + "AttributeId")),
                CanCreate = Util.LoadFromXml<int>(item.Element(Util.ns.a + "CanCreate")),
                CanRead = Util.LoadFromXml<int>(item.Element(Util.ns.a + "CanRead")),
                CanUpdate = Util.LoadFromXml<int>(item.Element(Util.ns.a + "CanUpdate"))
            };
            return attributePrivilege;
        }
    }
    public sealed class AttributePrivilegeCollection : DataCollection<AttributePrivilege>
    {
        public AttributePrivilegeCollection() { }
        public AttributePrivilegeCollection(IList<AttributePrivilege> list)
        {
            this.AddRange(list);
        }
        static internal AttributePrivilegeCollection LoadFromXml(XElement item)
        {
            AttributePrivilegeCollection attributePrivilegeCollection = new AttributePrivilegeCollection();
            foreach (var attributePrivilege in item.Elements(Util.ns.a + "AttributePrivilege"))
            {
                attributePrivilegeCollection.Add(AttributePrivilege.LoadFromXml(attributePrivilege));
            }
            return attributePrivilegeCollection;
        }
    }

    // Inheriting from Exception so that this class can be thrown as Exception
    public abstract class BaseServiceFault : Exception
    {
        public int ErrorCode { get; set; }
        public ErrorDetailCollection ErrorDetails { get; set; }
        public new string Message { get; set; }
        public DateTime Timestamp { get; set; }
        static internal void LoadFromXml(XElement item, BaseServiceFault fault)
        {
            if (item.Elements().Count() == 0)
                return;
            fault.ErrorCode = Util.LoadFromXml<int>(item.Element(Util.ns.a + "ErrorCode"));
            fault.ErrorDetails = ErrorDetailCollection.LoadFromXml(item.Element(Util.ns.a + "ErrorDetails"));
            fault.Message = Util.LoadFromXml<string>(item.Element(Util.ns.a + "Message"));
            fault.Timestamp = Util.LoadFromXml<DateTime>(item.Element(Util.ns.a + "Timestamp"));
        }
    }
    public sealed class BooleanManagedProperty : ManagedProperty<bool>
    {
        public BooleanManagedProperty() { }
        public BooleanManagedProperty(bool value)
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
        static internal BooleanManagedProperty LoadFromXml(XElement item)
        {
            if (item.Elements().Count() == 0)
                return new BooleanManagedProperty();
            BooleanManagedProperty booleanManagedProperty = new BooleanManagedProperty();
            ManagedProperty<bool>.LoadFromXml(item, booleanManagedProperty);
            booleanManagedProperty.Value = Util.LoadFromXml<bool>(item.Element(Util.ns.a + "Value"));
            return booleanManagedProperty;
        }
    }
    public sealed class BusinessEntityChanges
    {
        public BusinessEntityChangesCollection Changes { get; set; }
        public string DataToken { get; set; }
        public bool MoreRecords { get; set; }
        public string PagingCookie { get; set; }
        static internal BusinessEntityChanges LoadFromXml(XElement item)
        {
            BusinessEntityChanges businessEntityChanges = new BusinessEntityChanges()
            {
                Changes = Util.LoadFromXml<BusinessEntityChangesCollection>(item.Element(Util.ns.o + "Changes")),
                DataToken = Util.LoadFromXml<string>(item.Element(Util.ns.o + "DataToken")),
                MoreRecords = Util.LoadFromXml<bool>(item.Element(Util.ns.o + "MoreRecords")),
                PagingCookie = Util.LoadFromXml<string>(item.Element(Util.ns.o + "PagingCookie")),
            };
            
            return businessEntityChanges;
        }
    }
    public sealed class BusinessEntityChangesCollection : DataCollection<IChangedItem>
    {
        public BusinessEntityChangesCollection(){}
        public BusinessEntityChangesCollection(IList<IChangedItem> list)
        {
            this.AddRange(list);
        }
        static internal BusinessEntityChangesCollection LoadFromXml(XElement item)
        {
            BusinessEntityChangesCollection businessEntityChangesCollection = new BusinessEntityChangesCollection();
            foreach (var dataType in item.Elements(Util.ns.o + "anyType"))
            {
                if (dataType.Element(Util.ns.o + "Type").Value == ChangeType.NewOrUpdated.ToString())
                    businessEntityChangesCollection.Add(Util.LoadFromXml<NewOrUpdatedItem>(dataType));
                if (dataType.Element(Util.ns.o + "Type").Value == ChangeType.RemoveOrDeleted.ToString())
                    businessEntityChangesCollection.Add(Util.LoadFromXml<RemovedOrDeletedItem>(dataType));
            }
            return businessEntityChangesCollection;
        }
    }
    public class DataCollection<T> : Collection<T>
    {
        public void AddRange(params T[] items)
        {
            foreach (var item in items)
            {
                this.Add(item);
            }
        }
        public void AddRange(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                this.Add(item);
            }
        }
        public T[] ToArray()
        {
            return this.ToArray<T>();
        }
    }
    public abstract class DataCollection<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public virtual bool IsReadOnly { get; set; }
        public new void Add(TKey key, TValue value)
        {
            if (IsReadOnly)
                return;
            base.Add(key, value);
        }
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            if (IsReadOnly)
                return;
            this.Add(item.Key, item.Value);
        }
        public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> items)
        {
            if (IsReadOnly)
                return;
            this.AddRange(items.ToArray());
        }
        public void AddRange(params KeyValuePair<TKey, TValue>[] items)
        {
            if (IsReadOnly)
                return;
            foreach (var item in items)
            {
                this.Add(item);
            }
        }
        public bool Contains(TKey key)
        {
            return base.ContainsKey(key);
        }
        public bool Contains(KeyValuePair<TKey, TValue> key)
        {
            return base.ContainsKey(key.Key) && base.ContainsValue(key.Value);
        }
    }
    public sealed class DateTimeBehaviorConversionRule : ConstantsBase<string>
    {
        public static readonly DateTimeBehaviorConversionRule CreatedByTimeZone;
        public static readonly DateTimeBehaviorConversionRule LastUpdatedByTimeZone;
        public static readonly DateTimeBehaviorConversionRule OwnerTimeZone;
        public static readonly DateTimeBehaviorConversionRule SpecificTimeZone;
        public DateTimeBehaviorConversionRule() { }
        static DateTimeBehaviorConversionRule()
        {
            DateTimeBehaviorConversionRule.CreatedByTimeZone = Add<DateTimeBehaviorConversionRule>("CreatedByTimeZone");
            DateTimeBehaviorConversionRule.LastUpdatedByTimeZone = Add<DateTimeBehaviorConversionRule>("LastUpdatedByTimeZone");
            DateTimeBehaviorConversionRule.OwnerTimeZone = Add<DateTimeBehaviorConversionRule>("OwnerTimeZone");
            DateTimeBehaviorConversionRule.SpecificTimeZone = Add<DateTimeBehaviorConversionRule>("SpecificTimeZone");            
        }
        protected override bool ValueExistsInList(String value)
        {
            return ValidValues.Contains(value, StringComparer.OrdinalIgnoreCase);
        }
        internal string ToValueXml()
        {
            return Util.ObjectToXml(Value, "k:Value", true);
        }
        static internal DateTimeBehaviorConversionRule LoadFromXml(XElement item)
        {
            DateTimeBehaviorConversionRule dateTimeBehaviorConversionRule = new DateTimeBehaviorConversionRule();
            if (item.Elements().Count() == 0)
                return dateTimeBehaviorConversionRule;

            dateTimeBehaviorConversionRule.Value = Util.LoadFromXml<string>(item.Element(Util.ns.k + "Value"));
            return dateTimeBehaviorConversionRule;
        }
    }
    public class Entity
    {
        public AttributeCollection Attributes { get; set; }
        public EntityState? EntityState { get; set; }
        public FormattedValueCollection FormattedValues { get; set; }
        public virtual Guid Id { get; set; }
        public KeyAttributeCollection KeyAttributes { get; set; }
        public string LogicalName { get; set; }
        public RelatedEntityCollection RelatedEntities { get; set; }
        public string RowVersion { get; set; }
        public Entity()
        {
            Attributes = new AttributeCollection();
            FormattedValues = new FormattedValueCollection();
            KeyAttributes = new KeyAttributeCollection();
            RelatedEntities = new RelatedEntityCollection();
        }
        public Entity(string entityName)
            : this()
        {
            if (!String.IsNullOrEmpty(entityName))
                this.LogicalName = entityName;
        }
        public Entity(string LogicalName, KeyAttributeCollection keyAttributes)
            : this(LogicalName)
        {
            this.KeyAttributes.AddRange(keyAttributes);
        }
        public Entity(string entityName, Guid id)
            : this(entityName)
        {
            this.Id = id;
        }
        public Entity(string entityName, string keyName, Object keyValue)
            : this(entityName)
        {
            this.KeyAttributes.Add(new KeyValuePair<string, object>(keyName, keyValue));
        }
        public Entity(Entity entity)
        {
            Attributes = new AttributeCollection();
            FormattedValues = new FormattedValueCollection();
            RelatedEntities = new RelatedEntityCollection();
            this.LogicalName = entity.LogicalName;
            this.Attributes = entity.Attributes;
            this.FormattedValues = entity.FormattedValues;
            this.RelatedEntities = entity.RelatedEntities;
        }
        public object this[string attributeName]
        {
            get
            {
                if (this.Attributes.ContainsKey(attributeName))
                    return this.Attributes[attributeName];
                else
                    return null;
            }
            set
            {
                this.Attributes[attributeName] = value;
            }
        }
        public bool Contains(string attributeName)
        {
            if (!String.IsNullOrEmpty(attributeName))
                return this.Attributes.Contains(attributeName);
            else
                return false;
        }
        public virtual T GetAttributeValue<T>(string attributeLogicalName)
        {
            // Check if key exists and value type if same as specified.
            if (!String.IsNullOrEmpty(attributeLogicalName) && this.Contains(attributeLogicalName))
            {
                try
                {
                    return (T)this.Attributes[attributeLogicalName];
                }
                catch(Exception ex)
                {
                    return default(T);
                }
            }
            // If value not exist yet, return default value.
            else
                return default(T);            
        }
        protected virtual string GetFormattedAttributeValue(string attributeLogicalName)
        {
            // Check if this contains the key
            if (!String.IsNullOrEmpty(attributeLogicalName) && this.FormattedValues.Contains(attributeLogicalName))
                return this.FormattedValues[attributeLogicalName];
            else
                return null;
        }
        protected virtual IEnumerable<TEntity> GetRelatedEntities<TEntity>(string relationshipSchemaName,
            Nullable<EntityRole> primaryEntityRole) where TEntity : Entity
        {
            if (!String.IsNullOrEmpty(relationshipSchemaName))
            {
                Relationship key = new Relationship(relationshipSchemaName)
                {
                    PrimaryEntityRole = primaryEntityRole
                };

                // Create relationship
                if (RelatedEntities.Contains(key))
                    return RelatedEntities[key].Entities.Cast<TEntity>();
                else
                    return null;
            }
            else
                return null;
        }
        protected virtual TEntity GetRelatedEntity<TEntity>(string relationshipSchemaName,
            Nullable<EntityRole> primaryEntityRole) where TEntity : Entity
        {
            if (!String.IsNullOrEmpty(relationshipSchemaName))
            {
                // Create relationship
                Relationship key = new Relationship(relationshipSchemaName)
                {
                    PrimaryEntityRole = primaryEntityRole
                };

                if (RelatedEntities.Contains(key))
                    return (TEntity)RelatedEntities[key].Entities.FirstOrDefault();
                else
                    return null;
            }
            else
                return null;
        }
        protected virtual void SetAttributeValue(string attributeLogicalName, Object value)
        {
            if(!String.IsNullOrEmpty(attributeLogicalName))
                this[attributeLogicalName] = value;
        }
        protected virtual void SetRelatedEntities<TEntity>(string relationshipSchemaName, Nullable<EntityRole> primaryEntityRole,
            IEnumerable<TEntity> entities) where TEntity : Entity
        {
            // Check schemaname
            if (String.IsNullOrEmpty(relationshipSchemaName))
                return;
                
            // Create relationship
            Relationship key = new Relationship(relationshipSchemaName)
            {
                PrimaryEntityRole = primaryEntityRole
            };

            // check entities
            if (entities == null || entities.Count() == 0)
            {
                RelatedEntities[key] = null;
                return;
            }

            // Instantiate EntityCollection and pass properties
            EntityCollection collection = new EntityCollection();
            collection.EntityName = entities.First().LogicalName;
            collection.Entities.AddRange(entities);

            RelatedEntities[key] = collection;
        }
        protected virtual void SetRelatedEntity<TEntity>(string relationshipSchemaName, Nullable<EntityRole> primaryEntityRole,
            TEntity entity) where TEntity : Entity
        {
            // Check schemaname
            if (String.IsNullOrEmpty(relationshipSchemaName))
                return;

            // Create relationship
            Relationship key = new Relationship(relationshipSchemaName)
            {
                PrimaryEntityRole = primaryEntityRole
            };

            if (entity == null)
            {
                RelatedEntities[key] = null;
                return;
            }

            // Instantiate EntityCollection and pass properties
            EntityCollection collection = new EntityCollection();
            collection.EntityName = entity.LogicalName;
            collection.Entities.Add(entity);

            RelatedEntities[key] = collection;
        }
        // Convert Entity to early bound class like Account, Contact, etc.
        public T ToEntity<T>() where T : Entity
        {
            // If T is Entity, then just returns it's copy.
            if (typeof(T) == typeof(Entity))
            {
                Entity record = new Entity(this);
                return record as T;
            }

            // Instantiate early bound class object.
            T castedRecord = (T)Activator.CreateInstance(typeof(T), null);
            // Pass properties.
            castedRecord.Id = this.Id;
            castedRecord.Attributes = this.Attributes;
            castedRecord.FormattedValues = this.FormattedValues;
            return castedRecord;
        }
        // Convert Entity to EntityReference
        public EntityReference ToEntityReference()
        {
            if (this.Id == Guid.Empty)
                throw new Exception("Cannot convert to EntityReference without Id value");
            return new EntityReference()
            {
                Id = this.Id,
                LogicalName = this.LogicalName
            };
        }
        internal string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Attributes.ToXml());
            sb.Append("<a:EntityState i:nil='true' />");
            sb.Append("<a:FormattedValues />");
            sb.Append("<a:Id>");
            sb.Append((this.Id == Guid.Empty) ? "00000000-0000-0000-0000-000000000000" : Id.ToString());
            sb.Append("</a:Id>");
            sb.Append(this.KeyAttributes.ToXml());
            sb.Append(Util.ObjectToXml(LogicalName, "a:LogicalName", true));
            sb.Append(this.RelatedEntities.ToXml());
            sb.Append(Util.ObjectToXml(RowVersion, "a:RowVersion", true));
            return sb.ToString();
        }
        static internal Entity LoadFromXml(XElement item)
        {
            Entity entity = new Entity();
            entity.LogicalName = Util.LoadFromXml<string>(item.Element(Util.ns.a + "LogicalName"));
            entity.Id = Util.LoadFromXml<Guid>(item.Element(Util.ns.a + "Id"));
            entity.RowVersion = Util.LoadFromXml<string>(item.Element(Util.ns.a + "RowVersion"));
            entity.Attributes.LoadFromXml(item);
            entity.FormattedValues.LoadFromXml(item);
            if (!string.IsNullOrEmpty(item.Element(Util.ns.a + "EntityState").Value))
                entity.EntityState = Util.LoadFromXml<EntityState>(item.Element(Util.ns.a + "EntityState"));
            return OrganizationDataWebServiceProxy.ConvertToEarlyBound(entity);
        }
    }
    public sealed class EntityAttributeCollection : DataCollection<string, List<string>> 
    {
        internal string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in this)
            {
                sb.Append("<o:KeyValuePairOfstringArrayOfanyTypety7Ep6D1>");
                sb.Append(Util.ObjectToXml(item.Key, "b:key", true));
                sb.Append("<b:value>");
                foreach (var value in item.Value)
                {
                    sb.Append("<f:anyType i:type='c:string'>");
                    sb.Append(value);
                    sb.Append("</f:anyType>");
                }
                sb.Append("</b:value>");
            }
            sb.Append("</o:KeyValuePairOfstringArrayOfanyTypety7Ep6D1>");
            return sb.ToString();
        }
    }
    public sealed class EntityCollection
    {
        public string EntityName { get; set; }
        public string MinActiveRowVersion { get; set; }
        public bool MoreRecords { get; set; }
        public string PagingCookie { get; set; }
        public int TotalRecordCount { get; set; }
        public bool TotalRecordCountLimitExceeded { get; set; }
        public DataCollection<Entity> Entities { get; set; }
        public EntityCollection()
        {
            this.Entities = new DataCollection<Entity>();
        }
        public EntityCollection(Entity[] Entities)
            : this()
        {
            foreach (Entity item in Entities)
            {
                this.Entities.Add(item);
            }

        }
        internal string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Util.ObjectToXml(Entities.ToArray(), "a:Entities", true));
            sb.Append(Util.ObjectToXml(EntityName, "a:EntityName", true));
            sb.Append(Util.ObjectToXml(MinActiveRowVersion, "a:MinActiveRowVersion", true));
            sb.Append(Util.ObjectToXml(MoreRecords, "a:MoreRecords", true));
            sb.Append(Util.ObjectToXml(PagingCookie, "a:PagingCookie", true));
            sb.Append(Util.ObjectToXml(TotalRecordCount, "a:TotalRecordCount", true));
            sb.Append(Util.ObjectToXml(TotalRecordCountLimitExceeded, "a:TotalRecordCountLimitExceeded", true));
            return sb.ToString();
        }
        public Entity this[int i]
        {
            get
            {
                if (Entities.Count > 0)
                    return this.Entities[i];
                else
                    return null;
            }
        }
        static internal EntityCollection LoadFromXml(XElement item)
        {
            EntityCollection entityCollection = new EntityCollection()
            {
                EntityName = Util.LoadFromXml<string>(item.Element(Util.ns.a + "EntityName")),
                MinActiveRowVersion = Util.LoadFromXml<string>(item.Element(Util.ns.a + "MinActiveRowVersion")),
                PagingCookie = Util.LoadFromXml<string>(item.Element(Util.ns.a + "PagingCookie")),
                MoreRecords = Util.LoadFromXml<bool>(item.Element(Util.ns.a + "MoreRecords")),
                TotalRecordCount = Util.LoadFromXml<int>(item.Element(Util.ns.a + "TotalRecordCount")),
                TotalRecordCountLimitExceeded = Util.LoadFromXml<bool>(item.Element(Util.ns.a + "TotalRecordCountLimitExceeded")),
            };
            foreach (var entity in item.Element(Util.ns.a + "Entities").Elements(Util.ns.a + "Entity"))
            {
                entityCollection.Entities.Add(Entity.LoadFromXml(entity));
            }
            if (entityCollection.Entities.Count > 0)
                entityCollection.TotalRecordCount = entityCollection.Entities.Count;
            return entityCollection;
        }
    }
    public sealed class EntityImageCollection : DataCollection<string, Entity> { }
    public sealed class EntityReference
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public KeyAttributeCollection KeyAttributes { get; set; }
        public string LogicalName { get; set; }
        public string RowVersion { get; set; }
        public EntityReference()
        {
            this.KeyAttributes = new KeyAttributeCollection();
        }
        public EntityReference(string logicalName)
            : this()
        {

            this.LogicalName = logicalName;
        }        
        public EntityReference(string LogicalName, KeyAttributeCollection keyAttributeCollection)
            : this(LogicalName)
        {
            this.KeyAttributes.AddRange(keyAttributeCollection);
        }
        public EntityReference(string logicalName, Guid id)
            : this(logicalName)
        {
            this.Id = id;
        }
        public EntityReference(string logicalName, string keyName, Object keyValue)
            : this(logicalName)
        {
            this.KeyAttributes.Add(new KeyValuePair<string, object>(keyName, keyValue));
        }
        internal string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Util.ObjectToXml(Id, "a:Id", true));
            sb.Append(this.KeyAttributes.ToXml());
            sb.Append(Util.ObjectToXml(LogicalName, "a:LogicalName", true));
            sb.Append(Util.ObjectToXml(Name, "a:Name", true));
            sb.Append(Util.ObjectToXml(RowVersion, "a:RowVersion", true));
            return sb.ToString();
        }
        static internal EntityReference LoadFromXml(XElement item)
        {
            EntityReference entityReference = new EntityReference()
            {
                Id = Util.LoadFromXml<Guid>(item.Element(Util.ns.a + "Id")),
                LogicalName = Util.LoadFromXml<string>(item.Element(Util.ns.a + "LogicalName")),
                Name = Util.LoadFromXml<string>(item.Element(Util.ns.a + "Name")),
                RowVersion = Util.LoadFromXml<string>(item.Element(Util.ns.a + "RowVersion"))
            };
            return entityReference;
        }
    }
    public sealed class EntityReferenceCollection : DataCollection<EntityReference>
    {
        internal string ToValueXml()
        {
            return Util.ObjectToXml(this.ToArray(), "a:EntityReference", true);
        }
        static internal EntityReferenceCollection LoadFromXml(XElement item)
        {
            EntityReferenceCollection entityReferenceCollection = new EntityReferenceCollection();
            foreach (var entityReference in item.Elements(Util.ns.a + "EntityReference"))
            {
                entityReferenceCollection.Add(EntityReference.LoadFromXml(entityReference));
            }
            return entityReferenceCollection;
        }
    }
    public sealed class ErrorDetailCollection : DataCollection<string, Object>
    {
        static internal ErrorDetailCollection LoadFromXml(XElement item)
        {
            ErrorDetailCollection errorDetailCollection = new ErrorDetailCollection()
            {

            };
            return errorDetailCollection;
        }
    }
    public static class FieldPermissionType
    {
        public const int Allowed = 4;
        public const int NotAllowed = 0;
        public static void Validate(int value)
        { }
    }
    public sealed class FormattedValueCollection : DataCollection<string, string>
    {        
        internal void LoadFromXml(XElement item)
        {
            foreach (var fmt in item.Elements(Util.ns.a + "FormattedValues").Elements(Util.ns.a + "KeyValuePairOfstringstring"))
            {
                FormattedValueLoadFromXml(fmt);
            }
        }
        internal void FormattedValueLoadFromXml(XElement item)
        {
            string key = Util.LoadFromXml<string>(item.Element(Util.ns.b + "key"));
            string value = Util.LoadFromXml<string>(item.Element(Util.ns.b + "value"));
            this.Add(key, value);
        }
    }
    public sealed class KeyAttributeCollection : DataCollection<string, Object> 
    {
        internal string ToXml()
        {
            StringBuilder sb = new StringBuilder();

            if (this.Count == 0)
            {
                return sb.Append("<a:KeyAttributes/>").ToString();
            }
            sb.Append("<a:KeyAttributes>");
            foreach (var item in this)
            {
                sb.Append(KeyAttributeToXml(item));
            }
            sb.Append("</a:KeyAttributes>");
            return sb.ToString();
        }
        internal string KeyAttributeToXml(KeyValuePair<string, Object> item)
        {
            return "<o:KeyValuePairOfstringanyType>"
                + Util.ObjectToXml(item.Key, "b:key", true)
                + Util.ObjectToXml(item.Value, "b:value", false)
                + "</o:KeyValuePairOfstringanyType>";
        }
    }
    public sealed class Label
    {
        public LocalizedLabelCollection LocalizedLabels { get; set; }
        public LocalizedLabel UserLocalizedLabel { get; set; }
        public Label()
        {
            this.LocalizedLabels = new LocalizedLabelCollection();
        }
        public Label(LocalizedLabel userLocalizedLabel, LocalizedLabel[] labels)
            : this()
        {
            this.UserLocalizedLabel = userLocalizedLabel;
            foreach (LocalizedLabel label in labels)
            {
                this.LocalizedLabels.Add(label);
            }
        }
        public Label(string label, int languageCode)
            : this()
        {
            this.LocalizedLabels.Add(new LocalizedLabel(label, languageCode));
        }
        internal string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Util.ObjectToXml(LocalizedLabels, "a:LocalizedLabels", true));
            sb.Append(Util.ObjectToXml(UserLocalizedLabel, "a:UserLocalizedLabel", true));
            return sb.ToString();
        }
        static internal Label LoadFromXml(XElement item)
        {
            if (item.Elements().Count() == 0)
                return new Label();
            Label label = new Label()
            {
                UserLocalizedLabel = LocalizedLabel.LoadFromXml(item.Element(Util.ns.a + "UserLocalizedLabel")),
                LocalizedLabels = LocalizedLabelCollection.LoadFromXml(item.Element(Util.ns.a + "LocalizedLabels"))
            };
            return label;
        }
    }
    public sealed class LocalizedLabel : MetadataBase
    {
        public bool? IsManaged { get; set; }
        public string Label { get; set; }
        public int LanguageCode { get; set; }
        public LocalizedLabel() { }
        public LocalizedLabel(string label, int languageCode)
        {
            this.Label = label;
            this.LanguageCode = languageCode;
        }
        internal new string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToValueXml());
            sb.Append(Util.ObjectToXml(IsManaged, "a:IsManaged", true));
            sb.Append(Util.ObjectToXml(Label, "a:Label", true));
            sb.Append(Util.ObjectToXml(LanguageCode, "a:LanguageCode", true));
            return sb.ToString();
        }
        static internal LocalizedLabel LoadFromXml(XElement item)
        {
            if (item.Elements().Count() == 0)
                return new LocalizedLabel();
            LocalizedLabel localizedLabel = new LocalizedLabel()
            {
                IsManaged = Util.LoadFromXml<bool?>(item.Element(Util.ns.a + "IsManaged")),
                Label = Util.LoadFromXml<string>(item.Element(Util.ns.a + "Label")),
                LanguageCode = Util.LoadFromXml<int>(item.Element(Util.ns.a + "LanguageCode")),
            };
            MetadataBase.LoadFromXml(item, localizedLabel);
            return localizedLabel;
        }
    }
    public sealed class LocalizedLabelCollection : DataCollection<LocalizedLabel>
    {
        public LocalizedLabelCollection() { }
        public LocalizedLabelCollection(IList<LocalizedLabel> list)
        {
            this.AddRange(list);
        }
        internal string ToValueXml()
        {
            return Util.ObjectToXml(this.ToArray(), "a:LocalizedLabel", true);
        }
        static internal LocalizedLabelCollection LoadFromXml(XElement item)
        {
            LocalizedLabelCollection LocalizedLabelCollection = new LocalizedLabelCollection();
            foreach (var localizedLabel in item.Elements(Util.ns.a + "LocalizedLabel"))
            {
                LocalizedLabelCollection.Add(LocalizedLabel.LoadFromXml(localizedLabel));
            }
            return LocalizedLabelCollection;
        }
    }
    public sealed class MailboxTrackingFolderMapping
    {
        public string ExchangeFolderId { get; set; }
        public string ExchangeFolderName { get; set; }
        public bool IsFolderOnboarded { get; set; }
        public Guid RegardingObjectId { get; set; }
        public string RegardingObjectName { get; set; }
        public int RegardingObjectTypeCode { get; set; }

        public MailboxTrackingFolderMapping() { }
        public MailboxTrackingFolderMapping(string exchangeFolderId, string exchangeFolderName, Guid regardingObjectId, string regardingObjectName,
            int regardingObjectTypeCode, bool isFolderOnboarded)
        {
            this.ExchangeFolderId = exchangeFolderId;
            this.ExchangeFolderName = exchangeFolderName;
            this.IsFolderOnboarded = IsFolderOnboarded;
            this.RegardingObjectId = regardingObjectId;
            this.RegardingObjectName = regardingObjectName;
            this.RegardingObjectTypeCode = regardingObjectTypeCode;
        }
        static internal MailboxTrackingFolderMapping LoadFromXml(XElement item)
        {
            MailboxTrackingFolderMapping mailboxTrackingFolderMapping = new MailboxTrackingFolderMapping()
            {
                ExchangeFolderId = Util.LoadFromXml<string>(item.Element(Util.ns.o + "ExchangeFolderId")),
                ExchangeFolderName = Util.LoadFromXml<string>(item.Element(Util.ns.o + "ExchangeFolderName")),
                IsFolderOnboarded = Util.LoadFromXml<bool>(item.Element(Util.ns.o + "IsFolderOnboarded")),
                RegardingObjectId = Util.LoadFromXml<Guid>(item.Element(Util.ns.o + "RegardingObjectId")),
                RegardingObjectName = Util.LoadFromXml<string>(item.Element(Util.ns.o + "RegardingObjectName")),
                RegardingObjectTypeCode = Util.LoadFromXml<int>(item.Element(Util.ns.o + "RegardingObjectTypeCode"))
            };
            return mailboxTrackingFolderMapping;
        }
    }
    public sealed class MailboxTrackingFolderMappingCollection : DataCollection<MailboxTrackingFolderMapping>
    {
        public MailboxTrackingFolderMappingCollection() { }
        public MailboxTrackingFolderMappingCollection(IList<MailboxTrackingFolderMapping> list)
        {
            this.AddRange(list);
        }
        static internal MailboxTrackingFolderMappingCollection LoadFromXml(XElement item)
        {
            MailboxTrackingFolderMappingCollection LocalizedLabelCollection = new MailboxTrackingFolderMappingCollection();
            foreach (var mailboxTrackingFolderMapping in item.Elements(Util.ns.o + "MailboxTrackingFolderMapping"))
            {
                LocalizedLabelCollection.Add(MailboxTrackingFolderMapping.LoadFromXml(mailboxTrackingFolderMapping));
            }
            return LocalizedLabelCollection;
        }
    }
    public abstract class ManagedProperty<T>
    {
        public bool CanBeChanged { get; set; }
        public string ManagedPropertyLogicalName { get; set; }
        public T Value { get; set; }
        public ManagedProperty() { this.CanBeChanged = true; }
        public ManagedProperty(string managedPropertyLogicalName)
        {
            this.ManagedPropertyLogicalName = managedPropertyLogicalName;
            this.CanBeChanged = true;
        }
        internal string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Util.ObjectToXml(CanBeChanged, "a:CanBeChanged", true));
            sb.Append(Util.ObjectToXml(ManagedPropertyLogicalName, "a:ManagedPropertyLogicalName", true));
            return sb.ToString();
        }
        static internal void LoadFromXml(XElement item, ManagedProperty<T> meta)
        {
            if (item.Elements().Count() == 0)
                return;
            meta.CanBeChanged = Util.LoadFromXml<bool>(item.Element(Util.ns.a + "CanBeChanged"));
            meta.ManagedPropertyLogicalName = Util.LoadFromXml<string>(item.Element(Util.ns.a + "ManagedPropertyLogicalName"));
        }
    }
    public sealed class Money
    {
        public Money()
        {
        }
        public Money(decimal Value)
        {
            this.Value = Value;
        }
        public decimal Value { get; set; }
        internal string ToValueXml()
        {
            return Util.ObjectToXml(Value, "a:Value", true);
        }
        static internal Money LoadFromXml(XElement item)
        {
            Money money = new Money()
            {
                Value = Util.LoadFromXml<decimal>(item.Element(Util.ns.a + "Value"))
            };
            return money;
        }
    }
    public sealed class NewOrUpdatedItem : IChangedItem
    {
        public Entity NewOrUpdatedEntity { get; set; }
        public ChangeType Type { get; set; }
        public NewOrUpdatedItem()
        {

        }
        public NewOrUpdatedItem(ChangeType type, Entity entity)
        {
            this.NewOrUpdatedEntity = entity;
            this.Type = type;
        }
        static internal NewOrUpdatedItem LoadFromXml(XElement item)
        {
            NewOrUpdatedItem newOrUpdatedItem = new NewOrUpdatedItem
            {
                Type = Util.LoadFromXml<ChangeType>(item.Element(Util.ns.o + "Type")),
                NewOrUpdatedEntity = Util.LoadFromXml<Entity>(item.Element(Util.ns.o + "NewOrUpdatedEntity"))
            };
            return newOrUpdatedItem;
        }
    }
    public sealed class OptionSetValue
    {
        public int Value { get; set; }
        public OptionSetValue()
        {
        }
        public OptionSetValue(int value)
        {
            this.Value = value;
        }
        internal string ToValueXml()
        {
            return Util.ObjectToXml(Value, "a:Value", true);
        }
        static internal OptionSetValue LoadFromXml(XElement item)
        {
            OptionSetValue optionSet = new OptionSetValue()
            {
                Value = Util.LoadFromXml<int>(item.Element(Util.ns.a + "Value"))
            };
            return optionSet;
        }
    }
    public sealed class OrganizationServiceFault : BaseServiceFault
    {
        public OrganizationServiceFault InnerFault { get; set; }
        public string TraceText { get; set; }
        static internal OrganizationServiceFault LoadFromXml(XElement item)
        {
            if (item.Elements().Count() == 0)
                return null;
            OrganizationServiceFault organizationServiceFault = new OrganizationServiceFault()
            {
                TraceText = Util.LoadFromXml<string>(item.Element(Util.ns.a + "TraceText")),
                InnerFault = OrganizationServiceFault.LoadFromXml(item.Element(Util.ns.a + "InnerFault"))
            };
            BaseServiceFault.LoadFromXml(item, organizationServiceFault);
            return organizationServiceFault;
        }
    }
    public sealed class ParameterCollection : DataCollection<string, Object>
    {
    }
    public sealed class RelatedEntityCollection : DataCollection<Relationship, EntityCollection>
    {
        internal string ToXml()
        {
            StringBuilder sb = new StringBuilder();

            if (this.Count == 0)
            {
                return sb.Append("<a:RelatedEntities/>").ToString();
            }
            sb.Append("<a:RelatedEntities>");
            foreach (var item in this)
            {
                sb.Append(RelatedEntityToXml(item));
            }
            sb.Append("</a:RelatedEntities>");
            return sb.ToString();
        }
        internal string RelatedEntityToXml(KeyValuePair<Relationship, EntityCollection> item)
        {
            return "<a:KeyValuePairOfRelationshipEntityCollectionX_PsK4FkN>"
                + Util.ObjectToXml(item.Key, "b:key", true)
                + Util.ObjectToXml(item.Value, "b:value", true)
                + "</a:KeyValuePairOfRelationshipEntityCollectionX_PsK4FkN>";
        }
    }
    public sealed class Relationship
    {
        public EntityRole? PrimaryEntityRole { get; set; }
        public string SchemaName { get; set; }
        public Relationship()
        {
        }
        public Relationship(string schemaName)
        {
            if(!String.IsNullOrEmpty(schemaName))
                this.SchemaName = schemaName;
        }
        internal string ToValueXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Util.ObjectToXml(PrimaryEntityRole, "a:PrimaryEntityRole", true));
            sb.Append(Util.ObjectToXml(SchemaName, "a:SchemaName", true));
            return sb.ToString();
        }
    }
    public sealed class RemovedOrDeletedItem : IChangedItem
    {
        public EntityReference RemovedItem { get; set; }
        public ChangeType Type { get; set; }
        public RemovedOrDeletedItem()
        {

        }
        public RemovedOrDeletedItem(ChangeType type, EntityReference entityReference)
        {
            this.RemovedItem = entityReference;
            this.Type = type;
        }
        static internal RemovedOrDeletedItem LoadFromXml(XElement item)
        {
            RemovedOrDeletedItem removedOrDeletedItem = new RemovedOrDeletedItem
            {
                Type = Util.LoadFromXml<ChangeType>(item.Element(Util.ns.o + "Type")),
                RemovedItem = Util.LoadFromXml<EntityReference>(item.Element(Util.ns.o + "RemovedItem"))
            };
            return removedOrDeletedItem;
        }
    }

    #region OrganizationRequest/Response

    // In this code file, I only put basic Request/Response class.
    // All the other messages are in separate file(s).
    public class OrganizationRequest
    {
        public string RequestName { get; set; }
        public Guid RequestId { get; set; }
        public Object item { get; set; }
        public ParameterCollection Parameters { get; set; }
        // responseType stores each message response instance so that
        // Execute method can return correct type.
        // I am not sure if it is good idea to instantiate the response
        // when request instantiated though.  
        internal OrganizationResponse ResponseType { get; set; }
        // ctor
        public OrganizationRequest()
        {
            this.Parameters = new ParameterCollection();
        }
        public object this[string parameterName]
        {
            get
            {
                if (this.Parameters.ContainsKey(parameterName))
                    return this.Parameters[parameterName];
                else
                    return null;
            }
            set
            {
                this.Parameters[parameterName] = value;
            }
        }
        // Each message request has override method which generates
        // correct SOAP request.
        internal virtual string GetRequestBody() { return ""; }
        /// <summary>
        /// Generate Soap XML request
        /// </summary>
        /// <returns></returns>
        internal string GetSoapBody()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<d:request>");
            sb.Append((this.Parameters.Count == 0) ? "<a:Parameters />" :
                "<a:Parameters>" + GetParameters() + "</a:Parameters>");
            sb.Append((this.RequestId == null || this.RequestId == Guid.Empty) ? "<a:RequestId i:nil='true' />" :
                "<a:RequestId>" + RequestId.ToString() + "</a:RequestId>");
            sb.Append("<a:RequestName>" + RequestName + "</a:RequestName>");
            sb.Append("</d:request>");
            return sb.ToString();
        }
        /// <summary>
        /// This method generates parameter nodes for Soap request
        /// </summary>
        /// <returns></returns>
        internal string GetParameters()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var parameter in Parameters)
            {
                if (parameter.Value == null)
                    continue;
                sb.Append("<a:KeyValuePairOfstringanyType>");
                sb.Append("<b:key>" + parameter.Key + "</b:key>");
                // Use util class method to generate appropriate node.
                sb.Append(Util.ObjectToXml(parameter.Value, "b:value"));
                sb.Append("</a:KeyValuePairOfstringanyType>");
            }
            return sb.ToString();
        }
    }
    public class OrganizationResponse
    {
        public string ResponseName { get; set; }
        public Collection<KeyValuePair<string, object>> Results { get; set; }
        public string Item { get; set; }
        // Each message response has override method which restore
        // result to its members.
        internal virtual void StoreResult(HttpResponseMessage httpResponse) { }
    }

    #endregion OrganizationRequest/Response
}