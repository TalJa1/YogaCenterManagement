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
            var memberEmail = HttpContext.Session.GetString("email");
            var member = memberService.GetAll().FirstOrDefault(m => m.Email.Equals(memberEmail));

            var enrolledClassIds = enrollmentService.GetAll().Where(m => m.MemberId == member.MemberId).Select(e => e.ClassId).ToList();
            var availableClasses = classService.GetAll().Where(c => !enrolledClassIds.Contains(c.ClassId)).ToList();

            ViewData["ClassId"] = new SelectList(availableClasses, "ClassId", "ClassName");
            return Page();
        }

        [BindProperty]
        public ClassChangeRequest ClassChangeRequest { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var memberEmail = HttpContext.Session.GetString("email");
            var member = memberService.GetAll().FirstOrDefault(m => m.Email.Equals(memberEmail));

            ClassChangeRequest classChangeRequest = new ClassChangeRequest
            {
                //RequestId = service.GetAll().OrderByDescending(m => m.RequestId).FirstOrDefault().RequestId + 1,
                MemberId = member.MemberId,
                ClassId = ClassChangeRequest.ClassId,
                RequestDate = DateTime.Now,
                IsApproved = false
            };

            service.Add(classChangeRequest);

            ViewData["success"] = "Request has been seen";
            return RedirectToPage("HomePage");
        }
    }
}
