using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain.User;
using API.Enums;
using Bogus;
using Newtonsoft.Json;

namespace TEST.Domain.User.Builder
{
    public class UserDomainBuilder
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public String Name { get; private set; } = "";
        public String Password { get; private set; } = "";
        public String? Description { get; private set; }
        public Byte[]? Image { get; private set; }
        public DateTime? DateOfBirth { get; private set; }
        public Boolean? OnlineStatus { get; private set; }
        public String Role { get; private set; } = "";
        public static UserDomainBuilder New(Faker faker)
        {
            var user = Environment.GetEnvironmentVariable("USER");
            var userObject = user != null ? JsonConvert.DeserializeObject<UserMap>(user) : null;
            int nameLength = userObject?.Name != null ? int.Parse(userObject.Name) : 8;
            int passwordLength = userObject?.Password != null ? int.Parse(userObject.Password) : 8;

            var userDomainBuilder = new UserDomainBuilder()
            {
                Name = faker.Random.String2(nameLength, nameLength + 4),
                Password = faker.Random.String2(passwordLength, passwordLength + 4),
                Role = Roles.USER.ToString()
            };

            if (userObject != null)
            {
                if (userObject.Description)
                {
                    userDomainBuilder.Description = faker.Random.String2(8);
                }
                if (userObject.Image)
                {
                    userDomainBuilder.Image = faker.Random.Bytes(10);
                }
                if (userObject.DateOfBirth)
                {
                    userDomainBuilder.DateOfBirth = faker.Date.Between(DateTime.Now.AddYears(-18), DateTime.Now);
                }
                if (userObject.OnlineStatus)
                {
                    userDomainBuilder.OnlineStatus = faker.Random.Bool();
                }
            }

            return userDomainBuilder;
        }

        public UserDomainBuilder WithId(Guid id)
        {
            Id = id;
            return this;
        }
        public UserDomainBuilder WithName(String name)
        {
            Name = name;
            return this;
        }

        public UserDomainBuilder WithPassword(String password)
        {
            Password = password;
            return this;
        }

        public UserDomainBuilder WithDescription(String description)
        {
            Description = description;
            return this;
        }

        public UserDomainBuilder WithImage(Byte[] image)
        {
            Image = image;
            return this;
        }

        public UserDomainBuilder WithDateOfBirth(DateTime dateOfBirth)
        {
            DateOfBirth = dateOfBirth;
            return this;
        }

        public UserDomainBuilder WithOnlineStatus(Boolean onlineStatus)
        {
            OnlineStatus = onlineStatus;
            return this;
        }
        public UserDomainBuilder WithRole(String role)
        {
            Role = role;
            return this;
        }

        public UserDomain Build()
        => new UserDomain()
        {
            Id = Id,
            Name = Name,
            Password = Password,
            Description = Description,
            Image = Image,
            DateOfBirth = DateOfBirth,
            OnlineStatus = OnlineStatus,
            Role = Role
        };

    }
}