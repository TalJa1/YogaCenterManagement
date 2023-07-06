using System;
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
    public class ViewAttendenceModel : PageModel
    {
        private readonly InstructorService instructorService;
        private readonly MemberService memberService;
        private readonly AttendanceService attendanceService;
        private readonly ClassService classService;
        public ViewAttendenceModel(AttendanceService attendanceService, MemberService memberService, InstructorService instructorService, ClassService classService)
        {
            this.attendanceService = attendanceService;
            this.memberService = memberService;
            this.instructorService = instructorService;
            this.classService = classService;
        }

        [BindProperty]
        public IList<Attendance> Attendance { get; set; } = default!;
        [BindProperty]
        public bool IsAttendence { get; set; }


        public async Task<IActionResult> OnGetAsync(int? classId, int? memberId)
        {
            var email = HttpContext.Session.GetString("email");
            var member = memberService.GetAll().FirstOrDefault(m => m.Email.Equals(email));
            if (member != null)
            {
                var instructor = instructorService.GetAll().FirstOrDefault(i => i.MemberId == member.MemberId);
                if (instructor != null)
                {
                    Attendance = attendanceService.GetAll().Where(a => a.ClassId == classId && a.MemberId == memberId).ToList();
                    foreach(var attendance in Attendance)
                    {
                        attendance.Class = classService.GetAll().FirstOrDefault(c => c.ClassId == attendance.ClassId);
                        attendance.Member = memberService.GetAll().FirstOrDefault(m => m.MemberId == attendance.MemberId);
                    }
                    return Page();
                }
            }

            return RedirectToPage("../UserFlow/HomePage");
        }
    }
}
