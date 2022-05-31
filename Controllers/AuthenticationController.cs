using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using multi_tenant.Models;
using multi_tenant.Services;
using Newtonsoft.Json.Linq;
using Utility.OKResponse.Helper;

namespace multi_tenant.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthenticationController> _logger;
        
        public AuthenticationController(ILogger<AuthenticationController> logger,
            IAuthService authService)
        {
            this._logger = logger;
            this._authService = authService;
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK,
            Type = typeof(ResponseModel<AuthenticationResponse>))]
        public async Task<IActionResult> Login([FromBody] AuthenticationRequest param)
        {   
            try
            {
                var (val,message,result) =
                    await _authService.Authentication(param);
                
                if(val)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: "Login berhasil"
                    ));
                }

                if (message != "")
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: new JObject(),
                        _message: message
                    ));
                }
                
                return Ok(ResponseHelper.GetResponse(
                    _data: new JObject(),
                    _message: "Username dan Password salah"
                ));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"data {0}", param);
                throw;
            }
 
        }
    }
}