using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace upromiscontractapi.Models
{
    public class RequestDTO : DTOBase
    {

        [Key]
        public int ID { get; set; }

        public Guid ExternalID { get; set; } = Guid.NewGuid();

        [Required, StringLength(maximumLength: 20, MinimumLength = 1, ErrorMessage = "Code cannot be empty, but no longer than 20 characters")]
        public string Code { get; set; }

        [Required, StringLength(maximumLength: 100, MinimumLength = 1, ErrorMessage = "Title cannot be empty, but no longer than 100 characters")]
        public string Title { get; set; }
        public string Description { get; set; }

        [Required, StringLength(maximumLength: 20, MinimumLength = 1, ErrorMessage = "Status cannot be empty, but no longer than 20 characters")]
        public string Status { get; set; }

        [DisplayFormat(DataFormatString = "dd/MMM/yyyy")]
        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        [DisplayFormat(DataFormatString = "dd/MMM/yyyy")]
        public DateTime UpdatedOn { get; set; }

        public string UpdatedBy { get; set; }

        [EnumDataType(typeof(ContractType))]
        public ContractType ContractType { get; set; }

        [EnumDataType(typeof(RequestType))]
        public RequestType RequestType { get; set; }

        public int AccountInfoID { get; set; }
        public AccountInfoDTO AccountInfo { get; set; }
        public List<RequestTeamCompositionDTO> TeamComposition { get; private set; } = new List<RequestTeamCompositionDTO>();

        // decide if we have a simple set of dates to manage the workflow of something the more sophisticated

    }
}
