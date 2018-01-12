[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ReportCreator.WebUI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(ReportCreator.WebUI.App_Start.NinjectWebCommon), "Stop")]

namespace ReportCreator.WebUI.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using System.Data.Entity;
    using Ninject.Extensions.Conventions;
    using Ninject.Web.Common.WebHost;
    using ReportCreator.Domain.Entities;
    using ReportCreator.Repository.Common;
    using ReportCreator.Repository.Interfaces;
    using ReportCreator.Repository.Repo;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                ///System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = kernel.Get<System.Web.Http.Dependencies.IDependencyResolver>();
                RegisterServices(kernel);

                kernel.Bind(x => x.FromAssembliesMatching("ReportCreator.BLL.dll")
                    .SelectAllClasses()
                    .BindAllInterfaces()
                );


                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<DbContext>().To<RCContext>().InRequestScope();

            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            kernel.Bind<IGenericRepository<Payment>>().To<PaymentRepository>().InRequestScope();
            kernel.Bind<IGenericRepository<Expenditure>>().To<ExpenditureRepository>().InRequestScope();
            kernel.Bind<IGenericRepository<Category>>().To<CategoryRepository>().InRequestScope();

        }        
    }
}
