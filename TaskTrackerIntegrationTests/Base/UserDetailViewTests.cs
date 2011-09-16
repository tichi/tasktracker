using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenQA.Selenium;

using NUnit.Framework;

namespace TaskTrackerIntegrationTests.Base
{
    abstract class UserDetailViewTests : BasicTestFixture
    {
        protected void NonAdministratorUser_CanNavigateToOwnUserDetailView()
        {
        }
    }
}
