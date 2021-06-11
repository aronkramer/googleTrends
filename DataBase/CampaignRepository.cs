using BillTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using EntityFramework.BulkInsert.Extensions;

namespace BillTracker.DataBase
{
    public class CampaignRepository : Repository<CampaignsReport>
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public void ResetCampaignReport(IEnumerable<CampaignsReport> campaigns)
        {
            context.Database.ExecuteSqlCommand("Truncate Table CampaignsReports");
            context.CampaignsReport.AddRange(campaigns);
            context.SaveChanges();
        }

        public void CleanReport()
        {
            context.Database.ExecuteSqlCommand("exec deleteDoubles");
        }
    }
}