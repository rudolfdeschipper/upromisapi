using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIUtils.APIMessaging
{

    public class RecordGetInfo
    {
        public int ID { get; set; }
    }

    public class ListValueInfo
    {
        public string ValueType { get; set; }
    }

    public class LoadResult<T>
    {
        public T[] Data { get; set; }
        public double Pages { get; set; }
        public string Message { get; set; }
    }

    public class SaveMessage<T>
    {
        public int ID { get; set; }
        public T DataSubject { get; set; }
        public string Action { get; set; }
        public string SubAction { get; set; }
        public List<object> AdditionalData { get; set; }
    }

    public class APIResult<T>
    {
        public int ID { get; set; }
        public T DataSubject { get; set; }
        public string Message { get; set; }
    }

    public class ListValue
    {
        public string Label { get; set; }
        public object Value { get; set; }
    }

    public class ListValues
    {
        public string ValueType { get; set; }
        public List<ListValue> data { get; set; }
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
