using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.Profile;
using System.Web.Security;

namespace TaskTracker.DB
{
    /**
     * \brief Initializer that drops and recreates the database.
     * \author Katharine Gillis
     * \date 2011-09-14
     * 
     * If the model in code is different than the model defined in the database, the database is dropped and recreated.
     */
    public class TaskTrackerDropCreateDatabaseIfModelChanges : DropCreateDatabaseIfModelChanges<TaskTrackerContext>
    {
        /**
         * \brief Defines the seed data for the recreated database.
         *
         * Adds the seed data after the database is recreated, based on which database is in use.
         */
        protected override void Seed(TaskTrackerContext context)
        {
            // Create the default accounts and roles.
            if (ApplicationServices.GetInitialCatalog() == "tasktracker_testing")
            {
                if (Membership.GetUser("testuser", false) == null)
                {
                    Membership.CreateUser("testuser", "password", "testuser@test.com");
                    MembershipUser user = Membership.GetUser("testuser", false);
                    user.IsApproved = true;

                    var profile = ProfileBase.Create("testuser");
                    profile.SetPropertyValue("FirstName", "test");
                    profile.SetPropertyValue("LastName", "user");
                    profile.SetPropertyValue("TimeZone", "US Mountain Standard Time");
                    profile.Save();
                }
            }
        }
    }
}