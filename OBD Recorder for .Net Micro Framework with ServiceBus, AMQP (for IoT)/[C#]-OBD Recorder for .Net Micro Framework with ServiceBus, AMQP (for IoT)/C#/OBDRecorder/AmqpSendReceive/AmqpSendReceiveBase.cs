//  ------------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation
//  All rights reserved. 
//  
//  Licensed under the Apache License, Version 2.0 (the ""License""); you may not use this 
//  file except in compliance with the License. You may obtain a copy of the License at 
//  http://www.apache.org/licenses/LICENSE-2.0  
//  
//  THIS CODE IS PROVIDED *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, 
//  EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR 
//  CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABLITY OR 
//  NON-INFRINGEMENT. 
// 
//  See the Apache Version 2.0 License for specific language governing permissions and 
//  limitations under the License.
//  ------------------------------------------------------------------------------------

using System;

using Amqp;
using Amqp.Framing;

namespace AmqpSendReceive
{
    public interface ILogger
    {
        void TraceLog(string text);
        void TraceLog(string text, bool bShowOnDisplay, bool bSendToServiceBus);
    }
    
    // This base class allows for connection/session recreation on failure, when connections are to be shared across receivers/senders
    public abstract class AmqpSendReceiveBase
    {

        // Specific to this sample (utility functioms)
        public const string strMsgSubjectTrace = "Trace";
        public const string strMsgSubjectMessageLatency = "MessageLatency";
        public const string strPropertyEventTimestamp = "Event-Timestamp";
        public const string strPropertySendTimestamp = "Send-Timestamp";

        // Specific to Service Bus
        public const string strPropertyServiceBusEnqueuedTimestamp = "x-opt-enqueued-time";

        // Specific to Reykjavik
        public const string strPropertyDeviceGatewayDeviceId = "From";

        
        // Remember either the address to use, or who to ask for a new session
        readonly Address Address;
        protected AmqpSendReceiveBase _amqpSessionOwner = null;

        protected AmqpSendReceiveBase(string baseUri, string userName, string password) 
            : this(new Address(baseUri, userName, password))
        {
        }

        protected AmqpSendReceiveBase(Address address)
        {
            this.Address = address;
        }

        protected AmqpSendReceiveBase(AmqpSendReceiveBase owner)
        {
            this._amqpSessionOwner = owner;
        }

        private Session _amqpSession = null;
        public Session AmqpSession
        {
            get
            {
                // Does someone else own the connection? Get it from them
                if (this._amqpSessionOwner != null)
                {
                    return this._amqpSessionOwner.AmqpSession;
                }
                else if (this._amqpSession == null)
                {
                    // Create the session if there is no other owner
                    if (this.AmqpConnection == null)
                    {
                        this.AmqpConnection = new Connection(this.Address);
                        this.AmqpConnection.OnClosed += this.ConnectionClosed;
                    }

                    this._amqpSession = new Session(this.AmqpConnection);
                    this._amqpSession.OnClosed += this.SessionClosed;
                }

                return _amqpSession;
            }
            protected set
            {
                _amqpSession = value;
            }
        }

        public Connection AmqpConnection { get; protected set; }

        protected abstract void ConnectionClosed(AmqpObject sender, Error error);
        protected abstract void SessionClosed(AmqpObject sender, Error error);
    }
}
