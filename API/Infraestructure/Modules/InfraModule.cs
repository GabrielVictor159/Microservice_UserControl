using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Infraestructure.Database;
using API.Infraestructure.Database.Entities;
using API.Infraestructure.Messages;
using Autofac;

namespace API.Infraestructure.Modules
{
    public class InfraModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Context>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<Publisher>().As<IPublisher>().InstancePerLifetimeScope();
            builder.RegisterType<Receiver>().As<IReceiver>().InstancePerLifetimeScope();
        }
    }
}