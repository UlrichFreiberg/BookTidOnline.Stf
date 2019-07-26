// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BookTidOnlineWebShellModelBase.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The im shell model base.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BookTidOnline.Stf.BookTidOnlineWeb
{
    using Mir.Stf.Utilities;

    using BookTidOnline.Stf.Adapters.WebAdapter;
    using BookTidOnline.Stf.BookTidOnlineWeb.Interfaces;

    /// <summary>
    /// The im shell model base.
    /// </summary>
    public abstract class BookTidOnlineWebShellModelBase : StfModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookTidOnlineWebShellModelBase"/> class.
        /// </summary>
        /// <param name="bookTidOnlineWebShell">
        /// The book Tid Online Web Shell.
        /// </param>
        protected BookTidOnlineWebShellModelBase(IBookTidOnlineWebShell bookTidOnlineWebShell)
        {
            BookTidOnlineWebShell = bookTidOnlineWebShell;
            WebAdapter = BookTidOnlineWebShell.WebAdapter;
        }

        /// <summary>
        /// Gets or sets the wrap track web shell.
        /// </summary>
        protected IBookTidOnlineWebShell BookTidOnlineWebShell { get; set; }

        /// <summary>
        /// Gets or sets the web adapter.
        /// </summary>
        protected IWebAdapter WebAdapter { get; set; }
    }
}