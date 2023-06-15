using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain.User;
using API.Controllers.User.DTO;
using API.Infraestructure.Database;
using API.Infraestructure.Database.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
namespace API.Repository.User
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        public UserRepository(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Users> RegisterAsync(Users user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<Users?> LoginAsync(String Name, String Password)
        {
            var userResult = await _context.Users.FirstOrDefaultAsync(b => b.Name.Equals(Name) && b.Password.Equals(Password));
            return userResult;
        }

        public async Task<Users?> GetOneById(Guid id)
        {
            var userResult = await _context.Users.FirstOrDefaultAsync(b => b.Id.Equals(id));
            return userResult;
        }
        public async Task<List<Users>> GetPaginatedAsync(int pageIndex = 1, int pageSize = 10, Users? searchUser = null)
        {
            var query = _context.Set<Users>().AsQueryable();

            if (searchUser != null)
            {
                if (searchUser.Id != Guid.Empty)
                {
                    query = query.Where(u => u.Id == searchUser.Id);
                }

                if (!string.IsNullOrEmpty(searchUser.Name))
                {
                    query = query.Where(u => u.Name.ToLower().Contains(searchUser.Name.ToLower()));
                }

                if (!string.IsNullOrEmpty(searchUser.Description))
                {
                    query = query.Where(u => u.Description != null && u.Description.ToLower().Contains(searchUser.Description.ToLower()));
                }

                if (!string.IsNullOrEmpty(searchUser.Image))
                {
                    query = query.Where(u => u.Image != null && u.Image.ToLower().Contains(searchUser.Image.ToLower()));
                }

                if (searchUser.DateOfBirth.HasValue)
                {
                    query = query.Where(u => u.DateOfBirth != null && u.DateOfBirth.Value.Date == searchUser.DateOfBirth.Value.Date);
                }

                if (searchUser.OnlineStatus.HasValue)
                {
                    query = query.Where(u => u.OnlineStatus != null && u.OnlineStatus.Value == searchUser.OnlineStatus.Value);
                }

                if (!string.IsNullOrEmpty(searchUser.Role))
                {
                    query = query.Where(u => u.Role.ToLower().Contains(searchUser.Role.ToLower()));
                }
            }

            var paginatedUsers = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return paginatedUsers;
        }
        public async Task<Users> UpdateAsync(Users entity)
        {
            _context.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<Boolean> DeleteAsync(Guid id)
        {
            var user = await Task.FromResult(_context.Users.FirstOrDefault(c => c.Id == id));
            if (user == null)
            {
                return false;
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}