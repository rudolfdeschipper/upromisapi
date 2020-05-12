using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace upromiscontractapi.Models
{
    public class AccountField
    {
        [Key]
        public int ID { get; set; }

        public int AccountInfoID { get; set; }
        public AccountInfo AccountInfo { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Value { get; set; }
    }
}
