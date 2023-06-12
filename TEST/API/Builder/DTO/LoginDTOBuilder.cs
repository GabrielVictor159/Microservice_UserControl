using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers.User.DTO;
using Bogus;

namespace TEST.API.Builder.DTO
{
    public class LoginDTOBuilder
    {
        public String Name { get; private set; } = "";
        public String Password { get; private set; } = "";
        public static LoginDTOBuilder New(Faker faker)
        {
            return new LoginDTOBuilder()
            {
                Name = faker.Name.FullName(),
                Password = faker.Random.String2(10, 14)
            };
        }
        public LoginDTOBuilder WithName(string name)
        {
            Name = name;
            return this;
        }

        public LoginDTOBuilder WithPassword(string password)
        {
            Password = password;
            return this;
        }

        public LoginDTO Build()
        => new LoginDTO() { Name = Name, Password = Password };


    }
}