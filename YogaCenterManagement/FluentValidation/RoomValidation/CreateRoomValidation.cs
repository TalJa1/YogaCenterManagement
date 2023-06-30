using FluentValidation;
using Repository.DAO;
using Repository.Models;

namespace YogaCenterManagement.FluentValidation.RoomValidation
{
    public class CreateRoomValidation : AbstractValidator<Room>
    {
        RoomService _roomService;
        public CreateRoomValidation(RoomService roomService)
        {
            RuleFor(x => x.Capacity)
                .NotEmpty()
                .WithMessage("Capacity is required")
                .GreaterThan(0)
                .WithMessage("Capacity is greater than 0");
            RuleFor(x => x.RoomName)
                .NotEmpty()
                .WithMessage("RoomName is required")
                .Must(CheckName)
                .WithMessage("Duplicate RoomName");
            _roomService = roomService;
        }
        private bool CheckName(string name)
        {
            var roomName = _roomService.GetAll().FirstOrDefault(x => x.RoomName == name);
            if (roomName is null)
            {
                return true;
            }
            return false;
        }
    }
}
