using AzureApp;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using
Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(opts =>
{
    opts.ListenAnyIP(8080, lo =>
    {
        lo.Protocols = HttpProtocols.Http2;
    });
});
builder.Services.AddGrpc();


var app = builder.Build();
app.MapGrpcService<CalculatorService>();

app.Run();
