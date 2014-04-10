using System.Diagnostics.Tracing;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;

namespace WebApiTracingWithSlab
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

            // Only to test in-proc logging
            //var listener = new ObservableEventListener();
            //listener.EnableEvents(WebApiTracing.Log, EventLevel.LogAlways, Keywords.All);

            //listener.LogToFlatFile("test.log");
        }
    }
}
