using BusinessLogic.DTOs.Comment;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface ICommentService
    {
        void Create(CreateCommentDTO createCommentDTO);
        void Delete(int id);
        List<GetCommentDTO> GetAll();
        List<GetCommentDTO> GetByProductId(int productId);
        List<GetCommentDTO> GetByUserId(int userId);
    }
}
