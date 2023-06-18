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
    public class HomePageModel : PageModel
    {
        private readonly ClassService classServive;
        private readonly InstructorService instructorService;
        private readonly RoomService roomService;
        private readonly EnrollmentService enrollmentService;

        public HomePageModel(ClassService classServive, InstructorService instructorService, RoomService roomService, EnrollmentService enrollmentService)
        {
            this.classServive = classServive;
            this.instructorService = instructorService;
            this.roomService = roomService;
            this.enrollmentService = enrollmentService;
        }

        public IList<Class> Class { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (classServive.GetAll() != null)
            {
                var listClass = classServive.GetAll();
                foreach (var item in listClass)
                {
                    if(item.Capacity <= enrollmentService.GetAll().Where(m => m.ClassId == item.ClassId).Count())
                    {
                        listClass.Remove(item);
                    }
                }
                Class = listClass;
                foreach (var item in Class)
                {
                    item.Instructor = instructorService.GetAll().FirstOrDefault(m => m.InstructorId == item.InstructorId);
                    item.Room = roomService.GetAll().FirstOrDefault(m => m.RoomId == item.RoomId);
                }
            }
        }

        public IActionResult OnPost()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("HomePage");
        }

        public IActionResult OnPostAddToCart(int productId)
        {
            _cartService.AddItemToCart(productId);
            FlowerBouquet = _service.GetAll();
            return Page();
        }
    }
}
