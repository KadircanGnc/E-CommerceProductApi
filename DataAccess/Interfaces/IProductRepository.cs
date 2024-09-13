using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        List<Product> GetByCategoryId(int categoryId);
        List<Product> GetByIds(List<int> productIds);
        List<Product> OrderByPriceDescending();
        List<Product> OrderByPriceAscending();
        List<Product> OrderByDate();
        List<Product> GetByRange(decimal minValue, decimal maxValue);
        List<Product> SearchByName(string name);
    }
}
