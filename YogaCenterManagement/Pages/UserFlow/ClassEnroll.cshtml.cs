using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.DAO;
using Repository.Models;

namespace YogaCenterManagement.Pages.UserFlow
{
    public class ClassEnrollModel : PageModel
    {
        private readonly ClassService classService;
        private readonly InstructorService instructorService;
        private readonly RoomService roomService;

        public ClassEnrollModel(ClassService classService, InstructorService instructorService, RoomService roomService)
        {
            this.classService = classService;
            this.instructorService = instructorService;
            this.roomService = roomService;
        }

        public Class Class { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            var classView = classService.GetAll().FirstOrDefault(m => m.ClassId == id);
            if (classView != null)
            {
                classView.Room = roomService.GetAll().FirstOrDefault(m => m.RoomId == classView.RoomId);
                classView.Instructor = instructorService.GetAll().FirstOrDefault(m => m.InstructorId == classView.InstructorId);
                Class = classView;
            }
            return Page();
        }

        public IActionResult OnPostPayAndEnroll(int classId)
        {
            string amountString = Request.Form["amount"];
            if (decimal.TryParse(amountString, out decimal amount))
            {
                var moneyCheck = classService.GetAll().FirstOrDefault(m => m.MoneyNeedToPay == amount);
                if (moneyCheck != null)
                {
                    ViewData["success"] = "Your payment was successful, and you are now enrolled in the class.";
                    return RedirectToPage("HomePage");
                }
                else
                {
                    ViewData["fail"] = "Not enough amounts or overpay, please check again";
                    return RedirectToPage("ClassEnroll");
                }
            }
            else
            {
                ViewData["fail"] = "Error occur";
                return RedirectToPage("ClassEnroll");
            }
        }


    }
}
