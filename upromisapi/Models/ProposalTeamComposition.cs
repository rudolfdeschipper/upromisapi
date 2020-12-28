/*
**             ------ IMPORTANT ------
** This file was generated by ZeroCode2 on 28/Dec/2020 21:46:20
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

    public class ProposalTeamComposition
    {
        public static ListValue[] ProposalMemberTypeValues =
        {
            new ListValue() { Value = "Owner", Label = "Owner" } , 
            new ListValue() { Value = "Administrator", Label = "Administrator" } , 
            new ListValue() { Value = "Member", Label = "Member" } , 
            new ListValue() { Value = "Participant", Label = "Participant" } , 
            new ListValue() { Value = "Reader", Label = "Reader" } 
        };

        [Key]
        [Required]
        public int ID { get; set; } 
       
        [Required]
        public Guid ExternalID { get; set; } = Guid.NewGuid(); 
       
        [Required]
        public Guid TeamMember { get; set; } = Guid.NewGuid(); 
       
        [Required]
        public string ProposalMemberType { get; set; } 
       

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
