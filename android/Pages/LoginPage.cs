using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;

namespace BrowserStack.Pages
{
    public class LoginPage
    {
        private readonly AndroidDriver<AndroidElement> driver;
        private readonly WebDriverWait wait;


        public LoginPage(AndroidDriver<AndroidElement> driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }


        private By EmailField => MobileBy.AccessibilityId("input-email");
        private By PasswordField => MobileBy.AccessibilityId("input-password");
        private By LoginButton => MobileBy.AccessibilityId("button-LOGIN");
        public void GoToLoginScreen()
        {
            var loginButton = wait.Until(d => d.FindElement(MobileBy.AccessibilityId("Login")));
            loginButton.Click();
        }

        public void EnterLoginInfo(string email, string password)
        {
            
            driver.FindElement(EmailField).Clear();
            driver.FindElement(EmailField).Click();

            driver.FindElement(EmailField).SendKeys(email);

            driver.FindElement(PasswordField).Clear();
            driver.FindElement(PasswordField).Click();
            driver.FindElement(PasswordField).SendKeys(password);

            driver.FindElement(LoginButton).Click();
        }
    }
}
