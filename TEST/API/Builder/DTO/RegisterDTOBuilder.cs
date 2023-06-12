using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers.User.DTO;
using Bogus;

namespace TEST.API.Builder.DTO
{
    public class RegisterDTOBuilder
    {
        public String Name { get; private set; } = "";
        public String Password { get; private set; } = "";
        public static RegisterDTOBuilder New(Faker faker)
        {
            return new RegisterDTOBuilder()
            {
                Name = faker.Name.FullName(),
                Password = faker.Random.String2(10, 14)
            };
        }
        public RegisterDTOBuilder WithName(string name)
        {
            Name = name;
            return this;
        }

        public RegisterDTOBuilder WithPassword(string password)
        {
            Password = password;
            return this;
        }

        public RegisterDTO Build()
        => new RegisterDTO() { Name = Name, Password = Password };


    }
}