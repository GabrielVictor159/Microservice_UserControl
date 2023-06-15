using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Infraestructure.Messages;

namespace API.Services.Token
{
    public class SecretService
    {
        private readonly Publisher _publisher;
        public SecretService(Publisher publisher)
        {
            _publisher=publisher;
        }
        public void setSecret(string secret)
        {
             var messageHostname = Environment.GetEnvironmentVariable("messagesHostname");
            if(messageHostname==null)
            {
                throw new InvalidOperationException("messagesHostname environment variable not deified");   
            }
            Environment.SetEnvironmentVariable("Secret", secret);
            _publisher.Initialize(messageHostname,"Secret");
            _publisher.Publish(secret,""); 
        }
    }
}