using System;
using System.Net;
using dotenv.net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Service2
{
    class App
    {


        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
        }

        public static void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<CalculatorService>();
            });
        }

        static void Main(string[] args)
        {
            DotEnv.Load();
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();
            int port = 9090;
            var portstr = config["PORT"];
            if (!String.IsNullOrEmpty(portstr))
            {
                port = int.Parse(portstr);
            }
            var builder = Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(builder =>
            {
                builder
                .ConfigureKestrel(opts =>
                {
                    opts.Listen(IPAddress.Any, port, listenOptions =>
                    {
                        listenOptions.Protocols = HttpProtocols.Http2;
                    });
                })
                .UseConfiguration(config)
                .UseStartup<App>();
            });
            var host = builder.Build();
            host.Run();
        }
    }
}
