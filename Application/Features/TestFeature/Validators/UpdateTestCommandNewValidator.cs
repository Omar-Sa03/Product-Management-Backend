using Application.Common.Constants;
using Application.Common.Validator;
using Application.Features.TestFeature.Commands;
using FluentValidation;

namespace Application.Features.TestFeature.Validators
{
    public class UpdateTestCommandNewValidator : ValidatorBase<UpdateTestCommandNew>
    {
        public UpdateTestCommandNewValidator()
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
