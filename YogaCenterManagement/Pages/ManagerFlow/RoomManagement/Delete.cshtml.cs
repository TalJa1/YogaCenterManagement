﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.DAO;
using Repository.Models;

namespace YogaCenterManagement.Pages.ManagerFlow.RoomManagement
{
    public class DeleteModel : PageModel
    {
        private readonly RoomService _roomService;
        private readonly MemberService _memberService;

        public DeleteModel(RoomService roomService, MemberService memberService)
        {
            _roomService = roomService;
            _memberService = memberService;
        }

        [BindProperty]
        public Room Room { get; set; } = default!;

        public IActionResult OnGetAsync(int? id)
        {
            if (HttpContext.Session.GetString("email") == null || !HttpContext.Session.GetString("email").Equals("admin@admin.com"))
            {
                return RedirectToPage("/UserFlow/HomePage");
            }
            if (id == null || _roomService.GetAll() == null)
            {
                return NotFound();
            }

            var room = _roomService.GetAll().FirstOrDefault(m => m.RoomId == id);

            if (room == null)
            {
                return NotFound();
            }
            else
            {
                Room = room;
            }
            return Page();
        }

        public  IActionResult OnPostAsync(int? id)
        {
            if (id == null || _roomService.GetAll() == null)
            {
                return NotFound();
            }
            var room = _roomService.getById(id);

            if (room != null)
            {
                Room = room;
                _roomService.Delete(room);
            }
            return RedirectToPage("./RoomView");
        }
    }
}
