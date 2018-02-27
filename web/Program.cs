using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace web
{
    /// <summary>
    /// Main entry point.
    /// Starts the whole application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Static main function.
        /// Starts the whole application.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        /// <summary>
        /// Builts the actual web host.
        /// </summary>
        /// <param name="args">Given commandline parameters.</param>
        /// <returns>IWebHost</returns>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseKestrel()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseStartup<Startup>()
            .UseUrls("http://*:5000")
            .Build();
    }
}
