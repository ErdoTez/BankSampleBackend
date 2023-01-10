using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOM.SharedDTOs
{
    //EGER FILTRELENECEK ALANLAR BELIRTILMESEYDI GENERIC BIR FILTER MEKANIZMASI YAZARAK DB DEN OKUNAN QUERYABLE EXTENSIONS METHOD YAZARAK BASELISTREQ GELEN NAME VE VALUE LARA GORE DB DEN SORGU CEKILEBILIR UZUN BIR ISLEM
    //public class SalesListReq : BaseListRequest
    //{
    //}

    public class SalesListReq
    {
        public DateTime? ProcessBeginTime { get; set; }
        public DateTime? ProcessEndTime { get; set; }
        public decimal? MinPriceAmount { get; set; }
        public decimal? MaxPriceAmount { get; set; }


    }

}
