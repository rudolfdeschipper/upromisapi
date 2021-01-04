/*
**             ------ IMPORTANT ------
** This file was generated by ZeroCode2 on 04/Jan/2021 22:06:24
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

    public class ProposalRepository : IProposalRepository
    {
        ContractDbContext Context { get; set; }

        public ProposalRepository(ContractDbContext context)
        {
            Context = context;

        }
        public IQueryable<Proposal> List => Context.Proposals
        ;

        public async Task<(List<Proposal>, double)> FilteredAndSortedList(SortAndFilterInformation sortAndFilterInfo, bool paging)
        {
            return await DoSortFilterAndPaging(sortAndFilterInfo, paging);
        }

        private async Task<(List<Proposal>, double)> DoSortFilterAndPaging(SortAndFilterInformation sentModel, bool doPaging)
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
                        "proposalStatus",
                        "proposalType",

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


        public async Task<Proposal> Post(Proposal rec)
        {
            var record = rec;
            // TODO: add validations
            // TODO: check for new users to create in the team composition list
            record.Payments.ForEach(pi =>
            {
                pi.Proposal = record;
            });
            record.Teammembers.ForEach(pi =>
            {
                pi.Proposal = record;
            });

//            record.AccountInfo = Context.AccountInfo.FirstOrDefault();

//            record.ParentContract = Context.Contracts.Find(rec.DataSubject.ParentContractID);

            Context.Proposals.Add(record);

            await Context.SaveChangesAsync();

            return record;
        }

        public async Task<Proposal> Put(Proposal rec)
        {
            Context.Proposals.Attach(rec);

            await Context.SaveChangesAsync();

            return rec;
        }

        public async Task<bool> Delete(Proposal rec)
        {
            var ctr = Context.Proposals.FirstOrDefault(c => c.ID == rec.ID);

            if (ctr == null)
            {
                return false;
            }

            Context.Proposals.Remove(ctr);

            await Context.SaveChangesAsync();

            return true;
        }

        public async Task<Proposal> Get(int id)
        {
            var record = await Context.Proposals.Where(c => c.ID == id)
                .Include( p => p.Payments)
                .Include( p => p.Teammembers)
                .FirstOrDefaultAsync();
            return record;
        }

    }
}
