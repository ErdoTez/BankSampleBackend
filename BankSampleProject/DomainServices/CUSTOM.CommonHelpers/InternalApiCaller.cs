using CUSTOM.SharedDTOs;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOM.CommonHelpers
{
    public static class InternalApiCaller
    {
        public static AddSalesRes AddSalesInfo(AddSalesDbReq req, string baseUrl)
        {
            string actionName = "api/SalesDbProcess/AddDbSales";
            try
            {
                return Post<AddSalesDbReq, AddSalesRes>(actionName, req,baseUrl);
            }
            catch (Exception ex)
            {
                return BaseResponse.ResultMessage<AddSalesRes>(false, ex.Message, ResultCode.SystemError);
            }
        }

        public static SalesListRes SalesList(SalesListReq req, string baseUrl)
        {
            string actionName = "api/SalesDbProcess/SalesDbList";
            try
            {
                return Post<SalesListReq, SalesListRes>(actionName, req, baseUrl);
            }
            catch (Exception ex)
            {
                return BaseResponse.ResultMessage<SalesListRes>(false, ex.Message, ResultCode.SystemError);
            }
        }

        private static TRes Post<TReq, TRes>(string resource, TReq req,string baseUrl)
        {
            try
            {
                //var baseUrl = "http://localhost:7255/"; // appSetting yada KeyVaulttan okunabilir(Hashicrop)

                RestClient client = new RestClient(baseUrl);
                RestRequest request = new RestRequest(resource, Method.POST);
                var reqJsonString = System.Text.Json.JsonSerializer.Serialize<TReq>(req);
                request.AddParameter("application/json", reqJsonString, ParameterType.RequestBody);

                var restResponse = client.Execute(request);

                if (restResponse.IsSuccessful && !string.IsNullOrWhiteSpace(restResponse.Content))
                {
                    var options = new System.Text.Json.JsonSerializerOptions();
                    options.PropertyNameCaseInsensitive = true;
                    options.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());

                    var result = System.Text.Json.JsonSerializer.Deserialize<TRes>(restResponse.Content, options);
                    return result;
                }
                else
                {
                    throw new Exception($"InternalApiCaller işlemi sırasında bir hata alınmıştır. Hata Detayı : {restResponse.StatusCode} -- {restResponse.StatusDescription}", restResponse.ErrorException);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"InternalApiCaller işlemi sırasında bir hata alınmıştır. Hata Detayı : {ex.Message}", ex);
            }
        }
    }
}