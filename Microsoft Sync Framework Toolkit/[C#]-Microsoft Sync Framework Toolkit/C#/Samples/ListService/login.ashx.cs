// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;

namespace ListService
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class newuser1 : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                var userName = context.Request.QueryString["username"];

                // Check if we have a valid user
                if (String.IsNullOrEmpty(userName) || 0 == userName.Trim().Length)
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.StatusCode = 400;
                    context.Response.StatusDescription = "Invalid username in querystring";
                    return;
                }

                // We have a valid user
                userName = userName.Trim();

                // Do some basic checks to allow only alpha numeric characters
                var validator = new Regex("^([A-Za-z0-9 ])+$");

                if (!validator.IsMatch(userName))
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.StatusCode = 400;
                    context.Response.StatusDescription = "Invalid username in querystring";
                    return;
                }

                Guid userId = Guid.NewGuid();

                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ListDbConnectionString"].ToString()))
                {
                    connection.Open();

                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.Text;
                        command.CommandText = String.Format("SELECT ID FROM [User] WHERE [Name] = '{0}'", userName);

                        
                        object existingUserId = command.ExecuteScalar(); 

                        if (null != existingUserId)
                        {
                            context.Response.ContentType = "text/plain";
                            context.Response.StatusCode = 200;
                            context.Response.Write((Guid)existingUserId);
                            return;
                        }
                    }

                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.Text;
                        command.CommandText = String.Format("INSERT INTO [User] (ID, Name) VALUES ('{0}', '{1}')", userId, userName);

                        command.ExecuteNonQuery();
                    }
                }
                
                context.Response.ContentType = "text/plain";
                context.Response.StatusCode = 200;
                context.Response.Write(userId);
            }
            catch (Exception exception)
            {
                context.Response.ContentType = "text/plain";
                context.Response.StatusCode = 500;
                context.Response.StatusDescription = exception.Message;
                return;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
