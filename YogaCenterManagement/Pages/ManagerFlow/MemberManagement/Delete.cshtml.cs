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
    public class DeleteModel : PageModel
    {
        private readonly MemberService _memberService;

        public DeleteModel(MemberService memberService)
        {
            _memberService = memberService;
        }

        [BindProperty]
      public Member Member { get; set; } = default!;

        public IActionResult OnGetAsync(int? id)
        {
            if (HttpContext.Session.GetString("email") == null || !HttpContext.Session.GetString("email").Equals("admin@admin.com"))
            {
                return RedirectToPage("/UserFlow/HomePage");
            }
            if (id == null || _memberService.GetAll() == null)
            {
                return NotFound();
            }

            var member = _memberService.GetAll().FirstOrDefault(m => m.MemberId == id);

            if (member == null)
            {
                return NotFound();
            }
            else 
            {
                Member = member;
            }
            return Page();
        }

        public IActionResult OnPostAsync(int? id)
        {
            if (id == null || _memberService.GetAll() == null)
            {
                return NotFound();
            }
            var member = _memberService.getById(id);

            if (member != null)
            {
                Member = member;
                _memberService.Delete(member);
            }

            return RedirectToPage("./MemberView");
        }
    }
}
