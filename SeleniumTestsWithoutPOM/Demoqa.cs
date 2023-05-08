﻿
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Threading;

namespace SeleniumTestsWithoutPOM
{
    

    public class Demoqa
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            //driver.Manage().Window.Maximize();
        }


        [Test]
        public void TestValidEmailInput()
        {
            string name="Jonas Jonaitis";
            string eMailAddress= "jonasj.jonaitis@gmail.com";
            string currentAddress = "Karklu g. 5-5, Karklenai, Lietuva";
            string permanentAddress = "Varpu g. 15-1, Varniai, Lietuva";
                                  
            driver.Url = "https://demoqa.com/text-box";

            IWebElement fullNameField = driver.FindElement(By.XPath("//*[@id='userName']"));
            IWebElement emailField = driver.FindElement(By.XPath("//*[@id='userEmail']"));
            IWebElement currentAddressField = driver.FindElement(By.XPath("//*[@id='currentAddress']"));
            IWebElement permanentAddressField = driver.FindElement(By.XPath("//*[@id='permanentAddress']"));
            IWebElement submitButton = driver.FindElement(By.XPath("//*[@id='submit']"));
            
            fullNameField.SendKeys(name);
            emailField.SendKeys(eMailAddress);
            currentAddressField.SendKeys(currentAddress);
            permanentAddressField.SendKeys(permanentAddress);

            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", submitButton);
            submitButton.Click();

           

            IWebElement outputFullName = driver.FindElement(By.XPath("//*[@id='name']"));
            IWebElement outputEmail = driver.FindElement(By.XPath("//*[@id='email']"));

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='currentAddress']")));

            IWebElement outputCurrentAddress = driver.FindElement(By.Id("currentAddress")); //driver.FindElement(By.XPath("//textarea[@id = 'currentAddress']"));
            //IWebElement outputPermanentAddress = driver.FindElement(By.XPath("//*[@id='permanentAddress']"));

            Assert.AreEqual("Name:" + name, outputFullName.Text);
            Assert.AreEqual("Email:"+ eMailAddress, outputEmail.Text);
            Assert.AreEqual(currentAddress, outputCurrentAddress.Text);
            //Assert.AreEqual(permanentAddress, outputPermanentAddress.Text);
        }
        [Test]
    
        public void InvalidEmailInput()
        {
            string name = "Jonas Jonaitis";
            string invalidEmail = "invalid_email";
            string currentAddress = "Karklu g. 5-5, Karklenai, Lietuva";
            string permanentAddress = "Varpu g. 15-1, Varniai, Lietuva";

            driver.Url = "https://demoqa.com/text-box";

            IWebElement fullNameField = driver.FindElement(By.XPath("//*[@id='userName']"));
            IWebElement emailField = driver.FindElement(By.XPath("//*[@id='userEmail']"));
            IWebElement currentAddressField = driver.FindElement(By.XPath("//*[@id='currentAddress']"));
            IWebElement permanentAddressField = driver.FindElement(By.XPath("//*[@id='permanentAddress']"));
            IWebElement submitButton = driver.FindElement(By.XPath("//*[@id='submit']"));

            fullNameField.SendKeys(name);
            emailField.SendKeys(invalidEmail);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='currentAddress']")));

            currentAddressField.SendKeys(currentAddress);
            permanentAddressField.SendKeys(permanentAddress);

            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", submitButton);
            submitButton.Click();

            IWebElement emailFieldWithError = driver.FindElement(By.XPath("//*[@id='userEmail']"));

            Assert.IsTrue(emailFieldWithError.GetAttribute("class").Contains("error"));

        }
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}