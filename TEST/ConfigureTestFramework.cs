using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Bogus;
using TEST.API.Module;
using TEST.Infraestructure.Module;
using Xunit.Abstractions;
using Xunit.Frameworks.Autofac;

[assembly: TestFramework("TEST.ConfigureTestFramework", "TEST")]
namespace TEST
{
    public class ConfigureTestFramework : AutofacTestFramework
    {
        public ConfigureTestFramework(IMessageSink diagnosticMessageSink)
           : base(diagnosticMessageSink)
        {
        }
        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterInstance<Faker>(new Faker("pt_BR")).AsSelf().SingleInstance();
            builder.RegisterModule(new ControllerModule());
            builder.RegisterModule(new InfraModuleMock());
        }
    }
}