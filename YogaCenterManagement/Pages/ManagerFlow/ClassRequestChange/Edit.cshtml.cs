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

namespace YogaCenterManagement.Pages.ManagerFlow.ClassRequestChange
{
    public class EditModel : PageModel
    {
        private readonly ClassChangeRequestService _classChangeRequestService;
        private readonly ClassService _classService;
        private readonly MemberService _memberService;

        public EditModel(ClassChangeRequestService classChangeRequestService, ClassService classService, MemberService memberService)
        {
            _classChangeRequestService = classChangeRequestService;
            _classService = classService;
            _memberService = memberService;
        }

        [BindProperty]
        public ClassChangeRequest ClassChangeRequest { get; set; } = default!;

        public IActionResult OnGetAsync(int? id)
        {
            if (id == null || _classChangeRequestService.GetAll() == null)
            {
                return NotFound();
            }

            var classchangerequest =  _classChangeRequestService.GetAll().FirstOrDefault(m => m.RequestId == id);
            if (classchangerequest == null)
            {
                return NotFound();
            }
            ClassChangeRequest = classchangerequest;
           ViewData["MemberId"] = new SelectList(_memberService.GetAll(), "MemberId", "FullName");
           ViewData["NewClassId"] = new SelectList(_classService.GetAll(), "ClassId", "ClassName");
           ViewData["OldClassId"] = new SelectList(_classService.GetAll(), "ClassId", "ClassName");
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

            _classChangeRequestService.Update(ClassChangeRequest);

            return RedirectToPage("./ClassRequestView");
        }
    }
}
