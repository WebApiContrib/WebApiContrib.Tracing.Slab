using System;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Tracing;
using NUnit.Framework;
using WebApiContrib.Tracing.Log4Net.Tests;

namespace WebApiContrib.Tracing.Slab.Tests
{
	public class SlabTraceWriterTests
	{

		private static CancellationTokenSource _cts;
		private static HttpMessageInvoker _client;

		static SlabTraceWriterTests()
		{

			_cts = new CancellationTokenSource();

			GlobalConfiguration.Configuration.Routes.MapHttpRoute("DefaultApi", "{controller}/{id}", new { id = RouteParameter.Optional });
            GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new SlabTraceWriter());
			var server = new HttpServer(GlobalConfiguration.Configuration);
			_client = new HttpMessageInvoker(server);
		}

		

	}
}
