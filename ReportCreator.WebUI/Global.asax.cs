using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebUI;
using AutoMapper;
using ReportCreator.BLL.Infrastructure;

namespace ReportCreator.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {        
        protected void Application_Start()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MappingDtoProfile>();                
            });
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
