using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Enums;
using API.Infraestructure.Database.Entities;
using Bogus;

namespace TEST.Infraestructure.Builder
{
    public class UserBuilder
    {
        public Guid Id { get; set; }
        public String Name { get; private set; } = "";
        public String Password { get; private set; } = "";
        public String? Description { get; private set; }
        public String? ImageName { get; set; }
        public Byte[]? ImageBlob { get; set; }
        public DateTime? DateOfBirth { get; private set; }
        public Boolean? OnlineStatus { get; private set; }
        public String Role { get; private set; } = "";

        public static UserBuilder New(Faker faker)
        {
            return new UserBuilder()
            {
                Id = Guid.NewGuid(),
                Name = faker.Name.FullName(),
                Password = faker.Random.String2(10, 14),
                Role = Roles.ADMIN.ToString()
            };
        }
        public UserBuilder WithId(Guid id)
        {
            Id = id;
            return this;
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

        public UserBuilder WithImageName(String image)
        {
            ImageName = image;
            return this;
        }
        public UserBuilder WithImageBlob(Byte[] image)
        {
            ImageBlob = image;
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

        public Users Build()
        => new Users()
        {
            Id = Id,
            Name = Name,
            Password = Password,
            Description = Description,
            ImageName = ImageName,
            ImageBlob = ImageBlob,
            DateOfBirth = DateOfBirth,
            OnlineStatus = OnlineStatus,
            Role = Role
        };

    }
}