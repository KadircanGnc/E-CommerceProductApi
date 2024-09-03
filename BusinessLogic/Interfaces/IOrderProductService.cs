using BusinessLogic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IOrderProductService
    {
        void Add(OrderProductDTO orderProductDTO);
        void Update(int orderId, List<OrderProductDTO> orderProductDTOs);
        List<OrderProductDTO> GetByOrderId(int orderId);
        List<OrderProductDTO> GetByProductId(int productId);
    }
}
