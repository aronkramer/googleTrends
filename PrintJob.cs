using Hangfire.Annotations;
using Hangfire.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillTracker
{
    public class PrintJob
    {
        public static void Print()
        {
            new Controllers.TrendsController().HangfirePilot();
        }
    }

}