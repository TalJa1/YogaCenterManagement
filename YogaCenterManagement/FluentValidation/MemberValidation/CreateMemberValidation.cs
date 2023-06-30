using FluentValidation;
using Repository.DAO;
using Repository.Models;

namespace YogaCenterManagement.FluentValidation.MemberValidation
{
    public class CreateMemberValidation : AbstractValidator<Member>
    {
        MemberService _memberService;
        public CreateMemberValidation(MemberService memberService)
        {
            _memberService = memberService;

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is Required").Must(CheckEmail).WithMessage("Email is Exists");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is Required");
            RuleFor(x => x.Username).NotEmpty().WithMessage("User Name is Required").Must(CheckUserName).WithMessage("User Name is Exists");
            RuleFor(x => x.Role).NotEmpty().WithMessage("Role is Required");
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Full Name is Required");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone is Required");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is Required");
        }

        private bool CheckUserName(string userName)
        {
            var member = _memberService.GetAll().FirstOrDefault(x => x.Username == userName);
            if (member == null)
            {
                return true;
            }
            return false;
        }

        private bool CheckEmail(string email)
        {
            var member = _memberService.GetAll().FirstOrDefault(x => x.Email == email);
            if (member is null)
            {
                return true;
            }
            return false;
        }
    }
}
