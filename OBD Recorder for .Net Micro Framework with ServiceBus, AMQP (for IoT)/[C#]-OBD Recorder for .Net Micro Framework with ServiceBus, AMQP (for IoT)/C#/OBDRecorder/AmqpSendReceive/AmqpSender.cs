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
using System;
using System.Collections;

#if SB
using Amqp;
using Amqp.Framing;
#endif

#if !MF_FRAMEWORK_VERSION_V4_2
using System.Collections.Generic;
public class Queue : Queue<object>
{
}
#endif

namespace AmqpSendReceive
{
#if SB

    public partial class AmqpSender : AmqpSendReceiveBase
    {
        readonly ILogger Logger;
        readonly string EntityPath;

        public AmqpSender(ILogger logger, string baseUri, string userName, string password, string entityPath) 
            : base(baseUri, userName, password)
        {
            Logger = logger;
            EntityPath = entityPath;
        }

        public AmqpSender(ILogger logger, string connectionString, string entityPath) : base(new Address(connectionString))
        {
            Logger = logger;
            EntityPath = entityPath;
        }

        public AmqpSender(ILogger logger, Address address, string entityPath) : base (address)
        {
            Logger = logger;
            EntityPath = entityPath;
        }

        public AmqpSender(ILogger logger, AmqpSendReceiveBase sessionOwner, string entityPath) : base (sessionOwner)
        {
            Logger = logger;
            EntityPath = entityPath;
        }

        public void StartSender()
        {
            _amqpSender = new SenderLink(this.AmqpSession, "send-link"+this.EntityPath, this.EntityPath);
            _amqpSender.OnClosed += this.SenderLinkClosed;
        }

        Queue AMQPSendQueue = new Queue();
        bool bPendingSend = false;

        SenderLink _amqpSender = null;

        public void SendOrEnqueueMessage(Message message)
        {
            AMQPSendQueue.Enqueue(message);
            if (!this.bPendingSend)
            {
                this.bPendingSend = true;
                SendMessage(message);
            }
        }

        public void SendMessage(Message message)
        {
            if (this._amqpSender == null)
            {
                this.StartSender();
            }
            this._amqpSender.Send((Message)this.AMQPSendQueue.Dequeue(), MessageOutComeCallback, null);
        }

        protected override void ConnectionClosed(AmqpObject sender, Error error)
        {
            Logger.TraceLog("AMQP Connection Closed. Error: " + error.ToString());
            this.AmqpConnection = null;
            this.AmqpSession = null;
            this._amqpSender = null;
            this.bPendingSend = false;
        }
        protected override void SessionClosed(AmqpObject sender, Error error)
        {
            this.AmqpSession = null;
            this._amqpSender = null;
            Logger.TraceLog("AMQP Session Closed. Error: " + error.ToString());
            this.bPendingSend = false;
        }
        void SenderLinkClosed(AmqpObject sender, Error error)
        {
            Logger.TraceLog("AMQP Link Closed. Error: " + error.ToString());
            this._amqpSender = null;
            this.bPendingSend = false;
        }

        void MessageOutComeCallback(Message message, Outcome outcome, object state)
        {
            bool bTraceToSB = (message.Properties.Subject != AmqpSendReceiveBase.strMsgSubjectTrace);
            if (outcome is Accepted)
            {
                Logger.TraceLog("Sent " + message.Properties.Subject + " to SB via AMQP", true, bTraceToSB);
            }
            else
            {
                Logger.TraceLog("Error Sending " + message.Properties.Subject + " to SB via AMQP: " + outcome.ToString(), true, bTraceToSB);
            }
            if (this.AMQPSendQueue.Count > 0)
            {
                var nextMessage = (Message)this.AMQPSendQueue.Dequeue();
                Logger.TraceLog("Sending " + nextMessage.Properties.Subject + " to SB via AMQP. (Output queue length: " + this.AMQPSendQueue.Count + ")", true, (nextMessage.Properties.Subject != AmqpSendReceiveBase.strMsgSubjectTrace));
                this._amqpSender.Send(nextMessage, MessageOutComeCallback, null);
            }
            else
            {
                this.bPendingSend = false;
            }

        }


    }

    

#endif

}
