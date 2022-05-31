using Microsoft.AspNetCore.Http;

namespace PIS.API.Helper
{
    
    public class HttpContextHelper
    {
        
        public static long GetCurrentUserId(HttpContext httpContext)
        {
            var userId = httpContext.Items["userId"];
            if (userId is not null)
            {
                return (long) userId;
            }
            return 0;
        }
        
        public static long GetCurrenTenantId(HttpContext httpContext)
        {
            var tenantId = httpContext.Items["tenantId"];
            if (tenantId is not null)
            {
                return (long) tenantId;
            }
            return 0;
        }
        
    }
}