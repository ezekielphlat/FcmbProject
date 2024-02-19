using FcmbInterview.Application.Common.Interfaces.Persistence;
using FcmbInterview.Application.Common.Interfaces.Persistence.Common;
using FcmbInterview.Contract.Airtime;
using FcmbInterview.Contract.Transfer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace FcmbInterview.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirtimeController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;

        public AirtimeController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<string>))]
        [HttpPost]
        [Route("{username}")]
        public async  Task<ActionResult> DoAirtimeTransaction([FromBody] AirtimeRequest request, string username)
        {
            var result = await _transactionRepository.BuyAirtime(request.networkProvider,request.phoneNumber, username, request.amount);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
