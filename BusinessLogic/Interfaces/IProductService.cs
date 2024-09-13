using Common.DTOs;
using Common.DTOs.Product;
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
        PagedResult<GetProductDTO> GetAllPaged(int pageNumber, int pageSize, string sortBy);
        List<GetProductDTO> GetByCategoryId(int categoryId);
        List<GetProductDTO> GetByRange(decimal minValue, decimal maxValue);
        List<GetProductDTO> OrderByPriceDescending();
        List<GetProductDTO> OrderByPriceAscending();
        List<GetProductDTO> OrderByDate();
        List<GetProductDTO> SearchByName(string name);
        PagedResult<GetProductDTO> GetByCategoryIdPaged(int categoryId, int pageNumber, int pageSize);
        PagedResult<GetProductDTO> SearchByNamePaged(string name, int pageNumber, int pageSize);
    }
}
