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

#define SB
#define USEDIAGNOSTICSHELPER
using System;
using System.Collections;

using Amqp;
using Amqp.Framing;

namespace AmqpSendReceive
{
#if SB
    public class AmqpReceiver : AmqpSendReceiveBase
    {
        ILogger Logger;
        readonly string EntityPath;

        public int Credit = 5;
        MessageCallback Callback;

        public AmqpReceiver(ILogger logger, string baseUri, string userName, string password, string entityPath, MessageCallback callback = null, int credit = 5) 
            : base(baseUri, userName, password)
        {
            Logger = logger;
            EntityPath = entityPath;
            Callback = callback;
            Credit = credit;
        }

        public AmqpReceiver(ILogger logger, string connectionString, string entityPath, MessageCallback callback = null, int credit = 5)
            : base(new Address(connectionString))
        {
            Logger = logger;
            EntityPath = entityPath;
            Callback = callback;
            Credit = credit;
        }

        public AmqpReceiver(ILogger logger, Address address, string entityPath, MessageCallback callback = null, int credit = 5)
            : base(address)
        {
            Logger = logger;
            EntityPath = entityPath;
            Callback = callback;
            Credit = credit;
        }

        public AmqpReceiver(ILogger logger, AmqpSendReceiveBase owner, string entityPath, MessageCallback callback = null, int credit = 5)
            : base(owner)
        {
            Logger = logger;
            EntityPath = entityPath;
            Callback = callback;
            Credit = credit;
        }

        private ReceiverLink _amqpReceiver = null;

        public void Start()
        {
#if USEDIAGNOSTICSHELPER
            if (Callback == null)
            {
                Callback = this.OnMessage;
            }
#endif
            _amqpReceiver = new ReceiverLink(this.AmqpSession , "receive-link"+this.EntityPath, this.EntityPath);
            _amqpReceiver.OnClosed += this.ReceiverLinkClosed;
            _amqpReceiver.Start(this.Credit, Callback);
        }

        public void Stop()
        {
            _amqpReceiver.Close();
            _amqpReceiver = null;
        }

        public void AcceptMessage(Message message)
        {
            this._amqpReceiver.Accept(message);
            this._amqpReceiver.SetCredit(this.Credit);
        }

        public void RejectMessage(Message message)
        {
            this._amqpReceiver.Reject(message);
            this._amqpReceiver.SetCredit(this.Credit);
        }

        public void ReleaseMessage(Message message)
        {
            this._amqpReceiver.Release(message);
            this._amqpReceiver.SetCredit(this.Credit);
        }

        protected override void ConnectionClosed(AmqpObject sender, Error error)
        {
            Logger.TraceLog("AMQP Connection Closed. Error: " + error.ToString());
            if (this._amqpReceiver != null)
            {
                this._amqpReceiver.Close();
                this._amqpReceiver = null;
            }
            this.AmqpConnection = null;
        }

        protected override void SessionClosed(AmqpObject sender, Error error)
        {
            Logger.TraceLog("AMQP Session Closed. Error: " + error.ToString());
            if (this._amqpReceiver != null)
            {
                this._amqpReceiver.Close();
                this._amqpReceiver = null;
            }
            this.AmqpSession = null;
        }

        void ReceiverLinkClosed(AmqpObject sender, Error error)
        {
            Logger.TraceLog("AMQP Link Closed. Error: " + error.ToString());
            this._amqpReceiver = null;

            // TODO: Only reconnect on transient/recoverable errors
            if (error != null)
            {
                // TODO - keep retrying (on background thread) if network is down for longe time periods
                this.Start();
            }
        }


#region Diagnostics Helper

#if USEDIAGNOSTICSHELPER

        // These methods are not required, but useful for development/diagnostics:
        // - Default message processor that logs all incoming messages
        // - Method for logging of received messages from your own message processor, optionally including all properties
        // - If the service echos messages back, compute end-to-end message latencies and optionally send latency info back to the service

        public bool LogMessageProperties = false;
        public bool LogMessageLatencies = false;
        public bool SendMessageLatencies = false;
        public AmqpSender SenderForLatencies = null;
        public object DeviceIdForLatencies = "";

        public void OnMessage(ReceiverLink receiver, Message message)
        {
            LogMessage(message);
            AcceptMessage(message);
        }

        public void LogMessage(Message message)
        {
            if (this.LogMessageLatencies)
            {
                DateTime received = DateTime.UtcNow;

                DateTime generated = received;
                var et = message.ApplicationProperties[AmqpSendReceiveBase.strPropertyEventTimestamp];
                if (et != null)
                {
                    generated = (DateTime)et;
                }

                DateTime sent = generated;
                var st = message.ApplicationProperties[AmqpSendReceiveBase.strPropertySendTimestamp];
                if (st != null)
                {
                    sent = (DateTime)st;
                }

                DateTime enqueued = generated;
                var eqt = message.MessageAnnotations[AmqpSendReceiveBase.strPropertyServiceBusEnqueuedTimestamp];
                if (eqt != null)
                {
                    sent = (DateTime)eqt;
                }

                var sendLatency = enqueued - generated;
                var sendToReceiveLatency = received - sent;
                var receiveLatency = received - enqueued;
                var endToEndLatency = received - generated;

                Logger.TraceLog("Received message " + message.Properties.Subject + " - " + generated.ToString() + " Latencies - E2E: " + endToEndLatency.ToString() + " Send to Receive: " + sendToReceiveLatency + " Send: " + sendLatency + " Receive: " + receiveLatency.ToString());

                if (this.SendMessageLatencies && SenderForLatencies != null &&  message.Properties.Subject != AmqpSendReceiveBase.strMsgSubjectMessageLatency)
                {
                    SendLatencyToSb(this.SenderForLatencies, this.DeviceIdForLatencies, message.Properties.Subject, generated, endToEndLatency, sendToReceiveLatency, sendLatency, receiveLatency);
                }
            }
            if (this.LogMessageProperties)
            {
                foreach (DictionaryEntry property in message.ApplicationProperties)
                {
                    Logger.TraceLog("Name: " + property.Key + " Value: " + property.Value.ToString());
                }

                foreach (DictionaryEntry property in message.MessageAnnotations)
                {
                    Logger.TraceLog("Name: " + property.Key + " Value: " + property.Value.ToString());
                }
            }

        }

        public void SendLatencyToSb(AmqpSender sender, object deviceid, string subject, DateTime generated, TimeSpan endToEndLatency, TimeSpan sendToReceiveLatency, TimeSpan sendLatency, TimeSpan receiveLatency)
        {
            Message message = new Message();

            message.Properties = new Amqp.Framing.Properties();
            message.Properties.Subject = AmqpSendReceiveBase.strMsgSubjectMessageLatency;
            message.Properties.GroupId = deviceid.ToString();

            message.ApplicationProperties = new Amqp.Framing.ApplicationProperties();
            message.ApplicationProperties[AmqpSendReceiveBase.strPropertyDeviceGatewayDeviceId] = deviceid;
            message.ApplicationProperties[AmqpSendReceiveBase.strPropertyEventTimestamp] = DateTime.UtcNow;
            message.ApplicationProperties[AmqpSendReceiveBase.strPropertySendTimestamp] = DateTime.UtcNow;

            message.ApplicationProperties["OriginalSubject"] = subject;
            message.ApplicationProperties["Generated"] = generated;
            message.ApplicationProperties["EndToEndLatency"] = endToEndLatency.Ticks / 10000;
            message.ApplicationProperties["SendToReceiveLatency"] = sendToReceiveLatency.Ticks / 10000;
            message.ApplicationProperties["SendLatency"] = sendLatency.Ticks / 10000;
            message.ApplicationProperties["ReceiveLatency"] = receiveLatency.Ticks / 10000;

            sender.SendOrEnqueueMessage(message);
        }
#endif
#endregion

    }
#endif
}
