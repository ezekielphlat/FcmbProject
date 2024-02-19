using Azure;
using FcmbInterview.Application.Common.Interfaces.Persistence;
using FcmbInterview.Application.Common.Interfaces.Persistence.Common;
using FcmbInterview.Application.Common.Interfaces.Services;
using FcmbInterview.Domain.Enum;
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
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IDateTimeProvider _dateTimeProvider;

        public UserRepository(ApplicationDbContext context, IDateTimeProvider dateTimeProvider)
        {
            this._context = context;
            this._dateTimeProvider = dateTimeProvider;
        }
        public async Task<GenericResponse<string>> AddUser(string userName, string userType)
        {
            try
            {            
            var newUser = new User
            {
                UserName = userName,
                UserType = Enum.Parse<UserTypes>(userType),
                DateCreated = _dateTimeProvider.UtcNow,
                
            };
          await  _context.Users.AddAsync(newUser);
          var result = await _context.SaveChangesAsync();
                if (result <= 0) throw new Exception("unable to create user");

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

        public async Task<GenericResponse<List<GetUserResponse>>> GetAllUser()
        {
            var result = await _context.Users.Select(u => new GetUserResponse( u.UserName, u.UserType.ToString())).ToListAsync();

            return new GenericResponse<List<GetUserResponse>>()
            {
                Data = result,
                Message = string.Empty,
                StatusCode = System.Net.HttpStatusCode.OK,
                IsSuccess = true
            };
        }

        public async Task<GenericResponse<GetUserResponse>> GetUserByUserName(string userName)
        {
            var result = await  _context.Users.Where(u => u.UserName == userName).Select(u => new GetUserResponse(u.UserName, u.UserType.ToString())).SingleAsync();

            return new GenericResponse<GetUserResponse>()
            {
                Data = result,
                Message = string.Empty,
                StatusCode = System.Net.HttpStatusCode.OK,
                IsSuccess = true
            };
        }

        public Task<GenericResponse<GetUserResponse>> UpdateUser(string userName, string UserType)
        {
            throw new NotImplementedException();
        }

    }
}
