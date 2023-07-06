using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.DAO;
using Repository.Models;

namespace YogaCenterManagement.Pages.UserFlow
{
    public class CartPageModel : PageModel
    {
        private readonly CartService cartService;
        private readonly EquipmentService equipmentService;
        private readonly EquipmentRentalService equipmentRentalService;
        private readonly MemberService memberService;

        public CartPageModel(CartService cartService, EquipmentService equipmentService, EquipmentRentalService equipmentRentalService, MemberService memberService)
        {
            this.cartService = cartService;
            this.equipmentService = equipmentService;
            this.equipmentRentalService = equipmentRentalService;
            this.memberService = memberService;
        }
        [BindProperty]
        public IList<Dictionary<Equipment, int>> Equip { get; set; } = new List<Dictionary<Equipment, int>>();

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToPage("HomePage");
            }
            if (!HttpContext.Session.GetString("email").Equals("admin@admin.com"))
            {
                foreach (var item1 in cartService.GetCartItems())
                {
                    Equipment fl = equipmentService.GetAll().FirstOrDefault(m => m.EquipmentId == item1.Key);

                    if (fl != null)
                    {
                        var dictionary = new Dictionary<Equipment, int>();
                        dictionary.Add(fl, item1.Value);
                        Equip.Add(dictionary);
                    }
                }
                return Page();
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult OnPostRemoveItem(int index, int equipmentId)
        {
            cartService.RemoveItemFromCart(equipmentId);
            return RedirectToPage();
        }

        public IActionResult OnPostCreateOrder(List<int> EquipmentId)
        {
            var memberEmail = HttpContext.Session.GetString("email");
            Member membercheck = memberService.GetAll().FirstOrDefault(m => m.Email.Equals(memberEmail));

            if (EquipmentId.Count >= 1)
            {
                foreach (var equipmentId in EquipmentId)
                {
                    var equipment = equipmentService.GetAll().FirstOrDefault(m => m.EquipmentId == equipmentId);

                    if (equipment != null && membercheck != null)
                    {
                        EquipmentRental equipmentRental = new EquipmentRental
                        {
                            MemberId = membercheck.MemberId,
                            EquipmentId = equipmentId,
                            RentalDate = DateTime.Now,
                            ReturnDate = DateTime.Now.AddDays(1),
                            Isapprove = false
                        };
                        equipmentRentalService.Add(equipmentRental);
                    }
                    else
                    {
                        ViewData["err"] = "Can not send request now, error occur";
                        return RedirectToPage("CartPage");
                    }
                }
            }
            else
            {
                ViewData["cartNull"] = "Cannot create request.";
                return Page();
            }

            cartService.ClearCart();

            return RedirectToPage("RentalHistory");
        }

    }
}
