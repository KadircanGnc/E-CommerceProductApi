using BusinessLogic.DTOs.Product;
using BusinessLogic.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IUserService
    {
        void Create(CreateUserDTO createUserDTO);
        void Update(UpdateUserDTO updateUserDTO);
        void Delete(int id);
        GetUserDTO GetById(int id);
        List<GetUserDTO> GetAll();
        List<GetProductDTO> GetOrdersByUserId(int userId);
        int GetCurrentUserId();

    }
}
