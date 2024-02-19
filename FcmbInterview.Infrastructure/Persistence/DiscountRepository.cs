using FcmbInterview.Application.Common.Interfaces.Persistence;
using FcmbInterview.Application.Common.Interfaces.Persistence.Common;
using FcmbInterview.Domain.Enum;
using FcmbInterview.Domain.Transactions;
using FcmbInterview.Domain.Users;
using FcmbInterview.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FcmbInterview.Infrastructure.Persistence
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly ApplicationDbContext _context;

        public DiscountRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<GenericResponse<string>> AddDiscount(string name, string userType, string percentage, double discountLimit)
        {
            try
            {
                var newDiscount = new Discount
                {
                    Name  = name,
                    UserType = Enum.Parse<UserTypes>(userType),
                    Percentage = double.Parse(percentage),
                    DiscountLimit = discountLimit
                };
                await _context.Discount.AddAsync(newDiscount);
                var result = await _context.SaveChangesAsync();
                if (result <= 0) throw new Exception("unable to create country");

                return new GenericResponse<string>()
                {
                    Data = "Successful",
                    Message = string.Empty,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    IsSuccess = true
                };

            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse<string>()
                {
                    Data = ex.StackTrace ?? string.Empty,
                    Message = ex.Message,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    IsSuccess = false
                };
            }
        }

        public async Task<GenericResponse<List<DiscountResponse>>> GetAllDiscount()
        {

            var result = await _context.Discount
                .Select(d => new DiscountResponse(d.Name, d.UserType.ToString(), d.Percentage.ToString(), d.DiscountLimit)).ToListAsync();

            return new GenericResponse<List<DiscountResponse>>()
            {
                Data = result,
                Message = string.Empty,
                StatusCode = System.Net.HttpStatusCode.OK,
                IsSuccess = true
            };
        }

        public async Task<GenericResponse<DiscountResponse>> GetDiscountByName(string discountName)
        {
            var result = await _context.Discount
                .Where(d => d.Name == discountName).Select(d => new DiscountResponse(d.Name, d.UserType.ToString(), d.Percentage.ToString(), d.DiscountLimit)).SingleAsync();

            return new GenericResponse<DiscountResponse>()
            {
                Data = result,
                Message = string.Empty,
                StatusCode = System.Net.HttpStatusCode.OK,
                IsSuccess = true
            };
        }

    

        public Task<GenericResponse<string>> UpdateDiscount(string name, string userType, string percentage)
        {
            throw new NotImplementedException();
        }
    }
}
