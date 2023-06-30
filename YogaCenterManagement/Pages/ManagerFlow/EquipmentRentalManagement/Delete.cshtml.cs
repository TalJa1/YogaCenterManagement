using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.DAO;
using Repository.Models;

namespace YogaCenterManagement.Pages.ManagerFlow.EquipmentRentalManagement
{
    public class DeleteModel : PageModel
    {
        private readonly MemberService _memberService;
        private readonly EquipmentService _equipmentService;
        private readonly EquipmentRentalService _equipmentRentalService;

        public DeleteModel(EquipmentRentalService equipmentRentalService1, EquipmentService equipmentService, EquipmentRentalService equipmentRentalService)
        {
            _equipmentRentalService = equipmentRentalService;
            _equipmentService = equipmentService;
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

            var equipmentrental = _equipmentRentalService.GetAll().FirstOrDefault(m => m.RentalId == id);

            if (equipmentrental == null)
            {
                return NotFound();
            }
            else 
            {
                EquipmentRental = equipmentrental;
            }
            return Page();
        }

        public IActionResult OnPostAsync(int? id)
        {
            if (id == null || _equipmentRentalService.GetAll() == null)
            {
                return NotFound();
            }
            var equipmentrental = _equipmentRentalService.getById(id);

            if (equipmentrental != null)
            {
                EquipmentRental = equipmentrental;
                _equipmentRentalService.Delete(equipmentrental);
            }

            return RedirectToPage("./Index");
        }
    }
}
