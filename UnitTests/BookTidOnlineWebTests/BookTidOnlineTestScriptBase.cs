// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BookTidOnlineTestScriptBase.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the BookTidOnlineTestScriptBase type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BookTidOnlineWebTests
{
    using System;

    using BookTidOnline.Stf.BookTidOnlineWeb.Interfaces;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Mir.Stf;

    /// <summary>
    /// The wrap track test script base.
    /// </summary>
    public abstract class BookTidOnlineTestScriptBase : StfTestScriptBase
    {
        /// <summary>
        /// Gets or sets the wrap track shell.
        /// </summary>
        protected IBookTidOnlineWebShell BookTidOnlineShell { get; set; }

        /// <summary>
        /// The test cleanup.
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            // ensure to logout and to close down the web adapter
            BookTidOnlineShell?.CloseDown();
        }

        /// <summary>
        /// The get another user.
        /// </summary>
        /// <param name="BookTidOnlineWebShell">
        /// The wrap Track Web Shell.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        protected string GetAnotherUser(IBookTidOnlineWebShell BookTidOnlineWebShell = null)
        {
            var currentShell = BookTidOnlineWebShell ?? BookTidOnlineShell;
            var currentUser = currentShell.CurrentLoggedInUser;
            var retVal = "mie88";

            if (currentUser.Equals("mie88", StringComparison.InvariantCultureIgnoreCase))
            {
                retVal = "ida88";
            }

            return retVal;
        }

        /// <summary>
        /// Function used to wait a period of time.
        /// </summary>
        /// <param name="duration">
        /// The duration.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected bool Wait(TimeSpan duration)
        {
            System.Threading.Thread.Sleep(duration);

            return true;
        }
    }
}
