using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.DAO;
using Repository.Models;
using YogaCenterManagement.Pages.UserFlow;

namespace YogaCenterManagement.Pages.ManagerFlow
{
    public class CreateModel : PageModel
    {
        private readonly MemberService _memberService;
        private readonly ClassService _classService;
        private readonly InstructorService _instructorService;
        private readonly RoomService _roomService;
        private readonly IValidator<Class> _validation;
        public CreateModel(MemberService memberService, ClassService classService, InstructorService instructorService, RoomService roomService, IValidator<Class> validation)
        {
            _memberService = memberService;
            _classService = classService;
            _instructorService = instructorService;
            _roomService = roomService;
            _validation = validation;
        }

        public IActionResult OnGet()
        {
            if (TempData.TryGetValue("Errors", out var errors))
            {
                var errorMessages = (string[])errors;
                foreach (var errorMessage in errorMessages)
                {
                    ModelState.AddModelError("", errorMessage);
                }
            }
            var listInstructor = _instructorService.GetAll();
            var listRoom = _roomService.GetAll();
            Rooms = new SelectList(listRoom, "RoomId", "RoomName");
            Instructors = new SelectList(listInstructor, "InstructorId", "InstructorId");
            return Page();
        }

        [BindProperty]
        public Class Class { get; set; } = default!;
        public SelectList Instructors { get; set; }
        public SelectList Rooms { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPostAsync()
        {
            try
            {
                ValidationResult rs = _validation.Validate(Class);
                if (!rs.IsValid)
                {
                    ViewData["Fail"] = "Create Class Fail, Please Try Again!!!";
                    rs.AddToModelState(this.ModelState);
                    TempData["Errors"] = rs.Errors.Select(e => e.ErrorMessage).ToArray();
                    //return Page();
                    return RedirectToPage("Create");
                }
                if (_instructorService.GetAll() is null || Class is null)
                {
                    ViewData["Fail"] = "Create Class Fail, Please Try Again!!!";
                    return Page();
                }
                _classService.Add(Class);
                return RedirectToPage("ClassView");
            }
            catch (Exception ex)
            {
                ViewData["Fail"] = "An error occurred: " + ex.Message;
                return Page();
            }
        }
    }
}
