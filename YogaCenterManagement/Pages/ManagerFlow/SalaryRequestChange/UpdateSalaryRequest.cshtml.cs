using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository.DAO;
using Repository.Models;
using Repository.ViewModels;

namespace YogaCenterManagement.Pages.ManagerFlow.SalaryRequestChange
{
    public class UpdateSalaryRequestModel : PageModel
    {
        private readonly InstructorService _instructorService;
        private readonly SalaryChangeRequestService _salaryChangeRequestService;
        private readonly IValidator<SalaryChangeRequest> _validation;

        public UpdateSalaryRequestModel(InstructorService instructorService, SalaryChangeRequestService salaryChangeRequestService, IValidator<SalaryChangeRequest> validation)
        {
            _instructorService = instructorService;
            _salaryChangeRequestService = salaryChangeRequestService;
            _validation = validation;
        }

        [BindProperty]
        public SalaryChangeRequest SalaryChangeRequest { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync([FromQuery] int? instructorId)
        {
            try
            {
                var listInstructor = _instructorService.GetAll(include: x => x.Include(z => z.Member)).ToList();
                if (instructorId != null)
                {
                    ViewData["Instructor"] = new SelectList(listInstructor, "InstructorId", "Member.FullName", instructorId);
                }
                else ViewData["Instructor"] = new SelectList(listInstructor, "InstructorId", "Member.FullName");
            }
            catch (Exception ex)
            {
                ViewData["Fail"] = "An error occurred: " + ex.Message;
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                SalaryChangeRequest obj = new SalaryChangeRequest
                {
                    RequestId=SalaryChangeRequest.RequestId,
                    IsApproved= SalaryChangeRequest.IsApproved,
                    RequestDate=SalaryChangeRequest.RequestDate,
                    InstructorId=SalaryChangeRequest.InstructorId,
                    NewSalary=SalaryChangeRequest.NewSalary
                };
                _salaryChangeRequestService.Update(obj);
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ViewData["Fail"] = "An error occurred: " + ex.Message;
                return RedirectToPage("./SalaryChangeRequest");
            }
        }
    }
}
