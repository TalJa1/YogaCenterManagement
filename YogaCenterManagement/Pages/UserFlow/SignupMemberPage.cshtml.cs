using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.DAO;
using Repository.Models;

namespace YogaCenterManagement.Pages.UserFlow
{
    public class SignupMemberPageModel : PageModel
    {
        private readonly MemberService memberService;

        public SignupMemberPageModel(MemberService memberService)
        {
            this.memberService = memberService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Member Member { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var emailCheck = memberService.GetAll().FirstOrDefault(m => m.Email.Equals(Member.Email));
            if(emailCheck == null)
            {
                Member.Role = "member";
                memberService.Add(Member);
            }

            return RedirectToPage("LoginPage");
        }
    }
}
