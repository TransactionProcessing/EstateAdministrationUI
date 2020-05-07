namespace EstateAdministrationUI.IntegrationTests.Common
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using OpenQA.Selenium;
    using Shouldly;

    public static class Extensions
    {
        public static void FillIn(this IWebDriver webDriver,
                                  String elementName,
                                  String value)
        {
            IWebElement webElement = webDriver.FindElement(By.Name(elementName));
            webElement.ShouldNotBeNull();
            webElement.SendKeys(value);
        }

        public static IWebElement FindButtonById(this IWebDriver webDriver,
                                                 String buttonId)
        {
            IWebElement element = webDriver.FindElement(By.Id(buttonId));

            element.ShouldNotBeNull();

            return element;
        }

        public static IWebElement FindButtonByText(this IWebDriver webDriver,
                                                   String buttonText)
        {
            ReadOnlyCollection<IWebElement> elements = webDriver.FindElements(By.TagName("button"));

            List<IWebElement> e = elements.Where(e => e.GetAttribute("innerText") == buttonText).ToList();

            e.ShouldHaveSingleItem();

            return e.Single();
        }

        public static void ClickLink(this IWebDriver webDriver,
                                     String linkText)
        {
            IWebElement webElement = webDriver.FindElement(By.LinkText(linkText));
            webElement.ShouldNotBeNull();
            webElement.Click();
        }

        public static void ClickButtonById(this IWebDriver webDriver,
                                           String buttonId)
        {
            IWebElement webElement = webDriver.FindButtonById(buttonId);
            webElement.ShouldNotBeNull();
            webElement.Click();
        }

        public static void ClickButtonByText(this IWebDriver webDriver,
                                             String buttonText)
        {
            IWebElement webElement = webDriver.FindButtonByText(buttonText);
            webElement.ShouldNotBeNull();
            webElement.Click();
        }
    }
}