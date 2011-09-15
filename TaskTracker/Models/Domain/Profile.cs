using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Profile;

namespace TaskTracker.Models.Domain
{
    /**
     * \brief Serves the profile information for a user.
     * \author Katharine Gillis
     * \date 2011-09-15
     * 
     * Creates a strong model for the user profiles, with a datasource of the ProfileProvider.
     */
    public class Profile : ProfileBase
    {
        /**
         * \brief Gte or set the user's first name.
         * 
         * Get or set the user's first name.
         */
        public virtual string FirstName
        {
            get
            {
                return (this.GetPropertyValue("FirstName").ToString());
            }
            set
            {
                this.SetPropertyValue("FirstName", value);
            }
        }

        /**
         * \brief Get or set the user's last name.
         * 
         * Get or set the user's last name.
         */
        public virtual string LastName
        {
            get
            {
                return (this.GetPropertyValue("LastName").ToString());
            }
            set
            {
                this.SetPropertyValue("LastName", value);
            }
        }

        /**
         * \brief Get or set the user's time zone.
         * 
         * Get or set the user's time zone.
         */
        public virtual string TimeZone
        {
            get
            {
                return (this.GetPropertyValue("TimeZone").ToString());
            }
            set
            {
                this.SetPropertyValue("TimeZone", value);
            }
        }

        /**
         * \brief Get the specified user's profile.
         * 
         * Factory method for profiles. Returns the profile for the specified user.
         */
        public static Profile GetProfile(string username)
        {
            return Create(username) as Profile;
        }
    }
}