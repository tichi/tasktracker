using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskTracker.Utils
{
    /**
     * \Brief Base class for exceptions, to handle them with a different error page.
     * 
     * Base class for exceptions to be handled with a user-friendly error message.
     */
    public class TaskTrackerException : Exception
    {
        public TaskTrackerException(string message) : base(message) { }
    }

    /**
     * \brief Exception to throw when a requested record doesn't exist.
     * 
     * The exception to throw when a requested record doesn't exist.
     */
    public class NoSuchRecordException : TaskTrackerException
    {
        public NoSuchRecordException() : base(ErrorRes.ErrorStrings.NoSuchRecord) { }
    }
}