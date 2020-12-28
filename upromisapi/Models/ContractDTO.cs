/*
**             ------ IMPORTANT ------
** This file was generated by ZeroCode2 on 28/Dec/2020 21:46:19
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


    public class ContractDTO : DTOBase
    {
        [Key]
        [Required]
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; } 

        [Required]
        [StringLength(50)]
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; } 

        [Required]
        [StringLength(100)]
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; } 

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; } 

        [Required]
        [StringLength(50)]
        [JsonProperty(PropertyName = "contractType")]
        public string ContractType { get; set; } 
        [JsonProperty(PropertyName = "contractTypeLabel")]
        public string ContractTypeLabel { get; set; }

        [Required]
        [StringLength(50)]
        [JsonProperty(PropertyName = "contractStatus")]
        public string ContractStatus { get; set; } 
        [JsonProperty(PropertyName = "contractStatusLabel")]
        public string ContractStatusLabel { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/DD/YYYY}")]
        [JsonProperty(PropertyName = "startDate")]
        public DateTime Startdate { get; set; } = DateTime.Now; 

        [DisplayFormat(DataFormatString = "{0:MM/DD/YYYY}")]
        [JsonProperty(PropertyName = "endDate")]
        public DateTime Enddate { get; set; } = DateTime.Now; 

        [JsonProperty(PropertyName = "budget")]
        public double Budget { get; set; } 

        [Required]
        [JsonProperty(PropertyName = "externalId")]
        public Guid ExternalID { get; set; } = Guid.NewGuid(); 

        [JsonProperty(PropertyName = "proposal")]
        public int? ProposalId { get; set; } 
        [JsonProperty(PropertyName = "proposalLabel")]
        public string ProposalIdLabel { get; set; }


        // Default model properties
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public string CreatedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime UpdatedOn { get; set; } = DateTime.Now;

        public string UpdatedBy { get; set; }

        [JsonProperty(PropertyName = "Payments")]
        public virtual List<ContractPaymentInfoDTO> Payments { get; private set; } = new List<ContractPaymentInfoDTO>();
        [JsonProperty(PropertyName = "Teammembers")]
        public virtual List<ContractTeamCompositionDTO> Teammembers { get; private set; } = new List<ContractTeamCompositionDTO>();

    }
}

