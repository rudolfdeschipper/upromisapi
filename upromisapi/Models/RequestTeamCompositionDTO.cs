using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace upromiscontractapi.Models
{
    public class RequestTeamCompositionDTO : DTOBase
    {
        [Key]
        public int ID { get; set; }

        public int RequestID { get; set; }
        public RequestDTO Request { get; set; }

        [Required]
        public Guid TeamMember { get; set; }

        [Required, EnumDataType(typeof(TeamMemberType))]
        public TeamMemberType MemberType { get; set; }

    }
}
