using Application.Dtos;
using Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TransactionDto dto)
        {

            var result = await _transactionService.CreateAsync(dto);

            if (result.IsSuccess) return Ok(result);
            return BadRequest(result);
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> Get()
        {

            var result = await _transactionService.GetAll();

            if (result.IsSuccess) return Ok(result);
            return BadRequest(result);
        }

    }
}
