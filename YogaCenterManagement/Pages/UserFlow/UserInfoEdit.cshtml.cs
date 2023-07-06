using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository.DAO;
using Repository.Models;

namespace YogaCenterManagement.Pages.UserFlow
{
    public class UserInfoEditModel : PageModel
    {
        private readonly MemberService memberService;

        public UserInfoEditModel(MemberService memberService)
        {
            this.memberService = memberService;
        }

        [BindProperty]
        public Member Member { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToPage("HomePage");
            }
            if (!HttpContext.Session.GetString("email").Equals("admin@admin.com"))
            {
                var emailCheck = HttpContext.Session.GetString("email");
                var member = memberService.GetAll().FirstOrDefault(m => m.Email.Equals(emailCheck));
                if (member == null)
                {
                    ViewData["errSession"] = "Error occur";
                    return RedirectToPage("HomePage");
                }
                Member = member;
                return Page();
            }
            else
            {
                return NotFound();
            }
         
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            var memberCheck = memberService.GetAll().FirstOrDefault(m => m.MemberId == Member.MemberId);

            var emailCheckDuplicate = memberService.GetAll().FirstOrDefault(m => m.Email.Equals(Member.Email) && m.MemberId != Member.MemberId);

            if (emailCheckDuplicate == null)
            {
                memberCheck.Email = Member.Email;
                memberCheck.Address = Member.Address;
                memberCheck.Phone = Member.Phone;
                memberCheck.FullName = Member.FullName;
                memberCheck.Username = Member.Username;
                memberCheck.Password = Member.Password;
                memberCheck.Role = "member";

                memberService.Update(memberCheck);
                return RedirectToPage("HomePage");
            }
            else
            {
                ViewData["duplicateEmail"] = "Email is already in use";
                return Page();
            }
        }
    }
}
