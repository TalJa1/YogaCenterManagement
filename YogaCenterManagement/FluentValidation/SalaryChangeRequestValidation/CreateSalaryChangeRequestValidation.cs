using FluentValidation;
using Repository.Models;

namespace YogaCenterManagement.FluentValidation.SalaryChangeRequestValidation
{
    public class CreateSalaryChangeRequestValidation : AbstractValidator<SalaryChangeRequest>
    {
        public CreateSalaryChangeRequestValidation()
        {
            RuleFor(x => x.NewSalary)
            .NotEmpty().WithMessage("NewSalary is required.")
            .GreaterThan(0).WithMessage("NewSalary must be greater than 0.");
        }
    }
}
