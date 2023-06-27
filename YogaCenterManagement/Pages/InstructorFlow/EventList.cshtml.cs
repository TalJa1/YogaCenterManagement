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

namespace YogaCenterManagement.Pages.InstructorFlow
{
    public class EventListModel : PageModel
    {
        private readonly EventRequestService eventRequestService;
        private readonly InstructorService instructorService;
        private readonly ClassService classService;
        private readonly MemberService memberService;
        public EventListModel(EventRequestService eventRequestService, InstructorService instructorService, ClassService classService, MemberService memberService)
        {
            this.eventRequestService = eventRequestService;
            this.instructorService = instructorService;
            this.classService = classService;
            this.memberService = memberService;
        }
        [BindProperty]
        public IList<EventRequest> EventRequest { get; set; } = default!;
        [BindProperty]
        public Instructor Instructor { get; set; } = default!;
        [BindProperty]
        public IList<SelectListItem> Class { get; set; } = default!;
        public IActionResult OnGet()
        {
            var email = HttpContext.Session.GetString("email");
            var member = memberService.GetAll().FirstOrDefault(m => m.Email.Equals(email));
            if (member != null)
            {
                Instructor = instructorService.GetAll().FirstOrDefault(i => i.MemberId == member.MemberId);
                if (Instructor != null)
                {
                    EventRequest = eventRequestService.GetAll().ToList();
                    foreach(var item in EventRequest)
                    {
                        item.Class = classService.GetAll().FirstOrDefault(c => c.ClassId == item.ClassId);
                        item.Instructor = instructorService.GetAll().FirstOrDefault(i => i.InstructorId == item.InstructorId);
                        item.Instructor.Member = memberService.GetAll().FirstOrDefault(m => m.MemberId == item.Instructor.MemberId);
                    }

                    return Page();
                }
            }

            return RedirectToPage("../UserFlow/HomePage");
        }

    }
}
