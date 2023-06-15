using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain.Functions;

namespace API.Domain.User
{
    public class UserDomain
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public String Name { get; set; } = "";
        private String _Password = "";
        public int PasswordLength { get; set; } = 0;
        public String Password
        {
            get => _Password;
            set { PasswordLength = value.Length; _Password = Cryptography.md5Hash(value); }
        }
        public String? Description { get; set; }
        public String? Image { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Boolean? OnlineStatus { get; set; } = false;
        public String Role { get; set; } = "";
    }

}