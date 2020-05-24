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

        public Guid ExternalID { get; set; } = Guid.NewGuid();

        [Required, StringLength(100)]
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public string CreatedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime UpdatedOn { get; set; } = DateTime.Now;

        public string UpdatedBy { get; set; }

        public virtual List<Contract> Contracts { get; private set; } = new List<Contract>();
        public virtual List<Proposal> Proposals { get; private set; } = new List<Proposal>();
        public virtual List<Request> Requests { get; private set; } = new List<Request>();

        public List<AccountTeamComposition> TeamComposition { get; set; } = new List<AccountTeamComposition>();
        public List<AccountField> AccountFields { get; set; } = new List<AccountField>();
    }

}
