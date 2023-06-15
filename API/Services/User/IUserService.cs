using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers.User.DTO;
using API.Domain.User;

namespace API.Services.User
{
    public interface IUserService
    {
        Task<Object> RegisterUser(UserDomain domain);
        Task<String?> Login(LoginDTO dto);
        Task<Object> GetPaginated(GetPaginationDTO dto);
        Task<Object> Update(UserUpdateDTO dto);
        Task<Boolean> Delete(Guid id);
    }
}