using System;
using System.Collections.Generic;

namespace EstateAdministrationUI.IntegrationTests.Common
{
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Ductus.FluentDocker.Builders;
    using Ductus.FluentDocker.Common;
    using Ductus.FluentDocker.Executors;
    using Ductus.FluentDocker.Extensions;
    using Ductus.FluentDocker.Services;
    using Ductus.FluentDocker.Services.Extensions;
    using EstateManagement.Client;
    using Newtonsoft.Json;
    using SecurityService.Client;
    using Shared.HealthChecks;
    using Shared.IntegrationTesting;
    using Shouldly;

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
            this.SecurityServiceContainerName = $"identity-server";
            this.EstateManagementUiContainerName = $"estateadministrationui{this.TestId:N}";
        }

        private static void AddEntryToHostsFile(String ipaddress,
                                                String hostname)
        {
            if (FdOs.IsWindows())
            {
                using (StreamWriter w = File.AppendText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), @"drivers\etc\hosts")))
                {
                    w.WriteLine($"{ipaddress} {hostname}");
                }
            }
            else if (FdOs.IsLinux())
            {
                DockerHelper.ExecuteBashCommand($"echo {ipaddress} {hostname} | sudo tee -a /etc/hosts");
            }
        }

        /// <summary>
        /// Executes the bash command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        private static void ExecuteBashCommand(String command)
        {
            // according to: https://stackoverflow.com/a/15262019/637142
            // thans to this we will pass everything as one command
            command = command.Replace("\"", "\"\"");

            var proc = new Process
                       {
                           StartInfo = new ProcessStartInfo
                                       {
                                           FileName = "/bin/bash",
                                           Arguments = "-c \"" + command + "\"",
                                           UseShellExecute = false,
                                           RedirectStandardOutput = true,
                                           CreateNoWindow = true
                                       }
                       };
            Console.WriteLine(proc.StartInfo.Arguments);

            proc.Start();
            proc.WaitForExit();
        }
        
        /// <summary>
        /// Starts the containers for scenario run.
        /// </summary>
        /// <param name="scenarioName">Name of the scenario.</param>
        public override async Task StartContainersForScenarioRun(String scenarioName)
        {
            await base.StartContainersForScenarioRun(scenarioName);

            await this.StartEstateManagementUiContainer(this.TestNetworks, this.SecurityServicePort);
            
            // Setup the base address resolvers
            String EstateManagementBaseAddressResolver(String api) => $"http://127.0.0.1:{this.EstateManagementPort}";

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
            //Func<String, String> securityServiceBaseAddressResolver = api => $"https://sferguson.ddns.net:55001";
            Func<String, String> securityServiceBaseAddressResolver = api => $"https://127.0.0.1:{this.SecurityServicePort}";
            this.SecurityServiceClient = new SecurityServiceClient(securityServiceBaseAddressResolver, httpClient);
        }
        
        private async Task<IContainerService> StartEstateManagementUiContainer(List<INetworkService> networkServices,
                                                                               Int32 securityServiceContainerPort)
        {
            Trace("About to Start Estate Management UI Container");

            List<String> environmentVariables = this.GetCommonEnvironmentVariables();
            environmentVariables.Add($"AppSettings:Authority=https://identity-server:{securityServiceContainerPort}");
            //environmentVariables.Add($"AppSettings:IsIntegrationTest=true");
            environmentVariables.Add($"ASPNETCORE_ENVIRONMENT=Development");
            environmentVariables.Add($"EstateManagementScope=estateManagement");
            environmentVariables.Add("urls=https://*:5004");
            environmentVariables.Add($"AppSettings:ClientId=estateUIClient");
            environmentVariables.Add($"AppSettings:ClientSecret=Secret1");

            Trace("About to Built Estate Management UI Container");
            ContainerBuilder containerBuilder = new Builder().UseContainer().WithName(this.EstateManagementUiContainerName)
                                                             .UseImageDetails(("estateadministrationui", false)).WithEnvironment(environmentVariables.ToArray())
                                                             .UseNetwork(networkServices.ToArray()).ExposePort(5004).MountHostFolder(this.HostTraceFolder)
                                                             .SetDockerCredentials(this.DockerCredentials);
            Trace("About to Call .Start()");
            IContainerService builtContainer = containerBuilder.Build().Start().WaitForPort("5004/tcp", 30000);

            Trace("About to attach networkServices");
            foreach (INetworkService networkService in networkServices)
            {
                networkService.Attach(builtContainer, false);
            }

            //Trace("About to get port");
            ////  Do a health check here
            //var x = builtContainer.ToHostExposedEndpoint($"5004/tcp");
            //if (x == null){
            //    Trace("x is null");
            //}

            //ConsoleStream<String> logs = builtContainer.Logs(true, CancellationToken.None);
            //IList<String> xx = logs.ReadToEnd();
            //while (xx.Any()){
            //    foreach (String s in xx){
            //        Trace($"Logs|{s}");    
            //    }
            //    xx = logs.ReadToEnd();
            //}

            this.EstateManagementUiPort = builtContainer.ToHostExposedEndpoint($"5004/tcp").Port;

            Trace("Estate Management UI Started");
            this.Containers.Add(builtContainer);
            //await Retry.For(async () =>
            //{
            //    String healthCheck =
            //    await this.HealthCheckClient.PerformHealthCheck("http", "127.0.0.1", this.EstateManagementUiPort, CancellationToken.None);

            //    var result = JsonConvert.DeserializeObject<HealthCheckResult>(healthCheck);
            //    result.Status.ShouldBe(HealthCheckStatus.Healthy.ToString(), $"Details {healthCheck}");
            //});

            return builtContainer;
        }

        public override async Task<IContainerService> SetupSecurityServiceContainer(List<INetworkService> networkServices)
        {
            this.Trace("About to Start Security Container");

            DockerHelper.AddEntryToHostsFile("127.0.0.1", SecurityServiceContainerName);
            DockerHelper.AddEntryToHostsFile("localhost", SecurityServiceContainerName);

            List<String> environmentVariables = this.GetCommonEnvironmentVariables();
            environmentVariables.Add($"ServiceOptions:PublicOrigin=https://{this.SecurityServiceContainerName}:{DockerPorts.SecurityServiceDockerPort}");
            environmentVariables.Add($"ServiceOptions:IssuerUrl=https://{this.SecurityServiceContainerName}:{DockerPorts.SecurityServiceDockerPort}");
            environmentVariables.Add("ASPNETCORE_ENVIRONMENT=IntegrationTest");
            environmentVariables.Add($"urls=https://*:{DockerPorts.SecurityServiceDockerPort}");

            environmentVariables.Add($"ServiceOptions:PasswordOptions:RequiredLength=6");
            environmentVariables.Add($"ServiceOptions:PasswordOptions:RequireDigit=false");
            environmentVariables.Add($"ServiceOptions:PasswordOptions:RequireUpperCase=false");
            environmentVariables.Add($"ServiceOptions:UserOptions:RequireUniqueEmail=false");
            environmentVariables.Add($"ServiceOptions:SignInOptions:RequireConfirmedEmail=false");
            
            ContainerBuilder securityServiceContainer = new Builder().UseContainer().WithName(this.SecurityServiceContainerName)
                                                                     .WithEnvironment(environmentVariables.ToArray())
                                                                     .UseImageDetails(this.GetImageDetails(ContainerType.SecurityService))
                                                                     .ExposePort(DockerPorts.SecurityServiceDockerPort, DockerPorts.SecurityServiceDockerPort)
                                                                     .MountHostFolder(this.HostTraceFolder)
                                                                     .SetDockerCredentials(this.DockerCredentials);

            // Now build and return the container                
            IContainerService builtContainer = securityServiceContainer.Build().Start();//.WaitForPort($"{DockerPorts.SecurityServiceDockerPort}/tcp", 30000);

            foreach (INetworkService networkService in networkServices)
            {
                networkService.Attach(builtContainer, false);
            }

            this.Trace("Security Service Container Started");
            this.Containers.Add(builtContainer);

            //  Do a health check here
            this.SecurityServicePort = builtContainer.ToHostExposedEndpoint($"{DockerPorts.SecurityServiceDockerPort}/tcp").Port;
            await this.DoHealthCheck(ContainerType.SecurityService);

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
