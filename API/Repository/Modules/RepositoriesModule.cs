using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Repository.User;
using Autofac;

namespace API.Repository.Modules
{
    public class RepositoriesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
        }
    }
}