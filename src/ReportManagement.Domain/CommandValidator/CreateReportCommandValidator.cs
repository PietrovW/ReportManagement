using FluentValidation;
using ReportManagement.Application.Common;

namespace ReportManagement.Application.CommandValidator
{
    public class CreateReportCommandValidator: AbstractValidator<ICreateReportCommand>
    {
        public CreateReportCommandValidator()
        {
            RuleFor(p => p.Name)
               .NotEmpty().WithMessage("{Name} should be not empty. NEVER!");
        }
    }
}
