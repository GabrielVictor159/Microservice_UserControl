using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Infraestructure.Database.Entities;
using Bogus;

namespace TEST.Infraestructure.Builder
{
    public class UserBuilder
    {
        public String Name { get; private set; } = "";
        public String Password { get; private set; } = "";
        public String? Description { get; private set; }
        public String? Image { get; private set; }
        public DateTime? DateOfBirth { get; private set; }
        public Boolean? OnlineStatus { get; private set; }

        public static UserBuilder New(Faker faker)
        {
            return new UserBuilder()
            {
                Name = faker.Name.FullName(),
                Password = faker.Random.String2(10, 14)
            };
        }

        public UserBuilder WithName(String name)
        {
            Name = name;
            return this;
        }

        public UserBuilder WithPassword(String password)
        {
            Password = password;
            return this;
        }

        public UserBuilder WithDescription(String description)
        {
            Description = description;
            return this;
        }

        public UserBuilder WithImage(String image)
        {
            Image = image;
            return this;
        }

        public UserBuilder WithDateOfBirth(DateTime dateOfBirth)
        {
            DateOfBirth = dateOfBirth;
            return this;
        }

        public UserBuilder WithOnlineStatus(Boolean onlineStatus)
        {
            OnlineStatus = onlineStatus;
            return this;
        }

        public User Build()
        => new User()
        {
            Name = Name,
            Password = Password,
            Description = Description,
            Image = Image,
            DateOfBirth = DateOfBirth,
            OnlineStatus = OnlineStatus
        };

    }
}