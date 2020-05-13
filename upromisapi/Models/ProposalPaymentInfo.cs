using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace upromiscontractapi.Models
{
    public class ProposalPaymentInfo
    {
        [Key]
        public int ID { get; set; }

        public int ProposalID { get; set; }
        public Proposal Proposal { get; set; }

        [Required]
        public string Description { get; set; }

        [Required, Range(0,double.MaxValue), DisplayFormat(DataFormatString ="{0:€ #.##0,00}"), Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        [StringLength(100)]
        public string Comment { get; set; }
    }
}
