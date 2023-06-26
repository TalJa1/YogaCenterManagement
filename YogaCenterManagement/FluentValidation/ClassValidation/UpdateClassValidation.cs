using FluentValidation;
using Repository.Models;
using System.Data;

namespace YogaCenterManagement.FluentValidation.ClassValidation
{
    public class UpdateClassValidation : AbstractValidator<Class>
    {
        public UpdateClassValidation()
        {
            RuleFor(model => model.ClassName)
                           .NotEmpty()
                           .WithMessage("Class name is required.");

            RuleFor(model => model.StartTime)
                .NotEmpty()
                .WithMessage("Start time is required.")
                .LessThan(p => (DateTime.Now).TimeOfDay).WithMessage("Start Date can not exceed today");

            RuleFor(model => model.EndTime)
                .NotEmpty()
                .GreaterThan(model => model.StartTime)
                .WithMessage("End time must be greater than start time.")
                .LessThan(p => (DateTime.Now).TimeOfDay).WithMessage("End Date can not exceed today");

            RuleFor(model => model.Capacity)
                .GreaterThan(0)
                .WithMessage("Capacity must be greater than 0.");

            RuleFor(model => model.MoneyNeedToPay)
                .GreaterThan(0)
                .WithMessage("Money need to pay must be greater than 0.");
        }
    }
}
