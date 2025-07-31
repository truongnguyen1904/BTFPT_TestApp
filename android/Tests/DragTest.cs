using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using BrowserStack.Pages;
using TestApp.Base;

namespace BrowserStack.Tests
{
    [TestFixture]
    //[Parallelizable(ParallelScope.All)]
    public class DragTest : TestBase
    {
        private DragPage dragPage;

        [Test]
        public void TestDragAndDrop()
        {
            dragPage = new DragPage(driver, wait);

            dragPage.GoToDragScreen();

            dragPage.DragAndDropColumn("l");
            dragPage.DragAndDropColumn("r");
            dragPage.DragAndDropColumn("c");

            string result = dragPage.GetCongratulationsText();
            Assert.That(result, Does.Contain("Congratulations"), "Lỗi không hiện thông báo");
        }
    }
}
