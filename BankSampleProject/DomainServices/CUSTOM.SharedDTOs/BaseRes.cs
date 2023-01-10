using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOM.SharedDTOs
{
    public class BaseRes
    {
        public bool IsSuccess { get; set; }
        public string? ResultMessage { get; set; }
        public ResultCode? ResultCode { get; set; }
        
    }
}
