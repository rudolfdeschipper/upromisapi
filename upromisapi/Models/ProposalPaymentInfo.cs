using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace upromiscontractapi.Models
{
    public class ProposalPaymentInfo
    {
        [Key]
        public int ID { get; set; }
        
        [Required]
        public string Description { get; set; }

        [Required, Range(0,double.MaxValue), DisplayFormat(DataFormatString ="€ #.##0,00")]
        public decimal Amount { get; set; }

        public string Comment { get; set; }
    }
}
