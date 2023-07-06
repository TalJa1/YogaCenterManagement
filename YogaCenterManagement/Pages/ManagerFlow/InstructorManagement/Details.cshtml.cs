using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.DAO;
using Repository.Models;

namespace YogaCenterManagement.Pages.ManagerFlow.InstructorManagement
{
    public class DetailsModel : PageModel
    {
        private readonly InstructorService _instructorService;
        private readonly MemberService _memberService;

        public DetailsModel(InstructorService instructorService, MemberService memberService)
        {
            _instructorService = instructorService;
            _memberService = memberService;
        }

        public Instructor Instructor { get; set; } = default!;

        public IActionResult OnGetAsync(int? id)
        {
            if (HttpContext.Session.GetString("email") == null || !HttpContext.Session.GetString("email").Equals("admin@admin.com"))
            {
                return RedirectToPage("/UserFlow/HomePage");
            }
            if (id is null || _instructorService.GetAll() == null)
            {
                return NotFound();
            }

            var instructor = _instructorService.GetAll().FirstOrDefault(m => m.InstructorId == id);
            if (instructor == null)
            {
                return NotFound();
            }
            else
            {
                Instructor = instructor;
            }
            return Page();
        }
    }
}
