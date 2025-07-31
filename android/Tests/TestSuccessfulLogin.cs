using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using BrowserStack.Pages;
using TestApp.Base;

namespace BrowserStack.Tests
{
    [TestFixture]
    [Category("Login")]

    public class TestSuccessfulLogin : TestBase
    {
        private LoginPage loginPage;

        [Test]
        public void LoginTest()
        {
            loginPage = new LoginPage(driver, wait);
            loginPage.GoToLoginScreen();
            loginPage.EnterLoginInfo("truonfnguyye@gmail.com", "yourPassword123");

            var alertTitle = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("android:id/alertTitle")));
            Assert.AreEqual("Success", alertTitle.Text);

            var alertMsg = driver.FindElementById("android:id/message");
            Assert.IsTrue(alertMsg.Text.Contains("logged in"));

            driver.FindElementById("android:id/button1").Click();
        }
    }
}
