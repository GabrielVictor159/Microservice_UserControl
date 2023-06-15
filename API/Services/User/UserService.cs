using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers.User.DTO;
using API.Domain.User;
using API.Infraestructure.Database.Entities;
using API.Repository.User;
using AutoMapper;

namespace API.Services.User
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task<Object> RegisterUser(UserDomain domain)
        {
            var userDomainValidation = new UserDomainValidation().Validate(domain);
            if (!userDomainValidation.IsValid)
            {
                return userDomainValidation.ToString();
            }
            if (domain.Image != null)
            {
                // Tratar as imagens
            }
            Users user = _mapper.Map<Users>(domain);
            var userResult = await _userRepository.RegisterAsync(user);
            UserResponseDTO response = _mapper.Map<UserResponseDTO>(userResult);
            return response;
        }
        public async Task<String?> Login(LoginDTO dto)
        {
            UserDomain domain = _mapper.Map<UserDomain>(dto);
            var login = await _userRepository.LoginAsync(domain.Name, domain.Password);
            if (login == null)
            {
                return null;
            }
            // Gerar, armazenar e retornar o token 
            return "Sucesso";
        }
        public async Task<Object> GetPaginated(GetPaginationDTO dto)
        {
            var dtoValidate = new GetPaginationDTOValidation().Validate(dto);
            if (!dtoValidate.IsValid)
            {
                return dtoValidate.ToString();
            }
            List<Users> listUsers = await _userRepository.GetPaginatedAsync(dto.pageIndex, dto.pageSize, dto.searchUser);
            List<UserResponseDTO> listUsersResponse = _mapper.Map<List<UserResponseDTO>>(listUsers);
            return listUsersResponse;
        }
        public async Task<Object> Update(UserUpdateDTO dto)
        {
            var userSearch = await _userRepository.GetOneById(dto.Id);
            if (userSearch == null)
            {
                return "User not found";
            }
            UserDomain domain = _mapper.Map<UserDomain>(dto);
            var domainValidation = new UserDomainValidation().Validate(domain);
            if (!domainValidation.IsValid)
            {
                return domainValidation.ToString();
            }
            if (dto.Image != null)
            {
                // Tratar imagens
            }
            _mapper.Map(domain, userSearch);
            var userResult = await _userRepository.UpdateAsync(userSearch);
            UserResponseDTO response = _mapper.Map<UserResponseDTO>(userResult);
            return response;
        }
        public async Task<Boolean> Delete(Guid id)
        {
            // Tratar imagens
            Boolean result = await _userRepository.DeleteAsync(id);
            return result;
        }
    }
}