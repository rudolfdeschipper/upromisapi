/*
**             ------ IMPORTANT ------
** This file was generated by ZeroCode2 on 09/Jan/2021 16:57:52
** DO NOT MODIFY IT, as it can be regenerated at any moment.
** If you need this file changed, change the underlying model or its template.
*/
using APIUtils.APIMessaging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace upromiscontractapi.Models
{

    public class RequestRepository : IRequestRepository
    {
        ContractDbContext Context { get; set; }

        public RequestRepository(ContractDbContext context)
        {
            Context = context;

        }
        public IQueryable<Request> List => Context.Requests
        ;

        public async Task<(List<Request>, double)> FilteredAndSortedList(SortAndFilterInformation sortAndFilterInfo, bool paging)
        {
            return await DoSortFilterAndPaging(sortAndFilterInfo, paging);
        }

        private async Task<(List<Request>, double)> DoSortFilterAndPaging(SortAndFilterInformation sentModel, bool doPaging)
        {
            string whereClause = String.Empty;
            var records = List;
            int filteredCount = records.Count();

            if (sentModel != null)
            {
                string[] strings = {
                        "code",
                        "title",
                        "description",
                        "requestStatus",
                        "requestType",

                        ""
                };
                // filtering
                // column search is handled here:0
                if (sentModel.filtered != null)
                {
                    foreach (var item in sentModel.filtered)
                    {
                        if (!String.IsNullOrEmpty(item.value))
                        {
                                if(strings.Contains(item.id))
                                {
                                    whereClause = whereClause + item.id + ".Contains(\"" + item.value + "\") &&";
                                }
                                else
                                {
                                    whereClause = whereClause + item.id + ".ToString().Contains(\"" + item.value + "\") &&";
                                }
                        }
                    }
                    if (!string.IsNullOrEmpty(whereClause))
                    {
                        whereClause = whereClause[0..^2];
                        records = records.Where(whereClause);
                        filteredCount = await records.CountAsync();
                    }
                }
                // ordering
                if (sentModel.sorted != null)
                {
                    string orderBy = "";
                    foreach (var o in sentModel.sorted)
                    {
                        orderBy += " " + o.id + (o.desc ? " DESC" : " ASC") + ",";
                    }
                    if (orderBy.EndsWith(','))
                    {
                        orderBy = orderBy[0..^1];
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
            return (await records.ToListAsync(), sentModel?.pageSize != 0 ? Math.Ceiling((double)(filteredCount / sentModel.pageSize)) : 1.0);
        }


        public async Task<Request> Post(Request rec)
        {
            var record = rec;
            // TODO: add validations
            // TODO: check for new users to create in the team composition list
            record.Teammembers.ForEach(pi =>
            {
                pi.Request = record;
            });

//            record.AccountInfo = Context.AccountInfo.FirstOrDefault();

//            record.ParentContract = Context.Contracts.Find(rec.DataSubject.ParentContractID);

            Context.Requests.Add(record);

            await Context.SaveChangesAsync();

            return record;
        }

        public async Task<Request> Put(Request rec)
        {
            Context.Requests.Attach(rec);

            await Context.SaveChangesAsync();

            return rec;
        }

        public async Task<bool> Delete(Request rec)
        {
            var record = await Context.Requests.Where(c => c.ID == rec.ID)
                .Include( p => p.Teammembers)
                .FirstOrDefaultAsync();

            if (record == null)
            {
                return false;
            }

            Context.Requests.Remove(record);

            await Context.SaveChangesAsync();

            return true;
        }

        public async Task<Request> Get(int id)
        {
            var record = await Context.Requests.Where(c => c.ID == id)
                .Include( p => p.Teammembers)
                .FirstOrDefaultAsync();
            return record;
        }

    }
}
