using Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayableController : ControllerBase
    {
        private readonly IPayableService _payableService;
        public PayableController(IPayableService payableService)
        {
            _payableService = payableService;
        }
        [Authorize]
        [HttpGet("info")]
        public async Task<ActionResult> Get()
        {

            var result = await _payableService.GetAll();

            if (result.IsSuccess) return Ok(result);
            return BadRequest(result);
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetPayables()
        {

            var result = await _payableService.GetAllPayables();

            if (result.IsSuccess) return Ok(result);
            return BadRequest(result);
        }
    }
}
