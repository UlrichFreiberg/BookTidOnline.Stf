﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebAdapterButton.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the WebAdapter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BookTidOnline.Stf.Adapters.WebAdapter
{
    using OpenQA.Selenium;

    /// <summary>
    /// The web adapter.
    /// </summary>
    public partial class WebAdapter
    {
        /// <summary>
        /// The button click by xpath.
        /// </summary>
        /// <param name="xpath">
        /// The xpath.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool ButtonClickByXpath(string xpath)
        {
            var retVal = ButtonClickBy(By.XPath(xpath));

            return retVal;
        }

        /// <summary>
        /// The button click by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool ButtonClickById(string id)
        {
            var retVal = ButtonClickBy(By.Id(id));

            return retVal;
        }

        /// <summary>
        /// The button click by.
        /// </summary>
        /// <param name="by">
        /// The by.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool ButtonClickBy(By by)
        {
            var retVal = Click(by);

            return retVal;
        }
    }
}