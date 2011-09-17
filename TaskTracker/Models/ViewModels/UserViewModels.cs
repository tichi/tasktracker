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
        [Required(ErrorMessage = "User Name is required.")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        /**
         * \brief Get or set the password.
         * 
         * Get or set the password.
         */
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}