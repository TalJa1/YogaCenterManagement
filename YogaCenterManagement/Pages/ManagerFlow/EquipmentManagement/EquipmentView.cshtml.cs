using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.DAO;
using Repository.Models;

namespace YogaCenterManagement.Pages.ManagerFlow.EquipmentManagement
{
    public class EquipmentViewModel : PageModel
    {
        private readonly MemberService _memberService;
        private readonly ClassService _classService;
        private readonly InstructorService _instructorService;
        private readonly RoomService _roomService;
        private readonly EquipmentService _equipmentService;

        public EquipmentViewModel(MemberService memberService, ClassService classService, InstructorService instructorService, RoomService roomService, EquipmentService equipmentService)
        {
            _memberService = memberService;
            _classService = classService;
            _instructorService = instructorService;
            _roomService = roomService;
            _equipmentService = equipmentService;
        }

        public IList<Equipment> Equipment { get; set; } = default!;

        public IActionResult OnGetAsync()
        {
            if (HttpContext.Session.GetString("email") == null || !HttpContext.Session.GetString("email").Equals("admin@admin.com"))
            {
                return RedirectToPage("/UserFlow/HomePage");
            }
            if (_equipmentService.GetAll != null)
            {
                Equipment = _equipmentService.GetAll().ToList();
            }
            return Page();
        }
    }
}
