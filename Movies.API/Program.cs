using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Movies.API.Data;
using Movies.API.Data.Movies.API.Data;

namespace Movies.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            SeedDataBase(host);
            host.Run();
           // CreateHostBuilder(args).Build().Run();
        }

        private static void SeedDataBase(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var service = scope.ServiceProvider;
            var moviesContext = service.GetRequiredService<MoviesApiContext>();
            MoviesContextSeed.SeedAsync(moviesContext);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
