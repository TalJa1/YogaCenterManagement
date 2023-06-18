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
            var account = memberService.GetAll().FirstOrDefault(p => p.Email.Equals(Account.Email) && p.Password.Equals(Account.Password) && p.Role.Equals("member"));
            if (account == null)
            {
                ViewData["Message"] = "You not have permission";
                return Page();
            }
            HttpContext.Session.SetString("email", account.Email);
            return RedirectToPage("HomePage");
        }
    }
}
