using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Infraestructure.Database.Entities;

namespace API.Services.Token
{
    public interface ITokenService
    {
        string GenerateToken(Users user);
    }
}