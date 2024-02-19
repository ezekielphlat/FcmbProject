using FcmbInterview.Application.Common.Interfaces.Persistence.Common;
using FcmbInterview.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FcmbInterview.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task<GenericResponse<string>> AddUser(string userName, string userType);
        Task<GenericResponse<GetUserResponse>> UpdateUser(string userName, string userType);
        Task<GenericResponse<List<GetUserResponse>>> GetAllUser();
        Task<GenericResponse<GetUserResponse>> GetUserByUserName(string userName);
    }

    public record GetUserResponse(string userName, string userType);
}
