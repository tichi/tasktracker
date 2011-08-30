using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Management;

namespace TaskTracker.DB
{
    public static class ApplicationServices
    {
        readonly static string connectionString = WebConfigurationManager.ConnectionStrings["TaskTrackerContext"].ConnectionString;
        readonly static SqlConnectionStringBuilder myBuilder = new SqlConnectionStringBuilder(connectionString);

        public static void InstallServices(SqlFeatures sqlFeatures)
        {
            SqlServices.Install(myBuilder.InitialCatalog, sqlFeatures, connectionString);
        }

        public static void UninstallServices(SqlFeatures sqlFeatures)
        {
            SqlServices.Uninstall(myBuilder.InitialCatalog, sqlFeatures, connectionString);
        }

        public static string GetInitialCatalog()
        {
            return myBuilder.InitialCatalog;
        }
    }
}