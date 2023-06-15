using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain.User;
using API.Infraestructure.Database;
using API.Infraestructure.Database.Entities;
using API.Repository.User;
using AutoMapper;
using Bogus;
using FluentAssertions;
using TEST.Domain.User.Builder;
using TEST.Infraestructure.Builder;
using Xunit.Frameworks.Autofac;

namespace TEST.Repository.Test
{
    [UseAutofacTestFramework]
    public class UserRepositoryTest
    {
        private readonly Faker _faker;
        private readonly Context _context;
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public UserRepositoryTest(Faker faker, Context context, IUserRepository repository, IMapper mapper)
        {
            _faker = faker;
            _context = context;
            _repository = repository;
            _mapper = mapper;
        }

        [Fact]
        public async void RegisterAsync()
        {
            var user = UserBuilder.New(_faker).Build();
            await _repository.RegisterAsync(user);
            var result = await _context.Users.FindAsync(user.Id);
            result.Should().NotBeNull();
        }
        [Fact]
        public async void LoginAsync()
        {
            var user = UserBuilder.New(_faker).Build();
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            var result = await _repository.LoginAsync(user.Name, user.Password);
            result.Should().NotBeNull();
        }
        [Fact]
        public async void GetOneById()
        {
            var user = UserBuilder.New(_faker).Build();
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            var result1 = await _repository.GetOneById(user.Id);
            result1.Should().NotBeNull();
            var result2 = await _repository.GetOneById(Guid.NewGuid());
            result2.Should().BeNull();
        }
        [Fact]
        public async void GetPaginatedAsync()
        {
            var user = UserBuilder.New(_faker).Build();
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            var result = await _repository.GetPaginatedAsync();
            result.Should().NotBeNullOrEmpty();
        }
        [Fact]
        public async void DeleteAsync()
        {
            var user = UserBuilder.New(_faker).Build();
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            var result = await _repository.DeleteAsync(user.Id);
            result.Should().BeTrue();
        }
    }
}