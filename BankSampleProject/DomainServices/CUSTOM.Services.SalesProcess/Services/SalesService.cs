using CUSTOM.CommonHelpers;
using CUSTOM.SharedDTOs;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace CUSTOM.Services.SalesProcess.Services
{
    public class SalesService : ISalesService
    {
        private readonly RestClient _client;
        private readonly IConfiguration _config;

        private string baseUrl = string.Empty;

        public SalesService(IConfiguration config)
        {
            _client = new RestClient("https://lookup.binlist.net/");
            _config = config;
            baseUrl = _config.GetSection("AppSettings").GetSection("InternalApiBaseUserl").Value;
        }

        public async Task<AddSalesRes> AddSales(AddSalesReq req)
        {
            try
            {
                var validateReqRes = ValidateSalesRequest(req);

                if (validateReqRes.ResultCode != ResultCode.Success)
                    return validateReqRes;

                bool cardCheck = CardCheck.CardCheckWithLuhnAlg(req.CardNumber);

                if (!cardCheck)
                {
                    return new AddSalesRes
                    {
                        CardNumber = CardCheck.CardNumberMask(req.CardNumber),
                        IsSuccess = false,
                        PriceAmount = req.PriceAmount,
                        ResultCode = ResultCode.MissingOrInvalidData,
                        TransactionTime = DateTime.Now
                    };
                }

                //Card schema
                var request = new RestRequest($"{req.CardNumber}");
                var response = await _client.ExecuteGetAsync(request);

                if (!response.IsSuccessful)
                {
                    return new AddSalesRes
                    {
                        CardNumber = CardCheck.CardNumberMask(req.CardNumber),
                        IsSuccess = false,
                        PriceAmount = req.PriceAmount,
                        ResultCode = ResultCode.SystemError,
                        TransactionTime = DateTime.Now
                    };
                }

                var cardDetail = JsonSerializer.Deserialize<CardSchemaInfo>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                AddSalesDbReq internalApiReq = req; // implicit operator

                internalApiReq.CardType = (CardType)Enum.Parse(typeof(CardType), cardDetail!.Scheme, true);
                var apiResult = InternalApiCaller.AddSalesInfo(internalApiReq, baseUrl);

                return new AddSalesRes
                {
                    CardNumber = CardCheck.CardNumberMask(req.CardNumber),
                    IsSuccess = true,
                    PriceAmount = req.PriceAmount,
                    ResultCode = ResultCode.Success,
                    TransactionTime = DateTime.Now,
                    TransactionId = apiResult.TransactionId
                };
            }
            catch (Exception ex)
            {
                throw new Exception("", ex.InnerException);
            }
        }

        public async Task<SalesListRes> SalesList(SalesListReq req)
        {
            try
            {
                return InternalApiCaller.SalesList(req, baseUrl);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //private string CardNumberMask(string cardNumber)
        //{
        //    var reg = new Regex(@"(?<=\d{4}\d{2})\d{2}\d{4}(?=\d{4})|(?<=\d{4}( |-)\d{2})\d{2}\1\d{4}(?=\1\d{4})");
        //    return reg.Replace(cardNumber, new MatchEvaluator((m) => new String('*', m.Length)));
        //}

        private AddSalesRes ValidateSalesRequest(AddSalesReq req)
        {
            if (req.CardNumber is null)
            {
                return new AddSalesRes
                {
                    CardNumber = req.CardNumber,
                    PriceAmount = req.PriceAmount,
                    TransactionId = 0,
                    TransactionTime = DateTime.Now,
                    IsSuccess = false,
                    ResultCode = ResultCode.MissingOrInvalidData,
                    ResultMessage = "Kart numarası boş olamaz"
                };
            }

            if (req.CardHolderName is null)
            {
                return new AddSalesRes
                {
                    CardNumber = CardCheck.CardNumberMask(req.CardNumber),
                    PriceAmount = req.PriceAmount,
                    TransactionId = 0,
                    TransactionTime = DateTime.Now,
                    IsSuccess = false,
                    ResultCode = ResultCode.MissingOrInvalidData,
                    ResultMessage = "Kart ismi boş olamaz"
                };
            }

            if (req.ExpiryDate == 0)
            {
                return new AddSalesRes
                {
                    CardNumber = CardCheck.CardNumberMask(req.CardNumber),
                    PriceAmount = req.PriceAmount,
                    TransactionId = 0,
                    TransactionTime = DateTime.Now,
                    IsSuccess = false,
                    ResultCode = ResultCode.MissingOrInvalidData,
                    ResultMessage = "Kart son kullanma tarihi boş olamaz"
                };
            }

            return new AddSalesRes
            {
                IsSuccess = true,
                ResultCode = ResultCode.Success
            };
        }
    }
}