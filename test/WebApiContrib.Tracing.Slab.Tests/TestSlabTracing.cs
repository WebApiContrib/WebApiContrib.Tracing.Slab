using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Tracing;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using NUnit.Framework;

namespace WebApiContrib.Tracing.Slab.Tests
{
    public class TestSlabTracing
    {
        private static CancellationTokenSource _cts;
        private static HttpMessageInvoker _client;

        private static readonly HttpConfiguration _config = new HttpConfiguration();
        private readonly List<EventEntry> _testResults = new List<EventEntry>();

        [SetUp]
        public void SetupTest()
        {
            var listener = new ObservableEventListener();
            listener.EnableEvents(WebApiTracing.Log, EventLevel.LogAlways, Keywords.All);

            listener.LogToConsole();
            listener.LogToTestInMemorySink(_testResults);

            //WebApiTracing.Log.Critical("Hello world In-Process Critical");

            _cts = new CancellationTokenSource();

            _config.Routes.MapHttpRoute("DefaultApi", "{controller}/{id}", new { id = RouteParameter.Optional });
            _config.Services.Replace(typeof(ITraceWriter), new SlabTraceWriter());
            var server = new HttpServer(_config);
            _client = new HttpMessageInvoker(server);
        }

        [TearDown]
        public void TearDownTest()
        {

        }

        [Test]
        public void TestTracingGet()
        {
            _testResults.Clear();
            var _ = _client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "http://example.org/test"), _cts.Token).Result;
            Assert.IsTrue(_testResults.Any());
            Assert.IsTrue(_testResults.Any(t => t.FormattedMessage.Contains("TraceLevel.Info GET")));
        }

        [Test]
        public void TestTracingPost()
        {
            _testResults.Clear();
            var _ = _client.SendAsync(new HttpRequestMessage(HttpMethod.Post, "http://example.org/test"), _cts.Token).Result;
            Assert.IsTrue(_testResults.Any());
            Assert.IsTrue(_testResults.Any(t => t.FormattedMessage.Contains("TraceLevel.Info POST")));
        }
    }
}
