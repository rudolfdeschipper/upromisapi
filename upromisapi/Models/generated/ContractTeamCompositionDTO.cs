/*
**             ------ IMPORTANT ------
** This file was generated by ZeroCode2 on 04/Jan/2021 22:06:22
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


    public class ContractTeamCompositionDTO : DTOBase
    {
        [Key]
        [Required]
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; } 

        [Required]
        [JsonProperty(PropertyName = "externalId")]
        public Guid ExternalID { get; set; } = Guid.NewGuid(); 

        [Required]
        [JsonProperty(PropertyName = "teammemberID")]
        public Guid TeamMember { get; set; } = Guid.NewGuid(); 

        [Required]
        [JsonProperty(PropertyName = "memberType")]
        public string ContractMemberType { get; set; } 
        [JsonProperty(PropertyName = "memberTypeLabel")]
        public string ContractMemberTypeLabel { get; set; }


        // Parent property
        public Contract ContractDTO {get; set; }

        // Default model properties
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public string CreatedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime UpdatedOn { get; set; } = DateTime.Now;

        public string UpdatedBy { get; set; }

    }
}
