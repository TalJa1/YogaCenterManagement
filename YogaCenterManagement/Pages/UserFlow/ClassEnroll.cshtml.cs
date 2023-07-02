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
        private readonly MemberService memberService;
        private readonly PaymentService paymentService;
        private readonly EnrollmentService enrollmentService;

        public ClassEnrollModel(ClassService classService, InstructorService instructorService, RoomService roomService, EnrollmentService enrollmentService, PaymentService paymentService, MemberService memberService)
        {
            this.classService = classService;
            this.instructorService = instructorService;
            this.roomService = roomService;
            this.enrollmentService = enrollmentService;
            this.paymentService = paymentService;
            this.memberService = memberService;
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

        public IActionResult OnPostPayAndEnroll(int classId, decimal amount)
        {
            //string amountString = Request.Form["amount"];
            //if (decimal.TryParse(amountString, out decimal amount))
            //{
            //var moneyCheck = classService.GetAll().FirstOrDefault(m => m.MoneyNeedToPay == amount);
            //if (moneyCheck != null)
            //{
            var emailCheck = HttpContext.Session.GetString("email");
            //var getLastPayment = paymentService.GetAll().OrderByDescending(m => m.PaymentId).FirstOrDefault();
            if (emailCheck != null)
            {
                var memberCheck = memberService.GetAll().FirstOrDefault(m => m.Email.Equals(emailCheck));
                Payment pay = new Payment
                {
                    ClassId = classId,
                    MemberId = memberCheck.MemberId,
                    Amount = amount,
                    PaymentDate = DateTime.Now
                };
                Enrollment enrollment = new Enrollment
                {
                    MemberId = memberCheck.MemberId,
                    ClassId = classId,
                    EnrollmentDate = DateTime.Now
                };
                paymentService.Add(pay);
                enrollmentService.Add(enrollment);
                ViewData["success"] = "Your payment was successful, and you are now enrolled in the class.";
            }
            return RedirectToPage("HomePage");
            //}
            //else
            //{
            //    ViewData["fail"] = "Not enough amounts or overpay, please check again";
            //    return RedirectToPage("ClassEnroll");
            //}
            //}
            //else
            //{
            //    ViewData["fail"] = "Error occur";
            //    return RedirectToPage("ClassEnroll");
            //}
        }


    }
}
