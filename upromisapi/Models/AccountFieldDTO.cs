using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace upromiscontractapi.Models
{
    public class AccountFieldDTO : DTOBase
    {
        [Key]
        public int ID { get; set; }

        public int AccountInfoID { get; set; }
        public AccountInfoDTO AccountInfo { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Value { get; set; }

    }
}
