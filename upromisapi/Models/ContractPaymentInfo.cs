using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace upromiscontractapi.Models
{
    public enum ContractPaymentStatus
    { 
        Planned,
        Invoiced,
        Paid
    }

    public class ContractPaymentInfo
    {
        [Key]
        public int ID { get; set; }
        
        [Required]
        public string Description { get; set; }

        // TODO: when sending to JSON, format it to a proper date string - the client side is not very good at this
        [Required, DisplayFormat(DataFormatString = "dd/MMM/yyyy"), JsonProperty("plannedinvoicedate")]
        public DateTime PlannedInvoiceDate { get; set; }

        [DisplayFormat(DataFormatString = "dd/MMM/yyyy"), JsonProperty("actualinvoicedate")]
        public DateTime ActualInvoiceDate { get; set; }

        [Required, Range(0,double.MaxValue), DisplayFormat(DataFormatString ="€ #.##0,00")]
        public decimal Amount { get; set; }

        [EnumDataType(typeof(ContractPaymentStatus))]
        public ContractPaymentStatus PaymentStatus { get; set; }

        public string Comment { get; set; }
    }
}
