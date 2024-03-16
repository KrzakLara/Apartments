using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace RWA_Projekt
{
    public static class RouteConfig
    {
        //its making a route for any .aspx page
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;
            routes.EnableFriendlyUrls(settings);
        }

        internal static void RegisterRoutes()
        {
            throw new NotImplementedException();
        }
    }
}
