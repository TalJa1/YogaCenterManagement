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

namespace YogaCenterManagement.Pages.ManagerFlow.MemberManagement
{
    public class EditModel : PageModel
    {
        private readonly MemberService _memberService;

        public EditModel(MemberService memberService)
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

            var member =  _memberService.GetAll().FirstOrDefault(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }
            Member = member;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
           _memberService.Update(Member);

            return RedirectToPage("./MemberView");
        }

    }
}
