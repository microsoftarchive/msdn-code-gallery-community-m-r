

namespace MyCompany.Common.EventBus
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.ServiceBus;
    using Microsoft.ServiceBus.Messaging;
    using Microsoft.WindowsAzure;
    using MyCompany.Common.CrossCutting;
    using System.Configuration;

    /// <summary>
    /// <see cref="MyCompany.Common.EventBus.IEventBus"/>
    /// </summary>
    public class AzureEventBus : IEventBus
    {
        /// <summary>
        /// Handler Object
        /// </summary>
        protected IHandler Handler = null;

        string _connectionString = string.Empty;
        string _topicName = string.Empty;
        string _subscriptionName = string.Empty;
        string[] _readTopics = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="topicName"></param>
        /// <param name="subscriptionName"></param>
        /// <param name="readTopics"></param>
        public AzureEventBus(string topicName, string subscriptionName, string[] readTopics)
        {
            _connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            _topicName = topicName;
            _subscriptionName = subscriptionName;
            _readTopics = readTopics;

            bool eventBusEnabled = false;
            Boolean.TryParse(ConfigurationManager.AppSettings["EventBusEnabled"], out eventBusEnabled);
            
            if (eventBusEnabled)
                Initialize();
        }

        /// <summary>
        /// <see cref="MyCompany.Common.EventBus.IEventBus"/>
        /// </summary>
        public void Publish<TEvent>(TEvent @event, string contentType)
        {
            if (@event == null)
                throw new ArgumentNullException("@event");


            var topicClient = TopicClient.CreateFromConnectionString(_connectionString, _topicName);
            var brokeredMessage = new BrokeredMessage(@event)
            {
                ContentType = contentType
            };

            topicClient.Send(brokeredMessage);
        }

        /// <summary>
        /// <see cref="MyCompany.Common.EventBus.IEventBus"/>
        /// </summary>
        public void RegisterHandler()
        {
            if (_readTopics != null)
            {
                var eventDrivenMessagingOptions = new OnMessageOptions();
                eventDrivenMessagingOptions.AutoComplete = true;
                eventDrivenMessagingOptions.ExceptionReceived += OnExceptionReceived;
                eventDrivenMessagingOptions.MaxConcurrentCalls = 5;

                foreach (var topic in _readTopics)
                {
                    SubscriptionClient client = SubscriptionClient.CreateFromConnectionString(_connectionString, topic, _subscriptionName);
                    client.OnMessage(OnMessageArrived, eventDrivenMessagingOptions);
                }
            }
        }

        /// <summary>
        /// This event will be called each time a message arrives.
        /// </summary>
        /// <param name="message"></param>
        public void OnMessageArrived(BrokeredMessage message)
        {
            if (Handler != null)
                Handler.Handle(new BrokeredMessageEventArgs(message));
        }


        /// <summary>
        /// Event handler for each time an error occurs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnExceptionReceived(object sender, ExceptionReceivedEventArgs e)
        {
            if (e != null && e.Exception != null)
            {
                TraceManager.TraceError(e.Exception);
            }
        }

        /// <summary>
        /// Initialize
        /// </summary>
        public void Initialize()
        {
            var namespaceManager = NamespaceManager.CreateFromConnectionString(_connectionString);

            if (!String.IsNullOrEmpty(_topicName))
            {
                TopicDescription td = new TopicDescription(_topicName);
                td.MaxSizeInMegabytes = 1024;
                td.DefaultMessageTimeToLive = new TimeSpan(2, 0, 0);

                // Create a new topic to send information about expenses
                if (!namespaceManager.TopicExists(_topicName))
                {
                    namespaceManager.CreateTopic(td);
                }
            }

            if (_readTopics != null)
            {
                // Create subscription to read information from Staff Module
                foreach (var topic in _readTopics)
                {
                    if (!namespaceManager.SubscriptionExists(topic, _subscriptionName))
                    {
                        namespaceManager.CreateSubscription(topic, _subscriptionName);
                    }
                }
            }
        }
    }
}
