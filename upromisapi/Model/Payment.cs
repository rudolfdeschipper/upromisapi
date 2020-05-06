using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace upromiscontractapi.Models
{
    public partial class Contract
    {
        public class Payment
        {
            public int ID { get; set; }
            public string Description { get; set; }
            // TODO: when sending to JSON, format it to a proper date string - the client side is not very good at this
            [DisplayFormat(DataFormatString = "dd/MMM/yyyy"), JsonProperty("plannedinvoicedate")]
            public DateTime PlannedInvoiceDate { get; set; }
            [DisplayFormat(DataFormatString = "dd/MMM/yyyy"), JsonProperty("actualinvoicedate")]
            public DateTime ActualInvoiceDate { get; set; }
            [DisplayFormat(DataFormatString ="€ #.##0,00")]
            public decimal Amount { get; set; }
        }
    }
}
