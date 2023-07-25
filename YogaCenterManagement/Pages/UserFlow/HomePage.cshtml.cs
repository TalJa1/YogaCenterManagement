using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.DAO;
using Repository.Models;

namespace YogaCenterManagement.Pages.UserFlow
{
    public class HomePageModel : PageModel
    {
        private readonly ClassService classServive;
        private readonly InstructorService instructorService;
        private readonly RoomService roomService;
        private readonly EnrollmentService enrollmentService;
        private readonly MemberService memberService;
        private readonly SlotService slotService;

        public HomePageModel(ClassService classServive, InstructorService instructorService, RoomService roomService, EnrollmentService enrollmentService, MemberService memberService, SlotService slotService)
        {
            this.classServive = classServive;
            this.instructorService = instructorService;
            this.roomService = roomService;
            this.enrollmentService = enrollmentService;
            this.memberService = memberService;
            this.slotService = slotService;
        }

        public IList<Class> Class { get; set; } = default!;
        public List<string> InstructorNames { get; set; } = new List<string>();

        public async Task OnGetAsync()
        {
            if (classServive.GetAll() != null)
            {
                var listClass = classServive.GetAll();
                foreach (var item in listClass)
                {
                    if (item.Capacity <= enrollmentService.GetAll().Where(m => m.ClassId == item.ClassId).Count())
                    {
                        listClass.Remove(item);
                    }
                }
                Class = listClass;
                foreach (var item in Class)
                {
                    item.Instructor = instructorService.GetAll().FirstOrDefault(m => m.InstructorId == item.InstructorId);
                    item.Room = roomService.GetAll().FirstOrDefault(m => m.RoomId == item.RoomId);
                    item.Slot = slotService.GetAll().FirstOrDefault(m => m.SlotId == item.SlotId);

                    if (item.Instructor != null)
                    {
                        var instructor = memberService.GetAll().FirstOrDefault(m => m.MemberId == item.Instructor.MemberId);
                        if (instructor != null)
                        {
                            InstructorNames.Add(instructor.FullName);
                        }
                        else
                        {
                            InstructorNames.Add("Unknown");
                        }
                    }
                    else
                    {
                        InstructorNames.Add("Unknown");
                    }
                }
            }
        }

        public IActionResult OnPost()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("HomePage");
        }

        public bool IsEnrolled(int classId)
        {
            var email = HttpContext.Session.GetString("email");
            if (email != null)
            {
                var memberCheck = memberService.GetAll().FirstOrDefault(m => m.Email.Equals(email));
                if (memberCheck != null)
                {
                    return enrollmentService.GetAll().Any(e => e.MemberId == memberCheck.MemberId && e.ClassId == classId);
                }
            }
            return false;
        }

        public IActionResult OnPostClearErrorSession()
        {
            HttpContext.Session.Remove("error");
            return RedirectToPage("/UserFlow/HomePage");
        }

        public bool IsEnrolledAll()
        {
            var email = HttpContext.Session.GetString("email");
            if (email != null)
            {
                var memberCheck = memberService.GetAll().FirstOrDefault(m => m.Email.Equals(email));
                if (memberCheck != null)
                {
                    return enrollmentService.GetAll().Any(e => e.MemberId == memberCheck.MemberId);
                }
            }
            return false;
        }
    }
}
