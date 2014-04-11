using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Tracing;

namespace WebApiContrib.Tracing.Slab.DemoApp.WithSignalR
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Example using an example EventSource
            var exectueLogDict = new Dictionary<TraceLevel, Action<string>>();
            WebApiTracingWithSignalRExample.RegisterLogger(exectueLogDict);
            config.EnableSystemDiagnosticsTracing();
            config.Services.Replace(typeof(ITraceWriter), new SlabTraceWriter(exectueLogDict));
        } 
    }
}
