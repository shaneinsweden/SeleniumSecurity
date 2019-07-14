using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SelenSec
{
    public class WebUtils
    {
        public static IWebDriver InitDriver()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.100 Safari/537.36");
            ChromeDriver driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            return driver;
        }

        public static bool TestUrlForCsrfProtection(string[] urlsToTest)
        {
            IWebDriver driver = InitDriver();

            foreach (string url in urlsToTest)
            {
                if (!TestOneUrl(url, driver))
                    return false;
            }

            return true;
        }

        private static bool TestOneUrl(string url, IWebDriver driver)
        {
            driver.Url = url;

            int numberOfForms = GetNumberOfForms(driver);

            int numberOfCsrfTokens = GetNumberOfTokens(driver);

            if (numberOfCsrfTokens == numberOfForms)
                return true;

            return false;
        }

        private static int GetNumberOfTokens(IWebDriver driver)
        {
            return driver.FindElements(By.CssSelector("input[type='hidden'][name='__RequestVerificationToken']")).Count;
        }

        private static int GetNumberOfForms(IWebDriver driver)
        {
            return driver.FindElements(By.CssSelector("form")).Count;

        }
    }
}
