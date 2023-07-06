using FluentValidation;
using Repository.DAO;
using Repository.Models;

namespace YogaCenterManagement.FluentValidation.InstructorValidation
{
    public class CreateInstructorValidation : AbstractValidator<Instructor>
    {
        MemberService memberService;
        public CreateInstructorValidation()
        {
            RuleFor(x => x.Salary)
                .NotNull()
                .WithMessage("Salary is not null")
                .NotEmpty()
                .WithMessage("Salary is required")
                .GreaterThan(0)
                .WithMessage("Salary must greater than 0");
        }
    }
}
