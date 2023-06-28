using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository.DAO;
using Repository.Models;

namespace YogaCenterManagement.Pages.ManagerFlow.SalaryRequestChange
{
    public class ChangeRequestModel : PageModel
    {
        private readonly InstructorService _instructorService;
        private readonly SalaryChangeRequestService _salaryChangeRequestService;
        private readonly MemberService _memberService;


        public ChangeRequestModel(InstructorService instructorService, SalaryChangeRequestService salaryChangeRequestService)
        {
            _instructorService = instructorService;
            _salaryChangeRequestService = salaryChangeRequestService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var email=HttpContext.Session.GetString("email");
            if (_salaryChangeRequestService.GetAll() != null)
            {
                SalaryChangeRequest = _salaryChangeRequestService.GetAll(include: x => x.Include(a => a.Instructor).ThenInclude(b => b.Member));
            }
            return Page();
        }
        [BindProperty]
        public bool IsApproved { get; set; }
        [BindProperty]
        public IList<SalaryChangeRequest> SalaryChangeRequest { get; set; }
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int requestId,int instructorId, bool isApproved)
        {
            var request = _salaryChangeRequestService.getById(requestId);
            var salaryChangeRequest = new SalaryChangeRequest();
            salaryChangeRequest.RequestId = request.RequestId;
            salaryChangeRequest.InstructorId = instructorId;
            salaryChangeRequest.NewSalary = request.NewSalary;
            if (isApproved)
            {
                salaryChangeRequest.IsApproved = true;
            }
            else
            {
                salaryChangeRequest.IsApproved = false;
            }
            _salaryChangeRequestService.Add(salaryChangeRequest);
            return RedirectToPage("./SalaryChangeRequest");
        }
    }
}
