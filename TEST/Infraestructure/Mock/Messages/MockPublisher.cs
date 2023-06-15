using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Moq;
using API.Infraestructure.Messages;
using RabbitMQ.Client;

namespace TEST.Infraestructure.Mock.Messages
{


    public class MockPublisher : IPublisher
    {
        private Mock<IPublisher> _mock;

        public MockPublisher()
        {
            _mock = new Mock<IPublisher>();
        }

        public void Initialize(string hostname, string exchangeName, string exchangeType = ExchangeType.Fanout)
        {
            _mock.Setup(p => p.Initialize(hostname, exchangeName, exchangeType));
        }

        public void Publish<T>(T data, string routingKey = "")
        {
            _mock.Setup(p => p.Publish(data, routingKey));
        }

        public void Dispose()
        {
            _mock.Setup(p => p.Dispose());
        }

        public IPublisher Object => _mock.Object;
    }

}