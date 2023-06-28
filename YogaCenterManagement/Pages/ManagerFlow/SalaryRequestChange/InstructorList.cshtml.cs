using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.DAO;
using Repository.Models;

namespace YogaCenterManagement.Pages.ManagerFlow.SalaryRequestChange
{
    public class InstructorListModel : PageModel
    {
        private readonly InstructorService _instructorService;
        private readonly MemberService _memberService;

        public InstructorListModel(InstructorService instructorService, MemberService memberService)
        {
            _instructorService = instructorService;
            _memberService = memberService;
        }

        public IList<Instructor> Instructor { get; set; } = default!;

        public IActionResult OnGetAsync()
        {
            var email = HttpContext.Session.GetString("email");
            var member = _memberService.GetAll().FirstOrDefault(m => m.Email.Equals(email));
            var listInstructor = _instructorService
         .GetAll(include: x => x.Include(i => i.Member));
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int instructorId)
        {
            if (instructorId != null)
            {
                HttpContext.Session.SetInt32("InstructorId", instructorId);
                return RedirectToPage("ChangeRequest");
            }
            return Page();
        }
    }
}
