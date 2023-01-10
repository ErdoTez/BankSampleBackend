using CUSTOM.DbRepository.BaseDbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOM.Models.Models
{
    public partial class CardTransactionInfo : BaseEntity<long>
    {
        public long TransactionId { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string CardholderName { get; set; }
        public string Pan { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public int ExpiryDate { get; set; }
        public decimal Amount { get; set; }
        public int CardType { get; set; }

    }
}
