using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EstateAdministrationUI
{
    using System.Diagnostics.CodeAnalysis;
    using System.IdentityModel.Tokens.Jwt;
    using System.IO;
    using System.Net.Http;
    using BusinessLogic.Factories;
    using EstateManagement.Client;
    using Factories;
    using HealthChecks.UI.Client;
    using IdentityModel;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Logging;
    using Microsoft.IdentityModel.Tokens;
    using NLog.Extensions.Logging;
    using Services;
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
                                                                      .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                                                      .AddJsonFile($"appsettings.{webHostEnvironment.EnvironmentName}.json", optional: true).AddEnvironmentVariables();

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
                             tags: new string[] { "application", "estatemanagement" });

            services.AddControllersWithViews();

            services.Configure<CookiePolicyOptions>(options =>
                                                    {
                                                        // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                                                        options.CheckConsentNeeded = context => true;
                                                        options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
                                                        options.ConsentCookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                                                    });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = "oidc";
            }).AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.Cookie.Name = "mvchybridautorefresh";
                options.Cookie.SameSite = SameSiteMode.Unspecified;
                options.Cookie.IsEssential = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            }).AddAutomaticTokenManagement().AddOpenIdConnect("oidc",
                                                              options =>
                                                              {
                                                                  options.SignInScheme = "Cookies";
                                                                  options.Authority = ConfigurationReader.GetValue("Authority");

                                                                  options.RequireHttpsMetadata = false;

                                                                  options.ClientSecret =
                                                                      ConfigurationReader.GetValue("ClientSecret");
                                                                  options.ClientId = ConfigurationReader.GetValue("ClientId");

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

                                                                  options.TokenValidationParameters = new TokenValidationParameters
                                                                                                      {
                                                                                                          NameClaimType = JwtClaimTypes.Name,
                                                                                                          RoleClaimType = JwtClaimTypes.Role,
                                                                                                          ValidateIssuer = false
                                                                                                      };
                                                              });
            IdentityModelEventSource.ShowPII = true;

            services.AddSingleton<IApiClient, ApiClient>();
            services.AddSingleton<IModelFactory, ModelFactory>();
            services.AddSingleton<IViewModelFactory, ViewModelFactory>();
            services.AddSingleton<IEstateClient, EstateClient>();
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
