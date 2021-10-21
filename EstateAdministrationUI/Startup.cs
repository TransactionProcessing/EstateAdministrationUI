namespace EstateAdministrationUI
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IdentityModel.Tokens.Jwt;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;
    using BusinessLogic.Factories;
    using EstateManagement.Client;
    using EstateReporting.Client;
    using Factories;
    using FileProcessor.Client;
    using HealthChecks.UI.Client;
    using IdentityModel;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Logging;
    using Microsoft.IdentityModel.Tokens;
    using NLog.Extensions.Logging;
    using Services;
    using Shared.Extensions;
    using Shared.General;
    using Shared.Logger;
    using TokenManagement;

    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private static IWebHostEnvironment WebHostEnvironment;

        public static IConfigurationRoot Configuration { get; set; }

        public Startup(IWebHostEnvironment webHostEnvironment)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(webHostEnvironment.ContentRootPath)
                                                                      .AddJsonFile("/home/txnproc/config/appsettings.json", true, true)
                                                                      .AddJsonFile($"/home/txnproc/config/appsettings.{webHostEnvironment.EnvironmentName}.json", optional: true)
                                                                      .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                                                      .AddJsonFile($"appsettings.{webHostEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                                                                      .AddEnvironmentVariables();

            Startup.Configuration = builder.Build();
            Startup.WebHostEnvironment = webHostEnvironment;

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigurationReader.Initialise(Startup.Configuration);

            services.AddHealthChecks()
                    .AddUrlGroup(new Uri($"{ConfigurationReader.GetValue("AppSettings", "Authority")}/health"),
                                 name: "Security Service",
                                 httpMethod: HttpMethod.Get,
                                 failureStatus: HealthStatus.Unhealthy,
                                 tags: new string[] { "security", "authorisation" })
                .AddUrlGroup(new Uri($"{ConfigurationReader.GetValue("AppSettings", "EstateManagementApi")}/health"),
                             name: "Estate Management Service",
                             httpMethod: HttpMethod.Get,
                             failureStatus: HealthStatus.Unhealthy,
                             tags: new string[] { "application", "estatemanagement" })
                .AddUrlGroup(new Uri($"{ConfigurationReader.GetValue("AppSettings", "EstateReportingApi")}/health"),
                             name: "Estate Reporting Service",
                             httpMethod: HttpMethod.Get,
                             failureStatus: HealthStatus.Unhealthy,
                             tags: new string[] { "application", "estatereporting" });

            services.AddControllersWithViews();

            services.AddRazorPages().AddRazorRuntimeCompilation();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
                    .AddCookie("Cookies")
                    .AddOpenIdConnect("oidc", options =>
                    {
                        HttpClientHandler handler = new HttpClientHandler();
                        handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                        options.BackchannelHttpHandler = handler;

                        options.Authority = ConfigurationReader.GetValue("Authority");
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateAudience = false,
                            NameClaimType = JwtClaimTypes.Name,
                            RoleClaimType = JwtClaimTypes.Role,
                        };

                        options.ClientSecret =
                            ConfigurationReader.GetValue("ClientSecret");
                        options.ClientId = ConfigurationReader.GetValue("ClientId");

                        options.MetadataAddress = $"{ConfigurationReader.GetValue("Authority")}/.well-known/openid-configuration";

                        options.ResponseType = "code id_token";

                        options.Scope.Clear();
                        options.Scope.Add("openid");
                        options.Scope.Add("profile");
                        options.Scope.Add("email");
                        options.Scope.Add("offline_access");

                                                                              String? estateManagementScope =
                                                                                  Environment.GetEnvironmentVariable("EstateManagementScope");
                        options.Scope.Add(String.IsNullOrEmpty(estateManagementScope) ? "estateManagement" : estateManagementScope);

                        options.ClaimActions.MapAllExcept("iss",
                                                          "nbf",
                                                          "exp",
                                                          "aud",
                                                          "nonce",
                                                          "iat",
                                                          "c_hash");

                        options.GetClaimsFromUserInfoEndpoint = true;
                        options.SaveTokens = true;

                        options.Events.OnRedirectToIdentityProvider = context =>
                        {
                            // Intercept the redirection so the browser navigates to the right URL in your host
                            context.ProtocolMessage.IssuerAddress = $"{ConfigurationReader.GetValue("Authority")}/connect/authorize";
                            return Task.CompletedTask;
                        };
                    });

            services.AddSingleton<IApiClient, ApiClient>();
            services.AddSingleton<IModelFactory, ModelFactory>();
            services.AddSingleton<IViewModelFactory, ViewModelFactory>();
            services.AddSingleton<IEstateClient, EstateClient>();
            services.AddSingleton<IFileProcessorClient, FileProcessorClient>();
            services.AddSingleton<IEstateReportingClient, EstateReportingClient>();
            services.AddSingleton<Func<String, String>>(container => (serviceName) =>
            {
                return ConfigurationReader.GetBaseServerUri(serviceName).OriginalString;
            });
            services.AddSingleton<HttpClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
                              ILoggerFactory loggerFactory)
        {
            String nlogConfigFilename = "nlog.config";
            if (string.Compare(Startup.WebHostEnvironment.EnvironmentName, "Development", true) == 0)
            {
                nlogConfigFilename = $"nlog.{Startup.WebHostEnvironment.EnvironmentName}.config";
            }

            loggerFactory.ConfigureNLog(Path.Combine(Startup.WebHostEnvironment.ContentRootPath, nlogConfigFilename));
            loggerFactory.AddNLog();

            Microsoft.Extensions.Logging.ILogger logger = loggerFactory.CreateLogger("EstateAdministrationUI");

            Logger.Initialise(logger);

            Action<String> loggerAction = message =>
                                          {
                                              Logger.LogInformation(message);
                                          };
            Startup.Configuration.LogConfiguration(loggerAction);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
                             {
                                 endpoints.MapAreaControllerRoute("Account", "Account", "Account/{controller=Home}/{action=Index}/{id?}");
                                 endpoints.MapAreaControllerRoute("Estate", "Estate", "Estate/{controller=Home}/{action=Index}/{id?}");
                                 endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
                                 endpoints.MapHealthChecks("health", new HealthCheckOptions()
                                                                     {
                                                                         Predicate = _ => true,
                                                                         ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                                                                     });


                             });


        }
    }
}
