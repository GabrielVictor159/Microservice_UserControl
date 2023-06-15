using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain.User;
using Bogus;
using FluentAssertions;
using Newtonsoft.Json;
using TEST.Domain.User.Builder;
using Xunit.Frameworks.Autofac;

namespace TEST.Domain.User.Test
{
    [UseAutofacTestFramework]
    public class UserDomainValidationTest
    {
        private readonly Faker _faker;
        private UserMap? userObject;
        private int nameLength;
        private int passwordLength;
        public UserDomainValidationTest(Faker faker)
        {
            var user = Environment.GetEnvironmentVariable("USER");
            userObject = user != null ? JsonConvert.DeserializeObject<UserMap>(user) : null;
            nameLength = userObject?.Name != null ? int.Parse(userObject.Name) : 8;
            passwordLength = userObject?.Password != null ? int.Parse(userObject.Password) : 8;
            _faker = faker;
        }

        [Fact]
        public void UserDomain_Sucess()
        {
            var userDomain = UserDomainBuilder.New(_faker).Build();
            var result = new UserDomainValidation().Validate(userDomain);
            result.IsValid.Should().BeTrue();
        }
        [Fact]
        public void UserDomain_Name_Failure()
        {
            var userDomain = UserDomainBuilder.New(_faker).WithName(_faker.Random.String2(nameLength - 3, nameLength - 2)).Build();
            var result = new UserDomainValidation().Validate(userDomain);
            result.IsValid.Should().BeFalse();
        }
        [Fact]
        public void UserDomain_Password_Failure()
        {
            var userDomain = UserDomainBuilder.New(_faker).WithPassword(_faker.Random.String2(passwordLength - 3, passwordLength - 2)).Build();
            var result = new UserDomainValidation().Validate(userDomain);
            result.IsValid.Should().BeFalse();
        }
        [Fact]
        public void UserDomain_Description_Failure()
        {
            if (userObject != null && !userObject.Description)
            {
                var userDomain = UserDomainBuilder.New(_faker).WithDescription(_faker.Random.String2(8, 14)).Build();
                var result = new UserDomainValidation().Validate(userDomain);
                result.IsValid.Should().BeFalse();
            }
        }
        [Fact]
        public void UserDomain_Image_Failure()
        {
            if (userObject != null && !userObject.Image)
            {
                var userDomain = UserDomainBuilder.New(_faker).WithImage(_faker.Random.String2(8, 14)).Build();
                var result = new UserDomainValidation().Validate(userDomain);
                result.IsValid.Should().BeFalse();
            }
        }
        [Fact]
        public void UserDomain_DateOfBirth_Failure()
        {
            if (userObject != null && !userObject.DateOfBirth)
            {
                var userDomain = UserDomainBuilder.New(_faker).WithDateOfBirth(_faker.Date.Between(DateTime.Now.AddYears(-18), DateTime.Now)).Build();
                var result = new UserDomainValidation().Validate(userDomain);
                result.IsValid.Should().BeFalse();
            }
        }
        [Fact]
        public void UserDomain_OnlineStatus_Failure()
        {
            if (userObject != null && !userObject.OnlineStatus)
            {
                var userDomain = UserDomainBuilder.New(_faker).WithOnlineStatus(_faker.Random.Bool()).Build();
                var result = new UserDomainValidation().Validate(userDomain);
                result.IsValid.Should().BeFalse();
            }
        }
        [Fact]
        public void UserDomain_Role_Failure()
        {
            var userDomain1 = UserDomainBuilder.New(_faker).WithRole("").Build();
            var result1 = new UserDomainValidation().Validate(userDomain1);
            result1.IsValid.Should().BeFalse();
            var userDomain2 = UserDomainBuilder.New(_faker).WithRole(_faker.Random.String2(8, 10)).Build();
            var result2 = new UserDomainValidation().Validate(userDomain2);
            result2.IsValid.Should().BeFalse();
        }
    }
}