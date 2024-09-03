using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface ICommentRepository : IRepository<Comment>
    {
        List<Comment> GetByProductId(int productId);
        List<Comment> GetByUserId(int userId);
    }
}
