using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using TaskTracker.DB;
using TaskTracker.Models.ViewModels;

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
        public IFormsAuthentication FormsAuth
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
         * \param formsAuth The mocked authentication service, or null for the default.
         * \param membershipService The mocked membership service, or null for the default.
         */
        public UserController(IFormsAuthentication formsAuth, IMembershipService membershipService)
        {
            this.FormsAuth = formsAuth ?? new FormsAuthenticationService();
            this.MembershipService = membershipService ?? new UserMembershipService();
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
                this.ModelState.AddModelError("UserName", "UserName is required.");
            }

            if (String.IsNullOrEmpty(model.Password))
            {
                this.ModelState.AddModelError("Password", "Password is required.");
            }

            if (this.MembershipService.ValidateUser(model.UserName, model.Password))
            {
                this.FormsAuth.SetAuthCookie(model.UserName, true);

                return Redirect("~/");
            }

            this.ModelState.AddModelError("_FORM", "Invalid username or password.");
            return View("LogOn", model);
        }

        //
        // GET: /User/LogOff

        public ActionResult LogOff()
        {
            this.FormsAuth.SignOut();

            return Redirect("~/");
        }

    }

    public interface IMembershipService
    {
        bool ValidateUser(string userName, string password);
    }

    public class UserMembershipService : IMembershipService
    {
        private MembershipProvider provider;

        public UserMembershipService() : this(null) { }

        public UserMembershipService(MembershipProvider provider)
        {
            this.provider = provider ?? Membership.Provider;
        }

        public bool ValidateUser(string userName, string password)
        {
            return this.provider.ValidateUser(userName, password);
        }
    }

    public interface IFormsAuthentication
    {
        void SetAuthCookie(string userName, bool createPersistentCookie);

        void SignOut();
    }

    public class FormsAuthenticationService : IFormsAuthentication
    {
        public void SetAuthCookie(string userName, bool createPersistentCookie)
        {
            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}
