using BillTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillTracker.DataBase
{
    public class ReportsRepository : Repository<TopKeywords>
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public void insert(IEnumerable<CampaignsReport> campaigns)
        {
            var v = campaigns;
            string hello = "";
        }
    }
}