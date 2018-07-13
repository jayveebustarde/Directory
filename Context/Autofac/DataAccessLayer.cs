using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context.Autofac
{
    public class DataAccessLayer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DirectoryUnitOfwork>().As<IUnitOfWork>().InstancePerRequest();
        }
    }
}
