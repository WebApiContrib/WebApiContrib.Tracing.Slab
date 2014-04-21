using System.Web.Http.ExceptionHandling;

namespace WebApiContrib.Tracing.Slab
{
    public class SlabLoggingExceptionLogger: ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            WebApiTracing.Log.SlabUnhandledExceptionLogger(string.Format("{0}, {1}, {2}, {3}", 
                context.Request.Method, context.Request.RequestUri, 
                context.Exception.Message, context.Exception.StackTrace));
        }
    }
}
