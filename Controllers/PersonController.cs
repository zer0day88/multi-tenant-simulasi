using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using multi_tenant.Models;
using multi_tenant.Services;
using Utility.OKResponse.Helper;

namespace multi_tenant.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly ILogger<PersonController> _logger;

        public PersonController(
            ILogger<PersonController> logger, IPersonService personService)
        {
            this._logger = logger;
            this._personService = personService;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK,
            Type = typeof(ResponseModel<IEnumerable<Person>>))]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result =
                    await _personService.PersonGetAll();

                if (result != null)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }

                return Ok(ResponseHelper.GetResponse(
                    _data: new List<Person>(),
                    _message: "data tidak ditemukan"
                ));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw;
            }
        }
    }
}