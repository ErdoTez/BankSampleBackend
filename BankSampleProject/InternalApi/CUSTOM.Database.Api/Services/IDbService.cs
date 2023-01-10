using CUSTOM.SharedDTOs;

namespace CUSTOM.Database.Api.Services
{
    public interface IDbService
    {
        Task<AddSalesRes> AddSales(AddSalesDbReq req);

        Task<SalesListRes> SalesDbList(SalesListReq req);

    }
}
