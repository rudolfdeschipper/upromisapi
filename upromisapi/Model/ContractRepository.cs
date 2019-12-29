using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebApplicationReact.Models
{
    public class Contract
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        // TODO: when sending to JSON, format it to a proper date string - the client side is not very good at this
        [DisplayFormat(DataFormatString ="dd/MMM/yyyy"), JsonProperty("startdate")]
        public DateTime StartDate { get; set; }
        [DisplayFormat(DataFormatString = "dd/MMM/yyyy"), JsonProperty("enddate")]
        public DateTime EndDate { get; set; }
        public decimal Value { get; set; }

        public List<Contract.Payment> PaymentInfo { get; private set; } = new List<Payment>();

        public class Payment
        {
            public int ID { get; set; }
            public string Description { get; set; }
            // TODO: when sending to JSON, format it to a proper date string - the client side is not very good at this
            [DisplayFormat(DataFormatString = "dd/MMM/yyyy"), JsonProperty("plannedinvoicedate")]
            public DateTime PlannedInvoiceDate { get; set; }
            [DisplayFormat(DataFormatString = "dd/MMM/yyyy"), JsonProperty("actualinvoicedate")]
            public DateTime ActualInvoiceDate { get; set; }
            [DisplayFormat(DataFormatString ="€ #.##0,00")]
            public decimal Amount { get; set; }
        }
    }



    public class ContractRepository : IContractRepository
    {
        private static readonly ContractRepository sharedRepository = new ContractRepository();
        private Dictionary<string, Contract> contracts = new Dictionary<string, Contract>();

        public static ContractRepository SharedRepository => sharedRepository;

        public ContractRepository() {
            var initialItems = new[] {
                new Contract { ID = 100, Title = "Contract 1", Value = 275M, StartDate=DateTime.Now, EndDate=DateTime.Now.AddYears(1), Code="Code 1", Description="Description of a really long string" },
                new Contract { ID = 200, Title = "Contract 2", Value = -48.95M, StartDate=DateTime.Now, EndDate=DateTime.Now.AddYears(2), Code="Code 2", Description="Description of a really long string" },
                new Contract { ID = 300, Title = "Contract 3", Value = 19.50M, StartDate=DateTime.Now, EndDate=DateTime.Now.AddYears(3), Code="Code 3", Description="Description of a really long string" },
                new Contract { ID = 400, Title = "Contract 4", Value = 34.95M, StartDate=DateTime.Now, EndDate=DateTime.Now.AddMonths(10), Code="Code 4", Description="Description of a really long string" } };

            //for (int i = 0; i < 100; i++)
            {
                foreach (var p in initialItems)
                {
                    var pItems = new[] {
                        new Contract.Payment { ID= p.ID + 1, Description = p.Code + " payment term 1", PlannedInvoiceDate = DateTime.Now, Amount = p.Value / 3 },
                        new Contract.Payment { ID= p.ID + 2, Description = p.Code + " payment term 2", PlannedInvoiceDate = DateTime.Now.AddMonths(1), Amount = p.Value / 3 },
                        new Contract.Payment { ID= p.ID + 3, Description = p.Code + " payment term 3", PlannedInvoiceDate = DateTime.Now.AddMonths(2), Amount = p.Value / 3 },
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
