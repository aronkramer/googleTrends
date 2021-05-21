using BillTracker.DataBase;
using BillTracker.Excel;
using BillTracker.Helpers;
using BillTracker.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Globalization;
using System.Threading;

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
            if (kw.Asin == null && kw.SKU == null && kw.Keyword == null)
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

        //below here is where i need to focus
        [HttpPost]
        public static async Task<ActionResult> UploadExcel(HttpPostedFileBase file)
        {
            string helle = file.ToString();
            await UploadKWAsync(helle);
            return null;
        }

        public static async Task<ActionResult> UploadKWAsync(string filez)
        {
            var file = new FileInfo(filez);
            //var file = new FileInfo(@"C:\Users\talkp\OneDrive\Desktop\Topkeywordsq.xlsx");
            List<TopKeywordViewModels> kWFromExcel = await LoadExcelFile(file);
            var vv = kWFromExcel;
            return null;
        }

        private static async Task<List<TopKeywordViewModels>> LoadExcelFile(FileInfo file)
        {
            var output = new List<TopKeywordViewModels>();
            using (var package = new ExcelPackage(file))
            {
                await package.LoadAsync(file);
                var ws = package.Workbook.Worksheets[0];

                int row = 2;
                int col = 1;

                while (string.IsNullOrWhiteSpace(ws.Cells[row, col].Value?.ToString()) == false)
                {
                    var t = new TopKeywordViewModels();
                    t.SKU = ws.Cells[row, col].Value.ToString();
                    t.Asin = ws.Cells[row, col + 1].Value.ToString();
                    t.Keyword = ws.Cells[row, col + 2].Value.ToString();
                    output.Add(t);
                    row += 1;
                }
            }
            return output;
        }

        //we are done here
    }
}
