using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers.User;

namespace TEST.API.Module
{
    using Autofac;
    public class ControllerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserController>().AsSelf().InstancePerLifetimeScope();
        }
    }
}