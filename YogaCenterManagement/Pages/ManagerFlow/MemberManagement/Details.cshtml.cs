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
    public class DetailsModel : PageModel
    {
        private readonly MemberService _memberService;

        public DetailsModel(MemberService memberService)
        {
            _memberService = memberService;
        }

      public Member Member { get; set; } = default!; 

        public IActionResult OnGetAsync(int? id)
        {
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
    }
}
