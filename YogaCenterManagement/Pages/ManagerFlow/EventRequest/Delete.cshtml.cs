using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.DAO;
using Repository.Models;

namespace YogaCenterManagement.Pages.ManagerFlow.EventRequest
{
    public class DeleteModel : PageModel
    {
        private readonly EventRequestService _eventRequestService;
        private readonly InstructorService _instructorService;
        private readonly ClassService _classService;

        public DeleteModel(EventRequestService eventRequestService, InstructorService instructorService, ClassService classService)
        {
            _eventRequestService = eventRequestService;
            _instructorService = instructorService;
            _classService = classService;
        }

        [BindProperty]
      public Repository.Models.EventRequest EventRequest { get; set; } = default!;

        public IActionResult OnGetAsync(int? id)
        {
            if (id == null || _eventRequestService.GetAll() == null)
            {
                return NotFound();
            }

            var eventrequest = _eventRequestService.GetAll().FirstOrDefault(m => m.RequestId == id);

            if (eventrequest == null)
            {
                return NotFound();
            }
            else 
            {
                EventRequest = eventrequest;
            }
            return Page();
        }

        public IActionResult OnPostAsync(int? id)
        {
            if (id == null ||_eventRequestService.GetAll() == null)
            {
                return NotFound();
            }
            var eventrequest = _eventRequestService.getById(id);

            if (eventrequest != null)
            {
                EventRequest = eventrequest;
                _eventRequestService.Delete(eventrequest);
            }

            return RedirectToPage("./EventRequestView");
        }
    }
}
