using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository.DAO;
using Repository.Models;

namespace YogaCenterManagement.Pages.ManagerFlow.EquipmentRentalManagement
{
    public class EditModel : PageModel
    {
        private readonly EquipmentRentalService _equipmentRentalService;
        private readonly EquipmentService _equipmentService;
        private readonly MemberService _memberService;

        public EditModel(EquipmentRentalService equipmentRentalService, EquipmentService equipmentService, MemberService memberService)
        {
            _equipmentService = equipmentService;
            _memberService = memberService;
            _equipmentRentalService = equipmentRentalService;
        }

        [BindProperty]
        public EquipmentRental EquipmentRental { get; set; } = default!;

        public IActionResult OnGetAsync(int? id)
        {
            if (HttpContext.Session.GetString("email") == null || !HttpContext.Session.GetString("email").Equals("admin@admin.com"))
            {
                return RedirectToPage("/UserFlow/HomePage");
            }
            if (id == null || _equipmentRentalService.GetAll() == null)
            {
                return NotFound();
            }

            var equipmentrental = _equipmentRentalService.GetAll().FirstOrDefault(m => m.RentalId == id);
            if (equipmentrental == null)
            {
                return NotFound();
            }
            EquipmentRental = equipmentrental;
            ViewData["EquipmentId"] = new SelectList(_equipmentService.GetAll(), "EquipmentId", "EquipmentId");
            ViewData["MemberId"] = new SelectList(_memberService.GetAll(), "MemberId", "MemberId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            var euquipment = _equipmentRentalService.GetAll().FirstOrDefault(x => x.RentalId == EquipmentRental.RentalId);
            var getEquipment = _equipmentService.GetAll().FirstOrDefault(x => x.EquipmentId == EquipmentRental.EquipmentId);
            if (EquipmentRental.Isapprove is true && EquipmentRental.IsReturn is false)
            {
                //foreach (var item in euquipment)
                //{
                euquipment.Isapprove = true;
                euquipment.IsReturn = EquipmentRental.IsReturn;
                getEquipment.Quantity -= 1;

                _equipmentService.Update(getEquipment);
                //}
            }
            else if (EquipmentRental.Isapprove is true && EquipmentRental.IsReturn is true)
            {
                euquipment.Isapprove = true;
                getEquipment.Quantity += 1;
                euquipment.IsReturn = EquipmentRental.IsReturn;
                _equipmentService.Update(getEquipment);
            }
            else if (EquipmentRental.Isapprove is false && EquipmentRental.IsReturn is false)
            {
                //foreach (var item in euquipment)
                //{
                euquipment.Isapprove = false;
                euquipment.IsReturn = EquipmentRental.IsReturn;
                //}
            }
            _equipmentRentalService.Update(euquipment);

            return RedirectToPage("./EquipmentRentalView");
        }
    }
}
