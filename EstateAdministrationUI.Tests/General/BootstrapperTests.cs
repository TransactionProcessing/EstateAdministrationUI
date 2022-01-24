using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateAdministrationUI.Tests.General
{
    using System.Diagnostics;
    using Lamar;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    using Xunit;

    public class BootstrapperTests
    {
        [Fact]
        public void VerifyBootstrapperIsValid()
        {
            Mock<IWebHostEnvironment> hostingEnvironment = new Mock<IWebHostEnvironment>();
            hostingEnvironment.Setup(he => he.EnvironmentName).Returns("Development");
            hostingEnvironment.Setup(he => he.ContentRootPath).Returns("/home");
            hostingEnvironment.Setup(he => he.ApplicationName).Returns("Test Application");

            ServiceRegistry services = new ServiceRegistry();
            Startup s = new Startup(hostingEnvironment.Object);
            Startup.Configuration = this.SetupMemoryConfiguration();

            this.AddTestRegistrations(services, hostingEnvironment.Object);
            s.ConfigureContainer(services);
            Startup.Container.AssertConfigurationIsValid();
        }

        private IConfigurationRoot SetupMemoryConfiguration()
        {
            Dictionary<String, String> configuration = new Dictionary<String, String>();
            configuration.Add("AppSettings:EstateManagementApi", "http://127.0.0.1");
            configuration.Add("AppSettings:EstateReportingApi", "http://127.0.0.1");
            configuration.Add("SecurityConfiguration:Authority", "http://127.0.0.1");

            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddInMemoryCollection(configuration);

            return builder.Build();
        }

        private void AddTestRegistrations(ServiceRegistry serviceRegistry,
                                                     IWebHostEnvironment hostingEnvironment)
        {
            serviceRegistry.AddLogging();
            DiagnosticListener diagnosticSource = new DiagnosticListener(hostingEnvironment.ApplicationName);
            serviceRegistry.AddSingleton<DiagnosticSource>(diagnosticSource);
            serviceRegistry.AddSingleton<DiagnosticListener>(diagnosticSource);
            serviceRegistry.AddSingleton<IWebHostEnvironment>(hostingEnvironment);
        }
    }
}
