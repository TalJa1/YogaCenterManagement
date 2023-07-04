using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.DAO;
using Repository.Models;

namespace YogaCenterManagement.Pages.ManagerFlow.EventRequest
{
    public class EventRequestModel : PageModel
    {
        private readonly EventRequestService _eventRequestService;
        private readonly InstructorService _instructorService;
        private readonly ClassService _classService;

        public EventRequestModel(EventRequestService eventRequestService, InstructorService instructorService, ClassService classService)
        {
            _eventRequestService = eventRequestService;
            _instructorService = instructorService;
            _classService = classService;
        }

        public IList<Repository.Models.EventRequest> EventRequest { get; set; } = default!;

        public IActionResult OnGetAsync()
        {
            if (HttpContext.Session.GetString("email") == null || !HttpContext.Session.GetString("email").Equals("admin@admin.com"))
            {
                return RedirectToPage("/UserFlow/HomePage");
            }
            else
            {
                if (_eventRequestService.GetAll() != null)
                {
                    EventRequest = _eventRequestService.GetAll(include: x => x.Include(x => x.Instructor).ThenInclude(x => x.Member).Include(x => x.Class)).OrderByDescending(x => x.RequestId).ToList();
                }
            }
            return Page();
        }
    }
}
