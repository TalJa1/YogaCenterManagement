using FluentValidation;
using Repository.DAO;
using Repository.Models;
using Repository.ViewModels;

namespace YogaCenterManagement.FluentValidation.EquipmentValidation
{
    public class CreateEquipmentValidation : AbstractValidator<Equipment>
    {
        private readonly EquipmentService _equipmentService;
        public CreateEquipmentValidation(EquipmentService equipmentService)
        {
            RuleFor(x => x.EquipmentName)
                .NotEmpty().WithMessage("EquipmentName is required.")
                .MaximumLength(50).WithMessage("EquipmentName must not exceed 50 characters.")
                .Must(Exist)
                .WithMessage("Equipment name is exist!");
            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Quantity is required.")
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
            _equipmentService = equipmentService;
        }
        private bool Exist(string name)
        {
            var equipment=_equipmentService.GetAll().FirstOrDefault(x=>x.EquipmentName==name);
            if (equipment is null)
            {
                return true;
            }
            return false;
        }
    }
}
