using Common.DTOs.Comment;
using FluentValidation;

namespace E_CommerceApi.Validators
{
    public class CommentDTOValidator
    {
        public class CreateCommentDTOValidator : AbstractValidator<CreateCommentDTO>
        {
            public CreateCommentDTOValidator()
            {
                RuleFor(c => c.CommentText).NotEmpty().WithMessage("Please enter a comment");                
            }
        }
    }
}
