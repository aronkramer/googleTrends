using BillTracker.Helpers;
using BillTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BillTracker.DataBase
{
    public class SkuRepository : Repository<TopKeywords>
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public void SoftDeleteSku(int id)
        {
            context.Database.ExecuteSqlCommand("update TopKeywords set Deleted = 1 where Id = @Id", new SqlParameter("@Id", id));
        }

        public List<TopKeywords> GetAllNotDeleted()
        {
            return DbSet.Where(d => d.Deleted == false && d.Version == DbSet.Max(h => (int?)h.Version)).ToList();
        }

        public void EditSku(TopKeywords kw, int theid)
        {
            kw.Id = theid;
            context.Entry(kw).State = EntityState.Modified;
            context.SaveChanges();
        }

        public int LatestVersion()
        {
            return DbSet.Max(h => h.Version);
        }

        public void BulkEdit(IEnumerable<TopKeywords> kws)
        {
            context.TopKeywords.AddRange(kws);
            context.SaveChanges();
        }

        public string GetKeywords()
        {
            return String.Join(",", GetAllKeywords());
        }

        private List<string> GetAllKeywords()
        {
            return context.Database.SqlQuery<TopReportKWViewModel>("KeywordsForGoogleTrends").ToList().Select(s => s.SearchTerm).ToList();
            
            //return DbSet.Where(d => d.Deleted == false && d.Version == DbSet.Max(h => (int?)h.Version)).Select(k => k.Keyword).ToList();
        }
    }
}