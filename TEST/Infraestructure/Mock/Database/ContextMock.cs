using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Infraestructure.Database;
using Microsoft.EntityFrameworkCore;

namespace TEST.Infraestructure.Mock.Database
{
    public class ContextMock : Context
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("DatabaseMemory");
        }
    }
}