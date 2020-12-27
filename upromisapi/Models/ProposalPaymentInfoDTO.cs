/*
**             ------ IMPORTANT ------
** This file was generated by ZeroCode2 on 18/Dec/2020 22:34:56
** DO NOT MODIFY IT, as it can be regenerated at any moment.
** If you need this file changed, change the underlying model or its template.
*/
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace upromiscontractapi.Models
{


    public class ProposalPaymentInfoDTO : DTOBase
    {
        [Key]
        [Required]
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; } 

        [Required]
        [JsonProperty(PropertyName = "externalId")]
        public Guid ExternalID { get; set; } = Guid.NewGuid(); 

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; } 

        [DisplayFormat(DataFormatString = "{0:MM/DD/YYYY}")]
        [JsonProperty(PropertyName = "plannedInvoiceDate")]
        public DateTime PlannedInvoiceDate { get; set; } = DateTime.Now; 

        [JsonProperty(PropertyName = "amount")]
        public double Amount { get; set; } 


        // Parent property
        public Proposal ProposalDTO {get; set; }

        // Default model properties
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public string CreatedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime UpdatedOn { get; set; } = DateTime.Now;

        public string UpdatedBy { get; set; }

    }
}
