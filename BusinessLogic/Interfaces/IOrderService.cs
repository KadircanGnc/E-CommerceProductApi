using BusinessLogic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IOrderService
    {
        void Create(int cartId);
        void Delete(int id);
        OrderDTO GetById(int id);
        List<OrderDTO> GetAll();
    }
}
