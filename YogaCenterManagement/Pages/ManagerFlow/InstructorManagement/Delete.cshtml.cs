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
    public class DeleteModel : PageModel
    {
        private readonly InstructorService _instructorService;
        private readonly MemberService _memberService;

        public DeleteModel(InstructorService instructorService, MemberService memberService)
        {
            _instructorService = instructorService;
            _memberService = memberService;
        }

        [BindProperty]
        public Instructor Instructor { get; set; } = default!;

        public IActionResult OnGetAsync(int? id)
        {
            if (id is null || _instructorService.GetAll() is null)
            {
                return NotFound();
            }

            var instructor = _instructorService.GetAll().FirstOrDefault(m => m.InstructorId == id);

            if (instructor is null)
            {
                return NotFound();
            }
            else
            {
                Instructor = instructor;
            }
            return Page();
        }

        public  IActionResult OnPostAsync(int? id)
        {
            if (id is null || _instructorService.GetAll() == null)
            {
                return NotFound();
            }
            var instructor = _instructorService.getById(id);

            if (instructor != null)
            {
                Instructor = instructor;
                _instructorService.Delete(Instructor);
            }

            return RedirectToPage("./InstructorView");
        }
    }
}
