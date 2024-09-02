using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class CommentRepository : GenericRepository<Comment>
    {
        public CommentRepository(ECommerceDbContext context) : base(context) { }

        public List<Comment> GetByProductId(int productId)
        {
            var result = _context.Comments
                .Include(c => c.Product)         //List
                .Include(c => c.User)
                .Where(c => c.ProductId == productId)
                .ToList(); 

            return result!;
        }

        public List<Comment> GetByUserId(int userId)
        {           
            var comments = _context.Comments
                .Include(c => c.Product)  // Optionally include related Product
                .Include(c => c.User)     // Optionally include related User
                .Where(c => c.UserId == userId)
                .ToList();

            return comments;
        }

        public override List<Comment> GetAll()
        {
            return _context.Comments
                .Include(c => c.Product)
                .Include(c => c.User)
                .ToList();
        }

    }
}
