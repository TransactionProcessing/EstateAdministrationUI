using System;
using System.Collections.Generic;
using System.Text;

namespace EstateAdministrationUI.IntegrationTests.Common
{
    using System.Data;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Ductus.FluentDocker.Builders;
    using Ductus.FluentDocker.Common;
    using Ductus.FluentDocker.Model.Builders;
    using Ductus.FluentDocker.Services;
    using Ductus.FluentDocker.Services.Extensions;
    using EstateManagement.Client;
    using Microsoft.Data.SqlClient;
    using SecurityService.Client;
    using Shared.Logger;
    using TransactionProcessor.Client;
    using ILogger = Shared.Logger.ILogger;

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

        protected String EstateReportingContainerName;

        protected String SubscriptionServiceContainerName;

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

        /// <summary>
        /// The logger
        /// </summary>
        private readonly NlogLogger Logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DockerHelper"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public DockerHelper(NlogLogger logger)
        {
            this.Logger = logger;
            this.Containers = new List<IContainerService>();
            this.TestNetworks = new List<INetworkService>();
        }

        #endregion

        #region Methods

        public Int32 EstateManagementUIPort;

        protected String SecurityServiceContainerName;

        protected String EstateManagementContainerName;
        protected String EstateManagementUiContainerName;

        protected String EventStoreContainerName;

        /// <summary>
        /// Starts the containers for scenario run.
        /// </summary>
        /// <param name="scenarioName">Name of the scenario.</param>
        public override async Task StartContainersForScenarioRun(String scenarioName)
        {
            String traceFolder = FdOs.IsWindows() ? $"D:\\home\\txnproc\\trace\\{scenarioName}" : $"//home//txnproc//trace//{scenarioName}";

            Logging.Enabled();

            Guid testGuid = Guid.NewGuid();
            this.TestId = testGuid;

            this.Logger.LogInformation($"Test Id is {testGuid}");

            // Setup the container names
            this.SecurityServiceContainerName = $"sferguson.ddns.net";
            this.EstateManagementContainerName = $"estate{testGuid:N}";
            this.EstateReportingContainerName = $"estatereporting{testGuid:N}";
            this.SubscriptionServiceContainerName = $"subscription{testGuid:N}";
            this.EstateManagementUiContainerName = $"estateadministrationui{testGuid:N}";
            this.EventStoreContainerName = $"eventstore{testGuid:N}";

            (String, String, String) dockerCredentials = ("https://www.docker.com", "stuartferguson", "Sc0tland");

            INetworkService testNetwork = DockerHelper.SetupTestNetwork();
            this.TestNetworks.Add(testNetwork);
            IContainerService eventStoreContainer =
                DockerHelper.SetupEventStoreContainer(this.EventStoreContainerName, this.Logger, "eventstore/eventstore:release-5.0.2", testNetwork, traceFolder);

            List<String> estateManagementVariables = new List<String>();
            estateManagementVariables.Add($"SecurityConfiguration:ApiName=estateManagement{this.TestId.ToString("N")}");
            estateManagementVariables.Add($"EstateRoleName=Estate{this.TestId.ToString("N")}");
            estateManagementVariables.Add($"MerchantRoleName=Merchant{this.TestId.ToString("N")}");
            estateManagementVariables.Add($"SecurityConfiguration:ApiName=estateManagement{this.TestId.ToString("N")}");

            IContainerService estateManagementContainer = DockerHelper.SetupEstateManagementContainer(this.EstateManagementContainerName,
                                                                                                      this.Logger,
                                                                                                      "stuartferguson/estatemanagement",
                                                                                                      new List<INetworkService>
                                                                                                      {
                                                                                                          testNetwork,
                                                                                                          Setup.DatabaseServerNetwork
                                                                                                      },
                                                                                                      traceFolder,
                                                                                                      dockerCredentials,
                                                                                                      this.SecurityServiceContainerName,
                                                                                                      this.EventStoreContainerName,
                                                                                                      (Setup.SqlServerContainerName,
                                                                                                      Setup.SqlUserName,
                                                                                                      Setup.SqlPassword),
                                                                                                      ("serviceClient", "Secret1"),
                                                                                                      securityServicePort:55001,
                                                                                                      additionalEnvironmentVariables:estateManagementVariables,
                                                                                                      forceLatestImage:true);

            IContainerService estateReportingContainer = DockerHelper.SetupEstateReportingContainer(this.EstateReportingContainerName,
                                                                                                    this.Logger,
                                                                                                    "stuartferguson/estatereporting",
                                                                                                    new List<INetworkService>
                                                                                                    {
                                                                                                        testNetwork,
                                                                                                        Setup.DatabaseServerNetwork
                                                                                                    },
                                                                                                    traceFolder,
                                                                                                    dockerCredentials,
                                                                                                    this.SecurityServiceContainerName,
                                                                                                    (Setup.SqlServerContainerName,
                                                                                                    Setup.SqlUserName,
                                                                                                    Setup.SqlPassword),
                                                                                                    ("serviceClient", "Secret1"),
                                                                                                    true);
            
            IContainerService estateManagementUiContainer = SetupEstateManagementUIContainer(this.EstateManagementUiContainerName,
                                                                                             this.Logger,
                                                                                             "estateadministrationui",
                                                                                             new List<INetworkService>
                                                                                             {
                                                                                                 testNetwork
                                                                                             }, 
                                                                                             this.EstateManagementContainerName,
                                                                                             traceFolder,
                                                                                             dockerCredentials,
                                                                                             ($"estateUIClient{this.TestId.ToString("N")}", "Secret1"));

            this.Containers.AddRange(new List<IContainerService>
                                     {
                                         eventStoreContainer,
                                         estateManagementContainer,
                                         estateReportingContainer,
                                         estateManagementUiContainer
                                     });

            // Cache the ports
            this.EstateManagementApiPort = estateManagementContainer.ToHostExposedEndpoint("5000/tcp").Port;
            this.EventStoreHttpPort = eventStoreContainer.ToHostExposedEndpoint("2113/tcp").Port;
            this.EstateManagementUIPort = estateManagementUiContainer.ToHostExposedEndpoint("5004/tcp").Port;

            // Setup the base address resolvers
            String EstateManagementBaseAddressResolver(String api) => $"http://127.0.0.1:{this.EstateManagementApiPort}";

            HttpClient httpClient = new HttpClient();
            this.EstateClient = new EstateClient(EstateManagementBaseAddressResolver, httpClient);
            Func<String, String> securityServiceBaseAddressResolver = api => $"http://sferguson.ddns.net:55001";
            this.SecurityServiceClient = new SecurityServiceClient(securityServiceBaseAddressResolver, httpClient);

            await PopulateSubscriptionServiceConfiguration().ConfigureAwait(false);

            IContainerService subscriptionServiceContainer = DockerHelper.SetupSubscriptionServiceContainer(this.SubscriptionServiceContainerName,
                                                                                                            this.Logger,
                                                                                                            "stuartferguson/subscriptionservicehost",
                                                                                                            new List<INetworkService>
                                                                                                            {
                                                                                                                testNetwork,
                                                                                                                Setup.DatabaseServerNetwork
                                                                                                            },
                                                                                                            traceFolder,
                                                                                                            dockerCredentials,
                                                                                                            this.SecurityServiceContainerName,
                                                                                                            (Setup.SqlServerContainerName,
                                                                                                            Setup.SqlUserName,
                                                                                                            Setup.SqlPassword),
                                                                                                            this.TestId,
                                                                                                            ("serviceClient", "Secret1"),
                                                                                                            true);

            this.Containers.Add(subscriptionServiceContainer);
        }

        protected async Task PopulateSubscriptionServiceConfiguration()
        {
            String connectionString = Setup.GetLocalConnectionString("SubscriptionServiceConfiguration");

            await using(SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync(CancellationToken.None).ConfigureAwait(false);

                // Create an Event Store Server
                await this.InsertEventStoreServer(connection, this.EventStoreContainerName).ConfigureAwait(false);

                String endPointUri = $"http://{this.EstateReportingContainerName}:5005/api/domainevents";
                // Add Route for Estate Aggregate Events
                await this.InsertSubscription(connection, "$ce-EstateAggregate", "Reporting", endPointUri).ConfigureAwait(false);

                // Add Route for Merchant Aggregate Events
                await this.InsertSubscription(connection, "$ce-MerchantAggregate", "Reporting", endPointUri).ConfigureAwait(false);

                // Add Route for Transaction Aggregate Events
                await this.InsertSubscription(connection, "$ce-TransactionAggregate", "Reporting", endPointUri).ConfigureAwait(false);

                await connection.CloseAsync().ConfigureAwait(false);
            }
        }

        protected async Task CleanUpSubscriptionServiceConfiguration()
        {
            String connectionString = Setup.GetLocalConnectionString("SubscriptionServiceConfiguration");

            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync(CancellationToken.None).ConfigureAwait(false);

                // Delete the Event Store Server
                await this.DeleteEventStoreServer(connection).ConfigureAwait(false);

                // Delete the Subscriptions
                await this.DeleteSubscriptions(connection).ConfigureAwait(false);

                await connection.CloseAsync().ConfigureAwait(false);
            }
        }

        protected async Task InsertEventStoreServer(SqlConnection openConnection, String eventStoreContainerName)
        {
            String esConnectionString = $"ConnectTo=tcp://admin:changeit@{eventStoreContainerName}:{DockerHelper.EventStoreTcpDockerPort};VerboseLogging=true;";
            SqlCommand command = openConnection.CreateCommand();
            command.CommandText = $"INSERT INTO EventStoreServer(EventStoreServerId, ConnectionString,Name) SELECT '{this.TestId}', '{esConnectionString}', 'TestEventStore'";
            command.CommandType = CommandType.Text;
            await command.ExecuteNonQueryAsync(CancellationToken.None).ConfigureAwait(false);
        }

        protected async Task DeleteEventStoreServer(SqlConnection openConnection)
        {
            SqlCommand command = openConnection.CreateCommand();
            command.CommandText = $"DELETE FROM EventStoreServer WHERE EventStoreServerId = '{this.TestId}'";
            command.CommandType = CommandType.Text;
            await command.ExecuteNonQueryAsync(CancellationToken.None).ConfigureAwait(false);
        }

        protected async Task DeleteSubscriptions(SqlConnection openConnection)
        {
            SqlCommand command = openConnection.CreateCommand();
            command.CommandText = $"DELETE FROM Subscription WHERE EventStoreId = '{this.TestId}'";
            command.CommandType = CommandType.Text;
            await command.ExecuteNonQueryAsync(CancellationToken.None).ConfigureAwait(false);
        }

        protected async Task InsertSubscription(SqlConnection openConnection, String streamName, String groupName, String endPointUri)
        {
            SqlCommand command = openConnection.CreateCommand();
            command.CommandText = $"INSERT INTO subscription(SubscriptionId, EventStoreId, StreamName, GroupName, EndPointUri, StreamPosition) SELECT '{Guid.NewGuid()}', '{this.TestId}', '{streamName}', '{groupName}', '{endPointUri}', null";
            command.CommandType = CommandType.Text;
            await command.ExecuteNonQueryAsync(CancellationToken.None).ConfigureAwait(false);
        }

        private IContainerService SetupEstateManagementUIContainer(string containerName, ILogger logger,
                                                         string imageName,
                                                         List<INetworkService> networkServices,
                                                         String estateManagementContainerName,
                                                         string hostFolder,
                                                         (string URL, string UserName, string Password)? dockerCredentials,
                                                         (string clientId, string clientSecret) clientDetails)
        {
            logger.LogInformation("About to Start Estate Management UI Container");
            
            IContainerService containerService = new Builder().UseContainer().WithName(containerName)
                                                              .WithEnvironment($"AppSettings:Authority=http://sferguson.ddns.net:55001",
                                                                               $"AppSettings:ClientId={clientDetails.clientId}",
                                                                               $"AppSettings:ClientSecret={clientDetails.clientSecret}",
                                                                               $"AppSettings:IsIntegrationTest=true",
                                                                               $"EstateManagementScope=estateManagement{this.TestId.ToString("N")}",
                                                                               $"AppSettings:EstateManagementApi=http://{estateManagementContainerName}:{DockerHelper.EstateManagementDockerPort}")
                                                              .UseImage(imageName).ExposePort(5004)
                                                              .UseNetwork(networkServices.ToArray())
                                                              .Mount(hostFolder, "/home", MountType.ReadWrite)
                                                              .Build().Start().WaitForPort("5004/tcp", 30000);

            Console.Out.WriteLine("Started Estate Management UI");

            return containerService;
        }

        /// <summary>
        /// Stops the containers for scenario run.
        /// </summary>
        public override async Task StopContainersForScenarioRun()
        {
            if (this.Containers.Any())
            {
                foreach (IContainerService containerService in this.Containers)
                {
                    containerService.StopOnDispose = true;
                    containerService.RemoveOnDispose = true;
                    containerService.Dispose();
                }
            }

            if (this.TestNetworks.Any())
            {
                foreach (INetworkService networkService in this.TestNetworks)
                {
                    networkService.Stop();
                    networkService.Remove(true);
                }
            }
        }

        #endregion
    }
}
