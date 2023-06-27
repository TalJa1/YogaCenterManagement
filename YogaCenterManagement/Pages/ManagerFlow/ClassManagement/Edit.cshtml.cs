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

namespace YogaCenterManagement.Pages.ManagerFlow
{
    public class EditModel : PageModel
    {
        private readonly MemberService _memberService;
        private readonly ClassService _classService;
        private readonly InstructorService _instructorService;
        private readonly RoomService _roomService;

        public EditModel(MemberService memberService, ClassService classService, InstructorService instructorService, RoomService roomService)
        {
            _memberService = memberService;
            _classService = classService;
            _instructorService = instructorService;
            _roomService = roomService;
        }

        [BindProperty]
        public Class Class { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null || _classService.GetAll() == null)
            {
                return NotFound();
            }

            var listClass = _classService.GetAll().FirstOrDefault(m => m.ClassId == id);
            if (listClass is null)
            {
                return NotFound();
            }
            Class = listClass;
            ViewData["InstructorId"] = new SelectList(_instructorService.GetAll(), "InstructorId", "InstructorId");
            ViewData["RoomId"] = new SelectList(_roomService.GetAll(), "RoomId", "RoomName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Attach(Class).State = EntityState.Modified;
            _classService.Update(Class);
            return RedirectToPage("./ClassView");
        }
    }
}
