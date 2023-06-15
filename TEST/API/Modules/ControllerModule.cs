using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers.User;
using Autofac;
namespace TEST.API.Modules
{
    public class ControllerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserController>().AsSelf().InstancePerLifetimeScope();
        }
    }
}