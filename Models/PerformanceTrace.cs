namespace PocMiddleware.Models;

public class PerformanceTrace
{
    public string  Uid { get; set; }
    public string TraceId { get; set; }
    public string ActivityId { get; set; }
}


/*
 HttpContext.TraceIdentifier
Observability
Instrumentation

OpenTelemetry
 Wc3TraceContext


 RootRequestUid
 Activity  / ActivityIdFormat.W3C
 TraceIdentifier

Logs
Trace
Metrics
Audit
Span
Request
*/