namespace EstateAdministrationUI.IntegrationTests.Common
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BoDi;
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
            ChromeOptions options = new ChromeOptions();
            //options.AddArguments("--disable-gpu");
            //options.AddArguments("--no-sandbox");
            //options.AddArguments("--disable-dev-shm-usage");
            options.AddArguments("--window-size=1920,1080");
            options.AddArguments("--start-maximized");
            var experimentalFlags = new List<String>();
            experimentalFlags.Add("same-site-by-default-cookies@2");
            experimentalFlags.Add("cookies-without-same-site-must-be-secure@2");
            options.AddLocalStatePreference("browser.enabled_labs_experiments", experimentalFlags);
            this.WebDriver = new ChromeDriver(options);

            //FirefoxOptions options = new FirefoxOptions();
            //options.AddArguments("--headless");
            //options.AcceptInsecureCertificates = true;
            //options.UnhandledPromptBehavior = UnhandledPromptBehavior.Accept;
            //options.LogLevel = FirefoxDriverLogLevel.Trace;

            //this.WebDriver = new FirefoxDriver(options);

            this.ObjectContainer.RegisterInstanceAs(this.WebDriver);
        }

        [AfterScenario(Order = 0)]
        public void AfterScenario()
        {
            this.WebDriver.Quit();//.Dispose();
        }
    }
}
