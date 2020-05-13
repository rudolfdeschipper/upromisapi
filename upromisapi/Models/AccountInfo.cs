using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace upromiscontractapi.Models
{
    public class AccountInfo
    {
        [Key]
        public int ID { get; set; }

        public Guid ExternalID { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}"), JsonProperty("createdon")]
        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}"), JsonProperty("updatedon")]
        public DateTime UpdatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public virtual List<Contract> Contracts { get; private set; } = new List<Contract>();
        public virtual List<Proposal> Proposals { get; private set; } = new List<Proposal>();
        public virtual List<Request> Requests { get; private set; } = new List<Request>();

        public List<AccountTeamComposition> TeamComposition { get; private set; } = new List<AccountTeamComposition>();
        public List<AccountField> AccountFields { get; private set; } = new List<AccountField>();
    }
}
