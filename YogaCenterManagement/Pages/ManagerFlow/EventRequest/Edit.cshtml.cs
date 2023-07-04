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

namespace YogaCenterManagement.Pages.ManagerFlow.EventRequest
{
    public class EditModel : PageModel
    {
        private readonly EventRequestService _eventRequestService;
        private readonly InstructorService _instructorService;
        private readonly ClassService _classService;

        public EditModel(EventRequestService eventRequestService, InstructorService instructorService, ClassService classService)
        {
            _eventRequestService = eventRequestService;
            _instructorService = instructorService;
            _classService = classService;
        }

        [BindProperty]
        public Repository.Models.EventRequest EventRequest { get; set; } = default!;

        public IActionResult OnGetAsync(int? id)
        {
            if (HttpContext.Session.GetString("email") == null || !HttpContext.Session.GetString("email").Equals("admin@admin.com"))
            {
                return RedirectToPage("/UserFlow/HomePage");
            }
            if (id == null || _eventRequestService.GetAll() == null)
            {
                return NotFound();
            }

            var eventrequest =  _eventRequestService.GetAll().FirstOrDefault(m => m.RequestId == id);
            if (eventrequest == null)
            {
                return NotFound();
            }
            EventRequest = eventrequest;
           ViewData["ClassId"] = new SelectList(_classService.GetAll(), "ClassId", "ClassName");
           ViewData["InstructorId"] = new SelectList(_instructorService.GetAll(), "InstructorId", "InstructorId");
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
            _eventRequestService.Update(EventRequest);
            return RedirectToPage("./EventRequestView");
        }
    }
}
