using DataAccess.Interfaces;
using Entities;
using System;
using AutoMapper;
using Common.DTOs.Product;
using Common.DTOs.User;
using BusinessLogic.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IUserRepository userRepo, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        //Converts user id to integer from token value
        private int? GetUserIdFromToken()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(userIdClaim, out var userId) ? userId : (int?)null;
        }

        public int GetCurrentUserId()
        {
            return GetUserIdFromToken()!.Value;
        }       

        public void Create(CreateUserDTO createUserDTO)
        {
            if (createUserDTO == null)
                throw new ArgumentNullException(nameof(createUserDTO), "Value is Null!");

            var user = _mapper.Map<User>(createUserDTO);
            _userRepo.Create(user);
        }

        public void Update(UpdateUserDTO updateUserDTO)
        {
            if (updateUserDTO.Id <= 0)
                throw new ArgumentNullException(nameof(updateUserDTO), "Value is Null!");

            var existingUser = _userRepo.GetById(updateUserDTO.Id);
            if (existingUser == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            var user = _mapper.Map<User>(updateUserDTO);
            _userRepo.Update(user);
        }

        public void Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID!", nameof(id));
            }
            _userRepo.Delete(id);
        }

        public GetUserDTO GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID!", nameof(id));
            }

            var user = _userRepo.GetById(id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            return _mapper.Map<GetUserDTO>(user);
        }

        public List<UpdateUserDTO> GetAll()
        {
            var users = _userRepo.GetAll();
            if (users == null || !users.Any())
            {
                throw new KeyNotFoundException("No users found.");
            }

            return _mapper.Map<List<UpdateUserDTO>>(users);
        }

        public List<GetProductDTO> GetOrdersByUserId(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("Invalid User ID!", nameof(userId));
            }

            var products = _userRepo.GetOrdersByUserId(userId);
            if (products == null || !products.Any())
            {
                throw new KeyNotFoundException("No products found for this user.");
            }

            return _mapper.Map<List<GetProductDTO>>(products);
        } 
    }
}
