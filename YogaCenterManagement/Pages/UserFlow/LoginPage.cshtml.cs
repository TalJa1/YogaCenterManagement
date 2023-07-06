using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.DAO;
using Repository.Models;
using System.Security.Principal;

namespace YogaCenterManagement.Pages.UserFlow
{
    public class LoginPageModel : PageModel
    {
        public readonly MemberService memberService;
        public LoginPageModel(MemberService memberService)
        {
            this.memberService = memberService;
        }
        [BindProperty]
        public Member Account { get; set; } = default!;
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            var account = memberService.GetAll().Append(memberService.GetAdminAccount()).FirstOrDefault(p => p.Email.Equals(Account.Email) && p.Password.Equals(Account.Password));
            if (account is null)
            {
                ViewData["Message"] = "You not have permission";
                return Page();
            }
            if (account.Role.Equals("member"))
            {
                HttpContext.Session.SetString("email", account.Email);
                return RedirectToPage("HomePage");
            }else if (account.Role.Equals("instructor"))
            {
                HttpContext.Session.SetString("email", account.Email);
                return RedirectToPage("../InstructorFlow/ClassList");
            }else if (account.Role.Equals("Admin"))
            {
                HttpContext.Session.SetString("email", account.Email);
                return RedirectToPage("../ManagerFlow/Dashboard");
            }
            return Page();
        }
    }
}
