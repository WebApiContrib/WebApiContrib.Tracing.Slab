using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Formatters;

namespace WebApiContrib.Tracing.Slab.Tests
{
    public sealed class  TestInMemorySink : IObserver<EventEntry>
    {
        private List<EventEntry>  _testResults;
        public TestInMemorySink(List<EventEntry> testResults)
        {
            _testResults = testResults;
        }
        
        public void OnNext(EventEntry entry)
        {
            if (entry != null)
            {
                _testResults.Add(entry);
            }
        }
 
        public void OnCompleted()
        {
        }
 
        public void OnError(Exception error)
        {
        } 
    }

    public static class ElasticsearchSinkExtensions
    {
        public static SinkSubscription<TestInMemorySink> LogToTestInMemorySink(this IObservable<EventEntry> eventStream, List<EventEntry> testResult)
        {
            var sink = new TestInMemorySink(testResult);

            var subscription = eventStream.Subscribe(sink);

            return new SinkSubscription<TestInMemorySink>(subscription, sink);
        }
    }
}
