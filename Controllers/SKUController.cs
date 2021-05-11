using BillTracker.DataBase;
using BillTracker.Excel;
using BillTracker.Helpers;
using BillTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BillTracker.Controllers
{
    [Authorize]
    public class SKUController : Controller
    {
        public ActionResult TopKw()
        {
            return View(GetTopKW());
        }

        public List<TopKeywordViewModels> GetTopKW()
        {
            var repo = new SkuRepository();
            return repo.GetAllNotDeleted().ToViewModel<TopKeywords, TopKeywordViewModels>().ToList();
        }

        public ActionResult GetTopKWForVue()
        {
            return Json(GetTopKW(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void DeleteById(int id)
        {
            var repo = new SkuRepository();
            repo.SoftDeleteSku(id);
            repo.SaveChanges();
        }

        [HttpPost]
        public ActionResult AddSku(TopKeywords kw)
        {
            var repo = new SkuRepository();
            repo.Add(kw);
            repo.SaveChanges();
            return RedirectToAction("TopKw");
        }

        [HttpPost]
        public ActionResult EditSku(TopKeywordViewModels kw, int theid)
        {
            if(kw.Asin == null && kw.SKU == null && kw.Keyword == null)
            {
                return RedirectToAction("TopKw");
            }
            var repo = new SkuRepository();
            var original = repo.Get(theid);
            if (kw.Asin == null) kw.Asin = original.Asin;
            if (kw.SKU == null) kw.SKU = original.SKU;
            if (kw.Keyword == null) kw.Keyword = original.Keyword;

            var updatekw = kw.ToViewModelSingle<TopKeywordViewModels, TopKeywords>();
            repo.EditSku(updatekw, theid);
            
            return RedirectToAction("TopKw");
        }

        public ActionResult DownloadKW()
        {
            TopKeywordsExcel excel = new TopKeywordsExcel();
            Response.ClearContent();
            Response.BinaryWrite(excel.GenerateExcel(GetTopKW()));
            Response.AddHeader("Content-disposition", "attachment; filename=Topkeywords.xlsx");
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.Flush();
            Response.End();
            return RedirectToAction("TopKw");
        }
    }
}
