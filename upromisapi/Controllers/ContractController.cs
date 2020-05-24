using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Linq.Dynamic.Core;
using upromiscontractapi.Models;
using Microsoft.Extensions.Logging;
using APIUtils.APIMessaging;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace upromiscontractapi.Controllers
{
    // TODO: add authorisation

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

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var res = await Repository.Get(id);
            return Ok(res);
        }

        [HttpPost()]
        public async Task<ActionResult> Post([FromBody] SaveMessage<ContractDTO> rec)
        {
            APIResult<ContractDTO> res;

            Logger.Log(LogLevel.Information, rec.Action + "/" + rec.SubAction);

            try
            {
                res = await Repository.Post(rec);
            }
            catch (Exception ex)
            {
                return this.Problem(ex.Message, GetType().Name, 500, "Error");
            }

            // return posted values
            return Ok(res);
        }

        [HttpPut()]
        public async Task<ActionResult> Put([FromBody] SaveMessage<ContractDTO> rec)
        {   
            APIResult<ContractDTO> res;

            Logger.Log(LogLevel.Information, rec.Action + "/" + rec.SubAction);

            try
            {
                res = await Repository.Put(rec);
            }
            catch (Exception ex)
            {
                return this.Problem(ex.Message, GetType().Name, 500, "Error");
            }

            // return posted values
            return Ok(res);
        }

        [HttpDelete()]
        public async Task<ActionResult> Delete([FromBody] SaveMessage<ContractDTO> rec)
        {
            APIResult<ContractDTO> res;

            Logger.Log(LogLevel.Information, rec.Action + "/" + rec.SubAction);

            try
            {
                res = await Repository.Delete(rec);
            }
            catch (Exception ex)
            {
                return this.Problem(ex.Message, GetType().Name, 500, "Error");
            }

            // return posted values
            return Ok(res);
        }

        // TODO: transform into a get with a body (this is possible)
        [HttpPost("getlist")]
        public ActionResult GetList([FromBody] DataTableAjaxPostModel sentModel)
        {
            var records = DoSortFilterAndPaging(Repository.List, sentModel, true);

            return Ok(new LoadResult<ContractDTO>() { Data = records.Item1.ToArray(), Pages = records.Item2, Message = "" });
        }

        // TODO: this can be a normal "get", with a filter on the header " 'Content-Type': 'application/excel' or something
        [HttpPost("getforexport")]
        public IActionResult GetForExport([FromBody] DataTableAjaxPostModel sentModel)
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

                var records = DoSortFilterAndPaging(Repository.List, sentModel, false).Item1;

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

        // TODO: make into a get with a body
        [Route("getselectvalues")]
        [HttpPost]
        public ActionResult GetSelectValues([FromBody] ListValueInfo info)
        {
            List<ListValue> list = new List<ListValue>();
            Type t = null;

            switch (info.ValueType)
            {
                case "ContractType":
                    t = typeof(ContractType);
                    break;
                case "PaymentStatus":
                    t = typeof(ContractPaymentStatus);
                    break;
                case "RequestType":
                    t = typeof(RequestType);
                    break;
                case "ContractStatus":
                    list =  new List<ListValue>() { new ListValue() { Label = "Planned", Value = "Planned" }, new ListValue() { Label = "Open", Value = "Open" }, new ListValue() { Label = "Closed", Value = "Closed" } };
                    break;
                default:
                    break;
            }
            if (t != null)
            {
                foreach (var item in Enum.GetValues(t))
                {
                    list.Add(new ListValue() { Label = Enum.GetName(t, item), Value = item });
                }
            }

            return Ok(new ListValues() { ValueType = info.ValueType, data = list });
        }

        [HttpGet("getclaims/{UserID}")]
        [Authorize()]
        public IActionResult GetClaims(string UserID)
        {
            var claims = Repository.List.Where(c => c.TeamComposition.Any(t => t.TeamMember.ToString().Equals(UserID))).Select(c => new { Key = "Contract|" + c.ID, Value = c.TeamComposition.First(t => t.TeamMember.ToString().Equals(UserID)).MemberType.ToString() });
            return new JsonResult(claims);
        }

    }
}
