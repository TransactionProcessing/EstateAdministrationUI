namespace EstateAdministrationUI
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Security.Cryptography.X509Certificates;
    using Lamar.Microsoft.DependencyInjection;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Server.Kestrel.Core;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Shared.Logger;

    [ExcludeFromCodeCoverage]
    public class Program
    {
        #region Methods

        public static IHostBuilder CreateHostBuilder(String[] args){
            IHostBuilder hostBuilder = null;
            using (StreamWriter sw = new StreamWriter("C:\\Users\\runneradmin\\txnproc\\trace\\debugging.log")){
                //At this stage, we only need our hosting file for ip and ports
                FileInfo fi = new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);

                IConfigurationRoot config = new ConfigurationBuilder().SetBasePath(fi.Directory.FullName).AddJsonFile("hosting.json", optional:true)
                                                                      .AddJsonFile("hosting.development.json", optional:true).AddEnvironmentVariables().Build();

                hostBuilder = Host.CreateDefaultBuilder(args);
                hostBuilder.UseWindowsService();
                hostBuilder.UseLamar();
                hostBuilder.ConfigureWebHostDefaults(webBuilder => {
                                                         webBuilder.UseStartup<Startup>();
                                                         webBuilder.ConfigureServices(services => {
                                                                                          // This is important, the call to AddControllers()
                                                                                          // cannot be made before the usage of ConfigureWebHostDefaults
                                                                                          services.AddControllersWithViews().AddNewtonsoftJson(options => {
                                                                                                                                                   options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                                                                                                                                                   options.SerializerSettings.TypeNameHandling = TypeNameHandling.Auto;
                                                                                                                                                   options.SerializerSettings.Formatting = Formatting.Indented;
                                                                                                                                                   options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                                                                                                                                                   options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                                                                                                                                               });
                                                                                          services.AddRazorPages().AddRazorRuntimeCompilation();
                                                                                      });
                                                         webBuilder.UseConfiguration(config);
                                                         webBuilder.UseKestrel(options => {
                                                                                   var port = 5004;
                                                                                    sw.WriteLine("About to listen");
                                                                                    try{
                                                                                        options.Listen(IPAddress.Any,
                                                                                                       port,
                                                                                                       listenOptions => {
                                                                                                           // Enable support for HTTP1 and HTTP2 (required if you want to host gRPC endpoints)
                                                                                                           listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                                                                                                           // Configure Kestrel to use a certificate from a local .PFX file for hosting HTTPS
                                                                                                           listenOptions.UseHttps(Program.LoadCertificate(sw,fi.Directory.FullName));
                                                                                                       });
                                                                                    }
                                                                                    catch(Exception ex){
                                                                                        sw.WriteLine(ex.ToString());
                                                                                    }
                                                                                });
                                                     });
            }

            return hostBuilder;
        }

        public static void Main(String[] args)
        {
            Program.CreateHostBuilder(args).Build().Run();
        }

        private static X509Certificate2 LoadCertificate(StreamWriter sw, String path)
        {
            //just to ensure that we are picking the right file! little bit of ugly code:
            var files = Directory.GetFiles(path);
            var certificateFile = files.First(name => name.Contains("pfx"));
            sw.WriteLine(certificateFile);
            return new X509Certificate2(certificateFile, "password");
        }

        #endregion
    }
}