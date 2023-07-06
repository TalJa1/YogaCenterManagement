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
using Microsoft.EntityFrameworkCore;
using Repository.DAO;
using Repository.Models;
using Repository.ViewModels;
using YogaCenterManagement.FluentValidation.InstructorValidation;

namespace YogaCenterManagement.Pages.ManagerFlow.InstructorManagement
{
    public class CreateModel : PageModel
    {
        private readonly InstructorService _instructorService;
        private readonly MemberService _memberService;
        private readonly IValidator<Instructor> _validation;

        public CreateModel(InstructorService instructorService, MemberService memberService, IValidator<Instructor> validation)
        {
            _instructorService = instructorService;
            _memberService = memberService;
            _validation = validation;
        }

        public IActionResult OnGet([FromQuery] int? memberId)
        {
            try
            {
                if (HttpContext.Session.GetString("email") == null || !HttpContext.Session.GetString("email").Equals("admin@admin.com"))
                {
                    return RedirectToPage("/UserFlow/HomePage");
                }
                if (!ModelState.IsValid)
                {
                    // Add model errors to TempData
                    TempData["Errors"] = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                }
                var listIns = _instructorService.GetAll().Select(x => x.MemberId).ToList();
                var listMember = _memberService.GetAll().Where(m => m.Role == "instructor" && !listIns.Contains(m.MemberId)).ToList();

                if (memberId != null)
                {
                    ViewData["Member"] = new SelectList(listMember, "MemberId", "Username", memberId);
                }
                else
                {
                    ViewData["Member"] = new SelectList(listMember, "MemberId", "Username");
                }
            }
            catch (Exception ex)
            {
                ViewData["Fail"] = "An error occurred: " + ex.Message;
            }
            return Page();
        }

        [BindProperty]
        public Instructor Instructor { get; set; } = default!;
        [BindProperty]
        public int MemberId { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPostAsync()
        {
            try
            {
                ValidationResult rs = _validation.Validate(Instructor);
                if (!rs.IsValid)
                {
                    rs.AddToModelState(this.ModelState);
                    TempData["Errors"] = rs.Errors.Select(e => e.ErrorMessage).ToArray();
                    return RedirectToPage("Create");
                }
                var member = _memberService.getById(MemberId);
                if (member is null)
                {
                    throw new Exception("Member is not available now. Please choose another.");
                }
                var obj = new Instructor
                {
                    InstructorId = Instructor.InstructorId,
                    MemberId = member.MemberId,
                    IsSalaryChangeRequested = false,
                    Salary = Instructor.Salary,
                };
                _instructorService.Add(obj);
                return RedirectToPage("InstructorView");
            }
            catch (Exception ex)
            {
                ViewData["Fail"] = "An error occurred: " + ex.Message;
                return RedirectToPage("Create");
            }
        }
    }
}
