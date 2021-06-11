using BillTracker.Models;
using EntityFramework.BulkInsert.Extensions;
using EntityFramework.BulkInsert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace BillTracker.DataBase
{
    public class SearchTermRepository : Repository<SearchTermReport>
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public void ResetSearchTermReport(IEnumerable<SearchTermReport> searchTerms)
        {
            context.Database.ExecuteSqlCommand("Truncate Table SearchTermReports");
            context.SearchTermReport.AddRange(searchTerms);
            context.SaveChanges();
        }

    }
}

