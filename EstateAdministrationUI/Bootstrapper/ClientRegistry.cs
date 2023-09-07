namespace EstateAdministrationUI.Bootstrapper;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using Common;
using EstateManagement.Client;
using EstateReportingAPI.Client;
using FileProcessor.Client;
using Lamar;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Shared.General;
using TransactionProcessor.Client;

[ExcludeFromCodeCoverage]
public class ClientRegistry : ServiceRegistry
{
    public ClientRegistry() {
        this.AddSingleton<IConfigurationService, ConfigurationService>();
        this.AddSingleton<IApiClient, ApiClient>();
        this.AddSingleton<IEstateClient, EstateClient>();
        this.AddSingleton<IFileProcessorClient, FileProcessorClient>();
        this.AddSingleton<ITransactionProcessorClient, TransactionProcessorClient>();
        this.AddSingleton<IEstateReportingApiClient, EstateReportingApiClient>();
        this.AddSingleton<Func<String, String>>(container => (serviceName) =>
                                                             {
                                                                 return ConfigurationReader.GetBaseServerUri(serviceName).OriginalString;
                                                             });
        this.AddSingleton<HttpClient>();
    }
}