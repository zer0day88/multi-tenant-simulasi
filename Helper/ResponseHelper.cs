using System;

namespace Utility.OKResponse.Helper
{
    public class ResponseHelper
    {
        public static ResponseModel<T> GetResponse<T>(T _data=default ,
            bool _responseResult=false, string _message="")
        {
            
            ResponseModel<T> response = new();

            try
            {
                response.ResponseResult = _responseResult;
                response.data = _data;
                response.message = _message;
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    
    public class ResponseModel<T>
    {
        public bool ResponseResult { get; set; } = false;
        public T data { get; set; }
        public string message { get; set; } = string.Empty;
    }
    
    
}
