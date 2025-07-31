using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Support.UI;
using System;

namespace BrowserStack.Pages
{
    public class SwipePage
    {
        private readonly AndroidDriver<AndroidElement> driver;
        private readonly WebDriverWait wait;


        public SwipePage(AndroidDriver<AndroidElement> driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;

        }

        public void GoToSwipeTab()
        {
            var swipeTab = driver.FindElementByXPath("//android.widget.TextView[@text='Swipe']");
            swipeTab.Click();
        }

        public bool SwipeUntilSupportVideoVisible(int maxSwipes = 5)
        {
            var screenSize = driver.Manage().Window.Size;

            int startX = (int)(screenSize.Width * 0.8);
            int endX = (int)(screenSize.Width * 0.2);
            int y = (int)(screenSize.Height * 0.5);

            for (int i = 0; i < maxSwipes; i++)
            {
                try
                {
                    var supportVideoCard = driver.FindElementByXPath("//android.widget.TextView[@text='SUPPORT VIDEOS']");
                    if (supportVideoCard.Displayed)
                    {
                        return true;
                    }
                }
                catch (NoSuchElementException)
                {
                    new TouchAction(driver)
                        .Press(startX, y)
                        .Wait(300)
                        .MoveTo(endX, y)
                        .Release()
                        .Perform();
                }
            }

            return false;
        }
    }
}
