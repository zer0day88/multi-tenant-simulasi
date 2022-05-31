using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PIS.API.Helper;

namespace multi_tenant.Helper
{
    public class TenantMiddleware
    {
        public async Task InvokeAsync(HttpContext context)
        {
            // HttpContextHelper.GetCurrentUserId(context);
            
            
        }
    }
}