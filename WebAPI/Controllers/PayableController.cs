using Application.Services.Contracts;
using Microsoft.AspNetCore.Http;
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
        [HttpGet("info")]
        public async Task<ActionResult> Get()
        {

            var result = await _payableService.GetAll();

            if (result.IsSuccess) return Ok(result);
            return BadRequest(result);
        }
        [HttpGet]
        public async Task<ActionResult> GetPayables()
        {

            var result = await _payableService.GetAllPayables();

            if (result.IsSuccess) return Ok(result);
            return BadRequest(result);
        }
    }
}
