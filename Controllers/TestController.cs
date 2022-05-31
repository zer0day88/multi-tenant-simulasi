using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using multi_tenant.Models;
using PIS.API.Helper;
using Utility.OKResponse.Helper;

namespace multi_tenant.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TestController : ControllerBase
    {
        private long userId;
        private long tenantId;

        [HttpGet]
        public IActionResult cekContext()
        {
            try
            {
                userId = HttpContextHelper.GetCurrentUserId(this.HttpContext);
                tenantId = HttpContextHelper.GetCurrenTenantId(this.HttpContext);
                return Ok($"{userId}-{tenantId}");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}