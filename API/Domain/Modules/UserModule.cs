using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers.User.DTO;
using API.Domain.User;
using API.Infraestructure.Database.Entities;
using Autofac;
using AutoMapper;
namespace API.Domain.Modules
{
    public class UserModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDomain, Users>().ReverseMap();
                cfg.CreateMap<UserDomain, LoginDTO>().ReverseMap();
                cfg.CreateMap<UserDomain, RegisterDTO>().ReverseMap();
                cfg.CreateMap<UserDomain, UserUpdateDTO>().ReverseMap();
                cfg.CreateMap<Users, UserUpdateDTO>().ReverseMap();
                cfg.CreateMap<Users, UserResponseDTO>().ReverseMap();
            }));
            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve)).As<IMapper>().InstancePerLifetimeScope();
        }
    }
}