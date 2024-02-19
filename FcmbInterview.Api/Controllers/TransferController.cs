using FcmbInterview.Application.Common.Interfaces.Persistence;
using FcmbInterview.Application.Common.Interfaces.Persistence.Common;
using FcmbInterview.Contract.Transfer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FcmbInterview.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransferController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<string>))]
        [HttpPost]
        [Route("{username}")]
        public async Task<ActionResult> DoTransferTransaction([FromBody] TransferRequest request, string userName)
        {
            var result = await _transactionRepository.DoTransfer(request.sourceAccount, request.destinationAccount, userName, request.amount);
            return StatusCode((int)result.StatusCode, result);

        }
    }
}
