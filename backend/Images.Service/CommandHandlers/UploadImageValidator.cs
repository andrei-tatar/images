using FluentValidation;
using Images.Contracts.Commands;

namespace Images.Service.CommandHandlers
{
    public class UploadImageValidator : AbstractValidator<UploadImage>
    {
        public UploadImageValidator()
        {
            RuleFor(x => x.Image).NotNull().WithErrorCode(":(");
        }
    }
}
