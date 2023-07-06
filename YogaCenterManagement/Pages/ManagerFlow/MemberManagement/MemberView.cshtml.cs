using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.DAO;
using Repository.Models;

namespace YogaCenterManagement.Pages.ManagerFlow.MemberManagement
{
    public class MemberViewModel : PageModel
    {
        private readonly MemberService _memberService;

        public MemberViewModel(MemberService memberService)
        {
            _memberService = memberService;
        }

        public IList<Member> Member { get; set; } = default!;

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("email") == null || !HttpContext.Session.GetString("email").Equals("admin@admin.com"))
            {
                return RedirectToPage("/UserFlow/HomePage");
            }
            else
            {
                if (_memberService.GetAll() != null)
                {
                    Member = _memberService.GetAll().ToList();
                }
            }
            return Page();
        }
    }
}
