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

namespace YogaCenterManagement.Pages.InstructorFlow
{
    public class InstructorInforEditModel : PageModel
    {
        private readonly MemberService memberService;
        private readonly InstructorService instructorService;
        public InstructorInforEditModel(MemberService memberService, InstructorService instructorService)
        {
            this.memberService = memberService;
            this.instructorService = instructorService;
        }

        [BindProperty]
        public Member Member { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            var email = HttpContext.Session.GetString("email");
            Member = memberService.GetAll().FirstOrDefault(m => m.Email.Equals(email));
            if (Member != null)
            {
                var instructor = instructorService.GetAll().FirstOrDefault(i => i.MemberId == Member.MemberId);
                if (instructor != null)
                {
                    return Page();
                }
            }

            return RedirectToPage("../UserFlow/HomePage");
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
                memberCheck.Role = "instructor";

                memberService.Update(memberCheck);
                return RedirectToPage("../InstructorFlow/ClassList");
            }
            else
            {
                ViewData["duplicateEmail"] = "Email is already in use";
                return Page();
            }
        }
    }
}
