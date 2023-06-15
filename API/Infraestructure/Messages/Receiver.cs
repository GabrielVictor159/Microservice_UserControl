using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace API.Infraestructure.Messages
{
    public class Receiver : IReceiver
    {
        private string? _hostName;
        private string? _queueName;
        private string? _exchangeName;
        private string? _routingKey;
        private IConnection? _connection;
        private IModel? _channel;

        public event EventHandler<MessageReceivedEventArgs>? MessageReceived;

        public void Initialize(string hostName, string queueName, string exchangeName, string routingKey)
        {
            _hostName = hostName;
            _queueName = queueName;
            _exchangeName = exchangeName;
            _routingKey = routingKey;

            var factory = new ConnectionFactory() { HostName = _hostName };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void StartReceiving<T>()
        {
            _channel?.QueueDeclare(queue: _queueName,
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);

            _channel.QueueBind(queue: _queueName,
                               exchange: _exchangeName,
                               routingKey: _routingKey);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(message);

                var eventArgs = new MessageReceivedEventArgs { Message = data! };
                MessageReceived?.Invoke(this, eventArgs);
            };

            _channel.BasicConsume(queue: _queueName,
                                  autoAck: true,
                                  consumer: consumer);
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}
