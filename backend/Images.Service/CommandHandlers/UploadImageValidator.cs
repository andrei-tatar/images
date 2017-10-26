using System;
using FluentValidation;
using Images.Contracts.Commands;

namespace Images.Service.CommandHandlers
{
    public class UploadImageValidator : AbstractValidator<UploadImage>
    {
        public UploadImageValidator()
        {
            RuleFor(x => x.Image).NotNull().WithErrorCode("required");
            RuleFor(x => x.Description).NotEmpty().WithErrorCode("required");
            RuleFor(x => x.Date).GreaterThan(DateTime.MinValue).WithErrorCode("required");
            RuleFor(x => x.Location).NotEmpty().WithErrorCode("required");
        }
    }
}
