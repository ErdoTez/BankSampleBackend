using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CUSTOM.CommonHelpers
{
    public static class CardCheck
    {
        public static bool CardCheckWithLuhnAlg(string cardNumber)
        {
            ArgumentNullException.ThrowIfNull(cardNumber);
            int sumOfDigits = cardNumber.Where((e) => e >= '0' && e <= '9')
                    .Reverse()
                    .Select((e, i) => ((int)e - 48) * (i % 2 == 0 ? 1 : 2))
                    .Sum((e) => e / 10 + e % 10);

            return sumOfDigits % 10 == 0;
        }


        public static string CardNumberMask(string cardNumber)
        {
            var reg = new Regex(@"(?<=\d{4}\d{2})\d{2}\d{4}(?=\d{4})|(?<=\d{4}( |-)\d{2})\d{2}\1\d{4}(?=\1\d{4})");
            return reg.Replace(cardNumber, new MatchEvaluator((m) => new String('*', m.Length)));
        }
    }
}
