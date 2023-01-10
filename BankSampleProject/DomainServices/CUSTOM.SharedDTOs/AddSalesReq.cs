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
    public class AddSalesReq
    {
        [Required]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string CardHolderName { get; set; }

        [Required]
        public string CardNumber { get; set; }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [Required]
        public int ExpiryDate { get; set; }
        [Required]
        public decimal PriceAmount { get; set; }



        public static implicit operator AddSalesDbReq(AddSalesReq req)
        {
            var internalApiReq = new AddSalesDbReq
            {
                CardHolderName = req.CardHolderName,
                CardNumber = req.CardNumber,
                ExpiryDate = req.ExpiryDate,
                PriceAmount = req.PriceAmount
            };

            return internalApiReq;
        }

    }
}
