using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.DAO;
using Repository.Models;

namespace YogaCenterManagement.Pages.InstructorFlow
{
    public class CreateEventModel : PageModel
    {
        private readonly EventRequestService eventRequestService;
        private readonly InstructorService instructorService;
        private readonly ClassService classService;
        private readonly MemberService memberService;
        public CreateEventModel(EventRequestService eventRequestService, InstructorService instructorService, ClassService classService, MemberService memberService)
        {
            this.eventRequestService = eventRequestService;
            this.instructorService = instructorService;
            this.classService = classService;
            this.memberService = memberService;
        }
        [BindProperty]
        public EventRequest EventRequest { get; set; } = default!;
        [BindProperty]
        public Instructor Instructor { get; set; } = default!;
        [BindProperty]
        public IList<SelectListItem> Class { get; set; } = default!;
        public IActionResult OnGet()
        {
            var email = HttpContext.Session.GetString("email");
            var member = memberService.GetAll().FirstOrDefault(m => m.Email.Equals(email));
            if(member != null)
            {
                Instructor = instructorService.GetAll().FirstOrDefault(i => i.MemberId == member.MemberId);
                if (Instructor != null)
                {
                    List<Class> list = classService.GetAll().Where(c => c.InstructorId == Instructor.InstructorId).ToList();
                    Class = list.Select(c => new SelectListItem { Value = c.ClassId.ToString(), Text = c.ClassName }).ToList();

                    return Page();
                }
            }
            
            return RedirectToPage("../UserFlow/HomePage");
        }

        
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var eventcheck = eventRequestService.GetAll().FirstOrDefault(e => e.EventName.Equals(EventRequest.EventName));
            if(eventcheck == null) 
            { 
                EventRequest.IsApproved = false;
                EventRequest.InstructorId = Instructor.InstructorId;
                eventRequestService.Add(EventRequest);

                return RedirectToPage("../InstructorFlow/ClassList");
            }
            ViewData["duplicate"] = "Already have this Event";
            return Page();
        }
    }
}
