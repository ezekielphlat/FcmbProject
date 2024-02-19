using FcmbInterview.Application.Common.Interfaces.Persistence.Common;
using FcmbInterview.Domain.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FcmbInterview.Application.Common.Interfaces.Persistence
{
    public interface IDiscountRepository
    {
        Task<GenericResponse<string>> AddDiscount(string name, string userType, string percentage, double discountLimit);
        Task<GenericResponse<string>> UpdateDiscount(string name, string userType, string percentage);
        Task<GenericResponse<List<DiscountResponse>>> GetAllDiscount();
        Task<GenericResponse<DiscountResponse>> GetDiscountByName(string discountName);
    }
    public record DiscountResponse (string  name, string userType, string percentage, double discountLimit);
}
