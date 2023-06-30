using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.DAO;
using Repository.Models;

namespace YogaCenterManagement.Pages.ManagerFlow.ClassRequestChange
{
    public class DeleteModel : PageModel
    {
        private readonly ClassChangeRequestService _classChangeRequestService;
        private readonly ClassService _classService;
        private readonly MemberService _memberService;

        public DeleteModel(ClassChangeRequestService classChangeRequestService, ClassService classService, MemberService memberService)
        {
            _classChangeRequestService = classChangeRequestService;
            _classService = classService;
            _memberService = memberService;
        }

        [BindProperty]
      public ClassChangeRequest ClassChangeRequest { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _classChangeRequestService.GetAll() == null)
            {
                return NotFound();
            }

            var classchangerequest = _classChangeRequestService.GetAll().FirstOrDefault(m => m.RequestId == id);

            if (classchangerequest == null)
            {
                return NotFound();
            }
            else 
            {
                ClassChangeRequest = classchangerequest;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _classChangeRequestService.GetAll() == null)
            {
                return NotFound();
            }
            var classchangerequest = _classChangeRequestService.getById(id);

            if (classchangerequest != null)
            {
                ClassChangeRequest = classchangerequest;
                _classChangeRequestService.Delete(classchangerequest);
            }

            return RedirectToPage("./Index");
        }
    }
}
