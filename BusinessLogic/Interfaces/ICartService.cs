using Common.DTOs;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface ICartService
    {        
        void AddItems(List<int> productIds);
        void RemoveItems(List<int> productIds);
        void Clear();
        CartDTO Get();
        CartDTO GetByUserId(int userId);
        int GetUserId();
        int GetCartId();
        int GetItemCount();
        List<CartItemDTO> GetCartItems();
        void CreateCart();

	}
}
