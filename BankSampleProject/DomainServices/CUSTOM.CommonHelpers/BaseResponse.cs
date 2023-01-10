using CUSTOM.SharedDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOM.CommonHelpers
{
    public static class BaseResponse
    {
        public static TRes ResultMessage<TRes>(bool isSuccess, string? resultMessage, ResultCode? resultCode) where TRes : BaseRes, new()
        {
            BaseRes baseRes = new TRes();
            baseRes.IsSuccess = isSuccess;
            baseRes.ResultMessage = resultMessage;
            baseRes.ResultCode = resultCode;
            return (TRes)(object)(baseRes);
        }
    }
}
