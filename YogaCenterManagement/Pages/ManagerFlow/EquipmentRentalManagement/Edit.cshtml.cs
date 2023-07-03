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
            if (id == null || _equipmentRentalService.GetAll() == null)
            {
                return NotFound();
            }

            var equipmentrental =  _equipmentRentalService.GetAll().FirstOrDefault(m => m.RentalId == id);
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
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _equipmentRentalService.Update(EquipmentRental);

            return RedirectToPage("./EquipmentRentalView");
        }
    }
}
