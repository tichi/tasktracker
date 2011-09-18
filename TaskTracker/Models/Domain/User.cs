using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace TaskTracker.Models.Domain
{
    /**
     * \brief User interface.
     * \author Katharine Gillis
     * \date 2011-09-17
     * 
     * Defines the basic required members of a user.
     */
    public interface IUser
    {
        /**
         * \brief Get or set the UserName.
         * 
         * Get or set the UserName.
         */
        string UserName { get; }

        /** brief Get or set the Email.
         * 
         * Get or set the Email.
         */
        string Email { get; set; }

        /**
         * \brief Get or set the FirstName.
         * 
         * Get or set the FirstName.
         */
        string FirstName { get; set; }

        /**
         * \brief Get or set the LastName.
         * 
         * Get or set the LastName.
         */
        string LastName { get; set; }

        /**
         * \brief Get or set the TimeZone.
         * 
         * Get or set the TimeZone.
         */
        string TimeZone { get; set; }
    }

    /**
     * \brief CombinedMembershipUser that uses MembershipUser and Profile as a data source.
     * \author Katharine Gillis
     * \date 2011-09-17
     * 
     * CombinedMembershipUser manages user data with a MembershipUser and it's corresponding Profile as the data source.
     */
    public class User : IUser
    {
        MembershipUser user;
        Profile profile;

        /**
         * \brief Default constructor.
         * 
         * Takes a MembershipUser, sets it as a data store, and gets it's corresponding Profile.
         */
        public User(MembershipUser user)
        {
            this.user = user;
            this.profile = Profile.GetProfile(user.UserName);
        }

        /**
         * \brief Get or set the UserName.
         * 
         * Get or set the UserName.
         */
        public string UserName
        {
            get
            {
                return this.user.UserName;
            }
        }

        /** brief Get or set the Email.
        * 
        * Get or set the Email.
        */
        public string Email
        {
            get
            {
                return this.user.Email;
            }
            set
            {
                this.user.Email = value;
            }
        }

        /**
         * \brief Get or set the FirstName.
         * 
         * Get or set the FirstName.
         */
        public string FirstName
        {
            get
            {
                return this.profile.FirstName;
            }
            set
            {
                this.profile.FirstName = value;
                this.profile.Save();
            }
        }

        /**
         * \brief Get or set the LastName.
         * 
         * Get or set the LastName.
         */
        public string LastName
        {
            get
            {
                return this.profile.LastName;
            }
            set
            {
                this.profile.LastName = value;
                this.profile.Save();
            }
        }

        /**
         * \brief Get or set the TimeZone.
         * 
         * Get or set the TimeZone.
         */
        public string TimeZone
        {
            get
            {
                return this.profile.TimeZone;
            }
            set
            {
                this.profile.TimeZone = value;
                this.profile.Save();
            }
        }
    }
}