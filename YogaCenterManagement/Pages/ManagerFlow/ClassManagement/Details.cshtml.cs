using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.DAO;
using Repository.Models;

namespace YogaCenterManagement.Pages.ManagerFlow
{
    public class DetailsModel : PageModel
    {
        private readonly MemberService _memberService;
        private readonly ClassService _classService;
        private readonly InstructorService _instructorService;
        private readonly RoomService _roomService;

        public DetailsModel(MemberService memberService, ClassService classService, InstructorService instructorService, RoomService roomService)
        {
            _memberService = memberService;
            _classService = classService;
            _instructorService = instructorService;
            _roomService = roomService;
        }

        public Class Class { get; set; } 

        public IActionResult OnGet(int? id)
        {
            if (id is null || _classService.GetAll() is null)
            {
                return NotFound();
            }

            var listClass = _classService.GetAll().FirstOrDefault(m => m.ClassId == id);
            if (listClass is null)
            {
                return NotFound();
            }
            else
            {
                Class = listClass;
            }
            return Page();
        }
    }
}
