using System.Diagnostics.Tracing;

namespace WebApiContrib.Tracing.Slab
{
    [EventSource(Name = "WebApiTracing")]
    public class WebApiTracing : EventSource
    {
        private const int TraceLevelFatal = 301;
        private const int TraceLevelError = 302;
        private const int TraceLevelInformational = 303;
        private const int TraceLevelLogAlways = 304;
        private const int TraceLevelVerbose = 305;
        private const int TraceLevelWarning = 306;

        public static readonly WebApiTracing Log = new WebApiTracing();

        [Event(TraceLevelFatal, Message = "TraceLevel.Fatal {0}", Level = EventLevel.Critical)]
        public void Critical(string message)
        {
            if (IsEnabled()) WriteEvent(TraceLevelFatal, message);
        }

        [Event(TraceLevelError, Message = "TraceLevel.Error {0}", Level = EventLevel.Error)]
        public void Error(string message)
        {
            if (IsEnabled()) WriteEvent(TraceLevelError, message);
        }

        [Event(TraceLevelInformational, Message = "TraceLevel.Info {0}", Level = EventLevel.Informational)]
        public void Informational(string message)
        {
            if (IsEnabled()) WriteEvent(TraceLevelInformational, message);
        }

        [Event(TraceLevelLogAlways, Message = "WebApiTracing LogAlways {0}", Level = EventLevel.LogAlways)]
        public void LogAlways(string message)
        {
            if (IsEnabled()) WriteEvent(TraceLevelLogAlways, message);
        }

        [Event(TraceLevelVerbose, Message = "TraceLevel.Debug {0}", Level = EventLevel.Verbose)]
        public void Verbose(string message)
        {
            if (IsEnabled()) WriteEvent(TraceLevelVerbose, message);
        }

        [Event(TraceLevelWarning, Message = "TraceLevel.Warn {0}", Level = EventLevel.Warning)]
        public void Warning(string message)
        {
            if (IsEnabled()) WriteEvent(TraceLevelWarning, message);
        }
    }
}