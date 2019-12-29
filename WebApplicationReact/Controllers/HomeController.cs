using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic.Core;
using WebApplicationReact.Models;

namespace WebApplicationReact.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private IContractRepository Repository;

        public HomeController(IContractRepository repo)
        {
            Repository = repo;

        }

        public IActionResult Index()
        {
            return View();
        }

        [NonAction]
        private (IQueryable<Contract>, double) GetContractDatafromDB(IQueryable<Contract> records, DataTableAjaxPostModel sentModel, bool doPaging)
        {
            string whereClause = String.Empty;
            int filteredCount = records.Count();

            if (sentModel != null)
            {
                // filtering
                // column search is handled here:0
                foreach (var item in sentModel.filtered)
                {
                    if (!String.IsNullOrEmpty(item.value))
                    {
                        whereClause = whereClause + item.id + ".ToString().Contains(\"" + item.value + "\") &&";
                    }
                }
                if (!string.IsNullOrEmpty(whereClause))
                {
                    whereClause = whereClause.Substring(0, whereClause.Length - 2);
                    records = records.Where(whereClause);
                    filteredCount = records.Count();
                }

                // ordering
                if (sentModel.sorted.Any())
                {
                    string orderBy = "";
                    foreach (var o in sentModel.sorted)
                    {
                        orderBy += " " + o.id + (o.desc ? " DESC" : " ASC") + ",";
                    }
                    if (orderBy.EndsWith(','))
                    {
                        orderBy = orderBy.Substring(0, orderBy.Length - 1);
                    }
                    if (!string.IsNullOrEmpty(orderBy))
                    {
                        records = records.OrderBy(orderBy);
                    }
                }

                // paging:
                if (doPaging)
                {
                    records = records
                        .Skip(sentModel.page * sentModel.pageSize)
                        .Take(sentModel.pageSize);
                }
            }
            return (records, sentModel?.pageSize != 0 ? Math.Ceiling((double)(filteredCount / sentModel.pageSize)) : 1.0 );
        }

        [HttpPost("getonecontractdata")]
        public JsonResult GetContractData([FromBody] RecordGetInfo rec)
        {
            var record = Repository.Contracts.FirstOrDefault( d => d.ID == rec.ID);

            return Json(record);
        }

        [HttpPost("putonecontractdata")]
        public JsonResult PutContractData([FromBody] Contract rec)
        {
            var record = Repository.Contracts.FirstOrDefault(d => d.ID == rec.ID);

            return Json(Ok());
        }


        [HttpPost("getcontractdata")]
        public JsonResult GetContractData([FromBody] DataTableAjaxPostModel sentModel)
        {
            var records = GetContractDatafromDB(Repository.Contracts, sentModel, true);

            return Json(new { rows = records.Item1, pages = records.Item2 });
        }

        [HttpPost("getcontractdataexport")]
        public IActionResult GetContractDataExport([FromBody] DataTableAjaxPostModel sentModel)
        {
            using (var package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Inventory");
                //Add the headers
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "Code";
                worksheet.Cells[1, 3].Value = "Title";
                worksheet.Cells[1, 4].Value = "Description";
                worksheet.Cells[1, 5].Value = "Start date";
                worksheet.Cells[1, 6].Value = "End date";
                worksheet.Cells[1, 7].Value = "Value";
                worksheet.Cells[1, 1, 1, 7].Style.Font.Bold = true;

                int row = 2;

                var records = Repository.Contracts;

                foreach (var item in GetContractDatafromDB(records, sentModel, false).Item1)
                {
                    worksheet.Cells[row, 1].Value = item.ID;
                    worksheet.Cells[row, 2].Value = item.Code;
                    worksheet.Cells[row, 3].Value = item.Title;
                    worksheet.Cells[row, 4].Value = item.Description;
                    worksheet.Cells[row, 5].Value = item.StartDate;
                    worksheet.Cells[row, 5].Style.Numberformat.Format = "dd/MMM/yyyy";
                    worksheet.Cells[row, 6].Value = item.EndDate;
                    worksheet.Cells[row, 6].Style.Numberformat.Format = "dd/MMM/yyyy";
                    worksheet.Cells[row, 7].Value = item.Value;
                    worksheet.Cells[row, 7].Style.Numberformat.Format = "€ #,##0.00";
                    row++;
                }
                System.IO.MemoryStream fs = new System.IO.MemoryStream();
                package.SaveAs(fs);

                return File(fs.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                //return this.File(fs, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Contract data export " + DateTime.Today.ToString("yyyyMMdd"));
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
