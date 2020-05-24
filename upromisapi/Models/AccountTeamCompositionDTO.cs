using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace upromiscontractapi.Models
{
    public class AccountTeamCompositionDTO : DTOBase
    {
        [Key]
        public int ID { get; set; }

        public int AccountInfoID { get; set; }
        public AccountInfoDTO AccountInfo { get; set; }

        [Required]
        public Guid TeamMember { get; set; }

        [Required, EnumDataType(typeof(TeamMemberType))]
        public TeamMemberType MemberType { get; set; }

    }
}
