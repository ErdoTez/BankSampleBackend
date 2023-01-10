using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CUSTOM.SharedDTOs
{
    public class AddSalesDbReq
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public int ExpiryDate { get; set; }
        public decimal PriceAmount { get; set; }
        public CardType CardType { get; set; }

    }
}
