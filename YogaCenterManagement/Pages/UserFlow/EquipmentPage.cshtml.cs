using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Repository.DAO;

namespace YogaCenterManagement.Pages.UserFlow
{
    public class EquipmentPageModel : PageModel
    {
        private readonly EquipmentService equipmentService;
        private readonly CartService _cartService;

        public EquipmentPageModel(EquipmentService equipmentService, CartService cartService)
        {
            this.equipmentService = equipmentService;
            _cartService = cartService;
        }

        public IList<Equipment> Equipment { get;set; } = default!;

        public void OnGetAsync()
        {
            if (equipmentService.GetAll() != null)
            {
                Equipment = equipmentService.GetAll();
            }
        }

        public IActionResult OnPostAddToCart(int equipmentId)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToPage("HomePage");
            }
            if (!HttpContext.Session.GetString("email").Equals("admin@admin.com"))
            {
                _cartService.AddItemToCart(equipmentId);
                return RedirectToPage("EquipmentPage");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
