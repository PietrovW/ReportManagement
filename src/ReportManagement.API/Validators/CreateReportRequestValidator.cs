using FluentValidation;
using ReportManagement.API.Request;

namespace ReportManagement.API.Validators
{
    public class CreateReportRequestValidator : AbstractValidator<CreateReportRequest>
    {
        public CreateReportRequestValidator()
        {
            RuleFor(p => p.Name)
               .NotEmpty().WithMessage("{Name} should be not empty. NEVER!");
        }
    }
}
