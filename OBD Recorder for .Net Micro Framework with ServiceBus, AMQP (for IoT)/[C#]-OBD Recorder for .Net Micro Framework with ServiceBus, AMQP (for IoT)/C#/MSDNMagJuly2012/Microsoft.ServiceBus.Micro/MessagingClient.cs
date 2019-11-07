//#define CERTS

// 
//  (c) Microsoft Corporation. See LICENSE.TXT file for licensing details
//  
namespace Microsoft.ServiceBus.Micro
{
    using System;
    using System.Net;
    using Microsoft.SPOT.Net.Security;

    public class MessagingClient
    {
#if CERTS
        static X509Certificate[] caCerts;
#endif
        readonly Uri entityAddress;
        readonly TokenProvider tokenProvider;

        static MessagingClient()
        {
#if CERTS
            caCerts = new[]
                          {
                              new X509Certificate(Resources.GetBytes(Resources.BinaryResources.gte)),
                              new X509Certificate(Resources.GetBytes(Resources.BinaryResources.mia)),
                              new X509Certificate(Resources.GetBytes(Resources.BinaryResources.mssi))
                          };
#endif
        }

        public MessagingClient(Uri entityAddress, TokenProvider tokenProvider)
        {
            this.entityAddress = entityAddress;
            this.tokenProvider = tokenProvider;
        }

        public SimpleMessage Receive(TimeSpan timeout, ReceiveMode receiveMode)
        {
            if (receiveMode == ReceiveMode.PeekLock)
            {
                return this.PeekLockReceive(timeout);
            }
            else
            {
                return this.ReceiveAndDelete(timeout);
            }
        }

        SimpleMessage PeekLockReceive(TimeSpan timeout)
        {
            var wq = this.CreateReceiveRequest(timeout);
            wq.Method = "POST";
            wq.Headers.Add("Date", DateTime.UtcNow.ToString("R"));
            this.PreprocessRequest(wq);
            wq.ContentLength = 0;
            try
            {
                var wr = wq.GetResponse() as HttpWebResponse;
                if (wr.StatusCode ==
                    HttpStatusCode.Created)
                {
                    return new SimpleMessage(wr.GetResponseStream(), wr.ContentType, wr.Headers, this);
                }
                else
                {
                    return null;
                }
            }
            catch (WebException we)
            {
                if (we.Response == null)
                {
                    throw;
                }
                if (we.Status == WebExceptionStatus.Success &&
                    ((HttpWebResponse) we.Response).StatusCode == HttpStatusCode.NoContent)
                {
                    return null;
                }
                throw;
            }
        }


        SimpleMessage ReceiveAndDelete(TimeSpan timeout)
        {
            var wq = this.CreateReceiveRequest(timeout);
            wq.Method = "DELETE";
            wq.Headers.Add("Date", DateTime.UtcNow.ToString("R"));
            this.PreprocessRequest(wq);
            wq.ContentLength = 0;
            try
            {
                var wr = wq.GetResponse() as HttpWebResponse;
                if (wr.StatusCode == HttpStatusCode.OK &&
                    this.ValidateReceiveResponse(wr))
                {
                    return new SimpleMessage(wr.GetResponseStream(), wr.ContentType, wr.Headers, null);
                }
                else
                {
                    return null;
                }
            }
            catch (WebException we)
            {
                if (we.Status == WebExceptionStatus.Success &&
                    ((HttpWebResponse) we.Response).StatusCode == HttpStatusCode.NoContent)
                {
                    return null;
                }
                throw;
            }
        }

        bool ValidateReceiveResponse(HttpWebResponse wr)
        {
            if (wr.ResponseUri.Scheme == "https")
            {
                return true;
            }
            else
            {
                return this.tokenProvider.ValidateResponse(wr);
            }
        }

        void PreprocessRequest(HttpWebRequest wq)
        {
            if (wq.RequestUri.Scheme == "http")
            {
                wq.Headers.Add("Endpoint", wq.RequestUri.Host + ":" + (wq.RequestUri.Port == -1 ? 80 : wq.RequestUri.Port));
                this.tokenProvider.SignRequest(wq);
            }
        }

        public void Send(SimpleMessage message)
        {
            var wq = this.CreateSendRequest();
            wq.Method = "POST";
            wq.Headers.Add("Date", DateTime.UtcNow.ToString("R"));
            if (message.BrokerProperties != null && message.BrokerProperties.Count > 0)
            {
                string brokerProperties="";

                foreach (var brokerPropertyName in message.BrokerProperties.Keys)
                {
                    string brokerPropertyValue = message.BrokerProperties[brokerPropertyName] as string;
                    brokerProperties += "\""+brokerPropertyName + "\" : \"" + brokerPropertyValue + "\",";
                }
                wq.Headers.Add("BrokerProperties", "{ "+brokerProperties.TrimEnd(',')+" }");
            }

            foreach (string header in message.Properties.Keys)
            {
                var value = message.Properties[header];
                string valueAsString;
                if (value == null)
                {
                    valueAsString = null;
                }
                else if (value is DateTime)
                {
                    valueAsString = ((DateTime)value).ToString("R");
                }
                else if (value is int || value is Int16|| value is Int32|| value is Int64 
                        || value is uint || value is UInt16 || value is UInt32|| value is UInt64 
                        || value is float || value is double || value is bool)
                {
                    valueAsString = value.ToString();
                }
                else
                {
                    valueAsString = "\"" + value.ToString() + "\"";
                }
                wq.Headers.Add(header, valueAsString);
            }

            if (message.BodyStream != null)
            {
                wq.ContentLength = message.BodyStream.Length;
                var requestStream = wq.GetRequestStream();
                wq.ContentType = message.ContentType;
                var buffer = new byte[1024];
                var bytesRead = 0;
                while ((bytesRead = message.BodyStream.Read(buffer, 0, 1024)) > 0)
                {
                    requestStream.Write(buffer, 0, bytesRead);
                }
                requestStream.Close();
            }
            else
            {
                wq.ContentLength = 0;
            }

            this.PreprocessRequest(wq);

            try
            {
                var wr = wq.GetResponse() as HttpWebResponse;
                if (wr.StatusCode != HttpStatusCode.OK &&
                    wr.StatusCode != HttpStatusCode.Created)
                {
                    throw new WebException(wr.StatusDescription, null, WebExceptionStatus.ReceiveFailure, wr);
                }
                wr.Close();
            }
            catch (WebException we)
            {
                if (we.Response != null)
                {
                    we.Response.Close();
                }
                throw;
            }
        }

        public void Complete(Uri messageLockUri)
        {
            var wq = this.CreateLockRequest("DELETE", messageLockUri);
            wq.Headers.Add("Date", DateTime.UtcNow.ToString("R"));
            wq.ContentLength = 0;
            this.PreprocessRequest(wq);

            try
            {
                var wr = wq.GetResponse() as HttpWebResponse;
                if (wr.StatusCode !=
                    HttpStatusCode.OK)
                {
                    throw new WebException(wr.StatusDescription, null, WebExceptionStatus.ReceiveFailure, wr);
                }
                wr.Close();
            }
            catch (WebException we)
            {
                if (we.Response != null)
                {
                    we.Response.Close();
                }
                throw;
            }
        }

        public void Abandon(Uri messageLockUri)
        {
            var wq = this.CreateLockRequest("PUT", messageLockUri);
            wq.Headers.Add("Date", DateTime.UtcNow.ToString("R"));
            wq.ContentLength = 0;
            this.PreprocessRequest(wq);

            try
            {
                var wr = wq.GetResponse() as HttpWebResponse;
                if (wr.StatusCode !=
                    HttpStatusCode.OK)
                {
                    throw new WebException(wr.StatusDescription, null, WebExceptionStatus.ReceiveFailure, wr);
                }
                wr.Close();
            }
            catch (WebException we)
            {
                if (we.Response != null)
                {
                    we.Response.Close();
                }
                throw;
            }
        }

        HttpWebRequest CreateSendRequest()
        {
            var requestUriString = this.entityAddress.AbsoluteUri + "/messages";
            var wq = (HttpWebRequest) WebRequest.Create(requestUriString);
            wq.ProtocolVersion = HttpVersion.Version11;
            wq.KeepAlive = true;
            if (wq.RequestUri.Scheme == "https")
            {
                wq.Headers.Add("Authorization", this.tokenProvider.GetToken(this.entityAddress));
            }
            return wq;
        }

        HttpWebRequest CreateReceiveRequest(TimeSpan timeout)
        {
            var wq = (HttpWebRequest) WebRequest.Create(this.entityAddress.AbsoluteUri + "/messages/head?timeout=" + (timeout.Ticks/TimeSpan.TicksPerSecond));
            wq.ProtocolVersion = HttpVersion.Version11;
            wq.KeepAlive = true;
            if (wq.RequestUri.Scheme == "https")
            {
                wq.Headers.Add("Authorization", this.tokenProvider.GetToken(this.entityAddress));
            }
            return wq;
        }

        HttpWebRequest CreateLockRequest(string method, Uri lockUri)
        {
            var wq = (HttpWebRequest) WebRequest.Create(lockUri);
            wq.ProtocolVersion = HttpVersion.Version11;
            wq.KeepAlive = true;
            wq.Method = method;
            if (wq.RequestUri.Scheme == "https")
            {
                wq.Headers.Add("Authorization", this.tokenProvider.GetToken(this.entityAddress));
            }
            return wq;
        }
    }
}