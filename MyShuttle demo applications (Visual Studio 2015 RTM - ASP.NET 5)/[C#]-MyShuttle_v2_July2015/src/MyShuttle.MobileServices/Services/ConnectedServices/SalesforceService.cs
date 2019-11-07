

namespace MyShuttle.MobileServices.Services.ConnectedServices
{
    using MyShuttle.MobileServices.DataObjects;
    using MyShuttle.MobileServices.Models;
    using Newtonsoft.Json;
    using Salesforce.Common;
    using Salesforce.Force;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;

    public class SalesforceService
    {
        string _accountId = string.Empty;
        string _consumerKey = string.Empty;
        string _consumerSecret = string.Empty;
        string _username = string.Empty;
        string _password = string.Empty;
        AuthenticationClient _authenticationClient = null;

        public SalesforceService()
        {
            _accountId = ConfigurationManager.AppSettings["Salesforce:AccountId"];
            _consumerKey = ConfigurationManager.AppSettings["Salesforce:ConsumerKey"];
            _consumerSecret = ConfigurationManager.AppSettings["Salesforce:ConsumerSecret"];
            _username = ConfigurationManager.AppSettings["Salesforce:Username"];
            _password = ConfigurationManager.AppSettings["Salesforce:Password"];

            _authenticationClient = new AuthenticationClient(new HttpClient());
        }

        public async Task AddContact(Ride item, Employee employee)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            if (employee == null)
                throw new ArgumentNullException("employee");

            var client = await GetForceClient();

            var resultQueryExist = await client
                .QueryAsync<Contact>(string.Format(@"
                        SELECT Id, IsDeleted, FirstName, LastName, CreatedDate, LastModifiedDate, Email, AccountId  
                        From Contact Where Email='{0}'", employee.Email));

            if (!resultQueryExist.records.Any())
            {
                await client.CreateAsync("Contact", GetContact(employee));
                Trace.TraceInformation("Contact created successfully");
            }
        }

        async Task<ForceClient> GetForceClient()
        {
            await _authenticationClient.UsernamePasswordAsync(_consumerKey, _consumerSecret, _username, _password);

            return new ForceClient(
                _authenticationClient.InstanceUrl,
                _authenticationClient.AccessToken,
                _authenticationClient.ApiVersion);
        }


        Contact GetContact(Employee employee)
        {
            var contact = new Contact();

            string[] sName = employee.Name.Split(null);

            if (sName.Length > 1)
            {
                contact.FirstName = sName[0];
                contact.LastName = sName[1];
            }
            else
            {
                contact.FirstName = string.Empty;
                contact.LastName = sName[0];
            }

            contact.Email = employee.Email;
            contact.AccountId = _accountId; // In this demo the accoundId is always the same

            return contact;
        }
    }
}