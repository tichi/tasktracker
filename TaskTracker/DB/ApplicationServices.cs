using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Management;

namespace TaskTracker.DB
{
    /**
     * \brief Handles the Membership, Roles, and Profile services.
     * \author Katharine Gillis
     * \date 2011-09-14
     * 
     * Provides methods to install and uninstall the various services used by the application.
     */
    public static class ApplicationServices
    {
        readonly static string connectionString = WebConfigurationManager.ConnectionStrings["TaskTrackerContext"].ConnectionString;
        readonly static SqlConnectionStringBuilder myBuilder = new SqlConnectionStringBuilder(connectionString);

        /**
         * \brief Get the initial catalog.
         * 
         * Returns the name of the initial catalog defined in the Web.config.
         */
        public static string GetInitialCatalog()
        {
            return myBuilder.InitialCatalog;
        }
    }
}