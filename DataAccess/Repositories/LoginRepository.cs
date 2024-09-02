using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class LoginRepository
    {
        private readonly ECommerceDbContext _context;
        public LoginRepository(ECommerceDbContext context) 
        {
            _context = context;
        }


    }
}
