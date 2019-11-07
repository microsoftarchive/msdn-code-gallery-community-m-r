// 
//  (c) Microsoft Corporation. See LICENSE.TXT file for licensing details
//  
namespace Microsoft.ServiceBus.Micro
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Net;

    public class SimpleMessage
    {
        readonly MessagingClient client;
        readonly string messageLockUri;
        public string ContentType;

        public SimpleMessage()
        {
            this.client = null;
            this.messageLockUri = null;
            this.Properties = new Hashtable();
            this.BrokerProperties = new Hashtable();
        }

        internal SimpleMessage(Stream bodyStream, string contentType, WebHeaderCollection headers, MessagingClient client)
        {
            this.BodyStream = bodyStream;
            this.ContentType = contentType;
            this.Properties = new Hashtable();
            this.BrokerProperties = new Hashtable();

            foreach (var header in headers.AllKeys)
            {
                if (header.Equals("Location"))
                {
                    this.messageLockUri = (string) headers[header];
                }
                this.Properties.Add(header, headers[header].Trim('\"'));
            }
            this.client = client;
        }

        public Hashtable BrokerProperties { get; set; }
        public Hashtable Properties { get; set; }
        public Stream BodyStream { get; set; }

        public void Complete()
        {
            if (this.client == null ||
                this.messageLockUri == null)
            {
                throw new InvalidOperationException();
            }
            this.client.Complete(new Uri(this.messageLockUri));
        }

        public void Abandon()
        {
            if (this.client == null ||
                this.messageLockUri == null)
            {
                throw new InvalidOperationException();
            }
            this.client.Abandon(new Uri(this.messageLockUri));
        }
    }
}