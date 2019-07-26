// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegisterMyNeededTypes.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the BookTidOnlineWebShell type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BookTidOnline.Stf.BookTidOnlineWeb
{
    using Mir.Stf.Utilities;

    /// <summary>
    /// The demo corp web shell.
    /// </summary>
    public class RegisterMyNeededTypes
    {
        /// <summary>
        /// The stf container used to register the types for the shell
        /// </summary>
        private readonly IStfContainer stfContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterMyNeededTypes"/> class.
        /// </summary>
        /// <param name="wtShell">
        /// The wt shell.
        /// </param>
        public RegisterMyNeededTypes(Interfaces.IBookTidOnlineWebShell wtShell)
        {
            stfContainer = wtShell.StfContainer;
        }

        /// <summary>
        /// The register.
        /// </summary>
        public void Register()
        {
        }

    }
}
