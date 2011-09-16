using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.Profile;
using System.Web.Security;

using TaskTracker.Models.Domain;

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
         * 
         * \param context The database context.
         */
        protected override void Seed(TaskTrackerContext context)
        {
            // Create the default accounts and roles.
            if (!Roles.RoleExists("Administrator"))
            {
                Roles.CreateRole("Administrator");
            }

            if (Membership.GetUser("admin", false) == null)
            {
                string adminPassword = ConfigurationManager.AppSettings["adminPassword"];
                Membership.CreateUser("admin", adminPassword, "gillis.katharine@gmail.com");
                MembershipUser user = Membership.GetUser("admin", false);
                user.IsApproved = true;

                Profile profile = Profile.GetProfile("admin");
                profile.FirstName = "Admin";
                profile.LastName = "User";
                profile.TimeZone = "US Mountain Standard Time";
                profile.Save();

                Roles.AddUserToRole("admin", "Administrator");
            }

            if (ApplicationServices.GetInitialCatalog() == "tasktracker_testing")
            {
                if (Membership.GetUser("testuser", false) == null)
                {
                    Membership.CreateUser("testuser", "password", "testuser@test.com");
                    MembershipUser user = Membership.GetUser("testuser", false);
                    user.IsApproved = true;

                    Profile profile = Profile.GetProfile("testuser");
                    profile.FirstName = "test";
                    profile.LastName = "user";
                    profile.TimeZone = "US Mountain Standard Time";
                    profile.Save();
                }
            }
        }
    }
}