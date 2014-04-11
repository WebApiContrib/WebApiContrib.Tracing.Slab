using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebApiContrib.Tracing.Slab.DemoApp.WithSignalR.Startup))]

namespace WebApiContrib.Tracing.Slab.DemoApp.WithSignalR
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
