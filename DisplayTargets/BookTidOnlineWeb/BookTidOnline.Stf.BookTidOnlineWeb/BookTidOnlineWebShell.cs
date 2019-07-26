// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BookTidOnlineWebShell.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the BookTidOnlineWebShell type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace BookTidOnline.Stf.BookTidOnlineWeb
{
    using BookTidOnline.Stf.Adapters.WebAdapter;
    using BookTidOnline.Stf.BookTidOnlineWeb.Configuration;
    using BookTidOnline.Stf.BookTidOnlineWeb.Interfaces;
    using BookTidOnline.Stf.Core;

    using OpenQA.Selenium;

    /// <summary>
    /// The demo corp web shell.
    /// </summary>
    public class BookTidOnlineWebShell : TargetBase, IBookTidOnlineWebShell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookTidOnlineWebShell"/> class. 
        /// </summary>
        public BookTidOnlineWebShell()
        {
            Name = "BookTidOnlineWebShell";
            VersionInfo = new Version(1, 0, 0, 0);
        }

        /// <summary>
        /// Gets or sets the wt configuration.
        /// </summary>
        public BtoWebConfiguration BtoWebConfiguration { get; set; }

        /// <summary>
        /// Gets or sets the web adapter.
        /// </summary>
        public IWebAdapter WebAdapter { get; set; }

        /// <summary>
        /// Gets or sets the current logged in user.
        /// </summary>
        public string CurrentLoggedInUser { get; set; }

        /// <summary>
        /// The login as admin. 
        /// Using the same Login routine as a normal login, but with different username password
        /// </summary>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool LoginAsAdmin(string userName = null, string password = null)
        {
            // Handle defaults for username password
            userName = HandleDefault(userName, BtoWebConfiguration.AdminUserName);
            password = HandleDefault(password, BtoWebConfiguration.AdminPassword);

            var retVal = Login(userName, password);

            return retVal;
        }

        public IMarket Market()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The learn more.
        /// </summary>
        /// <returns>
        /// Indication of success
        /// </returns>
        /// <summary>
        /// The login.
        /// </summary>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        public bool Login(string userName = null, string password = null)
        {
            // Handle defaults for username password
            userName = HandleDefault(userName, BtoWebConfiguration.UserName);
            password = HandleDefault(password, BtoWebConfiguration.Password);

            // fill in credentials
            WebAdapter.TextboxSetTextById("Email", userName);
            WebAdapter.TextboxSetTextById("password", password);

            // Click tab page Login
            WebAdapter.ButtonClickByXpath("//button[text()='Log in']");

            // Remember the last logged in user
            CurrentLoggedInUser = userName;

            return true;
        }

        /// <summary>
        /// The sign up functionality for new users
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool SignUp()
        {
            const string Password = "123456";
            var randomUsername = WtUtils.GetRandomUsername();
            var retVal = SignUp(randomUsername, Password);

            // Remember the logged in user
            CurrentLoggedInUser = randomUsername;

            return retVal;
        }

        /// <summary>
        /// The sign up.
        /// </summary>
        /// <param name="newUserName">
        /// The new user name.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool SignUp(string newUserName, string password)
        {
            WebAdapter.ButtonClickById("nav_login");
            WebAdapter.TextboxSetTextById("input_newuser", newUserName);
            WebAdapter.TextboxSetTextById("input_newPW", password);
            WebAdapter.TextboxSetTextById("input_email", newUserName + "@mitsite.org");
            WebAdapter.CheckBoxSetValueById("check_cond", true);
            WebAdapter.ButtonClickById("OpretProfilKnap");

            // when debugging, we probably want to get to the signed up user 
            StfLogger.LogKeyValue("SignUpUserName", newUserName, "SignUpUserName");
            StfLogger.LogKeyValue("SignUpPassword", password, "SignUpPassword");

            // Check If still on LOGIN page <h1>Login</h1> - if so then the signup failed
            var loginHeader = WebAdapter.FindElement(By.XPath("//h1[text='Login']"), 2);
            var retVal = loginHeader == null || CheckSignUpValidationMessages();

            ChooseEnglish();

            return retVal;
        }

        /// <summary>
        /// The logout.
        /// </summary>
        /// <param name="doCareAboutErrors">
        /// Mostly used in close down scenarios - there we just want to close down - not really caring about success or not
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Logout(bool doCareAboutErrors = true)
        {
            if (CurrentLoggedInUser == null)
            {
                return true;
            }

            CurrentLoggedInUser = null;

            try
            {
                if (WebAdapter.Click(By.XPath("//img[@alt='avatar']")))
                {
                    if (WebAdapter.Click(By.XPath("//a[@href='/Account/LogOff']")))
                    {
                        return true;
                    }
                }

                if (doCareAboutErrors)
                {
                    StfLogger.LogError("Got error while logging out");
                }
            }
            catch
            {
                // slurp
            }

            // if we cant click the logout button, then the return value is down to if we care or not:-)
            return doCareAboutErrors;
        }

        /// <summary>
        /// The text-feedback to user 
        /// </summary>
        /// <param name="infoType">
        /// The info Type.
        /// </param>
        /// <returns>
        /// True if text-feedback found
        /// </returns>
        public bool InfoText(string infoType)
        {
            try
            {
                var svar = WebAdapter.FindElement(By.Id(infoType));
                var retVal = svar != null;

                return retVal;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Logout and Close down the web adapter
        /// </summary>
        public void CloseDown()
        {
            Logout(false);
            WebAdapter.CloseDown();
        }

        /// <summary>
        /// The init.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Init()
        {
            var registerMyNeededTypes = new RegisterMyNeededTypes(this);

            registerMyNeededTypes.Register();
            BtoWebConfiguration = SetConfig<BtoWebConfiguration>();

            // get what I need - a WebAdapter:-)
            WebAdapter = StfContainer.Get<IWebAdapter>();

            WebAdapter.OpenUrl(BtoWebConfiguration.Url);

            var currentDomainBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            StfLogger.LogKeyValue("Current Directory", currentDomainBaseDirectory, "Current Directory");
            return true;
        }

        /// <summary>
        /// The sign up and login.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool SignUpAndLogin()
        {
            try
            {
                const string Password = "123456";
                var newUsername = WtUtils.GetRandomUsername();
                var signUpNewUser = SignUp(newUsername, Password);

                return signUpNewUser;
            }
            catch (Exception ex)
            {
                StfLogger.LogError($"Something went wrong when sign up and login. Message : [{ex.Message}]");
                throw;
            }
        }

        /// <summary>
        /// The check sign up validation messages.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool CheckSignUpValidationMessages()
        {
            var signUpValidationMessages = new List<string>
            {
                "Please read and approve the terms and conditions",
                "The username must be between four and 25 characters long. Only numbers, letters, and hyphen are accepted.",
                "The password must be at least five characters long.",
                "Please specify a valid e-mail address"
            };

            // we dont want to wait "long time" for each message, which we would if not initially wating 3 secs.. Now each can wait 1!
            WebAdapter.WaitForComplete(3);

            foreach (var signUpValidationMessage in signUpValidationMessages)
            {
                var xpath = $@"(//p[text()='{signUpValidationMessage}'])[1]";
                var validationMessageElement = WebAdapter.FindElement(By.XPath(xpath), 1);

                if (validationMessageElement == null)
                {
                    // found no error indication - All's well - so far...
                    continue;
                }

                StfLogger.LogError($"SignUp. There is validation error. Message : [{signUpValidationMessage}]");
                return false;
            }

            return true;
        }

        /// <summary>
        /// The handle default.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="defaultIfNull">
        /// The default if null.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string HandleDefault(string value, string defaultIfNull)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return defaultIfNull;
            }

            return value;
        }

        /// <summary>
        /// The choose english.
        /// </summary>
        private void ChooseEnglish()
        {
            const string Xpath = @"//sprog_valg//img[@src=""http://wt.troldvaerk.org/grafik/flag/2.svg""]";

            WebAdapter.ButtonClickByXpath(Xpath);
            WebAdapter.WaitForComplete(1);
        }
    }
}
