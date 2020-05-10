using System;
using System.Collections.Generic;
using System.Linq;

namespace upromiscontractapi.Models
{

    public class ContractRepository : IContractRepository
    {
        private Dictionary<string, Contract> contracts = new Dictionary<string, Contract>();

        public static ContractRepository SharedRepository { get; } = new ContractRepository();

        public ContractRepository()
        {
            var initialItems = new[] {
                new Contract { ID = 100, Title = "Contract 1", Value = 275M, StartDate=DateTime.Now, EndDate=DateTime.Now.AddYears(1), Code="Code 1", Description="Description of a really long string", Status = "Planned" },
                new Contract { ID = 200, Title = "Contract 2", Value = -48.95M, StartDate=DateTime.Now, EndDate=DateTime.Now.AddYears(2), Code="Code 2", Description="Description of a really long string", Status = "Open" },
                new Contract { ID = 300, Title = "Contract 3", Value = 19.50M, StartDate=DateTime.Now, EndDate=DateTime.Now.AddYears(3), Code="Code 3", Description="Description of a really long string", Status = "Open" },
                new Contract { ID = 400, Title = "Contract 4", Value = 34.95M, StartDate=DateTime.Now, EndDate=DateTime.Now.AddMonths(10), Code="Code 4", Description="Description of a really long string", Status = "Closed" } };
            
            //for (int i = 0; i < 100; i++)
            {
                foreach (var p in initialItems)
                {
                    var pItems = new[] {
                        new ContractPaymentInfo { ID= p.ID + 1, Description = p.Code + " payment term 1", PlannedInvoiceDate = DateTime.Now, Amount = p.Value / 3 },
                        new ContractPaymentInfo { ID= p.ID + 2, Description = p.Code + " payment term 2", PlannedInvoiceDate = DateTime.Now.AddMonths(1), Amount = p.Value / 3 },
                        new ContractPaymentInfo { ID= p.ID + 3, Description = p.Code + " payment term 3", PlannedInvoiceDate = DateTime.Now.AddMonths(2), Amount = p.Value / 3 },
                    };
                    AddContract(1, p);
                    p.PaymentInfo.AddRange(pItems);
                }
            }
        }
        public IQueryable<Contract> Contracts => contracts.Values.AsQueryable();

        public void AddContract(int i, Contract p) => contracts.Add(p.ID.ToString(), p);

    }
}
