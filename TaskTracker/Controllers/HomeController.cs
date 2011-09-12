using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskTracker.Controllers
{
    /**
     * \brief Controls the home pages.
     * \author Katharine Gillis
     * \date 2011-09-11
     * 
     * Defines the actions for the Home pages.
     */
    public class HomeController : Controller
    {
        /**
         * \brief Index action.
         * 
         * This returns the default page for the site, as such it is the landing point for the viewers.
         * 
         * GET: /Home/
         * 
         * \return Index view as a ViewResult.
         */
        public ActionResult Index()
        {
            return View("Index");
        }

    }
}
