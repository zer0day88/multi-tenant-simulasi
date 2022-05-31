using System;
using Utility.OKResponse.Helper;

namespace Utility.ErrorResponse.Helper
{
    public static class ErrorHelper
    {
        public static ResponseModel<ErrorModel> GetErrorResponse(Exception ex)
        {
            ErrorModel error = new();
            ResponseModel<ErrorModel> ErrorResponse = new();
            try
            {
                if (ex.InnerException is not null)
                {
                    error.InMessage = ex.InnerException.Message;
                    error.InStackTrace = ex.InnerException.StackTrace;

                }
                else
                {
                    error.InMessage = string.Empty;
                    error.InStackTrace = string.Empty;
                }

                error.OutMessage = ex.Message;
                error.OutStackTrace = ex.StackTrace;

                ErrorResponse.ResponseResult = false;
                ErrorResponse.data = error;
                ErrorResponse.message = string.Empty;

                return ErrorResponse;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

    public class ErrorModel
    {
        public string InMessage { get; set; }
        public string InStackTrace { get; set; }
        public string OutMessage { get; set; }
        public string OutStackTrace { get; set; }
    }
}
