using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.User.DTO
{
    public class LoginDTO
    {
        public String? Name { get; set; }
        public String? Password { get; set; }
    }
}