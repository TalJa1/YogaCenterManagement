using System;
using System.Collections.Generic;
using System.Globalization;
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
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Repository.DAO;
using Repository.Models;
using Repository.ViewModels;
using YogaCenterManagement.Pages.UserFlow;

namespace YogaCenterManagement.Pages.ManagerFlow
{
    public class CreateModel : PageModel
    {
        private readonly MemberService _memberService;
        private readonly ClassService _classService;
        private readonly InstructorService _instructorService;
        private readonly RoomService _roomService;
        private readonly SlotService _slotService;
        private readonly IValidator<CreateClassViewModel> _validation;

        public CreateModel(MemberService memberService, ClassService classService, InstructorService instructorService, RoomService roomService, SlotService slotService, IValidator<CreateClassViewModel> validation)
        {
            _memberService = memberService;
            _classService = classService;
            _instructorService = instructorService;
            _roomService = roomService;
            _slotService = slotService;
            _validation = validation;
        }

        public IActionResult OnGet([FromQuery] int? instructorId, int? roomId, int slotId)
        {
            try
            {
                if (HttpContext.Session.GetString("email") == null || !HttpContext.Session.GetString("email").Equals("admin@admin.com"))
                {
                    return RedirectToPage("/UserFlow/HomePage");
                }
                else
                {
                    if (!ModelState.IsValid)
                    {
                        // Add model errors to TempData
                        TempData["Errors"] = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                    }
                    var listIns = _classService.GetAll().Select(c => c.InstructorId).Distinct().ToList();
                    var listInstructor = _instructorService.GetAll(include: x => x.Include(z => z.Member)).Where(i => !listIns.Contains(i.InstructorId)).ToList(); var listRoom = _roomService.GetAll();
                    var listSlot = _slotService.GetAll();
                    if (instructorId != null)
                    {
                        ViewData["Instructor"] = new SelectList(listInstructor, "InstructorId", "Member.FullName", instructorId);
                    }
                    else ViewData["Instructor"] = new SelectList(listInstructor, "InstructorId", "Member.FullName");
                    if (roomId != null)
                    {
                        ViewData["Room"] = new SelectList(listRoom, "RoomId", "RoomName", roomId);
                    }
                    else ViewData["Room"] = new SelectList(listRoom, "RoomId", "RoomName");
                    ViewData["Slot"] = new SelectList(listSlot, "SlotId", "SlotName", slotId);
                }
            }
            catch (Exception ex)
            {
                ViewData["Fail"] = "An error occurred: " + ex.Message;
            }
            return Page();
        }
        [BindProperty]
        public CreateClassViewModel createClassViewModel { get; set; }
        [BindProperty]
        public int RoomId { get; set; }
        [BindProperty]
        public int InstructorId { get; set; }
        [BindProperty]
        public int SlotId { get; set; }
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPostAsync()
        {
            try
            {
                ValidationResult rs = _validation.Validate(createClassViewModel);
                if (!rs.IsValid)
                {
                    rs.AddToModelState(this.ModelState);
                    TempData["Errors"] = rs.Errors.Select(e => e.ErrorMessage).ToArray();
                    return RedirectToPage("Create");
                }
                var instructor = _instructorService.getById(InstructorId);
                if (instructor is null)
                {
                    throw new Exception("Instructor is not available now. Please choose another.");
                }
                var room = _roomService.getById(RoomId);
                if (room is null)
                {
                    throw new Exception("Room is not available now. Please choose another.");
                }
                var slot = _slotService.getById(SlotId);
                if (slot is null)
                {
                    throw new Exception("Slot is never null please!");
                }

                var newClass = new Class
                {
                    ClassName = createClassViewModel.ClassName,
                    InstructorId = instructor.InstructorId,
                    RoomId = room.RoomId,
                    BeginDate = createClassViewModel.BeginDate,
                    EndDate = createClassViewModel.EndDate,
                    StartTime = createClassViewModel.StartTime,
                    EndTime = createClassViewModel.EndTime,
                    Capacity = createClassViewModel.Capacity,
                    IsApproved = true,
                    SlotId = slot.SlotId,
                    MoneyNeedToPay = createClassViewModel.MoneyNeedToPay,
                };
                _classService.Add(newClass);
                return RedirectToPage("ClassView");
            }
            catch (Exception ex)
            {
                ViewData["Fail"] = "An error occurred: " + ex.Message;
                return RedirectToPage("Create");
            }
        }
    }
}
