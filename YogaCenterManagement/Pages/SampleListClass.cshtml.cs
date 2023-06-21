using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.DAO;
using Repository.Models;

namespace YogaCenterManagement.Pages
{
    public class SampleListClassModel : PageModel
    {
        private readonly ClassService classService;

        public SampleListClassModel(ClassService classService)
        {
            this.classService = classService;
        }

        public IList<Class> Class { get;set; } = default!;

        public void OnGetAsync()
        {
            if (classService.GetAll() != null)
            {
                Class = classService.GetAll();
            }
        }
    }
}
