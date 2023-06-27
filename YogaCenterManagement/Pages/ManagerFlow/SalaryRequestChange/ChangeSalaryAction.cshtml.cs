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
    public class ChangeSalaryActionModel : PageModel
    {
        private readonly InstructorService _instructorService;
        private readonly SalaryChangeRequestService _salaryChangeRequestService;
        private readonly IValidator<SalaryChangeRequest> _validation;
        public ChangeSalaryActionModel(InstructorService instructorService, SalaryChangeRequestService salaryChangeRequestService, IValidator<SalaryChangeRequest> validation)
        {
            _instructorService = instructorService;
            _salaryChangeRequestService = salaryChangeRequestService;
            _validation = validation;
        }

        public IActionResult OnGet([FromQuery] int? instructorId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    // Add model errors to TempData
                    TempData["Errors"] = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                }
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

        [BindProperty]
        public SalaryChangeRequest SalaryChangeRequest { get; set; } = default!;
        [BindProperty]
        public int InstructorId { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var instructor=_instructorService.getById(InstructorId);
                ValidationResult rs = _validation.Validate(SalaryChangeRequest);
                if (!rs.IsValid)
                {
                    rs.AddToModelState(this.ModelState);
                    TempData["Errors"] = rs.Errors.Select(e => e.ErrorMessage).ToArray();
                    return RedirectToPage("ChangeSalaryAction");
                }
                var obj = new SalaryChangeRequest
                {
                    RequestId = SalaryChangeRequest.RequestId,
                    InstructorId = instructor.InstructorId,
                    NewSalary = SalaryChangeRequest.NewSalary,
                    RequestDate = DateTime.Now,
                    IsApproved = null
                };
                _salaryChangeRequestService.Add(obj);
                return RedirectToPage("./SalaryChangeRequest");
            }
            catch (Exception ex)
            {
                ViewData["Fail"] = "An error occurred: " + ex.Message;
                return RedirectToPage("ChangeSalaryAction");
            }
        }
    }
}
