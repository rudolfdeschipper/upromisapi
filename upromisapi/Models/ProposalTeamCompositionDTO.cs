using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace upromiscontractapi.Models
{
    public class ProposalTeamCompositionDTO : DTOBase
    {
        [Key]
        public int ID { get; set; }

        public int ProposalID { get; set; }
        public ProposalDTO Proposal { get; set; }

        [Required]
        public Guid TeamMember { get; set; }

        [Required, EnumDataType(typeof(TeamMemberType))]
        public TeamMemberType MemberType { get; set; }

    }
}
