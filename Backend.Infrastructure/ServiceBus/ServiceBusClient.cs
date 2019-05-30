using Backend.Infrastructure.ServiceBus.Contracts;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.ServiceBus
{
    public class ServiceBusClient : IServiceBusClient
    {
        private readonly IQueueClient _queueClient;
        //private readonly ITopicClient _topicClient;
        //private readonly ISubscriptionClient _subscriptionClient;

        public enum MessageProcessResponse { Complete, Abandon, Dead }

        public ServiceBusClient(IQueueClient queueClient)
        {
            _queueClient = queueClient;
            //_subscriptionClient = subscriptionClient;
           // _topicClient = topicClient;
        }

        public async Task SendMessageToQueue<T>(T message, Dictionary<string, object> properties = null)
        {
            string json = JsonConvert.SerializeObject(message);
            Message envelope = new Message(Encoding.UTF8.GetBytes(json)) { Label = message.GetType().FullName };

            if (properties != null)
            {
                foreach (KeyValuePair<string, object> prop in properties)
                {
                    envelope.UserProperties.Add(prop.Key, prop.Value);
                }
            }

            await _queueClient.SendAsync(envelope);
        }

        public async Task ReceiveMessageFromQueue<T>(Func<T, MessageProcessResponse> onProcess)
        {
            MessageHandlerOptions options = new MessageHandlerOptions(e =>
            {
                Trace.TraceError(e.Exception.Message);
                return Task.CompletedTask;
            })
            {
                AutoComplete = false,
                MaxAutoRenewDuration = TimeSpan.FromMinutes(1)
            };

            await Task.Run(() => _queueClient.RegisterMessageHandler(
                 async (message, token) =>
                 {
                     try
                     {
                         string data = Encoding.UTF8.GetString(message.Body);
                         T item = JsonConvert.DeserializeObject<T>(data);

                         MessageProcessResponse result = onProcess(item);

                         switch (result)
                         {
                             case MessageProcessResponse.Complete:
                                 await _queueClient.CompleteAsync(message.SystemProperties.LockToken);
                                 break;
                             case MessageProcessResponse.Abandon:
                                 await _queueClient.AbandonAsync(message.SystemProperties.LockToken);
                                 break;
                             case MessageProcessResponse.Dead:
                                 await _queueClient.DeadLetterAsync(message.SystemProperties.LockToken);
                                 break;
                         }
                     }
                     catch (Exception ex)
                     {
                         await _queueClient.DeadLetterAsync(message.SystemProperties.LockToken);
                         Trace.TraceError(ex.Message);
                     }
                 }, options));
        }


        //public async Task PublishMessageToTopic<T>(T message, Dictionary<string, object> properties = null)
        //{
        //    string json = JsonConvert.SerializeObject(message);
        //    Message envelope = new Message(Encoding.UTF8.GetBytes(json))
        //    {
        //        Label = message.GetType().FullName
        //    };

        //    if (properties != null)
        //    {
        //        foreach (KeyValuePair<string, object> prop in properties)
        //        {
        //            envelope.UserProperties.Add(prop.Key, prop.Value);
        //        }
        //    }

        //    await _topicClient.SendAsync(envelope);
        //}

        //public async Task ReceiveMessageFromTopic<T>(Func<T, MessageProcessResponse> onProcess)
        //{
        //    MessageHandlerOptions options = new MessageHandlerOptions(e =>
        //    {
        //        Trace.TraceError(e.Exception.Message);
        //        return Task.CompletedTask;
        //    })
        //    {
        //        AutoComplete = false,
        //        MaxAutoRenewDuration = TimeSpan.FromMinutes(1)
        //    };

        //    await Task.Run(() => _subscriptionClient.RegisterMessageHandler(
        //        async (message, token) =>
        //        {
        //            try
        //            {
        //                // Get message
        //                string data = Encoding.UTF8.GetString(message.Body);
        //                T item = JsonConvert.DeserializeObject<T>(data);

        //                // Process message
        //                MessageProcessResponse result = onProcess(item);

        //                switch (result)
        //                {
        //                    case MessageProcessResponse.Complete:
        //                        await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
        //                        break;
        //                    case MessageProcessResponse.Abandon:
        //                        await _subscriptionClient.AbandonAsync(message.SystemProperties.LockToken);
        //                        break;
        //                    case MessageProcessResponse.Dead:
        //                        await _subscriptionClient.DeadLetterAsync(message.SystemProperties.LockToken);
        //                        break;
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                await _subscriptionClient.DeadLetterAsync(message.SystemProperties.LockToken);
        //                Trace.TraceError(ex.Message);
        //            }
        //        }, options));
        //}
    }
}
