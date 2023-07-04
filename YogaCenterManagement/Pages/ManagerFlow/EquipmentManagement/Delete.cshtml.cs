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
    public class DeleteModel : PageModel
    {
        private readonly MemberService _memberService;
        private readonly ClassService _classService;
        private readonly InstructorService _instructorService;
        private readonly RoomService _roomService;
        private readonly EquipmentService _equipmentService;

        public DeleteModel(MemberService memberService, ClassService classService, InstructorService instructorService, RoomService roomService, EquipmentService equipmentService)
        {
            _memberService = memberService;
            _classService = classService;
            _instructorService = instructorService;
            _roomService = roomService;
            _equipmentService = equipmentService;
        }

        [BindProperty]
        public Equipment Equipment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (HttpContext.Session.GetString("email") == null || !HttpContext.Session.GetString("email").Equals("admin@admin.com"))
            {
                return RedirectToPage("/UserFlow/HomePage");
            }
            if (id == null || _equipmentService.GetAll() == null)
            {
                return NotFound();
            }

            var equipment = _equipmentService.GetAll().FirstOrDefault(z => z.EquipmentId == id);

            if (equipment is null)
            {
                return NotFound();
            }
            else
            {
                Equipment = equipment;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _equipmentService.GetAll() == null)
            {
                return NotFound();
            }
            var equipment = _equipmentService.getById(id);

            if (equipment is not null)
            {
                Equipment = equipment;
                _equipmentService.Delete(Equipment);
            }
            return RedirectToPage("./EquipmentView");
        }
    }
}
