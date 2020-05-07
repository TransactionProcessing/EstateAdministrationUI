namespace EstateAdministrationUI.IntegrationTests.Common
{
    using System.Threading.Tasks;
    using BoDi;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
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
            ChromeOptions option = new ChromeOptions();
            option.AddArgument("--headless");
            this.WebDriver = new ChromeDriver(option);
            this.ObjectContainer.RegisterInstanceAs(this.WebDriver);
        }

        [AfterScenario(Order = 0)]
        public void AfterScenario()
        {
            this.WebDriver.Dispose();
        }
    }
}