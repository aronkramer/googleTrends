using BillTracker.DataBase;
using BillTracker.Excel;
using BillTracker.Helpers;
using BillTracker.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BillTracker.Controllers
{
    [Authorize]
    public class TrendsController : Controller
    {
        public void HangfirePilot()
        {
            Index("startHF");
        }

        public ActionResult Index(string dataGetter)
        {
            ViewBag.LatestUpdate = new TrendsRepository().GetLatestUpdateTime();

            if (string.IsNullOrEmpty(dataGetter)) return View();

            var TheDefault = GetRoot().@default;
            //var doneMessage = Session["updateDone"];
            new TrendsRepository().DeleteOutdatedTrends();
            new EmailController().SendUpdateTrendsEmail();
            //return RedirectToAction("SendUpdateTrendsEmail", "Email");
            return View();
        }

        public List<TrendsViewModels> GetTrends()
        {
            var repo = new TrendsRepository();
            return repo.GetLatestTrends().ToViewModel<Trends, TrendsViewModels>().ToList();
        }

        public ActionResult DownloadTrends()
        {
            TrendsExcel excel = new TrendsExcel();
            Response.ClearContent();
            Response.BinaryWrite(excel.GenerateExcel(GetTrends()));
            Response.AddHeader("Content-disposition", "attachment; filename=Google Trends.xlsx");
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.Flush();
            Response.End();
            return RedirectToAction("Index");
        }

        public Root GetRoot()
        {
            // Needed if we start updating before a new day. we want all in this update to have the same day. 
            var today = DateTime.Today;

            var SkuRepository = new SkuRepository();
            var searchTerm1 = SkuRepository.GetAllKeywords();

            // taking the list and root out of the scoop to have access after
            var kw = new List<TrendsViewModels>();
            Root csharp = new Root();

            // Go through the list of keywords and get the trends for all

            foreach (var st in searchTerm1)
            {
                var result = Trends(st.SearchTerm);

                int num = 0;
                while (result == "" || result.Contains("302 Moved"))
                {
                    System.Threading.Thread.Sleep((num + 1) * 60 * 1000);
                    result = Trends(st.SearchTerm);
                    num++;
                    if (num == 5)
                    {
                        kw.Add(new TrendsViewModels { Keyword = $"Google crashed at searchterm {st}, try again!", UpdatedOn = today, NoData = true });
                        break;
                    }
                }
                if (result == "{\"default\":{\"timelineData\":[],\"averages\":[]}}")
                {
                    //kw.Add(new TrendsViewModels { Keyword = st.SearchTerm, UpdatedOn = today, NoData = true });
                }

                if (result != "" && !result.Contains("302 Moved"))
                {
                    csharp = JsonConvert.DeserializeObject<Root>(result);
                    foreach (var trends in csharp.@default.timelineData)
                    {
                        var date = trends.formattedAxisTime;
                        int rank = trends.value[0];
                        kw.Add(new TrendsViewModels {  Product = st.Sku ,Keyword = st.SearchTerm, Date = date, Ranking = rank, UpdatedOn = today, NoData = false });
                    }
                }
            }
            // end of loop to get all results

            var TrendsRepository = new TrendsRepository();
            var done = TrendsRepository.Add(kw);
            //Session["updateDone"] = done;

            return csharp;
        }
                
        static string Trends(string searchTerm)
        {
            if (searchTerm.Last() == ',')
            {
                searchTerm = searchTerm.Remove(searchTerm.Length - 1);
            }
            //System.Threading.Thread.Sleep(15 * 1000);
            var web = new WebClient();
            string dataResults = web.DownloadString($"https://loving-poitras-a08051.netlify.app/.netlify/functions/getTrends/?bob={searchTerm}");
            //string fakeResults = "{ \"default\":{ \"timelineData\":[{ \"time\":\"1578182400\",\"formattedTime\":\"Jan 5 – 11, 2020\",\"formattedAxisTime\":\"Jan 5, 2020\",\"value\":[76,30,59,13,8],\"hasData\": [true,true,true,true,true],\"formattedValue\":[\"76\",\"30\",\"59\",\"13\",\"8\"]},{ \"time\":\"1578787200\",\"formattedTime\":\"Jan 12 – 18, 2020\",\"formattedAxisTime\":\"Jan 12, 2020\",\"value\":[93,37,60,20,10],\"hasData\": [true,true,true,true,true],\"formattedValue\":[\"93\",\"37\",\"60\",\"20\",\"10\"]},{ \"time\":\"1579392000\",\"formattedTime\":\"Jan 19 – 25, 2020\",\"formattedAxisTime\":\"Jan 19, 2020\",\"value\":[100,35,71,17,10],\"hasData\":[true,true,true,true,true],\"formattedValue\":[\"100\",\"35\",\"71\",\"17\",\"10\"]},{ \"time\":\"1579996800\",\"formattedTime\":\"Jan 26 – Feb 1, 2020\",\"formattedAxisTime\":\"Jan 26, 2020\",\"value\":[84,36,51,18,13],\"hasData\":[true,true,true,true,true],\"formattedValue\":[\"84\",\"36\",\"51\",\"18\",\"13\"]},{ \"time\":\"1580601600\",\"formattedTime\":\"Feb 2 – 8, 2020\",\"formattedAxisTime\":\"Feb 2, 2020\",\"value\":[90,30,50,15,14],\"hasData\":[true,true,true,true,true],\"formattedValue\":[\"90\",\"30\",\"50\",\"15\",\"14\"]},{ \"time\":\"1615075200\",\"formattedTime\":\"Mar 7 – 13, 2021\",\"formattedAxisTime\":\"Mar 7, 2021\",\"value\":[62,26,52,23,10],\"hasData\":[true,true,true,true,true],\"formattedValue\":[\"62\",\"26\",\"52\",\"23\",\"10\"],\"isPartial\":true}],\"averages\":[78,31,58,29,14]} }";
            return dataResults;
        }
    }
}