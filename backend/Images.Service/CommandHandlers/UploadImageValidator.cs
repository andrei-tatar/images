using FluentValidation;
using Images.Contracts.Commands;

namespace Images.Service.CommandHandlers
{
    public class UploadImageValidator : AbstractValidator<UploadImage>
    {
        public UploadImageValidator()
        {
            RuleFor(x => x.Image).NotNull().WithErrorCode("error:required");
            RuleFor(x => x.Description).NotEmpty().WithErrorCode("error:required");
            RuleFor(x => x.Tags).NotEmpty().WithErrorCode("error:required");
            RuleFor(x => x.Date).NotEmpty().WithErrorCode("error:required");
            RuleFor(x => x.Location).NotEmpty().WithErrorCode("error:required");
        }
    }
}
