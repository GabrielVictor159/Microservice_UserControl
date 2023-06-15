using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Infraestructure.Database;
using API.Infraestructure.Messages;
using Autofac;
using TEST.Infraestructure.Mock.Database;
using TEST.Infraestructure.Mock.Messages;

namespace TEST.Infraestructure
{
    public class InfraModuleMock : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ContextMock>().As<Context>().InstancePerLifetimeScope();
            builder.RegisterType<MockPublisher>().As<IPublisher>().InstancePerLifetimeScope();
        }
    }
}