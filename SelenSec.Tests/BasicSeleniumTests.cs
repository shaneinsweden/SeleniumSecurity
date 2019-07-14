using System;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace SelenSec.Tests
{
    [TestClass]
    public class BasicSeleniumTests
    {
        [TestMethod]
        public void RunRecapcha_Version2Invisible_Works()
        {
            //Arrange
            IWebDriver driver = WebUtils.InitDriver();
            driver.Url = "https://recaptcha-demo.appspot.com/";
            WaitXSeconds(1000);

            //act
            driver.FindElement(By.LinkText("Invisible")).Click();
            WaitXSeconds(2000);
            driver.FindElement(By.Name("ex-a")).SendKeys(Keys.Clear);
            WaitXSeconds(1000);
            driver.FindElement(By.Name("ex-a")).SendKeys("a");
            WaitXSeconds(1000);
            driver.FindElement(By.Name("ex-b")).SendKeys(Keys.Clear);
            WaitXSeconds(1000);
            driver.FindElement(By.Name("ex-b")).SendKeys("3");
            WaitXSeconds(1000);
            var webElements = driver.FindElements(By.ClassName("g-recaptcha"));
            webElements[0].Click();
        }


        [TestMethod]
        public void Selenium_LoadPhilips_Success()
        {



            //Arrange
            IWebDriver driver = WebUtils.InitDriver();

            //Act
            driver.Url = "https://www.usa.philips.com/c-w/productadvisor/pa02-product-advisor.html#/questions";

            //< input ng - click = "setAnswer(ansItem, $index, question, $parent.$index)" type = "checkbox" id = "PA_OHC_Q1_A1" value = "PA_OHC_Q1_A1" name = "<p>I occasionally forget to brush</p>">
            driver.FindElement(By.Id("PA_OHC_Q1_A1")).Click();

            WaitXSeconds(1000);
            //< span class="p-advisor-icon-text ng-binding" ng-click="setSelectedQuestion(question.id)" ng-bind-html="question.text1 | stringToHTML" style="height: 22px;">2. My Teeth</span>
            var questElements = driver.FindElements(By.ClassName("p-advisor-icon-text")).ToList();

            questElements[1].Click();

            WaitXSeconds(1300);
            //answer the second checkbox
            driver.FindElement(By.Id("PA_OHC_Q3_A4")).Click();

            questElements[2].Click();
            //answer the third checkbox
            driver.FindElement(By.Id("PA_OHC_Q4_A1")).Click();
            WaitXSeconds(1300);
            questElements[3].Click();
            WaitXSeconds(1300);
            //answer the fourth checkbox
            driver.FindElement(By.Id("PA_OHC_Q2_A1")).Click();
            WaitXSeconds(1300);
            driver.FindElement(By.Id("PA_OHC_Q2_A3")).Click();

            //submit the form
            //< span class="ng-binding">Go</span>
            var buttonElements = driver.FindElements(By.ClassName("ng-binding")).Where(elem => elem.Text == "Go" && elem.TagName == "span").ToList();
            buttonElements[0].Click();


            //Assert
            Assert.IsNotNull(driver);
        }

        [TestMethod]
        public void Selenium_CheckGoodWebApplication_PassCsrfTest()
        {
            //Arrange
            string[] urlsToTest = new string[] {"http://www.axnunicode.com/" };

            //Act
            bool result = WebUtils.TestUrlForCsrfProtection(urlsToTest);

            //Assert
            Assert.IsTrue(result);
        }


        [TestMethod]
        public void Selenium_CheckBadWebApplication_FailCsrfTest()
        {
            //Arrange
            string[] urlsToTest = new string[] { "https://forums.iis.net/" };

            //Act
            bool result = WebUtils.TestUrlForCsrfProtection(urlsToTest);

            //Assert
            Assert.IsTrue(!result);
        }

        public static void WaitXSeconds(int timeInMilliSeconds)
        {
            Thread.Sleep(timeInMilliSeconds);
        }




    }
}
