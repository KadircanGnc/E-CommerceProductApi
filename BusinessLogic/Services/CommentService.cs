using AutoMapper;
using Common.DTOs.Comment;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IMapper _mapper;
        public CommentService(ICommentRepository commentRepository, IMapper mapper) 
        {
            _commentRepo = commentRepository;
            _mapper = mapper;
        }

        public void Create(CreateCommentDTO createCommentDTO)
        {
            if (createCommentDTO == null)
            {
                throw new ArgumentNullException(nameof(createCommentDTO), "Invalid Value!");
            }

            var comment = _mapper.Map<Comment>(createCommentDTO);
            _commentRepo.Create(comment);
        }
        
        public void Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id), "Invalid Value");
            }

            _commentRepo.Delete(id);
        }

        public List<GetCommentDTO> GetAll()
        {
            var comments = _commentRepo.GetAll();
            if (comments == null || !comments.Any())
            {
                throw new Exception("No Comments Found");
            }

            return _mapper.Map<List<GetCommentDTO>>(comments);
        }

        public List<GetCommentDTO> GetByProductId(int productId)
        {
            if (productId <= 0)
            {
                throw new ArgumentNullException(nameof(productId), "Invalid Value");
            }

            var comments = _commentRepo.GetByProductId(productId);
            if (comments == null)
            {
                throw new Exception("No Comments Found for the given Product ID");
            }            
            return _mapper.Map<List<GetCommentDTO>>(comments);
        }

        public List<GetCommentDTO> GetByUserId(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentNullException(nameof(userId), "Invalid Value");
            }

            var comments = _commentRepo.GetByUserId(userId);
            if (comments == null)
            {
                throw new Exception("No Comments Found for the given User ID");
            }

            return _mapper.Map<List<GetCommentDTO>>(comments);
        }

    }
}
