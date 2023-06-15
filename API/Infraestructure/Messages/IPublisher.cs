using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace API.Infraestructure.Messages
{
   public interface IPublisher : IDisposable
    {
    void Initialize(string hostname, string exchangeName, string exchangeType = ExchangeType.Fanout);
     void Publish<T>(T data, string routingKey = "");
    }

}