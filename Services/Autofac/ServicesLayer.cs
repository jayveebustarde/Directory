using Autofac;
using Context.Autofac;
using DTO;
using Microsoft.Owin.Security.DataProtection;
using Services.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Autofac
{
    public class ServicesLayer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new DataAccessLayer());

            builder.RegisterType<UserStore>().As<IUserStore>().InstancePerRequest();
            builder.RegisterType<RoleStore>().As<IRoleStore>().InstancePerRequest();
            builder.Register(c => ApplicationUserManager.Create(c.Resolve<IUserStore>(), c.Resolve<IDataProtectionProvider>())).AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationRoleManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();

            builder.RegisterType<ProductService>()
                .As<IBaseService<ProductDTO>>()
                .InstancePerRequest();

            builder.RegisterType<ProductTypeService>()
                .As<IBaseService<ProductTypeDTO>>()
                .InstancePerRequest();

            builder.RegisterType<ProductDiscountService>()
                .As<IBaseService<ProductDiscountDTO>>()
                .InstancePerRequest();

            builder.RegisterType<DiscountService>()
                .As<IBaseService<DiscountDTO>>()
                .InstancePerRequest();

            builder.RegisterType<VariationService>()
                .As<IBaseService<VariationDTO>>()
                .InstancePerRequest();
        }
    }
}
