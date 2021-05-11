using BillTracker.DataBase;
using BillTracker.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillTracker.Excel
{
    public class TrendsExcel
    {
        int rowIndex = 1;
        ExcelRange cell;

        public byte[] GenerateExcel(List<TrendsViewModels> trends)
        {
            using (var excelPackage = new ExcelPackage())
            {
                excelPackage.Workbook.Properties.Author = "PlatinumSupplies";
                excelPackage.Workbook.Properties.Title = "Google Trends";
                var sheet = excelPackage.Workbook.Worksheets.Add("Trends excel");
                var repo = new TrendsRepository();
                var date = repo.GetLatestUpdateTime();
                sheet.Name = $"Trends for {date}";
                sheet.Column(1).Width = 20;
                sheet.Column(2).Width = 15;
                sheet.Column(3).Width = 10;

                foreach (TrendsViewModels st in trends)
                {
                    
                    cell = sheet.Cells[rowIndex, 1];
                    cell.Value = st.Keyword;

                    cell = sheet.Cells[rowIndex, 2];
                    cell.Value = st.Date;

                    cell = sheet.Cells[rowIndex, 3];
                    cell.Value = st.Ranking;

                    rowIndex = rowIndex + 1;
                }
                return excelPackage.GetAsByteArray();
            }
        }
    }
}