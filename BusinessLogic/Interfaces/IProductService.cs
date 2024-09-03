using BusinessLogic.DTOs;
using BusinessLogic.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IProductService
    {
        void Create(CreateProductDTO createProductDTO);
        void Update(UpdateProductDTO updateProductDTO);
        void Delete(int id);
        GetProductDTO GetById(int id);
        List<GetProductDTO> GetAll();
        PagedResult<GetProductDTO> GetAllPaged(int pageNumber, int pageSize);
        List<GetProductDTO> GetByCategoryId(int categoryId);
        List<GetProductDTO> GetByRange(double minValue, double maxValue);
        List<GetProductDTO> OrderByPriceDescending();
        List<GetProductDTO> OrderByPriceAscending();
        List<GetProductDTO> OrderByDate();
    }
}
