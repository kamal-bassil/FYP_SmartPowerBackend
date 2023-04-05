using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Scripts
{
    public class Program
    {
        public static void Main(string[] args)
        {
            System.Console.Out.WriteLine("startup exe");
            CreateHostBuilder(args).Build().Run();
            System.Console.Out.WriteLine("shutting down exe");
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>(); });
    }
}