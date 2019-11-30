using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using ShallowLibAPI.Models;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ShallowLibAPI
{
    
    public class Program
    {
        
        public static void Main(string[] args)
        {
        //  CreateWebHostBuilder(args).Build().Run();

            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                try
                {
                    var context = service.GetRequiredService<Database.DatabaseContext>();
                    context.Database.Migrate();
                    DBinitializer.Seed(context);
                }
                catch (Exception ex)
                {
                    ///terh
                }
            }
            host.Run();

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
