using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Diagnostics;
using System.Linq.Dynamic.Core;
using WebApplicationReact.Models;
using Microsoft.Extensions.Logging;

namespace upromisapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IContractRepository Repository;
        private readonly ILogger Logger;

        public ContractController(IContractRepository repo, ILoggerProvider loggerProvider)
        {
            Repository = repo;
            Logger = loggerProvider.CreateLogger("HomeController");
        }

        [NonAction]
        private (IQueryable<T>, double) DoSortFilterAndPaging<T>(IQueryable<T> records, DataTableAjaxPostModel sentModel, bool doPaging)
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
            return Ok(new APIResult<Contract>() { ID = record.ID, DataSubject = record, Success = true, Message = "" });
        }

        [HttpGet("getcontractdata/{id}")]
        public ActionResult GetContractData(int id)
        {
            var record = Repository.Contracts.FirstOrDefault(d => d.ID == id);
            return Ok(new APIResult<Contract>() { ID = record.ID, DataSubject = record, Success = true, Message = "" });
        }

        [HttpPost("postonecontractdata")]
        [HttpPut("postonecontractdata")]
        [HttpDelete("postonecontractdata")]
        public ActionResult PostContractData([FromBody] SaveMessage<Contract> rec)
        {
            // var record = Repository.Contracts.FirstOrDefault(d => d.ID == rec.ID);
            Logger.Log(LogLevel.Information, rec.Action + "/" + rec.SubAction);
            // return posted values
            return Ok(new APIResult<Contract>() { ID = rec.ID, DataSubject = rec.DataSubject, Success = true, Message = rec.Action + " was performed." });
        }


        [HttpPost("getcontractdata")]
        public ActionResult GetContractData([FromBody] DataTableAjaxPostModel sentModel)
        {
            var records = DoSortFilterAndPaging(Repository.Contracts, sentModel, true);

            return Ok(new LoadResult<Contract>() { Data = records.Item1.ToArray(), Pages = records.Item2, Message = "" });
        }

        [HttpPost("getcontractdataexport")]
        public IActionResult GetContractDataExport([FromBody] DataTableAjaxPostModel sentModel)
        {
            using (var package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Contracts");
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

                var records = DoSortFilterAndPaging(Repository.Contracts, sentModel, false).Item1;

                foreach (var item in records)
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
            }
        }

        [Route("getselectvalues")]
        [HttpPost]
        public ActionResult GetSelectValues([FromBody] ListValueInfo info)
        {
            return Ok(new ListValues() { ValueType=info.ValueType, data = new List<ListValue>() { new ListValue() { Label = "Planned", Value = "Planned" }, new ListValue() { Label = "Open", Value = "Open" }, new ListValue() { Label = "Closed" , Value = "Closed" }  } });
        }


        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

    }
}
