using CUSTOM.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOM.SharedDTOs
{
    public class SalesListRes : BaseRes
    {
        public List<SalesListDetail> SalesListDetail { get; set; }
    }

    public class SalesListDetail : CardTransactionInfo
    {
    }
}
