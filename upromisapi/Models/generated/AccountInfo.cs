/*
**             ------ IMPORTANT ------
** This file was generated by ZeroCode2 on 04/Jan/2021 22:06:19
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

    public class AccountInfo
    {


        [Key]
        [Required]
        public int ID { get; set; } 

        [Required]
        public Guid ExternalID { get; set; } = Guid.NewGuid(); 

        [Required]
        [StringLength(50)]
        public string Code { get; set; } 

        [Required]
        [StringLength(100)]
        public string Title { get; set; } 

        public string Description { get; set; } 


        // Default model properties
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public string CreatedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime UpdatedOn { get; set; } = DateTime.Now;

        public string UpdatedBy { get; set; }

        public virtual List<AccountField> AccountFields { get; private set; } = new List<AccountField>();
        public virtual List<AccountTeamComposition> Teammembers { get; private set; } = new List<AccountTeamComposition>();
        public virtual List<Contract> Contracts { get; private set; } = new List<Contract>();
        public virtual List<Proposal> Proposals { get; private set; } = new List<Proposal>();
        public virtual List<Request> Requests { get; private set; } = new List<Request>();

    }
}

