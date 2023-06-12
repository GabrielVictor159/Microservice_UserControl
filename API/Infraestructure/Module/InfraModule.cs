using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Infraestructure.Database;
namespace API.Infraestructure.Module
{
    using Autofac;
    public class InfraModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Context>().AsSelf().InstancePerLifetimeScope();
        }
    }
}