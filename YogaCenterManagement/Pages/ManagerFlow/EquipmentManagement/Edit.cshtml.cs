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

namespace YogaCenterManagement.Pages.ManagerFlow.EquipmentManagement
{
    public class EditModel : PageModel
    {
        private readonly MemberService _memberService;
        private readonly ClassService _classService;
        private readonly InstructorService _instructorService;
        private readonly RoomService _roomService;
        private readonly EquipmentService _equipmentService;
        private readonly IValidator<UpdateEquipmentViewModels> _validation;

        public EditModel(MemberService memberService, ClassService classService, InstructorService instructorService, RoomService roomService, EquipmentService equipmentService, IValidator<UpdateEquipmentViewModels> validation)
        {
            _memberService = memberService;
            _classService = classService;
            _instructorService = instructorService;
            _roomService = roomService;
            _equipmentService = equipmentService;
            _validation = validation;
        }

        [BindProperty]
        public UpdateEquipmentViewModels UpdateEquipmentViewModels { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
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
                Equipment equipment = _equipmentService.getById(id);
                if (equipment is null)
                {
                    return NotFound();
                }

                UpdateEquipmentViewModels = new UpdateEquipmentViewModels
                {
                    EquipmentId=equipment.EquipmentId,
                    EquipmentName = equipment.EquipmentName,
                    Quantity = equipment.Quantity
                };
            }
            catch (Exception ex)
            {
                ViewData["Fail"] = "An error occurred: " + ex.Message;
                return RedirectToPage("./EquipmentView");
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                ValidationResult rs = _validation.Validate(UpdateEquipmentViewModels);
                if (!rs.IsValid)
                {
                    rs.AddToModelState(this.ModelState);
                    TempData["Errors"] = rs.Errors.Select(e => e.ErrorMessage).ToArray();
                    return RedirectToPage("Edit");
                }
                Equipment obj = new Equipment
                {
                    EquipmentId=UpdateEquipmentViewModels.EquipmentId,
                    EquipmentName=UpdateEquipmentViewModels.EquipmentName,
                    Quantity=UpdateEquipmentViewModels.Quantity
                };
                _equipmentService.Update(obj);
                return RedirectToPage("./EquipmentView");
            }
            catch (Exception ex)
            {
                ViewData["Fail"] = "An error occurred: " + ex.Message;
                return RedirectToPage("./EquipmentView");
            }
        }
    }
}
