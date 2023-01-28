using System.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;

namespace PocMiddleware.Middleware;

record PerformanceData(Guid uid, string data, TimeSpan time);

public class PerformanceMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        string tid = context.TraceIdentifier;
        var stopwatch = Stopwatch.StartNew();

        Console.WriteLine($"Enter PerformanceMiddleware ( Before Next() {tid}");
        await next(context);
        Console.WriteLine($"Enter PerformanceMiddleware (After Next() {tid}");

        stopwatch.Stop();
        Console.WriteLine($"Request {context.Request.Path} took {stopwatch.ElapsedMilliseconds} ms");

        // Just for fun add the performance message to the response header
        context.Response.Headers.Add(new KeyValuePair<string, StringValues>("Performance-Message", $"Api Pipeline Executed in {stopwatch.ElapsedMilliseconds}ms"));

        // Optionally write to a Performance DB or send a async message to processor
    }
}