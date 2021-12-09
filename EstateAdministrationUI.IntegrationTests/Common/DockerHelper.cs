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
                var network = docker.GetNetworks().Where(nw => nw.Name == networkName).SingleOrDefault();
                if (network == null)
                {
                    network = docker.CreateNetwork(networkName,
                                                       new NetworkCreateParams
                                                       {
                                                           Driver = "nat",
                                                       });
                }

                return network;
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
            this.HostTraceFolder = null;
            if (DockerHelper.GetDockerEnginePlatform() == DockerEnginePlatform.Linux)
            {
                this.HostTraceFolder = FdOs.IsWindows() ? $"F:\\home\\txnproc\\trace\\{scenarioName}" : $"//home//txnproc//trace//{scenarioName}";
            }

            Logging.Enabled();

            Guid testGuid = Guid.NewGuid();
            this.TestId = testGuid;

            this.Logger.LogInformation($"Test Id is {testGuid}");

            // Setup the container names
            this.SecurityServiceContainerName = $"sferguson.ddns.net";
            this.EstateManagementContainerName = $"estate{testGuid:N}";
            this.EstateReportingContainerName = $"estatereporting{testGuid:N}";
            this.EstateManagementUiContainerName = $"estateadministrationui{testGuid:N}";
            this.EventStoreContainerName = $"eventstore{testGuid:N}";

            String eventStoreAddress = $"http://{this.EventStoreContainerName}";
            this.ClientDetails = ("serviceClient", "Secret1");
            this.SqlServerDetails = (Setup.SqlServerContainerName, Setup.SqlUserName, Setup.SqlPassword);
            this.DockerCredentials = ("https://www.docker.com", "stuartferguson", "Sc0tland");

            INetworkService testNetwork = DockerHelper.SetupTestNetwork($"testnetwork{this.TestId:N}");
            this.TestNetworks.Add(testNetwork);

            // Setup the docker image names
            String eventStoreImageName = "eventstore/eventstore:21.2.0-buster-slim";
            String estateMangementImageName = "stuartferguson/estatemanagement";
            String estateReportingImageName = "stuartferguson/estatereporting";

            DockerEnginePlatform enginePlatform = DockerHelper.GetDockerEnginePlatform();
            if (enginePlatform == DockerEnginePlatform.Windows)
            {
                estateMangementImageName = "stuartferguson/estatemanagementwindows";
                estateReportingImageName = "stuartferguson/estatereportingwindows";
                eventStoreImageName = "stuartferguson/eventstore";
            }

            IContainerService eventStoreContainer = this.SetupEventStoreContainer(eventStoreImageName, testNetwork);
            this.EventStoreHttpPort = eventStoreContainer.ToHostExposedEndpoint($"{DockerHelper.EventStoreHttpDockerPort}/tcp").Port;
            
            List<String> estateManagementVariables = new List<String>();
            estateManagementVariables.Add($"SecurityConfiguration:ApiName=estateManagement{this.TestId.ToString("N")}");
            estateManagementVariables.Add($"EstateRoleName=Estate{this.TestId.ToString("N")}");
            estateManagementVariables.Add($"MerchantRoleName=Merchant{this.TestId.ToString("N")}");

            IContainerService estateManagementContainer = this.SetupEstateManagementContainer(        estateMangementImageName,
                                                                                                      new List<INetworkService>
                                                                                                      {
                                                                                                          testNetwork,
                                                                                                          Setup.DatabaseServerNetwork
                                                                                                      },
                                                                                                      securityServicePort:55001,
                                                                                                      additionalEnvironmentVariables:estateManagementVariables,
                                                                                                      forceLatestImage:true);

            IContainerService estateReportingContainer = this.SetupEstateReportingContainer(        estateReportingImageName,
                                                                                                    new List<INetworkService>
                                                                                                    {
                                                                                                        testNetwork,
                                                                                                        Setup.DatabaseServerNetwork
                                                                                                    },
                                                                                                    true);

            IContainerService estateManagementUiContainer = SetupEstateManagementUIContainer(this.EstateManagementUiContainerName,
                                                                                             this.Logger,
                                                                                             "estateadministrationui",
                                                                                             new List<INetworkService>
                                                                                             {
                                                                                                 testNetwork
                                                                                             },
                                                                                             this.EstateManagementContainerName,
                                                                                             this.EstateReportingContainerName,
                                                                                             this.HostTraceFolder,
                                                                                             this.DockerCredentials,
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

            await LoadEventStoreProjections().ConfigureAwait(false);
        }

        private static EventStoreClientSettings ConfigureEventStoreSettings(Int32 eventStoreHttpPort)
        {
            String connectionString = $"http://127.0.0.1:{eventStoreHttpPort}";

            EventStoreClientSettings settings = new EventStoreClientSettings();
            settings.CreateHttpMessageHandler = () => new SocketsHttpHandler
                                                      {
                                                          SslOptions =
                                                          {
                                                              RemoteCertificateValidationCallback = (sender,
                                                                                                     certificate,
                                                                                                     chain,
                                                                                                     errors) => true,
                                                          }
                                                      };
            settings.ConnectionName = "Specflow";
            settings.ConnectivitySettings = new EventStoreClientConnectivitySettings
                                            {
                                                Insecure = true,
                                                Address = new Uri(connectionString),
                                            };

            settings.DefaultCredentials = new UserCredentials("admin", "changeit");
            return settings;
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

                    EventStoreProjectionManagementClient projectionClient = new EventStoreProjectionManagementClient(ConfigureEventStoreSettings(this.EventStoreHttpPort));

                    foreach (FileInfo file in files)
                    {
                        String projection = File.ReadAllText(file.FullName);
                        String projectionName = file.Name.Replace(".js", String.Empty);

                        try
                        {
                            Logger.LogInformation($"Creating projection [{projectionName}]");
                            await projectionClient.CreateContinuousAsync(projectionName, projection, trackEmittedStreams:true).ConfigureAwait(false);
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

        public async Task PopulateSubscriptionServiceConfiguration(String estateName)
        {
            EventStorePersistentSubscriptionsClient client = new EventStorePersistentSubscriptionsClient(ConfigureEventStoreSettings(this.EventStoreHttpPort));

            PersistentSubscriptionSettings settings = new PersistentSubscriptionSettings(resolveLinkTos: true, StreamPosition.Start);
            await client.CreateAsync(estateName.Replace(" ", ""), "Reporting", settings);
            await client.CreateAsync($"EstateManagementSubscriptionStream_{estateName.Replace(" ", "")}", "Estate Management", settings);
        }

        private IContainerService SetupEstateManagementUIContainer(string containerName, ILogger logger,
                                                         string imageName,
                                                         List<INetworkService> networkServices,
                                                         String estateManagementContainerName,
                                                         String estateReportingtContainerName,
                                                         string hostFolder,
                                                         (string URL, string UserName, string Password)? dockerCredentials,
                                                         (string clientId, string clientSecret) clientDetails)
        {
            logger.LogInformation("About to Start Estate Management UI Container");
            
            ContainerBuilder containerBuilder = new Builder().UseContainer().WithName(containerName)
                                                             .WithEnvironment($"AppSettings:Authority=https://sferguson.ddns.net:55001",
                                                                              $"AppSettings:ClientId={clientDetails.clientId}",
                                                                              $"AppSettings:ClientSecret={clientDetails.clientSecret}",
                                                                              $"AppSettings:IsIntegrationTest=true",
                                                                              $"EstateManagementScope=estateManagement{this.TestId.ToString("N")}",
                                                                              $"AppSettings:EstateManagementApi=http://{estateManagementContainerName}:{DockerHelper.EstateManagementDockerPort}",
                                                                              $"AppSettings:EstateReportingApi=http://{estateReportingtContainerName}:{DockerHelper.EstateReportingDockerPort}")
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
