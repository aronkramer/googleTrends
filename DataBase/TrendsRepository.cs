using BillTracker.Helpers;
using BillTracker.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BillTracker.DataBase
{
    public class TrendsRepository : Repository<Trends>
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public string Add(List<TrendsViewModels> ListTrends)
        {
            var dbList = ListTrends.ToViewModel<TrendsViewModels, Trends>();
            context.Trends.AddRange(dbList);
            context.SaveChanges();
            return "Update complete";
        }

        public List<Trends> GetLatestTrends()
        {
            return context.Trends.SqlQuery("SELECT * FROM Trends WHERE UpdatedOn IN(SELECT max(UpdatedOn) FROM Trends);").ToList();
        }

        public string GetLatestUpdateTime() => context.Database.SqlQuery<DateTime>("SELECT distinct UpdatedOn FROM Trends WHERE UpdatedOn IN (SELECT max(UpdatedOn) FROM Trends);").FirstOrDefault().ToString();

        public void DeleteOutdatedTrends()
        {
            context.Trends.SqlQuery("Delete from Trends WHERE UpdatedOn <>(SELECT max(UpdatedOn) FROM Trends)");
            context.SaveChanges();
        }
    }
}