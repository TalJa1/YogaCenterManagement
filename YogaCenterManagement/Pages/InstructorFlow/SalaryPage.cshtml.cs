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
    public class SalaryPageModel : PageModel
    {
        private readonly InstructorService instructorService;
        private readonly MemberService memberService;
        private readonly SalaryChangeRequestService salaryChangeRequestService;
        public SalaryPageModel(SalaryChangeRequestService salaryChangeRequestService, MemberService memberService, InstructorService instructorService)
        {
            this.salaryChangeRequestService = salaryChangeRequestService;
            this.memberService = memberService;
            this.instructorService = instructorService;
        }
        [BindProperty]
        public Instructor Instructor { get; set; } = default!;

        [BindProperty]
        public SalaryChangeRequest SalaryChangeRequest { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var email = HttpContext.Session.GetString("email");
            var member = memberService.GetAll().FirstOrDefault(m => m.Email.Equals(email));
            if (member != null)
            {
                Instructor = instructorService.GetAll().FirstOrDefault(i => i.MemberId == member.MemberId);
                if (Instructor != null)
                {
                    return Page();
                }
            }

            return RedirectToPage("../UserFlow/HomePage");
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            SalaryChangeRequest.InstructorId = Instructor.InstructorId;
            SalaryChangeRequest.RequestDate = DateTime.Now;
            SalaryChangeRequest.IsApproved = false;
            salaryChangeRequestService.Add(SalaryChangeRequest);
            return RedirectToPage("../InstructorFlow/ClassList");
        }

       
    }
}
