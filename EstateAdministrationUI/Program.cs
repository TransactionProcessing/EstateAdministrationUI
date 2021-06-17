namespace EstateAdministrationUI
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Security.Cryptography.X509Certificates;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Server.Kestrel.Core;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;

    [ExcludeFromCodeCoverage]
    public class Program
    {
        #region Methods

        public static IHostBuilder CreateHostBuilder(String[] args)
        {
            Console.Title = "Estate Administration UI";

            //At this stage, we only need our hosting file for ip and ports
            IConfigurationRoot config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("hosting.json", optional:true)
                                                                  .AddJsonFile("hosting.development.json", optional:true).AddEnvironmentVariables().Build();

            IHostBuilder hostBuilder = Host.CreateDefaultBuilder(args);
            hostBuilder.ConfigureWebHostDefaults(webBuilder =>
                                                 {
                                                     webBuilder.UseStartup<Startup>();
                                                     webBuilder.UseConfiguration(config);
                                                     webBuilder.UseKestrel(options =>
                                                                           {
                                                                               var port = 5004;

                                                                               options.Listen(IPAddress.Any,
                                                                                              port,
                                                                                              listenOptions =>
                                                                                              {
                                                                                                  // Enable support for HTTP1 and HTTP2 (required if you want to host gRPC endpoints)
                                                                                                  listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                                                                                                  // Configure Kestrel to use a certificate from a local .PFX file for hosting HTTPS
                                                                                                  listenOptions.UseHttps(Program.LoadCertificate());
                                                                                              });
                                                                           });
                                                 });
            return hostBuilder;
        }

        public static void Main(String[] args)
        {
            Program.CreateHostBuilder(args).Build().Run();
        }

        private static X509Certificate2 LoadCertificate()
        {
            //just to ensure that we are picking the right file! little bit of ugly code:
            var files = Directory.GetFiles(Directory.GetCurrentDirectory());
            var certificateFile = files.First(name => name.Contains("pfx"));
            Console.WriteLine(certificateFile);

            return new X509Certificate2(certificateFile, "password");
        }

        #endregion
    }
}