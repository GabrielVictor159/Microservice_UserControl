using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.User
{
    using API.Controllers.User.DTO;
    using API.Domain.User;
    using API.Infraestructure.Database.Entities;
    public interface IUserRepository
    {
        Task<Users> RegisterAsync(Users user);
        Task<Users?> LoginAsync(String Name, String Password);
        Task<Users?> GetOneById(Guid id);
        Task<List<Users>> GetPaginatedAsync(int pageIndex = 0, int pageSize = 10, Users? searchUser = null);
        Task<Users> UpdateAsync(Users entity);
        Task<Boolean> DeleteAsync(Guid id);
    }
}