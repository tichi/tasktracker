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
        [Required(ErrorMessageResourceType=typeof(ModelRes.ValidationStrings), ErrorMessageResourceName="Required")]
        [Display(Name="LogOnUserName", ResourceType=typeof(ModelRes.NameStrings))]
        public string UserName { get; set; }

        /**
         * \brief Get or set the password.
         * 
         * Get or set the password.
         */
        [Required(ErrorMessageResourceType = typeof(ModelRes.ValidationStrings), ErrorMessageResourceName = "Required")]
        [DataType(DataType.Password)]
        [Display(Name="LogOnPassword", ResourceType=typeof(ModelRes.NameStrings))]
        public string Password { get; set; }
    }
}