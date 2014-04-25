WebAPIContrib
=============

Community Contributions for ASP.NET Web API

WebApiContrib.Tracing.Slab
=============================

**[Visit the project website][project-website] for more information and documentation.**

[project-website]: http://damienbod.wordpress.com/2014/04/10/web-api-tracing-with-slab-and-elasticsearch/

Here's how you can activate tracing with slab in your Web API config

```csharp
	config.EnableSystemDiagnosticsTracing();
    config.Services.Replace(typeof(ITraceWriter), new SlabTraceWriter());
```

Here's how you could use the Slab Action Filter for request, response logging
  // Here you can log a resquest/response  messages.
 

```csharp
    [SlabLoggingFilterAttribute]
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }

```

Here's how you can log all unhandled exceptions to your slab log.

```csharp
    // Do this if you want to log all unhandled exceptions
    config.Services.Add(typeof(IExceptionLogger), new SlabLoggingExceptionLogger());
```

Here's how you can configure your own EventSource for tracing.

```csharp
	// Example using an example EventSource
    var exectueLogDict = new Dictionary<TraceLevel, Action<string>>();
    WebApiTracingCustom.RegisterLogger(exectueLogDict);
    config.EnableSystemDiagnosticsTracing();
    config.Services.Replace(typeof(ITraceWriter), new SlabTraceWriter(exectueLogDict));
```

and the EventSource

```csharp
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Web.Http.Tracing;

namespace WebApiContrib.Tracing.Slab.Demo
{
    [EventSource(Name = "WebApiTracing")]
    public class WebApiTracingCustom : EventSource
    {
        private const int TraceLevelFatal = 401;
        private const int TraceLevelError = 402;
        private const int TraceLevelInformational = 403;
        private const int TraceLevelLogAlways = 404;
        private const int TraceLevelVerbose = 405;
        private const int TraceLevelWarning = 406;

        public static void RegisterLogger(Dictionary<TraceLevel, Action<string>> exectueLogDict)
        {
            exectueLogDict.Add(TraceLevel.Info, Log.Informational);
            exectueLogDict.Add(TraceLevel.Debug, Log.Verbose);
            exectueLogDict.Add(TraceLevel.Error, Log.Error);
            exectueLogDict.Add(TraceLevel.Fatal, Log.Critical);
            exectueLogDict.Add(TraceLevel.Warn, Log.Warning);
        }

        public static readonly WebApiTracingCustom Log = new WebApiTracingCustom();

        [Event(TraceLevelFatal, Message = "TraceLevel.Fatal{0}", Level = EventLevel.Critical)]
        public void Critical(string message)
        {
            if (IsEnabled())
            {
                WriteEvent(TraceLevelFatal, message);
            }          
        }

        [Event(TraceLevelError, Message = "TraceLevel.Error{0}", Level = EventLevel.Error)]
        public void Error(string message)
        {
            if (IsEnabled())
            {
                WriteEvent(TraceLevelError, message);
            }
        }

        [Event(TraceLevelInformational, Message = "TraceLevel.Info{0}", Level = EventLevel.Informational)]
        public void Informational(string message)
        {
            if (IsEnabled())
            {
                WriteEvent(TraceLevelInformational, message);
            }
        }

        [Event(TraceLevelLogAlways, Message = "WebApiTracing LogAlways{0}", Level = EventLevel.LogAlways)]
        public void LogAlways(string message)
        {
            if (IsEnabled())
            {
                WriteEvent(TraceLevelLogAlways, message);
            }
        }

        [Event(TraceLevelVerbose, Message = "TraceLevel.Debug{0}", Level = EventLevel.Verbose)]
        public void Verbose(string message)
        {
            if (IsEnabled())
            {
                WriteEvent(TraceLevelVerbose, message);
            }
        }

        [Event(TraceLevelWarning, Message = "TraceLevel.Warn{0}", Level = EventLevel.Warning)]
        public void Warning(string message)
        {
            if (IsEnabled())
            {
                WriteEvent(TraceLevelWarning, message);
            }
        }
    }
}

```