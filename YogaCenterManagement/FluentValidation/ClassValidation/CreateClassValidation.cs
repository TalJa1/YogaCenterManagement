using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Repository.Models;
using Repository.ViewModels;

namespace YogaCenterManagement.FluentValidation.ClassValidation
{
    public class CreateClassValidation : AbstractValidator<CreateClassViewModel>
    {
        public CreateClassValidation()
        {
            RuleFor(model => model.ClassName)
               .NotEmpty()
               .WithMessage("Class name is required.");

            RuleFor(model => model.BeginDate)
               .NotEmpty()
               .WithMessage("Begin Date is required.");

            RuleFor(model => model.EndDate)
               .NotEmpty()
               .WithMessage("End Date is required.")
               .Must((model, endDate) => endDate > model.BeginDate)
               .WithMessage("End Date must be greater than Begin Date.");

            RuleFor(model => model.Capacity)
                .GreaterThan(0)
                .WithMessage("Capacity must be greater than 0.");

            RuleFor(model => model.MoneyNeedToPay)
                .GreaterThan(0)
                .WithMessage("Money need to pay must be greater than 0.");
        }
    }
}
