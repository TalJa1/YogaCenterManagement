using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.DAO;
using Repository.Models;

namespace YogaCenterManagement.Pages.InstructorFlow
{
    public class CheckAttendenceModel : PageModel
    {
        private readonly InstructorService instructorService;
        private readonly MemberService memberService;
        private readonly AttendanceService attendanceService;
        private readonly EnrollmentService enrollmentService;
        public CheckAttendenceModel(AttendanceService attendanceService, MemberService memberService, InstructorService instructorService, EnrollmentService enrollmentService)
        {
            this.attendanceService = attendanceService;
            this.memberService = memberService;
            this.instructorService = instructorService;
            this.enrollmentService = enrollmentService;
        }
  
        [BindProperty]
        public IList<Enrollment> Member { get; set; } = default!;
        [BindProperty]
        public bool IsAttendence { get; set; }
        

        public async Task<IActionResult> OnGetAsync()
        {
            var email = HttpContext.Session.GetString("email");
            var id = HttpContext.Session.GetInt32("ClassId");
            var member = memberService.GetAll().FirstOrDefault(m => m.Email.Equals(email));
            if (member != null)
            {
                var instructor = instructorService.GetAll().FirstOrDefault(i => i.MemberId == member.MemberId);
                if (instructor != null)
                {
                    Member = enrollmentService.GetAll().Where(i => i.ClassId == id).ToList();
                    foreach(var item in Member)
                    {
                        item.Member = memberService.GetAll().FirstOrDefault(m => m.MemberId == item.MemberId);
                    }
                    return Page();
                }
            }

            return RedirectToPage("../UserFlow/HomePage");
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPost(int memberId, bool isAttendence)
        {


            var attendence = new Attendance();
            attendence.AttendanceDate = DateTime.Now;
            attendence.MemberId = memberId;
            attendence.ClassId = HttpContext.Session.GetInt32("ClassId");

            if (isAttendence)
            {
                attendence.IsPresent = true;
            }
            else 
            {
                attendence.IsPresent = false;
            }
            attendanceService.Add(attendence);
            
            return RedirectToPage("CheckAttendence");
        }
    }
}
