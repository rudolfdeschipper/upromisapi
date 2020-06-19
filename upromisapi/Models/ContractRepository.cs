using APIUtils.APIMessaging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace upromiscontractapi.Models
{

    public class ContractRepository : IContractRepository
    {
        ApplicationDbContext _context { get; set; }

        public ContractRepository(ApplicationDbContext context)
        {
            _context = context;

        }
        public IQueryable<ContractDTO> List => _context.Contracts.Select(c => Transformers.Transform(c, new ContractDTO() { Modifier = "Unchanged" })).AsQueryable();

        public async Task<ContractDTO> Post(SaveMessage<ContractDTO> rec)
        {
            // TODO: add validations
            // TODO: check for new users to create in the team composition list

            Contract contract = Transformers.Transform(rec.DataSubject, new Contract()) as Contract;

            contract.PaymentInfo.ForEach(pi =>
            {
                pi.Contract = contract;
            });

            contract.TeamComposition.ForEach(pi =>
            {
                pi.Contract = contract;
            });

            contract.AccountInfo = _context.AccountInfo.FirstOrDefault();

            contract.ParentContract = _context.Contracts.Find(rec.DataSubject.ParentContractID);

            _context.Contracts.Add(contract);

            await _context.SaveChangesAsync();

            return Transformers.Transform(contract, new ContractDTO());
        }

        public async Task<ContractDTO> Put(SaveMessage<ContractDTO> rec)
        {
            var ctr = await _context.Contracts.Where(c => c.ID == rec.ID)
                .Include("PaymentInfo")
                .Include("TeamComposition")
                .FirstOrDefaultAsync();

            if (ctr == null)
            {
                return null;
            }

            ctr = Transformers.Transform(rec.DataSubject, ctr);

            await _context.SaveChangesAsync();

            return Transformers.Transform(ctr, new ContractDTO());
        }

        public async Task<bool> Delete(SaveMessage<ContractDTO> rec)
        {
            var ctr = _context.Contracts.FirstOrDefault(c => c.ID == rec.ID);

            if (ctr == null)
            {
                return false;
            }

            _context.Contracts.Remove(ctr);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ContractDTO> Get(int id)
        {
            var contract = await _context.Contracts.Where(c => c.ID == id)
                .Include("PaymentInfo")
                .Include("TeamComposition")
                .FirstOrDefaultAsync();

            var ctr = Transformers.Transform(contract, new ContractDTO() { Modifier = "Unchanged" });

            return ctr;
        }
    }
}
