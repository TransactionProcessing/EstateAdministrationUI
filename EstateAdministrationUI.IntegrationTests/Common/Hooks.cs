namespace EstateAdministrationUI.IntegrationTests.Common
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BoDi;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Edge;
    using OpenQA.Selenium.Firefox;
    using Shared.IntegrationTesting;
    using TechTalk.SpecFlow;

    [Binding]
    public class Hooks
    {
        private readonly IObjectContainer ObjectContainer;
        private IWebDriver WebDriver;

        public Hooks(IObjectContainer objectContainer)
        {
            this.ObjectContainer = objectContainer;
        }

        [BeforeScenario(Order = 0)]
        public async Task BeforeScenario()
        {
            String? browser = Environment.GetEnvironmentVariable("Browser");
            //browser = "Edge";

            if (browser == null || browser == "Chrome")
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArguments("--disable-gpu");
                options.AddArguments("--no-sandbox");
                options.AddArguments("--disable-dev-shm-usage");
                options.AcceptInsecureCertificates = true;

                ChromeDriverService x = ChromeDriverService.CreateDefaultService();

                this.WebDriver = new ChromeDriver(x, options, TimeSpan.FromMinutes(3));
            }

            if (browser == "Firefox")
            {
                FirefoxOptions options = new FirefoxOptions();
                options.AcceptInsecureCertificates = true;
                options.AddArguments("-headless");
                options.LogLevel = FirefoxDriverLogLevel.Debug;
                FirefoxDriverService x = FirefoxDriverService.CreateDefaultService();

                this.WebDriver = new FirefoxDriver(x, options, TimeSpan.FromMinutes(3));
            }

            if (browser == "Edge")
            {
                EdgeOptions options = new EdgeOptions();
                options.AcceptInsecureCertificates = true;
                EdgeDriverService x = EdgeDriverService.CreateDefaultService();

                this.WebDriver = new EdgeDriver(x, options, TimeSpan.FromMinutes(3));
            }

            this.WebDriver.Manage().Timeouts().PageLoad.Add(TimeSpan.FromSeconds(30));
            this.WebDriver.Manage().Window.Maximize();
            this.ObjectContainer.RegisterInstanceAs(this.WebDriver);
        }

        [AfterScenario(Order = 0)]
        public void AfterScenario()
        {
            if (this.WebDriver != null)
            {
                this.WebDriver.Quit(); //.Dispose();
            }
        }
    }
}
