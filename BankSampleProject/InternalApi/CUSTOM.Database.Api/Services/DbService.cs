using CUSTOM.CommonHelpers;
using CUSTOM.DbRepository;
using CUSTOM.Models;
using CUSTOM.Models.Models;
using CUSTOM.SharedDTOs;
using System.Security.Cryptography;

namespace CUSTOM.Database.Api.Services
{
    public class DbService : IDbService
    {
        private readonly IRepository<DbContext> _repo;

        public DbService(IRepository<DbContext> repo)
        {
            _repo = repo;
        }
        public async Task<AddSalesRes> AddSales(AddSalesDbReq req)
        {
            try
            {
                AddSalesRes res = new AddSalesRes();
                long tempTransactionId = 0;
                var db = _repo.DbContext;
                CardTransactionInfo cardTransactionInfo = new CardTransactionInfo();
                cardTransactionInfo.TransactionId = tempTransactionId = GetNextInt64();
                cardTransactionInfo.CardholderName = req.CardHolderName;
                cardTransactionInfo.Pan = req.CardNumber;
                cardTransactionInfo.Amount = req.PriceAmount;
                cardTransactionInfo.ExpiryDate = req.ExpiryDate;
                cardTransactionInfo.CardType = (int)req.CardType;

                db.Add<CardTransactionInfo>(cardTransactionInfo);
                db.SaveChanges();

                if (db.SaveChanges() > 0)
                {
                    res.TransactionId = tempTransactionId;
                    res.IsSuccess = true;
                }
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }

        }

        public async Task<SalesListRes> SalesDbList(SalesListReq req)
        {
            SalesListRes res = new SalesListRes();
            res.SalesListDetail = new List<SalesListDetail>();
            try
            {
                var queryList = (from cardTransactionInfo in _repo.DbContext.CardTransactionInfo
                                 where (req.MinPriceAmount != null ? cardTransactionInfo.Amount > req.MinPriceAmount : (req.MaxPriceAmount != null ? cardTransactionInfo.Amount < req.MaxPriceAmount : cardTransactionInfo.Amount > 0))
                                 && (req.ProcessBeginTime != null ? cardTransactionInfo.CreatedAt > req.ProcessBeginTime : (req.ProcessEndTime != null ? cardTransactionInfo.CreatedAt < req.ProcessEndTime : cardTransactionInfo.CreatedAt > DateTime.MinValue))
                                 select cardTransactionInfo).ToList();

                foreach (var item in queryList)
                {
                    res.SalesListDetail.Add(new SalesListDetail
                    {
                        Amount = item.Amount,
                        CardholderName = item.CardholderName,
                        ExpiryDate = item.ExpiryDate,
                        TransactionId = item.TransactionId,
                        Pan = CardCheck.CardNumberMask(item.Pan),
                        CreatedAt = item.CreatedAt,
                        UpdatedAt = item.UpdatedAt
                    });
                }

                res.ResultCode = ResultCode.Success;
                res.IsSuccess = true;
            }
            catch (Exception ex)
            {

                throw new Exception($"{nameof(SalesDbList)} işlemi sırasında hata oluşmuştur", ex);
            }

            return res;
        }

        private long GetNextInt64()
        {
            var random = RandomNumberGenerator.Create();
            var bytes = new byte[sizeof(Int64)];
            random.GetNonZeroBytes(bytes);
            var result = BitConverter.ToInt64(bytes, 0);
            string pos = result.ToString().Replace("-", "").Substring(0, 12);
            return Convert.ToInt64(pos);
        }

    }
}
