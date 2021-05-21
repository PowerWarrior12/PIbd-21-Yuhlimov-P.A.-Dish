using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using DishProjectBusinessLogic.ViewModels;

namespace DishProjectClientApi
{
    public class Program
    {
        public static ClientViewModel Client { get; set; }
        public static void Main(string[] args) =>
        CreateHostBuilder(args).Build().Run();
        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });

    }
}
