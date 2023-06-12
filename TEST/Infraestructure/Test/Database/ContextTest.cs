using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Infraestructure.Database;
using API.Infraestructure.Database.Entities;
using Bogus;
using FluentAssertions;
using TEST.Infraestructure.Builder;
using Xunit.Frameworks.Autofac;

namespace TEST.Infraestructure.Test.Database
{
    [UseAutofacTestFramework]
    public class ContextTest
    {
        private readonly Context _context;
        private readonly Faker _faker;

        public ContextTest(Context context, Faker faker)
        {
            _context = context;
            _faker = faker;
        }

        [Fact]
        public async void UserTest()
        {
            var dto = UserBuilder.New(_faker).Build();
            await _context.Users.AddAsync(dto);
            await _context.SaveChangesAsync();
            var resultTask = _context.Users.FindAsync(dto.Id);
            var result = await resultTask;
            result.Should().NotBeNull();
            result!.Id.Should().Be(dto.Id);
        }
    }
}