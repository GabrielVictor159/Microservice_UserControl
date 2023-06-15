using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace API.Infraestructure.Messages
{
    public class Publisher : IPublisher
    {
        private IConnection? _connection;
        private IModel? _channel;
        private string? _exchangeName;
        public Publisher()
        {}
        public void Initialize(string hostname, string exchangeName, string exchangeType = ExchangeType.Fanout)
        {
            var factory = new ConnectionFactory() { HostName = hostname };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _exchangeName = exchangeName;

            _channel.ExchangeDeclare(exchangeName, exchangeType);
        }

        public void Publish<T>(T data, string routingKey = "")
        {
            var message = SerializeData(data);
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: _exchangeName,
                                  routingKey: routingKey,
                                  basicProperties: null,
                                  body: body);
        }

        private string SerializeData<T>(T data)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(data);
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}
