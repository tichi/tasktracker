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
    public class TaskTrackerDropCreateDatabaseIfModelChanges : DropCreateDatabaseIfModelChanges<TaskTrackerContext>
    {
        protected override void Seed(TaskTrackerContext context)
        {
            // Set up the membership, roles, and profile systems.
            ApplicationServices.InstallServices(SqlFeatures.Membership | SqlFeatures.Profile | SqlFeatures.RoleManager);

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