using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace upromiscontractapi.Models
{

    public enum TeamMemberType
    { 
        Owner,
        Administrator,
        Member,
        Participant,
        Reader
    }

    public class ContractTeamComposition
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public Guid TeamMember { get; set; }

        [Required, EnumDataType(typeof(TeamMemberType))]
        public TeamMemberType MemberType { get; set; }

        [DisplayFormat(DataFormatString = "dd/MMM/yyyy"), JsonProperty("startdate")]
        public DateTime? Startdate { get; set; }

        [DisplayFormat(DataFormatString = "dd/MMM/yyyy"), JsonProperty("enddate")]
        public DateTime? Enddate { get; set; }
    }
}
