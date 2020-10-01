/*
**             ------ IMPORTANT ------
** This file was generated by ZeroCode2 on 01/Oct/2020 22:57:33
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
        public int ID { get; set; } 
        [Required]
        [StringLength(50)]
        public string Code { get; set; } 
        public string Description { get; set; } 
        [StringLength(50)]
        [EnumDataType(typeof(Status))]
        public Status Status { get; set; } 
        [DisplayFormat(DataFormatString = "{0:MM/DD/YYYY}")]
        public DateTime Startdate { get; set; } = DateTime.Now; 
        [DisplayFormat(DataFormatString = "{0:MM/DD/YYYY}")]
        public DateTime Enddate { get; set; } = DateTime.Now; 
        public double Budget { get; set; } 
        [Required]
        public Guid ExternalID { get; set; } = Guid.NewGuid(); 
        [StringLength(50)]
        [EnumDataType(typeof(ContractType))]
        public ContractType ContractType { get; set; } 

        // Default model properties
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public string CreatedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime UpdatedOn { get; set; } = DateTime.Now;

        public string UpdatedBy { get; set; }

        public virtual List<ContractPaymentInfoDTO> Payments { get; private set; } = new List<ContractPaymentInfoDTO>();
        public virtual List<ContractTeamCompositionDTO> Teammembers { get; private set; } = new List<ContractTeamCompositionDTO>();

    }
}

