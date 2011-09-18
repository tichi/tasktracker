using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskTracker.Models.ViewModels
{
    /**
     * \brief View model for UserController LogOn action.
     * \author Katharine Gillis
     * \date 2011-09-15
     * 
     * View model for UserController LogOn action.
     */
    public class UserLogOnViewModel
    {
        /**
         * \brief Get or set the username.
         * 
         * Get or set the username.
         */
        [Required(ErrorMessageResourceType = typeof(ModelRes.UserValidationStrings), ErrorMessageResourceName = "Required")]
        [Display(Name = "LogOnUserName", ResourceType = typeof(ModelRes.UserNameStrings))]
        public string UserName { get; set; }

        /**
         * \brief Get or set the password.
         * 
         * Get or set the password.
         */
        [Required(ErrorMessageResourceType = typeof(ModelRes.UserValidationStrings), ErrorMessageResourceName = "Required")]
        [DataType(DataType.Password)]
        [Display(Name = "LogOnPassword", ResourceType = typeof(ModelRes.UserNameStrings))]
        public string Password { get; set; }
    }

    /**
     * \brief View model for UserController Detail action.
     * \author Katharine Gillis
     * \date 2011-09-17
     * 
     * View model for UserController Detail action.
     */
    public class UserDetailViewModel
    {
        /**
         * \brief Get or set the username.
         * 
         * Get or set the username.
         */
        [Display(Name = "DetailUserName", ResourceType = typeof(ModelRes.UserNameStrings))]
        public string UserName { get; set; }

        /**
         * \brief Get or set the email.
         * 
         * Get or set the email.
         */
        [Display(Name = "DetailEmail", ResourceType = typeof(ModelRes.UserNameStrings))]
        public string Email { get; set; }

        /**
         * \brief Get or set the first name.
         * 
         * Get or set the first name.
         */
        [Display(Name = "DetailFirstName", ResourceType = typeof(ModelRes.UserNameStrings))]
        public string FirstName { get; set; }

        /**
         * \brief Get or set the last name.
         * 
         * Get or set the last name.
         */
        [Display(Name = "DetailLastName", ResourceType = typeof(ModelRes.UserNameStrings))]
        public string LastName { get; set; }

        /**
         * \brief Get or set the time zone.
         * 
         * Get or set the time zone.
         */
        [Display(Name = "DetailTimeZone", ResourceType = typeof(ModelRes.UserNameStrings))]
        public string TimeZone { get; set; }
    }
}