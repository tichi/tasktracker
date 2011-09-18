using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskTracker.Utils
{
    /**
     * \brief Exception to throw when a requested record doesn't exist.
     * 
     * The exception to throw when a requested record doesn't exist.
     */
    public class NoSuchRecordException : Exception
    {
        public NoSuchRecordException() : base("The requested record doesn't exist.") { }
    }
}