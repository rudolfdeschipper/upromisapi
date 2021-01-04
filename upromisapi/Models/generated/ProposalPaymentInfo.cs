/*
**             ------ IMPORTANT ------
** This file was generated by ZeroCode2 on 04/Jan/2021 22:06:24
** DO NOT MODIFY IT, as it can be regenerated at any moment.
** If you need this file changed, change the underlying model or its template.
*/
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using APIUtils.APIMessaging;

namespace upromiscontractapi.Models
{

    public class ProposalPaymentInfo
    {

        [Key]
        [Required]
        public int ID { get; set; } 
       
        [Required]
        public Guid ExternalID { get; set; } = Guid.NewGuid(); 
       
        public string Description { get; set; } 
       
        [DisplayFormat(DataFormatString = "{0:MM/DD/YYYY}")]
        public DateTime PlannedInvoiceDate { get; set; } = DateTime.Now; 
       
        public double Amount { get; set; } 
       

        // Parent property
        public Proposal Proposal { get; set; }

        // Default model properties
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public string CreatedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime UpdatedOn { get; set; } = DateTime.Now;

        public string UpdatedBy { get; set; }

    }
}
