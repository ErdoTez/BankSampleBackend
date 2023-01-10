using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOM.SharedDTOs
{
    public class AddSalesRes : BaseRes
    {
        public string CardNumber { get; set; }
        public decimal PriceAmount { get; set; }
        public long TransactionId { get; set; }
        public DateTime TransactionTime { get; set; }
    }
}
