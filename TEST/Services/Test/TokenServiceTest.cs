using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Infraestructure.Messages;
using API.Services.Token;
using Bogus;
using FluentAssertions;
using TEST.Infraestructure.Builder;
using Xunit.Frameworks.Autofac;

namespace TEST.Services.Test
{
    [UseAutofacTestFramework]
    public class TokenServiceTest
    {
        private readonly ITokenService _tokenService;
        private readonly SecretService _secretService;
        private readonly Faker _faker;
        public TokenServiceTest(ITokenService tokenService, Faker faker)
        {
            _tokenService = tokenService;
            _faker = faker;
            _secretService = new SecretService(new Publisher());
        }

        [Fact]
        public void GenerateToken()
        {
            var user = UserBuilder.New(_faker).Build();
            Environment.SetEnvironmentVariable("Secret", Guid.NewGuid().ToString());
            var result = _tokenService.GenerateToken(user);
            result.Should().BeOfType<string>();
        }
        [Fact]
        public void setSecret()
        {
            _secretService.setSecret(Guid.NewGuid().ToString());
            var result = Environment.GetEnvironmentVariable("Secret");
            result.Should().NotBeNull();
        }
    }
}