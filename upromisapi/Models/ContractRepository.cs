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
        public IQueryable<ContractDTO> List => _context.Contracts.Select(c => Transformers.Transform(c, new ContractDTO() { Modifier = "Unchanged"} ) ).AsQueryable();

        public async Task<APIResult<ContractDTO>> Post(SaveMessage<ContractDTO> rec )
        {
            // TODO: add validations
            // TODO: check for new users to create in the team composition list

            Contract contract = Transformers.Transform(rec.DataSubject, new Contract()) as Contract;
            
            contract.PaymentInfo.ForEach(pi => { pi.Contract = contract;
            });

            contract.TeamComposition.ForEach(pi => {
                pi.Contract = contract;
            });

            contract.AccountInfo = _context.AccountInfo.FirstOrDefault();

            contract.ParentContract = _context.Contracts.Find(rec.DataSubject.ParentContractID);

            _context.Contracts.Add(contract);
            
            await _context.SaveChangesAsync();

            return new APIResult<ContractDTO>() { ID = rec.DataSubject.ID, Success = true, DataSubject = Transformers.Transform(contract, new ContractDTO()), Message = "Post was performed" };
        }

        public async Task<APIResult<ContractDTO>> Put(SaveMessage<ContractDTO> rec )
        {
            var ctr = await _context.Contracts.Where(c => c.ID == rec.ID)
                .Include("PaymentInfo")
                .Include("TeamComposition")
                .FirstOrDefaultAsync();

            ctr = Transformers.Transform(rec.DataSubject, ctr);

            await _context.SaveChangesAsync();

            return new APIResult<ContractDTO>() { ID = ctr.ID, DataSubject=Transformers.Transform(ctr, new ContractDTO()), Success = true, Message = "Put was performed" };
        }

        public async Task<APIResult<ContractDTO>> Delete(SaveMessage<ContractDTO> rec )
        {
            var ctr = _context.Contracts.FirstOrDefault(c => c.ID == rec.ID);

            _context.Contracts.Remove(ctr);

            await _context.SaveChangesAsync();

            return new APIResult<ContractDTO>() { ID = rec.ID, Success = true, DataSubject = null, Message = "Delete was performed" };
        }

        public async Task<APIResult<ContractDTO>> Get(int id)
        {
            var contract = await _context.Contracts.Where(c => c.ID == id)
                .Include("PaymentInfo")
                .Include("TeamComposition")
                .FirstOrDefaultAsync();

            var ctr = Transformers.Transform(contract, new ContractDTO() { Modifier = "Unchanged" });

            return new APIResult<ContractDTO>() { ID = id, Success = true, DataSubject = ctr, Message = "Get was performed" };
        }
    }
}
