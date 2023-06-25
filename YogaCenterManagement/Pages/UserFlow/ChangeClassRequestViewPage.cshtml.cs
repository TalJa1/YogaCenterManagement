﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Repository.DAO;

namespace YogaCenterManagement.Pages.UserFlow
{
    public class ChangeClassRequestViewPageModel : PageModel
    {
        private readonly ClassChangeRequestService classChangeRequestService;
        private readonly MemberService memberService;
        private readonly ClassService classService;

        public ChangeClassRequestViewPageModel(ClassChangeRequestService classChangeRequestService, MemberService memberService, ClassService classService)
        {
            this.classChangeRequestService = classChangeRequestService;
            this.memberService = memberService;
            this.classService = classService;
        }

        public IList<ClassChangeRequest> ClassChangeRequest { get;set; } = default!;

        public IActionResult OnGetAsync()
        {
            var memberEmail = HttpContext.Session.GetString("email");
            var member = memberService.GetAll().FirstOrDefault(m => m.Email.Equals(memberEmail));
            
            if (classChangeRequestService.GetAll() != null && member != null)
            {
                ClassChangeRequest = classChangeRequestService.GetAll().Where(m => m.MemberId == member.MemberId).ToList();
                foreach (var item in ClassChangeRequest)
                {
                    item.Member = memberService.GetAll().FirstOrDefault(m => m.MemberId == item.MemberId);
                    item.Class = classService.GetAll().FirstOrDefault(m => m.ClassId == item.ClassId);
                }
            }
            else
            {
                ViewData["null"] = "No change request";
                return RedirectToPage();
            }
            return Page();
        }
    }
}