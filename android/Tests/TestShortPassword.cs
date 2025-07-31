using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using BrowserStack.Pages;
using TestApp.Base;

namespace BrowserStack.Tests
{
    [TestFixture]
    [Category("Login")]
    public class TestShortPassword : TestBase
    {
        private LoginPage loginPage;

        [Test]
        public void LoginTest()
        {
            loginPage = new LoginPage(driver, wait);
            loginPage.GoToLoginScreen();
            loginPage.EnterLoginInfo("truonfnguyye@gmail.com", "123");

            var error = wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath("//android.widget.TextView[@text='Please enter at least 8 characters']")));
            Assert.AreEqual("Please enter at least 8 characters", error.Text);
        }
    }
}
