/*
**             ------ IMPORTANT ------
** This file was generated by ZeroCode2 on 28/Dec/2020 21:46:18
** DO NOT MODIFY IT, as it can be regenerated at any moment.
** If you need this file changed, change the underlying model or its template.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Linq.Dynamic.Core;
using upromiscontractapi.Models;
using Microsoft.Extensions.Logging;
using APIUtils;
using APIUtils.APIMessaging;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace upromiscontractapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RequestController : ControllerBase
    {
        private readonly IRequestRepository Repository;
        private readonly ILogger Logger;

        public RequestController(IRequestRepository repo, ILoggerProvider loggerProvider)
        {
            Repository = repo;
            Logger = loggerProvider.CreateLogger("RequestController");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var res = await Repository.Get(id);

            if (res == null)
            {
                return NotFound(new APIResult<RequestDTO>() { ID = id, DataSubject = null, Message = "Get failed" });
            }

            var record = Transformers.Transform(res, new RequestDTO() { Modifier = "Unchanged" });

            return Ok(new APIResult<RequestDTO>() { ID = id, DataSubject = record, Message = "Get was performed" });
        }

        [HttpPost()]
        public async Task<ActionResult> Post([FromBody] SaveMessage<RequestDTO> rec)
        {
            RequestDTO res;

            Logger.Log(LogLevel.Information, rec.Action + "/" + rec.SubAction);

            try
            {
                Request record = Transformers.Transform(rec.DataSubject, new Request()) as Request;

                record = await Repository.Post(record);
                res = Transformers.Transform(record, new RequestDTO());
            }
            catch (Exception ex)
            {
                return this.Problem(ex.Message, GetType().Name, 500, "Error");
            }

            // return posted values
            return Ok(new APIResult<RequestDTO>() { ID = res.ID, DataSubject = res, Message = "New Request was created." });
        }

        [HttpPut()]
        public async Task<ActionResult> Put([FromBody] SaveMessage<RequestDTO> rec)
        {
            RequestDTO res;

            Logger.Log(LogLevel.Information, rec.Action + "/" + rec.SubAction);

            try
            {
                Request originalRecord = await Repository.Get(rec.ID);

                if (originalRecord == null)
                {
                    return NotFound(new APIResult<RequestDTO>() { ID = rec.ID, DataSubject = null, Message = "Put failed - record is not found" });
                }

                Request record = Transformers.Transform(rec.DataSubject, originalRecord) as Request;

                record = await Repository.Put(record);

                res = Transformers.Transform(record, new RequestDTO());

            }
            catch (Exception ex)
            {
                return this.Problem(ex.Message, GetType().Name, 500, "Error");
            }

            // return posted values
            return Ok(new APIResult<RequestDTO>() { ID = res.ID, DataSubject = res, Message = "Request was saved." });
        }

        [HttpDelete()]
        public async Task<ActionResult> Delete([FromBody] SaveMessage<RequestDTO> rec)
        {
            bool res;

            Logger.Log(LogLevel.Information, rec.Action + "/" + rec.SubAction);

            try
            {
                Request record = Transformers.Transform(rec.DataSubject, new Request()) as Request;

                res = await Repository.Delete(record);

                if (res == false)
                {
                    return NotFound(new APIResult<RequestDTO>() { ID = rec.ID, DataSubject = null, Message = "Delete failed - record not found" });
                }
            }
            catch (Exception ex)
            {
                return this.Problem(ex.Message, GetType().Name, 500, "Error");
            }

            // return 
            return Ok(new APIResult<RequestDTO>() { ID = rec.ID, DataSubject = null, Message = "Request was deleted." });
        }

        // TODO: transform into a get with a body (this is possible)
        [HttpPost("getlist")]
        public async Task<ActionResult> GetList([FromBody] SortAndFilterInformation sentModel)
        {
            var records = await Repository.FilteredAndSortedList(sentModel, true);

            var recordsDTO = records.Item1.Select(c => Transformers.Transform(c, new RequestDTO() { Modifier = "Unchanged" }));

            return Ok(new LoadResult<RequestDTO>() { Data = recordsDTO.ToArray(), Pages = records.Item2, Message = "" });
        }

        // TODO: this can be a normal "get", with a filter on the header " 'Content-Type': 'application/excel' or something
        [HttpPost("getforexport")]
        public async Task<IActionResult> GetForExport([FromBody] SortAndFilterInformation sentModel)
        {
            using ExcelPackage package = new ExcelPackage();
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Request List");
            //Add the headers
            int col = 0;
            int row = 1;
            col++;
            worksheet.Cells[row, col].Value = "ID";
            col++;
            worksheet.Cells[row, col].Value = "Code";
            col++;
            worksheet.Cells[row, col].Value = "Title";
            col++;
            worksheet.Cells[row, col].Value = "Description";
            col++;
            worksheet.Cells[row, col].Value = "Start date";
            col++;
            worksheet.Cells[row, col].Value = "End date";
            col++;
            worksheet.Cells[row, col].Value = "Status";
            col++;
            worksheet.Cells[row, col].Value = "Request type";
            worksheet.Cells[1, 1, 1, col].Style.Font.Bold = true;

            var records = (await Repository.FilteredAndSortedList(sentModel, true)).Item1;

            foreach (var item in records)
            {
                row++;
                col = 1;
                worksheet.Cells[row, col].Value = item.ID;
                col++;
                worksheet.Cells[row, col].Value = item.Code;
                col++;
                worksheet.Cells[row, col].Value = item.Title;
                col++;
                worksheet.Cells[row, col].Value = item.Description;
                col++;
                worksheet.Cells[row, col].Value = item.Startdate;
                worksheet.Cells[row, col].Style.Numberformat.Format = "MM/DD/YYYY";
                col++;
                worksheet.Cells[row, col].Value = item.Enddate;
                worksheet.Cells[row, col].Style.Numberformat.Format = "MM/DD/YYYY";
                col++;
                worksheet.Cells[row, col].Value = item.RequestStatus;
                col++;
                worksheet.Cells[row, col].Value = item.RequestType;
                col++;
            }

            System.IO.MemoryStream fs = new System.IO.MemoryStream();
            await package.SaveAsAsync(fs);

            return File(fs.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        // TODO: make into a get with a body
        [Route("getselectvalues")]
        [HttpPost]
        public async Task<ActionResult> GetSelectValues([FromBody] ListValueInfo info)
        {
            //var EnumProducer = new SelectValueFromEnumProducer();
            List<ListValue> list = new List<ListValue>();

            switch (info.ValueType)
            {
                case "RequestStatus" :
                    // provide empty value in the dropdown:
                    list.Add(new ListValue() { Value = "", Label = "" });

                     list.AddRange( Models.Request.RequestStatusValues);
                    break;
                case "RequestType" :
                    // provide empty value in the dropdown:
                    list.Add(new ListValue() { Value = "", Label = "" });

                     list.AddRange( Models.Request.RequestTypeValues);
                    break;
                case "RequestMemberType" :
                    // provide empty value in the dropdown:
                    list.Add(new ListValue() { Value = "", Label = "" });

                     list.AddRange( Models.RequestTeamComposition.RequestMemberTypeValues);
                    break;
                default:
                    break;
            }
            return Ok(new ListValues() { ValueType = info.ValueType, data = list });
        }

        [HttpGet("getclaims/{UserID}")]
        [Authorize()]
        public async Task<IActionResult> GetClaims(string UserID)
        {
            var claims = Repository.List.Select(c => Transformers.Transform(c, new RequestDTO() { Modifier = "Unchanged" }))
                .Where(c => c.Teammembers.Any(t => t.TeamMember.ToString().Equals(UserID)))
                .Select(c => new { 
                    Key = "Request|" + c.ID, 
                    Value = c.Teammembers
                        .First(t => t.TeamMember.ToString().Equals(UserID)).RequestMemberType.ToString() 
                });
            return new JsonResult(await claims.ToArrayAsync());
        }

    }
}
