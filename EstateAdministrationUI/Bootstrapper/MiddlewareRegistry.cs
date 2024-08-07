﻿using Microsoft.Extensions.Logging;

namespace EstateAdministrationUI.Bootstrapper
{
    using System;
    using System.Collections.Generic;
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
    using Shared.Middleware;

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
                                      String securityServiceLocalPort = ConfigurationReader.GetValueOrDefault<String>("AppSettings", "SecurityServiceLocalPort", null);
                                      String securityServicePort = ConfigurationReader.GetValueOrDefault<String>("AppSettings", "SecurityServicePort", null);

                                      (string authorityAddress, string issuerAddress) results = Helpers.GetSecurityServiceAddresses(authority, securityServiceLocalPort, securityServicePort);

                                      HttpClientHandler handler = new HttpClientHandler();
                                      handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                                      options.BackchannelHttpHandler = handler;

                                      options.Authority = results.authorityAddress;
                                      options.TokenValidationParameters = new TokenValidationParameters{
                                                                                                           ValidateAudience = false,
                                                                                                           NameClaimType = JwtClaimTypes.Name,
                                                                                                           RoleClaimType = JwtClaimTypes.Role,
                                                                                                       };

                                      options.ClientSecret =
                                          ConfigurationReader.GetValue("ClientSecret");
                                      options.ClientId = ConfigurationReader.GetValue("ClientId");

                                      options.MetadataAddress = $"{results.authorityAddress}/.well-known/openid-configuration";

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
                                                                                        context.ProtocolMessage.IssuerAddress = $"{results.issuerAddress}/connect/authorize";
                                                                                        return Task.CompletedTask;
                                                                                    };
                                  });

            bool logRequests = ConfigurationReader.GetValueOrDefault<Boolean>("MiddlewareLogging", "LogRequests", true);
            bool logResponses = ConfigurationReader.GetValueOrDefault<Boolean>("MiddlewareLogging", "LogResponses", true);
            LogLevel middlewareLogLevel = ConfigurationReader.GetValueOrDefault<LogLevel>("MiddlewareLogging", "MiddlewareLogLevel", LogLevel.Warning);

            RequestResponseMiddlewareLoggingConfig config =
                new RequestResponseMiddlewareLoggingConfig(middlewareLogLevel, logRequests, logResponses);

            this.AddSingleton(config);
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

    public static class Helpers
    {
        public static (String authorityAddress, String issuerAddress) GetSecurityServiceAddresses(String authority, String securityServiceLocalPort, String securityServicePort)
        {
            Console.WriteLine($"authority is {authority}");
            Console.WriteLine($"securityServiceLocalPort is {securityServiceLocalPort}");
            Console.WriteLine($"securityServicePort is {securityServicePort}");

            if (String.IsNullOrEmpty(securityServiceLocalPort))
            {
                securityServiceLocalPort = "5001";
            }

            if (String.IsNullOrEmpty(securityServicePort))
            {
                securityServicePort = "5001";
            }
            
            Uri u = new Uri(authority);

            var authorityAddress = u.Port switch
            {
                _ when u.Port.ToString() != securityServiceLocalPort => $"{u.Scheme}://{u.Host}:{securityServiceLocalPort}{u.AbsolutePath}",
                _ => authority
            };
            
            var issuerAddress = u.Port switch
            {
                _ when u.Port.ToString() != securityServicePort => $"{u.Scheme}://{u.Host}:{securityServicePort}{u.AbsolutePath}",
                _ => authority
            };

            if (authorityAddress.EndsWith("/"))
            {
                authorityAddress = $"{authorityAddress.Substring(0, authorityAddress.Length - 1)}";
            }
            if (issuerAddress.EndsWith("/"))
            {
                issuerAddress = $"{issuerAddress.Substring(0, issuerAddress.Length - 1)}";
            }

            Console.WriteLine($"authorityAddress is {authorityAddress}");
            Console.WriteLine($"issuerAddress is {issuerAddress}");

            return (authorityAddress, issuerAddress);
        }
    }
}
