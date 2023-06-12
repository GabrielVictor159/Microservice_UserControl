using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers.User;
using API.Controllers.User.DTO;
using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using TEST.API.Builder.DTO;
using Xunit.Frameworks.Autofac;

namespace TEST.API.Test
{
    [UseAutofacTestFramework]
    public class UserControllerTest
    {
        private readonly UserController _controller;
        private readonly Faker _faker;
        public UserControllerTest(UserController controller, Faker faker)
        {
            _controller = controller;
            _faker = faker;
        }

        [Fact]
        public async void LoginTest()
        {
            var dto = LoginDTOBuilder.New(_faker).Build();
            var result1 = await _controller.Login(dto);
            result1.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void RegisterUserTest()
        {
            var dto = RegisterDTOBuilder.New(_faker).Build();
            var result1 = await _controller.RegisterUser(dto);
            result1.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void RegisterUserAdminTest()
        {
            var dto = RegisterDTOBuilder.New(_faker).Build();
            var result1 = await _controller.RegisterUserAdmin(dto);
            result1.Result.Should().BeOfType<OkObjectResult>();
        }
    }
}