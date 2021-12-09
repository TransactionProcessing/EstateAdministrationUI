namespace EstateAdministrationUI.IntegrationTests.Common
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BoDi;
    using Microsoft.Edge.SeleniumTools;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
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
            //browser = "Firefox";
            if (browser == null || browser == "Chrome")
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArguments("--disable-gpu");
                options.AddArguments("--no-sandbox");
                options.AddArguments("--disable-dev-shm-usage");
                options.AcceptInsecureCertificates = true;
                this.WebDriver = new ChromeDriver(options);
                this.WebDriver.Manage().Window.Maximize();
            }

            if (browser == "Firefox")
            {
                FirefoxOptions options = new FirefoxOptions();
                options.SetPreference("network.cookie.cookieBehavior", 0);
                options.AcceptInsecureCertificates = true;

                await Retry.For(async () =>
                                {
                                    this.WebDriver = new FirefoxDriver(options);
                                }, TimeSpan.FromMinutes(5), TimeSpan.FromSeconds(60));
                
                this.WebDriver.Manage().Window.Maximize();
            }

            if (browser == "Edge")
            {
                EdgeOptions options = new EdgeOptions();
                options.UseChromium = true;
                options.AcceptInsecureCertificates = true;
                //this.WebDriver = new EdgeDriver(options);
                await Retry.For(async () =>
                                {
                                    this.WebDriver = new EdgeDriver(options);
                                }, TimeSpan.FromMinutes(5), TimeSpan.FromSeconds(60));
                this.WebDriver.Manage().Window.Maximize();
            }

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
