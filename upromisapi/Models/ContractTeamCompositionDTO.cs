using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace upromiscontractapi.Models
{
    public class ContractTeamCompositionDTO : DTOBase
    {
        [Key]
        public int ID { get; set; }

        public int ContractID { get; set; }
        public ContractDTO Contract { get; set; }

        [Required]
        public Guid TeamMember { get; set; }

        [Required, EnumDataType(typeof(TeamMemberType))]
        public TeamMemberType MemberType { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime? Startdate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime? Enddate { get; set; }

    }
}
