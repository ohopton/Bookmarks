using Microsoft.AspNetCore.Hosting;

namespace Bookmarks.Api
{
    public class Program
    {
        // ReSharper disable once UnusedMember.Global
        // ReSharper disable once UnusedParameter.Global
        public static void Main(string[] args)
        {
            IWebHost host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
