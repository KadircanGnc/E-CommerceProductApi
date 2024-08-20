using DataAccess.Repositories;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepo;
        public UserService(UserRepository userRepo)
        {
            _userRepo = userRepo;
        }
        public void CreateUser(User entity)
        {
            if (_userRepo.GetById(entity.Id) is null && entity.Id != 0)
                _userRepo.Create(entity);
        }
        public void UpdateUser(User entity)
        {
            if (_userRepo.GetById(entity.Id) is not null)
                _userRepo.Update(entity);
        }
        public void DeleteUser(User entity)
        {
            if (_userRepo.GetById(entity.Id) is not null)
                _userRepo.Delete(entity.Id);
        }
        public User GetById(int id)
        {
            if (_userRepo.GetById(id) is not null)
                return _userRepo.GetById(id);

            return null;
        }
        public List<User> GetUsers()
        {
            return _userRepo.GetAll();
        }

        public List<Product> GetOrdersByUserId(int userId)
        {
            if (userId != 0)
                return _userRepo.GetOrdersByUserId(userId);
            return null;
        }
    }
}
