using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Threading;


namespace TestApp.Base
{
    [Parallelizable(ParallelScope.Children)]
    public class TestBase
    {
        protected AndroidDriver<AndroidElement> driver;
        protected WebDriverWait wait;
        protected static AventStack.ExtentReports.ExtentReports extent;
        protected static ThreadLocal<ExtentTest> test = new ThreadLocal<ExtentTest>();

        [OneTimeSetUp]
        public void SetupReporting()
        {
            string folderPath = Path.Combine(TestContext.CurrentContext.WorkDirectory, "ExtentReports");
            Directory.CreateDirectory(folderPath);

            string reportPath = Path.Combine(folderPath, "Report.html");

            var htmlReporter = new ExtentHtmlReporter(reportPath);
            extent = new AventStack.ExtentReports.ExtentReports();
            extent.AttachReporter(htmlReporter);

            Console.WriteLine("Extent report path: " + reportPath);
        }

        [SetUp]
        public virtual void SetUp()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability("platformName", "Android");
            options.AddAdditionalCapability("deviceName", "Samsung Galaxy S22 Ultra");
            options.AddAdditionalCapability("os_version", "12.0");
            options.AddAdditionalCapability("app", "bs://4856e392ec3c5004e5e73fb1aa0017e9c83fa4fb");

            options.AddAdditionalCapability("browserstack.debug", "true");
            options.AddAdditionalCapability("browserstack.video", "true");
            options.AddAdditionalCapability("project", "Android App Test");
            options.AddAdditionalCapability("build", "Test Build");
            options.AddAdditionalCapability("name", TestContext.CurrentContext.Test.Name);

            driver = new AndroidDriver<AndroidElement>(
                new Uri("http://hub.browserstack.com/wd/hub"),
                options,
                TimeSpan.FromSeconds(60)
            );

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            test.Value = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            test.Value.Info("Starting test setup");

            Console.WriteLine("Created ExtentTest: " + TestContext.CurrentContext.Test.Name);

        }

        [TearDown]
        public virtual void TearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var errorMsg = TestContext.CurrentContext.Result.Message;

            if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                string screenshotPath = CaptureScreenshot(TestContext.CurrentContext.Test.Name);
                test.Value.Fail("Test Failed: " + errorMsg)
                    .AddScreenCaptureFromPath(screenshotPath);

                ((IJavaScriptExecutor)driver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"failed\", \"reason\": \"" + errorMsg + "\"}}");
            }
            else
            {
                test.Value.Pass("Test Passed");
                ((IJavaScriptExecutor)driver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"passed\", \"reason\": \"Test passed successfully\"}}");
            }

            driver?.Quit();
            Thread.Sleep(3000);
        }

        [OneTimeTearDown]
        public void TearDownReport()
        {
            Console.WriteLine("Flushing extent report...");

            extent.Flush();
        }

        private string CaptureScreenshot(string name)
        {
            string dir = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Screenshots");
            Directory.CreateDirectory(dir);
            string filePath = Path.Combine(dir, $"{name}_{DateTime.Now:yyyyMMdd_HHmmss}.png");
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(filePath, ScreenshotImageFormat.Png);

            return filePath;
        }
    }
}