using NUnit.Framework;
using BrowserStack.Pages;
using TestApp.Base;

namespace BrowserStack.Tests
{
    [TestFixture]
    //[Parallelizable(ParallelScope.All)]
    public class SwipeToSupportVideoTest : TestBase
    {
        private SwipePage swipePage;

        [Test]
        public void TestSwipeUntilSupportVideoVisible()
        {
            swipePage = new SwipePage(driver, wait);

            swipePage.GoToSwipeTab();

            bool found = swipePage.SwipeUntilSupportVideoVisible();

            Assert.IsTrue(found, "Không tìm thấy SUPPORT VIDEOS sau khi vuốt");
        }
    }
}
