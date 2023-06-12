using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Infraestructure.Database;


namespace TEST.Infraestructure.Module
{
    using Autofac;
    using TEST.Infraestructure.Mock.Database;

    public class InfraModuleMock : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ContextMock>().As<Context>().InstancePerLifetimeScope();
        }
    }
}