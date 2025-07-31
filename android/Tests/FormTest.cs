using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using BrowserStack.Pages;
using TestApp.Base;

namespace BrowserStack.Tests
{
    [TestFixture]
    //[Parallelizable(ParallelScope.All)]
    public class FormsTest : TestBase
    {
        private FormsPage formsPage;

        [Test]
        public void TestFormsSubmission()
        {
            formsPage = new FormsPage(driver, wait);
            formsPage.OpenForms();

            formsPage.EnterText("Nhập test");
            Assert.AreEqual("Nhập test", formsPage.GetInputResult(), "Text input không khớp");

            formsPage.ToggleSwitch();

            formsPage.TapActiveButton();

            var alertTitle = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("android:id/alertTitle")));
            Assert.AreEqual("This button is", alertTitle.Text, "Tiêu đề cảnh báo không khớp");

            formsPage.AcceptAlert();
        }
    }
}
