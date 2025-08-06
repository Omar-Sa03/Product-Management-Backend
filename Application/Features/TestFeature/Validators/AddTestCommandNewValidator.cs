using Application.Common.Constants;
using Application.Common.Validator;
using Application.Features.TestFeature.Commands;
using FluentValidation;

namespace Application.Features.CustomerFeatures.Validators
{
    public class AddTestCommandNewValidator : ValidatorBase<AddTestCommandNew>
    {
        public AddTestCommandNewValidator()
        {
            RuleFor(v => v.name).NotEmpty()
                .WithMessage(ValidationConstants.FirstNameMustHasValue);

            RuleFor(v => v.companyName).NotEmpty()
                .WithMessage(ValidationConstants.CompanyNameMustHasValue);

            RuleFor(v => v.contact).NotEmpty()
               .WithMessage(ValidationConstants.ContactMustHasValue);

        }
    }
}
