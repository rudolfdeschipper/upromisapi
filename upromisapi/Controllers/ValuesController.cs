﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Diagnostics;
using System.Linq.Dynamic.Core;
using WebApplicationReact.Models;

namespace upromisapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private IContractRepository Repository;

        public HomeController(IContractRepository repo)
        {
            Repository = repo;

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
            return (records, sentModel?.pageSize != 0 ? Math.Ceiling((double)(filteredCount / sentModel.pageSize)) : 1.0);
        }

        [Route("getonecontractdata")]
        [HttpPost, HttpOptions]
        public ActionResult GetContractData([FromBody] RecordGetInfo rec)
        {
            var record = Repository.Contracts.FirstOrDefault(d => d.ID == rec.ID);
            return Ok(record);
        }

        [HttpGet("getcontractdata/{id}")]
        public ActionResult GetContractData(int id)
        {
            var record = Repository.Contracts.FirstOrDefault(d => d.ID == id);
            return Ok(record);
        }

        [HttpPost("putonecontractdata")]
        public ActionResult<OkResult> PutContractData([FromBody] Contract rec)
        {
            var record = Repository.Contracts.FirstOrDefault(d => d.ID == rec.ID);

            return Ok();
        }


        [HttpPost("getcontractdata")]
        public ActionResult GetContractData([FromBody] DataTableAjaxPostModel sentModel)
        {
            var records = GetContractDatafromDB(Repository.Contracts, sentModel, true);

            return Ok(new { data = records.Item1, pages = records.Item2 });
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

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}