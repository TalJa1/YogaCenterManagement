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

namespace YogaCenterManagement.Pages.ManagerFlow.InstructorManagement
{
    public class UpdateStatusModel : PageModel
    {
        private readonly InstructorService _instructorService;
        private readonly MemberService _memberService;

        public UpdateStatusModel(InstructorService instructorService, MemberService memberService)
        {
            _instructorService = instructorService;
            _memberService = memberService;
        }

        [BindProperty]
        public Instructor Instructor { get; set; } = default!;

        public IActionResult OnGetAsync(int? id)
        {
            if (id == null || _instructorService.GetAll() == null)
            {
                return NotFound();
            }

            var instructor = _instructorService.GetAll().FirstOrDefault(m => m.InstructorId == id);
            if (instructor == null)
            {
                return NotFound();
            }
            Instructor = instructor;
            ViewData["MemberId"] = new SelectList(_memberService.GetAll(), "MemberId", "FullName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            _instructorService.Update(Instructor);
            return RedirectToPage("./Index");
        }
    }
}
