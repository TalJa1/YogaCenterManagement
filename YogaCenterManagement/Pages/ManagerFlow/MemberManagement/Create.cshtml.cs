using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.DAO;
using Repository.Models;

namespace YogaCenterManagement.Pages.ManagerFlow.MemberManagement
{
    public class CreateModel : PageModel
    {
        private readonly MemberService _memberService;
        private readonly IValidator<Member> _validator;

        public CreateModel(MemberService memberService, IValidator<Member> validator)
        {
            _memberService = memberService;
            _validator = validator;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("email") == null || !HttpContext.Session.GetString("email").Equals("admin@admin.com"))
            {
                return RedirectToPage("/UserFlow/HomePage");
            }
            return Page();
        }

        [BindProperty]
        public Member Member { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                ValidationResult result = _validator.Validate(Member);
                if(!result.IsValid)
                {
                    result.AddToModelState(this.ModelState);
                    TempData["Errors"] = result.Errors.Select(x => x.ErrorMessage).ToArray();
                    return RedirectToPage("Create");
                }
                _memberService.Add(Member);
                return RedirectToPage("./MemberView");

            }catch (Exception ex)
            {
                ViewData["Fail"] = "An error occurred" + ex.Message;
                return RedirectToPage("Create");
            }
        }
    }
}
