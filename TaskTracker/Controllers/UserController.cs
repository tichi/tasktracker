using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using TaskTracker.DB;
using TaskTracker.Models.Domain;
using TaskTracker.Models.Mapper;
using TaskTracker.Models.ViewModels;
using TaskTracker.Utils;

namespace TaskTracker.Controllers
{
    /**
     * \brief Controls the user pages.
     * \author Katharine Gillis
     * \date
     * 
     * Defines the actions for the user pages.
     */
    public class UserController : Controller
    {
        /**
         * \brief Serves the membership functionality.
         * 
         * Serves the membership functionality for the controller. By default, it is the MembershipProvider defined in Web.config.
         */
        public IMembershipService MembershipService
        {
            get;
            private set;
        }

        /**
         * \brief Serves the authentication functionality.
         * 
         * Serves the authentication functionality for the controller. By default, it is the FormsAuthentication.
         */
        public IAuthentication Auth
        {
            get;
            private set;
        }

        /**
         * \brief Default constructor.
         * 
         * Used by the MVC framework, and sets the MembershipService and FormsAuth as the defaults.
         */
        public UserController() : this(null, null) { }

        /**
         * \brief Testing constructor.
         * 
         * Used in testing, to allow injection of mocked authentication and membership services.
         * 
         * \param auth The mocked authentication service, or null for the default.
         * \param membershipService The mocked membership service, or null for the default.
         */
        public UserController(IAuthentication auth, IMembershipService membershipService)
        {
            this.Auth = auth ?? new FormsAuthenticationService { UsePersistentCookies = true };
            this.MembershipService = membershipService ?? new MembershipService();
        }

        /**
         * \brief LogOn action.
         * 
         * Returns the LogOn view.
         * 
         * GET: /User/Logon
         */
        public ActionResult LogOn()
        {
            return View("LogOn");
        }

        /**
         * \brief LogOn action.
         * 
         * If the model state is invalid, or the username and password do not validate, the LogOn view is returned with model errors. Otherwise a redirect action to the default url is returned.
         * 
         * POST: /User/Logon
         */
        [HttpPost]
        public ActionResult LogOn(UserLogOnViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View("LogOn", model);
            }

            if (String.IsNullOrEmpty(model.UserName))
            {
                this.ModelState.AddModelError("UserName", String.Format(ModelRes.UserValidationStrings.Required, ModelRes.UserNameStrings.LogOnUserName));
                
            }

            if (String.IsNullOrEmpty(model.Password))
            {
                this.ModelState.AddModelError("Password", String.Format(ModelRes.UserValidationStrings.Required, ModelRes.UserNameStrings.LogOnPassword));
            }

            if (this.MembershipService.ValidateUser(model.UserName, model.Password))
            {
                this.Auth.SignIn(model.UserName);

                return Redirect("~/");
            }

            this.ModelState.AddModelError("_FORM", ModelRes.UserValidationStrings.InvalidLogOn);
            return View("LogOn", model);
        }

        /**
         * \brief LogOff action.
         * 
         * Signs an authenticated user out, and redirects to the default url.
         * 
         * GET: /User/LogOff
         */
        public ActionResult LogOff()
        {
            this.Auth.SignOut();

            return Redirect("~/");
        }

        /**
         * \brief Detail view action.
         * 
         * Displays the requested user's detail view.
         * 
         * GET: /Detail/{id}
         */
        [Authorize]
        public ActionResult Detail(string id)
        {
            IUser user = this.MembershipService.GetUser(id);
            if (user == null)
            {
                throw new NoSuchRecordException();
            }

            UserDetailViewModel model = (new ModelMapper<IUser, UserDetailViewModel>()).Map(user);
            return View("Detail", model);
        }

    }

    /**
     * \brief Membership services interface.
     * \author Katharine Gillis
     * \date 2011-09-13
     * 
     * Defines the basic required members for a class to be considered a Membership service.
     */
    public interface IMembershipService
    {
        /**
         * \brief Determine if a username and password are valid.
         * 
         * Returns whether or not the username and password are valid.
         * 
         * \param userName The username.
         * \param password The password.
         * 
         * \return True if the user is valid, otherwise false.
         */
        bool ValidateUser(string userName, string password);

        /**
         * \brief Get a MembershipUser.
         * 
         * \param userId The user's ProviderUserKey.
         * 
         * \return A MembershipUser of the given key, or null if not found.
         */
        IUser GetUser(string userId, bool userIsOnline = false);
    }

    /**
     * \brief Membership services that use the Membership Provider.
     * \author Katharine Gillis
     * \date 2011-09-13
     * 
     * Defines a membership service that uses the Membership Provider as its data source.
     */
    public class MembershipService : IMembershipService
    {
        /**
         * \brief Determine if a username and password are valid.
         * 
         * Returns whether or not the username and password are valid.
         * 
         * \param userName The username.
         * \param password The password.
         * 
         * \return True if the user is valid, otherwise false.
         */
        public bool ValidateUser(string userName, string password)
        {
            return Membership.ValidateUser(userName, password);
        }

        /**
         * \brief Get a MembershipUser.
         * 
         * \param userId The user's ProviderUserKey.
         * 
         * \return A MembershipUser of the given key, or null if not found.
         */
        public IUser GetUser(string userId, bool userIsOnline = false)
        {
            return new User(Membership.GetUser(new Guid(userId), userIsOnline));
        }
    }

    /**
     * \brief Authentication service.
     * \author Katharine Gillis
     * \date 2011-09-13
     * 
     * Defines the basic required members of a authentication service.
     */
    public interface IAuthentication
    {
        /**
         * \brief Store an authenticated user.
         * 
         * Store authentication information for the given user.
         * 
         * \param userName The username.
         */
        void SignIn(string userName);

        /**
         * \brief Sign out the authenticated user.
         * 
         * Sign out the authenticated user.
         */
        void SignOut();
    }

    /**
     * \brief Authentication service that uses FormsAuthentication.
     * \author Katharine Gillis
     * \date 2011-09-13
     * 
     * Defines an authentication service that uses FormsAuthentication to store authenticated users.
     */
    public class FormsAuthenticationService : IAuthentication
    {
        /**
         * \brief Get or set whether to use persistent cookies.
         * 
         * Get or set whether to use persistent cookies.
         */
        public bool UsePersistentCookies
        {
            get;
            set;
        }

        /**
         * \brief Store an authenticated user.
         * 
         * Store authentication information for the given user in an encrypted cookie.
         * 
         * \param userName The username.
         */
        public void SignIn(string userName)
        {
            FormsAuthentication.SetAuthCookie(userName, this.UsePersistentCookies);
        }

        /**
         * \brief Sign out the authenticated user.
         * 
         * Sign out the authenticated user using FormsAuthentication.
         */
        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}
