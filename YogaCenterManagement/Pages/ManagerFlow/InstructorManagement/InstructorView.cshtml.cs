﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.DAO;
using Repository.Models;

namespace YogaCenterManagement.Pages.ManagerFlow.InstructorManagement
{
    public class InstructorViewModel : PageModel
    {
        private readonly InstructorService _instructorService;
        private readonly MemberService _memberService;

        public InstructorViewModel(InstructorService instructorService, MemberService memberService)
        {
            _instructorService = instructorService;
            _memberService = memberService;
        }

        public IList<Instructor> Instructor { get; set; } = default!;

        public IActionResult OnGetAsync()
        {
            if (HttpContext.Session.GetString("email") == null || !HttpContext.Session.GetString("email").Equals("admin@admin.com"))
            {
                return RedirectToPage("/UserFlow/HomePage");
            }
            else
            {
                if (_instructorService.GetAll() is not null)
                {
                    Instructor = _instructorService.GetAll(include: x => x.Include(a => a.Member)).ToList();
                }
            }
            return Page();
        }
    }
}
