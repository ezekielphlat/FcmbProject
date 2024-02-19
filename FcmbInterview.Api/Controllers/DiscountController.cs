using FcmbInterview.Application.Common.Interfaces.Persistence;
using FcmbInterview.Application.Common.Interfaces.Persistence.Common;
using FcmbInterview.Contract.Transaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FcmbInterview.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountController(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<string>))]
        [HttpPost]
        [Route("addDiscount")]
        public async Task<ActionResult> PostDiscount([FromBody] DiscountRequest request)
        {
            var result = await _discountRepository.AddDiscount(request.name, request.userType.ToUpper(), request.percentage, request.limitedAmount);
            return StatusCode((int)result.StatusCode, result);
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<string>))]
        [HttpPost]
        [Route("allDiscount")]
        public async Task<ActionResult> GetAllDiscount()
        {
            var result = await _discountRepository.GetAllDiscount();
            return StatusCode((int)result.StatusCode, result);
        }
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<string>))]
        [HttpPost]
        [Route("{discountName}")]
        public async Task<ActionResult> GetDiscountByName(string discountName)
        {
            var result = await _discountRepository.GetDiscountByName(discountName);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
