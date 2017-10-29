using FluentValidation;
using Images.Contracts.Commands;

namespace Images.Service.CommandHandlers
{
    public class RateImageValidator : AbstractValidator<RateImage>
    {
        public RateImageValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithErrorCode("required");
            RuleFor(x => x.ImageId).NotEmpty().WithErrorCode("required");
            RuleFor(x => x.Rate)
                .GreaterThanOrEqualTo(1).WithErrorCode("invalid_range")
                .LessThanOrEqualTo(5).WithErrorCode("invalid_range");
        }
    }
}