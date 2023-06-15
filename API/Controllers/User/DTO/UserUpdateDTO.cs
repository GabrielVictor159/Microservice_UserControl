using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.User.DTO
{
    public class UserUpdateDTO
    {
        public Guid Id { get; set; }
        public String? Name { get; set; }
        public String? Password { get; set; }
        public String? Description { get; set; }
        public Byte[]? Image { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public String? Role { get; set; }
    }
}