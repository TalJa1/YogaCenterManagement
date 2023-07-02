﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.DAO;
using Repository.Models;

namespace YogaCenterManagement.Pages.ManagerFlow.ClassRequestChange
{
    public class IndexModel : PageModel
    {
        private readonly ClassChangeRequestService _classChangeRequestService;
        private readonly ClassService _classService;
        private readonly MemberService _memberService;

        public IndexModel(ClassChangeRequestService classChangeRequestService, ClassService classService, MemberService memberService)
        {
            _classChangeRequestService = classChangeRequestService;
            _classService = classService;
            _memberService = memberService;
        }

        public IList<ClassChangeRequest> ClassChangeRequest { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_classChangeRequestService.GetAll() != null)
            {
                ClassChangeRequest = _classChangeRequestService.GetAll(include: x => x.Include(x =>x.Member).Include(x => x.OldClass).Include(x => x.NewClass));
            }
        }
    }
}