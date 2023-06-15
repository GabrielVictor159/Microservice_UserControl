using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Enums;
using FluentValidation;
using Newtonsoft.Json;

namespace API.Domain.User
{
    public class UserDomainValidation : AbstractValidator<UserDomain>
    {
        public UserDomainValidation()
        {
            var user = Environment.GetEnvironmentVariable("USER");
            var userObject = user != null ? JsonConvert.DeserializeObject<UserMap>(user) : null;
            int NameLength = userObject?.Name != null ? int.Parse(userObject.Name) : 8;
            int PasswordLength = userObject?.Password != null ? int.Parse(userObject.Password) : 8;
            RuleFor(e => e.Id)
            .NotNull()
            .WithMessage("Id cannot be null");
            RuleFor(e => e.Name)
            .MinimumLength(NameLength)
            .WithMessage($"The Name must be at least {NameLength} characters long");
            RuleFor(e => e.PasswordLength)
            .Must(e => e >= PasswordLength)
            .WithMessage($"The Password must be at least {PasswordLength} characters long");
            RuleFor(e => e.Role)
            .NotNull()
            .WithMessage("Role cannot be null")
            .NotEmpty()
            .WithMessage("Role cannot be empty")
            .Must(role => Enum.TryParse(typeof(Roles), role, out _))
            .WithMessage("Role type not defined");
            if (userObject != null)
            {
                if (!userObject.Description)
                {
                    RuleFor(e => e.Description)
                    .Null()
                    .WithMessage($"The Description option is disabled");
                }
                if (!userObject.Image)
                {
                    RuleFor(e => e.Image)
                    .Null()
                    .WithMessage($"The Image option is disabled");
                }
                if (!userObject.DateOfBirth)
                {
                    RuleFor(e => e.DateOfBirth)
                    .Null()
                    .WithMessage($"The DateOfBirth option is disabled");
                }
                if (!userObject.OnlineStatus)
                {
                    RuleFor(e => e.OnlineStatus)
                    .Null()
                    .WithMessage($"The OnlineStatus option is disabled");
                }
            }
        }
    }
    public class UserMap
    {
        public string Name { get; set; } = "";
        public string Password { get; set; } = "";
        public bool Description { get; set; } = false;
        public bool Image { get; set; } = false;
        public bool DateOfBirth { get; set; } = false;
        public bool OnlineStatus { get; set; } = false;
    }
}