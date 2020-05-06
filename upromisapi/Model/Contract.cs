using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace upromiscontractapi.Models
{
    public partial class Contract
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        // TODO: when sending to JSON, format it to a proper date string - the client side is not very good at this
        [DisplayFormat(DataFormatString ="dd/MMM/yyyy"), JsonProperty("startdate")]
        public DateTime StartDate { get; set; }
        [DisplayFormat(DataFormatString = "dd/MMM/yyyy"), JsonProperty("enddate")]
        public DateTime EndDate { get; set; }

        public string Status { get; set; }

        public decimal Value { get; set; }

        public List<Contract.Payment> PaymentInfo { get; private set; } = new List<Payment>();
    }
}
