using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Socotra.VinLookup;
public class Program {
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                  .UseUrls()
                  .ConfigureKestrel(serverOptions =>
                  {
                    if (!int.TryParse(Environment.GetEnvironmentVariable("SMP_PORT"), out int port))
                          port = 10101;

                    serverOptions.ListenAnyIP(port);
                  })
                  .UseStartup<Startup>();
            });
}
