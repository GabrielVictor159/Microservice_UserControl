
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Infraestructure.Messages
{
    public interface IReceiver : IDisposable
    {
        event EventHandler<MessageReceivedEventArgs> MessageReceived;
        void Initialize(string hostName, string queueName, string exchangeName, string routingKey);
        void StartReceiving<T>();
    }

    public class MessageReceivedEventArgs : EventArgs
    {
        public object? Message { get; set; }
    }
}
