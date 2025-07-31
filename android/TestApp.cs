using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Appium.Interfaces;
using System;
using System.Threading;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium;

namespace TestApp
{
    [TestFixture]
    public class TestApp
    {
        private AndroidDriver<AndroidElement> driver;
        private WebDriverWait wait;

        [SetUp]
        public void SetUp()
        {
            var options = new AppiumOptions();
            options.PlatformName = "Android";
            options.AddAdditionalCapability("automationName", "UiAutomator2");
            options.AddAdditionalCapability("project", "Android Wdio Sample");
            options.AddAdditionalCapability("build", "Login Button Test");
            options.AddAdditionalCapability("name", "TestLoginButtonVisible");

            driver = new AndroidDriver<AndroidElement>(
                new Uri("http://hub.browserstack.com/wd/hub"),
                options,
                TimeSpan.FromSeconds(60)
            );

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        }

        [Test]
        public void TestLoginButtonVisible()
        {
            Thread.Sleep(5000);
            var loginBtn = wait.Until(driver => driver.FindElement(MobileBy.AccessibilityId("Login")));
            Assert.IsTrue(loginBtn.Displayed);

            Thread.Sleep(2000);
        }

        [Test]
        public void LoginFlowTest()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            try
            {
                Console.WriteLine("Đợi màn hình login...");
                var loginButton = wait.Until(d => d.FindElement(MobileBy.AccessibilityId("Login")));
                loginButton.Click();

                Console.WriteLine("Tìm thấy nút Login và đã click");

                var emailField = wait.Until(d => d.FindElement(MobileBy.AccessibilityId("input-email")));
                emailField.Clear();
                emailField.SendKeys("sai@email.com");

                var passwordField = wait.Until(d => d.FindElement(MobileBy.AccessibilityId("input-password")));
                passwordField.Clear();
                passwordField.SendKeys("sai_password");

                Console.WriteLine("Nhập sai email và mật khẩu");

                var submitBtn = wait.Until(d => d.FindElement(MobileBy.AccessibilityId("button-LOGIN")));
                submitBtn.Click();

                Console.WriteLine(" Đợi phản hồi sau khi nhấn LOGIN");

                try
                {
                    var errorEmail = wait.Until(d => d.FindElement(MobileBy.AccessibilityId("input-email-error")));
                    var errorPassword = wait.Until(d => d.FindElement(MobileBy.AccessibilityId("input-password-error")));

                    Console.WriteLine("Email error: " + errorEmail.Text);
                    Console.WriteLine("Password error: " + errorPassword.Text);

                    ((IJavaScriptExecutor)driver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"passed\", \"reason\": \"Thông báo lỗi hiển thị đúng cho tài khoản sai\"}}");
                }
                catch (WebDriverTimeoutException)
                {
                    Console.WriteLine("Không thấy thông báo lỗi dưới field");

                    bool isOnHome = false;
                    try
                    {
                        var home = driver.FindElement(MobileBy.AccessibilityId("Home-screen"));
                        isOnHome = home.Displayed;
                    }
                    catch { }

                    if (isOnHome)
                    {
                        ((IJavaScriptExecutor)driver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"failed\", \"reason\": \"App vẫn login được với tài khoản sai\"}}");
                        Assert.Fail("App không nên login được với tài khoản sai.");
                    }
                    else
                    {
                        ((IJavaScriptExecutor)driver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"failed\", \"reason\": \"Không thấy thông báo lỗi và cũng không chuyển trang\"}}");
                        Assert.Fail("Không thấy thông báo lỗi và cũng không chuyển trang.");
                    }
                }
            }
            catch (Exception ex)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"failed\", \"reason\": \"" + ex.Message + "\"}}");
                throw;
            }
        }

        [Test]
        public void TestFormsSubmission()
        {
            Thread.Sleep(3000);

            var formsButton = wait.Until(d => d.FindElement(MobileBy.AccessibilityId("Forms")));
            formsButton.Click();

            var inputField = wait.Until(d => d.FindElement(MobileBy.AccessibilityId("text-input")));
            inputField.Clear();
            inputField.SendKeys("Hello from test!");

            var inputResult = driver.FindElement(MobileBy.AccessibilityId("input-text-result"));
            Assert.AreEqual("Hello from test!", inputResult.Text);

            var switchElement = driver.FindElement(MobileBy.AccessibilityId("switch"));
            switchElement.Click();

            var activeBtn = driver.FindElement(MobileBy.AccessibilityId("button-Active"));
            activeBtn.Click();

            var alert = wait.Until(d => d.FindElement(MobileBy.Id("android:id/alertTitle")));
            Assert.AreEqual("This button is", alert.Text);

            var okButton = driver.FindElement(MobileBy.Id("android:id/button1"));
            okButton.Click();

            Thread.Sleep(2000);
        }

        [Test]
        public void TestDragAndDrop()
        {
            var dragButton = wait.Until(d => d.FindElement(MobileBy.AccessibilityId("Drag")));
            dragButton.Click();


            for (int i = 1; i <= 3; i++)
            {
                var dragId = $"drag-l{i}";
                var dropId = $"drop-l{i}";

                var dragDot = wait.Until(d => d.FindElement(MobileBy.AccessibilityId(dragId)));
                var dropZone = wait.Until(d => d.FindElement(MobileBy.AccessibilityId(dropId)));

                var action = new TouchAction(driver);
                action
                    .LongPress(dragDot)
                    .MoveTo(dropZone)
                    .Release()
                    .Perform();
                Thread.Sleep(1000);

            }
            Thread.Sleep(300);

            for (int i = 1; i <= 3; i++)
            {
                var dragId = $"drag-r{i}";
                var dropId = $"drop-r{i}";

                var dragDot = wait.Until(d => d.FindElement(MobileBy.AccessibilityId(dragId)));
                var dropZone = wait.Until(d => d.FindElement(MobileBy.AccessibilityId(dropId)));

                var action = new TouchAction(driver);
                action
                    .LongPress(dragDot)
                    .MoveTo(dropZone)
                    .Release()
                    .Perform();
                Thread.Sleep(1000);

            }
            Thread.Sleep(300);
            for (int i = 1; i <= 3; i++)
            {
                var dragId = $"drag-c{i}";
                var dropId = $"drop-c{i}";

                var dragDot = wait.Until(d => d.FindElement(MobileBy.AccessibilityId(dragId)));
                var dropZone = wait.Until(d => d.FindElement(MobileBy.AccessibilityId(dropId)));

                var action = new TouchAction(driver);
                action
                    .LongPress(dragDot)
                    .MoveTo(dropZone)
                    .Release()
                    .Perform();
                Thread.Sleep(1000);

            }
            var congratsText = wait.Until(d => d.FindElement(MobileBy.XPath("//*[contains(@text, 'Congratulations')]")));
            Assert.That(congratsText.Text, Contains.Substring("Congratulations"));
        }



        [TearDown]
        public void TearDown()
        {
            driver?.Quit();
        }
    }
}
