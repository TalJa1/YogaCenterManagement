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

namespace YogaCenterManagement.Pages.ManagerFlow.ClassRequestChange
{
    public class EditModel : PageModel
    {
        private readonly ClassChangeRequestService _classChangeRequestService;
        private readonly ClassService _classService;
        private readonly MemberService _memberService;
        private readonly EnrollmentService _enrollmentService;

        public EditModel(ClassChangeRequestService classChangeRequestService, ClassService classService, MemberService memberService, EnrollmentService enrollmentService)
        {
            _classChangeRequestService = classChangeRequestService;
            _classService = classService;
            _memberService = memberService;
            _enrollmentService = enrollmentService;
        }

        [BindProperty]
        public ClassChangeRequest ClassChangeRequest { get; set; } = default!;

        public IActionResult OnGetAsync(int? id)
        {
            if (HttpContext.Session.GetString("email") == null || !HttpContext.Session.GetString("email").Equals("admin@admin.com"))
            {
                return RedirectToPage("/UserFlow/HomePage");
            }
            else
            {
                if (id == null || _classChangeRequestService.GetAll() == null)
                {
                    return NotFound();
                }

                var classchangerequest = _classChangeRequestService.GetAll().FirstOrDefault(m => m.RequestId == id);
                if (classchangerequest == null)
                {
                    return NotFound();
                }
                ClassChangeRequest = classchangerequest;
                ViewData["MemberId"] = new SelectList(_memberService.GetAll(), "MemberId", "FullName");
                ViewData["NewClassId"] = new SelectList(_classService.GetAll(), "ClassId", "ClassName");
                ViewData["OldClassId"] = new SelectList(_classService.GetAll(), "ClassId", "ClassName");
            }
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
            var enrollments = _enrollmentService.GetAll().Where(e => e.ClassId == ClassChangeRequest.OldClassId);
            if (ClassChangeRequest.IsApproved is true)
            {
                foreach (var item in enrollments)
                {
                    item.ClassId = ClassChangeRequest.NewClassId;
                    _enrollmentService.Update(item);
                }
            }
            _classChangeRequestService.Update(ClassChangeRequest);
            return RedirectToPage("./ClassRequestView");
        }
    }
}
