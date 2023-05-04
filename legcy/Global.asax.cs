using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace legcy
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            SystemWebAdapterConfiguration.AddSystemWebAdapters(this)
                //.AddProxySupport(options => options.UseForwardedHeaders = true)
                .AddJsonSessionSerializer(options =>
                {
                    // Serialization/deserialization requires each session key to be registered to a type
                    options.RegisterKey<string>("Test");
                    //options.KnownKeys.Add("Test", typeof(string));

                })
                .AddRemoteAppServer(options =>
                {
                    options.ApiKey = "e411badc-f72a-4414-b470-a94eabd7d5a4";
                })
                //.AddAuthenticationServer()
                .AddSessionServer();



        }
        protected void Session_Start(Object sender, EventArgs e)
        {
            HttpContext.Current.Session["Test"] = "3452345325";
            HttpContext.Current.Session["SessionStartTime"] = DateTime.Now;
        }
    }
}