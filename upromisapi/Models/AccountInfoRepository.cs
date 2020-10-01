/*
**             ------ IMPORTANT ------
** This file was generated by ZeroCode2 on 01/Oct/2020 22:57:30
** DO NOT MODIFY IT, as it can be regenerated at any moment.
** If you need this file changed, change the underlying model or its template.
*/
using APIUtils.APIMessaging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace upromiscontractapi.Models
{

    public class AccountInfoRepository : IAccountInfoRepository
    {
        ContractDbContext _context { get; set; }

        public AccountInfoRepository(ContractDbContext context)
        {
            _context = context;

        }
        public IQueryable<AccountInfoDTO> List => _context.Accounts.Select(c => Transformers.Transform(c, new AccountInfoDTO() { Modifier = "Unchanged" })).AsQueryable();

        public async Task<AccountInfoDTO> Post(SaveMessage<AccountInfoDTO> rec)
        {
            // TODO: add validations
            // TODO: check for new users to create in the team composition list

            AccountInfo record = Transformers.Transform(rec.DataSubject, new AccountInfo()) as AccountInfo;

            record.AccountFields.ForEach(pi =>
            {
                pi.AccountInfo = record;
            });
            record.Teammembers.ForEach(pi =>
            {
                pi.AccountInfo = record;
            });

//            record.AccountInfo = _context.AccountInfo.FirstOrDefault();

//            record.ParentContract = _context.Contracts.Find(rec.DataSubject.ParentContractID);

            _context.Accounts.Add(record);

            await _context.SaveChangesAsync();

            return Transformers.Transform(record, new AccountInfoDTO());
        }

        public async Task<AccountInfoDTO> Put(SaveMessage<AccountInfoDTO> rec)
        {
            var ctr = await _context.Accounts.Where(c => c.ID == rec.ID)
                .Include("AccountField")
                .Include("AccountTeamComposition")
                .Include("Contract")
                .Include("Proposal")
                .Include("Request")
                .FirstOrDefaultAsync();

            if (ctr == null)
            {
                return null;
            }

            ctr = Transformers.Transform(rec.DataSubject, ctr);

            await _context.SaveChangesAsync();

            return Transformers.Transform(ctr, new AccountInfoDTO());
        }

        public async Task<bool> Delete(SaveMessage<AccountInfoDTO> rec)
        {
            var ctr = _context.Accounts.FirstOrDefault(c => c.ID == rec.ID);

            if (ctr == null)
            {
                return false;
            }

            _context.Accounts.Remove(ctr);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<AccountInfoDTO> Get(int id)
        {
            var record = await _context.Accounts.Where(c => c.ID == id)
                .Include("AccountField")
                .Include("AccountTeamComposition")
                .Include("Contract")
                .Include("Proposal")
                .Include("Request")
                .FirstOrDefaultAsync();

            var ctr = Transformers.Transform(record, new AccountInfoDTO() { Modifier = "Unchanged" });

            return ctr;
        }
    }
}
