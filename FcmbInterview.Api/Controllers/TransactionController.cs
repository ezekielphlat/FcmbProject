using FcmbInterview.Application.Common.Interfaces.Persistence;
using FcmbInterview.Application.Common.Interfaces.Persistence.Common;
using FcmbInterview.Contract.Transfer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FcmbInterview.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        /// <summary>
        /// Returns all transacions with pagination capability
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<string>))]
        [HttpGet]
        [Route("transfer/allTransactions")]
        public async Task<ActionResult> GetAllTransations()
        {
            // Todo: add Pagination
            var result = await _transactionRepository.GetAllTransferTransaction();
            return StatusCode((int)result.StatusCode, result);
        }
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<string>))]
        [HttpGet]
        [Route("transfer/{accountNumber}")]
        public async Task<ActionResult> GetAllTransationsByAccountNumber(string accountNumber)
        {
            // Todo: add Pagination
            var result = await _transactionRepository.GetTransferTransactionsByAccountNumber(accountNumber);
            return StatusCode((int)result.StatusCode, result);
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<string>))]
        [HttpGet]
        [Route("airtime/allTransactions")]
        public async Task<ActionResult> GetAllAirtimeTransations()
        {
            // Todo: add Pagination
            var result = await _transactionRepository.GetAllAirtimeTransaction();
            return StatusCode((int)result.StatusCode, result);
        }
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<string>))]
        [HttpGet]
        [Route("transfer/{phoneNumber}")]
        public async Task<ActionResult> GetAllAirtimeTransationsByPhone(string phoneNumber)
        {
            // Todo: add Pagination
            var result = await _transactionRepository.GetAirtimeTransactionsByPhoneNumber(phoneNumber);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
