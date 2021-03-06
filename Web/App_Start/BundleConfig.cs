﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;


namespace Notifications.Web
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/signalR").Include("~/Scripts/jquery.signalR-*"));
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-1.*", "~/Scripts/json2.*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

        }
    }
}