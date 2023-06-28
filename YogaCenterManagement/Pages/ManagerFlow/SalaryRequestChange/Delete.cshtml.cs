using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.DAO;
using Repository.Models;

namespace YogaCenterManagement.Pages.ManagerFlow.SalaryRequestChange
{
    public class DeleteModel : PageModel
    {
        private readonly InstructorService _instructorService;
        private readonly SalaryChangeRequestService _salaryChangeRequestService;

        public DeleteModel(InstructorService instructorService, SalaryChangeRequestService salaryChangeRequestService)
        {
            _instructorService = instructorService;
            _salaryChangeRequestService = salaryChangeRequestService;
        }

        [BindProperty]
        public SalaryChangeRequest SalaryChangeRequest { get; set; } = default!;

        public IActionResult OnGetAsync(int? id)
        {
            var salarychangerequest = _salaryChangeRequestService.GetAll().FirstOrDefault(m => m.RequestId == id);

            SalaryChangeRequest = salarychangerequest;

            return Page();
        }

        public IActionResult OnPostAsync(int? id)
        {
            var salarychangerequest = _salaryChangeRequestService.getById(id);

            if (salarychangerequest != null)
            {
                SalaryChangeRequest = salarychangerequest;
                _salaryChangeRequestService.Delete(SalaryChangeRequest);
            }
            return RedirectToPage("./Index");
        }
    }
}
