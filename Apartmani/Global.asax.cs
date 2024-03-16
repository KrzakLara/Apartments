using Apartmani.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Apartmani
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //AutoMapperConfig.Init();
        }
        protected void Application_Error()
        {
            //redirectanje kada se dogodi neulovljeni exception (~=server)
            //Response.Redirect("~/Error/Index?error="+Server?.GetLastError().GetBaseException().Message);

        }

    }
}
