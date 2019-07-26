// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BtoWebConfiguration.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//       http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the BtoWebConfiguration type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BookTidOnline.Stf.BookTidOnlineWeb.Configuration
{
    using Mir.Stf.Utilities;

    /// <summary>
    /// The im configuration.
    /// </summary>
    public class BtoWebConfiguration
    {
        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        [StfConfiguration("DisplayTargets.BookTidOnlineWeb.BaseUrl")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the browser.
        /// </summary>
        [StfConfiguration("DisplayTargets.BookTidOnlineWeb.Browser")]
        public string Browser { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        [StfConfiguration("DisplayTargets.BookTidOnlineWeb.Users.UserName")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [StfConfiguration("DisplayTargets.BookTidOnlineWeb.Users.Password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the Admin user name.
        /// </summary>
        [StfConfiguration("DisplayTargets.BookTidOnlineWeb.AdminUsers.UserName")]
        public string AdminUserName { get; set; }

        /// <summary>
        /// Gets or sets the Admin password.
        /// </summary>
        [StfConfiguration("DisplayTargets.BookTidOnlineWeb.AdminUsers.Password")]
        public string AdminPassword { get; set; }
    }
}
