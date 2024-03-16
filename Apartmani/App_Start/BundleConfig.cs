using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Apartmani.App_Start
{
    public class BundleConfig
    {
        //kad se aplikacija pokrece zbildaj mi sve cssove
        //Bundle => pomoc ce nam u loadanju podataka
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content").Include(
                "~/Content/bootstrap.min.css"));
            
            bundles.Add(new Bundle("~/Scripts").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/jquery-3.6.0.js"
                ));
            
        }
    }
}