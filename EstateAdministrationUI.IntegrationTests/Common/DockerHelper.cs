using System;
using System.Collections.Generic;
using System.Text;

namespace EstateAdministrationUI.IntegrationTests.Common
{
    using System.Data;
    using System.Diagnostics.Eventing.Reader;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Ductus.FluentDocker;
    using Ductus.FluentDocker.Builders;
    using Ductus.FluentDocker.Commands;
    using Ductus.FluentDocker.Common;
    using Ductus.FluentDocker.Executors;
    using Ductus.FluentDocker.Extensions;
    using Ductus.FluentDocker.Model.Builders;
    using Ductus.FluentDocker.Model.Containers;
    using Ductus.FluentDocker.Model.Networks;
    using Ductus.FluentDocker.Services;
    using Ductus.FluentDocker.Services.Extensions;
    using EstateManagement.Client;
    using EventStore.Client;
    using Microsoft.Data.SqlClient;
    using SecurityService.Client;
    using Shared.IntegrationTesting;
    using Shared.Logger;
    using TransactionProcessor.Client;
    using ILogger = Shared.Logger.ILogger;

    public enum DockerEnginePlatform
    {
        Linux,
        Windows
    }

    public class DockerHelper : global::Shared.IntegrationTesting.DockerHelper
    {
        #region Fields

        /// <summary>
        /// The estate client
        /// </summary>
        public IEstateClient EstateClient;

        /// <summary>
        /// The HTTP client
        /// </summary>
        public HttpClient HttpClient;

        /// <summary>
        /// The security service client
        /// </summary>
        public ISecurityServiceClient SecurityServiceClient;

        /// <summary>
        /// The test identifier
        /// </summary>
        public Guid TestId;
        
        /// <summary>
        /// The containers
        /// </summary>
        protected List<IContainerService> Containers;

        /// <summary>
        /// The estate management API port
        /// </summary>
        protected Int32 EstateManagementApiPort;

        /// <summary>
        /// The event store HTTP port
        /// </summary>
        protected Int32 EventStoreHttpPort;

        /// <summary>
        /// The security service port
        /// </summary>
        protected Int32 SecurityServicePort;

        /// <summary>
        /// The test networks
        /// </summary>
        protected List<INetworkService> TestNetworks;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DockerHelper"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public DockerHelper()
        {
            this.TestingContext = new TestingContext();
        }

        #endregion

        #region Methods
        
        public Int32 EstateManagementUiPort;
        
        protected String EstateManagementUiContainerName;

        private readonly TestingContext TestingContext;

        public override void SetupContainerNames() {
            base.SetupContainerNames();
            this.EstateManagementUiContainerName = $"estateadministrationui{this.TestId:N}";
        }

        /// <summary>
        /// Starts the containers for scenario run.
        /// </summary>
        /// <param name="scenarioName">Name of the scenario.</param>
        public override async Task StartContainersForScenarioRun(String scenarioName)
        {
            await base.StartContainersForScenarioRun(scenarioName);

            await this.StartEstateManagementUiContainer(this.TestNetworks);
            
            // Cache the ports
            //this.EstateManagementUiPort = estateManagementUiContainer.ToHostExposedEndpoint("5004/tcp").Port;

            // Setup the base address resolvers
            String EstateManagementBaseAddressResolver(String api) => $"http://127.0.0.1:{this.EstateManagementApiPort}";

            HttpClientHandler clientHandler = new HttpClientHandler
                                              {
                                                  ServerCertificateCustomValidationCallback = (message,
                                                                                               certificate2,
                                                                                               arg3,
                                                                                               arg4) =>             
                                                  {
                                                    return true;
                                                  }

                                              };
            HttpClient httpClient = new HttpClient(clientHandler);
            this.EstateClient = new EstateClient(EstateManagementBaseAddressResolver, httpClient);
            Func<String, String> securityServiceBaseAddressResolver = api => $"https://sferguson.ddns.net:55001";
            this.SecurityServiceClient = new SecurityServiceClient(securityServiceBaseAddressResolver, httpClient);
        }
        
        private async Task<IContainerService> StartEstateManagementUiContainer(List<INetworkService> networkServices)
        {
            Trace("About to Start Estate Management UI Container");

            List<String> environmentVariables = this.GetCommonEnvironmentVariables(DockerPorts.SecurityServiceDockerPort);
            environmentVariables.Add($"AppSettings:Authority=https://sferguson.ddns.net:55001");
            environmentVariables.Add($"AppSettings:IsIntegrationTest=true");
            environmentVariables.Add($"EstateManagementScope=estateManagement{this.TestId.ToString("N")}");
            ContainerBuilder containerBuilder = new Builder().UseContainer().WithName(this.EstateManagementUiContainerName)
                                                             .UseImageDetails(("estatemanagementui", false))
                                                             .WithEnvironment(environmentVariables.ToArray()).UseNetwork(networkServices.ToArray()).ExposePort(5004)
                                                             .MountHostFolder(this.HostTraceFolder).SetDockerCredentials(this.DockerCredentials);


            IContainerService builtContainer = containerBuilder.Build().Start().WaitForPort("5004/tcp", 30000);

            foreach (INetworkService networkService in networkServices)
            {
                networkService.Attach(builtContainer, false);
            }

            //  Do a health check here
            this.CallbackHandlerPort = builtContainer.ToHostExposedEndpoint($"{DockerPorts.CallbackHandlerDockerPort}/tcp").Port;

            Trace("Estate Management UI Started");

            //await Retry.For(async () => {
                                //String healthCheck =
                                    //await this.HealthCheckClient.PerformHealthCheck("http", "127.0.0.1", 5004, CancellationToken.None);

                                //var result = JsonConvert.DeserializeObject<HealthCheckResult>(healthCheck);
                                //result.Status.ShouldBe(HealthCheckStatus.Healthy.ToString(), $"Service Type: {containerType} Details {healthCheck}");
                            //});

            return builtContainer;
        }

        /// <summary>
        /// Stops the containers for scenario run.
        /// </summary>
        public override async Task StopContainersForScenarioRun()
        {
            await this.RemoveEstateReadModel().ConfigureAwait(false);

            await base.StopContainersForScenarioRun();
        }

        private async Task RemoveEstateReadModel()
        {
            //List<Guid> estateIdList = this.TestingContext.GetAllEstateIds();

            //foreach (Guid estateId in estateIdList)
            //{
            //    String databaseName = $"EstateReportingReadModel{estateId}";

            //    // Build the connection string (to master)
            //    String connectionString = Setup.GetLocalConnectionString(databaseName);
            //    await Retry.For(async () =>
            //                    {
            //                        EstateReportingSqlServerContext context = new EstateReportingSqlServerContext(connectionString);
            //                        await context.Database.EnsureDeletedAsync(CancellationToken.None);
            //                    },
            //                    retryFor: TimeSpan.FromMinutes(2),
            //                    retryInterval: TimeSpan.FromSeconds(30));
            //}
        }

        #endregion
    }
}
