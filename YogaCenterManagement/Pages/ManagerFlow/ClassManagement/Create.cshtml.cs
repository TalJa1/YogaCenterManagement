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
        private readonly IList<string> TIME_LIST = new List<string> { "07:00", "08:00", "09:00", "10:00", "11:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00" };

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
                if (!ModelState.IsValid)
                {
                    // Add model errors to TempData
                    TempData["Errors"] = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                }
                var listInstructor = _instructorService.GetAll(include: x => x.Include(z => z.Member)).ToList();
                var listRoom = _roomService.GetAll();
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
                ViewData["TimeList"] = new SelectList(TIME_LIST);
            }
            catch (Exception ex)
            {
                ViewData["Fail"] = "An error occurred: " + ex.Message;
            }
            return Page();
        }
        public IList<string> TimeList { get; set; }
        [BindProperty]
        public CreateClassViewModel createClassViewModel { get; set; }
        [BindProperty]
        public int RoomId { get; set; }
        [BindProperty]
        public int InstructorId { get; set; }
        [BindProperty]
        public int SlotId { get; set; }
        [BindProperty]
        public string TimeStart { get; set; }
        [BindProperty]
        public string TimeEnd { get; set; }



        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPostAsync()
        {
            try
            {
                var timeSpanStart = TimeStart;
                var timeSpanEnd = TimeEnd;
                ValidationResult rs = _validation.Validate(createClassViewModel);
                if (!rs.IsValid)
                {
                    rs.AddToModelState(this.ModelState);
                    TempData["Errors"] = rs.Errors.Select(e => e.ErrorMessage).ToArray();
                    return RedirectToPage("Create");
                }
                TimeSpan startTime;
                TimeSpan endTime;
                //ViewData["Fail"] = "Create Class Fail, Please Try Again!!!";
                //return Page();

                if (!TimeSpan.TryParseExact(TimeStart, @"hh\:mm", CultureInfo.CurrentCulture, out startTime))
                {
                    ModelState.AddModelError("", "Invalid start time format.");
                    return Page();
                }

                if (!TimeSpan.TryParseExact(TimeEnd, @"hh\:mm", CultureInfo.CurrentCulture, out endTime))
                {
                    ModelState.AddModelError("", "Invalid end time format.");
                    return Page();
                }
                var instructor = _instructorService.getById(InstructorId);
                if (instructor is null)
                {
                    throw new Exception("Instructor is not available now. Please choose another.");
                }
                if (string.IsNullOrEmpty(TimeStart) || string.IsNullOrEmpty(TimeEnd))
                {
                    throw new Exception("You must choose start time and end time ");
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
                //Validate Datetime
                if (endTime <= startTime)
                {
                    ModelState.AddModelError("", "End time must be greater than start time.");
                    return RedirectToPage("Create");
                }
                var newClass = new Class
                {
                    ClassName = createClassViewModel.ClassName,
                    InstructorId = instructor.InstructorId,
                    RoomId = room.RoomId,
                    BeginDate = createClassViewModel.BeginDate,
                    EndDate = createClassViewModel.EndDate,
                    StartTime = startTime,
                    EndTime = endTime,
                    Capacity = createClassViewModel.Capacity,
                    IsApproved = createClassViewModel.IsApproved,
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
