using BusinessLogic.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface ICategoryService
    {
        void Create(CreateCategoryDTO createCategoryDTO);
        void Update(UpdateCategoryDTO updateCategoryDTO);
        void Delete(int id);
        GetCategoryDTO GetById(int id);
        List<GetCategoryDTO> GetAll();
    }
}
