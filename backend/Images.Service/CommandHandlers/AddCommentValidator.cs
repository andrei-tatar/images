using System;
using FluentValidation;
using Images.Contracts.Commands;

namespace Images.Service.CommandHandlers
{
    public class AddCommentValidator : AbstractValidator<AddComment>
    {
        public AddCommentValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithErrorCode("required");
            RuleFor(x => x.Comment).NotEmpty().WithErrorCode("required");
            RuleFor(x => x.Date).GreaterThan(DateTime.MinValue).WithErrorCode("required");
            RuleFor(x => x.Id).NotEmpty().WithErrorCode("required");
            RuleFor(x => x.ImageId).NotEmpty().WithErrorCode("required");
        }
    }
}
