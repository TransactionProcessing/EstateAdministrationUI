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


        public static INetworkService SetupTestNetwork(String networkName)
        {
            DockerEnginePlatform engineType = DockerHelper.GetDockerEnginePlatform();
            
            if (engineType == DockerEnginePlatform.Windows)
            {
                var docker = DockerHelper.GetDockerHost();
                var created = docker.CreateNetwork(networkName,
                                     new NetworkCreateParams
                                     {
                                         Driver = "nat",
                                     });
                return created;
            }

            if (engineType == DockerEnginePlatform.Linux)
            {
                // Build a network
                NetworkBuilder networkService = new Builder().UseNetwork(networkName).ReuseIfExist();

                return networkService.Build();
            }

            return null;
        }

        public Int32 EstateManagementUIPort;

        protected String SecurityServiceContainerName;

        protected String EstateManagementContainerName;
        protected String EstateManagementUiContainerName;

        protected String EventStoreContainerName;

        public static IHostService GetDockerHost()
        {
            IList<IHostService> hosts = new Hosts().Discover();
            IHostService docker = hosts.FirstOrDefault(x => x.IsNative) ?? hosts.FirstOrDefault(x => x.Name == "default");
            return docker;
        }

        public static DockerEnginePlatform GetDockerEnginePlatform()
        {
            IHostService docker = DockerHelper.GetDockerHost();

            if (docker.Host.IsLinuxEngine())
            {
                return DockerEnginePlatform.Linux;
            }

            if (docker.Host.IsWindowsEngine())
            {
                return DockerEnginePlatform.Windows;
            }
            throw new Exception("Unknown Engine Type");
        }

        /// <summary>
        /// Starts the containers for scenario run.
        /// </summary>
        /// <param name="scenarioName">Name of the scenario.</param>
        public override async Task StartContainersForScenarioRun(String scenarioName)
        {
            String traceFolder = null;
            if (DockerHelper.GetDockerEnginePlatform() == DockerEnginePlatform.Linux)
            {
                traceFolder = FdOs.IsWindows() ? $"D:\\home\\txnproc\\trace\\{scenarioName}" : $"//home//txnproc//trace//{scenarioName}";
            }

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

            INetworkService testNetwork = DockerHelper.SetupTestNetwork($"testnetwork{this.TestId:N}");
            this.TestNetworks.Add(testNetwork);

            // Setup the docker image names
            String eventStoreImageName = "eventstore/eventstore:20.6.0-buster-slim";
            String estateMangementImageName = "stuartferguson/estatemanagement";
            String estateReportingImageName = "stuartferguson/estatereporting";
            String subscriptionServiceHostImageName = "stuartferguson/subscriptionservicehost";

            DockerEnginePlatform enginePlatform = DockerHelper.GetDockerEnginePlatform();
            if (enginePlatform == DockerEnginePlatform.Windows)
            {
                estateMangementImageName = "stuartferguson/estatemanagementwindows";
                estateReportingImageName = "stuartferguson/estatereportingwindows";
                eventStoreImageName = "stuartferguson/eventstore";
                subscriptionServiceHostImageName = "stuartferguson/subscriptionservicehostwindows";
            }

            IContainerService eventStoreContainer =
                DockerHelper.SetupEventStoreContainer(this.EventStoreContainerName,
                                                      this.Logger,
                                                      eventStoreImageName,
                                                      testNetwork,
                                                      traceFolder,
                                                      usesEventStore2006OrLater:true);

            List<String> estateManagementVariables = new List<String>();
            estateManagementVariables.Add($"SecurityConfiguration:ApiName=estateManagement{this.TestId.ToString("N")}");
            estateManagementVariables.Add($"EstateRoleName=Estate{this.TestId.ToString("N")}");
            estateManagementVariables.Add($"MerchantRoleName=Merchant{this.TestId.ToString("N")}");
            estateManagementVariables.Add($"SecurityConfiguration:ApiName=estateManagement{this.TestId.ToString("N")}");

            IContainerService estateManagementContainer = DockerHelper.SetupEstateManagementContainer(this.EstateManagementContainerName,
                                                                                                      this.Logger,
                                                                                                      estateMangementImageName,
                                                                                                      new List<INetworkService>
                                                                                                      {
                                                                                                          testNetwork,
                                                                                                          Setup.DatabaseServerNetwork
                                                                                                      },
                                                                                                      traceFolder,
                                                                                                      dockerCredentials,
                                                                                                      this.SecurityServiceContainerName,
                                                                                                      this.EventStoreContainerName,
                                                                                                      (Setup.SqlServerContainerName, Setup.SqlUserName,
                                                                                                          Setup.SqlPassword),
                                                                                                      ("serviceClient", "Secret1"),
                                                                                                      securityServicePort:55001,
                                                                                                      additionalEnvironmentVariables:estateManagementVariables,
                                                                                                      forceLatestImage:true);

            IContainerService estateReportingContainer = DockerHelper.SetupEstateReportingContainer(this.EstateReportingContainerName,
                                                                                                    this.Logger,
                                                                                                    estateReportingImageName,
                                                                                                    new List<INetworkService>
                                                                                                    {
                                                                                                        testNetwork,
                                                                                                        Setup.DatabaseServerNetwork
                                                                                                    },
                                                                                                    traceFolder,
                                                                                                    dockerCredentials,
                                                                                                    this.SecurityServiceContainerName,
                                                                                                    (Setup.SqlServerContainerName, Setup.SqlUserName, Setup.SqlPassword),
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

            await LoadEventStoreProjections().ConfigureAwait(false);

            await PopulateSubscriptionServiceConfiguration().ConfigureAwait(false);

            IContainerService subscriptionServiceContainer = DockerHelper.SetupSubscriptionServiceContainer(this.SubscriptionServiceContainerName,
                                                                                                            this.Logger,
                                                                                                            subscriptionServiceHostImageName,
                                                                                                            new List<INetworkService>
                                                                                                            {
                                                                                                                testNetwork,
                                                                                                                Setup.DatabaseServerNetwork
                                                                                                            },
                                                                                                            traceFolder,
                                                                                                            dockerCredentials,
                                                                                                            this.SecurityServiceContainerName,
                                                                                                            ($"{Setup.SqlServerContainerName},1433", Setup.SqlUserName,
                                                                                                                Setup.SqlPassword),
                                                                                                            this.TestId,
                                                                                                            ("serviceClient", "Secret1"),
                                                                                                            true);

            IHostService docker = DockerHelper.GetDockerHost();
            var networkList = docker.GetNetworks();
            foreach (INetworkService networkService in networkList)
            {
                Console.WriteLine(networkService.Id);
                
                var cfg = networkService.GetConfiguration(true);
                Console.WriteLine(cfg.Name);

                foreach (KeyValuePair<String, NetworkedContainer> networkedContainer in cfg.Containers)
                {
                    Console.WriteLine($"{networkedContainer.Key} : {networkedContainer.Value.Name}");
                }
            }

            ConsoleStream<String> logs = subscriptionServiceContainer.Logs(true);
            await Retry.For(async () =>
                            {
                                IList<String> loglines = logs.ReadToEnd();

                                foreach (String logline in loglines)
                                {
                                    Console.WriteLine(logline);
                                }

                                if (loglines.Any(l => l.Contains("] connected on [")) == false)
                                {
                                    // SS is not running
                                    throw new Exception("SS is not running yet");
                                }
                            }, TimeSpan.FromMinutes(2));
        
        //foreach (String logline in loglines)
        //{
        //    Console.WriteLine(logline);
        //}


        this.Containers.Add(subscriptionServiceContainer);

            
        }

        private async Task LoadEventStoreProjections()
        {
            //Start our Continous Projections - we might decide to do this at a different stage, but now lets try here
            String projectionsFolder = "../../../projections/continuous";
            IPAddress[] ipAddresses = Dns.GetHostAddresses("127.0.0.1");

            if (!String.IsNullOrWhiteSpace(projectionsFolder))
            {
                DirectoryInfo di = new DirectoryInfo(projectionsFolder);

                if (di.Exists)
                {
                    FileInfo[] files = di.GetFiles();

                    EventStoreClientSettings eventStoreClientSettings = new EventStoreClientSettings
                    {
                        ConnectivitySettings = new EventStoreClientConnectivitySettings
                        {
                            Address = new Uri($"https://{ipAddresses.First().ToString()}:{this.EventStoreHttpPort}")
                        },
                        CreateHttpMessageHandler = () => new SocketsHttpHandler
                        {
                            SslOptions =
                                                                                                                 {
                                                                                                                     RemoteCertificateValidationCallback = (sender,
                                                                                                                                                            certificate,
                                                                                                                                                            chain,
                                                                                                                                                            errors) => true,
                                                                                                                 }
                        },
                        DefaultCredentials = new UserCredentials("admin", "changeit")

                    };
                    EventStoreProjectionManagementClient projectionClient = new EventStoreProjectionManagementClient(eventStoreClientSettings);

                    foreach (FileInfo file in files)
                    {
                        String projection = File.ReadAllText(file.FullName);
                        String projectionName = file.Name.Replace(".js", String.Empty);

                        try
                        {
                            Logger.LogInformation($"Creating projection [{projectionName}]");
                            await projectionClient.CreateContinuousAsync(projectionName, projection).ConfigureAwait(false);
                        }
                        catch (Exception e)
                        {
                            Logger.LogError(new Exception($"Projection [{projectionName}] error", e));
                        }
                    }
                }
            }

            Logger.LogInformation("Loaded projections");
        }

        protected async Task PopulateSubscriptionServiceConfiguration()
        {
            String connectionString = Setup.GetLocalConnectionString("SubscriptionServiceConfiguration");

            await using(SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync(CancellationToken.None).ConfigureAwait(false);

                // Create an Event Store Server
                await this.InsertEventStoreServer(connection, this.EventStoreContainerName).ConfigureAwait(false);
                Console.WriteLine($"SS Event Store Server {this.EventStoreContainerName} Created");
                String endPointUri = $"http://{this.EstateReportingContainerName}:5005/api/domainevents";
                // Add Route for Estate Aggregate Events
                await this.InsertSubscription(connection, "$ce-EstateAggregate", "Reporting", endPointUri).ConfigureAwait(false);
                Console.WriteLine("SS Subscription Created $ce-EstateAggregate");
                // Add Route for Merchant Aggregate Events
                await this.InsertSubscription(connection, "$ce-MerchantAggregate", "Reporting", endPointUri).ConfigureAwait(false);
                Console.WriteLine("SS Subscription Created $ce-MerchantAggregate");
                // Add Route for Transaction Aggregate Events
                await this.InsertSubscription(connection, "$ce-TransactionAggregate", "Reporting", endPointUri).ConfigureAwait(false);
                Console.WriteLine("SS Subscription Created $ce-TransactionAggregate");
                // Add Route for Transaction Aggregate Events
                await this.InsertSubscription(connection, "$ce-ContractAggregate", "Reporting", endPointUri).ConfigureAwait(false);
                Console.WriteLine("SS Subscription Created $ce-ContractAggregate");

                String esConnectionString = $"ConnectTo=tcp://admin:changeit@{this.EventStoreContainerName}:{DockerHelper.EventStoreTcpDockerPort};VerboseLogging=true;";
                SqlCommand command = connection.CreateCommand();
                command.CommandText = $"SELECT COUNT(*) FROM Subscription WHERE EventStoreId = '{this.TestId}'";
                command.CommandType = CommandType.Text;
                var scalar = await command.ExecuteScalarAsync(CancellationToken.None).ConfigureAwait(false);
                Console.WriteLine(scalar.ToString());


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
            
            ContainerBuilder containerBuilder = new Builder().UseContainer().WithName(containerName)
                                                             .WithEnvironment($"AppSettings:Authority=http://sferguson.ddns.net:55001",
                                                                              $"AppSettings:ClientId={clientDetails.clientId}",
                                                                              $"AppSettings:ClientSecret={clientDetails.clientSecret}",
                                                                              $"AppSettings:IsIntegrationTest=true",
                                                                              $"EstateManagementScope=estateManagement{this.TestId.ToString("N")}",
                                                                              $"AppSettings:EstateManagementApi=http://{estateManagementContainerName}:{DockerHelper.EstateManagementDockerPort}")
                                                             .UseImage(imageName).ExposePort(5004)
                                                             .UseNetwork(networkServices.ToArray());

            if (String.IsNullOrEmpty(hostFolder) == false)
            {
                containerBuilder = containerBuilder.Mount(hostFolder, "/home", MountType.ReadWrite);
            }

            IContainerService containerService = containerBuilder.Build().Start().WaitForPort("5004/tcp", 30000);

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
