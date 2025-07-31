using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using BrowserStack.Pages;
using TestApp.Base;

namespace BrowserStack.Tests
{

    [TestFixture]
    [Category("Login")]
    public class TestInvalidEmailFormat : TestBase
    {
        private LoginPage loginPage;

        [Test]
        public void LoginTest()
        {
            loginPage = new LoginPage(driver, wait);
            loginPage.GoToLoginScreen();
            loginPage.EnterLoginInfo("truonfnguyye", "yourPassword123");

            var error = wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath("//android.widget.TextView[@text='Please enter a valid email address']")));
            Assert.AreEqual("Please enter a valid email address", error.Text);
        }
    }
}
