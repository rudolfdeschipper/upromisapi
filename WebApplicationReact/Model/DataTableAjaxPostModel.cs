using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationReact.Models
{

    public class RecordGetInfo
    {
        public int ID { get; set; }
    }

    public class DataTableAjaxPostModel
    {
        // properties are not capital due to json mapping
//        public int draw { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
        public List<Search> filtered { get; set; }
        public List<Order> sorted { get; set; }
    }

    public class Search
    {
        public string id { get; set; }
        public string value { get; set; }
    }

    public class Order
    {
        public string id { get; set; }
        public bool desc { get; set; }
    }
}
