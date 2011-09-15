using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Management;

namespace TaskTracker.DB
{
    /**
     * \brief Initializer that creates the database.
     * \author Katharine Gillis
     * \date 2011-09-14
     * 
     * A database initializer that creates the database if it does not already exist, and sets up the requested services.
     */
    public class TaskTrackerCreateDatabaseIfNotExists : CreateDatabaseIfNotExists<TaskTrackerContext>
    {
        /**
         * \brief Defines the seed data for the database.
         * 
         * Adds the seed data to the database after it has been created.
         * 
         * \param context The database context.
         */
        protected override void Seed(TaskTrackerContext context)
        {
            
        }
    }
}