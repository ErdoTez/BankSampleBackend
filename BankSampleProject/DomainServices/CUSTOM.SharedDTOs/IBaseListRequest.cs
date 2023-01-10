using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOM.SharedDTOs
{
    public interface IBaseListRequest
    {
        List<RequestFilter> Filters { get; set; }
    }

    public class BaseListRequest : IBaseListRequest
    {
        public List<RequestFilter> Filters { get; set; }
    }

    public class RequestFilter
    {
        public string Name { get; set; }
        public Condition Condition { get; set; }
        public object Value { get; set; }
    }

    public enum Condition
    {
        Equal,
        Equals,
        Contains,
        GreaterThan,
        GreaterThanOrEqual,
        LessThan,
        LessThanOrEqual
    }
}
