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
    public class IndexModel : PageModel
    {
        private readonly EquipmentRentalService _equipmentRentalService;
        private readonly MemberService _memberService;
        private readonly EquipmentService _equipmentService; 

        public IndexModel(MemberService memberService, EquipmentService equipmentService, EquipmentRentalService equipmentRentalService)
        {
            _memberService = memberService;
            _equipmentService = equipmentService;
            _equipmentRentalService = equipmentRentalService;
        }

        public IList<EquipmentRental> EquipmentRental { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_equipmentRentalService.GetAll() != null)
            {
                EquipmentRental = _equipmentRentalService.GetAll(include: x => x.Include(x => x.Member).Include(x => x.Equipment));
                
            }
        }
    }
}
