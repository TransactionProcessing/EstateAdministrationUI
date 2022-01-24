namespace EstateAdministrationUI.Bootstrapper
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using BusinessLogic.Factories;
    using EstateManagement.Client;
    using EstateReporting.Client;
    using Factories;
    using FileProcessor.Client;
    using IdentityModel;
    using Lamar;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Services;
    using Shared.Extensions;
    using Shared.General;

    public class MiddlewareRegistry :ServiceRegistry
    {
        public MiddlewareRegistry()
        {
            this.AddHealthChecks().AddSecurityService(this.ApiEndpointHttpHandler).AddEstateManagementService().AddEstateReportingService();
            
            this.AddAuthentication(options =>
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
        }

        private HttpClientHandler ApiEndpointHttpHandler(IServiceProvider serviceProvider)
        {
            return new HttpClientHandler
                   {
                       ServerCertificateCustomValidationCallback = (message,
                                                                    cert,
                                                                    chain,
                                                                    errors) =>
                                                                   {
                                                                       return true;
                                                                   }
                   };
        }
    }

    public class ClientRegistry : ServiceRegistry
    {
        public ClientRegistry()
        {
            this.AddSingleton<IApiClient, ApiClient>();
            this.AddSingleton<IEstateClient, EstateClient>();
            this.AddSingleton<IFileProcessorClient, FileProcessorClient>();
            this.AddSingleton<IEstateReportingClient, EstateReportingClient>();
            this.AddSingleton<Func<String, String>>(container => (serviceName) =>
                                                                 {
                                                                     return ConfigurationReader.GetBaseServerUri(serviceName).OriginalString;
                                                                 });
            this.AddSingleton<HttpClient>();
        }
    }

    public class FactoryRegistry : ServiceRegistry
    {
        public FactoryRegistry()
        {
            this.AddSingleton<IModelFactory, ModelFactory>();
            this.AddSingleton<IViewModelFactory, ViewModelFactory>();
        }
    }
}
