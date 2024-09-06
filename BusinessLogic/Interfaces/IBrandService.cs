using Common.DTOs.Brand;
using Common.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IBrandService
    {
        void Create(CreateBrandDTO createBrandDTO);
        void Update(UpdateBrandDTO updateBrandDTO);
        void Delete(int id);
        GetBrandDTO GetById(int id);
        List<GetBrandDTO> GetAll();
        List<GetProductDTO> GetProductsByBrandId(int brandId);
    }
}
