using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web.Http.Tracing;

namespace WebApiContrib.Tracing.Slab
{
    public class SlabTraceWriter : ITraceWriter
    {
        public SlabTraceWriter()
        {
            RegisterLogger(_exectueLogDict);
        }

        public void Trace(HttpRequestMessage request, string category, TraceLevel level, Action<TraceRecord> traceAction)
        {
            if (level == TraceLevel.Off)
            {
                return;
            }

            var record = new TraceRecord(request, category, level);
            traceAction(record);
            LogToSlab(record);
        }

        private void LogToSlab(TraceRecord record)
        {
            var message = new StringBuilder();
            AddRequestDataToMessage(record, message);
            AddRecordDataToMessage(record, message);

            var result = message.ToString();
            _exectueLogDict[record.Level].Invoke(result);
        }

        private void AddRecordDataToMessage(TraceRecord record, StringBuilder message)
        {
            if (!string.IsNullOrWhiteSpace(record.Category))
                message.Append(" ").Append(record.Category);

            if (!string.IsNullOrWhiteSpace(record.Operator))
                message.Append(" ").Append(record.Operator).Append(" ").Append(record.Operation);

            if (!string.IsNullOrWhiteSpace(record.Message))
                message.Append(" ").Append(record.Message);

            if (record.Exception != null && !string.IsNullOrEmpty(record.Exception.GetBaseException().Message))
                message.Append(" ").AppendLine(record.Exception.GetBaseException().Message);
        }

        private void AddRequestDataToMessage(TraceRecord record, StringBuilder message)
        {
            if (record.Request != null)
            {
                if (record.Request.Method != null)
                {
                    message.Append(" ").Append(record.Request.Method.Method);
                }

                if (record.Request.RequestUri != null)
                {
                    message.Append(" ").Append(record.Request.RequestUri.AbsoluteUri);
                }

                message.Append(" ").Append(record.Request.Headers);
            }
        }

        private readonly Dictionary<TraceLevel, Action<string>> _exectueLogDict = new Dictionary<TraceLevel, Action<string>>();
 
        public void RegisterLogger(Dictionary<TraceLevel, Action<string>> exectueLogDict)
        {
            exectueLogDict.Add(TraceLevel.Info, WebApiTracing.Log.Informational);
            exectueLogDict.Add(TraceLevel.Debug, WebApiTracing.Log.Verbose);
            exectueLogDict.Add(TraceLevel.Error, WebApiTracing.Log.Error);
            exectueLogDict.Add(TraceLevel.Fatal, WebApiTracing.Log.Critical);
            exectueLogDict.Add(TraceLevel.Warn, WebApiTracing.Log.Warning);
        }
    }
}