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
    public class RoomViewModel : PageModel
    {
        private readonly RoomService _roomService;
        private readonly MemberService _memberService;

        public IList<Room> Room { get; set; } = default!;

        public RoomViewModel(RoomService roomService, MemberService memberService)
        {
            _roomService = roomService;
            _memberService = memberService;
        }

        public IActionResult OnGetAsync()
        {
            if (HttpContext.Session.GetString("email") == null || !HttpContext.Session.GetString("email").Equals("admin@admin.com"))
            {
                return RedirectToPage("/UserFlow/HomePage");
            }
            else
            {
                if (_roomService.GetAll() is not null)
                {
                    Room = _roomService.GetAll().ToList();
                }
            }
            return Page();
           
        }
    }
}
