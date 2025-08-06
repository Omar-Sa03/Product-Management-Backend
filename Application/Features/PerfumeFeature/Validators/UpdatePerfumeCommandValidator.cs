using Application.Common.Constants;
using Application.Common.Validator;
using Application.Features.PerfumeFeature.Commands;
using FluentValidation;

namespace Application.Features.PerfumeFeatures.UpdateValidators
{
    public class UpdatePerfumeCommandValidator : ValidatorBase<UpdatePerfumeCommand>
    {
        public UpdatePerfumeCommandValidator()
        {
            RuleFor(v => v.Name).NotEmpty()
                .WithMessage(ValidationConstants.NameMustHasValue);

            RuleFor(v => v.Brand).NotEmpty()
                .WithMessage(ValidationConstants.BrandMustHasValue);

            RuleFor(v => v.Description).NotEmpty()
               .WithMessage(ValidationConstants.DescriptionMustHasValue);

            RuleFor(v => v.Image).NotEmpty()
               .WithMessage(ValidationConstants.ImageMustHasValue);
        }
    }
}
