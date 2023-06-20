using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.DAO;
using Repository.Models;
using YogaCenterManagement.Pages.UserFlow;

namespace YogaCenterManagement.Pages.ManagerFlow
{
    public class CreateModel : PageModel
    {
        private readonly MemberService _memberService;
        private readonly ClassService _classService;
        private readonly InstructorService _instructorService;
        private readonly RoomService _roomService;
        public CreateModel(MemberService memberService, ClassService classService, InstructorService instructorService, RoomService roomService)
        {
            _memberService = memberService;
            _classService = classService;
            _instructorService = instructorService;
            _roomService = roomService;
        }

        public IActionResult OnGet()
        {
            ViewData["InstructorId"] = new SelectList(_instructorService.GetAll(), "InstructorId", "InstructorId");
            ViewData["RoomId"] = new SelectList(_roomService.GetAll(), "RoomId", "RoomName");
            return Page();
        }

        [BindProperty]
        public Class Class { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPostAsync()
        {
            if (!ModelState.IsValid || _instructorService.GetAll() is null || Class is null)
            {
                return Page();
            }
            _classService.Add(Class);
            return RedirectToPage("./Index");
        }
    }
}
