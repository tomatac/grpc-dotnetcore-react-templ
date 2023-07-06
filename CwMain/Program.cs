using GrpcAspNetCore.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddControllersWithViews();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.WebHost.ConfigureKestrel(options =>
{
    // If an HTTP/2 endpoint is configured without TLS, the endpoint's ListenOptions.Protocols must be set to HttpProtocols.Http2.
    // An endpoint with multiple protocols, such as HttpProtocols.Http1AndHttp2 for example, can't be used without TLS because there's no negotiation.
    options.Listen(IPAddress.Parse("192.168.229.128"), 50001, o => o.Protocols = HttpProtocols.Http1);  // running this on an specific IP made it work
    options.Listen(IPAddress.Parse("192.168.229.128"), 50002, o => o.Protocols = HttpProtocols.Http2);  // running this on an specific IP made it work
    // options.Listen(IPAddress.Parse("192.168.229.128"), 50001, o => o.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1AndHttp2);
    //options.Listen(System.Net.IPAddress.Any, 50001, o => o.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1AndHttp2);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}

app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowAll");

app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true }); // Enable gRPC-Web for all services.

app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<GreeterService>();
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}");
});

app.MapFallbackToFile("index.html"); ;

app.Run();
