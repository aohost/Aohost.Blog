using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Aohost.Blog.HttpApi.Hosting
{
    public class Program
    {
        public static int Main(string[] args)
        {
                CreateHostBuilder(args).Build().Run();
                return 0;
        }

        internal static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseAutofac();
                //.UseSerilog();
    }
}
