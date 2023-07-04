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
    public class EquipmentRentalModel : PageModel
    {
        private readonly EquipmentRentalService _equipmentRentalService;
        private readonly MemberService _memberService;
        private readonly EquipmentService _equipmentService;

        public EquipmentRentalModel(EquipmentRentalService equipmentRentalService, MemberService memberService, EquipmentService equipmentService)
        {
            _equipmentRentalService = equipmentRentalService;
            _memberService = memberService;
            _equipmentService = equipmentService;
        }

        public IList<EquipmentRental> EquipmentRental { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_equipmentRentalService.GetAll() != null)
            {
                EquipmentRental = _equipmentRentalService.GetAll(include: x => x.Include(x => x.Member).Include(x => x.Equipment)).OrderByDescending(x=>x.ReturnDate).ThenByDescending(x=>x.ReturnDate).ToList();
                
            }
        }
    }
}
