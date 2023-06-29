using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.DAO;
using Repository.Models;

namespace YogaCenterManagement.Pages
{
    public class DashBoardModel : PageModel
    {
        private readonly MemberService _memberService;

        public DashBoardModel(MemberService memberService)
        {
            _memberService = memberService;
        }

        public IActionResult OnGet()
        {
            var email = HttpContext.Session.GetString("email");
            var member = _memberService.GetAll().Append(_memberService.GetAdminAccount()).FirstOrDefault(m => m.Email.Equals(email));
            if (member is null)
            {
                return RedirectToPage("../UserFlow/HomePage");
            }
            return Page();
        }
    }
}
