using DataAccess.Repositories;
using Entities;
using System;
using System.Collections.Generic;
using AutoMapper;
using BusinessLogic.DTOs;

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

        public void CreateUser(UserDTO userDTO)
        {
            if (userDTO == null)
                throw new ArgumentNullException(nameof(userDTO), "Value is Null!");

            var user = _mapper.Map<User>(userDTO);
            _userRepo.Create(user);
        }

        public void UpdateUser(UserDTO userDTO)
        {
            if (userDTO == null)
                throw new ArgumentNullException(nameof(userDTO), "Value is Null!");

            var existingUser = _userRepo.GetById(userDTO.Id);
            if (existingUser == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            var user = _mapper.Map<User>(userDTO);
            _userRepo.Update(user);
        }

        public void DeleteUser(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID!", nameof(id));
            }
            _userRepo.Delete(id);
        }

        public UserDTO GetById(int id)
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

            return _mapper.Map<UserDTO>(user);
        }

        public List<UserDTO> GetUsers()
        {
            var users = _userRepo.GetAll();
            if (users == null || !users.Any())
            {
                throw new KeyNotFoundException("No users found.");
            }

            return _mapper.Map<List<UserDTO>>(users);
        }

      /*  public List<ProductDTO> GetOrdersByUserId(int userId)
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

            return _mapper.Map<List<ProductDTO>>(products);
        } */
    }
}
