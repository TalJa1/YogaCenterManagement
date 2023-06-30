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
using Repository.ViewModels;

namespace YogaCenterManagement.Pages.ManagerFlow.RoomManagement
{
    public class CreateModel : PageModel
    {
        private readonly RoomService _roomService;
        private readonly MemberService _memberService;
        private readonly IValidator<Room> _validation;
        public CreateModel(RoomService roomService, MemberService memberService, IValidator<Room> validation)
        {
            _roomService = roomService;
            _memberService = memberService;
            _validation = validation;
        }

        public IActionResult OnGet()
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    // Add model errors to TempData
                    TempData["Errors"] = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                }
                catch (Exception ex)
                {
                    ViewData["Fail"] = "An error occurred: " + ex.Message;
                }
            }
            return Page();
        }

        [BindProperty]
        public Room Room { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                ValidationResult rs = _validation.Validate(Room);
                if (!rs.IsValid)
                {
                    rs.AddToModelState(this.ModelState);
                    TempData["Errors"] = rs.Errors.Select(e => e.ErrorMessage).ToArray();
                    return RedirectToPage("Create");
                }
                _roomService.Add(Room);

                return RedirectToPage("./RoomView");
            }
            catch (Exception ex)
            {
                ViewData["Fail"] = "An error occurred: " + ex.Message;
                return RedirectToPage("Create");
            }
        }
    }
}
