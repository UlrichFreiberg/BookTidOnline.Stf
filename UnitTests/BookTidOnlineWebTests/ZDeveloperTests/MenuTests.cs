// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuTests.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the UnitTest1 type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BookTidOnlineWebTests.ZDeveloperTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BookTidOnline.Stf.BookTidOnlineWeb;
    using BookTidOnline.Stf.BookTidOnlineWeb.Interfaces;

    /// <summary>
    /// The menu tests.
    /// </summary>
    [TestClass]
    public class MenuTests : BookTidOnlineTestScriptBase
    {
        /// <summary>
        /// The menu mananger.
        /// </summary>
        private MenuManager menuMananger;

        /// <summary>
        /// The test initialize.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            BookTidOnlineShell = Get<IBookTidOnlineWebShell>();
            menuMananger = new MenuManager(BookTidOnlineShell.WebAdapter);
        }

        /// <summary>
        /// The test top menu.
        /// </summary>
        [TestMethod]
        public void TestTopMenuNotLoggedIn()
        {
            GoMenu(TopMenu.Home);
            GoMenu(TopMenu.Explore);
            GoMenu(TopMenu.Market);
            GoMenu(TopMenu.FaqContact);

            Assert.IsFalse(false);
        }

        /// <summary>
        /// The test top menu logged in.
        /// </summary>
        [TestMethod]
        public void TestTopMenuLoggedIn()
        {
            BookTidOnlineShell.Login();

            GoMenu(TopMenu.Explore);
            GoMenu(TopMenu.Market);
            GoMenu(TopMenu.Me);
            GoMenu(TopMenu.FaqContact);

            Assert.IsFalse(false);
        }

        /// <summary>
        /// The test explore menu.
        /// </summary>
        [TestMethod]
        public void TestExploreMenu()
        {
            GoMenu(ExploreMenu.Wraps);
            GoMenu(ExploreMenu.Users);
            GoMenu(ExploreMenu.Brands);
            GoMenu(ExploreMenu.Patterns);
            GoMenu(ExploreMenu.Models);
            GoMenu(ExploreMenu.BirthWraps);
        }

        /// <summary>
        /// The go top menu.
        /// </summary>
        /// <param name="topMenu">
        /// The top menu.
        /// </param>
        private void GoMenu(TopMenu topMenu)
        {
            menuMananger.GoMenu(topMenu);
            Sleep(2);
        }

        /// <summary>
        /// The go menu.
        /// </summary>
        /// <param name="exploreMenu">
        /// The menu item.
        /// </param>
        private void GoMenu(ExploreMenu exploreMenu)
        {
            menuMananger.GoMenu(exploreMenu);
            Sleep(2);
        }

        /// <summary>
        /// The sleep.
        /// </summary>
        /// <param name="seconds">
        /// The seconds.
        /// </param>
        private void Sleep(int seconds)
        {
            System.Threading.Thread.Sleep(seconds * 1000);
        }
    }
}
