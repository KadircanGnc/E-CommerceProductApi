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
            if (entity == null)
                throw new ArgumentNullException("Value is Null!");

            _userRepo.Create(entity);
        }
        public void UpdateUser(User entity)
        {
            var user = _userRepo.GetById(entity.Id);
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            _userRepo.Update(entity);
        }
        public void DeleteUser(int id)
        {
            if (_userRepo.GetById(id) is not null)
                _userRepo.Delete(id);
        }
        public User GetById(int id)
        {
            if (_userRepo.GetById(id) is not null)
                return _userRepo.GetById(id);

            throw new Exception("ID is not valid");
        }
        public List<User> GetUsers()
        {
            return _userRepo.GetAll();
        }

        public List<Product> GetOrdersByUserId(int userId)
        {
            if (userId != 0)
                return _userRepo.GetOrdersByUserId(userId);

            throw new Exception("ID is not valid");
        }
    }
}
