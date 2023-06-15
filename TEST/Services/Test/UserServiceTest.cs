using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers.User.DTO;
using API.Domain.User;
using API.Infraestructure.Database;
using API.Infraestructure.Database.Entities;
using API.Services.User;
using AutoMapper;
using Bogus;
using FluentAssertions;
using Newtonsoft.Json;
using TEST.Domain.User.Builder;
using TEST.Infraestructure.Builder;
using Xunit.Frameworks.Autofac;

namespace TEST.Services.Test
{
    [UseAutofacTestFramework]
    public class UserServiceTest
    {
        private readonly Faker _faker;
        private readonly Context _context;
        private readonly IUserService _service;
        private readonly IMapper _mapper;
        private UserMap? userObject;
        private int nameLength;
        int passwordLength;
        public UserServiceTest(Faker faker, Context context, IUserService service, IMapper mapper)
        {
            _faker = faker;
            _context = context;
            _service = service;
            _mapper = mapper;
            var user = Environment.GetEnvironmentVariable("USER");
            userObject = user != null ? JsonConvert.DeserializeObject<UserMap>(user) : null;
            nameLength = userObject?.Name != null ? int.Parse(userObject.Name) : 8;
            passwordLength = userObject?.Password != null ? int.Parse(userObject.Password) : 8;
        }
        [Fact]
        public async void RegisterUser_Sucess()
        {
            var userDomain = UserDomainBuilder.New(_faker).Build();
            var result = await _service.RegisterUser(userDomain);
            result.Should().BeOfType<UserResponseDTO>();
        }
        [Fact]
        public async void RegisterUser_Failure()
        {
            var userDomain = UserDomainBuilder.New(_faker).WithName(_faker.Random.String2(nameLength - 3, nameLength - 2)).Build();
            var result = await _service.RegisterUser(userDomain);
            result.Should().BeOfType<string>();
        }
        [Fact]
        public async void Login_Sucess()
        {
            String Password = _faker.Random.String2(passwordLength, passwordLength + 4);
            var userDomain = UserDomainBuilder.New(_faker).WithPassword(Password).Build();
            var user = _mapper.Map<Users>(userDomain);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            LoginDTO dto = new LoginDTO() { Name = user.Name, Password = Password };
            var result = await _service.Login(dto);
            result.Should().NotBeNull();
        }
        [Fact]
        public async void Login_Failure()
        {
            var userDomain = UserDomainBuilder.New(_faker).Build();
            var user = _mapper.Map<Users>(userDomain);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            LoginDTO dto = new LoginDTO() { Name = user.Name, Password = _faker.Random.String2(passwordLength, passwordLength + 4) };
            var result = await _service.Login(dto);
            result.Should().BeNull();
        }
        [Fact]
        public async void GetPaginated()
        {
            var user = UserBuilder.New(_faker).Build();
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            GetPaginationDTO dto = new GetPaginationDTO();
            var result1 = await _service.GetPaginated(dto);
            result1.Should().BeOfType<List<UserResponseDTO>>();
            if (result1 is List<UserResponseDTO> r)
            {
                r.Should().NotBeNullOrEmpty();
            }
            dto.searchUser = new Users() { Name = user.Name };
            var result2 = await _service.GetPaginated(dto);
            result2.Should().BeOfType<List<UserResponseDTO>>();
            if (result2 is List<UserResponseDTO> a)
            {
                a.Should().NotBeNullOrEmpty();
            }
            dto.searchUser = new Users() { Name = _faker.Random.String2(nameLength, nameLength + 4) };
            var result3 = await _service.GetPaginated(dto);
            result3.Should().BeOfType<List<UserResponseDTO>>();
            if (result3 is List<UserResponseDTO> z)
            {
                z.Should().BeNullOrEmpty();
            }
            dto.pageIndex = 0;
            var result4 = await _service.GetPaginated(dto);
            result4.Should().BeOfType<string>();
        }
        [Fact]
        public async void Update_Sucess()
        {
            var user = UserBuilder.New(_faker).Build();
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            UserUpdateDTO dto = new UserUpdateDTO();
            var userNewProperties = UserBuilder.New(_faker).Build();
            _mapper.Map(userNewProperties, dto);
            dto.Id = user.Id;
            var result = await _service.Update(dto);
            result.Should().BeOfType<UserResponseDTO>();
            var userResult = await _context.Users.FindAsync(user.Id);
            userResult?.Name.Should().Be(dto.Name);
        }
        [Fact]
        public async void Update_Failure()
        {
            var user = UserBuilder.New(_faker).Build();
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            UserUpdateDTO dto = new UserUpdateDTO();
            var userNewProperties = UserBuilder.New(_faker).Build();
            _mapper.Map(userNewProperties, dto);
            var result1 = await _service.Update(dto);
            result1.Should().BeOfType<string>();
            dto.Name = _faker.Random.String2(nameLength - 3, nameLength - 2);
            dto.Id = user.Id;
            var result2 = await _service.Update(dto);
            result2.Should().BeOfType<string>();
        }
        [Fact]
        public async void Delete()
        {
            var user = UserBuilder.New(_faker).Build();
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            var result1 = await _service.Delete(Guid.NewGuid());
            result1.Should().BeFalse();
            var result2 = await _service.Delete(user.Id);
            result2.Should().BeTrue();
        }
    }
}