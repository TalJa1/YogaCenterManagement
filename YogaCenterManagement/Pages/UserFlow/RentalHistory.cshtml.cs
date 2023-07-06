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
    public class RentalHistoryModel : PageModel
    {
        private readonly EquipmentRentalService equipmentRentalService;
        private readonly MemberService memberService;
        private readonly EquipmentService equipmentService;

        public RentalHistoryModel(EquipmentRentalService equipmentRentalService, MemberService memberService, EquipmentService equipmentService)
        {
            this.equipmentRentalService = equipmentRentalService;
            this.memberService = memberService;
            this.equipmentService = equipmentService;
        }

        public IList<EquipmentRental> EquipmentRental { get; set; } = new List<EquipmentRental>();

        public IActionResult OnGetAsync()
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToPage("HomePage");
            }
            if (!HttpContext.Session.GetString("email").Equals("admin@admin.com"))
            {
                var member = HttpContext.Session.GetString("email");
                Member me = memberService.GetAll().FirstOrDefault(m => m.Email.Equals(member));
                var equipmentHistory = equipmentRentalService.GetAll();

                if (equipmentHistory != null)
                {
                    foreach (var item in equipmentHistory)
                    {
                        if (item.MemberId == me.MemberId)
                        {
                            item.Equipment = equipmentService.GetAll().FirstOrDefault(m => m.EquipmentId == item.EquipmentId);
                            EquipmentRental.Add(item);
                        }
                    }
                    return Page();
                }
                else
                {
                    ViewData["null"] = "there are no rental history.";
                    return Page();
                }
            }
            else
            {
                return NotFound();
            }
        }
    }
}
