using Autofac;
using Autofac.Integration.WebApi;
using Context;
using DTO;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace RestApiProject
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ConfigureAutofac();
        }

        private void ConfigureAutofac()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());


            //builder.RegisterInstance<IBaseService<ProductDTO>>(new ProductService);

            builder.RegisterType<DirectoryUnitOfwork>()
                .As<IUnitOfWork>()
                .InstancePerRequest();

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

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

        }
    }
}
