// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestCase001.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Login Tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BookTidOnlineWebTests
{
    using BookTidOnline.Stf.BookTidOnlineWeb.Interfaces;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Mir.Stf.Utilities;

    /// <summary>
    /// The main page tests.
    /// </summary>
    [TestClass]
    public class TestCase002 : BookTidOnlineTestScriptBase
    {
        /// <summary>
        /// The wt test script utils.
        /// </summary>
        private BtoTestScriptUtils btoTestScriptUtils;

        /// <summary>
        /// The test initialize.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            BookTidOnlineShell = Get<IBookTidOnlineWebShell>();
            btoTestScriptUtils = new BtoTestScriptUtils(StfLogger);
        }

        /// <summary>
        /// The log in test.
        /// </summary>
        [TestMethod]
        public void Tc001()
        {
            // Use default user
            BookTidOnlineShell.Login(); 

            StfAssert.IsNotNull("BookTidOnlineShell", BookTidOnlineShell);
            StfLogger.LogScreenshot(StfLogLevel.Info, "Logged in");
        }
    }
}
