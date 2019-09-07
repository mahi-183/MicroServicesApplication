// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Mahesh Aurad"/>
// --------------------------------------------------------------------------------------------------------------------
namespace UserMicroservice
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;

    /// <summary>
    /// The default Program class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
           CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Creates the web host builder.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>return the result.</returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
