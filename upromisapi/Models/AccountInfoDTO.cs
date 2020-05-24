using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace upromiscontractapi.Models
{
    public class AccountInfoDTO : DTOBase
    {
        [Key]
        public int ID { get; set; }

        public Guid ExternalID { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime UpdatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public virtual List<Contract> Contracts { get; private set; } = new List<Contract>();
        public virtual List<Proposal> Proposals { get; private set; } = new List<Proposal>();
        public virtual List<Request> Requests { get; private set; } = new List<Request>();

        public List<AccountTeamCompositionDTO> TeamComposition { get; private set; } = new List<AccountTeamCompositionDTO>();
        public List<AccountFieldDTO> AccountFields { get; private set; } = new List<AccountFieldDTO>();

    }

}
