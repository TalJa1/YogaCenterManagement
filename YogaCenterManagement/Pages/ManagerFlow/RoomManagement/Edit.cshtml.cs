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

namespace YogaCenterManagement.Pages.ManagerFlow.RoomManagement
{
    public class EditModel : PageModel
    {
        private readonly RoomService _roomService;
        private readonly MemberService _memberService;

        public EditModel(RoomService roomService, MemberService memberService)
        {
            _roomService = roomService;
            _memberService = memberService;
        }

        [BindProperty]
        public Room Room { get; set; } = default!;

        public IActionResult OnGetAsync(int? id)
        {
            if (id == null || _roomService.GetAll() == null)
            {
                return NotFound();
            }

            var room = _roomService.GetAll().FirstOrDefault(m => m.RoomId == id);
            if (room == null)
            {
                return NotFound();
            }
            Room = room;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            _roomService.Update(Room);
            return RedirectToPage("./RoomView");
        }
    }
}
