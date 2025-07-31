using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using System;

namespace BrowserStack.Pages
{
    public class FormsPage
    {
        private readonly AndroidDriver<AndroidElement> driver;
        private readonly WebDriverWait wait;

        public FormsPage(AndroidDriver<AndroidElement> driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }

        public void OpenForms()
        {
            var formsButton = wait.Until(d => d.FindElement(MobileBy.AccessibilityId("Forms")));
            formsButton.Click();
        }

        public void EnterText(string text)
        {
            var inputField = wait.Until(d => d.FindElement(MobileBy.AccessibilityId("text-input")));
            inputField.Clear();
            inputField.SendKeys(text);
        }

        public string GetInputResult()
        {
            return driver.FindElement(MobileBy.AccessibilityId("input-text-result")).Text;
        }

        public void ToggleSwitch()
        {
            driver.FindElement(MobileBy.AccessibilityId("switch")).Click();
        }

        public void TapActiveButton()
        {
            driver.FindElement(MobileBy.AccessibilityId("button-Active")).Click();
        }

        public string GetAlertTitle()
        {
            var alert = wait.Until(d => d.FindElement(MobileBy.Id("android:id/alertTitle")));
            return alert.Text;
        }

        public void AcceptAlert()
        {
            driver.FindElement(MobileBy.Id("android:id/button1")).Click();
        }
    }
}
