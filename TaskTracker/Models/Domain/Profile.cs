using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Profile;

namespace TaskTracker.Models.Domain
{
    public class Profile : ProfileBase
    {
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

        public static Profile GetProfile(string username)
        {
            return Create(username) as Profile;
        }
    }
}