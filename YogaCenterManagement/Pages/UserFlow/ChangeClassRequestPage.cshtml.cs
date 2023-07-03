using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Models;
using Repository.DAO;
using System.Diagnostics.Metrics;

namespace YogaCenterManagement.Pages.UserFlow
{
    public class ChangeClassRequestPageModel : PageModel
    {
        private readonly ClassChangeRequestService service;
        private readonly ClassService classService;
        private readonly MemberService memberService;
        private readonly EnrollmentService enrollmentService;

        public ChangeClassRequestPageModel(ClassChangeRequestService service, ClassService classService, MemberService memberService, EnrollmentService enrollmentService)
        {
            this.service = service;
            this.classService = classService;
            this.memberService = memberService;
            this.enrollmentService = enrollmentService;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToPage("HomePage");
            }
            if (!HttpContext.Session.GetString("email").Equals("admin@admin.com"))
            {
                var memberEmail = HttpContext.Session.GetString("email");
                var member = memberService.GetAll().FirstOrDefault(m => m.Email.Equals(memberEmail));

                var enrolledClassIds = enrollmentService.GetAll().Where(m => m.MemberId == member.MemberId).Select(e => e.ClassId).ToList();
                var availableClasses = classService.GetAll().Where(c => !enrolledClassIds.Contains(c.ClassId)).ToList();

                ViewData["ClassId"] = new SelectList(availableClasses, "ClassId", "ClassName");
                return Page();
            }
            else
            {
                return NotFound();
            }

        }

        [BindProperty]
        public ClassChangeRequest ClassChangeRequest { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int classId)
        {
            var memberEmail = HttpContext.Session.GetString("email");
            var member = memberService.GetAll().FirstOrDefault(m => m.Email.Equals(memberEmail));

            ClassChangeRequest classChangeRequest = new ClassChangeRequest
            {
                MemberId = member.MemberId,
                OldClassId = classId,
                NewClassId = ClassChangeRequest.NewClassId,
                RequestDate = DateTime.Now,
                IsApproved = false
            };

            service.Add(classChangeRequest);

            ViewData["success"] = "Request has been seen";
            return RedirectToPage("HomePage");
        }
    }
}
