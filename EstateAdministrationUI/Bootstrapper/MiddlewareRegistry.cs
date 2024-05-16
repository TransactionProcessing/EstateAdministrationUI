namespace EstateAdministrationUI.Bootstrapper
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Net.Http;
    using System.Threading.Tasks;
    using IdentityModel;
    using Lamar;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Shared.Extensions;
    using Shared.General;

    [ExcludeFromCodeCoverage]
    public class MiddlewareRegistry :ServiceRegistry
    {
        public MiddlewareRegistry()
        {
            this.AddHealthChecks().AddSecurityService(this.ApiEndpointHttpHandler).AddEstateManagementService();

            this.AddAuthentication(options => {
                                       options.DefaultScheme = "Cookies";
                                       options.DefaultChallengeScheme = "oidc";
                                   })
                .AddCookie("Cookies")
                .AddOpenIdConnect("oidc",
                                  options => {
                                      String authority = ConfigurationReader.GetValue("Authority");
                                      String securityServiceLocalPort = ConfigurationReader.GetValue("SecurityServiceLocalPort");
                                      String securityServicePort = ConfigurationReader.GetValue("SecurityServicePort");

                                      if (String.IsNullOrEmpty(securityServiceLocalPort)){
                                          securityServiceLocalPort = "5001";
                                      }

                                      if (String.IsNullOrEmpty(securityServicePort)){
                                          securityServicePort = "5001";
                                      }

                                      HttpClientHandler handler = new HttpClientHandler();
                                      handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                                      options.BackchannelHttpHandler = handler;

                                      options.Authority = $"{authority}:{securityServiceLocalPort}";
                                      options.TokenValidationParameters = new TokenValidationParameters{
                                                                                                           ValidateAudience = false,
                                                                                                           NameClaimType = JwtClaimTypes.Name,
                                                                                                           RoleClaimType = JwtClaimTypes.Role,
                                                                                                       };

                                      options.ClientSecret =
                                          ConfigurationReader.GetValue("ClientSecret");
                                      options.ClientId = ConfigurationReader.GetValue("ClientId");

                                      options.MetadataAddress = $"{authority}:{securityServiceLocalPort}/.well-known/openid-configuration";

                                      options.ResponseType = "code id_token";

                                      options.Scope.Clear();
                                      options.Scope.Add("openid");
                                      options.Scope.Add("profile");
                                      options.Scope.Add("email");
                                      options.Scope.Add("offline_access");

                                      options.Scope.Add("estateManagement");
                                      options.Scope.Add("fileProcessor");
                                      options.Scope.Add("transactionProcessor");

                                      options.ClaimActions.MapAllExcept("iss",
                                                                        "nbf",
                                                                        "exp",
                                                                        "aud",
                                                                        "nonce",
                                                                        "iat",
                                                                        "c_hash");

                                      options.GetClaimsFromUserInfoEndpoint = true;
                                      options.SaveTokens = true;

                                      options.Events.OnRedirectToIdentityProvider = context => {
                                                                                        // Intercept the redirection so the browser navigates to the right URL in your host
                                                                                        context.ProtocolMessage.IssuerAddress = $"{authority}:{securityServicePort}/connect/authorize";
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
}
