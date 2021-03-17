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

            if (browser == null || browser == "Chrome")
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArguments("--disable-gpu");
                options.AddArguments("--no-sandbox");
                options.AddArguments("--disable-dev-shm-usage");
                var experimentalFlags = new List<String>();
                experimentalFlags.Add("same-site-by-default-cookies@2");
                experimentalFlags.Add("cookies-without-same-site-must-be-secure@2");
                options.AddLocalStatePreference("browser.enabled_labs_experiments", experimentalFlags);
                this.WebDriver = new ChromeDriver(options);
                this.WebDriver.Manage().Window.Maximize();
            }

            if (browser == "Firefox")
            {
                //FirefoxOptions options = new FirefoxOptions();
                //options.AddArguments("-headless");
                //this.WebDriver = new FirefoxDriver(options);
                //FirefoxProfile profile = new ProfilesIni().getProfile("default");
                //profile.setPreference("network.cookie.cookieBehavior", 2);
                //this.WebDriver = new FirefoxDriver(profile);
                FirefoxOptions options = new FirefoxOptions();
                options.SetPreference("network.cookie.sameSite.laxByDefault", false);
                options.SetPreference("network.cookie.sameSite.noneRequiresSecure", false);
                options.SetPreference("network.cookie.sameSite.schemeful", false);
                options.SetPreference("network.cookie.cookieBehavior", 0);
                this.WebDriver = new FirefoxDriver(options);
                this.WebDriver.Manage().Window.Maximize();
            }

            if (browser == "Edge")
            {
                EdgeOptions options = new EdgeOptions();
                options.UseChromium = true;
                List<String> experimentalFlags = new List<String>();
                experimentalFlags.Add("same-site-by-default-cookies@2");
                experimentalFlags.Add("cookies-without-same-site-must-be-secure@2");
                options.AddLocalStatePreference("browser.enabled_labs_experiments", experimentalFlags);

                this.WebDriver = new EdgeDriver(options);
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
