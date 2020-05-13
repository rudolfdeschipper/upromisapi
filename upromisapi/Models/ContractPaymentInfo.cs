using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public int ContractID { get; set; }
        public Contract Contract { get; set; }

        [Required]
        public string Description { get; set; }

        // TODO: when sending to JSON, format it to a proper date string - the client side is not very good at this
        [Required, DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}"), JsonProperty("plannedinvoicedate")]
        public DateTime PlannedInvoiceDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}"), JsonProperty("actualinvoicedate")]
        public DateTime ActualInvoiceDate { get; set; }

        [Required, Range(0,double.MaxValue), DisplayFormat(DataFormatString ="{0:€ #.##0,00}"), Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        [EnumDataType(typeof(ContractPaymentStatus))]
        public ContractPaymentStatus PaymentStatus { get; set; }

        [StringLength(100)]
        public string Comment { get; set; }
    }
}
