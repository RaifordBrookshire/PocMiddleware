using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using PocMiddleware.Middleware;

namespace PocMiddleware;

internal class Program
{
    public static void Main(string[] args)
    {
        // Create the App Builder
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.Configure<KestrelServerOptions>(o =>
        {
            o.Limits.MaxConcurrentConnections = 100;
        });

        // Wire Up Your Service Dependencies Here
        builder.Services.AddRouting();
        builder.Services.AddTransient<PerformanceMiddleware>();

        var app = builder.Build();

        // Wire Up Your Middleware Pipeline Here
        app.UseMiddleware<PerformanceMiddleware>();

        // Map your Endpoints Here
        app.MapGet("/", () => "Hello World!");
        app.Map("/test2 ", () => "Hello Test 2!");
       //pp.MapGet("/person/{id:int}", )

        // Run the App... This will block until shutdown.
        app.Run();

        Write("End of Main");
    }


    #region Helper Methods
    static void Write(string output, ConsoleColor color = ConsoleColor.White)
    {
        Console.ForegroundColor = color;
        Console.Write(output);
        Console.ResetColor();
    }
    static void WriteLine(string output, ConsoleColor color = ConsoleColor.DarkGray)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(output);
        Console.ResetColor();
    }
    # endregion
}