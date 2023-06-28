using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.DAO;
using Repository.Models;

namespace YogaCenterManagement.Pages.InstructorFlow
{
    public class SalaryHistoryModel : PageModel
    {
        private readonly InstructorService instructorService;
        private readonly MemberService memberService;
        private readonly SalaryChangeRequestService salaryChangeRequestService;
        public SalaryHistoryModel(SalaryChangeRequestService salaryChangeRequestService, MemberService memberService, InstructorService instructorService)
        {
            this.salaryChangeRequestService = salaryChangeRequestService;
            this.memberService = memberService;
            this.instructorService = instructorService;
        }
        [BindProperty]
        public Instructor Instructor { get; set; } = default!;

        [BindProperty]
        public IList<SalaryChangeRequest> SalaryChangeRequest { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var email = HttpContext.Session.GetString("email");
            var member = memberService.GetAll().FirstOrDefault(m => m.Email.Equals(email));
            if (member != null)
            {
                Instructor = instructorService.GetAll().FirstOrDefault(i => i.MemberId == member.MemberId);
                if (Instructor != null)
                {
                    SalaryChangeRequest = salaryChangeRequestService.GetAll().Where(s => s.InstructorId == Instructor.InstructorId).ToList();
                    return Page();
                }
            }

            return RedirectToPage("../UserFlow/HomePage");
        }

    }
}
