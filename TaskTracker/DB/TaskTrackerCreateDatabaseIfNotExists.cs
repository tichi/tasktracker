using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Management;

namespace TaskTracker.DB
{
    public class TaskTrackerCreateDatabaseIfNotExists : CreateDatabaseIfNotExists<TaskTrackerContext>
    {
        protected override void Seed(TaskTrackerContext context)
        {
            // Set up the membership, roles, and profile systems.
            ApplicationServices.InstallServices(SqlFeatures.Membership | SqlFeatures.Profile | SqlFeatures.RoleManager);
        }
    }
}