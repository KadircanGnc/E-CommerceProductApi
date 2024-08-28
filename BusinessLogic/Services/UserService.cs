using DataAccess.Repositories;
using Entities;
using System;
using System.Collections.Generic;
using AutoMapper;
using BusinessLogic.DTOs.Product;
using BusinessLogic.DTOs.User;

namespace BusinessLogic.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepo;
        private readonly IMapper _mapper;

        public UserService(UserRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
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

        public List<GetUserDTO> GetAll()
        {
            var users = _userRepo.GetAll();
            if (users == null || !users.Any())
            {
                throw new KeyNotFoundException("No users found.");
            }

            return _mapper.Map<List<GetUserDTO>>(users);
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
