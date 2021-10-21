namespace EstateAdministrationUI.IntegrationTests.Common
{
    using System;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using System.Threading;
    using Ductus.FluentDocker;
    using Ductus.FluentDocker.Builders;
    using Ductus.FluentDocker.Services;
    using Ductus.FluentDocker.Services.Extensions;
    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Logging;
    using NLog;
    using Shared.IntegrationTesting;
    using Shared.Logger;
    using Shouldly;
    using TechTalk.SpecFlow;
    using ILogger = Microsoft.Extensions.Logging.ILogger;

    [Binding]
    public class Setup
    {
        protected Setup()
        {

        }

        protected static IContainerService DatabaseServerContainer;

        public static INetworkService DatabaseServerNetwork;

        public static String SqlServerContainerName = "shareddatabasesqlserver";

        public static String SqlUserName = "sa";

        public static String SqlPassword = "thisisalongpassword123!";

        [BeforeTestRun]
        protected static void GlobalSetup()
        {
            ShouldlyConfiguration.DefaultTaskTimeout = TimeSpan.FromMinutes(1);

            (String, String, String) dockerCredentials = ("https://www.docker.com", "stuartferguson", "Sc0tland");

            // Setup a network for the DB Server
            Setup.DatabaseServerNetwork = DockerHelper.SetupTestNetwork("sharednetwork");

            NlogLogger logger = new NlogLogger();
            logger.Initialise(LogManager.GetLogger("Specflow"), "Specflow");
            LogManager.AddHiddenAssembly(typeof(NlogLogger).Assembly);

            String sqlServerImageName = "mcr.microsoft.com/mssql/server:2019-latest";
            DockerEnginePlatform enginePlatform = DockerHelper.GetDockerEnginePlatform();
            if (enginePlatform == DockerEnginePlatform.Windows)
            {
                sqlServerImageName = "tobiasfenster/mssql-server-dev-unsupported";
            }

            // Start the Database Server here
            Setup.DatabaseServerContainer = Setup.StartSqlContainerWithOpenConnection(Setup.SqlServerContainerName,
                                                                                                                               logger,
                                                                                                                               sqlServerImageName,
                                                                                                                               Setup.DatabaseServerNetwork,
                                                                                                                               dockerCredentials,
                                                                                                                               Setup.SqlUserName,
                                                                                                                               Setup.SqlPassword);
        }

        /// <summary>
        /// Starts the SQL container with open connection.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="imageName">Name of the image.</param>
        /// <param name="networkService">The network service.</param>
        /// <param name="hostFolder">The host folder.</param>
        /// <param name="dockerCredentials">The docker credentials.</param>
        /// <param name="sqlUserName">Name of the SQL user.</param>
        /// <param name="sqlPassword">The SQL password.</param>
        /// <returns></returns>
        public static IContainerService StartSqlContainerWithOpenConnection(String containerName,
                                                                            NlogLogger logger,
                                                                            String imageName,
                                                                            INetworkService networkService,
                                                                            (String URL, String UserName, String Password)? dockerCredentials,
                                                                            String sqlUserName = "sa",
                                                                            String sqlPassword = "thisisalongpassword123!")
        {
            logger.LogInformation("About to start SQL Server Container");
            IContainerService databaseServerContainer = new Builder().UseContainer().WithName(containerName).UseImage(imageName)
                                                                     .WithEnvironment("ACCEPT_EULA=Y", $"SA_PASSWORD={sqlPassword}").ExposePort(1433)
                                                                     .UseNetwork(networkService).KeepContainer().KeepRunning().ReuseIfExists().Build().Start();

            logger.LogInformation("SQL Server Container Started");

            logger.LogInformation("About to SQL Server Container is running");
            IPEndPoint sqlServerEndpoint = null;
            Retry.For(async () => { sqlServerEndpoint = databaseServerContainer.ToHostExposedEndpoint("1433/tcp"); }).Wait();

            // Try opening a connection
            Int32 maxRetries = 10;
            Int32 counter = 1;

            //String localhostaddress = Environment.GetEnvironmentVariable("localhostaddress");
            //if (String.IsNullOrEmpty(localhostaddress))
            //{
            //    localhostaddress = "192.168.1.67";
            //}

            String server = "127.0.0.1";
            String database = "master";
            String user = sqlUserName;
            String password = sqlPassword;
            String port = sqlServerEndpoint.Port.ToString();

            String connectionString = $"server={server},{port};user id={user}; password={password}; database={database};";
            logger.LogInformation($"Connection String {connectionString}");
            SqlConnection connection = new SqlConnection(connectionString);
            while (counter <= maxRetries)
            {
                try
                {
                    logger.LogInformation($"Database Connection Attempt {counter}");

                    connection.Open();

                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "select * from sys.databases";
                    SqlDataReader dataReader = command.ExecuteReader(CommandBehavior.Default);

                    logger.LogInformation("Connection Opened");

                    //// Check if we need to create the SS database
                    //if (dataReader.HasRows)
                    //{
                    //    while (dataReader.Read())
                    //    {
                    //        if (dataReader.GetFieldValue<String>(0) == "SubscriptionServiceConfiguration")
                    //        {
                    //            databaseFound = true;
                    //            break;
                    //        }
                    //    }
                    //}

                    dataReader.Close();
                    connection.Close();
                    logger.LogInformation("SQL Server Container Running");
                    break;
                }
                catch (SqlException ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }

                    logger.LogError(ex);
                    Thread.Sleep(20000);
                }
                finally
                {
                    counter++;
                }
            }
            
            return databaseServerContainer;
        }

        public static String GetConnectionString(String databaseName)
        {
            return $"server={Setup.DatabaseServerContainer.Name};database={databaseName};user id={Setup.SqlUserName};password={Setup.SqlPassword}";
        }

        public static String GetLocalConnectionString(String databaseName)
        {
            Int32 databaseHostPort = Setup.DatabaseServerContainer.ToHostExposedEndpoint("1433/tcp").Port;

            return $"server=localhost,{databaseHostPort};database={databaseName};user id={Setup.SqlUserName};password={Setup.SqlPassword}";
        }
    }
}