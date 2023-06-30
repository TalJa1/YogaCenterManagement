using FluentValidation;
using Repository.Models;

namespace YogaCenterManagement.FluentValidation.InstructorValidation
{
    public class CreateInstructorValidation : AbstractValidator<Instructor>
    {
        public CreateInstructorValidation()
        {
            RuleFor(x => x.Salary)
            .GreaterThan(0)
            .WithMessage("Salary is always greater than 0")
            .NotEmpty()
            .WithMessage("Salary is required");
        }
    }
}
