﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.DAO;
using Repository.Models;

namespace YogaCenterManagement.Pages.InstructorFlow
{
    public class ClassListModel : PageModel
    {
        private readonly ClassService classService;
        private readonly InstructorService instructorService;
        private readonly RoomService roomService;
        private readonly MemberService memberService;

        [BindProperty]
        public IList<Class> Class { get; set; } = default!;
        public ClassListModel(ClassService classService, InstructorService instructorService, RoomService roomService, MemberService memberService)
        {
            this.classService = classService;
            this.instructorService = instructorService;
            this.roomService = roomService;
            this.memberService = memberService;
        }

        public IActionResult OnGet()
        {
            /*
            var emailCheck = HttpContext.Session.GetString("email");
            var member = memberService.GetAll().FirstOrDefault(m => m.Email.Equals(emailCheck));
            if (member == null)
            {
                ViewData["errSession"] = "Error occur";
                return RedirectToPage("HomePage");
            }
            return Page();
            */
            Class = classService.GetAll();            
            if (Class != null)
            {
                foreach (var c in Class)
                {
                    c.Instructor = instructorService.GetAll().FirstOrDefault(i => i.InstructorId == c.InstructorId);
                    c.Room = roomService.GetAll().FirstOrDefault(r => r.RoomId == c.RoomId);
                    c.Instructor.Member = memberService.GetAll().FirstOrDefault(m => m.MemberId == c.Instructor.MemberId);
                }
            }
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {


            return Page();
        }
    }
}
