using CUSTOM.SharedDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOM.Services.SalesProcess.Services
{
    public interface ISalesService
    {
        Task<AddSalesRes> AddSales(AddSalesReq req);

        Task<SalesListRes> SalesList(SalesListReq req);
    }
}
