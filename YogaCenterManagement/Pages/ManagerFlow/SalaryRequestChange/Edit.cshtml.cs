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

namespace YogaCenterManagement.Pages.ManagerFlow.SalaryRequestChange
{
    public class EditModel : PageModel
    {
        private readonly InstructorService _instructorService;
        private readonly SalaryChangeRequestService _salaryChangeRequestService;

        public EditModel(InstructorService instructorService, SalaryChangeRequestService salaryChangeRequestService)
        {
            _instructorService = instructorService;
            _salaryChangeRequestService = salaryChangeRequestService;
        }

        [BindProperty]
        public SalaryChangeRequest SalaryChangeRequest { get; set; } = default!;

        public IActionResult OnGetAsync(int? id)
        {


            var salarychangerequest = _salaryChangeRequestService.GetAll().FirstOrDefault(m => m.RequestId == id);
            if (salarychangerequest == null)
            {
                return NotFound();
            }
            SalaryChangeRequest = salarychangerequest;
            ViewData["InstructorId"] = new SelectList(_instructorService.GetAll(), "InstructorId", "InstructorId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _salaryChangeRequestService.Update(SalaryChangeRequest);
            return RedirectToPage("./Index");
        }
    }
}
