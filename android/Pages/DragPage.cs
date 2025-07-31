using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace BrowserStack.Pages
{
    public class DragPage
    {
        private readonly AndroidDriver<AndroidElement> driver;
        private readonly WebDriverWait wait;

        public DragPage(AndroidDriver<AndroidElement> driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }

        public void GoToDragScreen()
        {
            var dragButton = wait.Until(d => d.FindElement(MobileBy.AccessibilityId("Drag")));
            dragButton.Click();
        }

        public void DragAndDropColumn(string prefix)
        {
            for (int i = 1; i <= 3; i++)
            {
                var dragId = $"drag-{prefix}{i}";
                var dropId = $"drop-{prefix}{i}";

                var dragDot = wait.Until(d => d.FindElement(MobileBy.AccessibilityId(dragId)));
                var dropZone = wait.Until(d => d.FindElement(MobileBy.AccessibilityId(dropId)));

                var action = new TouchAction(driver);
                action.LongPress(dragDot)
                      .MoveTo(dropZone)
                      .Release()
                      .Perform();

                Thread.Sleep(800); 
            }
        }

        public string GetCongratulationsText()
        {
            var element = wait.Until(d => d.FindElement(MobileBy.XPath("//*[contains(@text, 'Congratulations')]")));
            return element.Text;
        }
    }
}
