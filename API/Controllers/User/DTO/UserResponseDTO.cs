using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.User.DTO
{
    public class UserResponseDTO
    {
        public Guid Id { get; set; }
        public String Name { get; set; } = "";
        public String? Description { get; set; }
        public String? Image { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Boolean? OnlineStatus { get; set; }
        public String? Role { get; set; }
    }
}