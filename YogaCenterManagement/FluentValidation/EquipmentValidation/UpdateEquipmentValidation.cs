using FluentValidation;
using Repository.DAO;
using Repository.ViewModels;

namespace YogaCenterManagement.FluentValidation.EquipmentValidation
{
    public class UpdateEquipmentValidation : AbstractValidator<UpdateEquipmentViewModels>
    {
        private readonly EquipmentService _equipmentService;
        public UpdateEquipmentValidation(EquipmentService equipmentService)
        {
            RuleFor(x => x.EquipmentName)
                .MaximumLength(50)
                .WithMessage("EquipmentName must not exceed 50 characters.");
            RuleFor(x => x.Quantity)
                 .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
            _equipmentService = equipmentService;
        }
    }
}

