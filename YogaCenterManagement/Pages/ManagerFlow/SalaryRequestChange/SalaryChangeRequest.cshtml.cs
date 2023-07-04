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
    public class SalaryChangeRequestModel : PageModel
    {
        private readonly InstructorService _instructorService;
        private readonly SalaryChangeRequestService _salaryChangeRequestService;

        public SalaryChangeRequestModel(InstructorService instructorService, SalaryChangeRequestService salaryChangeRequestService)
        {
            _instructorService = instructorService;
            _salaryChangeRequestService = salaryChangeRequestService;
        }

        public IList<SalaryChangeRequest> SalaryChangeRequest { get; set; }

        public async Task OnGetAsync()
        {
            if (_salaryChangeRequestService.GetAll() is not null)
            {
                SalaryChangeRequest = _salaryChangeRequestService.GetAll(include: x => x.Include(a => a.Instructor).ThenInclude(b => b.Member)).OrderByDescending(x=>x.RequestDate).ToList();
            }
        }
    }
}
