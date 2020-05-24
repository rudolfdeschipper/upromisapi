using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace upromiscontractapi.Models
{

    public class Proposal
    {

        [Key]
        public int ID { get; set; }

        public Guid ExternalID { get; set; } = Guid.NewGuid();

        [Required, StringLength(maximumLength: 20, MinimumLength = 1, ErrorMessage = "Code cannot be empty, but no longer than 20 characters")]
        public string Code { get; set; }

        [Required, StringLength(maximumLength: 100, MinimumLength = 1, ErrorMessage = "Title cannot be empty, but no longer than 100 characters")]
        public string Title { get; set; }
        public string Description { get; set; }

        [Required, StringLength(maximumLength: 20, MinimumLength = 1, ErrorMessage = "Status cannot be empty, but no longer than 20 characters")]
        public string Status { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public string CreatedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime UpdatedOn { get; set; } = DateTime.Now;

        public string UpdatedBy { get; set; }

        [EnumDataType(typeof(ContractType))]
        public ContractType ContractType { get; set; }

        [EnumDataType(typeof(RequestType))]
        public RequestType RequestType { get; set; }

        [Required, Range(0, double.MaxValue), Column(TypeName = "decimal(18, 2)"), DisplayFormat(DataFormatString = "{0:€ #.##0,00}")]
        public decimal Value { get; set; }

        public int AccountInfoID { get; set; }
        public AccountInfo AccountInfo { get; set; }

        // decide if we have a simple set of dates to manage the workflow of something the more sophisticated
        public List<ProposalPaymentInfo> PaymentInfo { get; set; } = new List<ProposalPaymentInfo>();
        public List<ProposalTeamComposition> TeamComposition { get; set; } = new List<ProposalTeamComposition>();
    }
}
