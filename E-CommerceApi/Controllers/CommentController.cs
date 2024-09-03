using BusinessLogic.DTOs.Comment;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace E_CommerceApi.Controllers
{
    [Route("[Controller]s")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<List<GetCommentDTO>> GetAll()
        {
            var comments = _commentService.GetAll();
            return Ok(comments);
        }

        [Authorize]
        [HttpGet("by-product-id")]
        public ActionResult<List<GetCommentDTO>> GetByProductId(int productId)
        {
            var comments = _commentService.GetByProductId(productId);
            if (comments == null || comments.Count == 0)
            {
                return NotFound("No comments found for the specified product.");
            }
            return Ok(comments);
        }

        [Authorize]
        [HttpGet("by-user-id")]
        public ActionResult<List<GetCommentDTO>> GetByUserId(int userId)
        {
            var comments = _commentService.GetByUserId(userId);
            if (comments == null || comments.Count == 0)
            {
                return NotFound("No comments found for the specified user.");
            }
            return Ok(comments);
        }

        [Authorize]
        [HttpPost("create")]
        public ActionResult Create([FromBody] CreateCommentDTO createCommentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _commentService.Create(createCommentDTO);
            return CreatedAtAction(nameof(GetByProductId), new { productId = createCommentDTO.ProductId }, createCommentDTO);
        }

        [Authorize]
        [HttpDelete("delete")]
        public ActionResult Delete(int id)
        {
            var comment = _commentService.GetByProductId(id);
            if (comment == null)
            {
                return NotFound("Comment not found.");
            }

            _commentService.Delete(id);
            return NoContent();
        }
    }
}
